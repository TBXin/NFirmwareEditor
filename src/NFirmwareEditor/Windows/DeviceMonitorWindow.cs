using System;
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
		private static readonly Color s_batteryVoltageColor = Color.DarkSlateGray;
		private static readonly Color s_powerColor = Color.LimeGreen;
		private static readonly Color s_outputVoltageColor = Color.LightSkyBlue;
		private static readonly Color s_outputCurrentColor = Color.Orange;
		private static readonly Color s_resistanceColor = Color.BlueViolet;
		private static readonly Color s_realResistanceColor = Color.Violet;
		private static readonly Color s_temperatureColor = Color.DarkSlateGray;
		private static readonly Color s_boardTemperatureColor = Color.SaddleBrown;

		private const int MaxItems = 50;
		private readonly COMConnector m_comConnector;

		public DeviceMonitorWindow([NotNull] COMConnector comConnector)
		{
			if (comConnector == null) throw new ArgumentNullException("comConnector");
			m_comConnector = comConnector;

			InitializeComponent();
			InitializeControls();
			InitializeChart();

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

		private void ComConnector_Disconnected()
		{
			EnsureConnection();
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

		private void InitializeControls()
		{
			panel1.BackColor = s_batteryVoltageColor;
			panel2.BackColor = s_powerColor;
			panel3.BackColor = s_outputVoltageColor;
			panel4.BackColor = s_outputCurrentColor;
			panel5.BackColor = s_resistanceColor;
			panel6.BackColor = s_realResistanceColor;
			panel7.BackColor = s_temperatureColor;
			panel8.BackColor = s_boardTemperatureColor;
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

			AddSeries(SensorKeys.BatteryVoltage, s_batteryVoltageColor);
			AddSeries(SensorKeys.OutputVoltage, s_outputVoltageColor);
			AddSeries(SensorKeys.OutputCurrent, s_outputCurrentColor);
			AddSeries(SensorKeys.Resistance, s_resistanceColor);
			AddSeries(SensorKeys.RealResistance, s_realResistanceColor);
		}

		private void AddSeries(string name, Color color)
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
			MainChart.Series.Add(series);
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
					if (sensors.BatteryVoltage >= 0) MainChart.Series[SensorKeys.BatteryVoltage].Points.Add(sensors.BatteryVoltage);
					if (sensors.OutputVoltage >= 0) MainChart.Series[SensorKeys.OutputVoltage].Points.Add(sensors.OutputVoltage);
					if (sensors.OutputCurrent >= 0) MainChart.Series[SensorKeys.OutputCurrent].Points.Add(sensors.OutputCurrent);
					if (sensors.Resistance >= 0) MainChart.Series[SensorKeys.Resistance].Points.Add(sensors.Resistance);
					if (sensors.RealResistance >= 0) MainChart.Series[SensorKeys.RealResistance].Points.Add(sensors.RealResistance);

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
