using System;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using JetBrains.Annotations;
using NCore.UI;
using NToolbox.Models;
using NToolbox.Services;

namespace NToolbox.Windows
{
	internal partial class PowerCurveProfileWindow : EditorDialogWindow
	{
		private const ushort MinTime = 0;
		private const ushort MaxTime = 25;
		
		private const int MinPercents = 0;
		private const int MaxPercents = 250;
		private const string PointsSeriesName = "points";

		private static readonly Regex s_blackList = new Regex("(?![a-zA-Z0-9\\+\\-\\.\\s]).", RegexOptions.Compiled);
		private readonly ArcticFoxConfiguration.PowerCurve m_curve;

		private TimePercentControlGroup[] m_curveControls;

		private bool m_isDragginPoint;
		private DataPoint m_pointUnderCursor;
		private int m_pointUnderCursorIndex;

		private ContextMenu m_timeScaleMenu;
		private double m_timeInterval = 0.5;
		private int m_timeFrame = 5;

		public PowerCurveProfileWindow([NotNull] ArcticFoxConfiguration.PowerCurve curve)
		{
			if (curve == null) throw new ArgumentNullException("curve");

			m_curve = curve;

			InitializeComponent();
			InitializeChart();
			InitializeControls();
			InitializeWorkspace();

			Load += (s, e) => ZoomChart(m_timeFrame);
		}

		private void InitializeChart()
		{
			PowerCurveChart.Palette = ChartColorPalette.Pastel;
			var area = new ChartArea();
			{
				area.AxisX.IsMarginVisible = false;
				area.AxisX.Minimum = MinTime;
				area.AxisX.Maximum = MaxTime;
				area.AxisX.MajorGrid.Enabled = true;
				area.AxisX.MajorGrid.LineColor = Color.FromArgb(230, 230, 230);
				area.AxisX.LineColor = Color.DarkGray;
				area.AxisX.Interval = m_timeInterval;
				area.AxisX.ScaleView.Zoomable = true;
				area.AxisX.ScrollBar.Enabled = false;

				area.AxisY.IsMarginVisible = false;
				area.AxisY.Minimum = MinPercents;
				area.AxisY.Maximum = MaxPercents;
				area.AxisY.MajorGrid.Enabled = true;
				area.AxisY.MajorGrid.LineColor = Color.FromArgb(230, 230, 230);
				area.AxisY.LineColor = Color.DarkGray;
			}
			PowerCurveChart.ChartAreas.Add(area);

			var series = new Series
			{
				Name = PointsSeriesName,
				ChartType = SeriesChartType.Line,
				YValueType = ChartValueType.Double,
				Color = Color.YellowGreen,
				BorderWidth = 2,
				SmartLabelStyle =
				{
					Enabled = true,
					AllowOutsidePlotArea = LabelOutsidePlotAreaStyle.Yes,
					IsOverlappedHidden = false,
					IsMarkerOverlappingAllowed = true,
					MovingDirection = LabelAlignmentStyles.Right
				}
			};
			var centerSeries = new Series
			{
				ChartType = SeriesChartType.FastLine,
				Color = Color.DeepSkyBlue,
				BorderWidth = 1
			};
			
			centerSeries.Points.Add(new DataPoint(0, 100));
			centerSeries.Points.Add(new DataPoint(25, 100));

			PowerCurveChart.Series.Add(centerSeries);
			PowerCurveChart.Series.Add(series);
			PowerCurveChart.MouseMove += PowerCurveChart_MouseMove;
			PowerCurveChart.MouseDown += PowerCurveChart_MouseDown;
			PowerCurveChart.MouseUp += PowerCurveChart_MouseUp;
		}

