using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using JetBrains.Annotations;
using NCore;
using NCore.UI;
using NCore.USB;
using NToolbox.Models;
using NToolbox.Services;

namespace NToolbox.Windows
{
	internal partial class DeviceMonitorWindow : EditorDialogWindow
	{
		private const int RequestDataIntervalInMs = 100;

		private const int ChartMaxDataPointsCount = 18000;
		private const int ChartMaxFiringAnnotationsCount = 100;
		private const int ChartMaxYValue = 100;
		private const int ChartMarkerSize = 0;
		private const int ChartSelectedMarkerSize = 7;

		private readonly IDictionary<string, float> m_sensorsData = new Dictionary<string, float>
		{
			{ SensorsKeys.Timestamp, 0 },

			{ SensorsKeys.IsFiring, 0 },
			{ SensorsKeys.IsCharging, 0 },
			{ SensorsKeys.IsCelcius, 0 },

			{ SensorsKeys.Battery1, 0 },
			{ SensorsKeys.Battery2, 0 },
			{ SensorsKeys.Battery3, 0 },
			{ SensorsKeys.Battery4, 0 },
			{ SensorsKeys.BatteryPack, 0 },

			{ SensorsKeys.Power, 0 },
			{ SensorsKeys.PowerSet, 0 },
			{ SensorsKeys.TemperatureSet, 0 },
			{ SensorsKeys.Temperature, 0 },

			{ SensorsKeys.OutputVoltage, 0 },
			{ SensorsKeys.OutputCurrent, 0 },

			{ SensorsKeys.Resistance, 0 },
			{ SensorsKeys.RealResistance, 0 },

			{ SensorsKeys.BoardTemperature, 0 }
		};

		private readonly ToolboxConfiguration m_configuration;
		private readonly StringBuilder m_lineBuilder = new StringBuilder();

		private IDictionary<string, SeriesRelatedData> m_seriesData;
		private TimeSpan m_timeFrame = TimeSpan.FromSeconds(10);
		private int m_verticalFrame = 100;
		private DateTime? m_startTime;
		
		private ContextMenu m_xScaleMenu;
		private ContextMenu m_yScaleMenu;
		private ContextMenu m_puffsMenu;

		private double m_xPrevValue;
		private DateTime? m_prevReceiveTime;

		private bool m_isTracking = true;
		private bool m_isScrollingHorizontally;
		private bool m_stopRequested;
		private bool m_isChartUpdating;
		private bool m_isChartPaused;
		private bool m_isFiring;
		private bool m_isRecording;

		private CalloutAnnotation m_valueAnnotation;
		private bool m_isPlacingAnnotation;
		private DataPoint m_pointUnderCursor;
		private bool m_pointerUnderCursorSelected;

		private StreamWriter m_fileWriter;
		
		public bool IsTracking
		{
			get { return m_isTracking; }
			set
			{
				m_isTracking = value;
				TrackingButton.Enabled = !m_isTracking;
			}
		}

		public DeviceMonitorWindow(ToolboxConfiguration configuration)
		{
			m_configuration = configuration;

			InitializeComponent();
			Initialize();
			InitializeControls();
			InitializeChart();
			InitializeSeries();
			InitializeContextMenus();
		}

		private void Initialize()
		{
			Opacity = 0;
			Load += (s, e) =>
			{
				if (!EnsureConnection()) return;

				Opacity = 1;
				new Thread(MonitoringProc) { IsBackground = true }.Start();
			};
			Closing += (s, e) =>
			{
				m_stopRequested = true;
				SaveMonitoringConfiguration();
			};
		}

		private void MonitoringProc()
		{
			while (true)
			{
				if (m_stopRequested) return;
				if (!m_isChartPaused)
				{
					byte[] bytes;
					try
					{
						bytes = HidConnector.Instance.ReadMonitoringData();
					}
					catch (Exception)
					{
						break;
					}

					var data = BinaryStructure.ReadBinary<MonitoringData>(bytes);
					var kvp = CreateMonitoringDataCollection(data);

					UpdateUI(() =>
					{
						try
						{
							m_isChartUpdating = true;
							UpdateSeries(kvp);
						}
						finally
						{
							m_isChartUpdating = false;
						}
					});
				}
				Thread.Sleep(RequestDataIntervalInMs);
			}

			if (EnsureConnection()) MonitoringProc();
		}

