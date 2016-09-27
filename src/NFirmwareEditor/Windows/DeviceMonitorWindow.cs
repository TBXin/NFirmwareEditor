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
					ComConnector_MessageReceived(string.Format("STANDBY BATT=417 RES=98 RESM={0}", resm));
					Thread.Sleep(500);
				}
			}) { IsBackground = true }.Start();
#endif
		}

		#region Overrides of EditorDialogWindow
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (keyData.HasFlag(Keys.Space))
			{

			}

			return base.ProcessCmdKey(ref msg, keyData);
		}
		#endregion

		private void InitializeControls()
		{
			m_seriesData = new Dictionary<string, SeriesRelatedData>
			{
				{ SensorKeys.BatteryVoltage, new SeriesRelatedData(Color.DarkSlateGray, BatteryCheckBox, panel1) },
				{ SensorKeys.Power, new SeriesRelatedData(Color.LimeGreen, PowerCheckBox, panel2) },
				{ SensorKeys.OutputVoltage, new SeriesRelatedData(Color.LightSkyBlue, OutputVoltageCheckBox, panel3) },
				{ SensorKeys.OutputCurrent, new SeriesRelatedData(Color.Orange, OutputCurrentCheckBox, panel4) },
				{ SensorKeys.Resistance, new SeriesRelatedData(Color.BlueViolet, ResistanceCheckBox, panel5) },
				{ SensorKeys.RealResistance, new SeriesRelatedData(Color.Violet, RealResistanceCheckBox, panel6) },
				{ SensorKeys.Temperature, new SeriesRelatedData(Color.DeepPink, TemperatureCheckBox, panel7) },
				{ SensorKeys.BoardTemperature, new SeriesRelatedData(Color.SaddleBrown, BoardTemperatureCheckBox, panel8) }
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

				var series = CreateSeries(seriesName, data.Color);
				MainChart.Series.Add(series);

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
			MainChart.Series[seriesName].Enabled = checkbox.Checked;
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
					if (!MainChart.Series.IsUniqueName(SensorKeys.BatteryVoltage) && sensors.BatteryVoltage >= 0)
						MainChart.Series[SensorKeys.BatteryVoltage].Points.Add(sensors.BatteryVoltage);

					if (!MainChart.Series.IsUniqueName(SensorKeys.OutputVoltage) && sensors.OutputVoltage >= 0)
						MainChart.Series[SensorKeys.OutputVoltage].Points.Add(sensors.OutputVoltage);

					if (!MainChart.Series.IsUniqueName(SensorKeys.OutputCurrent) && sensors.OutputCurrent >= 0)
						MainChart.Series[SensorKeys.OutputCurrent].Points.Add(sensors.OutputCurrent);

					if (!MainChart.Series.IsUniqueName(SensorKeys.Resistance) && sensors.Resistance >= 0)
						MainChart.Series[SensorKeys.Resistance].Points.Add(sensors.Resistance);

					if (!MainChart.Series.IsUniqueName(SensorKeys.RealResistance) && sensors.RealResistance >= 0)
						MainChart.Series[SensorKeys.RealResistance].Points.Add(sensors.RealResistance);

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
			public SeriesRelatedData(Color color, CheckBox checkBox, Panel panel)
			{
				Color = color;
				CheckBox = checkBox;
				Panel = panel;
			}

			public Color Color { get; private set; }

			public CheckBox CheckBox { get; private set; }

			public Panel Panel { get; private set; }
		}
	}
}
