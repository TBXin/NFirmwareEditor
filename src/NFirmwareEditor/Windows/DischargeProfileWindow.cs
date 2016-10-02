using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using JetBrains.Annotations;
using NFirmwareEditor.Models;

namespace NFirmwareEditor.Windows
{
	internal partial class DischargeProfileWindow : EditorDialogWindow
	{
		private readonly Dataflash m_dataflash;
		private PercentVoltsControlGroup[] m_curveControls;
		private bool m_isUpdating;

		public DischargeProfileWindow([NotNull] Dataflash dataflash)
		{
			if (dataflash == null) throw new ArgumentNullException("dataflash");
			m_dataflash = dataflash;

			InitializeComponent();
			InitializeChart();
			InitializeControls();
			InitializeWorkspaceFromDataflash(m_dataflash);
		}

		private void InitializeControls()
		{
			m_curveControls = new[]
			{
				new PercentVoltsControlGroup(Percents1UpDown, Volts1UpDown),
				new PercentVoltsControlGroup(Percents2UpDown, Volts2UpDown),
				new PercentVoltsControlGroup(Percents3UpDown, Volts3UpDown),
				new PercentVoltsControlGroup(Percents4UpDown, Volts4UpDown),
				new PercentVoltsControlGroup(Percents5UpDown, Volts5UpDown),
				new PercentVoltsControlGroup(Percents6UpDown, Volts6UpDown),
				new PercentVoltsControlGroup(Percents7UpDown, Volts7UpDown),
				new PercentVoltsControlGroup(Percents8UpDown, Volts8UpDown),
				new PercentVoltsControlGroup(Percents9UpDown, Volts9UpDown),
				new PercentVoltsControlGroup(Percents10UpDown, Volts10UpDown),
				new PercentVoltsControlGroup(Percents11UpDown, Volts11UpDown)
			};

			SaveButton.Click += SaveButton_Click;
		}

		private void InitializeChart()
		{
			DischargeChart.Palette = ChartColorPalette.Pastel;
			var area = new ChartArea();
			{
				area.AxisX.IsMarginVisible = false;
				area.AxisX.Maximum = 100;
				area.AxisX.MajorGrid.Enabled = true;
				area.AxisX.MajorGrid.LineColor = Color.FromArgb(230, 230, 230);
				area.AxisX.LineColor = Color.DarkGray;
				area.AxisX.IsReversed = true;
				area.AxisX.Interval = 5;

				area.AxisY.IsMarginVisible = false;
				area.AxisY.Maximum = 4.5;
				area.AxisY.MajorGrid.Enabled = true;
				area.AxisY.MajorGrid.LineColor = Color.FromArgb(230, 230, 230);
				area.AxisY.LineColor = Color.DarkGray;
				area.AxisY.Interval = 0.5;
			}
			DischargeChart.ChartAreas.Add(area);

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
			DischargeChart.Series.Add(series);
		}

		private void InitializeWorkspaceFromDataflash([NotNull] Dataflash dataflash)
		{
			if (dataflash == null) throw new ArgumentNullException("dataflash");

			for (var i = 0; i < dataflash.ParamsBlock.CustomBattery.Data.Length; i++)
			{
				var data = dataflash.ParamsBlock.CustomBattery.Data[i];
				var percents = Math.Max((ushort)0, Math.Min(data.Percents, (ushort)100));
				var voltage = Math.Max(3.0m, Math.Min(data.Voltage / 100m, 4.2m));
				var point = new DataPoint(percents, (double)voltage)
				{
					MarkerStyle = MarkerStyle.Circle,
					MarkerSize = 7,
					Label = string.Format("{0} %", percents)
				};
				var tag = new Tuple<int, DataPoint>(i, point);

				m_curveControls[i].PercentsUpDown.Value = percents;
				m_curveControls[i].PercentsUpDown.Tag = tag;
				m_curveControls[i].PercentsUpDown.ValueChanged += PercentsUpDown_ValueChanged;

				m_curveControls[i].VoltsUpDown.Value = voltage;
				m_curveControls[i].VoltsUpDown.Tag = tag;
				m_curveControls[i].VoltsUpDown.ValueChanged += VoltsUpDown_ValueChanged;

				DischargeChart.Series[0].Points.Add(point);
			}
		}

		private void SaveWorkspaceToDataflash([NotNull] Dataflash dataflash)
		{
			if (dataflash == null) throw new ArgumentNullException("dataflash");

			for (var i = 0; i < dataflash.ParamsBlock.CustomBattery.Data.Length; i++)
			{
				var data = m_curveControls[i];

				dataflash.ParamsBlock.CustomBattery.Data[i].Percents = (ushort)data.PercentsUpDown.Value;
				dataflash.ParamsBlock.CustomBattery.Data[i].Voltage = (ushort)(data.VoltsUpDown.Value * 100);
			}
		}

		private void VoltsUpDown_ValueChanged(object sender, EventArgs e)
		{
			if (m_isUpdating) return;
			var control = sender as NumericUpDown;
			if (control == null) return;

			var data = control.Tag as Tuple<int, DataPoint>;
			if (data == null) return;

			var idx = data.Item1;
			var point = data.Item2;
			var value = control.Value;

			if (idx - 1 >= 0)
			{
				var prev = m_curveControls[idx - 1].VoltsUpDown;
				if (prev.Value >= value) prev.Value = Math.Max(prev.Minimum, value - 0.01m);
			}
			if (idx + 1 < m_curveControls.Length)
			{
				var next = m_curveControls[idx + 1].VoltsUpDown;
				if (next.Value <= value) next.Value = Math.Min(next.Maximum, value + 0.01m);
			}

			point.YValues = new[] { (double)value };
		}

		private void PercentsUpDown_ValueChanged(object sender, EventArgs e)
		{
			if (m_isUpdating) return;
			var control = sender as NumericUpDown;
			if (control == null) return;

			var data = control.Tag as Tuple<int, DataPoint>;
			if (data == null) return;

			var idx = data.Item1;
			var point = data.Item2;
			var value = control.Value;

			if (idx - 1 >= 0)
			{
				var prev = m_curveControls[idx - 1].PercentsUpDown;
				if (prev.Value >= value) prev.Value = Math.Max(prev.Minimum, value - 1);
			}
			if (idx + 1 < m_curveControls.Length)
			{
				var next = m_curveControls[idx + 1].PercentsUpDown;
				if (next.Value <= value) next.Value = Math.Min(next.Maximum, value + 1);
			}

			point.XValue = (double)value;
			point.Label = string.Format("{0} %", value);
		}

		private void SaveButton_Click(object sender, EventArgs e)
		{
			SaveWorkspaceToDataflash(m_dataflash);
			DialogResult = DialogResult.OK;
		}

		private class PercentVoltsControlGroup
		{
			public PercentVoltsControlGroup(NumericUpDown percentsUpDown, NumericUpDown voltsUpDown)
			{
				PercentsUpDown = percentsUpDown;
				VoltsUpDown = voltsUpDown;
			}

			public NumericUpDown PercentsUpDown { get; private set; }

			public NumericUpDown VoltsUpDown { get; private set; }
		}
	}
}
