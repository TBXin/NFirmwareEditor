using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using JetBrains.Annotations;
using NCore;
using NCore.Serialization;
using NCore.UI;
using NToolbox.Models;
using NToolbox.Models.Export;

namespace NToolbox.Windows
{
	internal partial class DischargeProfileWindow : EditorDialogWindow
	{
		private const ushort MinPrc = 0;
		private const ushort MaxPrc = 100;
		private const decimal MinVolts = 3.0m;
		private const decimal MaxVolts = 4.2m;

		private readonly ArcticFoxConfiguration.CustomBattery m_battery;

		private PercentVoltsControlGroup[] m_curveControls;
		private ContextMenu m_presetsMenu;
		private bool m_isInstallingPreset;
		private bool m_isDragginPoint;
		private DataPoint m_pointUnderCursor;
		private int m_pointUnderCursorIndex;

		public DischargeProfileWindow([NotNull] ArcticFoxConfiguration.CustomBattery battery)
		{
			if (battery == null) throw new ArgumentNullException("battery");

			m_battery = battery;

			InitializeComponent();
			InitializeChart();
			InitializeControls();
			InitializeWorkspaceFromDataflash(m_battery);
		}

		private void InitializeControls()
		{
			m_curveControls = new[]
			{
				new PercentVoltsControlGroup(Percents11UpDown, Volts11UpDown),
				new PercentVoltsControlGroup(Percents10UpDown, Volts10UpDown),
				new PercentVoltsControlGroup(Percents9UpDown, Volts9UpDown),
				new PercentVoltsControlGroup(Percents8UpDown, Volts8UpDown),
				new PercentVoltsControlGroup(Percents7UpDown, Volts7UpDown),
				new PercentVoltsControlGroup(Percents6UpDown, Volts6UpDown),
				new PercentVoltsControlGroup(Percents5UpDown, Volts5UpDown),
				new PercentVoltsControlGroup(Percents4UpDown, Volts4UpDown),
				new PercentVoltsControlGroup(Percents3UpDown, Volts3UpDown),
				new PercentVoltsControlGroup(Percents2UpDown, Volts2UpDown),
				new PercentVoltsControlGroup(Percents1UpDown, Volts1UpDown)
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
			ExportButton.Click += ExportButton_Click;
			ImportButton.Click += ImportButton_Click;
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
					IsMarkerOverlappingAllowed = false,
					MovingDirection = LabelAlignmentStyles.Top
				}
			};
			DischargeChart.Series.Add(series);
			DischargeChart.MouseMove += DischargeChart_MouseMove;
			DischargeChart.MouseDown += DischargeChart_MouseDown;
			DischargeChart.MouseUp += DischargeChart_MouseUp;
		}

		private void InstallPreset(ArcticFoxConfiguration.CustomBattery customBattery)
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

		private void InitializeWorkspaceFromDataflash([NotNull] ArcticFoxConfiguration.CustomBattery battery)
		{
			if (battery == null) throw new ArgumentNullException("battery");

			for (var i = 0; i < battery.Data.Length; i++)
			{
				var data = battery.Data[i];
				var percents = Math.Max(MinPrc, Math.Min(data.Percents, MaxPrc));
				var voltage = Math.Max(MinVolts, Math.Min(data.Voltage / 100m, MaxVolts));
				var point = new DataPoint(percents, (double)voltage)
				{
					MarkerStyle = MarkerStyle.Circle,
					MarkerSize = 7,
					Label = string.Format("{0} %", percents),
					Tag = "draggable"
				};

				m_curveControls[i].PercentsUpDown.Value = percents;
				m_curveControls[i].PercentsUpDown.Tag = point;
				m_curveControls[i].PercentsUpDown.ValueChanged += PercentsUpDown_ValueChanged;

				m_curveControls[i].VoltsUpDown.Value = voltage;
				m_curveControls[i].VoltsUpDown.Tag = point;
				m_curveControls[i].VoltsUpDown.ValueChanged += VoltsUpDown_ValueChanged;

				DischargeChart.Series[0].Points.Add(point);
			}
			CutoffUpDown.Value = battery.Cutoff / 100m;
		}

