using System;
using System.Globalization;
using JetBrains.Annotations;
using NFirmwareEditor.Managers;
using NFirmwareEditor.Models;
using NLog;

namespace NFirmwareEditor.Windows
{
	internal partial class DeviceConfiguratorWindow : EditorDialogWindow
	{
		private static readonly ILogger s_logger = LogManager.GetCurrentClassLogger();

		private readonly FirmwareUpdater m_updater = new FirmwareUpdater();
		private readonly DataflashManager m_manager = new DataflashManager();
		private SimpleDataflash m_simple;
		private Dataflash m_dataflash;

		public DeviceConfiguratorWindow()
		{
			InitializeComponent();
			Initialize();
		}

		private void Initialize()
		{
			m_updater.DeviceConnected += DeviceConnected;
			m_updater.StartMonitoring();

			MainContainer.SelectedPageChanged += MainContainer_SelectedPageChanged;
		}

		private void InitializeWorkspace([NotNull] Dataflash dataflash)
		{
			if (dataflash == null) throw new ArgumentNullException("dataflash");

			DeviceNameLabel.Text = FirmwareUpdater.GetDeviceInfo(dataflash.Info.ProductID).Name;
			FirmwareVersionTextBox.Text = (dataflash.Info.FWVersion / 100f).ToString("0.00", CultureInfo.InvariantCulture);
			HardwareVersionTextBox.Text = (dataflash.Params.HardwareVersion / 100f).ToString("0.00", CultureInfo.InvariantCulture);
			BootModeTextBox.Text = dataflash.Params.BootMode.ToString();

			PowerTextBox.Text = (dataflash.Params.Power / 10f).ToString(CultureInfo.InvariantCulture);
			TCPowerTextBox.Text = (dataflash.Params.TCPower / 10f).ToString(CultureInfo.InvariantCulture);
			TemperatureTextBox.Text = dataflash.Params.Temperature.ToString(CultureInfo.InvariantCulture);
			TemperatureComboBox.SelectedIndex = dataflash.Params.IsCelsius ? 0 : 1;

			ResistanceNiTextBox.Text = (dataflash.Params.ResistanceNi / 100f).ToString("0.00", CultureInfo.InvariantCulture);
			ResistanceNiCheckBox.Checked = dataflash.Params.ResistanceNiLocked;

			ResistanceTiTextBox.Text = (dataflash.Params.ResistanceTi / 100f).ToString("0.00", CultureInfo.InvariantCulture);
			ResistanceTiCheckBox.Checked = dataflash.Params.ResistanceTiLocked;

			ResistanceSSTextBox.Text = (dataflash.Params.ResistanceSS / 100f).ToString("0.00", CultureInfo.InvariantCulture);
			ResistanceSSCheckBox.Checked = dataflash.Params.ResistanceSSLocked;

			ResistanceTCRTextBox.Text = (dataflash.Params.ResistanceTCR / 100f).ToString("0.00", CultureInfo.InvariantCulture);
			ResistanceTCRCheckBox.Checked = dataflash.Params.ResistanceTCRLocked;
		}

		private void DeviceConnected(bool isConnected)
		{
			if (!isConnected)
			{
				UpdateUI(() =>
				{
					WelcomeLabel.Text = @"Waiting for device...";
					MainContainer.SelectedPage = WelcomePage;
					m_simple = null;
				});
				return;
			}

			UpdateUI(() => WelcomeLabel.Text = @"Downloading settings...");
			try
			{
				m_simple = m_updater.ReadDataflash();
				m_dataflash = m_manager.Read(m_simple.Data);

				UpdateUI(() => MainContainer.SelectedPage = WorkspacePage);
			}
			catch (Exception ex)
			{
				s_logger.Warn(ex);
				UpdateUI(() => WelcomeLabel.Text = @"Unable to download device settings. Reconnect your device.");
			}
		}

		// ReSharper disable once InconsistentNaming
		private void UpdateUI(Action action)
		{
			try
			{
				Invoke(action);
			}
			catch (Exception)
			{
				// Ignore
			}
		}

		private void MainContainer_SelectedPageChanged(object sender, EventArgs e)
		{
			if (MainContainer.SelectedPage == WorkspacePage)
			{
				InitializeWorkspace(m_dataflash);
			}
		}
	}
}
