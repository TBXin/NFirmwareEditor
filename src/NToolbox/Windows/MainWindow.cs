using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using NCore;
using NCore.UI;
using NCore.USB;
using NToolbox.Models;
using NToolbox.Services;
using NToolbox.Storages;

namespace NToolbox.Windows
{
	internal partial class MainWindow : WindowBase
	{
		private const string ApplicationVersion = "1.7";
		private const string SettingsFileName = "NToolboxConfiguration.xml";
		private readonly ContextMenuStrip m_languageContextMenu = new ContextMenuStrip();
		private readonly ConfigurationStorage m_configurationStorage = new ConfigurationStorage();
		private readonly StartupMode m_startupMode;
		private readonly string m_firmwareFile;

		private ToolboxConfiguration m_configuration;
		private WindowBase m_openedWindow;
		private bool m_hideToTray;

		public MainWindow(StartupMode startupMode, string[] args)
		{
			m_startupMode = startupMode;
			m_firmwareFile = args != null && args.Length > 0 ? args[0] : null;
			m_hideToTray = m_startupMode.HasFlag(StartupMode.Minimized) || GetAutorunState();

			InitializeComponent();
			Initialize();
			InitializeControls();
			InitializeTray();
			InitializeLanguages();
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
			Closing += (s, e) =>
			{
				if (m_hideToTray)
				{
					e.Cancel = true;
					WindowState = FormWindowState.Minimized;
				}
			};
			SizeChanged += (s, e) =>
			{
				if (WindowState == FormWindowState.Minimized)
				{
					HideToTray();
				}
			};
			HidConnector.Instance.DeviceConnected += DeviceConnected;
		}

		private void InitializeControls()
		{
			VersionLabel.Text = @"v" + ApplicationVersion;
			ArcticFoxConfigurationButton.Click += StartArcticFoxConfiguration;
			DeviceMonitorButton.Click += StartDeviceMonitor;
			ScreenshooterButton.Click += StartScreenshooter;
			FirmwareUpdaterButton.Click += StartFirmwareUpdater;

			AboutLinkLabel.Click += (s, e) =>
			{
				#if DEBUG
				if (System.Diagnostics.Debugger.IsAttached)
				{
					// Initialize all localizable strings.
					typeof(LocalizableStrings).GetProperties().ForEach(p => p.GetValue(null, null));
					// Create exportable lpack.txt
					var result = LocalizationManager.Instance.GetLanguagePack();
					System.Diagnostics.Debugger.Break();
				}
				#endif

				using (var aboutWindow = new AboutWindow())
				{
					aboutWindow.ShowDialog();
				}
			};
		}

		private void InitializeTray()
		{
			TrayNotifyIcon.Text = @"NToolbox v" + ApplicationVersion + @" © ReikoKitsune";
			TrayNotifyIcon.Icon = Icon;
			TrayNotifyIcon.MouseDoubleClick += (s, e) =>
			{
				if (e.Button != MouseButtons.Left) return;
				ShowFromTray();
			};
			ShowTrayMenuItem.Click += (s, e) => ShowFromTray();
			ExitTrayMenuItem.Click += (s, e) => Application.Exit();

			ArcticFoxConfigurationTrayMenuItem.Click += StartArcticFoxConfiguration;
			DeviceMonitorTrayMenuItem.Click += StartDeviceMonitor;
			ScreenshooterTrayMenuItem.Click += StartScreenshooter;
			FirmwareUpdaterTrayMenuItem.Click += StartFirmwareUpdater;

			OpenArcticFoxConfigurationTrayMenuItem.Checked = m_configuration.OpenArcticFoxConfigurationWhenDeviceIsConnected;
			OpenArcticFoxConfigurationTrayMenuItem.CheckedChanged += (s, e) =>
			{
				m_configuration.OpenArcticFoxConfigurationWhenDeviceIsConnected = OpenArcticFoxConfigurationTrayMenuItem.Checked;
				SaveConfiguration();
			};
			CloseArcticFoxConfigurationTrayMenuItem.Checked = m_configuration.CloseArcticFoxConfigurationWhenDeviceIsDisconnected;
			CloseArcticFoxConfigurationTrayMenuItem.CheckedChanged += (s, e) =>
			{
				m_configuration.CloseArcticFoxConfigurationWhenDeviceIsDisconnected = CloseArcticFoxConfigurationTrayMenuItem.Checked;
				SaveConfiguration();
			};
			TimeSyncTrayMenuItem.Checked = m_configuration.SynchronizeTimeWhenDeviceIsConnected;
			TimeSyncTrayMenuItem.CheckedChanged += (s, e) =>
			{
				m_configuration.SynchronizeTimeWhenDeviceIsConnected = TimeSyncTrayMenuItem.Checked;
				SaveConfiguration();
			};

			AutorunTrayMenuItem.Checked = GetAutorunState();
			AutorunTrayMenuItem.CheckedChanged += (s, e) =>
			{
				m_hideToTray = AutorunTrayMenuItem.Checked;
				SetAutorunState(AutorunTrayMenuItem.Checked);
			};
		}

