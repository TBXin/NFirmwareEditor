using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using JetBrains.Annotations;
using NCore;
using NCore.UI;
using NToolbox.Models;

namespace NToolbox.Windows
{
	internal partial class TFRProfileWindow : EditorDialogWindow
	{
		private const ushort MinTemperature = 0;
		private const ushort MaxTemperature = 800;
		private const decimal MinFactor = 1.0m;
		private const decimal MaxFactor = 4.0m;

		private static readonly Regex s_blackList = new Regex("(?![a-zA-Z0-9\\+\\-\\.\\s]).", RegexOptions.Compiled);
		private readonly ArcticFoxConfiguration.TFRTable m_tfrTable;

		private TempFactorControlGroup[] m_curveControls;
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
				area.AxisX.Minimum = MinTemperature;
				area.AxisX.Maximum = MaxTemperature;
				area.AxisX.MajorGrid.Enabled = true;
				area.AxisX.MajorGrid.LineColor = Color.FromArgb(230, 230, 230);
				area.AxisX.LineColor = Color.DarkGray;
				area.AxisX.Interval = 100;

				area.AxisY.IsMarginVisible = false;
				area.AxisY.Minimum = (double)MinFactor;
				area.AxisY.Maximum = (double)MaxFactor;
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

			ExportButton.Click += ExportButton_Click;
			ImportButton.Click += ImportButton_Click;

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

				var temperature = Math.Max(MinTemperature, Math.Min(data.Temperature, MaxTemperature));
				var factor = Math.Max(MinFactor, Math.Min(data.Factor / 10000m, MaxFactor));
				var point = new DataPoint(temperature, (double)factor)
				{
					MarkerStyle = MarkerStyle.Circle,
					MarkerSize = 7,
					Label = factor.ToString(CultureInfo.InvariantCulture)
				};

				temperatureUpDown.Minimum = MinTemperature;
				temperatureUpDown.Maximum = MaxTemperature;
				temperatureUpDown.Value = temperature;
				temperatureUpDown.Tag = point;
				temperatureUpDown.ValueChanged += TemperatureUpDown_ValueChanged;

				factorUpDown.Minimum = MinFactor;
				factorUpDown.Maximum = MaxFactor;
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

		private void ImportTFRCurve(CsvFile file)
		{
			try
			{
				m_isInstallingPreset = true;
				var counter = 0;
				for (var i = file.Lines.Count - m_curveControls.Length; i < file.Lines.Count; i++)
				{
					var line = file.Lines[i];
					var temp = line[0];
					var factor = line[1];

					m_curveControls[counter].TemperatureUpDown.Value = Math.Round(decimal.Parse(temp, CultureInfo.InvariantCulture), 0);
					m_curveControls[counter].FactorUpDown.Value = Math.Round(decimal.Parse(factor, CultureInfo.InvariantCulture), 4);
					counter++;
				}
			}
			catch (Exception ex)
			{
				m_isInstallingPreset = false;
				InfoBox.Show("An error occurred during TFR curve import.\n" + ex);
			}
		}

		public CsvFile ExportTFRCurve()
		{
			var headers = new[] { "Temperature (degF)", "Electrical Resistivity" };
			var lines = new List<string[]>(m_curveControls.Length);
			foreach (var group in m_curveControls)
			{
				lines.Add(new[]
				{
					group.TemperatureUpDown.Value.ToString(CultureInfo.InvariantCulture),
					group.FactorUpDown.Value.ToString(CultureInfo.InvariantCulture)
				});
			}
			return new CsvFile(headers, lines);
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
					var prevTemperature = m_curveControls[i - 1].TemperatureUpDown;
					var prevFactor = m_curveControls[i - 1].FactorUpDown;

					group.TemperatureUpDown.Minimum = Math.Min(MaxTemperature, prevTemperature.Value + 1);
					group.FactorUpDown.Minimum = Math.Min(MaxFactor, prevFactor.Value + 0.0001m);
				}

				if (i + 1 < m_curveControls.Length)
				{
					var nextTemperature = m_curveControls[i + 1].TemperatureUpDown;
					var nextFactor = m_curveControls[i + 1].FactorUpDown;

					group.TemperatureUpDown.Maximum = Math.Max(MinTemperature, nextTemperature.Value - 1);
					group.FactorUpDown.Maximum = Math.Max(MinFactor, nextFactor.Value - 0.0001m);
				}
			}
		}

		private void ImportButton_Click(object sender, EventArgs e)
		{
			string fileName;
			using (var op = new OpenFileDialog { Filter = FileFilters.CsvFilter })
			{
				if (op.ShowDialog() != DialogResult.OK)
					return;
				fileName = op.FileName;
			}

			CsvFile file;
			try
			{
				file = CsvManager.Read(fileName);
				if (file == null || file.Headers.Length != 2 || file.Lines.Any(x => x.Length != 2))
				{
					InfoBox.Show("Wrong file format. File should have header, and every line should have 2 values");
				}
			}
			catch (Exception ex)
			{
				m_isInstallingPreset = false;
				InfoBox.Show("An error occurred during parsing file.\n" + ex);
				return;
			}

			ImportTFRCurve(file);
		}

		private void ExportButton_Click(object sender, EventArgs e)
		{
			string fileName;
			using (var sf = new SaveFileDialog { FileName = string.Format("Arctic Fox-{0}-(TFR Curve)", NameTextBox.Text), Filter = FileFilters.CsvFilter })
			{
				if (sf.ShowDialog() != DialogResult.OK) return;
				fileName = sf.FileName;
			}

			try
			{
				var file = ExportTFRCurve();
				CsvManager.Write(file, fileName);
			}
			catch (Exception ex)
			{
				InfoBox.Show("An error occurred during TFR curve export.\n" + ex);
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
