using System.Threading;
using System.Windows.Forms;
using NCore;
using NCore.UI;

namespace NToolbox.Windows
{
	public partial class MainWindow : WindowBase
	{
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
				InfoBox.Show("Work in progress... Be patient.");
			};

			DeviceMonitorButton.Click += (s, e) =>
			{
				InfoBox.Show("Work in progress... Be patient.");
			};

			ScreenshooterButton.Click += (s, e) =>
			{
				using (var ss = new ScreenshooterWindow())
				{
					ShowDialogWindow(ss);
				}
			};

			FirmwareUpdaterButton.Click += (s, e) =>
			{
				InfoBox.Show("Work in progress... Be patient.");
			};
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
