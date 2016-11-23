using System.Threading;
using System.Windows.Forms;
using NCore;
using NCore.UI;

namespace NToolbox.Windows
{
	public partial class MainWindow : WindowBase
	{
		private readonly bool m_startMinimized;

		public MainWindow(bool startMinimized)
		{
			m_startMinimized = startMinimized;

			InitializeComponent();
			Initialize();
			InitializeTray();
		}

		private void Initialize()
		{
			Visible = ShowInTaskbar = !m_startMinimized;

			SizeChanged += (s, e) =>
			{
				if (WindowState == FormWindowState.Minimized) HideToTray();
			};

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
				using (var dmw = new DeviceMonitorWindow())
				{
					ShowDialogWindow(dmw);
				}
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

		private void InitializeTray()
		{
			TrayNotifyIcon.Icon = Icon;
			TrayNotifyIcon.DoubleClick += (s, e) => ShowFromTray();
			ShowTrayMenuItem.Click += (s, e) => ShowFromTray();
			ExitTrayMenuItem.Click += (s, e) => Application.Exit();
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
