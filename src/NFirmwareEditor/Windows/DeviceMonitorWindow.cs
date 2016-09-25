using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
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
		private static readonly IDictionary<string, Color> s_sensorColorMap = new Dictionary<string, Color>
		{
			{ SensorKeys.BatteryVoltage,  Color.DarkSlateGray},
			{ SensorKeys.Power,  Color.LimeGreen},
			{ SensorKeys.OutputVoltage,  Color.LightSkyBlue},
			{ SensorKeys.OutputCurrent,  Color.Orange},
			{ SensorKeys.Resistance,  Color.BlueViolet},
			{ SensorKeys.RealResistance,  Color.Violet},
			{ SensorKeys.Temperature,  Color.DeepPink},
			{ SensorKeys.BoardTemperature,  Color.SaddleBrown},
		};

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
		}

		private void InitializeControls()
		{
			InitializeChart();

			InititalizeSeriesCheckbox(BatteryCheckBox, SensorKeys.BatteryVoltage);
			InititalizeSeriesCheckbox(PowerCheckBox, SensorKeys.Power);
			InititalizeSeriesCheckbox(OutputVoltageCheckBox, SensorKeys.OutputVoltage);
			InititalizeSeriesCheckbox(OutputCurrentCheckBox, SensorKeys.OutputCurrent);
			InititalizeSeriesCheckbox(ResistanceCheckBox, SensorKeys.Resistance);
			InititalizeSeriesCheckbox(RealResistanceCheckBox, SensorKeys.RealResistance);
			InititalizeSeriesCheckbox(TemperatureCheckBox, SensorKeys.Temperature);
			InititalizeSeriesCheckbox(BoardTemperatureCheckBox, SensorKeys.BoardTemperature);

			panel1.BackColor = s_sensorColorMap[BatteryCheckBox.Tag.ToString()];
			panel2.BackColor = s_sensorColorMap[PowerCheckBox.Tag.ToString()];
			panel3.BackColor = s_sensorColorMap[OutputVoltageCheckBox.Tag.ToString()];
			panel4.BackColor = s_sensorColorMap[OutputCurrentCheckBox.Tag.ToString()];
			panel5.BackColor = s_sensorColorMap[ResistanceCheckBox.Tag.ToString()];
			panel6.BackColor = s_sensorColorMap[RealResistanceCheckBox.Tag.ToString()];
			panel7.BackColor = s_sensorColorMap[TemperatureCheckBox.Tag.ToString()];
			panel8.BackColor = s_sensorColorMap[BoardTemperatureCheckBox.Tag.ToString()];
		}

		private void InititalizeSeriesCheckbox(CheckBox checkbox, string seriesName)
		{
			checkbox.Tag = seriesName;
			checkbox.CheckedChanged += SeriesCheckBox_CheckedChanged;
			checkbox.Checked = true;
		}

		private void SeriesCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			var checkbox = sender as CheckBox;
			if (checkbox == null || checkbox.Tag == null || string.IsNullOrEmpty(checkbox.Tag.ToString())) return;

			var seriesName = checkbox.Tag.ToString();
			if (checkbox.Checked)
			{
				var idx = MainChart.Series.IndexOf(seriesName);
				if (idx != -1) return;

				var series = CreateSeries(seriesName, s_sensorColorMap[seriesName]);
				MainChart.Series.Add(series);
			}
			else
			{
				var idx = MainChart.Series.IndexOf(seriesName);
				if (idx == -1) return;

				MainChart.Series.RemoveAt(idx);
			}
		}

		private void InitializeChart()
		{
			MainChart.Palette = ChartColorPalette.Pastel;
			var area = new ChartArea();
			{
				area.AxisX.MajorGrid.LineColor = Color.LightGray;
				area.AxisX.Maximum = MaxItems;
				area.AxisX.IsMarginVisible = false;
				area.AxisX.LabelStyle.Enabled = false;
				area.AxisY.MajorGrid.LineColor = Color.LightGray;
				area.AxisY.LabelStyle.Enabled = false;
			}
			MainChart.ChartAreas.Add(area);
		}

		private void EnsureConnection()
		{
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
					UpdateUI(() => Close());
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
					if (!MainChart.Series.IsUniqueName(SensorKeys.BatteryVoltage) && sensors.BatteryVoltage >= 0)
						MainChart.Series[SensorKeys.BatteryVoltage].Points.Add(sensors.BatteryVoltage);

					if (!MainChart.Series.IsUniqueName(SensorKeys.OutputVoltage) && sensors.OutputVoltage >= 0)
						MainChart.Series[SensorKeys.OutputVoltage].Points.Add(sensors.OutputVoltage);

					if (!MainChart.Series.IsUniqueName(SensorKeys.OutputCurrent) && sensors.OutputCurrent >= 0)
						MainChart.Series[SensorKeys.OutputCurrent].Points.Add(sensors.OutputCurrent);

					if (!MainChart.Series.IsUniqueName(SensorKeys.Resistance) && sensors.Resistance >= 0)
						MainChart.Series[SensorKeys.Resistance].Points.Add(sensors.Resistance);

					if (!MainChart.Series.IsUniqueName(SensorKeys.RealResistance) && sensors.RealResistance >= 0)
						MainChart.Series[SensorKeys.RealResistance].Points.Add(sensors.RealResistance);

					foreach (var series in MainChart.Series)
					{
						while (series.Points.Count > MaxItems)
						{
							series.Points.RemoveAt(0);
						}

						if (series.Points.Count > 0)
						{
							var point = series.Points[series.Points.Count - 1];
							if (point.YValues[0] != 0)
							{
								point.Label = Math.Round(point.YValues[0], 2).ToString(CultureInfo.InvariantCulture);
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
	}
}
