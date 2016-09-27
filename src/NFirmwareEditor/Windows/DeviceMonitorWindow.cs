using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using JetBrains.Annotations;
using NFirmwareEditor.Core;
using NFirmwareEditor.Managers;
using NFirmwareEditor.Models;

namespace NFirmwareEditor.Windows
{
	internal partial class DeviceMonitorWindow : EditorDialogWindow
	{
		private const int MaxItems = 50;
		private IDictionary<string, SeriesRelatedData> m_seriesData;

		private readonly COMConnector m_comConnector;

		public DeviceMonitorWindow([NotNull] COMConnector comConnector)
		{
			if (comConnector == null) throw new ArgumentNullException("comConnector");
			m_comConnector = comConnector;

			InitializeComponent();
			InitializeControls();

			m_comConnector.MessageReceived += ComConnector_MessageReceived;
			m_comConnector.Disconnected += ComConnector_Disconnected;

			Load += (s, e) => EnsureConnection();
			Closing += (s, e) => Safe.Execute(() =>
			{
				m_comConnector.MessageReceived -= ComConnector_MessageReceived;
				m_comConnector.Disconnected -= ComConnector_Disconnected;
				m_comConnector.Disconnect();
			});

#if DEBUG
			new Thread(() =>
			{
				var rnd = new Random();
				while (true)
				{
					var resm = rnd.Next(990, 1020);
					var temp = m_cels ? rnd.Next(100, 120) : rnd.Next(300, 350);
					ComConnector_MessageReceived(string.Format("STANDBY BATT=417 RES=98 RESM={0} CELS={1} TEMP={2}", resm, m_cels ? 1 : 0, temp));
					Thread.Sleep(500);
				}
			}) { IsBackground = true }.Start();
#endif
		}

#if DEBUG
		private bool m_cels;
		#region Overrides of EditorDialogWindow
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (keyData.HasFlag(Keys.Space))
			{
				m_cels = !m_cels;
			}

			return base.ProcessCmdKey(ref msg, keyData);
		}
		#endregion
#endif
		private void InitializeControls()
		{
			m_seriesData = new Dictionary<string, SeriesRelatedData>
			{
				{ SensorKeys.BatteryVoltage, new SeriesRelatedData(Color.DarkSlateGray, BatteryCheckBox, panel1, BatteryVoltageLabel, "{0} V") },
				{ SensorKeys.Power, new SeriesRelatedData(Color.LimeGreen, PowerCheckBox, panel2, PowerLabel, "{0} W") },
				{ SensorKeys.OutputVoltage, new SeriesRelatedData(Color.LightSkyBlue, OutputVoltageCheckBox, panel3, OutputVoltageLabel, "{0} V") },
				{ SensorKeys.OutputCurrent, new SeriesRelatedData(Color.Orange, OutputCurrentCheckBox, panel4, OutputCurrentLabel, "{0} A") },
				{ SensorKeys.Resistance, new SeriesRelatedData(Color.BlueViolet, ResistanceCheckBox, panel5, ResistanceLabel, "{0} Ω") },
				{ SensorKeys.RealResistance, new SeriesRelatedData(Color.Violet, RealResistanceCheckBox, panel6, RealResistanceLabel, "{0} Ω") },
				{ SensorKeys.Temperature, new SeriesRelatedData(Color.DeepPink, TemperatureCheckBox, panel7, TemperatureLabel, "{0} °C") },
				{ SensorKeys.BoardTemperature, new SeriesRelatedData(Color.SaddleBrown, BoardTemperatureCheckBox, panel8, BoardTemperatureLabel, "{0} °C") }
			};

			InitializeChart();
			InitializeSeries();
		}

		private void InitializeChart()
		{
			MainChart.Palette = ChartColorPalette.Pastel;
			var area = new ChartArea();
			{
				area.AxisX.IsMarginVisible = false;
				area.AxisX.Maximum = MaxItems;
				area.AxisX.Enabled = AxisEnabled.False;
				area.AxisY.Enabled = AxisEnabled.False;
			}
			MainChart.ChartAreas.Add(area);
		}

