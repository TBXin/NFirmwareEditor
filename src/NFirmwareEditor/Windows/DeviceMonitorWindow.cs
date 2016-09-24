using System;
using System.Drawing;
using System.Globalization;
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
		private readonly COMConnector m_comConnector;

		public DeviceMonitorWindow([NotNull] COMConnector comConnector)
		{
			if (comConnector == null) throw new ArgumentNullException("comConnector");
			m_comConnector = comConnector;

			InitializeComponent();
			InitializeChart();

			Load += (s, e) =>
			{
				m_comConnector.Connect();
				m_comConnector.Send("M1");
				m_comConnector.MessageReceived += ComConnector_MessageReceived;
			};
			Closing += (s, e) => Safe.Execute(() => m_comConnector.Disconnect());
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

			AddSeries(SensorKeys.BatteryVoltage, Color.DarkSlateGray);
			AddSeries(SensorKeys.OutputVoltage, Color.LightSkyBlue);
			AddSeries(SensorKeys.OutputCurrent, Color.Orange);
			AddSeries(SensorKeys.Resistance, Color.BlueViolet);
			AddSeries(SensorKeys.RealResistance, Color.Violet);
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
