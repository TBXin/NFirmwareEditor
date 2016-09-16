using System;
using System.Globalization;
using System.Linq;
using JetBrains.Annotations;
using NFirmwareEditor.Managers;
using NFirmwareEditor.Models;
using NFirmwareEditor.UI;
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
			InitializeControls();
			Initialize();
		}

		private void InitializeControls()
		{
			CurrentModeComboBox.Items.Clear();
			CurrentModeComboBox.Items.AddRange(new object[]
			{
				new NamedItemContainer<Mode>("Temperature Ni", Mode.TempNi),
				new NamedItemContainer<Mode>("Temperature Ti", Mode.TempTi),
				new NamedItemContainer<Mode>("Temperature SS", Mode.TempSS),
				new NamedItemContainer<Mode>("Temperature TCR", Mode.TCR),
				new NamedItemContainer<Mode>("Power", Mode.Power),
				new NamedItemContainer<Mode>("Bypass", Mode.Bypass),
				new NamedItemContainer<Mode>("Smart / Start", Mode.Start)
			});

			BrightnessTrackBar.ValueChanged += (s, e) => BrightnessPercentLabel.Text = BrightnessTrackBar.Value + @"%";
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

			// Power & Resistance Tab
			PowerTextBox.Text = (dataflash.Params.Power / 10f).ToString(CultureInfo.InvariantCulture);
			TCPowerTextBox.Text = (dataflash.Params.TCPower / 10f).ToString(CultureInfo.InvariantCulture);
			Step1WCheckBox.Checked = dataflash.Params.Status.Step1W;

			TemperatureTextBox.Text = dataflash.Params.Temperature.ToString(CultureInfo.InvariantCulture);
			TemperatureComboBox.SelectedIndex = dataflash.Params.IsCelsius ? 0 : 1;
			TemperatureDominantCheckBox.Checked = dataflash.Params.Status.TemperatureDominant;

			// Coils Manager Tab
			ResistanceNiTextBox.Text = (dataflash.Params.ResistanceNi / 100f).ToString("0.00", CultureInfo.InvariantCulture);
			ResistanceNiCheckBox.Checked = dataflash.Params.ResistanceNiLocked;

			ResistanceTiTextBox.Text = (dataflash.Params.ResistanceTi / 100f).ToString("0.00", CultureInfo.InvariantCulture);
			ResistanceTiCheckBox.Checked = dataflash.Params.ResistanceTiLocked;

			ResistanceSSTextBox.Text = (dataflash.Params.ResistanceSS / 100f).ToString("0.00", CultureInfo.InvariantCulture);
			ResistanceSSCheckBox.Checked = dataflash.Params.ResistanceSSLocked;

			ResistanceTCRTextBox.Text = (dataflash.Params.ResistanceTCR / 100f).ToString("0.00", CultureInfo.InvariantCulture);
			ResistanceTCRCheckBox.Checked = dataflash.Params.ResistanceTCRLocked;

			if (dataflash.Params.Status.PreheatPercent)
			{
				PreheatPowerTextBox.Text = dataflash.Params.PreheatPwr.ToString(CultureInfo.InvariantCulture);
				PreheatTypeComboBox.SelectedIndex = 1;
			}
			else
			{
				PreheatPowerTextBox.Text = (dataflash.Params.PreheatPwr / 10f).ToString("0.0", CultureInfo.InvariantCulture);
				PreheatTypeComboBox.SelectedIndex = 0;
			}
			
			PreheatTimeTextBox.Text = (dataflash.Params.PreheatTime / 100f).ToString("0.0", CultureInfo.InvariantCulture);

			// TCR Tab
			M1TextBox.Text = dataflash.Params.TCR[0].ToString();
			M2TextBox.Text = dataflash.Params.TCR[1].ToString();
			M3TextBox.Text = dataflash.Params.TCR[2].ToString();

			// Modes Tab
			CurrentModeComboBox.SelectedItem = CurrentModeComboBox.Items.Cast<NamedItemContainer<Mode>>().First(x => x.Data == dataflash.Params.Mode);

			TCRIndexLabel.Visible = dataflash.Params.Mode == Mode.TCR;
			SelectedTCRComboBox.Visible = dataflash.Params.Mode == Mode.TCR;
			SelectedTCRComboBox.SelectedIndex = dataflash.Params.TCRIndex;

			TempNiModeCheckBox.Checked = !dataflash.Params.DisabledModes.HasFlag(Modes.TempNi);
			TempTiModeCheckBox.Checked = !dataflash.Params.DisabledModes.HasFlag(Modes.TempTi);
			TempSSModeCheckBox.Checked = !dataflash.Params.DisabledModes.HasFlag(Modes.TempSS);
			TCRModeCheckBox.Checked = !dataflash.Params.DisabledModes.HasFlag(Modes.TCR);
			PowerModeCheckBox.Checked = !dataflash.Params.DisabledModes.HasFlag(Modes.Power);
			BypassModeCheckBox.Checked = !dataflash.Params.DisabledModes.HasFlag(Modes.Bypass);
			SmartModeCheckBox.Checked = !dataflash.Params.DisabledModes.HasFlag(Modes.Start);

			// Screen Tab
			BrightnessTrackBar.Value = (int)(dataflash.Params.Contrast * 100f / 255);
			IdleTimeTextBox.Text = dataflash.Params.ScreenDimTimeout.ToString(CultureInfo.InvariantCulture);
			StealthModeCheckBox.Checked = dataflash.Params.StealthOn;
			FlippedModeCheckBox.Checked = dataflash.Params.Status.Flipped;
			BatteryPercentsCheckBox.Checked = dataflash.Params.Status.BatteryPercent;
			ShowLogoCheckBox.Checked = !dataflash.Params.Status.NoLogo;
			if (!dataflash.Params.Status.AnalogClock && !dataflash.Params.Status.DigitalClock)
			{
				ClockModeComboBox.SelectedIndex = 0;
			}
			else if (dataflash.Params.Status.AnalogClock)
			{
				ClockModeComboBox.SelectedIndex = 1;
			}
			else if (dataflash.Params.Status.DigitalClock)
			{
				ClockModeComboBox.SelectedIndex = 2;
			}
			WakeUpByPlusMinusCheckBox.Checked = dataflash.Params.Status.WakeUpByPlusMinus;
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
