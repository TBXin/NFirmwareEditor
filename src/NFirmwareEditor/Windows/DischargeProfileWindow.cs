using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using JetBrains.Annotations;
using NCore.UI;
using NFirmwareEditor.Models;

namespace NFirmwareEditor.Windows
{
	internal partial class DischargeProfileWindow : EditorDialogWindow
	{
		private const ushort MinPrc = 0;
		private const ushort MaxPrc = 100;
		private const decimal MinVolts = 3.0m;
		private const decimal MaxVolts = 4.2m;

		private readonly Dataflash m_dataflash;

		private PercentVoltsControlGroup[] m_curveControls;
		private ContextMenu m_presetsMenu;
		private bool m_isInstallingPreset;

		public DischargeProfileWindow([NotNull] Dataflash dataflash)
		{
			if (dataflash == null || dataflash.ParamsBlock == null || dataflash.ParamsBlock.CustomBattery == null) throw new ArgumentNullException("dataflash");
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

			m_presetsMenu = new ContextMenu();
			foreach (var kvp in BatteryPresets.Presets)
			{
				var presetName = kvp.Key;
				var customBattery = kvp.Value;
				m_presetsMenu.MenuItems.Add(presetName, (s, e) => InstallPreset(customBattery));
			}

			PresetsButton.Click += (s, e) =>
			{
				var control = (Control)s;
				m_presetsMenu.Show(control, new Point(control.Width, 0));
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
				area.AxisY.Minimum = 3.0;
				area.AxisY.Maximum = 4.2;
				area.AxisY.MajorGrid.Enabled = true;
				area.AxisY.MajorGrid.LineColor = Color.FromArgb(230, 230, 230);
				area.AxisY.LineColor = Color.DarkGray;
				area.AxisY.Interval = 0.1;
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

		private void InstallPreset(CustomBattery customBattery)
		{
			m_isInstallingPreset = true;
			for (var i = 0; i < customBattery.Data.Length; i++)
			{
				var data = customBattery.Data[i];
				var group = m_curveControls[i];

				var percents = Math.Max(MinPrc, Math.Min(data.Percents, MaxPrc));
				var voltage = Math.Max(MinVolts, Math.Min(data.Voltage / 100m, MaxVolts));

				group.PercentsUpDown.Minimum = MinPrc;
				group.PercentsUpDown.Maximum = MaxPrc;
				group.PercentsUpDown.Value = percents;

				group.VoltsUpDown.Minimum = MinVolts;
				group.VoltsUpDown.Maximum = MaxVolts;
				group.VoltsUpDown.Value = voltage;
			}
			m_isInstallingPreset = false;
			UpdatePointsMinMax();
		}

		private void InitializeWorkspaceFromDataflash([NotNull] Dataflash dataflash)
		{
			if (dataflash == null) throw new ArgumentNullException("dataflash");

			for (var i = 0; i < dataflash.ParamsBlock.CustomBattery.Data.Length; i++)
			{
				var data = dataflash.ParamsBlock.CustomBattery.Data[i];
				var percents = Math.Max(MinPrc, Math.Min(data.Percents, MaxPrc));
				var voltage = Math.Max(MinVolts, Math.Min(data.Voltage / 100m, MaxVolts));
				var point = new DataPoint(percents, (double)voltage)
				{
					MarkerStyle = MarkerStyle.Circle,
					MarkerSize = 7,
					Label = string.Format("{0} %", percents)
				};

				m_curveControls[i].PercentsUpDown.Value = percents;
				m_curveControls[i].PercentsUpDown.Tag = point;
				m_curveControls[i].PercentsUpDown.ValueChanged += PercentsUpDown_ValueChanged;

				m_curveControls[i].VoltsUpDown.Value = voltage;
				m_curveControls[i].VoltsUpDown.Tag = point;
				m_curveControls[i].VoltsUpDown.ValueChanged += VoltsUpDown_ValueChanged;

				DischargeChart.Series[0].Points.Add(point);
			}
			CutoffUpDown.Value = dataflash.ParamsBlock.CustomBattery.Cutoff / 100m;
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
			dataflash.ParamsBlock.CustomBattery.Cutoff = (ushort)(CutoffUpDown.Value * 100);
		}

		private void UpdatePointsMinMax()
		{
			if (m_isInstallingPreset) return;
			for (var i = 0; i < m_curveControls.Length; i++)
			{
				var group = m_curveControls[i];

				if (i - 1 >= 0)
				{
					var prevPercents = m_curveControls[i - 1].PercentsUpDown;
					var prevVoltage = m_curveControls[i - 1].VoltsUpDown;

					group.PercentsUpDown.Minimum = Math.Min(MaxPrc, prevPercents.Value + 1);
					group.VoltsUpDown.Minimum = Math.Min(MaxVolts, prevVoltage.Value + 0.01m);
				}

				if (i + 1 < m_curveControls.Length)
				{
					var nextPercents = m_curveControls[i + 1].PercentsUpDown;
					var nextVoltage = m_curveControls[i + 1].VoltsUpDown;

					group.PercentsUpDown.Maximum = Math.Max(MinPrc, nextPercents.Value - 1);
					group.VoltsUpDown.Maximum = Math.Max(MinVolts, nextVoltage.Value - 0.01m);
				}
			}
		}

		private void VoltsUpDown_ValueChanged(object sender, EventArgs e)
		{
			var control = sender as NumericUpDown;
			if (control == null) return;

			var point = control.Tag as DataPoint;
			if (point == null) return;

			var value = control.Value;

			UpdatePointsMinMax();
			point.YValues = new[] { (double)value };
		}

		private void PercentsUpDown_ValueChanged(object sender, EventArgs e)
		{
			var control = sender as NumericUpDown;
			if (control == null) return;

			var point = control.Tag as DataPoint;
			if (point == null) return;

			var value = control.Value;

			UpdatePointsMinMax();

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
