using System;
using System.Globalization;
using System.Windows.Forms;
using JetBrains.Annotations;
using NFirmwareEditor.Core;
using NFirmwareEditor.Managers;
using NFirmwareEditor.Models;
using NFirmwareEditor.Properties;
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
		private bool m_isDeviceWasConnectedOnce;
		private bool m_isDeviceConnected;

		public DeviceConfiguratorWindow()
		{
			InitializeComponent();
			InitializeControls();
			Initialize();
		}

		private void InitializeControls()
		{
			var errorIcon = BitmapProcessor.CreateIcon(Resources.exclamation);
			if (errorIcon != null) MainErrorProvider.Icon = errorIcon;

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

			TemperatureComboBox.Items.Clear();
			TemperatureComboBox.Items.AddRange(new object[]
			{
			    new NamedItemContainer<bool>("°F", false),
			    new NamedItemContainer<bool>("°C", true)
			});
			TemperatureComboBox.SelectedValueChanged += (s, e) =>
			{
				var isCelcius = TemperatureComboBox.GetSelectedItem<bool>();
				if (isCelcius)
				{
					TemperatureUpDown.Minimum = 100;
					TemperatureUpDown.Maximum = 315;
				}
				else
				{
					TemperatureUpDown.Minimum = 200;
					TemperatureUpDown.Maximum = 600;
				}
			};

			PreheatTypeComboBox.Items.Clear();
			PreheatTypeComboBox.Items.AddRange(new object[]
			{
				new NamedItemContainer<bool>("%", true),
				new NamedItemContainer<bool>("W", false)
			});
			PreheatTypeComboBox.SelectedValueChanged += (s, e) =>
			{
				var isPercents = PreheatTypeComboBox.GetSelectedItem<bool>();
				if (isPercents)
				{
					PreheatPowerUpDown.Minimum = 100;
					PreheatPowerUpDown.Maximum = 250;
				}
				else
				{
					PreheatPowerUpDown.Minimum = 1;
					PreheatPowerUpDown.Maximum = 75;
				}
			};

			CurrentModeComboBox.SelectedValueChanged += (s, e) =>
			{
				var mode = CurrentModeComboBox.GetSelectedItem<Mode>();
				if (mode == Mode.TCR)
				{
					SelectedTCRComboBox.Visible = TCRIndexLabel.Visible = true;
					SelectedTCRComboBox.SelectedIndex = m_dataflash.Params.TCRIndex;
				}
				else
				{
					SelectedTCRComboBox.Visible = TCRIndexLabel.Visible = false;
				}
			};

			ClockModeComboBox.Items.Clear();
			ClockModeComboBox.Items.AddRange(new object[]
			{
				new NamedItemContainer<ClockMode>("Disabled", ClockMode.Disabled),
				new NamedItemContainer<ClockMode>("Analog", ClockMode.Analog),
				new NamedItemContainer<ClockMode>("Digital", ClockMode.Digital)
			});

			BrightnessTrackBar.ValueChanged += (s, e) => BrightnessPercentLabel.Text = BrightnessTrackBar.Value + @"%";

			DownloadButton.Click += DownloadButton_Click;
			UploadButton.Click += UploadButton_Click;
			ResetButton.Click += ResetButton_Click;

			//PowerTextBox.Validating += (s, e) => TextBoxValidator(s, e, PowerValidator);
		}

		/*private string PowerValidator(string text)
		{
			int value;
			if (!int.TryParse(text, out value)) return "Provide correct integer value.";
			if (value > 75) return "Provide correct value: from 0 to 75.";
			return null;
		}

		private void TextBoxValidator(object sender, CancelEventArgs args, Func<string, string> validator)
		{
			var textBox = sender as TextBox;
			if (textBox == null) throw new InvalidOperationException("Invalid TextBoxValidator usage.");

			var errorMessage = validator(textBox.Text);
			if (string.IsNullOrEmpty(errorMessage))
			{
				MainErrorProvider.Clear();
			}
			else
			{
				MainErrorProvider.SetError(textBox, errorMessage);
				args.Cancel = true;
			}
		}*/

		private void Initialize()
		{
			m_updater.DeviceConnected += DeviceConnected;
			m_updater.StartMonitoring();

			MainContainer.SelectedPageChanged += MainContainer_SelectedPageChanged;
		}

		private void InitializeWorkspaceFromDataflash([NotNull] Dataflash dataflash)
		{
			if (dataflash == null) throw new ArgumentNullException("dataflash");

			DeviceNameLabel.Text = FirmwareUpdater.GetDeviceInfo(dataflash.Info.ProductID).Name;
			FirmwareVersionTextBox.Text = (dataflash.Info.FWVersion / 100f).ToString("0.00", CultureInfo.InvariantCulture);
			HardwareVersionTextBox.Text = (dataflash.Params.HardwareVersion / 100f).ToString("0.00", CultureInfo.InvariantCulture);
			BootModeTextBox.Text = dataflash.Params.BootMode.ToString();

			// General -> Power & Temp Tab
			PowerUpDown.Value = Math.Min(dataflash.Params.Power / 10, 75);
			TCPowerUpDown.Value = Math.Min(dataflash.Params.TCPower / 10, 75);
			Step1WCheckBox.Checked = dataflash.Params.Status.Step1W;

			TemperatureComboBox.SelectItem(dataflash.Params.IsCelsius);
			TemperatureUpDown.Value = dataflash.Params.Temperature;
			TemperatureDominantCheckBox.Checked = dataflash.Params.Status.TemperatureDominant;

			PreheatTypeComboBox.SelectItem(dataflash.Params.Status.PreheatPercent);
			PreheatPowerUpDown.Value = dataflash.Params.Status.PreheatPercent ? dataflash.Params.PreheatPwr : Math.Min(dataflash.Params.PreheatPwr / 10m, 75);
			PreheatTimeUpDown.Value = dataflash.Params.PreheatTime / 100m;

			// General -> Coils Manager Tab
			ResistanceNiUpDown.Value = dataflash.Params.ResistanceNi / 100m;
			ResistanceNiCheckBox.Checked = dataflash.Params.ResistanceNiLocked;

			ResistanceTiUpDown.Value = dataflash.Params.ResistanceTi / 100m;
			ResistanceTiCheckBox.Checked = dataflash.Params.ResistanceTiLocked;

			resistanceSSUpDown.Value = dataflash.Params.ResistanceSS / 100m;
			ResistanceSSCheckBox.Checked = dataflash.Params.ResistanceSSLocked;

			ResistanceTCRUpDown.Value = dataflash.Params.ResistanceTCR / 100m;
			ResistanceTCRCheckBox.Checked = dataflash.Params.ResistanceTCRLocked;

			TCRM1UpDown.Value = dataflash.Params.TCR[0];
			TCRM2UpDown.Value = dataflash.Params.TCR[1];
			TCRM3UpDown.Value = dataflash.Params.TCR[2];

			// General -> Modes Tab
			CurrentModeComboBox.SelectItem(dataflash.Params.Mode);
			TempNiModeCheckBox.Checked = !dataflash.Params.DisabledModes.HasFlag(Modes.TempNi);
			TempTiModeCheckBox.Checked = !dataflash.Params.DisabledModes.HasFlag(Modes.TempTi);
			TempSSModeCheckBox.Checked = !dataflash.Params.DisabledModes.HasFlag(Modes.TempSS);
			TCRModeCheckBox.Checked = !dataflash.Params.DisabledModes.HasFlag(Modes.TCR);
			PowerModeCheckBox.Checked = !dataflash.Params.DisabledModes.HasFlag(Modes.Power);
			BypassModeCheckBox.Checked = !dataflash.Params.DisabledModes.HasFlag(Modes.Bypass);
			SmartModeCheckBox.Checked = !dataflash.Params.DisabledModes.HasFlag(Modes.Start);

			// General -> Controls Tab
			WakeUpByPlusMinusCheckBox.Checked = dataflash.Params.Status.WakeUpByPlusMinus;

			// Screen -> Display Tab
			BrightnessTrackBar.Value = (int)(dataflash.Params.Contrast * 100f / 255);
			IdleTimeUpDow.Value = dataflash.Params.ScreenDimTimeout;
			StealthModeCheckBox.Checked = dataflash.Params.StealthOn;
			FlippedModeCheckBox.Checked = dataflash.Params.Status.Flipped;

			// Screen -> Layout Tab
			BatteryPercentsCheckBox.Checked = dataflash.Params.Status.BatteryPercent;
			ShowLogoCheckBox.Checked = !dataflash.Params.Status.NoLogo;

			// Screen -> Screensaver Tab
			if (!dataflash.Params.Status.AnalogClock && !dataflash.Params.Status.DigitalClock)
			{
				ClockModeComboBox.SelectItem(ClockMode.Disabled);
			}
			else if (dataflash.Params.Status.AnalogClock)
			{
				ClockModeComboBox.SelectItem(ClockMode.Analog);
			}
			else if (dataflash.Params.Status.DigitalClock)
			{
				ClockModeComboBox.SelectItem(ClockMode.Digital);
			}
		}

		private void SaveWorkspaceToDataflash([NotNull] Dataflash dataflash)
		{
			if (dataflash == null) throw new ArgumentNullException("dataflash");

			// General -> Power & Temp Tab
			dataflash.Params.Power = (ushort)(PowerUpDown.Value * 10);
			dataflash.Params.TCPower = (ushort)(TCPowerUpDown.Value * 10);
			dataflash.Params.Status.Step1W = Step1WCheckBox.Checked;

			dataflash.Params.IsCelsius = TemperatureComboBox.GetSelectedItem<bool>();
			dataflash.Params.Temperature = (ushort)TemperatureUpDown.Value;
			dataflash.Params.Status.TemperatureDominant = TemperatureDominantCheckBox.Checked;

			dataflash.Params.Status.PreheatPercent = PreheatTypeComboBox.GetSelectedItem<bool>();
			dataflash.Params.PreheatPwr = (ushort)PreheatPowerUpDown.Value;
			dataflash.Params.PreheatTime = (byte)(PreheatTimeUpDown.Value * 100);

			// General -> Coils Manager Tab
			dataflash.Params.ResistanceNi = (ushort)(ResistanceNiUpDown.Value * 100);
			dataflash.Params.ResistanceNiLocked = ResistanceNiCheckBox.Checked;

			dataflash.Params.ResistanceTi = (ushort)(ResistanceTiUpDown.Value * 100);
			dataflash.Params.ResistanceTiLocked = ResistanceTiCheckBox.Checked;

			dataflash.Params.ResistanceSS = (ushort)(resistanceSSUpDown.Value * 100);
			dataflash.Params.ResistanceSSLocked = ResistanceSSCheckBox.Checked;

			dataflash.Params.ResistanceTCR = (ushort)(ResistanceTCRUpDown.Value * 100);
			dataflash.Params.ResistanceTCRLocked = ResistanceTCRCheckBox.Checked;

			dataflash.Params.TCR[0] = (ushort)TCRM1UpDown.Value;
			dataflash.Params.TCR[1] = (ushort)TCRM2UpDown.Value;
			dataflash.Params.TCR[2] = (ushort)TCRM3UpDown.Value;

			// General -> Modes Tab
			dataflash.Params.Mode = CurrentModeComboBox.GetSelectedItem<Mode>();
			dataflash.Params.DisabledModes = Modes.None;
			{
				if (!TempNiModeCheckBox.Checked) dataflash.Params.DisabledModes |= Modes.TempNi;
				if (!TempTiModeCheckBox.Checked) dataflash.Params.DisabledModes |= Modes.TempTi;
				if (!TempSSModeCheckBox.Checked) dataflash.Params.DisabledModes |= Modes.TempSS;
				if (!TCRModeCheckBox.Checked) dataflash.Params.DisabledModes |= Modes.TCR;
				if (!PowerModeCheckBox.Checked) dataflash.Params.DisabledModes |= Modes.Power;
				if (!BypassModeCheckBox.Checked) dataflash.Params.DisabledModes |= Modes.Bypass;
				if (!SmartModeCheckBox.Checked) dataflash.Params.DisabledModes |= Modes.Start;
			}

			// General -> Controls Tab
			dataflash.Params.Status.WakeUpByPlusMinus = WakeUpByPlusMinusCheckBox.Checked;

			// Screen -> Display Tab
			//BrightnessTrackBar.Value = (int)(dataflash.Params.Contrast * 100f / 255);
			dataflash.Params.Contrast = (byte)(BrightnessTrackBar.Value * 255f / 100);
			dataflash.Params.ScreenDimTimeout = (byte)IdleTimeUpDow.Value;
			dataflash.Params.StealthOn = StealthModeCheckBox.Checked;
			dataflash.Params.Status.Flipped = FlippedModeCheckBox.Checked;

			// Screen -> Layout Tab
			dataflash.Params.Status.BatteryPercent = BatteryPercentsCheckBox.Checked;
			dataflash.Params.Status.NoLogo = !ShowLogoCheckBox.Checked;

			// Screen -> Screensaver Tab
			var clockMode = ClockModeComboBox.GetSelectedItem<ClockMode>();
			dataflash.Params.Status.AnalogClock = clockMode == ClockMode.Analog;
			dataflash.Params.Status.DigitalClock = clockMode == ClockMode.Digital;
		}

		private Dataflash ReadDataflash()
		{
			m_simple = m_updater.ReadDataflash();
			return m_manager.Read(m_simple.Data);
		}

		private void DeviceConnected(bool isConnected)
		{
			m_isDeviceConnected = isConnected;
			if (m_isDeviceWasConnectedOnce) return;

			if (!isConnected)
			{
				UpdateUI(() =>
				{
					UpdateUI(() => WelcomeLabel.Text = "Waiting for device with\n\nmyEvic NFE Edition.");
					MainContainer.SelectedPage = WelcomePage;
					m_simple = null;
				});
				return;
			}

			UpdateUI(() => WelcomeLabel.Text = @"Downloading settings...");
			try
			{
				m_dataflash = ReadDataflash();
				if (m_dataflash.Params.Magic != 0xFE)
				{
					DeviceConnected(false);
					return;
				}

				UpdateUI(() =>
				{
					MainContainer.SelectedPage = WorkspacePage;
					m_isDeviceWasConnectedOnce = true;
				});
			}
			catch (Exception ex)
			{
				s_logger.Warn(ex);
				UpdateUI(() => WelcomeLabel.Text = @"Unable to download device settings. Reconnect your device.");
			}
		}

		private void DownloadButton_Click(object sender, EventArgs e)
		{
			if (!ValidateConnectionStatus()) return;

			try
			{
				m_dataflash = ReadDataflash();
				InitializeWorkspaceFromDataflash(m_dataflash);
			}
			catch (Exception ex)
			{
				InfoBox.Show("An error occurred during downloading settings...\n\n" + ex);
			}
		}

		private void UploadButton_Click(object sender, EventArgs e)
		{
			if (!ValidateConnectionStatus()) return;

			try
			{
				var dataflashCopy = new byte[m_simple.Data.Length];
				Buffer.BlockCopy(m_simple.Data, 0, dataflashCopy, 0, m_simple.Data.Length);

				SaveWorkspaceToDataflash(m_dataflash);
				m_manager.Write(m_dataflash, dataflashCopy);
				m_updater.WriteDataflash(new SimpleDataflash { Data = dataflashCopy });
			}
			catch (Exception ex)
			{
				InfoBox.Show("An error occurred during uploading settings...\n\n" + ex);
			}
		}

		private void ResetButton_Click(object sender, EventArgs e)
		{
			if (!ValidateConnectionStatus()) return;

			try
			{
				m_updater.ResetDataflash();
				m_dataflash = ReadDataflash();
				InitializeWorkspaceFromDataflash(m_dataflash);
			}
			catch (Exception ex)
			{
				InfoBox.Show("An error occurred during resetting settings...\n\n" + ex);
			}
		}

		private bool ValidateConnectionStatus()
		{
			while (!m_isDeviceConnected)
			{
				var result = InfoBox.Show
				(
					"No compatible USB devices are connected." +
					"\n\n" +
					"To continue, please connect one." +
					"\n\n" +
					"If one already IS connected, try unplugging and plugging it back in. The cable may be loose.",
					MessageBoxButtons.OKCancel
				);
				if (result == DialogResult.Cancel)
				{
					return false;
				}
			}
			return true;
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
				InitializeWorkspaceFromDataflash(m_dataflash);
			}
		}
	}
}