		private void InitializeControls()
		{
			var batteryLimits = new[] { new ValueLimit<float, int>(2.75f, 80), new ValueLimit<float, int>(4.2f, 95) };
			var batteryPackLimits = new[] { new ValueLimit<float, int>(2.75f, 80), new ValueLimit<float, int>(12.6f, 95) };
			var powerLimits = new[] { new ValueLimit<float, int>(1, 50), new ValueLimit<float, int>(75, 80) };
			var powerSetLimits = new[] { new ValueLimit<float, int>(1, 50), new ValueLimit<float, int>(75, 80) };
			var tempLimits = new[] { new ValueLimit<float, int>(100, 50), new ValueLimit<float, int>(600, 80) };
			var tempSetLimits = new[] { new ValueLimit<float, int>(100, 50), new ValueLimit<float, int>(600, 80) };
			var resistanceLimits = new[] { new ValueLimit<float, int>(0.05f, 30), new ValueLimit<float, int>(3f, 50) };
			var realResistanceLimits = new[] { new ValueLimit<float, int>(0.05f, 30), new ValueLimit<float, int>(3f, 50) };
			var outputVoltageLimits = new[] { new ValueLimit<float, int>(1, 20), new ValueLimit<float, int>(9, 30) };
			var outputCurrentLimits = new[] { new ValueLimit<float, int>(1, 10), new ValueLimit<float, int>(50, 20) };
			var boardTemperatureLimits = new[] { new ValueLimit<float, int>(0, 1), new ValueLimit<float, int>(99, 10) };

			m_seriesData = new Dictionary<string, SeriesRelatedData>
			{
				{
					SensorsKeys.Battery1,
					new SeriesRelatedData(Color.DarkSlateGray, Battery1CheckBox, Battery1Panel, Battery1VoltageLabel, "{0} V", batteryLimits)
				},
				{
					SensorsKeys.Battery2,
					new SeriesRelatedData(Color.DarkSlateGray, Battery2CheckBox, Battery2Panel, Battery2VoltageLabel, "{0} V", batteryLimits)
				},
				{
					SensorsKeys.Battery3,
					new SeriesRelatedData(Color.DarkSlateGray, Battery3CheckBox, Battery3Panel, Battery3VoltageLabel, "{0} V", batteryLimits)
				},
				{
					SensorsKeys.Battery4,
					new SeriesRelatedData(Color.DarkSlateGray, Battery4CheckBox, Battery4Panel, Battery4VoltageLabel, "{0} V", batteryLimits)
				},
				{
					SensorsKeys.BatteryPack,
					new SeriesRelatedData(Color.DarkSlateGray, BatteryPackCheckBox, BatteryPackPanel, BatteryPackVoltageLabel, "{0} V", batteryPackLimits)
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

			ShowPuffsBoundariesCheckBox.Checked = m_configuration.ShowPuffsBoundaries;
			PauseButton.Click += (s, e) =>
			{
				if (m_isChartPaused)
				{
					m_prevReceiveTime = DateTime.Now.Add(-TimeSpan.FromMilliseconds(RequestDataIntervalInMs));
				}
				m_isChartPaused = !m_isChartPaused;
				PauseButton.Text = m_isChartPaused ? LocalizableStrings.DeviceMonitorPauseResumeButton : LocalizableStrings.DeviceMonitorPauseButton;
			};

			TrackingButton.Click += (s, e) => ChangeXScale(m_timeFrame);
			RecordButton.Click += (s, e) =>
			{
				if (m_isRecording)
				{
					StopRecording();
				}
				else
				{
					StartRecording();
				}
			};
		}

		private void InitializeChart()
		{
			MainChart.Palette = ChartColorPalette.Pastel;
			var area = new ChartArea();
			{
				area.AxisX.Minimum = -1;
				area.AxisX.IsMarginVisible = false;
				area.AxisX.MajorGrid.Enabled = true;
				area.AxisX.MajorGrid.LineColor = Color.FromArgb(230, 230, 230);
				area.AxisX.MajorTickMark.TickMarkStyle = TickMarkStyle.None;
				area.AxisX.LabelStyle.Enabled = false;
				area.AxisX.LineColor = Color.DarkGray;
				area.AxisX.IntervalOffsetType = DateTimeIntervalType.Milliseconds;
				area.AxisX.ScaleView.Zoomable = true;
				area.AxisX.ScrollBar.Enabled = false;

				area.AxisY.Minimum = 0;
				area.AxisY.Maximum = ChartMaxYValue;
				area.AxisY.IsMarginVisible = false;
				area.AxisY.MajorGrid.Enabled = true;
				area.AxisY.MajorGrid.LineColor = Color.FromArgb(230, 230, 230);
				area.AxisY.MajorTickMark.TickMarkStyle = TickMarkStyle.None;
				area.AxisY.LabelStyle.Enabled = false;
				area.AxisY.LineColor = Color.DarkGray;
				area.AxisY.ScaleView.Zoomable = true;
				area.AxisY.ScrollBar.Enabled = false;
			}
			m_valueAnnotation = new CalloutAnnotation
			{
				AxisX = area.AxisX,
				AxisY = area.AxisY
			};
			MainChart.ChartAreas.Add(area);
			MainChart.Annotations.Add(m_valueAnnotation);
			MainChart.MouseMove += MainChart_MouseMove;

			MainChartHorizontalScrollBar.Scroll += (s, e) =>
			{
				m_isScrollingHorizontally = e.Type != ScrollEventType.EndScroll;
				IsTracking = MainChartHorizontalScrollBar.Value == MainChartHorizontalScrollBar.Maximum;
			};
			MainChartHorizontalScrollBar.ValueChanged += (s, e) =>
			{
				ScrollChartHorizontally(MainChartHorizontalScrollBar.Value == MainChartHorizontalScrollBar.Maximum);
			};
			MainChartVerticalScrollBar.ValueChanged += (s, e) => ScrollChartVertically();
		}

		private void MainChart_MouseMove(object sender, MouseEventArgs e)
		{
			if (m_isPlacingAnnotation) return;

			while (m_isChartUpdating)
			{
			}

			try
			{
				m_isPlacingAnnotation = true;
				var xValueUnderCursor = MainChart.ChartAreas[0].AxisX.PixelPositionToValue(e.X);
				var results = MainChart.HitTest(e.X, e.Y, false, ChartElementType.DataPoint);
				var point = results.Where(x => x.ChartElementType == ChartElementType.DataPoint)
				                   .Select(x => x.Object as DataPoint)
				                   .Where(x => x != null && x.Tag != null)
				                   .OrderBy(x => Math.Abs(xValueUnderCursor - x.XValue))
				                   .FirstOrDefault();

				if (point == null) return;
				if (m_pointUnderCursor != null && !m_pointerUnderCursorSelected)
				{
					m_pointUnderCursor.MarkerSize = ChartMarkerSize;
				}

				m_pointUnderCursor = point;
				m_pointerUnderCursorSelected = m_pointUnderCursor.MarkerSize == ChartSelectedMarkerSize;
				m_pointUnderCursor.MarkerSize = ChartSelectedMarkerSize;

				m_valueAnnotation.BeginPlacement();
				{
					m_valueAnnotation.AnchorX = m_pointUnderCursor.XValue;
					m_valueAnnotation.AnchorY = m_pointUnderCursor.YValues[0];
					m_valueAnnotation.Text = m_pointUnderCursor.Tag.ToString();
				}
				m_valueAnnotation.EndPlacement();
			}
			catch (Exception)
			{
				// Ignore
			}
			finally
			{
				m_isPlacingAnnotation = false;
			}
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
				if (!m_configuration.ActiveSeries.TryGetValue(seriesName, out isChecked)) isChecked = true;

				data.CheckBox.Tag = seriesName;
				data.CheckBox.Checked = data.Seires.Enabled = isChecked;
				data.CheckBox.CheckedChanged += SeriesCheckBox_CheckedChanged;
				data.Panel.BackColor = data.Color;
			}
		}

		private void InitializeContextMenus()
		{
			m_xScaleMenu = new ContextMenu(new[]
			{
				new MenuItem("5 " + LocalizableStrings.Seconds, (s, e) => ChangeXScale(TimeSpan.FromSeconds(5))),
				new MenuItem("10 " + LocalizableStrings.Seconds, (s, e) => ChangeXScale(TimeSpan.FromSeconds(10))),
				new MenuItem("20 " + LocalizableStrings.Seconds, (s, e) => ChangeXScale(TimeSpan.FromSeconds(20))),
				new MenuItem("30 " + LocalizableStrings.Seconds, (s, e) => ChangeXScale(TimeSpan.FromSeconds(30))),
				new MenuItem("45 " + LocalizableStrings.Seconds, (s, e) => ChangeXScale(TimeSpan.FromSeconds(45))),
				new MenuItem("60 " + LocalizableStrings.Seconds, (s, e) => ChangeXScale(TimeSpan.FromSeconds(60)))
			});
			SetXScaleButton.Click += (s, e) =>
			{
				var control = (Control)s;
				m_xScaleMenu.Show(control, new Point(control.Width, 0));
			};
			m_yScaleMenu = new ContextMenu(new[]
			{
				new MenuItem("5%", (s, e) => ChangeYScale(5)),
				new MenuItem("10%", (s, e) => ChangeYScale(10)),
				new MenuItem("25%", (s, e) => ChangeYScale(25)),
				new MenuItem("50%", (s, e) => ChangeYScale(50)),
				new MenuItem("100%", (s, e) => ChangeYScale(100))
			});
			SetYScaleButton.Click += (s, e) =>
			{
				var control = (Control)s;
				m_yScaleMenu.Show(control, new Point(control.Width, 0));
			};

			m_puffsMenu = new ContextMenu();
			for (var i = 1; i <= 9; i++)
			{
				var seconds = i;
				m_puffsMenu.MenuItems.Add(seconds + " " + (seconds == 1 ? LocalizableStrings.Second : LocalizableStrings.Seconds), (s, e) => PuffMenuItem_Click(seconds));
			}
			PuffButton.Click += (s, e) =>
			{
				var control = (Control)s;
				m_puffsMenu.Show(control, new Point(control.Width, 0));
			};
		}

		private bool EnsureConnection()
		{
			if (HidConnector.Instance.IsDeviceConnected) return true;

			var result = InfoBox.Show(LocalizableStrings.MessageNoCompatibleUSBDevice, MessageBoxButtons.OKCancel);
			if (result == DialogResult.OK)
			{
				return EnsureConnection();
			}
			if (result == DialogResult.Cancel)
			{
				UpdateUI(Close);
				return false;
			}
			return true;
		}

		private Series CreateSeries(string name, Color color)
		{
			var series = new Series
			{
				Name = name,
				ChartType = SeriesChartType.Line,
				XValueType = ChartValueType.Double,
				YValueType = ChartValueType.Int32,
				Color = color,
				BorderWidth = 2,
				SmartLabelStyle =
				{
					Enabled = true,
					AllowOutsidePlotArea = LabelOutsidePlotAreaStyle.Yes,
					IsOverlappedHidden = false,
					IsMarkerOverlappingAllowed = true,
					MinMovingDistance = 1,
					CalloutStyle = LabelCalloutStyle.None,
					CalloutLineDashStyle = ChartDashStyle.Solid,
					CalloutLineAnchorCapStyle = LineAnchorCapStyle.None,
					CalloutLineWidth = 0,
					MovingDirection = LabelAlignmentStyles.BottomLeft
				}
			};
			return series;
		}

		private void CreateFiringAnnotation(double xAnchor, bool isStart)
		{
			var firingAnnotation = new VerticalLineAnnotation
			{
				AxisX = MainChart.ChartAreas[0].AxisX,
				AxisY = MainChart.ChartAreas[0].AxisY,
				AnchorX = xAnchor,
				LineColor = isStart ? Color.CornflowerBlue : Color.CadetBlue,
				LineWidth = 1,
				IsInfinitive = true
			};

			MainChart.Annotations.Add(firingAnnotation);
			firingAnnotation.ClipToChartArea = MainChart.ChartAreas[0].Name;
		}

		private void ChangeXScale(TimeSpan timeFrame)
		{
			m_timeFrame = timeFrame;
			UpdateHorizontalScrollAndAxisXMax(m_xPrevValue + 1);
			MainChartHorizontalScrollBar.Value = MainChartHorizontalScrollBar.Maximum;
			ScrollChartHorizontally(true);
			IsTracking = true;
		}

		private void ChangeYScale(int yScale)
		{
			m_verticalFrame = Math.Max(0, Math.Min(ChartMaxYValue, yScale));
			if (m_verticalFrame == ChartMaxYValue)
			{
				MainChartVerticalScrollBar.Value = 0;
				MainChartVerticalScrollBar.Maximum = 0;
			}
			else
			{
				MainChartVerticalScrollBar.Maximum = ChartMaxYValue - m_verticalFrame;
				MainChartVerticalScrollBar.Value = 0;
			}
			ScrollChartVertically();
		}

		private IDictionary<string, float> CreateMonitoringDataCollection(MonitoringData data)
		{
			var battery1 = data.Battery1Voltage == 0 ? 0 : (data.Battery1Voltage + 275) / 100f;
			var battery2 = data.Battery2Voltage == 0 ? 0 : (data.Battery2Voltage + 275) / 100f;
			var battery3 = data.Battery3Voltage == 0 ? 0 : (data.Battery3Voltage + 275) / 100f;
			var battery4 = data.Battery4Voltage == 0 ? 0 : (data.Battery4Voltage + 275) / 100f;
			var batteryPack = battery1 + battery2 + battery3  + battery4;

			var outputVoltage = data.OutputVoltage / 100f;
			var outputCurrent = data.OutputCurrent / 100f;
			var outputPower = outputVoltage * outputCurrent;

			{
				m_sensorsData[SensorsKeys.IsFiring] = data.IsFiring ? 1 : 0;
				m_sensorsData[SensorsKeys.IsCharging] = data.IsCharging ? 1 : 0;
				m_sensorsData[SensorsKeys.IsCelcius] = data.IsCelcius ? 1 : 0;

				m_sensorsData[SensorsKeys.Battery1] = battery1;
				m_sensorsData[SensorsKeys.Battery2] = battery2;
				m_sensorsData[SensorsKeys.Battery3] = battery3;
				m_sensorsData[SensorsKeys.Battery4] = battery4;
				m_sensorsData[SensorsKeys.BatteryPack] = batteryPack;

				m_sensorsData[SensorsKeys.Power] = outputPower;
				m_sensorsData[SensorsKeys.PowerSet] = data.PowerSet / 10f;
				m_sensorsData[SensorsKeys.TemperatureSet] = data.TemperatureSet;
				m_sensorsData[SensorsKeys.Temperature] = data.Temperature;

				m_sensorsData[SensorsKeys.OutputVoltage] = outputVoltage;
				m_sensorsData[SensorsKeys.OutputCurrent] = outputCurrent;

				m_sensorsData[SensorsKeys.Resistance] = data.Resistance / 1000f;
				m_sensorsData[SensorsKeys.RealResistance] = data.RealResistance / 1000f;

				m_sensorsData[SensorsKeys.BoardTemperature] = data.BoardTemperature;
			}
			return m_sensorsData;
		}

		private void UpdateSeries(IDictionary<string, float> sensors)
		{
			if (!m_startTime.HasValue) m_startTime = DateTime.Now;

			var now = DateTime.Now;
			var xValue = m_prevReceiveTime.HasValue ? m_xPrevValue + (now - m_prevReceiveTime.Value).TotalSeconds : 0;
			var xAxisMax = xValue + 1;

			m_prevReceiveTime = now;
			if (ShowPuffsBoundariesCheckBox.Checked)
			{
				var isFiring = sensors[SensorsKeys.IsFiring] > 0;
				if (isFiring && !m_isFiring)
				{
					CreateFiringAnnotation(xValue, true);
				}
				if (!isFiring && m_isFiring && m_xPrevValue > 0)
				{
					CreateFiringAnnotation(m_xPrevValue, false);
				}
				m_isFiring = isFiring;
			}
			m_xPrevValue = xValue;

			var isCelcius = sensors[SensorsKeys.IsCelcius] > 0;
			m_seriesData[SensorsKeys.Temperature].SetLastValueFormat(isCelcius ? "{0} °C" : "{0} °F");
			m_seriesData[SensorsKeys.TemperatureSet].SetLastValueFormat(isCelcius ? "{0} °C" : "{0} °F");

			foreach (var kvp in m_seriesData)
			{
				var sensorName = kvp.Key;
				var data = kvp.Value;
				var readings = sensors[sensorName];
				var interpolatedValue = Interpolate(readings, data.InterpolationLimits);

				var point = new DataPoint();
				if (Math.Abs(readings) > 0.001)
				{
					var roundedValue = (float)Math.Round(readings, 3);
					point.XValue = xValue;
					point.YValues = new double[] { interpolatedValue };
					point.Tag = point.Label = roundedValue.ToString(CultureInfo.InvariantCulture);
					point.MarkerSize = ChartSelectedMarkerSize;
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

			if (m_isRecording)
			{
				m_lineBuilder.Clear();
				// Trace time
				m_lineBuilder.Append(xValue.ToString(CultureInfo.InvariantCulture));
				m_lineBuilder.Append(",");
				// Other values
				var values = m_seriesData.Values
										 .Where(x => x.CheckBox.Checked)
										 .Select(x => x.LastValue.HasValue ? x.LastValue.Value.ToString(CultureInfo.InvariantCulture) : string.Empty);

				m_lineBuilder.Append(string.Join(",", values));
				var ex = Safe.Execute(() =>
				{
					m_fileWriter.WriteLine(m_lineBuilder.ToString());
					m_fileWriter.Flush();
				});
				if (ex != null)
				{
					InfoBox.Show("Recording was stopped because of error:\n" + ex.Message);
					RecordButton.PerformClick();
				}
			}

			while (MainChart.Annotations.Count > 1 && MainChart.Annotations.Count - 1 > ChartMaxFiringAnnotationsCount)
			{
				MainChart.Annotations.RemoveAt(1);
			}

			foreach (var series in MainChart.Series)
			{
				while (series.Points.Count > ChartMaxDataPointsCount)
				{
					series.Points.RemoveAt(0);
				}
				MainChart.ChartAreas[0].AxisX.Minimum = MainChart.Series[0].Points[0].XValue - 1;

				if (series.Points.Count > 0)
				{
					var lastPoint = series.Points[series.Points.Count - 1];
					if (lastPoint.IsEmpty) continue;

					if (series.Points.Count > 1)
					{
						var preLastPoint = series.Points[series.Points.Count - 2];
						preLastPoint.Label = null;
						preLastPoint.MarkerSize = ChartMarkerSize;
					}
				}
			}

			UpdateHorizontalScrollAndAxisXMax(xAxisMax);
			if (IsTracking)
			{
				if (MainChartHorizontalScrollBar.Value == MainChartHorizontalScrollBar.Maximum)
				{
					// Force zoom.
					ScrollChartHorizontally(true);
				}
				else if (!m_isScrollingHorizontally)
				{
					// Zoom applies automatically when changing scrollbar value.
					MainChartHorizontalScrollBar.Value = MainChartHorizontalScrollBar.Maximum;
				}
			}
		}

		private void UpdateHorizontalScrollAndAxisXMax(double potentialAxisXMax)
		{
			var timeFrameInSeconds = m_timeFrame.TotalSeconds;
			if (timeFrameInSeconds >= potentialAxisXMax)
			{
				MainChart.ChartAreas[0].AxisX.Maximum = timeFrameInSeconds;
				MainChartHorizontalScrollBar.Maximum = 0;
			}
			else
			{
				MainChart.ChartAreas[0].AxisX.Maximum = potentialAxisXMax;
				MainChartHorizontalScrollBar.Maximum = (int)Math.Ceiling(potentialAxisXMax - timeFrameInSeconds - MainChart.ChartAreas[0].AxisX.Minimum);
			}
		}

		private void ScrollChartHorizontally(bool toEnd)
		{
			if (!m_startTime.HasValue) return;

			double fromValue;
			double toValue;

			if (toEnd)
			{
				toValue = MainChart.ChartAreas[0].AxisX.Maximum;
				fromValue = toValue - m_timeFrame.TotalSeconds;
			}
			else
			{
				fromValue = MainChartHorizontalScrollBar.Value - 1;
				toValue = fromValue + m_timeFrame.TotalSeconds;
			}

			MainChart.ChartAreas[0].AxisX.ScaleView.Zoom(fromValue, toValue);
		}

		private void ScrollChartVertically()
		{
			var fromValue = Math.Min(ChartMaxYValue - m_verticalFrame, ChartMaxYValue - MainChartVerticalScrollBar.Value - m_verticalFrame);
			var toValue = Math.Min(ChartMaxYValue, ChartMaxYValue - MainChartVerticalScrollBar.Value);

			MainChart.ChartAreas[0].AxisY.ScaleView.Zoom(fromValue, toValue);
		}

		private void StartRecording()
		{
			if (m_isRecording) return;

			using (var sf = new SaveFileDialog { Filter = FileFilters.CsvFilter })
			{
				if (sf.ShowDialog() != DialogResult.OK) return;

				var fileName = sf.FileName;
				var ex = Safe.Execute(() =>
				{
					m_fileWriter = new StreamWriter(File.Open(fileName, FileMode.Create, FileAccess.Write, FileShare.Read));
					var header = "Time," + string.Join(",", m_seriesData.Where(x => x.Value.CheckBox.Checked).Select(x => x.Key));
					m_fileWriter.WriteLine(header);
				});
				if (ex != null)
				{
					InfoBox.Show("Unable to start recoding...\n" + ex.Message);
					return;
				}
			}

			m_isRecording = true;
			m_seriesData.ForEach(x => x.Value.CheckBox.Enabled = false);
			RecordButton.Text = LocalizableStrings.DeviceMonitorStopRecording;
		}

		private void StopRecording()
		{
			if (!m_isRecording) return;

			Safe.Execute(() =>
			{
				m_fileWriter.Flush();
				m_fileWriter.Dispose();
			});

			m_isRecording = false;
			m_seriesData.ForEach(x => x.Value.CheckBox.Enabled = true);
			RecordButton.Text = LocalizableStrings.DeviceMonitorRecord;
		}

		private void SaveMonitoringConfiguration()
		{
			m_configuration.ShowPuffsBoundaries = ShowPuffsBoundariesCheckBox.Checked;
			m_configuration.ActiveSeries.Clear();

			foreach (var kvp in m_seriesData)
			{
				var seriesName = kvp.Key;
				var data = kvp.Value;
				m_configuration.ActiveSeries[seriesName] = data.CheckBox.Checked;
			}
		}

		private static float Interpolate(float value, IList<ValueLimit<float, int>> lowHigh)
		{
			var low = lowHigh[0];
			var high = lowHigh[1];

			if (value > high.Value) return high.Limit;
			if (value < low.Value) return low.Limit;

			return low.Limit + (value - low.Value) / (high.Value - low.Value) * (high.Limit - low.Limit);
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

			HidConnector.Instance.MakePuff(seconds);
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

			public SeriesRelatedData(Color color, CheckBox checkBox, Panel panel, Label lastValueLabel, string labelFormat, [NotNull] ValueLimit<float, int>[] interpolationLimits)
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

			public float? LastValue { get; private set; }

			public void SetLastValueFormat(string format)
			{
				m_labelFormat = format;
			}

			public void SetLastValue(float? value)
			{
				LastValue = value;
				m_lastValueLabel.Text = Seires.Enabled && LastValue.HasValue
					? string.Format(CultureInfo.InvariantCulture, m_labelFormat, LastValue)
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

		private static class SensorsKeys
		{
			internal const string Timestamp = "Timestamp";

			internal const string IsFiring = "IsFiring";
			internal const string IsCharging = "IsCharging";
			internal const string IsCelcius = "IsCelcius";

			internal const string Battery1 = "Battery1Voltage";
			internal const string Battery2 = "Battery2Voltage";
			internal const string Battery3 = "Battery3Voltage";
			internal const string Battery4 = "Battery4Voltage";
			internal const string BatteryPack = "BatteryPack";

			internal const string Power = "Power";
			internal const string PowerSet = "PowerSet";

			internal const string TemperatureSet = "TemperatureSet";
			internal const string Temperature = "Temperature";
			
			internal const string OutputVoltage = "OutputVolts";
			internal const string OutputCurrent = "OutputCurrent";

			internal const string Resistance = "Resistance";
			internal const string RealResistance = "RealResistance";

			internal const string BoardTemperature = "BoardTemperature";
		}
	}
}
