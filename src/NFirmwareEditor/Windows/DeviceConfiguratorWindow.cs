using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Linq;
using System.Threading;
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
		private const int MinimumSupportedBuildNumber = 161002;
		private static readonly ILogger s_logger = LogManager.GetCurrentClassLogger();

		private readonly BackgroundWorker m_worker = new BackgroundWorker { WorkerReportsProgress = true };
		private readonly USBConnector m_usbConnector = new USBConnector();
		private readonly COMConnector m_comConnector = new COMConnector();
		private readonly DataflashManager m_manager = new DataflashManager();
		private SimpleDataflash m_simple;
		private Dataflash m_dataflash;
		private bool m_isDeviceWasConnectedOnce;
		private bool m_isDeviceConnected;
		private bool m_isBroadcasting;

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

			MainContainer.SelectedPage = WelcomePage;

			FirmwareVersionTextBox.ReadOnly = true;
			FirmwareVersionTextBox.BackColor = Color.White;

			BuildTextBox.ReadOnly = true;
			BuildTextBox.BackColor = Color.White;

			HardwareVersionTextBox.ReadOnly = true;
			HardwareVersionTextBox.BackColor = Color.White;

			BootModeTextBox.ReadOnly = true;
			BootModeTextBox.BackColor = Color.White;

			TraceTextBox.ReadOnly = true;
			TraceTextBox.BackColor = Color.White;

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
					PreheatPowerUpDown.DecimalPlaces = 0;
					PreheatPowerUpDown.Increment = 1;
					PreheatPowerUpDown.Minimum = 100;
					PreheatPowerUpDown.Maximum = 250;
				}
				else
				{
					PreheatPowerUpDown.DecimalPlaces = 1;
					PreheatPowerUpDown.Increment = 0.1m;
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

				SelectedTCRComboBox.SelectedIndex = Math.Min(m_dataflash.ParamsBlock.SelectedTCRIndex, (byte)2);
				SelectedTCRComboBox.Visible = TCRIndexLabel.Visible = mode == VapeMode.TCR;
			};

			BatteryModelComboBox.SelectedValueChanged += (s, e) =>
			{
				var batteryModel = BatteryModelComboBox.GetSelectedItem<BatteryModel>();
				BatteryEditButton.Visible = batteryModel == BatteryModel.Custom;
			};

			PortComboBox.SelectedIndex = 0;
			BrightnessTrackBar.ValueChanged += (s, e) => BrightnessPercentLabel.Text = (int)(BrightnessTrackBar.Value * 100m / 255) + @"%";

			DownloadButton.Click += DownloadButton_Click;
			UploadButton.Click += UploadButton_Click;
			ResetButton.Click += ResetButton_Click;

			BatteryEditButton.Click += BatteryEditButton_Click;

			TakeScreenshotButton.Click += TakeScreenshotButton_Click;
			SaveScreenshotButton.Click += SaveScreenshotButton_Click;
			BroadcastButton.Click += BroadcastButton_Click;
			RestartButton.Click += RestartButton_Click;
			ResetAndRestartButton.Click += ResetAndRestartButton_Click;

			ComConnectButton.Click += ComConnectButton_Click;
			ComDisconnectButton.Click += ComDisconnectButton_Click;
			CommandTextBox.KeyDown += CommandTextBox_KeyDown;
		}

		private void InititalizeComboBoxes()
		{
			SelectedModeComboBox.Items.Clear();
			SelectedModeComboBox.Items.AddRange(new object[]
			{
				new NamedItemContainer<VapeMode>("Power", VapeMode.Power),
				new NamedItemContainer<VapeMode>("Bypass", VapeMode.Bypass),
				new NamedItemContainer<VapeMode>("Smart / Start", VapeMode.Start),
				new NamedItemContainer<VapeMode>("Temperature Ni", VapeMode.TempNi),
				new NamedItemContainer<VapeMode>("Temperature Ti", VapeMode.TempTi),
				new NamedItemContainer<VapeMode>("Temperature SS", VapeMode.TempSS),
				new NamedItemContainer<VapeMode>("Temperature TCR", VapeMode.TCR),
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
				new NamedItemContainer<ScreenProtectionTime>("Off", ScreenProtectionTime.Off),
				new NamedItemContainer<ScreenProtectionTime>("1 Min", ScreenProtectionTime.Min1),
				new NamedItemContainer<ScreenProtectionTime>("2 Min", ScreenProtectionTime.Min2),
				new NamedItemContainer<ScreenProtectionTime>("5 Min", ScreenProtectionTime.Min5),
				new NamedItemContainer<ScreenProtectionTime>("10 Min", ScreenProtectionTime.Min10),
				new NamedItemContainer<ScreenProtectionTime>("15 Min", ScreenProtectionTime.Min15),
				new NamedItemContainer<ScreenProtectionTime>("20 Min", ScreenProtectionTime.Min20),
				new NamedItemContainer<ScreenProtectionTime>("30 Min", ScreenProtectionTime.Min30)
			});

			BatteryModelComboBox.Items.Clear();
			BatteryModelComboBox.Items.AddRange(new object[]
			{
				new NamedItemContainer<BatteryModel>("Generic Battery", BatteryModel.Generic),
				new NamedItemContainer<BatteryModel>("Samsung 25R", BatteryModel.Samsung25R),
				new NamedItemContainer<BatteryModel>("Samsung 30Q", BatteryModel.Samsung30Q),
				new NamedItemContainer<BatteryModel>("LG HG2", BatteryModel.LGHG2),
				new NamedItemContainer<BatteryModel>("LG HE4", BatteryModel.LGHE4),
				new NamedItemContainer<BatteryModel>("Sony VTC4", BatteryModel.SonyVTC4),
				new NamedItemContainer<BatteryModel>("Sony VTC5", BatteryModel.SonyVTC5),
				new NamedItemContainer<BatteryModel>("Custom", BatteryModel.Custom),
			});

			PortComboBox.Items.Clear();
			var ports = new object[9];
			ports[0] = new NamedItemContainer<string>("Auto", null);
			for (var i = 1; i < ports.Length; i++)
			{
				var portName = "COM" + i;
				ports[i] = new NamedItemContainer<string>(portName, portName);
			}
			PortComboBox.Items.AddRange(ports);
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

		private Image TakeScreenshot(bool ignoreErrors = false)
		{
			try
			{
				var data = m_usbConnector.Screenshot();
				if (data == null || data.All(x => x == 0x00))
				{
					throw new InvalidOperationException("Invalid screenshot data!");
				}
				return BitmapProcessor.CreateBitmapFromBytesArray(64, 128, data);
			}
			catch (Exception ex)
			{
				if (ignoreErrors) return null;

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
			m_worker.DoWork += Worker_DoWork;
			m_worker.ProgressChanged += (s, e) => ProgressLabel.Text = e.ProgressPercentage + @"%";
			m_worker.RunWorkerCompleted += (s, e) => ProgressLabel.Text = @"Operation completed";

			m_usbConnector.DeviceConnected += DeviceConnected;
			m_usbConnector.StartUSBConnectionMonitoring();
			m_comConnector.MessageReceived += COMMessage_Received;

			Closing += (s, e) =>
			{
				m_isBroadcasting = false;
				Safe.Execute(() => m_comConnector.Disconnect());
			};
		}

		private void InitializeWorkspaceFromDataflash([NotNull] Dataflash dataflash)
		{
			if (dataflash == null) throw new ArgumentNullException("dataflash");

			DeviceNameLabel.Text = USBConnector.GetDeviceInfo(dataflash.InfoBlock.ProductID).Name;
			FirmwareVersionTextBox.Text = (dataflash.InfoBlock.FWVersion / 100f).ToString("0.00", CultureInfo.InvariantCulture);
			BuildTextBox.Text = m_simple.Build.ToString();
			HardwareVersionTextBox.Text = (dataflash.ParamsBlock.HardwareVersion / 100f).ToString("0.00", CultureInfo.InvariantCulture);
			BootModeTextBox.Text = dataflash.ParamsBlock.BootMode.ToString();

			// General -> Power & Temp Tab
			PowerUpDown.Value = Math.Max(1, Math.Min(dataflash.ParamsBlock.Power / 10m, 75));
			TCPowerUpDown.Value = Math.Max(1, Math.Min(dataflash.ParamsBlock.TCPower / 10m, 75));
			Step1WCheckBox.Checked = dataflash.ParamsBlock.Status.Step1W;

			TemperatureTypeComboBox.SelectItem(dataflash.ParamsBlock.IsCelsius);
			TemperatureUpDown.Value = dataflash.ParamsBlock.Temperature;
			TemperatureDominantCheckBox.Checked = dataflash.ParamsBlock.Status.TemperatureDominant;

			PreheatTypeComboBox.SelectItem(dataflash.ParamsBlock.Status.PreheatPercent);
			PreheatPowerUpDown.Value = dataflash.ParamsBlock.Status.PreheatPercent ? dataflash.ParamsBlock.PreheatPwr : Math.Max(1, Math.Min(dataflash.ParamsBlock.PreheatPwr / 10m, 75));
			PreheatTimeUpDown.Value = dataflash.ParamsBlock.PreheatTime / 100m;

			// General -> Coils Manager Tab
			ResistanceNiUpDown.Value = dataflash.ParamsBlock.ResistanceNi / 100m;
			ResistanceNiCheckBox.Checked = dataflash.ParamsBlock.ResistanceNiLocked;

			ResistanceTiUpDown.Value = dataflash.ParamsBlock.ResistanceTi / 100m;
			ResistanceTiCheckBox.Checked = dataflash.ParamsBlock.ResistanceTiLocked;

			ResistanceSSUpDown.Value = dataflash.ParamsBlock.ResistanceSS / 100m;
			ResistanceSSCheckBox.Checked = dataflash.ParamsBlock.ResistanceSSLocked;

			ResistanceTCRUpDown.Value = dataflash.ParamsBlock.ResistanceTCR / 100m;
			ResistanceTCRCheckBox.Checked = dataflash.ParamsBlock.ResistanceTCRLocked;

			TCRM1UpDown.Value = dataflash.ParamsBlock.TCR[0];
			TCRM2UpDown.Value = dataflash.ParamsBlock.TCR[1];
			TCRM3UpDown.Value = dataflash.ParamsBlock.TCR[2];

			// General -> Modes Tab
			SelectedModeComboBox.SelectItem(dataflash.ParamsBlock.SelectedMode);
			TempNiModeCheckBox.Checked = !dataflash.ParamsBlock.DisabledModes.HasFlag(VapeModes.TempNi);
			TempTiModeCheckBox.Checked = !dataflash.ParamsBlock.DisabledModes.HasFlag(VapeModes.TempTi);
			TempSSModeCheckBox.Checked = !dataflash.ParamsBlock.DisabledModes.HasFlag(VapeModes.TempSS);
			TCRModeCheckBox.Checked = !dataflash.ParamsBlock.DisabledModes.HasFlag(VapeModes.TCR);
			PowerModeCheckBox.Checked = !dataflash.ParamsBlock.DisabledModes.HasFlag(VapeModes.Power);
			BypassModeCheckBox.Checked = !dataflash.ParamsBlock.DisabledModes.HasFlag(VapeModes.Bypass);
			SmartModeCheckBox.Checked = !dataflash.ParamsBlock.DisabledModes.HasFlag(VapeModes.Start);

			// General -> Controls Tab
			Clicks2ComboBox.SelectItem(dataflash.ParamsBlock.MClicks[0]);
			Clicks3ComboBox.SelectItem(dataflash.ParamsBlock.MClicks[1]);
			Clicks4ComboBox.SelectItem(dataflash.ParamsBlock.MClicks[2]);
			WakeUpByPlusMinusCheckBox.Checked = dataflash.ParamsBlock.Status.WakeUpByPlusMinus;

			// General -> Stats Tab
			PuffsUpDown.Value = Math.Max(0, Math.Min(dataflash.InfoBlock.PuffCount, 99999));
			PuffsTimeUpDown.Value = Math.Max(0, Math.Min(dataflash.InfoBlock.TimeCount / 10m, 99999));

			// Screen -> Display Tab
			BrightnessTrackBar.Value = dataflash.ParamsBlock.Contrast;
			IdleTimeUpDow.Value = dataflash.ParamsBlock.ScreenDimTimeout;
			StealthModeCheckBox.Checked = dataflash.ParamsBlock.StealthOn;
			FlippedModeCheckBox.Checked = dataflash.ParamsBlock.Status.Flipped;

			// Screen -> Layout Tab
			ThirdLineContentComboBox.SelectItem(dataflash.ParamsBlock.ThirdLineContent);
			BatteryPercentsCheckBox.Checked = dataflash.ParamsBlock.Status.BatteryPercent;
			ShowLogoCheckBox.Checked = !dataflash.ParamsBlock.Status.NoLogo;

			if (!dataflash.ParamsBlock.Status.AnalogClock)
			{
				ClockTypeComboBox.SelectItem(ClockType.Disabled);
			}
			else if (dataflash.ParamsBlock.Status.AnalogClock && dataflash.ParamsBlock.Status.DigitalClock)
			{
				ClockTypeComboBox.SelectItem(ClockType.Digital);
			}
			else if (dataflash.ParamsBlock.Status.AnalogClock)
			{
				ClockTypeComboBox.SelectItem(ClockType.Analog);
			}
			UseClassicMenuCheckBox.Checked = dataflash.ParamsBlock.Status.UseClassicMenu;

			// Screen -> Screensaver Tab
			ScreensaverTypeComboBox.SelectItem(dataflash.ParamsBlock.ScreensaverType);
			ScreenProtectionTimeComboBox.SelectItem(dataflash.ParamsBlock.ScreenProtectionTime);

			// Developer -> Expert
			ShuntCorrectionUpDown.Value = Math.Max((byte)85, Math.Min(dataflash.ParamsBlock.ShuntCorrection, (byte)115));
			BatteryModelComboBox.SelectItem(dataflash.ParamsBlock.SelectedBatteryModel);
		}

		private void SaveWorkspaceToDataflash([NotNull] Dataflash dataflash)
		{
			if (dataflash == null) throw new ArgumentNullException("dataflash");

			// General -> Power & Temp Tab
			dataflash.ParamsBlock.Power = (ushort)(PowerUpDown.Value * 10);
			dataflash.ParamsBlock.TCPower = (ushort)(TCPowerUpDown.Value * 10);
			dataflash.ParamsBlock.Status.Step1W = Step1WCheckBox.Checked;

			dataflash.ParamsBlock.IsCelsius = TemperatureTypeComboBox.GetSelectedItem<bool>();
			dataflash.ParamsBlock.Temperature = (ushort)TemperatureUpDown.Value;
			dataflash.ParamsBlock.Status.TemperatureDominant = TemperatureDominantCheckBox.Checked;

			dataflash.ParamsBlock.Status.PreheatPercent = PreheatTypeComboBox.GetSelectedItem<bool>();
			dataflash.ParamsBlock.PreheatPwr = (ushort)(dataflash.ParamsBlock.Status.PreheatPercent ? PreheatPowerUpDown.Value : PreheatPowerUpDown.Value * 10);
			dataflash.ParamsBlock.PreheatTime = (byte)(PreheatTimeUpDown.Value * 100);

			// General -> Coils Manager Tab
			dataflash.ParamsBlock.ResistanceNi = (ushort)(ResistanceNiUpDown.Value * 100);
			dataflash.ParamsBlock.ResistanceNiLocked = ResistanceNiCheckBox.Checked;

			dataflash.ParamsBlock.ResistanceTi = (ushort)(ResistanceTiUpDown.Value * 100);
			dataflash.ParamsBlock.ResistanceTiLocked = ResistanceTiCheckBox.Checked;

			dataflash.ParamsBlock.ResistanceSS = (ushort)(ResistanceSSUpDown.Value * 100);
			dataflash.ParamsBlock.ResistanceSSLocked = ResistanceSSCheckBox.Checked;

			dataflash.ParamsBlock.ResistanceTCR = (ushort)(ResistanceTCRUpDown.Value * 100);
			dataflash.ParamsBlock.ResistanceTCRLocked = ResistanceTCRCheckBox.Checked;

			dataflash.ParamsBlock.TCR[0] = (ushort)TCRM1UpDown.Value;
			dataflash.ParamsBlock.TCR[1] = (ushort)TCRM2UpDown.Value;
			dataflash.ParamsBlock.TCR[2] = (ushort)TCRM3UpDown.Value;

			// General -> Modes Tab
			dataflash.ParamsBlock.SelectedMode = SelectedModeComboBox.GetSelectedItem<VapeMode>();
			dataflash.ParamsBlock.SelectedTCRIndex = (byte)SelectedTCRComboBox.SelectedIndex;
			dataflash.ParamsBlock.DisabledModes = VapeModes.None;
			{
				if (!TempNiModeCheckBox.Checked) dataflash.ParamsBlock.DisabledModes |= VapeModes.TempNi;
				if (!TempTiModeCheckBox.Checked) dataflash.ParamsBlock.DisabledModes |= VapeModes.TempTi;
				if (!TempSSModeCheckBox.Checked) dataflash.ParamsBlock.DisabledModes |= VapeModes.TempSS;
				if (!TCRModeCheckBox.Checked) dataflash.ParamsBlock.DisabledModes |= VapeModes.TCR;
				if (!PowerModeCheckBox.Checked) dataflash.ParamsBlock.DisabledModes |= VapeModes.Power;
				if (!BypassModeCheckBox.Checked) dataflash.ParamsBlock.DisabledModes |= VapeModes.Bypass;
				if (!SmartModeCheckBox.Checked) dataflash.ParamsBlock.DisabledModes |= VapeModes.Start;
			}

			// General -> Controls Tab
			dataflash.ParamsBlock.MClicks[0] = Clicks2ComboBox.GetSelectedItem<ClickAction>();
			dataflash.ParamsBlock.MClicks[1] = Clicks3ComboBox.GetSelectedItem<ClickAction>();
			dataflash.ParamsBlock.MClicks[2] = Clicks4ComboBox.GetSelectedItem<ClickAction>();
			dataflash.ParamsBlock.Status.WakeUpByPlusMinus = WakeUpByPlusMinusCheckBox.Checked;

			// General -> Stats Tab
			dataflash.InfoBlock.PuffCount = (uint)PuffsUpDown.Value;
			dataflash.InfoBlock.TimeCount = (uint)PuffsTimeUpDown.Value * 10;

			// Screen -> Display Tab
			dataflash.ParamsBlock.Contrast = (byte)BrightnessTrackBar.Value;
			dataflash.ParamsBlock.ScreenDimTimeout = (byte)IdleTimeUpDow.Value;
			dataflash.ParamsBlock.StealthOn = StealthModeCheckBox.Checked;
			dataflash.ParamsBlock.Status.Flipped = FlippedModeCheckBox.Checked;

			// Screen -> Layout Tab
			dataflash.ParamsBlock.ThirdLineContent = ThirdLineContentComboBox.GetSelectedItem<LineContentType>();
			dataflash.ParamsBlock.Status.BatteryPercent = BatteryPercentsCheckBox.Checked;
			dataflash.ParamsBlock.Status.NoLogo = !ShowLogoCheckBox.Checked;

			var clockMode = ClockTypeComboBox.GetSelectedItem<ClockType>();
			switch (clockMode)
			{
				case ClockType.Disabled:
				{
					dataflash.ParamsBlock.Status.AnalogClock = false;
					dataflash.ParamsBlock.Status.DigitalClock = false;
					break;
				}
				case ClockType.Analog:
				{
					dataflash.ParamsBlock.Status.AnalogClock = true;
					dataflash.ParamsBlock.Status.DigitalClock = false;
					break;
				}
				case ClockType.Digital:
				{
					dataflash.ParamsBlock.Status.AnalogClock = true;
					dataflash.ParamsBlock.Status.DigitalClock = true;
					break;
				}
				default:
					throw new ArgumentOutOfRangeException();
			}
			dataflash.ParamsBlock.Status.UseClassicMenu = UseClassicMenuCheckBox.Checked;

			// Screen -> Screensaver Tab
			dataflash.ParamsBlock.ScreensaverType = ScreensaverTypeComboBox.GetSelectedItem<ScreensaverType>();
			dataflash.ParamsBlock.ScreenProtectionTime = ScreenProtectionTimeComboBox.GetSelectedItem<ScreenProtectionTime>();

			// Developer -> Expert
			dataflash.ParamsBlock.ShuntCorrection = (byte)ShuntCorrectionUpDown.Value;
			dataflash.ParamsBlock.SelectedBatteryModel = BatteryModelComboBox.GetSelectedItem<BatteryModel>();

			// Setup DateTime
			dataflash.InfoBlock.Year = (ushort)DateTime.Now.Year;
			dataflash.InfoBlock.Month = (byte)DateTime.Now.Month;
			dataflash.InfoBlock.Day = (byte)DateTime.Now.Day;
			dataflash.InfoBlock.Hour = (byte)DateTime.Now.Hour;
			dataflash.InfoBlock.Minute = (byte)DateTime.Now.Minute;
			dataflash.InfoBlock.Second = (byte)DateTime.Now.Second;
		}

		private Dataflash ReadDataflash(BackgroundWorker worker = null)
		{
			m_simple = m_usbConnector.ReadDataflash(worker);
			return m_simple.Build < MinimumSupportedBuildNumber
				? null
				: m_manager.Read(m_simple.Data);
		}

		private void Worker_DoWork(object sender, DoWorkEventArgs e)
		{
			var worker = (BackgroundWorker)sender;
			var wrapper = (AsyncProcessWrapper)e.Argument;

			try
			{
				UpdateUI(() => SetControlButtonsState(false));
				wrapper.Processor(worker);
			}
			finally
			{
				UpdateUI(() => SetControlButtonsState(true));
			}
		}

		private void DeviceConnected(bool isConnected)
		{
			m_isDeviceConnected = isConnected;
			UpdateUI(() => StatusLabel.Text = @"Device is " + (m_isDeviceConnected ? "connected" : "disconnected"));

			if (m_isDeviceWasConnectedOnce) return;

			if (!m_isDeviceConnected)
			{
				UpdateUI(() =>
				{
					UpdateUI(() => WelcomeLabel.Text = string.Format("Waiting for device with\n\nmyEvic NFE Edition\n\n{0} or newer", MinimumSupportedBuildNumber));
					MainContainer.SelectedPage = WelcomePage;
					m_simple = null;
				});
				return;
			}

			UpdateUI(() => WelcomeLabel.Text = @"Downloading settings...");
			try
			{
				var dataflash = ReadDataflash();
				if (dataflash == null)
				{
					DeviceConnected(false);
					return;
				}

				m_dataflash = dataflash;
				if (m_dataflash.ParamsBlock.Magic != 0xFE)
				{
					DeviceConnected(false);
					return;
				}

				UpdateUI(() =>
				{
					InitializeWorkspaceFromDataflash(m_dataflash);
					MainContainer.SelectedPage = WorkspacePage;
					m_isDeviceWasConnectedOnce = true;
				}, false);
			}
			catch (Exception ex)
			{
				s_logger.Warn(ex);
				UpdateUI(() => WelcomeLabel.Text = @"Unable to download device settings. Reconnect your device.");
			}
		}

		private void COMMessage_Received(string message)
		{
			UpdateUI(() => AppendTrace(message));
		}

		private void DownloadButton_Click(object sender, EventArgs e)
		{
			if (!ValidateConnectionStatus()) return;

			m_worker.RunWorkerAsync(new AsyncProcessWrapper(worker =>
			{
				try
				{
					var dataflash = ReadDataflash(worker);
					if (dataflash == null)
					{
						InfoBox.Show("Something strange happened! Please restart application.");
						return;
					}
					m_dataflash = dataflash;
					UpdateUI(() => InitializeWorkspaceFromDataflash(m_dataflash));
				}
				catch (Exception ex)
				{
					s_logger.Warn(ex);
					InfoBox.Show(GetErrorMessage("downloading settings"));
				}
			}));
		}

		private void UploadButton_Click(object sender, EventArgs e)
		{
			if (!ValidateConnectionStatus()) return;

			m_worker.RunWorkerAsync(new AsyncProcessWrapper(worker =>
			{
				try
				{
					var dataflashCopy = new byte[m_simple.Data.Length];
					Buffer.BlockCopy(m_simple.Data, 0, dataflashCopy, 0, m_simple.Data.Length);

					UpdateUI(() => SaveWorkspaceToDataflash(m_dataflash));

					m_manager.Write(m_dataflash, dataflashCopy);
					m_usbConnector.WriteDataflash(new SimpleDataflash { Data = dataflashCopy }, worker);
				}
				catch (Exception ex)
				{
					s_logger.Warn(ex);
					InfoBox.Show(GetErrorMessage("uploading settings"));
				}
			}));
		}

		private void ResetButton_Click(object sender, EventArgs e)
		{
			if (!ValidateConnectionStatus()) return;

			m_worker.RunWorkerAsync(new AsyncProcessWrapper(worker =>
			{
				try
				{
					m_usbConnector.ResetDataflash();
					m_dataflash = ReadDataflash(worker);
					UpdateUI(() => InitializeWorkspaceFromDataflash(m_dataflash));
				}
				catch (Exception ex)
				{
					s_logger.Warn(ex);
					InfoBox.Show(GetErrorMessage("resetting settings"));
				}
			}));
		}

		private void BatteryEditButton_Click(object sender, EventArgs e)
		{
			using (var editor = new DischargeProfileWindow(m_dataflash))
			{
				editor.ShowDialog();
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
			if (TakeScreenshotBeforeSaveCheckBox.Checked || ScreenshotPictureBox.Image == null)
			{
				if (!ValidateConnectionStatus()) return;

				var screenshot = TakeScreenshot();
				if (screenshot == null) return;

				ShowScreenshot(screenshot);
			}

			using (var containerImage = new Bitmap(ScreenshotContainerPanel.Width, ScreenshotContainerPanel.Height))
			{
				ScreenshotContainerPanel.DrawToBitmap(containerImage, ScreenshotContainerPanel.DisplayRectangle);

				using (var sf = new SaveFileDialog { FileName = string.Format("{0:yyyy.MM.dd HH.mm.ss}", DateTime.Now), Filter = Consts.PngExportFilter })
				{
					if (sf.ShowDialog() != DialogResult.OK) return;

					using (var exportImage = BitmapProcessor.EnlargePixelSize(containerImage, (int)PixelSizeUpDown.Value))
					{
						exportImage.Save(sf.FileName, ImageFormat.Png);
					}
				}
			}
		}

		private void BroadcastButton_Click(object sender, EventArgs e)
		{
			if (m_isBroadcasting)
			{
				m_isBroadcasting = false;
			}
			else
			{
				if (!ValidateConnectionStatus()) return;

				TerminalTabPage.Enabled = false;
				SetControlButtonsState(false);
				BroadcastButton.Text = @"Stop broadcast";
				m_isBroadcasting = true;
				new Thread(() =>
				{
					while (m_isBroadcasting)
					{
						Image screenshot = null;
						var ex = Safe.Execute(() =>
						{
							screenshot = TakeScreenshot(true);
							if (screenshot == null) return;

							UpdateUI(() => ShowScreenshot(screenshot));
						});
						if (ex != null || screenshot == null) break;
					}

					m_isBroadcasting = false;
					UpdateUI(() =>
					{
						TerminalTabPage.Enabled = true;
						SetControlButtonsState(true);
						BroadcastButton.Text = @"Start broadcast";
					});
				}) { IsBackground = true }.Start();
			}
		}

		private void RestartButton_Click(object sender, EventArgs e)
		{
			if (!ValidateConnectionStatus()) return;

			try
			{
				m_usbConnector.RestartDevice();
			}
			catch (Exception ex)
			{
				s_logger.Warn(ex);
				InfoBox.Show(GetErrorMessage("restarting device"));
			}
		}

		private void ResetAndRestartButton_Click(object sender, EventArgs e)
		{
			if (!ValidateConnectionStatus()) return;

			try
			{
				m_usbConnector.ResetDataflash();
				m_usbConnector.RestartDevice();
			}
			catch (Exception ex)
			{
				s_logger.Warn(ex);
				InfoBox.Show(GetErrorMessage("resetting dataflash and restarting device"));
			}
		}

		private void ComConnectButton_Click(object sender, EventArgs e)
		{
			var portName = PortComboBox.GetSelectedItem<string>();
			string selectedPort = null;

			try
			{
				selectedPort = m_comConnector.Connect(portName);
			}
			catch (Exception ex)
			{
				s_logger.Warn(ex);
			}

			if (string.IsNullOrEmpty(selectedPort))
			{
				InfoBox.Show("Switch USB mode to COM and try again.");
				return;
			}

			ComConnectButton.Enabled = false;
			ComDisconnectButton.Enabled = true;
		}

		private void ComDisconnectButton_Click(object sender, EventArgs e)
		{
			try
			{
				m_comConnector.Disconnect();
			}
			catch (Exception ex)
			{
				s_logger.Warn(ex);
			}

			ComConnectButton.Enabled = true;
			ComDisconnectButton.Enabled = false;
		}

		private void CommandTextBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode != Keys.Enter) return;

			var command = CommandTextBox.Text;
			if (string.IsNullOrEmpty(command)) return;

			var sendResult = false;
			try
			{
				CommandTextBox.Clear();
				sendResult = m_comConnector.Send(command);
			}
			catch (Exception ex)
			{
				s_logger.Warn(ex);
			}

			AppendTrace(sendResult ? ">" + command : "Failed to send command.");
		}

		private void SetControlButtonsState(bool enabled)
		{
			DownloadButton.Enabled = UploadButton.Enabled = ResetButton.Enabled = RestartButton.Enabled = ResetAndRestartButton.Enabled = enabled;
			TakeScreenshotButton.Enabled = SaveScreenshotButton.Enabled = enabled;
		}

		private void AppendTrace(string message)
		{
			TraceTextBox.AppendText(message + Environment.NewLine);
			TraceTextBox.ScrollToEnd();
		}

		private string GetErrorMessage(string operationName)
		{
			return "An error occurred during " +
			       operationName +
			       "...\n\n" +
				   "To continue, please activate or reconnect your device.";
		}

		internal enum ClockType
		{
			Disabled,
			Analog,
			Digital
		}
	}
}
