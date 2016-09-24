using System;
using System.Drawing;
using System.Linq;
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
				//area.AxisY.Maximum = 100;
				//area.AxisY.Title = "CPU Usage %";
			}
			MainChart.ChartAreas.Add(area);

			AddSeries(SensorKeys.BatteryVoltage, Color.DarkSlateGray);
			AddSeries(SensorKeys.OutputVoltage, Color.LightSkyBlue);
			AddSeries(SensorKeys.OutputCurrent, Color.Orange);
			AddSeries(SensorKeys.Resistance, Color.BlueViolet);
			AddSeries(SensorKeys.RealResistance, Color.Violet);

			MainChart.PostPaint += MainChart_PostPaint;
		}

		private void MainChart_PostPaint(object sender, ChartPaintEventArgs e)
		{
			var area = e.Chart.ChartAreas[0];
			if (area == null) return;

			foreach (var series in e.Chart.Series)
			{
				if (series.Points.Count == 0) continue;

				series.IsValueShownAsLabel = true;
				var lastPoint = series.Points[series.Points.Count - 1];

				var xPos = (float)area.AxisX.ValueToPixelPosition(lastPoint.XValue);
				var yPos = (float)area.AxisY.ValueToPixelPosition(lastPoint.YValues.First());

				e.ChartGraphics.Graphics.DrawString(lastPoint.YValues[0].ToString(), Font, Brushes.Black, xPos, yPos);
			}
		}

		private void AddSeries(string name, Color color)
		{
			MainChart.Series.Add(new Series
			{
				Name = name,
				ChartType = SeriesChartType.FastLine,
				Color = color,
				BorderWidth = 2
			});
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
					MainChart.Series[SensorKeys.BatteryVoltage].Points.Add(sensors.BatteryVoltage);
					MainChart.Series[SensorKeys.OutputVoltage].Points.Add(sensors.OutputVoltage);
					MainChart.Series[SensorKeys.OutputCurrent].Points.Add(sensors.OutputCurrent);
					MainChart.Series[SensorKeys.Resistance].Points.Add(sensors.Resistance);
					MainChart.Series[SensorKeys.RealResistance].Points.Add(sensors.RealResistance);

					foreach (var series in MainChart.Series)
					{
						while (series.Points.Count > MaxItems)
						{
							series.Points.RemoveAt(0);
						}
					}

					MainChart.ChartAreas[0].RecalculateAxesScale();
				});
			}
		}
	}
}