		private void PowerCurveChart_MouseMove(object sender, MouseEventArgs e)
		{
			if (m_isDragginPoint && m_pointUnderCursor != null)
			{
				var xValueUnderCursor = PowerCurveChart.ChartAreas[0].AxisX.PixelPositionToValue(e.X);
				var yValueUnderCursor = PowerCurveChart.ChartAreas[0].AxisY.PixelPositionToValue(e.Y);

				var leftBound = m_pointUnderCursorIndex == 0 ? MinTime : PowerCurveChart.Series[PointsSeriesName].Points[m_pointUnderCursorIndex - 1].XValue + 0.1;
				var rightBound = m_pointUnderCursorIndex == m_curve.Points.Length - 1 ? MaxTime : PowerCurveChart.Series[PointsSeriesName].Points[m_pointUnderCursorIndex + 1].XValue - 0.1;

				double tempXValue;
				if (xValueUnderCursor <= leftBound) tempXValue = leftBound;
				else if (xValueUnderCursor >= rightBound) tempXValue = rightBound;
				else tempXValue = xValueUnderCursor;

				double tempYValue;
				if (yValueUnderCursor <= MinPercents) tempYValue = MinPercents;
				else if (yValueUnderCursor >= MaxPercents) tempYValue = MaxPercents;
				else tempYValue = yValueUnderCursor;

				tempXValue = Math.Round(tempXValue, 1);
				tempYValue = Math.Round(tempYValue);

				m_curveControls[m_pointUnderCursorIndex].TimeUpDown.Value = (decimal)tempXValue;
				m_curveControls[m_pointUnderCursorIndex].PercentsUpDown.Value = (decimal)tempYValue;
			}
			else
			{
				var results = PowerCurveChart.HitTest(e.X, e.Y, false, ChartElementType.DataPoint);

				DataPoint point = null;
				foreach (var result in results)
				{
					if (result.ChartElementType != ChartElementType.DataPoint) continue;

					var tmpPoint = result.Object as DataPoint;
					if (tmpPoint == null) continue;
					if (!Equals(tmpPoint.Tag, "draggable")) continue;

					var pointX = PowerCurveChart.ChartAreas[0].AxisX.ValueToPixelPosition(tmpPoint.XValue);
					var pointY = PowerCurveChart.ChartAreas[0].AxisY.ValueToPixelPosition(tmpPoint.YValues[0]);

					if (Math.Abs(e.X - pointX) <= 12 && Math.Abs(e.Y - pointY) <= 12)
					{
						point = tmpPoint;
						m_pointUnderCursorIndex = result.PointIndex;
						break;
					}
				}

				if (m_pointUnderCursor != null) m_pointUnderCursor.MarkerSize = 7;
				m_pointUnderCursor = point;
				if (m_pointUnderCursor != null) m_pointUnderCursor.MarkerSize = 10;
			}
		}

		private void PowerCurveChart_MouseDown(object sender, MouseEventArgs e)
		{
			m_isDragginPoint = true;
		}

		private void PowerCurveChart_MouseUp(object sender, MouseEventArgs e)
		{
			m_isDragginPoint = false;
		}

		private void InitializeControls()
		{
			m_curveControls = new[]
			{
				new TimePercentControlGroup(Time1UpDown, Percents1UpDown),
				new TimePercentControlGroup(Time2UpDown, Percents2UpDown),
				new TimePercentControlGroup(Time3UpDown, Percents3UpDown),
				new TimePercentControlGroup(Time4UpDown, Percents4UpDown),
				new TimePercentControlGroup(Time5UpDown, Percents5UpDown),
				new TimePercentControlGroup(Time6UpDown, Percents6UpDown),
				new TimePercentControlGroup(Time7UpDown, Percents7UpDown),
				new TimePercentControlGroup(Time8UpDown, Percents8UpDown),
				new TimePercentControlGroup(Time9UpDown, Percents9UpDown),
				new TimePercentControlGroup(Time10UpDown, Percents10UpDown),
				new TimePercentControlGroup(Time11UpDown, Percents11UpDown),
				new TimePercentControlGroup(Time12UpDown, Percents12UpDown)
			};

			m_timeScaleMenu = new ContextMenu(new[]
			{
				new MenuItem("1 " + LocalizableStrings.Second, (s, e) => ZoomChart(1)),
				new MenuItem("2 " + LocalizableStrings.Seconds, (s, e) => ZoomChart(2)),
				new MenuItem("5 " + LocalizableStrings.Seconds, (s, e) => ZoomChart(5)),
				new MenuItem("10 " + LocalizableStrings.Seconds, (s, e) => ZoomChart(10)),
				new MenuItem("25 " + LocalizableStrings.Seconds, (s, e) => ZoomChart(25))
			});

			TimeScaleButton.Click += (s, e) =>
			{
				var control = (Control)s;
				m_timeScaleMenu.Show(control, new Point(control.Width, 0));
			};

			ChartHorizontalScrollBar.ValueChanged += (s, e) => ScrollChart();

			NameTextBox.TextChanged += (s, e) =>
			{
				var position = NameTextBox.SelectionStart;
				var input = NameTextBox.Text;
				var matches = s_blackList.Matches(input);
				foreach (Match match in matches)
				{
					if (!match.Success) continue;
					input = input.Replace(match.Value, string.Empty);
				}
				NameTextBox.Text = input;
				NameTextBox.SelectionStart = position;
			};

			SaveButton.Click += SaveButton_Click;
		}