		private void SaveWorkspaceToDataflash([NotNull] ArcticFoxConfiguration.CustomBattery battery)
		{
			if (battery == null) throw new ArgumentNullException("battery");

			for (var i = 0; i < battery.Data.Length; i++)
			{
				var data = m_curveControls[i];

				battery.Data[i].Percents = (ushort)data.PercentsUpDown.Value;
				battery.Data[i].Voltage = (ushort)(data.VoltsUpDown.Value * 100);
			}
			battery.Cutoff = (ushort)(CutoffUpDown.Value * 100);
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

		private void DischargeChart_MouseMove(object sender, MouseEventArgs e)
		{
			if (m_isDragginPoint && m_pointUnderCursor != null)
			{
				var xValueUnderCursor = DischargeChart.ChartAreas[0].AxisX.PixelPositionToValue(e.X);
				var yValueUnderCursor = DischargeChart.ChartAreas[0].AxisY.PixelPositionToValue(e.Y);

				var leftBound = m_pointUnderCursorIndex == m_battery.Data.Length - 1 ? MaxPrc : DischargeChart.Series[0].Points[m_pointUnderCursorIndex + 1].XValue -1;
				var rightBound = m_pointUnderCursorIndex == 0 ? MinPrc : DischargeChart.Series[0].Points[m_pointUnderCursorIndex -1].XValue + 1;

				var upperBound = m_pointUnderCursorIndex == m_battery.Data.Length - 1 ? (double)MaxVolts : DischargeChart.Series[0].Points[m_pointUnderCursorIndex + 1].YValues[0] - 0.01;
				var lowerBound = m_pointUnderCursorIndex == 0 ? (double)MinVolts : DischargeChart.Series[0].Points[m_pointUnderCursorIndex - 1].YValues[0] + 0.01;

				double tempXValue;
				if (xValueUnderCursor >= leftBound) tempXValue = leftBound;
				else if (xValueUnderCursor <= rightBound) tempXValue = rightBound;
				else tempXValue = xValueUnderCursor;

				double tempYValue;
				if (yValueUnderCursor >= upperBound) tempYValue = upperBound;
				else if (yValueUnderCursor <= lowerBound) tempYValue = lowerBound;
				else tempYValue = yValueUnderCursor;

				tempXValue = Math.Round(tempXValue, 0);
				tempYValue = Math.Round(tempYValue, 2);

				m_curveControls[m_pointUnderCursorIndex].VoltsUpDown.Value = (decimal)tempYValue;
				m_curveControls[m_pointUnderCursorIndex].PercentsUpDown.Value = (decimal)tempXValue;
			}
			else
			{
				var results = DischargeChart.HitTest(e.X, e.Y, false, ChartElementType.DataPoint);

				DataPoint point = null;
				foreach (var result in results)
				{
					if (result.ChartElementType != ChartElementType.DataPoint) continue;

					var tmpPoint = result.Object as DataPoint;
					if (tmpPoint == null) continue;
					if (!Equals(tmpPoint.Tag, "draggable")) continue;

					var pointX = DischargeChart.ChartAreas[0].AxisX.ValueToPixelPosition(tmpPoint.XValue);
					var pointY = DischargeChart.ChartAreas[0].AxisY.ValueToPixelPosition(tmpPoint.YValues[0]);

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

		private void DischargeChart_MouseDown(object sender, MouseEventArgs e)
		{
			m_isDragginPoint = true;
		}

		private void DischargeChart_MouseUp(object sender, MouseEventArgs e)
		{
			m_isDragginPoint = false;
		}

		private void ExportButton_Click(object sender, EventArgs e)
		{
			string filePath;
			using (var sf = new SaveFileDialog { Filter = FileFilters.XmlFilter })
			{
				if (sf.ShowDialog() != DialogResult.OK)
					return;
				filePath = sf.FileName;
			}

			try
			{
				using (var fs = File.Open(filePath, FileMode.Create))
				{
					var data = new BatteryProfile
					{
						Cutoff = CutoffUpDown.Value,
						Points = m_curveControls.Select(x => new BatteryProfilePoint
						{
							Percent = (byte)x.PercentsUpDown.Value,
							Voltage = x.VoltsUpDown.Value
						}).ToArray()
					};

					Serializer.Write(data, fs);
				}
			}
			catch (Exception ex)
			{
				Trace.Warn(ex);
				InfoBox.Show("An error occurred during battery profile export.");
			}
		}

		private void ImportButton_Click(object sender, EventArgs e)
		{
			string filePath;
			using (var op = new OpenFileDialog { Filter = FileFilters.XmlFilter })
			{
				if (op.ShowDialog() != DialogResult.OK) return;
				filePath = op.FileName;
			}

			try
			{
				using (var fs = File.Open(filePath, FileMode.Open))
				{
					var data = Serializer.Read<BatteryProfile>(fs);
					if (data == null || data.Points == null || data.Points.Length != m_curveControls.Length)
					{
						InfoBox.Show("Invalid battery profile file.");
						return;
					}
					var custom = new ArcticFoxConfiguration.CustomBattery
					{
						Cutoff = (ushort)(data.Cutoff * 100),
						Data = data.Points.Select(x => new ArcticFoxConfiguration.PercentsVoltage
						{
							Percents = x.Percent,
							Voltage = (ushort)(x.Voltage * 100)
						}).ToArray()
					};
					InstallPreset(custom);
				}
			}
			catch (Exception ex)
			{
				Trace.Warn(ex);
				InfoBox.Show("An error occurred during battery profile import.");
			}
		}

		private void SaveButton_Click(object sender, EventArgs e)
		{
			SaveWorkspaceToDataflash(m_battery);
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
