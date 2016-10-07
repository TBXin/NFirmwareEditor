using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
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
		private const int MaxItems = 1200;

		private readonly ApplicationConfiguration m_configuration;
		private readonly USBConnector m_usbConnector;
		private readonly COMConnector m_comConnector;

		private IDictionary<string, SeriesRelatedData> m_seriesData;
		private TimeSpan m_timeFrame = TimeSpan.FromSeconds(10);
		private DateTime? m_startTime;
		private bool m_isTracking = true;

		private ContextMenu m_timeFrameMenu;
		private ContextMenu m_puffsMenu;
		private bool m_isComPortConnected;
		private bool m_realClosing;
		private bool m_isPaused;

		public bool IsTracking
		{
			get { return m_isTracking; }
			set
			{
				m_isTracking = value;
				TrackingButton.Enabled = !m_isTracking;
			}
		}

		public DeviceMonitorWindow([NotNull] ApplicationConfiguration configuration, [NotNull] USBConnector usbConnector, [NotNull] COMConnector comConnector)
		{
			if (configuration == null) throw new ArgumentNullException("configuration");
			if (usbConnector == null) throw new ArgumentNullException("usbConnector");
			if (comConnector == null) throw new ArgumentNullException("comConnector");

			m_configuration = configuration;
			m_usbConnector = usbConnector;
			m_comConnector = comConnector;

			InitializeComponent();
			InitializeControls();
			InitializeChart();
			InitializeSeries();
			InitializeContextMenus();

			m_comConnector.Connected += COMConnector_Connected;
			m_comConnector.Disconnected += COMConnector_Disconnected;
			m_comConnector.MonitorDataReceived += ComConnector_MonitorDataReceived;

			Load += (s, e) => EnsureConnection();
			Closing += (s, e) =>
			{
				if (m_realClosing) return;

				SaveCheckedSeries();
				e.Cancel = true;
				m_realClosing = true;
				new Thread(() =>
				{
					// The serial port cannot close until the DataReceived event handler stops running. 
					// But the Invoke() call cannot complete until the UI thread goes idle and pumps the message loop. 
					// It isn't idle, it is stuck in the Close() call. So the event handler cannot make progress because 
					// it is stuck in the Invoke() call and your main thread cannot make progress because 
					// it is stuck in the Close() call, deadlock city.
					if (m_usbConnector.IsDeviceConnected)
					{
						m_usbConnector.SetupDeviceMonitor(false);
					}
					m_comConnector.MonitorDataReceived -= ComConnector_MonitorDataReceived;
					m_comConnector.Connected -= COMConnector_Connected;
					m_comConnector.Disconnected -= COMConnector_Disconnected;
					if (m_isComPortConnected)
					{
						m_comConnector.Disconnect();
					}
					UpdateUI(Close);
				}).Start();
			};
		}

		private void InitializeControls()
		{
			var batteryLimits = new[] { new ValueLimit<float, int>(3.0f, 80), new ValueLimit<float, int>(4.2f, 95) };
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
				{
					SensorsKeys.BatteryVoltage,
					new SeriesRelatedData(Color.DarkSlateGray, BatteryCheckBox, BatteryPanel, BatteryVoltageLabel, "{0} V", batteryLimits)
				},
				{
					SensorsKeys.Power,
					new SeriesRelatedData(Color.LimeGreen, PowerCheckBox, PowerPanel, PowerLabel, "{0} W", powerLimits)
				},
				{
					SensorsKeys.PowerSet,
					new SeriesRelatedData(Color.Green, PowerSetCheckBox, PowerSetPanel, PowerSetLabel, "{0} W", powerSetLimits)
				},
				{
					SensorsKeys.Temperature,
					new SeriesRelatedData(Color.Red, TemperatureCheckBox, TemperaturePanel, TemperatureLabel, "{0} °C", tempLimits)
				},
				{
					SensorsKeys.TemperatureSet,
					new SeriesRelatedData(Color.DarkRed, TemperatureSetCheckBox, TemperatureSetPanel, TemperatureSetLabel, "{0} °C", tempSetLimits)
				},
				{
					SensorsKeys.OutputCurrent,
					new SeriesRelatedData(Color.Orange, OutputCurrentCheckBox, OutputCurrentPanel, OutputCurrentLabel, "{0} A", outputCurrentLimits)
				},
				{
					SensorsKeys.OutputVoltage,
					new SeriesRelatedData(Color.LightSkyBlue, OutputVoltageCheckBox, OutputVoltagePanel, OutputVoltageLabel, "{0} V", outputVoltageLimits)
				},
				{
					SensorsKeys.Resistance,
					new SeriesRelatedData(Color.Violet, ResistanceCheckBox, ResistancePanel, ResistanceLabel, "{0} Ω", resistanceLimits)
				},
				{
					SensorsKeys.RealResistance,
					new SeriesRelatedData(Color.BlueViolet, RealResistanceCheckBox, RealResistancePanel, RealResistanceLabel, "{0} Ω", realResistanceLimits)
				},
				{
					SensorsKeys.BoardTemperature,
					new SeriesRelatedData(Color.SaddleBrown, BoardTemperatureCheckBox, BoardTemperaturePanel, BoardTemperatureLabel, "{0} °C", boardTemperatureLimits)
				}
			};

			PauseButton.Click += (s, e) =>
			{
				m_isPaused = !m_isPaused;
				PauseButton.Text = m_isPaused ? "Resume" : "Pause";
			};

			TrackingButton.Click += (s, e) => ChangeTimeFrameAndTrack(m_timeFrame);
		}

		private void InitializeChart()
		{
			MainChart.Palette = ChartColorPalette.Pastel;
			var area = new ChartArea();
			{
				area.AxisX.IsMarginVisible = false;
				area.AxisX.MajorGrid.Enabled = true;
				area.AxisX.MajorGrid.LineColor = Color.FromArgb(230, 230, 230);
				area.AxisX.MajorTickMark.TickMarkStyle = TickMarkStyle.None;
				area.AxisX.LabelStyle.Enabled = false;
				area.AxisX.LineColor = Color.DarkGray;
				area.AxisX.IntervalOffsetType = DateTimeIntervalType.Milliseconds;
				area.AxisX.ScaleView.Zoomable = true;
				area.AxisX.ScrollBar.Enabled = false;

				area.AxisY.IsMarginVisible = false;
				area.AxisY.MajorGrid.Enabled = true;
				area.AxisY.MajorGrid.LineColor = Color.FromArgb(230, 230, 230);
				area.AxisY.MajorTickMark.TickMarkStyle = TickMarkStyle.None;
				area.AxisY.LabelStyle.Enabled = false;
				area.AxisY.LineColor = Color.DarkGray;
			}
			var valueAnnotation = new CalloutAnnotation
			{
				AxisX = area.AxisX,
				AxisY = area.AxisY
			};
			MainChart.ChartAreas.Add(area);
			MainChart.Annotations.Add(valueAnnotation);
			MainChart.MouseMove += (s, e) =>
			{
				var result = MainChart.HitTest(e.X, e.Y);

				if (result.ChartElementType != ChartElementType.DataPoint ||
				    result.PointIndex < 0 ||
				    result.Series.Points.Count <= result.PointIndex)
				{
					return;
				}

				if (result.Series.Points.Count <= result.PointIndex) return;
				var point = result.Series.Points[result.PointIndex];

				valueAnnotation.BeginPlacement();

				// You must set AxisX before binding to xValue!
				valueAnnotation.AnchorX = point.XValue;
				valueAnnotation.AnchorY = point.YValues[0];
				valueAnnotation.Text = point.Tag.ToString();

				valueAnnotation.EndPlacement();
				valueAnnotation.Visible = true;
			};

			MainChartScrollBar.Scroll += (s, e) => IsTracking = MainChartScrollBar.Value == MainChartScrollBar.Maximum;
			MainChartScrollBar.ValueChanged += (s, e) => ScrollChart(false);
		}

		private void InitializeSeries()
		{
			foreach (var kvp in m_seriesData)
			{
				var seriesName = kvp.Key;
				var data = kvp.Value;

				data.Seires = CreateSeries(seriesName, data.Color);
				MainChart.Series.Add(data.Seires);

				bool isChecked;
				if (!m_configuration.DeviceMonitorSeries.TryGetValue(seriesName, out isChecked)) isChecked = true;

				data.CheckBox.Tag = seriesName;
				data.CheckBox.Checked = data.Seires.Enabled = isChecked;
				data.CheckBox.CheckedChanged += SeriesCheckBox_CheckedChanged;
				data.Panel.BackColor = data.Color;
			}
		}

		private void InitializeContextMenus()
		{
			m_timeFrameMenu = new ContextMenu(new[]
			{
				new MenuItem("10 seconds", (s, e) => ChangeTimeFrameAndTrack(TimeSpan.FromSeconds(10))),
				new MenuItem("30 seconds", (s, e) => ChangeTimeFrameAndTrack(TimeSpan.FromSeconds(30))),
				new MenuItem("1 minute", (s, e) => ChangeTimeFrameAndTrack(TimeSpan.FromMinutes(1))),
				new MenuItem("2 minutes", (s, e) => ChangeTimeFrameAndTrack(TimeSpan.FromMinutes(2))),
				new MenuItem("5 minutes", (s, e) => ChangeTimeFrameAndTrack(TimeSpan.FromMinutes(5)))
			});
			TimeFrameButton.Click += (s, e) =>
			{
				var control = (Control)s;
				m_timeFrameMenu.Show(control, new Point(control.Width, 0));
			};

			m_puffsMenu = new ContextMenu();
			for (var i = 1; i <= 9; i++)
			{
				var seconds = i;
				m_puffsMenu.MenuItems.Add(seconds + (seconds == 1 ? " second" : " seconds"), (s, e) => PuffMenuItem_Click(seconds));
			}
			PuffButton.Click += (s, e) =>
			{
				var control = (Control)s;
				m_puffsMenu.Show(control, new Point(control.Width, 0));
			};
		}

		private bool EnsureConnection()
		{
			if (m_isComPortConnected) return true;
			if (!m_usbConnector.IsDeviceConnected)
			{
				var result = InfoBox.Show
				(
					"No compatible USB devices are connected." +
					"\n\n" +
					"To continue, please connect one." +
					"\n\n" +
					"If one already IS connected, try unplugging and plugging it back in. The cable may be loose.",
					MessageBoxButtons.OKCancel
				);

				if (result == DialogResult.OK)
				{
					return EnsureConnection();
				}
				if (result == DialogResult.Cancel)
				{
					UpdateUI(Close);
					return false;
				}
			}

			var port = m_comConnector.Connect();
			if (string.IsNullOrEmpty(port))
			{
				var result = InfoBox.Show
				(
					"Compatible USB device was found." +
					"\n\n" +
					"But VCOM connection could not be established." +
					"\n\n" +
					"Would you like to enable VCOM mode to continue?",
					MessageBoxButtons.OKCancel
				);

				if (result == DialogResult.OK)
				{
					m_usbConnector.EnableCOM();
					Thread.Sleep(2000);
					return EnsureConnection();
				}
				if (result == DialogResult.Cancel)
				{
					UpdateUI(Close);
					return false;
				}
			}
			return true;
		}

		private Series CreateSeries(string name, Color color)
		{
			var series = new Series
			{
				Name = name,
				ChartType = SeriesChartType.Line,
				XValueType = ChartValueType.DateTime,
				YValueType = ChartValueType.Double,
				Color = color,
				BorderWidth = 2,
				SmartLabelStyle =
				{
					Enabled = true,
					AllowOutsidePlotArea = LabelOutsidePlotAreaStyle.Yes,
					IsOverlappedHidden = false,
					IsMarkerOverlappingAllowed = true,
					MinMovingDistance = 1,
					CalloutStyle = LabelCalloutStyle.Underlined,
					CalloutLineDashStyle = ChartDashStyle.Solid,
					CalloutLineAnchorCapStyle = LineAnchorCapStyle.Arrow,
					CalloutLineWidth = 0,
					MovingDirection = LabelAlignmentStyles.BottomLeft
				}
			};
			return series;
		}

		private void ChangeTimeFrameAndTrack(TimeSpan timeFrame)
		{
			m_timeFrame = timeFrame;
			MainChartScrollBar.Value = MainChartScrollBar.Maximum;
			ScrollChart(true);
			IsTracking = true;
		}

		private void UpdateSeries(IDictionary<string, float> sensors)
		{
			if (!m_startTime.HasValue) m_startTime = DateTime.Now;

			var isCelcius = sensors[SensorsKeys.Celcius] > 0;
			m_seriesData[SensorsKeys.Temperature].SetLastValueFormat(isCelcius ? "{0} °C" : "{0} °F");
			m_seriesData[SensorsKeys.TemperatureSet].SetLastValueFormat(isCelcius ? "{0} °C" : "{0} °F");

			var now = DateTime.Now;
			var xValue = now.ToOADate();
			var xAxisMax = now.AddSeconds(m_timeFrame.TotalSeconds * 0.07).ToOADate();
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
					var roundedValue = (float)Math.Round(readings, 3);
					point.XValue = xValue;
					point.YValues = new double[] { interpolatedValue };
					point.Tag = point.Label = roundedValue.ToString(CultureInfo.InvariantCulture);
					point.MarkerSize = 5;
					point.MarkerStyle = MarkerStyle.Circle;
					data.SetLastValue(roundedValue);
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
						if (series.Points.Count > 1)
						{
							series.Points[series.Points.Count - 2].Label = null;
						}
					}
				}
			}

			var points = MainChart.Series.SelectMany(x => x.Points).Where(x => !x.IsEmpty).ToArray();

			var minDate = DateTime.FromOADate(points.Min(x => x.XValue));
			var maxDate = DateTime.FromOADate(points.Max(x => x.XValue));

			var range = maxDate - minDate;
			var framesCount = Math.Floor(range.TotalSeconds / m_timeFrame.TotalSeconds);

			MainChartScrollBar.Maximum = (int)(framesCount * 30);
			if (IsTracking)
			{
				MainChartScrollBar.Value = MainChartScrollBar.Maximum;
				ScrollChart(true);
			}

			MainChart.ChartAreas[0].AxisX.Minimum = m_startTime.Value.AddSeconds(-5).ToOADate();
			MainChart.ChartAreas[0].AxisX.Maximum = xAxisMax;			
		}
		
		private void ScrollChart(bool toEnd)
		{
			if (!m_startTime.HasValue) return;

			if (toEnd)
			{
				var toValue = MainChart.ChartAreas[0].AxisX.Maximum;
				var toDate = DateTime.FromOADate(toValue);
				var fromValue = toDate.Add(-m_timeFrame).ToOADate();

				MainChart.ChartAreas[0].AxisX.ScaleView.Zoom(fromValue, toValue);
			}
			else
			{
				var frameIndex = MainChartScrollBar.Value;
				var fromValue = m_startTime.Value.AddSeconds(frameIndex / 30f * m_timeFrame.TotalSeconds).ToOADate();
				var toValue = m_startTime.Value.AddSeconds((frameIndex / 30f + 1) * m_timeFrame.TotalSeconds).ToOADate();

				MainChart.ChartAreas[0].AxisX.ScaleView.Zoom(fromValue, toValue);
			}
		}

		private static float Interpolate(float value, IList<ValueLimit<float, int>> lowHigh)
		{
			var low = lowHigh[0];
			var high = lowHigh[1];

			if (value > high.Value) return high.Value;
			if (value < low.Value) return low.Value;

			return low.Limit + (value - low.Value) / (high.Value - low.Value) * (high.Limit - low.Limit);
		}

		private void SaveCheckedSeries()
		{
			foreach (var kvp in m_seriesData)
			{
				var seriesName = kvp.Key;
				var data = kvp.Value;
				m_configuration.DeviceMonitorSeries[seriesName] = data.CheckBox.Checked;
			}
		}

		private void COMConnector_Connected()
		{
			m_isComPortConnected = true;
			m_usbConnector.SetupDeviceMonitor(true);
		}

		private void COMConnector_Disconnected()
		{
			m_isComPortConnected = false;
			EnsureConnection();
		}

		private void ComConnector_MonitorDataReceived(string message)
		{
			if (m_isPaused || string.IsNullOrEmpty(message)) return;

			var sensors = DeviceSensorsData.Parse(message);
			if (sensors == null) return;

			UpdateUI(() => UpdateSeries(sensors));
		}

		private void SeriesCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			var checkbox = sender as CheckBox;
			if (checkbox == null || checkbox.Tag == null || string.IsNullOrEmpty(checkbox.Tag.ToString())) return;

			var seriesName = checkbox.Tag.ToString();
			m_seriesData[seriesName].Seires.Enabled = checkbox.Checked;
		}

		private void PuffMenuItem_Click(int seconds)
		{
			if (!EnsureConnection()) return;

			m_usbConnector.MakePuff(seconds);
			PuffButton.Enabled = false;
			new Thread(() =>
			{
				Thread.Sleep(TimeSpan.FromSeconds(seconds));
				UpdateUI(() => PuffButton.Enabled = true);
			}) { IsBackground = true }.Start();
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
