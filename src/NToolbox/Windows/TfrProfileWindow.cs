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
	internal partial class TFRProfileWindow : EditorDialogWindow
	{
		private static readonly Regex s_blackList = new Regex("(?![a-zA-Z0-9\\+\\-\\.\\s]).", RegexOptions.Compiled);
		private readonly ArcticFoxConfiguration.TFRTable m_tfrTable;

		private TempFactorControlGroup[] m_curveControls;
		private ContextMenu m_presetsMenu;
		private bool m_isInstallingPreset;

		public TFRProfileWindow([NotNull] ArcticFoxConfiguration.TFRTable tfrTable)
		{
			if (tfrTable == null) throw new ArgumentNullException("tfrTable");

			m_tfrTable = tfrTable;

			InitializeComponent();
			InitializeChart();
			InitializeControls();
			InitializeWorkspace();
		}

		private void InitializeChart()
		{
			TFRChart.Palette = ChartColorPalette.Pastel;
			var area = new ChartArea();
			{
				area.AxisX.IsMarginVisible = false;
				area.AxisX.Minimum = 0;
				area.AxisX.Maximum = 600;
				area.AxisX.MajorGrid.Enabled = true;
				area.AxisX.MajorGrid.LineColor = Color.FromArgb(230, 230, 230);
				area.AxisX.LineColor = Color.DarkGray;
				area.AxisX.Interval = 100;

				area.AxisY.IsMarginVisible = false;
				area.AxisY.Minimum = 1;
				area.AxisY.Maximum = 4;
				area.AxisY.MajorGrid.Enabled = true;
				area.AxisY.MajorGrid.LineColor = Color.FromArgb(230, 230, 230);
				area.AxisY.LineColor = Color.DarkGray;
				area.AxisY.Interval = 0.5;
			}
			TFRChart.ChartAreas.Add(area);

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
			TFRChart.Series.Add(series);
		}

		private void InitializeControls()
		{
			m_curveControls = new[]
			{
				new TempFactorControlGroup(Percents1UpDown, Volts1UpDown),
				new TempFactorControlGroup(Percents2UpDown, Volts2UpDown),
				new TempFactorControlGroup(Percents3UpDown, Volts3UpDown),
				new TempFactorControlGroup(Percents4UpDown, Volts4UpDown),
				new TempFactorControlGroup(Percents5UpDown, Volts5UpDown),
				new TempFactorControlGroup(Percents6UpDown, Volts6UpDown),
				new TempFactorControlGroup(Percents7UpDown, Volts7UpDown),
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
			NameTextBox.Text = m_tfrTable.Name;

			for (var i = 0; i < m_tfrTable.Points.Length; i++)
			{
				var data = m_tfrTable.Points[i];
				var temperatureUpDown = m_curveControls[i].TemperatureUpDown;
				var factorUpDown = m_curveControls[i].FactorUpDown;

				var temperature = Math.Max(temperatureUpDown.Minimum, Math.Min(data.Temperature, temperatureUpDown.Maximum));
				var factor = Math.Max(factorUpDown.Minimum, Math.Min(data.Factor / 10000m, factorUpDown.Maximum));
				var point = new DataPoint((double)temperature, (double)factor)
				{
					MarkerStyle = MarkerStyle.Circle,
					MarkerSize = 7,
					Label = factor.ToString(CultureInfo.InvariantCulture)
				};

				temperatureUpDown.Value = temperature;
				temperatureUpDown.Tag = point;
				temperatureUpDown.ValueChanged += TemperatureUpDown_ValueChanged;

				factorUpDown.Value = factor;
				factorUpDown.Tag = point;
				factorUpDown.ValueChanged += FactorUpDown_ValueChanged;

				TFRChart.Series[0].Points.Add(point);
			}
		}

		private void SaveWorkspace()
		{
			m_tfrTable.Name = NameTextBox.Text;

			for (var i = 0; i < m_tfrTable.Points.Length; i++)
			{
				var data = m_curveControls[i];

				m_tfrTable.Points[i].Temperature = (ushort)data.TemperatureUpDown.Value;
				m_tfrTable.Points[i].Factor = (ushort)(data.FactorUpDown.Value * 10000);
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
			if (m_isInstallingPreset) return;

			for (var i = 0; i < m_curveControls.Length; i++)
			{
				var group = m_curveControls[i];

				if (i - 1 >= 0)
				{
					var prevPercents = m_curveControls[i - 1].TemperatureUpDown;
					group.TemperatureUpDown.Minimum = Math.Min(600, prevPercents.Value + 1);
				}

				if (i + 1 < m_curveControls.Length)
				{
					var nextPercents = m_curveControls[i + 1].TemperatureUpDown;
					group.TemperatureUpDown.Maximum = Math.Max(0, nextPercents.Value - 1);
				}
			}
		}

		private void SaveButton_Click(object sender, EventArgs e)
		{
			SaveWorkspace();
			DialogResult = DialogResult.OK;
		}

		private class TempFactorControlGroup
		{
			public TempFactorControlGroup(NumericUpDown percentsUpDown, NumericUpDown voltsUpDown)
			{
				TemperatureUpDown = percentsUpDown;
				FactorUpDown = voltsUpDown;
			}

			public NumericUpDown TemperatureUpDown { get; private set; }

			public NumericUpDown FactorUpDown { get; private set; }
		}
	}
}
