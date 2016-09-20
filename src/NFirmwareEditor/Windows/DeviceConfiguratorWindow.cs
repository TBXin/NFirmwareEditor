using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Linq;
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

			InititalizeComboBoxes();

			TemperatureTypeComboBox.SelectedValueChanged += (s, e) =>
			{
				var isCelcius = TemperatureTypeComboBox.GetSelectedItem<bool>();
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

			SelectedModeComboBox.SelectedValueChanged += (s, e) =>
			{
				var mode = SelectedModeComboBox.GetSelectedItem<VapeMode>();
				switch (mode)
				{
					case VapeMode.TempNi:
						SetupModesCheckBoxes(TempNiModeCheckBox);
						break;
					case VapeMode.TempTi:
						SetupModesCheckBoxes(TempTiModeCheckBox);
						break;
					case VapeMode.TempSS:
						SetupModesCheckBoxes(TempSSModeCheckBox);
						break;
					case VapeMode.TCR:
						SetupModesCheckBoxes(TCRModeCheckBox);
						break;
					case VapeMode.Power:
						SetupModesCheckBoxes(PowerModeCheckBox);
						break;
					case VapeMode.Bypass:
						SetupModesCheckBoxes(BypassModeCheckBox);
						break;
					case VapeMode.Start:
						SetupModesCheckBoxes(SmartModeCheckBox);
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}

				if (mode == VapeMode.TCR)
				{
					SelectedTCRComboBox.Visible = TCRIndexLabel.Visible = true;
					SelectedTCRComboBox.SelectedIndex = m_dataflash.Params.SelectedTCRIndex;
				}
				else
				{
					SelectedTCRComboBox.Visible = TCRIndexLabel.Visible = false;
				}
			};

			BrightnessTrackBar.ValueChanged += (s, e) => BrightnessPercentLabel.Text = BrightnessTrackBar.Value + @"%";

			DownloadButton.Click += DownloadButton_Click;
			UploadButton.Click += UploadButton_Click;
			ResetButton.Click += ResetButton_Click;

			TakeScreenshotButton.Click += TakeScreenshotButton_Click;
			SaveScreenshotButton.Click += SaveScreenshotButton_Click;
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

		private Image TakeScreenshot()
		{
			try
			{
				var data = m_updater.Screenshot();
				if (data == null || data.All(x => x == 0x00))
				{
					throw new InvalidOperationException("Invalid screenshot data!");
				}
				return BitmapProcessor.CreateBitmapFromBytesArray(64, 128, data);
			}
			catch (Exception ex)
			{
				s_logger.Warn(ex);
				InfoBox.Show
				(
					"An error occurred during taking screenshot..." +
					"\n\n" +
					"To continue, please activate or reconnect your device."
				);
				return null;
			}
		}

		private void ShowScreenshot([NotNull] Image screenshot)
		{
			if (screenshot == null) throw new ArgumentNullException("screenshot");
			if (ScreenshotPictureBox.Image != null)
			{
				ScreenshotPictureBox.Image.Dispose();
				ScreenshotPictureBox.Image = null;
			}
			ScreenshotPictureBox.Image = screenshot;
		}

		private void InititalizeComboBoxes()
		{
			SelectedModeComboBox.Items.Clear();
			SelectedModeComboBox.Items.AddRange(new object[]
			{
			    new NamedItemContainer<VapeMode>("Temperature Ni", VapeMode.TempNi),
			    new NamedItemContainer<VapeMode>("Temperature Ti", VapeMode.TempTi),
			    new NamedItemContainer<VapeMode>("Temperature SS", VapeMode.TempSS),
			    new NamedItemContainer<VapeMode>("Temperature TCR", VapeMode.TCR),
			    new NamedItemContainer<VapeMode>("Power", VapeMode.Power),
			    new NamedItemContainer<VapeMode>("Bypass", VapeMode.Bypass),
			    new NamedItemContainer<VapeMode>("Smart / Start", VapeMode.Start)
			});

			TemperatureTypeComboBox.Items.Clear();
			TemperatureTypeComboBox.Items.AddRange(new object[]
			{
			    new NamedItemContainer<bool>("°F", false),
			    new NamedItemContainer<bool>("°C", true)
			});

			PreheatTypeComboBox.Items.Clear();
			PreheatTypeComboBox.Items.AddRange(new object[]
			{
			    new NamedItemContainer<bool>("%", true),
			    new NamedItemContainer<bool>("W", false)
			});

			var clicks = new object[]
			{
				new NamedItemContainer<ClickAction>("None", ClickAction.None),
				new NamedItemContainer<ClickAction>("Edit", ClickAction.Edit),
				new NamedItemContainer<ClickAction>("Clock", ClickAction.Clock),
				new NamedItemContainer<ClickAction>("Temperature Dominant", ClickAction.TDom),
				new NamedItemContainer<ClickAction>("Next Mode", ClickAction.NextMode),
				new NamedItemContainer<ClickAction>("On / Off", ClickAction.OnOff)
			};

			Clicks2ComboBox.Items.Clear();
			Clicks2ComboBox.Items.AddRange(clicks);

			Clicks3ComboBox.Items.Clear();
			Clicks3ComboBox.Items.AddRange(clicks);

			Clicks4ComboBox.Items.Clear();
			Clicks4ComboBox.Items.AddRange(clicks);

			ThirdLineContentComboBox.Items.Clear();
			ThirdLineContentComboBox.Items.AddRange(new object[]
			{
				new NamedItemContainer<LineContentType>("Amps", LineContentType.Amps),
				new NamedItemContainer<LineContentType>("Puffs", LineContentType.Puffs),
				new NamedItemContainer<LineContentType>("Time", LineContentType.Time),
				new NamedItemContainer<LineContentType>("Date and Time", LineContentType.DataTime),
				new NamedItemContainer<LineContentType>("Battery Voltage", LineContentType.BatteryVoltage),
				new NamedItemContainer<LineContentType>("Output Voltage", LineContentType.OutputVoltage),
				new NamedItemContainer<LineContentType>("Board Temperature", LineContentType.BoardTemperature),
				new NamedItemContainer<LineContentType>("Real-time Resistance", LineContentType.RealTimeResistance)
			});

			ClockTypeComboBox.Items.Clear();
			ClockTypeComboBox.Items.AddRange(new object[]
			{
			   new NamedItemContainer<ClockType>("Disabled", ClockType.Disabled),
			   new NamedItemContainer<ClockType>("Analog", ClockType.Analog),
			   new NamedItemContainer<ClockType>("Digital", ClockType.Digital)
			});

			ScreensaverTypeComboBox.Items.Clear();
			ScreensaverTypeComboBox.Items.AddRange(new object[]
			{
			    new NamedItemContainer<ScreensaverType>("None", ScreensaverType.None),
			    new NamedItemContainer<ScreensaverType>("Clock", ScreensaverType.Clock),
			    new NamedItemContainer<ScreensaverType>("Cube", ScreensaverType.Cube)
			});

			ScreenProtectionTimeComboBox.Items.Clear();
			ScreenProtectionTimeComboBox.Items.AddRange(new object[]
			{
			    new NamedItemContainer<ScreenProtectionTime>("1 Min", ScreenProtectionTime.Min1),
			    new NamedItemContainer<ScreenProtectionTime>("2 Min", ScreenProtectionTime.Min2),
			    new NamedItemContainer<ScreenProtectionTime>("5 Min", ScreenProtectionTime.Min5),
			    new NamedItemContainer<ScreenProtectionTime>("10 Min", ScreenProtectionTime.Min10),
			    new NamedItemContainer<ScreenProtectionTime>("15 Min", ScreenProtectionTime.Min15),
			    new NamedItemContainer<ScreenProtectionTime>("20 Min", ScreenProtectionTime.Min20),
			    new NamedItemContainer<ScreenProtectionTime>("30 Min", ScreenProtectionTime.Min30),
			    new NamedItemContainer<ScreenProtectionTime>("Off", ScreenProtectionTime.Off),
			});
		}

		private void SetupModesCheckBoxes(CheckBox selected)
		{
			var checkBoxes = new[]
			{
				TempNiModeCheckBox,
				TempTiModeCheckBox,
				TempSSModeCheckBox,
				TCRModeCheckBox,
				PowerModeCheckBox,
				BypassModeCheckBox,
				SmartModeCheckBox
			};

			foreach (var checkBox in checkBoxes)
			{
				if (checkBox == selected)
				{
					checkBox.Checked = true;
					checkBox.Enabled = false;
				}
				else
				{
					checkBox.Enabled = true;
				}
			}
		}

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
			PowerUpDown.Value = Math.Max(1, Math.Min(dataflash.Params.Power / 10, 75));
			TCPowerUpDown.Value = Math.Max(1, Math.Min(dataflash.Params.TCPower / 10, 75));
			Step1WCheckBox.Checked = dataflash.Params.Status.Step1W;

			TemperatureTypeComboBox.SelectItem(dataflash.Params.IsCelsius);
			TemperatureUpDown.Value = dataflash.Params.Temperature;
			TemperatureDominantCheckBox.Checked = dataflash.Params.Status.TemperatureDominant;

			PreheatTypeComboBox.SelectItem(dataflash.Params.Status.PreheatPercent);
			PreheatPowerUpDown.Value = dataflash.Params.Status.PreheatPercent ? dataflash.Params.PreheatPwr : Math.Max(1, Math.Min(dataflash.Params.PreheatPwr / 10m, 75));
			PreheatTimeUpDown.Value = dataflash.Params.PreheatTime / 100m;

			// General -> Coils Manager Tab
			ResistanceNiUpDown.Value = dataflash.Params.ResistanceNi / 100m;
			ResistanceNiCheckBox.Checked = dataflash.Params.ResistanceNiLocked;

			ResistanceTiUpDown.Value = dataflash.Params.ResistanceTi / 100m;
			ResistanceTiCheckBox.Checked = dataflash.Params.ResistanceTiLocked;

			ResistanceSSUpDown.Value = dataflash.Params.ResistanceSS / 100m;
			ResistanceSSCheckBox.Checked = dataflash.Params.ResistanceSSLocked;

			ResistanceTCRUpDown.Value = dataflash.Params.ResistanceTCR / 100m;
			ResistanceTCRCheckBox.Checked = dataflash.Params.ResistanceTCRLocked;

			TCRM1UpDown.Value = dataflash.Params.TCR[0];
			TCRM2UpDown.Value = dataflash.Params.TCR[1];
			TCRM3UpDown.Value = dataflash.Params.TCR[2];

			// General -> Modes Tab
			SelectedModeComboBox.SelectItem(dataflash.Params.SelectedMode);
			TempNiModeCheckBox.Checked = !dataflash.Params.DisabledModes.HasFlag(VapeModes.TempNi);
			TempTiModeCheckBox.Checked = !dataflash.Params.DisabledModes.HasFlag(VapeModes.TempTi);
			TempSSModeCheckBox.Checked = !dataflash.Params.DisabledModes.HasFlag(VapeModes.TempSS);
			TCRModeCheckBox.Checked = !dataflash.Params.DisabledModes.HasFlag(VapeModes.TCR);
			PowerModeCheckBox.Checked = !dataflash.Params.DisabledModes.HasFlag(VapeModes.Power);
			BypassModeCheckBox.Checked = !dataflash.Params.DisabledModes.HasFlag(VapeModes.Bypass);
			SmartModeCheckBox.Checked = !dataflash.Params.DisabledModes.HasFlag(VapeModes.Start);

			// General -> Controls Tab
			Clicks2ComboBox.SelectItem(dataflash.Params.MClicks[0]);
			Clicks3ComboBox.SelectItem(dataflash.Params.MClicks[1]);
			Clicks4ComboBox.SelectItem(dataflash.Params.MClicks[2]);
			WakeUpByPlusMinusCheckBox.Checked = dataflash.Params.Status.WakeUpByPlusMinus;

			// Screen -> Display Tab
			BrightnessTrackBar.Value = (int)(dataflash.Params.Contrast * 100f / 255);
			IdleTimeUpDow.Value = dataflash.Params.ScreenDimTimeout;
			StealthModeCheckBox.Checked = dataflash.Params.StealthOn;
			FlippedModeCheckBox.Checked = dataflash.Params.Status.Flipped;

			// Screen -> Layout Tab
			ThirdLineContentComboBox.SelectItem(dataflash.Params.ThirdLineContent);
			BatteryPercentsCheckBox.Checked = dataflash.Params.Status.BatteryPercent;
			ShowLogoCheckBox.Checked = !dataflash.Params.Status.NoLogo;

			if (!dataflash.Params.Status.AnalogClock)
			{
				ClockTypeComboBox.SelectItem(ClockType.Disabled);
			}
			else if (dataflash.Params.Status.AnalogClock && dataflash.Params.Status.DigitalClock)
			{
				ClockTypeComboBox.SelectItem(ClockType.Digital);
			}
			else if (dataflash.Params.Status.AnalogClock)
			{
				ClockTypeComboBox.SelectItem(ClockType.Analog);
			}

			// Screen -> Screensaver Tab
			ScreensaverTypeComboBox.SelectItem(dataflash.Params.ScreensaverType);
			ScreenProtectionTimeComboBox.SelectItem(dataflash.Params.ScreenProtectionTime);
		}

		private void SaveWorkspaceToDataflash([NotNull] Dataflash dataflash)
		{
			if (dataflash == null) throw new ArgumentNullException("dataflash");

			// General -> Power & Temp Tab
			dataflash.Params.Power = (ushort)(PowerUpDown.Value * 10);
			dataflash.Params.TCPower = (ushort)(TCPowerUpDown.Value * 10);
			dataflash.Params.Status.Step1W = Step1WCheckBox.Checked;

			dataflash.Params.IsCelsius = TemperatureTypeComboBox.GetSelectedItem<bool>();
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

			dataflash.Params.ResistanceSS = (ushort)(ResistanceSSUpDown.Value * 100);
			dataflash.Params.ResistanceSSLocked = ResistanceSSCheckBox.Checked;

			dataflash.Params.ResistanceTCR = (ushort)(ResistanceTCRUpDown.Value * 100);
			dataflash.Params.ResistanceTCRLocked = ResistanceTCRCheckBox.Checked;

			dataflash.Params.TCR[0] = (ushort)TCRM1UpDown.Value;
			dataflash.Params.TCR[1] = (ushort)TCRM2UpDown.Value;
			dataflash.Params.TCR[2] = (ushort)TCRM3UpDown.Value;

			// General -> Modes Tab
			dataflash.Params.SelectedMode = SelectedModeComboBox.GetSelectedItem<VapeMode>();
			dataflash.Params.SelectedTCRIndex = (byte)SelectedTCRComboBox.SelectedIndex;
			dataflash.Params.DisabledModes = VapeModes.None;
			{
				if (!TempNiModeCheckBox.Checked) dataflash.Params.DisabledModes |= VapeModes.TempNi;
				if (!TempTiModeCheckBox.Checked) dataflash.Params.DisabledModes |= VapeModes.TempTi;
				if (!TempSSModeCheckBox.Checked) dataflash.Params.DisabledModes |= VapeModes.TempSS;
				if (!TCRModeCheckBox.Checked) dataflash.Params.DisabledModes |= VapeModes.TCR;
				if (!PowerModeCheckBox.Checked) dataflash.Params.DisabledModes |= VapeModes.Power;
				if (!BypassModeCheckBox.Checked) dataflash.Params.DisabledModes |= VapeModes.Bypass;
				if (!SmartModeCheckBox.Checked) dataflash.Params.DisabledModes |= VapeModes.Start;
			}

			// General -> Controls Tab
			dataflash.Params.MClicks[0] = Clicks2ComboBox.GetSelectedItem<ClickAction>();
			dataflash.Params.MClicks[1] = Clicks3ComboBox.GetSelectedItem<ClickAction>();
			dataflash.Params.MClicks[2] = Clicks4ComboBox.GetSelectedItem<ClickAction>();
			dataflash.Params.Status.WakeUpByPlusMinus = WakeUpByPlusMinusCheckBox.Checked;

			// Screen -> Display Tab
			dataflash.Params.Contrast = (byte)(BrightnessTrackBar.Value * 255f / 100);
			dataflash.Params.ScreenDimTimeout = (byte)IdleTimeUpDow.Value;
			dataflash.Params.StealthOn = StealthModeCheckBox.Checked;
			dataflash.Params.Status.Flipped = FlippedModeCheckBox.Checked;

			// Screen -> Layout Tab
			dataflash.Params.ThirdLineContent = ThirdLineContentComboBox.GetSelectedItem<LineContentType>();
			dataflash.Params.Status.BatteryPercent = BatteryPercentsCheckBox.Checked;
			dataflash.Params.Status.NoLogo = !ShowLogoCheckBox.Checked;

			var clockMode = ClockTypeComboBox.GetSelectedItem<ClockType>();
			switch (clockMode)
			{
				case ClockType.Disabled:
				{
					dataflash.Params.Status.AnalogClock = false;
					dataflash.Params.Status.DigitalClock = false;
					break;
				}
				case ClockType.Analog:
				{
					dataflash.Params.Status.AnalogClock = true;
					dataflash.Params.Status.DigitalClock = false;
					break;
				}
				case ClockType.Digital:
				{
					dataflash.Params.Status.AnalogClock = true;
					dataflash.Params.Status.DigitalClock = true;
					break;
				}
				default:
					throw new ArgumentOutOfRangeException();
			}

			// Screen -> Screensaver Tab
			dataflash.Params.ScreensaverType = ScreensaverTypeComboBox.GetSelectedItem<ScreensaverType>();
			dataflash.Params.ScreenProtectionTime = ScreenProtectionTimeComboBox.GetSelectedItem<ScreenProtectionTime>();

			// Setup DateTime
			dataflash.Info.Year = (ushort)DateTime.Now.Year;
			dataflash.Info.Month = (byte)DateTime.Now.Month;
			dataflash.Info.Day = (byte)DateTime.Now.Day;
			dataflash.Info.Hour = (byte)DateTime.Now.Hour;
			dataflash.Info.Minute = (byte)DateTime.Now.Minute;
			dataflash.Info.Second = (byte)DateTime.Now.Second;
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
				s_logger.Warn(ex);
				InfoBox.Show(GetErrorMessage("downloading settings"));
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
				s_logger.Warn(ex);
				InfoBox.Show(GetErrorMessage("uploading settings"));
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
				s_logger.Warn(ex);
				InfoBox.Show(GetErrorMessage("resetting settings"));
			}
		}

		private void TakeScreenshotButton_Click(object sender, EventArgs e)
		{
			if (!ValidateConnectionStatus()) return;

			var screenshot = TakeScreenshot();
			if (screenshot == null) return;

			ShowScreenshot(screenshot);
		}

		private void SaveScreenshotButton_Click(object sender, EventArgs e)
		{
			if (!ValidateConnectionStatus()) return;

			var screenshot = TakeScreenshot();
			if (screenshot == null) return;

			ShowScreenshot(screenshot);
			using (var export = new Bitmap(panel1.Width, panel1.Height))
			{
				panel1.DrawToBitmap(export, panel1.DisplayRectangle);

				using (var sf = new SaveFileDialog { FileName = string.Format("{0:yyyy.MM.dd HH.mm.ss}", DateTime.Now), Filter = Consts.PngExportFilter })
				{
					if (sf.ShowDialog() != DialogResult.OK) return;
					export.Save(sf.FileName, ImageFormat.Png);
				}
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

		private string GetErrorMessage(string operationName)
		{
			return "An error occurred during " +
			       operationName +
			       "...\n\n" +
				   "To continue, please activate or reconnect your device.";
		}

		private void MainContainer_SelectedPageChanged(object sender, EventArgs e)
		{
			if (MainContainer.SelectedPage == WorkspacePage)
			{
				InitializeWorkspaceFromDataflash(m_dataflash);
			}
		}

		internal enum ClockType
		{
			Disabled,
			Analog,
			Digital
		}
	}
}
