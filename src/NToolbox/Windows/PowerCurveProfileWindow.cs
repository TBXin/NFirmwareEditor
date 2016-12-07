using System;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using JetBrains.Annotations;
using NCore.UI;
using NToolbox.Models;

namespace NToolbox.Windows
{
	internal partial class PowerCurveProfileWindow : EditorDialogWindow
	{
		private const ushort MinTime = 0;
		private const ushort MaxTime = 25;
		private const decimal MinPercents = 0;
		private const decimal MaxPercents = 250;

		private static readonly Regex s_blackList = new Regex("(?![a-zA-Z0-9\\+\\-\\.\\s]).", RegexOptions.Compiled);
		private readonly ArcticFoxConfiguration.PowerCurve m_curve;

		private TimePercentControlGroup[] m_curveControls;

		public PowerCurveProfileWindow([NotNull] ArcticFoxConfiguration.PowerCurve curve)
		{
			if (curve == null) throw new ArgumentNullException("curve");

			m_curve = curve;

			InitializeComponent();
			InitializeChart();
			InitializeControls();
			InitializeWorkspace();
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
				area.AxisX.Interval = 1;

				area.AxisY.IsMarginVisible = false;
				area.AxisY.Minimum = (double)MinPercents;
				area.AxisY.Maximum = (double)MaxPercents;
				area.AxisY.MajorGrid.Enabled = true;
				area.AxisY.MajorGrid.LineColor = Color.FromArgb(230, 230, 230);
				area.AxisY.LineColor = Color.DarkGray;
				//area.AxisY.Interval = 0.5;
			}
			PowerCurveChart.ChartAreas.Add(area);

			var series = new Series
			{
				ChartType = SeriesChartType.Spline,
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
			PowerCurveChart.Series.Add(series);
			PowerCurveChart.Series.Add(centerSeries);
			PowerCurveChart.Series[1].Points.Add(new DataPoint(0, 100));
			PowerCurveChart.Series[1].Points.Add(new DataPoint(25, 100));
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

		private void InitializeWorkspace()
		{
			NameTextBox.Text = m_curve.Name;

			for (var i = 0; i < m_curve.Points.Length; i++)
			{
				var data = m_curve.Points[i];
				var timeUpDown = m_curveControls[i].TimeUpDown;
				var percentsUpDown = m_curveControls[i].PercentsUpDown;

				var time = Math.Max(MinTime, Math.Min(data.Time / 10m, MaxTime));
				var percents = Math.Max(MinPercents, Math.Min(data.Percent, MaxPercents));
				var point = new DataPoint((double)time, (double)percents)
				{
					MarkerStyle = MarkerStyle.Circle,
					MarkerSize = 7,
					Label = percents.ToString(CultureInfo.InvariantCulture)
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

				PowerCurveChart.Series[0].Points.Add(point);
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