		private void ZoomChart(int timeFrame)
		{
			m_timeFrame = timeFrame;
			m_timeInterval = timeFrame > 2 ? 0.5 : 0.1;
			PowerCurveChart.ChartAreas[0].AxisX.Interval = m_timeInterval;
			ChartHorizontalScrollBar.Maximum = Math.Max(0, (int)((MaxTime - m_timeFrame) / m_timeInterval));
			ScrollChart();
		}

		private void ScrollChart()
		{
			var from = ChartHorizontalScrollBar.Value * m_timeInterval;
			var to = from + m_timeFrame;

			PowerCurveChart.ChartAreas[0].AxisX.ScaleView.Zoom(from, to);
		}

		private void InitializeWorkspace()
		{
			NameTextBox.Text = m_curve.Name;

			for (var i = 0; i < m_curve.Points.Length; i++)
			{
				var data = m_curve.Points[i];
				var timeUpDown = m_curveControls[i].TimeUpDown;
				var percentsUpDown = m_curveControls[i].PercentsUpDown;

				var time = Math.Max(MinTime, Math.Min(data.Time / 10m, MaxTime));
				var percents = Math.Max(MinPercents, Math.Min(data.Percent, (decimal)MaxPercents));
				var point = new DataPoint((double)time, (double)percents)
				{
					MarkerStyle = MarkerStyle.Circle,
					MarkerSize = 7,
					Label = percents.ToString(CultureInfo.InvariantCulture),
					Tag = "draggable"
				};

				timeUpDown.Minimum = MinTime;
				timeUpDown.Maximum = MaxTime;
				timeUpDown.Value = time;
				timeUpDown.Tag = point;
				timeUpDown.ValueChanged += TemperatureUpDown_ValueChanged;

				percentsUpDown.Minimum = MinPercents;
				percentsUpDown.Maximum = MaxPercents;
				percentsUpDown.Value = percents;
				percentsUpDown.Tag = point;
				percentsUpDown.ValueChanged += FactorUpDown_ValueChanged;

				PowerCurveChart.Series[PointsSeriesName].Points.Add(point);
			}
		}

		private void TemperatureUpDown_ValueChanged(object sender, EventArgs e)
		{
			var control = sender as NumericUpDown;
			if (control == null) return;

			var point = control.Tag as DataPoint;
			if (point == null) return;

			var value = control.Value;

			UpdatePointsMinMax();
			point.XValue = (double)value;
		}

		private void FactorUpDown_ValueChanged(object sender, EventArgs e)
		{
			var control = sender as NumericUpDown;
			if (control == null) return;

			var point = control.Tag as DataPoint;
			if (point == null) return;

			var value = control.Value;

			UpdatePointsMinMax();
			point.YValues = new[] { (double)value };
			point.Label = value.ToString(CultureInfo.InvariantCulture);
		}

		private void UpdatePointsMinMax()
		{
			for (var i = 0; i < m_curveControls.Length; i++)
			{
				var group = m_curveControls[i];

				if (i - 1 >= 0)
				{
					var prevTime = m_curveControls[i - 1].TimeUpDown;
					group.TimeUpDown.Minimum = Math.Min(MaxTime, prevTime.Value + 0.1m);
				}

				if (i + 1 < m_curveControls.Length)
				{
					var nextTime = m_curveControls[i + 1].TimeUpDown;
					group.TimeUpDown.Maximum = Math.Max(MinTime, nextTime.Value - 0.1m);
				}
			}
		}

		private void SaveWorkspace()
		{
			m_curve.Name = NameTextBox.Text;

			for (var i = 0; i < m_curve.Points.Length; i++)
			{
				var data = m_curveControls[i];

				m_curve.Points[i].Time = (byte)(data.TimeUpDown.Value * 10m);
				m_curve.Points[i].Percent = (byte)data.PercentsUpDown.Value;
			}
		}

		private void SaveButton_Click(object sender, EventArgs e)
		{
			SaveWorkspace();
			DialogResult = DialogResult.OK;
		}

		private class TimePercentControlGroup
		{
			public TimePercentControlGroup(NumericUpDown timeUpDown, NumericUpDown percentsUpDown)
			{
				TimeUpDown = timeUpDown;
				PercentsUpDown = percentsUpDown;
			}

			public NumericUpDown TimeUpDown { get; private set; }

			public NumericUpDown PercentsUpDown { get; private set; }
		}
	}
}
