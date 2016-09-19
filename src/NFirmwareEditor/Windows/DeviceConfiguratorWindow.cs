using System;
using System.Globalization;
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

			BrightnessTrackBar.ValueChanged += (s, e) => BrightnessPercentLabel.Text = BrightnessTrackBar.Value + @"%";

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

		private void InitializeWorkspace([NotNull] Dataflash dataflash)
		{
			if (dataflash == null) throw new ArgumentNullException("dataflash");

			DeviceNameLabel.Text = FirmwareUpdater.GetDeviceInfo(dataflash.Info.ProductID).Name;
			FirmwareVersionTextBox.Text = (dataflash.Info.FWVersion / 100f).ToString("0.00", CultureInfo.InvariantCulture);
			HardwareVersionTextBox.Text = (dataflash.Params.HardwareVersion / 100f).ToString("0.00", CultureInfo.InvariantCulture);
			BootModeTextBox.Text = dataflash.Params.BootMode.ToString();

			// Power & Resistance Tab
			PowerUpDown.Value = Math.Min(dataflash.Params.Power / 10, 75);
			TCPowerUpDown.Value = Math.Min(dataflash.Params.TCPower / 10, 75);
			Step1WCheckBox.Checked = dataflash.Params.Status.Step1W;

			TemperatureComboBox.SelectItem(dataflash.Params.IsCelsius);
			TemperatureUpDown.Value = dataflash.Params.Temperature;
			TemperatureDominantCheckBox.Checked = dataflash.Params.Status.TemperatureDominant;

			PreheatTypeComboBox.SelectItem(dataflash.Params.Status.PreheatPercent);
			PreheatPowerUpDown.Value = dataflash.Params.Status.PreheatPercent ? dataflash.Params.PreheatPwr : Math.Min(dataflash.Params.PreheatPwr / 10m, 75);
			PreheatTimeUpDown.Value = dataflash.Params.PreheatTime / 100m;

			// Coils Manager Tab
			ResistanceNiUpDown.Value = dataflash.Params.ResistanceNi / 100m;
			ResistanceNiCheckBox.Checked = dataflash.Params.ResistanceNiLocked;

			ResistanceTiUpDown.Value = dataflash.Params.ResistanceTi / 100m;
			ResistanceTiCheckBox.Checked = dataflash.Params.ResistanceTiLocked;

			resistanceSsUpDown.Value = dataflash.Params.ResistanceSS / 100m;
			ResistanceSSCheckBox.Checked = dataflash.Params.ResistanceSSLocked;

			ResistanceTCRUpDown.Value = dataflash.Params.ResistanceTCR / 100m;
			ResistanceTCRCheckBox.Checked = dataflash.Params.ResistanceTCRLocked;

			TCRM1UpDown.Value = dataflash.Params.TCR[0];
			TCRM2UpDown.Value = dataflash.Params.TCR[1];
			TCRM3UpDown.Value = dataflash.Params.TCR[2];

			// Modes Tab
			CurrentModeComboBox.SelectItem(dataflash.Params.Mode);
			TempNiModeCheckBox.Checked = !dataflash.Params.DisabledModes.HasFlag(Modes.TempNi);
			TempTiModeCheckBox.Checked = !dataflash.Params.DisabledModes.HasFlag(Modes.TempTi);
			TempSSModeCheckBox.Checked = !dataflash.Params.DisabledModes.HasFlag(Modes.TempSS);
			TCRModeCheckBox.Checked = !dataflash.Params.DisabledModes.HasFlag(Modes.TCR);
			PowerModeCheckBox.Checked = !dataflash.Params.DisabledModes.HasFlag(Modes.Power);
			BypassModeCheckBox.Checked = !dataflash.Params.DisabledModes.HasFlag(Modes.Bypass);
			SmartModeCheckBox.Checked = !dataflash.Params.DisabledModes.HasFlag(Modes.Start);

			// Screen Tab
			BrightnessTrackBar.Value = (int)(dataflash.Params.Contrast * 100f / 255);
			IdleTimeUpDow.Value = dataflash.Params.ScreenDimTimeout;
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
					SetConfiguratorButtonsState(false);
					UpdateUI(() => WelcomeLabel.Text = "Waiting for device with\n\nmyEvic NFE Edition.");
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
				if (m_dataflash.Params.Magic != 0xFE)
				{
					DeviceConnected(false);
					return;
				}

				UpdateUI(() =>
				{
					MainContainer.SelectedPage = WorkspacePage;
					SetConfiguratorButtonsState(true);
				});
			}
			catch (Exception ex)
			{
				s_logger.Warn(ex);
				UpdateUI(() => WelcomeLabel.Text = @"Unable to download device settings. Reconnect your device.");
			}
		}

		private void SetConfiguratorButtonsState(bool isActive)
		{
			DownloadButton.Enabled = isActive;
			UploadButton.Enabled = isActive;
			ResetButton.Enabled = isActive;
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
