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

/*#if DEBUG
			new Thread(() =>
			{
				var rnd = new Random();
				while (true)
				{
					var resm = rnd.Next(990, 1020);
					var temp = m_cels ? rnd.Next(100, 120) : rnd.Next(300, 350);
					var brd = rnd.Next(28, 31);
					var message = string.Format("STANDBY BATT=417 RES=98 RESM={0} CELS={1} TEMP={2} BRD={3}", resm, m_cels ? 1 : 0, temp, brd);
					if (m_amps) message += " CUR=" + rnd.Next(10, 50);
					ComConnector_MessageReceived(message);
					Thread.Sleep(100);
				}
			}) { IsBackground = true }.Start();
#endif*/
		}

#if DEBUG
		private bool m_cels;
		private bool m_amps;
		#region Overrides of EditorDialogWindow
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (keyData.HasFlag(Keys.Space))
			{
				m_amps = !m_amps;
				//m_cels = !m_cels;
			}

			return base.ProcessCmdKey(ref msg, keyData);
		}
		#endregion
#endif
		private void InitializeControls()
		{
			var batteryLimits = new[] { new ValueLimit<float, int>(3.0f, 80), new ValueLimit<float, int>(4.2f, 100) };
			var powerLimits = new[] { new ValueLimit<float, int>(1, 50), new ValueLimit<float, int>(75, 80) };
			var powerSetLimits = new[] { new ValueLimit<float, int>(1, 50), new ValueLimit<float, int>(75, 80) };
			var tempLimits = new[] { new ValueLimit<float, int>(100, 50), new ValueLimit<float, int>(600, 80) };
			var tempSetLimits = new[] { new ValueLimit<float, int>(100, 50), new ValueLimit<float, int>(600, 80) };
			var resistanceLimits = new[] { new ValueLimit<float, int>(0.05f, 30), new ValueLimit<float, int>(3f, 50) };
			var realResistanceLimits = new[] { new ValueLimit<float, int>(0.05f, 30), new ValueLimit<float, int>(3f, 50) };
			var outputVoltageLimits = new[] { new ValueLimit<float, int>(1, 10), new ValueLimit<float, int>(10, 30) };
			var outputCurrentLimits = new[] { new ValueLimit<float, int>(1, 10), new ValueLimit<float, int>(25, 30) };
			var boardTemperatureLimits = new[] { new ValueLimit<float, int>(0, 1), new ValueLimit<float, int>(99, 10) };

			m_seriesData = new Dictionary<string, SeriesRelatedData>
			{
				{ SensorsKeys.BatteryVoltage, new SeriesRelatedData(Color.DarkSlateGray, BatteryCheckBox, BatteryPanel, BatteryVoltageLabel, "{0} V", batteryLimits) },
				{ SensorsKeys.Power, new SeriesRelatedData(Color.LimeGreen, PowerCheckBox, PowerPanel, PowerLabel, "{0} W", powerLimits) },
				{ SensorsKeys.PowerSet, new SeriesRelatedData(Color.Green, PowerSetCheckBox, PowerSetPanel, PowerSetLabel, "{0} W", powerSetLimits) },
				{ SensorsKeys.Temperature, new SeriesRelatedData(Color.Red, TemperatureCheckBox, TemperaturePanel, TemperatureLabel, "{0} °C", tempLimits) },
				{ SensorsKeys.TemperatureSet, new SeriesRelatedData(Color.DarkRed, TemperatureSetCheckBox, TemperatureSetPanel, TemperatureSetLabel, "{0} °C", tempSetLimits) },
				{ SensorsKeys.OutputCurrent, new SeriesRelatedData(Color.Orange, OutputCurrentCheckBox, OutputCurrentPanel, OutputCurrentLabel, "{0} A", outputCurrentLimits) },
				{ SensorsKeys.OutputVoltage, new SeriesRelatedData(Color.LightSkyBlue, OutputVoltageCheckBox, OutputVoltagePanel, OutputVoltageLabel, "{0} V", outputVoltageLimits) },
				{ SensorsKeys.Resistance, new SeriesRelatedData(Color.Violet, ResistanceCheckBox, ResistancePanel, ResistanceLabel, "{0} Ω", resistanceLimits) },
				{ SensorsKeys.RealResistance, new SeriesRelatedData(Color.BlueViolet, RealResistanceCheckBox, RealResistancePanel, RealResistanceLabel, "{0} Ω", realResistanceLimits) },
				{ SensorsKeys.BoardTemperature, new SeriesRelatedData(Color.SaddleBrown, BoardTemperatureCheckBox, BoardTemperaturePanel, BoardTemperatureLabel, "{0} °C", boardTemperatureLimits) }
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

		private void EnsureConnection()
		{
/*#if DEBUG
			return;
#endif*/
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
					UpdateUI(Close);
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

		private float Interpolate(float value, IList<ValueLimit<float, int>> lowHigh)
		{
			var low = lowHigh[0];
			var high = lowHigh[1];

			if (value > high.Value) return high.Value;
			if (value < low.Value) return low.Value;

			return low.Limit + (value - low.Value) / (high.Value - low.Value) * (high.Limit - low.Limit);
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
					var isCelcius = sensors[SensorsKeys.Celcius] > 0;
					m_seriesData[SensorsKeys.Temperature].SetLastValueFormat(isCelcius ? "{0} °C" : "{0} °F");
					m_seriesData[SensorsKeys.TemperatureSet].SetLastValueFormat(isCelcius ? "{0} °C" : "{0} °F");

					foreach (var kvp in m_seriesData)
					{
						var sensorName = kvp.Key;
						var data = kvp.Value;
						var readings = sensorName == SensorsKeys.Power
							? sensors[SensorsKeys.OutputCurrent] * sensors[SensorsKeys.OutputVoltage]
							: sensors[sensorName];

						var interpolatedValue = Interpolate(readings, data.InterpolationLimits);

						var point = new DataPoint();
						if (Math.Abs(readings) > 0.001)
						{
							point.YValues = new double[] { interpolatedValue };
							point.Label = Math.Round(readings, 3).ToString(CultureInfo.InvariantCulture);
							data.SetLastValue(readings);
						}
						else
						{
							point.IsEmpty = true;
							data.SetLastValue(null);
						}
						data.Seires.Points.Add(point);
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
							if (!point.IsEmpty)
							{
								//point.Label = Math.Round(point.YValues[0], 2).ToString(CultureInfo.InvariantCulture);
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

		private void SeriesCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			var checkbox = sender as CheckBox;
			if (checkbox == null || checkbox.Tag == null || string.IsNullOrEmpty(checkbox.Tag.ToString())) return;

			var seriesName = checkbox.Tag.ToString();
			m_seriesData[seriesName].Seires.Enabled = checkbox.Checked;
		}

		private class SeriesRelatedData
		{
			private readonly Label m_lastValueLabel;
			private string m_labelFormat;

			public SeriesRelatedData(Color color, CheckBox checkBox, Panel panel, Label lastValueLabel, string labelFormat, [NotNull] ValueLimit<float,int>[] interpolationLimits)
			{
				if (interpolationLimits == null || interpolationLimits.Length != 2) throw new ArgumentNullException("interpolationLimits");

				m_lastValueLabel = lastValueLabel;
				m_labelFormat = labelFormat;

				Color = color;
				CheckBox = checkBox;
				Panel = panel;
				InterpolationLimits = interpolationLimits;
			}

			public Color Color { get; private set; }

			public CheckBox CheckBox { get; private set; }

			public Panel Panel { get; private set; }

			public ValueLimit<float, int>[] InterpolationLimits { get; private set; }

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

		private class ValueLimit<TValue, TLimit>
		{
			public ValueLimit(TValue value, TLimit limit)
			{
				Value = value;
				Limit = limit;
			}

			public TValue Value { get; private set; }

			public TLimit Limit { get; private set; }
		}
	}
}