		private void InitializeLanguages()
		{
			var languages = LocalizationManager.GetAvailableLanguages();
			m_languageContextMenu.SuspendLayout();
			foreach (var item in languages)
			{
				var lang = item;
				var menuItem = new ToolStripMenuItem(lang.DisplayName, lang.Flag, (s, e) =>
				{
					foreach (ToolStripMenuItem child in m_languageContextMenu.Items)
					{
						child.Checked = false;
					}

					LocalizationManager.Instance.InitializeLanguagePack(lang.FilePath);
					LocalizeSelf();

					m_configuration.Language = lang.DisplayName;
					SaveConfiguration();

					((ToolStripMenuItem)s).Checked = true;
					LanguageButton.Image = lang.Flag;
					LanguageButton.Text = lang.DisplayName + @" ▾ ";
				});
				m_languageContextMenu.Items.Add(menuItem);
			}
			m_languageContextMenu.ResumeLayout(false);
			LanguageButton.Click += (s, e) =>
			{
				ArcticFoxConfigurationButton.Focus();
				m_languageContextMenu.Show(LanguageButton, new Point(0, LanguageButton.Height + 1));
				m_languageContextMenu.Focus();
			};

			if (string.IsNullOrEmpty(m_configuration.Language) && m_languageContextMenu.Items.Count > 0)
			{
				m_languageContextMenu.Items[0].PerformClick();
			}
			else
			{
				var activeLanguage = languages.FindIndex(x => string.Equals(x.DisplayName, m_configuration.Language, StringComparison.OrdinalIgnoreCase));
				if (activeLanguage != -1) m_languageContextMenu.Items[activeLanguage].PerformClick();
			}
		}

		private ToolboxConfiguration LoadConfiguration()
		{
			return m_configurationStorage.Load(Path.Combine(ApplicationService.ApplicationDirectory, SettingsFileName));
		}

		private void SaveConfiguration()
		{
			m_configurationStorage.Save(Path.Combine(ApplicationService.ApplicationDirectory, SettingsFileName), m_configuration);
		}

		private void StartArcticFoxConfiguration(object sender, EventArgs e)
		{
			using (var cfg = new ArcticFoxConfigurationWindow(m_configuration))
			{
				ShowDialogWindow(cfg);
			}
		}

		private void StartDeviceMonitor(object sender, EventArgs e)
		{
			using (var deviceMonitorWindow = new DeviceMonitorWindow(m_configuration))
			{
				ShowDialogWindow(deviceMonitorWindow);
			}
			SaveConfiguration();
		}

		private void StartScreenshooter(object sender, EventArgs e)
		{
			using (var screenshooterWindow = new ScreenshooterWindow(m_configuration))
			{
				ShowDialogWindow(screenshooterWindow);
			}
			SaveConfiguration();
		}

		private void StartFirmwareUpdater(object sender, EventArgs e)
		{
			using (var sync = new CrossApplicationSynchronizer(CrossApplicationIndentifiers.FirmwareUpdater))
			{
				if (!sync.IsLockObtained)
				{
					InfoBox.Show("\"NFirmwareEditor - Firmware Updater\" is already running.\n\nTo continue you need to close it first.");
					return;
				}

				using (var updaterWindow = new FirmwareUpdaterWindow(m_firmwareFile))
				{
					ShowDialogWindow(updaterWindow);
				}
			}
		}

		private void ShowDialogWindow(WindowBase window)
		{
			IgnoreFirstInstanceMessages = true;
			SetTrayItemsState(false);
			{
				m_openedWindow = window;
				Hide();
				window.ShowDialog();

				if (m_startupMode == StartupMode.ArcticFoxConfiguration ||
				    m_startupMode == StartupMode.DeviceMonitor ||
				    m_startupMode == StartupMode.FirmwareUpdater ||
				    m_startupMode == StartupMode.MyEvicConfiguration)
				{
					Opacity = 0;
					Application.Exit();
					return;
				}

				Thread.Sleep(150);
				m_openedWindow = null;
				Show();
			}
			SetTrayItemsState(true);
			IgnoreFirstInstanceMessages = false;
		}

		private void SetTrayItemsState(bool enable)
		{
			ArcticFoxConfigurationTrayMenuItem.Enabled =
			//MyEvicConfigurationTrayMenuItem.Enabled = 
			DeviceMonitorTrayMenuItem.Enabled = 
			ScreenshooterTrayMenuItem.Enabled = 
			FirmwareUpdaterTrayMenuItem.Enabled = enable;
		}

		private bool GetAutorunState()
		{
			return ApplicationService.GetAutorunState(StartupArgs.Minimzed);
		}

		private bool SetAutorunState(bool enabled)
		{
			return ApplicationService.UpdateAutorunState(enabled, StartupArgs.Minimzed);
		}

		#region Overrides of WindowBase
		protected override void OnLocalization()
		{
			ShowTrayMenuItem.Text = LocalizableStrings.TrayShowToolbox;
			ArcticFoxConfigurationTrayMenuItem.Text = LocalizableStrings.TrayArcticFoxConfiguration;
			DeviceMonitorTrayMenuItem.Text = LocalizableStrings.TrayDeviceMonitor;
			ScreenshooterTrayMenuItem.Text = LocalizableStrings.TrayScreenshooter;
			FirmwareUpdaterTrayMenuItem.Text = LocalizableStrings.TrayFirmwareUpdater;
			OpenArcticFoxConfigurationTrayMenuItem.Text = LocalizableStrings.TrayArcticFoxConfigurationAutostart;
			CloseArcticFoxConfigurationTrayMenuItem.Text = LocalizableStrings.TrayArcticFoxConfigurationAutoClose;
			AutorunTrayMenuItem.Text = LocalizableStrings.TrayToolboxAutostart;
			TimeSyncTrayMenuItem.Text = LocalizableStrings.TrayTimeSync;
			ExitTrayMenuItem.Text = LocalizableStrings.TrayExit;
		}
		#endregion

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (keyData == Keys.Escape)
			{
				WindowState = FormWindowState.Minimized;
			}
			return base.ProcessCmdKey(ref msg, keyData);
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
					var data = BinaryStructure.WriteBinary(dateTime);
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
