using System.Threading;
using System.Windows.Forms;
using NCore;
using NCore.UI;
using NToolbox.Models;

namespace NToolbox.Windows
{
	internal partial class MainWindow : WindowBase
	{
		private readonly StartupMode m_startupMode;

		public MainWindow(StartupMode startupMode)
		{
			m_startupMode = startupMode;

			InitializeComponent();
			Initialize();
			InitializeTray();
		}

		private void Initialize()
		{
			if (m_startupMode == StartupMode.Minimized)
			{
				Opacity = 0;
				ShowInTaskbar = false;
			}

			Load += (s, e) =>
			{
				switch (m_startupMode)
				{
					case StartupMode.ArcticFoxConfiguration:
						ArcticFoxConfigurationButton.PerformClick();
						break;
					case StartupMode.DeviceMonitor:
						DeviceMonitorButton.PerformClick();
						break;
					case StartupMode.FirmwareUpdater:
						FirmwareUpdaterButton.PerformClick();
						break;
				}
			};

			AutorunCheckBox.Checked = GetAutorunState();
			AutorunCheckBox.CheckedChanged += (s, e) => SetAutorunState(AutorunCheckBox.Checked);

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
			TrayNotifyIcon.MouseDoubleClick += (s, e) =>
			{
				if (e.Button != MouseButtons.Left) return;
				ShowFromTray();
			};
			ShowTrayMenuItem.Click += (s, e) => ShowFromTray();
			ExitTrayMenuItem.Click += (s, e) => Application.Exit();
		}

		private DialogResult ShowDialogWindow(Form window)
		{
			DialogResult result;
			IgnoreFirstInstanceMessages = true;
			{
				Hide();
				result = window.ShowDialog();
				Thread.Sleep(150);
				Show();
				IgnoreFirstInstanceMessages = false;
			}
			return result;
		}

		private bool GetAutorunState()
		{
			return ApplicationService.GetAutorunState(StartupArgs.Minimzed);
		}

		private bool SetAutorunState(bool enabled)
		{
			return ApplicationService.UpdateAutorunState(enabled, StartupArgs.Minimzed);
		}
	}
}
