using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using NCore;
using NCore.UI;
using NCore.USB;
using NToolbox.Models;
using NToolbox.Storages;

namespace NToolbox.Windows
{
	internal partial class MainWindow : WindowBase
	{
		private const string SettingsFileName = "NToolboxConfiguration.xml";
		private readonly ConfigurationStorage m_configurationStorage = new ConfigurationStorage();
		private readonly StartupMode m_startupMode;

		private ToolboxConfiguration m_configuration;
		private WindowBase m_openedWindow;

		public MainWindow(StartupMode startupMode)
		{
			m_startupMode = startupMode;

			InitializeComponent();
			Initialize();
			InitializeControls();
			InitializeTray();
		}

		private void Initialize()
		{
			m_configuration = LoadConfiguration();
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

			SizeChanged += (s, e) =>
			{
				if (WindowState == FormWindowState.Minimized) HideToTray();
			};

			HidConnector.Instance.DeviceConnected += DeviceConnected;
		}

		private void InitializeControls()
		{
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
				using (var dmw = new DeviceMonitorWindow(m_configuration))
				{
					ShowDialogWindow(dmw);
				}
				SaveConfiguration();
			};

			ScreenshooterButton.Click += (s, e) =>
			{
				using (var ss = new ScreenshooterWindow(m_configuration))
				{
					ShowDialogWindow(ss);
				}
				SaveConfiguration();
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

			OpenArcticFoxConfigurationTrayMenuItem.Checked = m_configuration.OpenArcticFoxConfigurationWhenDeviceIsConnected;
			OpenArcticFoxConfigurationTrayMenuItem.CheckedChanged += (s, e) =>
			{
				m_configuration.OpenArcticFoxConfigurationWhenDeviceIsConnected = OpenArcticFoxConfigurationTrayMenuItem.Checked;
				SaveConfiguration();
			};
			TimeSyncTrayMenuItem.Checked = m_configuration.SynchronizeTimeWhenDeviceIsConnected;
			TimeSyncTrayMenuItem.CheckedChanged += (s, e) =>
			{
				m_configuration.SynchronizeTimeWhenDeviceIsConnected = TimeSyncTrayMenuItem.Checked;
				SaveConfiguration();
			};

			AutorunTrayMenuItem.Checked = GetAutorunState();
			AutorunTrayMenuItem.CheckedChanged += (s, e) => SetAutorunState(AutorunTrayMenuItem.Checked);
		}

		private ToolboxConfiguration LoadConfiguration()
		{
			return m_configurationStorage.Load(Path.Combine(ApplicationService.ApplicationDirectory, SettingsFileName));
		}

		private void SaveConfiguration()
		{
			m_configurationStorage.Save(Path.Combine(ApplicationService.ApplicationDirectory, SettingsFileName), m_configuration);
		}

		private void ShowDialogWindow(WindowBase window)
		{
			IgnoreFirstInstanceMessages = true;
			{
				m_openedWindow = window;
				Hide();
				window.ShowDialog();
				Thread.Sleep(150);
				m_openedWindow = null;
				Show();
			}
			IgnoreFirstInstanceMessages = false;
		}

		private bool GetAutorunState()
		{
			return ApplicationService.GetAutorunState(StartupArgs.Minimzed);
		}

		private bool SetAutorunState(bool enabled)
		{
			return ApplicationService.UpdateAutorunState(enabled, StartupArgs.Minimzed);
		}

		private void DeviceConnected(bool isConnected)
		{
			try
			{
				if (HidConnector.Instance.IsDeviceConnected && m_configuration.SynchronizeTimeWhenDeviceIsConnected)
				{
					var now = DateTime.Now;
					var dateTime = new ArcticFoxConfiguration.DateTime
					{
						Year = (ushort)now.Year,
						Month = (byte)now.Month,
						Day = (byte)now.Day,
						Hour = (byte)now.Hour,
						Minute = (byte)now.Minute,
						Second = (byte)now.Second
					};
					var data = BinaryStructure.Write(dateTime);
					HidConnector.Instance.SetDateTime(data);
				}

				if (HidConnector.Instance.IsDeviceConnected && m_configuration.OpenArcticFoxConfigurationWhenDeviceIsConnected)
				{
					UpdateUI(() =>
					{
						var window = m_openedWindow;
						if (window == null)
						{
							ArcticFoxConfigurationButton.PerformClick();
						}
						else if (window.GetType() == typeof(ArcticFoxConfigurationWindow))
						{
							NativeMethods.SetForegroundWindow(m_openedWindow.Handle);
						}
					});
				}
			}
			catch (Exception)
			{
				// Ignore
			}
		}
	}
}
