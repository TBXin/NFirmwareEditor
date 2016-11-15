using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using NCore;
using NCore.USB;
using NToolbox.Models;

namespace NToolbox.Windows
{
	public partial class MainWindow : Form
	{
		private readonly HidConnector m_connector = new HidConnector();

		public MainWindow()
		{
			InitializeComponent();

			ArcticFoxConfigurationButton.Click += (s, e) =>
			{
				using (var cfg = new ArcticFoxConfigurationWindow())
				{
					ShowDialogWindow(cfg);
				}
			};

			MyEvicConfigurationButton.Click += (s, e) =>
			{
				using (var cfg = new ArcticFoxConfigurationWindow())
				{
					ShowDialogWindow(cfg);
				}
			};

			DeviceMonitorButton.Click += (s, e) =>
			{
				using (var cfg = new ArcticFoxConfigurationWindow())
				{
					ShowDialogWindow(cfg);
				}
			};

			FirmwareUpdaterButton.Click += (s, e) =>
			{
				using (var cfg = new ArcticFoxConfigurationWindow())
				{
					ShowDialogWindow(cfg);
				}
			};

			m_connector.DeviceConnected += DeviceConnected;
			Load += (s, e) => m_connector.StartUSBConnectionMonitoring();
			Closing += (s, e) => m_connector.StopUSBConnectionMonitoring();
		}

		private void DeviceConnected(bool isConnected)
		{
			if (!isConnected) return;

			var data = m_connector.ReadConfiguration();
			var af = BinaryStructureManager.Read<ArcticFoxConfiguration>(data);
			
			/*af.General.Profiles[0].Name = "ZBSVASHE";
			af.Interface.VWLines.Line1 = ArcticFoxConfiguration.LineContent.DateTime;
			af.Interface.VWLines.Line2 = ArcticFoxConfiguration.LineContent.Puffs;
			af.Interface.VWLines.Line3 = ArcticFoxConfiguration.LineContent.Time;
			af.Interface.VWLines.Line4 = (ArcticFoxConfiguration.LineContent)0x35 | ArcticFoxConfiguration.LineContent.FireTimeMask;*/

			var data2 = BinaryStructureManager.Write(af, new byte[512]);
			m_connector.WriteConfiguration(data2);
		}

		private DialogResult ShowDialogWindow(Form window)
		{
			Hide();
			var result = window.ShowDialog();
			Thread.Sleep(150);
			Show();

			return result;
		}
	}
}