		private void InitializeSeries()
		{
			foreach (var kvp in m_seriesData)
			{
				var seriesName = kvp.Key;
				var data = kvp.Value;

				data.Seires = CreateSeries(seriesName, data.Color);
				MainChart.Series.Add(data.Seires);

				data.CheckBox.Tag = seriesName;
				data.CheckBox.CheckedChanged += SeriesCheckBox_CheckedChanged;
				data.CheckBox.Checked = true;
				data.Panel.BackColor = data.Color;
			}
		}

		private void SeriesCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			var checkbox = sender as CheckBox;
			if (checkbox == null || checkbox.Tag == null || string.IsNullOrEmpty(checkbox.Tag.ToString())) return;

			var seriesName = checkbox.Tag.ToString();
			m_seriesData[seriesName].Seires.Enabled = checkbox.Checked;
		}

		private void EnsureConnection()
		{
#if DEBUG
			return;
#endif
			while (true)
			{
				var port = m_comConnector.Connect();
				if (!string.IsNullOrEmpty(port))
				{
					m_comConnector.Send("M1");
					break;
				}

				var result = InfoBox.Show
				(
					"No compatible USB devices are connected." +
					"\n\n" +
					"To continue, please connect one." +
					"\n\n" +
					"If one already IS connected, try unplugging and plugging it back in. The cable may be loose.",
					MessageBoxButtons.OKCancel
				);
				if (result == DialogResult.Cancel)
				{
					UpdateUI(() => Close());
					return;
				}
			}
		}

		private Series CreateSeries(string name, Color color)
		{
			var series = new Series
			{
				Name = name,
				ChartType = SeriesChartType.Line,
				YValueType = ChartValueType.Double,
				Color = color,
				BorderWidth = 2,
				SmartLabelStyle =
				{
					Enabled = true,
					AllowOutsidePlotArea = LabelOutsidePlotAreaStyle.Yes,
					IsMarkerOverlappingAllowed = false,
					MovingDirection = LabelAlignmentStyles.Right
				}
			};
			return series;
		}

		private void ComConnector_Disconnected()
		{
			EnsureConnection();
		}

		private void ComConnector_MessageReceived(string message)
		{
			if (string.IsNullOrEmpty(message)) return;

			var dataMessages = message.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
			foreach (var dataMessage in dataMessages)
			{
				var sensors = DeviceSensorsData.Parse(dataMessage);
				if (sensors == null) return;

				UpdateUI(() =>
				{
					var isCelcius = sensors[SensorKeys.Celcius] > 0;
					m_seriesData[SensorKeys.Temperature].SetLastValueFormat(isCelcius ? "{0} °C" : "{0} °F");

					foreach (var kvp in m_seriesData)
					{
						var sensorName = kvp.Key;
						var data = kvp.Value;

						var readings = sensors[sensorName];
						data.Seires.Points.Add(readings);
						data.SetLastValue(readings == 0 ? (float?)null : readings);
					}

					foreach (var series in MainChart.Series)
					{
						while (series.Points.Count > MaxItems)
						{
							series.Points.RemoveAt(0);
						}

						if (series.Points.Count > 0)
						{
							var point = series.Points[series.Points.Count - 1];
							if (Math.Abs(point.YValues[0]) > 0.01)
							{
								point.Label = Math.Round(point.YValues[0], 2).ToString(CultureInfo.InvariantCulture);
								if (series.Points.Count > 1)
								{
									series.Points[series.Points.Count - 2].Label = null;
								}
							}
						}
					}

					MainChart.ChartAreas[0].RecalculateAxesScale();
					MainChart.ResetAutoValues();
				});
			}
		}

		private class SeriesRelatedData
		{
			private readonly Label m_lastValueLabel;
			private string m_labelFormat;

			public SeriesRelatedData(Color color, CheckBox checkBox, Panel panel, Label lastValueLabel, string labelFormat)
			{
				m_lastValueLabel = lastValueLabel;
				m_labelFormat = labelFormat;

				Color = color;
				CheckBox = checkBox;
				Panel = panel;
			}

			public Color Color { get; private set; }

			public CheckBox CheckBox { get; private set; }

			public Panel Panel { get; private set; }

			public Series Seires { get; set; }

			public void SetLastValueFormat(string format)
			{
				m_labelFormat = format;
			}

			public void SetLastValue(float? value)
			{
				m_lastValueLabel.Text = Seires.Enabled && value.HasValue
					? string.Format(CultureInfo.InvariantCulture, m_labelFormat, value)
					: "?";
			}
		}
	}
}
