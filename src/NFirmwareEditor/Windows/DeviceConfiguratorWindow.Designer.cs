namespace NFirmwareEditor.Windows
{
	partial class DeviceConfiguratorWindow
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.MainContainer = new NFirmwareEditor.UI.MultiPanel();
			this.WelcomePage = new NFirmwareEditor.UI.MultiPanelPage();
			this.WelcomeLabel = new System.Windows.Forms.Label();
			this.WorkspacePage = new NFirmwareEditor.UI.MultiPanelPage();
			this.MainStatusBar = new System.Windows.Forms.StatusStrip();
			this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.ProgressLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.groupPanel1 = new NFirmwareEditor.UI.GroupPanel();
			this.BuildTextBox = new System.Windows.Forms.TextBox();
			this.label48 = new System.Windows.Forms.Label();
			this.FirmwareVersionTextBox = new System.Windows.Forms.TextBox();
			this.HardwareVersionTextBox = new System.Windows.Forms.TextBox();
			this.DeviceNameLabel = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.BootModeTextBox = new System.Windows.Forms.TextBox();
			this.ResetButton = new System.Windows.Forms.Button();
			this.UploadButton = new System.Windows.Forms.Button();
			this.DownloadButton = new System.Windows.Forms.Button();
			this.MainTabControl = new System.Windows.Forms.TabControl();
			this.GeneralTabPage = new System.Windows.Forms.TabPage();
			this.tabControl2 = new System.Windows.Forms.TabControl();
			this.PowerTabPage = new System.Windows.Forms.TabPage();
			this.TemperatureUpDown = new System.Windows.Forms.NumericUpDown();
			this.PreheatTimeUpDown = new System.Windows.Forms.NumericUpDown();
			this.PreheatPowerUpDown = new System.Windows.Forms.NumericUpDown();
			this.TCPowerUpDown = new System.Windows.Forms.NumericUpDown();
			this.PowerUpDown = new System.Windows.Forms.NumericUpDown();
			this.Step1WCheckBox = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.TemperatureDominantCheckBox = new System.Windows.Forms.CheckBox();
			this.label30 = new System.Windows.Forms.Label();
			this.label29 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.PreheatTypeComboBox = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label28 = new System.Windows.Forms.Label();
			this.TemperatureTypeComboBox = new System.Windows.Forms.ComboBox();
			this.label11 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.CoilsManagerTabPage = new System.Windows.Forms.TabPage();
			this.label19 = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.TCRM3UpDown = new System.Windows.Forms.NumericUpDown();
			this.label7 = new System.Windows.Forms.Label();
			this.label18 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.TCRM2UpDown = new System.Windows.Forms.NumericUpDown();
			this.ResistanceNiUpDown = new System.Windows.Forms.NumericUpDown();
			this.label17 = new System.Windows.Forms.Label();
			this.TCRM1UpDown = new System.Windows.Forms.NumericUpDown();
			this.label14 = new System.Windows.Forms.Label();
			this.ResistanceTiUpDown = new System.Windows.Forms.NumericUpDown();
			this.label13 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.ResistanceTCRCheckBox = new System.Windows.Forms.CheckBox();
			this.ResistanceSSUpDown = new System.Windows.Forms.NumericUpDown();
			this.ResistanceSSCheckBox = new System.Windows.Forms.CheckBox();
			this.label9 = new System.Windows.Forms.Label();
			this.ResistanceTiCheckBox = new System.Windows.Forms.CheckBox();
			this.ResistanceTCRUpDown = new System.Windows.Forms.NumericUpDown();
			this.ResistanceNiCheckBox = new System.Windows.Forms.CheckBox();
			this.label10 = new System.Windows.Forms.Label();
			this.ModesTabPage = new System.Windows.Forms.TabPage();
			this.TCRIndexLabel = new System.Windows.Forms.Label();
			this.label21 = new System.Windows.Forms.Label();
			this.SelectedTCRComboBox = new System.Windows.Forms.ComboBox();
			this.label20 = new System.Windows.Forms.Label();
			this.SelectedModeComboBox = new System.Windows.Forms.ComboBox();
			this.TempNiModeCheckBox = new System.Windows.Forms.CheckBox();
			this.SmartModeCheckBox = new System.Windows.Forms.CheckBox();
			this.label22 = new System.Windows.Forms.Label();
			this.label27 = new System.Windows.Forms.Label();
			this.TempTiModeCheckBox = new System.Windows.Forms.CheckBox();
			this.BypassModeCheckBox = new System.Windows.Forms.CheckBox();
			this.label23 = new System.Windows.Forms.Label();
			this.label26 = new System.Windows.Forms.Label();
			this.TempSSModeCheckBox = new System.Windows.Forms.CheckBox();
			this.PowerModeCheckBox = new System.Windows.Forms.CheckBox();
			this.label24 = new System.Windows.Forms.Label();
			this.label25 = new System.Windows.Forms.Label();
			this.TCRModeCheckBox = new System.Windows.Forms.CheckBox();
			this.ControlsTabPage = new System.Windows.Forms.TabPage();
			this.label44 = new System.Windows.Forms.Label();
			this.Clicks4ComboBox = new System.Windows.Forms.ComboBox();
			this.label43 = new System.Windows.Forms.Label();
			this.Clicks3ComboBox = new System.Windows.Forms.ComboBox();
			this.label42 = new System.Windows.Forms.Label();
			this.Clicks2ComboBox = new System.Windows.Forms.ComboBox();
			this.WakeUpByPlusMinusCheckBox = new System.Windows.Forms.CheckBox();
			this.label39 = new System.Windows.Forms.Label();
			this.StatsTabPage = new System.Windows.Forms.TabPage();
			this.label52 = new System.Windows.Forms.Label();
			this.PuffsTimeUpDown = new System.Windows.Forms.NumericUpDown();
			this.PuffsUpDown = new System.Windows.Forms.NumericUpDown();
			this.label50 = new System.Windows.Forms.Label();
			this.label51 = new System.Windows.Forms.Label();
			this.ScreenTabPage = new System.Windows.Forms.TabPage();
			this.tabControl3 = new System.Windows.Forms.TabControl();
			this.DisplayTabPage = new System.Windows.Forms.TabPage();
			this.label32 = new System.Windows.Forms.Label();
			this.BrightnessPercentLabel = new System.Windows.Forms.Label();
			this.label31 = new System.Windows.Forms.Label();
			this.label37 = new System.Windows.Forms.Label();
			this.label33 = new System.Windows.Forms.Label();
			this.StealthModeCheckBox = new System.Windows.Forms.CheckBox();
			this.IdleTimeUpDow = new System.Windows.Forms.NumericUpDown();
			this.label36 = new System.Windows.Forms.Label();
			this.BrightnessTrackBar = new System.Windows.Forms.TrackBar();
			this.FlippedModeCheckBox = new System.Windows.Forms.CheckBox();
			this.LayoutTabPage = new System.Windows.Forms.TabPage();
			this.label53 = new System.Windows.Forms.Label();
			this.UseClassicMenuCheckBox = new System.Windows.Forms.CheckBox();
			this.label45 = new System.Windows.Forms.Label();
			this.ThirdLineContentComboBox = new System.Windows.Forms.ComboBox();
			this.label38 = new System.Windows.Forms.Label();
			this.ClockTypeComboBox = new System.Windows.Forms.ComboBox();
			this.label35 = new System.Windows.Forms.Label();
			this.BatteryPercentsCheckBox = new System.Windows.Forms.CheckBox();
			this.label34 = new System.Windows.Forms.Label();
			this.ShowLogoCheckBox = new System.Windows.Forms.CheckBox();
			this.ScreensaverTabPage = new System.Windows.Forms.TabPage();
			this.ScreenProtectionTimeComboBox = new System.Windows.Forms.ComboBox();
			this.ScreensaverTypeComboBox = new System.Windows.Forms.ComboBox();
			this.label41 = new System.Windows.Forms.Label();
			this.label40 = new System.Windows.Forms.Label();
			this.DeveloperTabPage = new System.Windows.Forms.TabPage();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.ExpertTabPage = new System.Windows.Forms.TabPage();
			this.BatteryEditButton = new System.Windows.Forms.Button();
			this.label56 = new System.Windows.Forms.Label();
			this.BatteryModelComboBox = new System.Windows.Forms.ComboBox();
			this.label54 = new System.Windows.Forms.Label();
			this.ShuntCorrectionUpDown = new System.Windows.Forms.NumericUpDown();
			this.label55 = new System.Windows.Forms.Label();
			this.ToolsTabPage = new System.Windows.Forms.TabPage();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.ResetAndRestartButton = new System.Windows.Forms.Button();
			this.RestartButton = new System.Windows.Forms.Button();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.ScreenshotContainerPanel = new System.Windows.Forms.Panel();
			this.ScreenshotPictureBox = new System.Windows.Forms.PictureBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.SaveScreenshotButton = new System.Windows.Forms.Button();
			this.label46 = new System.Windows.Forms.Label();
			this.label47 = new System.Windows.Forms.Label();
			this.PixelSizeUpDown = new System.Windows.Forms.NumericUpDown();
			this.TakeScreenshotBeforeSaveCheckBox = new System.Windows.Forms.CheckBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.TakeScreenshotButton = new System.Windows.Forms.Button();
			this.BroadcastButton = new System.Windows.Forms.Button();
			this.TerminalTabPage = new System.Windows.Forms.TabPage();
			this.CommandTextBox = new System.Windows.Forms.TextBox();
			this.TraceTextBox = new System.Windows.Forms.TextBox();
			this.ComDisconnectButton = new System.Windows.Forms.Button();
			this.ComConnectButton = new System.Windows.Forms.Button();
			this.PortComboBox = new System.Windows.Forms.ComboBox();
			this.label49 = new System.Windows.Forms.Label();
			this.MainErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
			this.MainContainer.SuspendLayout();
			this.WelcomePage.SuspendLayout();
			this.WorkspacePage.SuspendLayout();
			this.MainStatusBar.SuspendLayout();
			this.groupPanel1.SuspendLayout();
			this.MainTabControl.SuspendLayout();
			this.GeneralTabPage.SuspendLayout();
			this.tabControl2.SuspendLayout();
			this.PowerTabPage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.TemperatureUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PreheatTimeUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PreheatPowerUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.TCPowerUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PowerUpDown)).BeginInit();
			this.CoilsManagerTabPage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.TCRM3UpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.TCRM2UpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ResistanceNiUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.TCRM1UpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ResistanceTiUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ResistanceSSUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ResistanceTCRUpDown)).BeginInit();
			this.ModesTabPage.SuspendLayout();
			this.ControlsTabPage.SuspendLayout();
			this.StatsTabPage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.PuffsTimeUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PuffsUpDown)).BeginInit();
			this.ScreenTabPage.SuspendLayout();
			this.tabControl3.SuspendLayout();
			this.DisplayTabPage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.IdleTimeUpDow)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.BrightnessTrackBar)).BeginInit();
			this.LayoutTabPage.SuspendLayout();
			this.ScreensaverTabPage.SuspendLayout();
			this.DeveloperTabPage.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.ExpertTabPage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ShuntCorrectionUpDown)).BeginInit();
			this.ToolsTabPage.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.ScreenshotContainerPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ScreenshotPictureBox)).BeginInit();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.PixelSizeUpDown)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.TerminalTabPage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.MainErrorProvider)).BeginInit();
			this.SuspendLayout();
			// 
			// MainContainer
			// 
			this.MainContainer.Controls.Add(this.WelcomePage);
			this.MainContainer.Controls.Add(this.WorkspacePage);
			this.MainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MainContainer.Location = new System.Drawing.Point(0, 0);
			this.MainContainer.Name = "MainContainer";
			this.MainContainer.SelectedPage = this.WorkspacePage;
			this.MainContainer.Size = new System.Drawing.Size(374, 477);
			this.MainContainer.TabIndex = 0;
			// 
			// WelcomePage
			// 
			this.WelcomePage.BackColor = System.Drawing.Color.White;
			this.WelcomePage.Controls.Add(this.WelcomeLabel);
			this.WelcomePage.Description = null;
			this.WelcomePage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.WelcomePage.Location = new System.Drawing.Point(0, 0);
			this.WelcomePage.Name = "WelcomePage";
			this.WelcomePage.Size = new System.Drawing.Size(374, 477);
			this.WelcomePage.TabIndex = 0;
			this.WelcomePage.Text = "WelcomePage";
			// 
			// WelcomeLabel
			// 
			this.WelcomeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.WelcomeLabel.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.WelcomeLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.WelcomeLabel.Location = new System.Drawing.Point(0, 0);
			this.WelcomeLabel.Name = "WelcomeLabel";
			this.WelcomeLabel.Size = new System.Drawing.Size(374, 477);
			this.WelcomeLabel.TabIndex = 0;
			this.WelcomeLabel.Text = "Waiting for device...";
			this.WelcomeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// WorkspacePage
			// 
			this.WorkspacePage.Controls.Add(this.MainStatusBar);
			this.WorkspacePage.Controls.Add(this.groupPanel1);
			this.WorkspacePage.Controls.Add(this.ResetButton);
			this.WorkspacePage.Controls.Add(this.UploadButton);
			this.WorkspacePage.Controls.Add(this.DownloadButton);
			this.WorkspacePage.Controls.Add(this.MainTabControl);
			this.WorkspacePage.Description = null;
			this.WorkspacePage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.WorkspacePage.Location = new System.Drawing.Point(0, 0);
			this.WorkspacePage.Name = "WorkspacePage";
			this.WorkspacePage.Size = new System.Drawing.Size(374, 477);
			this.WorkspacePage.TabIndex = 1;
			this.WorkspacePage.Text = "WorkspacePage";
			// 
			// MainStatusBar
			// 
			this.MainStatusBar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.MainStatusBar.GripMargin = new System.Windows.Forms.Padding(0);
			this.MainStatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel,
            this.ProgressLabel});
			this.MainStatusBar.Location = new System.Drawing.Point(0, 455);
			this.MainStatusBar.Name = "MainStatusBar";
			this.MainStatusBar.Size = new System.Drawing.Size(374, 22);
			this.MainStatusBar.SizingGrip = false;
			this.MainStatusBar.TabIndex = 35;
			this.MainStatusBar.Text = "statusStrip1";
			// 
			// StatusLabel
			// 
			this.StatusLabel.Name = "StatusLabel";
			this.StatusLabel.Size = new System.Drawing.Size(321, 17);
			this.StatusLabel.Spring = true;
			this.StatusLabel.Text = "StatusLabel";
			this.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ProgressLabel
			// 
			this.ProgressLabel.Name = "ProgressLabel";
			this.ProgressLabel.Size = new System.Drawing.Size(38, 17);
			this.ProgressLabel.Text = "Ready";
			// 
			// groupPanel1
			// 
			this.groupPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupPanel1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(185)))), ((int)(((byte)(185)))));
			this.groupPanel1.Controls.Add(this.BuildTextBox);
			this.groupPanel1.Controls.Add(this.label48);
			this.groupPanel1.Controls.Add(this.FirmwareVersionTextBox);
			this.groupPanel1.Controls.Add(this.HardwareVersionTextBox);
			this.groupPanel1.Controls.Add(this.DeviceNameLabel);
			this.groupPanel1.Controls.Add(this.label2);
			this.groupPanel1.Controls.Add(this.label5);
			this.groupPanel1.Controls.Add(this.label4);
			this.groupPanel1.Controls.Add(this.BootModeTextBox);
			this.groupPanel1.HeaderBackColor = System.Drawing.Color.White;
			this.groupPanel1.HeaderHeight = 30;
			this.groupPanel1.Location = new System.Drawing.Point(11, 41);
			this.groupPanel1.Name = "groupPanel1";
			this.groupPanel1.Size = new System.Drawing.Size(352, 112);
			this.groupPanel1.TabIndex = 34;
			this.groupPanel1.TabStop = false;
			// 
			// BuildTextBox
			// 
			this.BuildTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BuildTextBox.Location = new System.Drawing.Point(303, 33);
			this.BuildTextBox.Name = "BuildTextBox";
			this.BuildTextBox.Size = new System.Drawing.Size(45, 21);
			this.BuildTextBox.TabIndex = 34;
			this.BuildTextBox.TabStop = false;
			// 
			// label48
			// 
			this.label48.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label48.AutoSize = true;
			this.label48.Location = new System.Drawing.Point(264, 36);
			this.label48.Name = "label48";
			this.label48.Size = new System.Drawing.Size(33, 13);
			this.label48.TabIndex = 33;
			this.label48.Text = "Build:";
			// 
			// FirmwareVersionTextBox
			// 
			this.FirmwareVersionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.FirmwareVersionTextBox.Location = new System.Drawing.Point(106, 33);
			this.FirmwareVersionTextBox.Name = "FirmwareVersionTextBox";
			this.FirmwareVersionTextBox.Size = new System.Drawing.Size(152, 21);
			this.FirmwareVersionTextBox.TabIndex = 27;
			this.FirmwareVersionTextBox.TabStop = false;
			// 
			// HardwareVersionTextBox
			// 
			this.HardwareVersionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.HardwareVersionTextBox.Location = new System.Drawing.Point(106, 60);
			this.HardwareVersionTextBox.Name = "HardwareVersionTextBox";
			this.HardwareVersionTextBox.Size = new System.Drawing.Size(242, 21);
			this.HardwareVersionTextBox.TabIndex = 28;
			this.HardwareVersionTextBox.TabStop = false;
			// 
			// DeviceNameLabel
			// 
			this.DeviceNameLabel.AutoSize = true;
			this.DeviceNameLabel.BackColor = System.Drawing.Color.White;
			this.DeviceNameLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.DeviceNameLabel.Location = new System.Drawing.Point(6, 5);
			this.DeviceNameLabel.Name = "DeviceNameLabel";
			this.DeviceNameLabel.Size = new System.Drawing.Size(123, 19);
			this.DeviceNameLabel.TabIndex = 0;
			this.DeviceNameLabel.Text = "LostVape Triade";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(8, 90);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(62, 13);
			this.label2.TabIndex = 32;
			this.label2.Text = "Boot mode:";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(8, 63);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(96, 13);
			this.label5.TabIndex = 30;
			this.label5.Text = "Hardware version:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(8, 36);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(93, 13);
			this.label4.TabIndex = 29;
			this.label4.Text = "Firmware version:";
			// 
			// BootModeTextBox
			// 
			this.BootModeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.BootModeTextBox.Location = new System.Drawing.Point(106, 87);
			this.BootModeTextBox.Name = "BootModeTextBox";
			this.BootModeTextBox.Size = new System.Drawing.Size(242, 21);
			this.BootModeTextBox.TabIndex = 31;
			this.BootModeTextBox.TabStop = false;
			// 
			// ResetButton
			// 
			this.ResetButton.Image = global::NFirmwareEditor.Properties.Resources.reset_settings;
			this.ResetButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.ResetButton.Location = new System.Drawing.Point(257, 10);
			this.ResetButton.Name = "ResetButton";
			this.ResetButton.Size = new System.Drawing.Size(107, 25);
			this.ResetButton.TabIndex = 3;
			this.ResetButton.Text = "Reset settings";
			this.ResetButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.ResetButton.UseVisualStyleBackColor = true;
			// 
			// UploadButton
			// 
			this.UploadButton.Image = global::NFirmwareEditor.Properties.Resources.upload_settings;
			this.UploadButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.UploadButton.Location = new System.Drawing.Point(141, 10);
			this.UploadButton.Name = "UploadButton";
			this.UploadButton.Size = new System.Drawing.Size(110, 25);
			this.UploadButton.TabIndex = 2;
			this.UploadButton.Text = "Upload settings";
			this.UploadButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.UploadButton.UseVisualStyleBackColor = true;
			// 
			// DownloadButton
			// 
			this.DownloadButton.Image = global::NFirmwareEditor.Properties.Resources.download_settings;
			this.DownloadButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.DownloadButton.Location = new System.Drawing.Point(11, 10);
			this.DownloadButton.Name = "DownloadButton";
			this.DownloadButton.Size = new System.Drawing.Size(124, 25);
			this.DownloadButton.TabIndex = 1;
			this.DownloadButton.Text = "Download settings";
			this.DownloadButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.DownloadButton.UseVisualStyleBackColor = true;
			// 
			// MainTabControl
			// 
			this.MainTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.MainTabControl.Controls.Add(this.GeneralTabPage);
			this.MainTabControl.Controls.Add(this.ScreenTabPage);
			this.MainTabControl.Controls.Add(this.DeveloperTabPage);
			this.MainTabControl.Location = new System.Drawing.Point(11, 159);
			this.MainTabControl.Name = "MainTabControl";
			this.MainTabControl.SelectedIndex = 0;
			this.MainTabControl.Size = new System.Drawing.Size(354, 292);
			this.MainTabControl.TabIndex = 0;
			// 
			// GeneralTabPage
			// 
			this.GeneralTabPage.Controls.Add(this.tabControl2);
			this.GeneralTabPage.Location = new System.Drawing.Point(4, 22);
			this.GeneralTabPage.Name = "GeneralTabPage";
			this.GeneralTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.GeneralTabPage.Size = new System.Drawing.Size(346, 266);
			this.GeneralTabPage.TabIndex = 0;
			this.GeneralTabPage.Text = "General";
			this.GeneralTabPage.UseVisualStyleBackColor = true;
			// 
			// tabControl2
			// 
			this.tabControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl2.Controls.Add(this.PowerTabPage);
			this.tabControl2.Controls.Add(this.CoilsManagerTabPage);
			this.tabControl2.Controls.Add(this.ModesTabPage);
			this.tabControl2.Controls.Add(this.ControlsTabPage);
			this.tabControl2.Controls.Add(this.StatsTabPage);
			this.tabControl2.Location = new System.Drawing.Point(6, 6);
			this.tabControl2.Name = "tabControl2";
			this.tabControl2.SelectedIndex = 0;
			this.tabControl2.Size = new System.Drawing.Size(336, 254);
			this.tabControl2.TabIndex = 0;
			// 
			// PowerTabPage
			// 
			this.PowerTabPage.Controls.Add(this.TemperatureUpDown);
			this.PowerTabPage.Controls.Add(this.PreheatTimeUpDown);
			this.PowerTabPage.Controls.Add(this.PreheatPowerUpDown);
			this.PowerTabPage.Controls.Add(this.TCPowerUpDown);
			this.PowerTabPage.Controls.Add(this.PowerUpDown);
			this.PowerTabPage.Controls.Add(this.Step1WCheckBox);
			this.PowerTabPage.Controls.Add(this.label1);
			this.PowerTabPage.Controls.Add(this.TemperatureDominantCheckBox);
			this.PowerTabPage.Controls.Add(this.label30);
			this.PowerTabPage.Controls.Add(this.label29);
			this.PowerTabPage.Controls.Add(this.label3);
			this.PowerTabPage.Controls.Add(this.PreheatTypeComboBox);
			this.PowerTabPage.Controls.Add(this.label6);
			this.PowerTabPage.Controls.Add(this.label28);
			this.PowerTabPage.Controls.Add(this.TemperatureTypeComboBox);
			this.PowerTabPage.Controls.Add(this.label11);
			this.PowerTabPage.Controls.Add(this.label12);
			this.PowerTabPage.Location = new System.Drawing.Point(4, 22);
			this.PowerTabPage.Name = "PowerTabPage";
			this.PowerTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.PowerTabPage.Size = new System.Drawing.Size(328, 228);
			this.PowerTabPage.TabIndex = 0;
			this.PowerTabPage.Text = "Power & Temp";
			this.PowerTabPage.UseVisualStyleBackColor = true;
			// 
			// TemperatureUpDown
			// 
			this.TemperatureUpDown.Location = new System.Drawing.Point(101, 89);
			this.TemperatureUpDown.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
			this.TemperatureUpDown.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.TemperatureUpDown.Name = "TemperatureUpDown";
			this.TemperatureUpDown.Size = new System.Drawing.Size(106, 21);
			this.TemperatureUpDown.TabIndex = 67;
			this.TemperatureUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.TemperatureUpDown.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
			// 
			// PreheatTimeUpDown
			// 
			this.PreheatTimeUpDown.DecimalPlaces = 1;
			this.PreheatTimeUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
			this.PreheatTimeUpDown.Location = new System.Drawing.Point(101, 170);
			this.PreheatTimeUpDown.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
			this.PreheatTimeUpDown.Name = "PreheatTimeUpDown";
			this.PreheatTimeUpDown.Size = new System.Drawing.Size(106, 21);
			this.PreheatTimeUpDown.TabIndex = 66;
			this.PreheatTimeUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// PreheatPowerUpDown
			// 
			this.PreheatPowerUpDown.DecimalPlaces = 1;
			this.PreheatPowerUpDown.Location = new System.Drawing.Point(101, 143);
			this.PreheatPowerUpDown.Maximum = new decimal(new int[] {
            75,
            0,
            0,
            0});
			this.PreheatPowerUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.PreheatPowerUpDown.Name = "PreheatPowerUpDown";
			this.PreheatPowerUpDown.Size = new System.Drawing.Size(106, 21);
			this.PreheatPowerUpDown.TabIndex = 65;
			this.PreheatPowerUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.PreheatPowerUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// TCPowerUpDown
			// 
			this.TCPowerUpDown.DecimalPlaces = 1;
			this.TCPowerUpDown.Location = new System.Drawing.Point(101, 35);
			this.TCPowerUpDown.Maximum = new decimal(new int[] {
            75,
            0,
            0,
            0});
			this.TCPowerUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.TCPowerUpDown.Name = "TCPowerUpDown";
			this.TCPowerUpDown.Size = new System.Drawing.Size(106, 21);
			this.TCPowerUpDown.TabIndex = 64;
			this.TCPowerUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.TCPowerUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// PowerUpDown
			// 
			this.PowerUpDown.DecimalPlaces = 1;
			this.PowerUpDown.Location = new System.Drawing.Point(101, 8);
			this.PowerUpDown.Maximum = new decimal(new int[] {
            75,
            0,
            0,
            0});
			this.PowerUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.PowerUpDown.Name = "PowerUpDown";
			this.PowerUpDown.Size = new System.Drawing.Size(106, 21);
			this.PowerUpDown.TabIndex = 63;
			this.PowerUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.PowerUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// Step1WCheckBox
			// 
			this.Step1WCheckBox.AutoSize = true;
			this.Step1WCheckBox.Location = new System.Drawing.Point(101, 62);
			this.Step1WCheckBox.Name = "Step1WCheckBox";
			this.Step1WCheckBox.Size = new System.Drawing.Size(67, 17);
			this.Step1WCheckBox.TabIndex = 62;
			this.Step1WCheckBox.Text = "Step 1W";
			this.Step1WCheckBox.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(4, 11);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 13);
			this.label1.TabIndex = 35;
			this.label1.Text = "Power:";
			// 
			// TemperatureDominantCheckBox
			// 
			this.TemperatureDominantCheckBox.AutoSize = true;
			this.TemperatureDominantCheckBox.Location = new System.Drawing.Point(101, 116);
			this.TemperatureDominantCheckBox.Name = "TemperatureDominantCheckBox";
			this.TemperatureDominantCheckBox.Size = new System.Drawing.Size(136, 17);
			this.TemperatureDominantCheckBox.TabIndex = 61;
			this.TemperatureDominantCheckBox.Text = "Temperature Dominant";
			this.TemperatureDominantCheckBox.UseVisualStyleBackColor = true;
			// 
			// label30
			// 
			this.label30.AutoSize = true;
			this.label30.Location = new System.Drawing.Point(213, 173);
			this.label30.Name = "label30";
			this.label30.Size = new System.Drawing.Size(12, 13);
			this.label30.TabIndex = 60;
			this.label30.Text = "s";
			// 
			// label29
			// 
			this.label29.AutoSize = true;
			this.label29.Location = new System.Drawing.Point(4, 173);
			this.label29.Name = "label29";
			this.label29.Size = new System.Drawing.Size(74, 13);
			this.label29.TabIndex = 59;
			this.label29.Text = "Preheat Time:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(4, 38);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(57, 13);
			this.label3.TabIndex = 37;
			this.label3.Text = "TC Power:";
			// 
			// PreheatTypeComboBox
			// 
			this.PreheatTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.PreheatTypeComboBox.FormattingEnabled = true;
			this.PreheatTypeComboBox.Items.AddRange(new object[] {
            "W",
            "%"});
			this.PreheatTypeComboBox.Location = new System.Drawing.Point(213, 143);
			this.PreheatTypeComboBox.Name = "PreheatTypeComboBox";
			this.PreheatTypeComboBox.Size = new System.Drawing.Size(42, 21);
			this.PreheatTypeComboBox.TabIndex = 57;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(4, 92);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(73, 13);
			this.label6.TabIndex = 39;
			this.label6.Text = "Temperature:";
			// 
			// label28
			// 
			this.label28.AutoSize = true;
			this.label28.Location = new System.Drawing.Point(4, 146);
			this.label28.Name = "label28";
			this.label28.Size = new System.Drawing.Size(82, 13);
			this.label28.TabIndex = 56;
			this.label28.Text = "Preheat Power:";
			// 
			// TemperatureTypeComboBox
			// 
			this.TemperatureTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.TemperatureTypeComboBox.FormattingEnabled = true;
			this.TemperatureTypeComboBox.Items.AddRange(new object[] {
            "°C",
            "°F"});
			this.TemperatureTypeComboBox.Location = new System.Drawing.Point(213, 89);
			this.TemperatureTypeComboBox.Name = "TemperatureTypeComboBox";
			this.TemperatureTypeComboBox.Size = new System.Drawing.Size(42, 21);
			this.TemperatureTypeComboBox.TabIndex = 40;
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(213, 11);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(17, 13);
			this.label11.TabIndex = 51;
			this.label11.Text = "W";
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(213, 38);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(17, 13);
			this.label12.TabIndex = 52;
			this.label12.Text = "W";
			// 
			// CoilsManagerTabPage
			// 
			this.CoilsManagerTabPage.Controls.Add(this.label19);
			this.CoilsManagerTabPage.Controls.Add(this.label16);
			this.CoilsManagerTabPage.Controls.Add(this.TCRM3UpDown);
			this.CoilsManagerTabPage.Controls.Add(this.label7);
			this.CoilsManagerTabPage.Controls.Add(this.label18);
			this.CoilsManagerTabPage.Controls.Add(this.label15);
			this.CoilsManagerTabPage.Controls.Add(this.TCRM2UpDown);
			this.CoilsManagerTabPage.Controls.Add(this.ResistanceNiUpDown);
			this.CoilsManagerTabPage.Controls.Add(this.label17);
			this.CoilsManagerTabPage.Controls.Add(this.TCRM1UpDown);
			this.CoilsManagerTabPage.Controls.Add(this.label14);
			this.CoilsManagerTabPage.Controls.Add(this.ResistanceTiUpDown);
			this.CoilsManagerTabPage.Controls.Add(this.label13);
			this.CoilsManagerTabPage.Controls.Add(this.label8);
			this.CoilsManagerTabPage.Controls.Add(this.ResistanceTCRCheckBox);
			this.CoilsManagerTabPage.Controls.Add(this.ResistanceSSUpDown);
			this.CoilsManagerTabPage.Controls.Add(this.ResistanceSSCheckBox);
			this.CoilsManagerTabPage.Controls.Add(this.label9);
			this.CoilsManagerTabPage.Controls.Add(this.ResistanceTiCheckBox);
			this.CoilsManagerTabPage.Controls.Add(this.ResistanceTCRUpDown);
			this.CoilsManagerTabPage.Controls.Add(this.ResistanceNiCheckBox);
			this.CoilsManagerTabPage.Controls.Add(this.label10);
			this.CoilsManagerTabPage.Location = new System.Drawing.Point(4, 22);
			this.CoilsManagerTabPage.Name = "CoilsManagerTabPage";
			this.CoilsManagerTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.CoilsManagerTabPage.Size = new System.Drawing.Size(328, 228);
			this.CoilsManagerTabPage.TabIndex = 1;
			this.CoilsManagerTabPage.Text = "Coils Manager";
			this.CoilsManagerTabPage.UseVisualStyleBackColor = true;
			// 
			// label19
			// 
			this.label19.AutoSize = true;
			this.label19.Location = new System.Drawing.Point(4, 200);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(48, 13);
			this.label19.TabIndex = 41;
			this.label19.Text = "TCR M3:";
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label16.Location = new System.Drawing.Point(213, 91);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(17, 16);
			this.label16.TabIndex = 68;
			this.label16.Text = "Ω";
			// 
			// TCRM3UpDown
			// 
			this.TCRM3UpDown.Location = new System.Drawing.Point(101, 197);
			this.TCRM3UpDown.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
			this.TCRM3UpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.TCRM3UpDown.Name = "TCRM3UpDown";
			this.TCRM3UpDown.Size = new System.Drawing.Size(106, 21);
			this.TCRM3UpDown.TabIndex = 40;
			this.TCRM3UpDown.TabStop = false;
			this.TCRM3UpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.TCRM3UpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(4, 11);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(75, 13);
			this.label7.TabIndex = 56;
			this.label7.Text = "Resistance Ni:";
			// 
			// label18
			// 
			this.label18.AutoSize = true;
			this.label18.Location = new System.Drawing.Point(4, 173);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(48, 13);
			this.label18.TabIndex = 39;
			this.label18.Text = "TCR M2:";
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label15.Location = new System.Drawing.Point(213, 64);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(17, 16);
			this.label15.TabIndex = 69;
			this.label15.Text = "Ω";
			// 
			// TCRM2UpDown
			// 
			this.TCRM2UpDown.Location = new System.Drawing.Point(101, 170);
			this.TCRM2UpDown.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
			this.TCRM2UpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.TCRM2UpDown.Name = "TCRM2UpDown";
			this.TCRM2UpDown.Size = new System.Drawing.Size(106, 21);
			this.TCRM2UpDown.TabIndex = 38;
			this.TCRM2UpDown.TabStop = false;
			this.TCRM2UpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.TCRM2UpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// ResistanceNiUpDown
			// 
			this.ResistanceNiUpDown.DecimalPlaces = 3;
			this.ResistanceNiUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
			this.ResistanceNiUpDown.Location = new System.Drawing.Point(101, 8);
			this.ResistanceNiUpDown.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
			this.ResistanceNiUpDown.Name = "ResistanceNiUpDown";
			this.ResistanceNiUpDown.Size = new System.Drawing.Size(106, 21);
			this.ResistanceNiUpDown.TabIndex = 55;
			this.ResistanceNiUpDown.TabStop = false;
			this.ResistanceNiUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.ResistanceNiUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            131072});
			// 
			// label17
			// 
			this.label17.AutoSize = true;
			this.label17.Location = new System.Drawing.Point(4, 146);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(48, 13);
			this.label17.TabIndex = 37;
			this.label17.Text = "TCR M1:";
			// 
			// TCRM1UpDown
			// 
			this.TCRM1UpDown.Location = new System.Drawing.Point(101, 143);
			this.TCRM1UpDown.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
			this.TCRM1UpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.TCRM1UpDown.Name = "TCRM1UpDown";
			this.TCRM1UpDown.Size = new System.Drawing.Size(106, 21);
			this.TCRM1UpDown.TabIndex = 36;
			this.TCRM1UpDown.TabStop = false;
			this.TCRM1UpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.TCRM1UpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label14.Location = new System.Drawing.Point(213, 37);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(17, 16);
			this.label14.TabIndex = 70;
			this.label14.Text = "Ω";
			// 
			// ResistanceTiUpDown
			// 
			this.ResistanceTiUpDown.DecimalPlaces = 3;
			this.ResistanceTiUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
			this.ResistanceTiUpDown.Location = new System.Drawing.Point(101, 35);
			this.ResistanceTiUpDown.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
			this.ResistanceTiUpDown.Name = "ResistanceTiUpDown";
			this.ResistanceTiUpDown.Size = new System.Drawing.Size(106, 21);
			this.ResistanceTiUpDown.TabIndex = 57;
			this.ResistanceTiUpDown.TabStop = false;
			this.ResistanceTiUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.ResistanceTiUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            131072});
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label13.Location = new System.Drawing.Point(213, 10);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(17, 16);
			this.label13.TabIndex = 67;
			this.label13.Text = "Ω";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(4, 38);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(74, 13);
			this.label8.TabIndex = 58;
			this.label8.Text = "Resistance Ti:";
			// 
			// ResistanceTCRCheckBox
			// 
			this.ResistanceTCRCheckBox.AutoSize = true;
			this.ResistanceTCRCheckBox.Location = new System.Drawing.Point(242, 92);
			this.ResistanceTCRCheckBox.Name = "ResistanceTCRCheckBox";
			this.ResistanceTCRCheckBox.Size = new System.Drawing.Size(59, 17);
			this.ResistanceTCRCheckBox.TabIndex = 64;
			this.ResistanceTCRCheckBox.Text = "Locked";
			this.ResistanceTCRCheckBox.UseVisualStyleBackColor = true;
			// 
			// ResistanceSSUpDown
			// 
			this.ResistanceSSUpDown.DecimalPlaces = 3;
			this.ResistanceSSUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
			this.ResistanceSSUpDown.Location = new System.Drawing.Point(101, 62);
			this.ResistanceSSUpDown.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
			this.ResistanceSSUpDown.Name = "ResistanceSSUpDown";
			this.ResistanceSSUpDown.Size = new System.Drawing.Size(106, 21);
			this.ResistanceSSUpDown.TabIndex = 59;
			this.ResistanceSSUpDown.TabStop = false;
			this.ResistanceSSUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.ResistanceSSUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            131072});
			// 
			// ResistanceSSCheckBox
			// 
			this.ResistanceSSCheckBox.AutoSize = true;
			this.ResistanceSSCheckBox.Location = new System.Drawing.Point(242, 65);
			this.ResistanceSSCheckBox.Name = "ResistanceSSCheckBox";
			this.ResistanceSSCheckBox.Size = new System.Drawing.Size(59, 17);
			this.ResistanceSSCheckBox.TabIndex = 65;
			this.ResistanceSSCheckBox.Text = "Locked";
			this.ResistanceSSCheckBox.UseVisualStyleBackColor = true;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(4, 65);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(78, 13);
			this.label9.TabIndex = 60;
			this.label9.Text = "Resistance SS:";
			// 
			// ResistanceTiCheckBox
			// 
			this.ResistanceTiCheckBox.AutoSize = true;
			this.ResistanceTiCheckBox.Location = new System.Drawing.Point(242, 38);
			this.ResistanceTiCheckBox.Name = "ResistanceTiCheckBox";
			this.ResistanceTiCheckBox.Size = new System.Drawing.Size(59, 17);
			this.ResistanceTiCheckBox.TabIndex = 66;
			this.ResistanceTiCheckBox.Text = "Locked";
			this.ResistanceTiCheckBox.UseVisualStyleBackColor = true;
			// 
			// ResistanceTCRUpDown
			// 
			this.ResistanceTCRUpDown.DecimalPlaces = 3;
			this.ResistanceTCRUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
			this.ResistanceTCRUpDown.Location = new System.Drawing.Point(101, 89);
			this.ResistanceTCRUpDown.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
			this.ResistanceTCRUpDown.Name = "ResistanceTCRUpDown";
			this.ResistanceTCRUpDown.Size = new System.Drawing.Size(106, 21);
			this.ResistanceTCRUpDown.TabIndex = 61;
			this.ResistanceTCRUpDown.TabStop = false;
			this.ResistanceTCRUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.ResistanceTCRUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            131072});
			// 
			// ResistanceNiCheckBox
			// 
			this.ResistanceNiCheckBox.AutoSize = true;
			this.ResistanceNiCheckBox.Location = new System.Drawing.Point(242, 11);
			this.ResistanceNiCheckBox.Name = "ResistanceNiCheckBox";
			this.ResistanceNiCheckBox.Size = new System.Drawing.Size(59, 17);
			this.ResistanceNiCheckBox.TabIndex = 63;
			this.ResistanceNiCheckBox.Text = "Locked";
			this.ResistanceNiCheckBox.UseVisualStyleBackColor = true;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(4, 92);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(86, 13);
			this.label10.TabIndex = 62;
			this.label10.Text = "Resistance TCR:";
			// 
			// ModesTabPage
			// 
			this.ModesTabPage.Controls.Add(this.TCRIndexLabel);
			this.ModesTabPage.Controls.Add(this.label21);
			this.ModesTabPage.Controls.Add(this.SelectedTCRComboBox);
			this.ModesTabPage.Controls.Add(this.label20);
			this.ModesTabPage.Controls.Add(this.SelectedModeComboBox);
			this.ModesTabPage.Controls.Add(this.TempNiModeCheckBox);
			this.ModesTabPage.Controls.Add(this.SmartModeCheckBox);
			this.ModesTabPage.Controls.Add(this.label22);
			this.ModesTabPage.Controls.Add(this.label27);
			this.ModesTabPage.Controls.Add(this.TempTiModeCheckBox);
			this.ModesTabPage.Controls.Add(this.BypassModeCheckBox);
			this.ModesTabPage.Controls.Add(this.label23);
			this.ModesTabPage.Controls.Add(this.label26);
			this.ModesTabPage.Controls.Add(this.TempSSModeCheckBox);
			this.ModesTabPage.Controls.Add(this.PowerModeCheckBox);
			this.ModesTabPage.Controls.Add(this.label24);
			this.ModesTabPage.Controls.Add(this.label25);
			this.ModesTabPage.Controls.Add(this.TCRModeCheckBox);
			this.ModesTabPage.Location = new System.Drawing.Point(4, 22);
			this.ModesTabPage.Name = "ModesTabPage";
			this.ModesTabPage.Size = new System.Drawing.Size(328, 228);
			this.ModesTabPage.TabIndex = 2;
			this.ModesTabPage.Text = "Modes";
			this.ModesTabPage.UseVisualStyleBackColor = true;
			// 
			// TCRIndexLabel
			// 
			this.TCRIndexLabel.AutoSize = true;
			this.TCRIndexLabel.Location = new System.Drawing.Point(225, 11);
			this.TCRIndexLabel.Name = "TCRIndexLabel";
			this.TCRIndexLabel.Size = new System.Drawing.Size(31, 13);
			this.TCRIndexLabel.TabIndex = 70;
			this.TCRIndexLabel.Text = "TCR:";
			// 
			// label21
			// 
			this.label21.AutoSize = true;
			this.label21.Location = new System.Drawing.Point(4, 11);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(81, 13);
			this.label21.TabIndex = 53;
			this.label21.Text = "Selected Mode:";
			// 
			// SelectedTCRComboBox
			// 
			this.SelectedTCRComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.SelectedTCRComboBox.FormattingEnabled = true;
			this.SelectedTCRComboBox.Items.AddRange(new object[] {
            "M1",
            "M2",
            "M3"});
			this.SelectedTCRComboBox.Location = new System.Drawing.Point(258, 8);
			this.SelectedTCRComboBox.Name = "SelectedTCRComboBox";
			this.SelectedTCRComboBox.Size = new System.Drawing.Size(55, 21);
			this.SelectedTCRComboBox.TabIndex = 69;
			// 
			// label20
			// 
			this.label20.AutoSize = true;
			this.label20.Location = new System.Drawing.Point(4, 119);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(85, 13);
			this.label20.TabIndex = 54;
			this.label20.Text = "Temperature Ni:";
			// 
			// SelectedModeComboBox
			// 
			this.SelectedModeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.SelectedModeComboBox.FormattingEnabled = true;
			this.SelectedModeComboBox.Items.AddRange(new object[] {
            "°C",
            "°F"});
			this.SelectedModeComboBox.Location = new System.Drawing.Point(101, 8);
			this.SelectedModeComboBox.Name = "SelectedModeComboBox";
			this.SelectedModeComboBox.Size = new System.Drawing.Size(120, 21);
			this.SelectedModeComboBox.TabIndex = 68;
			// 
			// TempNiModeCheckBox
			// 
			this.TempNiModeCheckBox.AutoSize = true;
			this.TempNiModeCheckBox.Location = new System.Drawing.Point(101, 119);
			this.TempNiModeCheckBox.Name = "TempNiModeCheckBox";
			this.TempNiModeCheckBox.Size = new System.Drawing.Size(64, 17);
			this.TempNiModeCheckBox.TabIndex = 55;
			this.TempNiModeCheckBox.Text = "Enabled";
			this.TempNiModeCheckBox.UseVisualStyleBackColor = true;
			// 
			// SmartModeCheckBox
			// 
			this.SmartModeCheckBox.AutoSize = true;
			this.SmartModeCheckBox.Location = new System.Drawing.Point(101, 92);
			this.SmartModeCheckBox.Name = "SmartModeCheckBox";
			this.SmartModeCheckBox.Size = new System.Drawing.Size(64, 17);
			this.SmartModeCheckBox.TabIndex = 67;
			this.SmartModeCheckBox.Text = "Enabled";
			this.SmartModeCheckBox.UseVisualStyleBackColor = true;
			// 
			// label22
			// 
			this.label22.AutoSize = true;
			this.label22.Location = new System.Drawing.Point(4, 146);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(84, 13);
			this.label22.TabIndex = 56;
			this.label22.Text = "Temperature Ti:";
			// 
			// label27
			// 
			this.label27.AutoSize = true;
			this.label27.Location = new System.Drawing.Point(4, 92);
			this.label27.Name = "label27";
			this.label27.Size = new System.Drawing.Size(73, 13);
			this.label27.TabIndex = 66;
			this.label27.Text = "Smart / Start:";
			// 
			// TempTiModeCheckBox
			// 
			this.TempTiModeCheckBox.AutoSize = true;
			this.TempTiModeCheckBox.Location = new System.Drawing.Point(101, 146);
			this.TempTiModeCheckBox.Name = "TempTiModeCheckBox";
			this.TempTiModeCheckBox.Size = new System.Drawing.Size(64, 17);
			this.TempTiModeCheckBox.TabIndex = 57;
			this.TempTiModeCheckBox.Text = "Enabled";
			this.TempTiModeCheckBox.UseVisualStyleBackColor = true;
			// 
			// BypassModeCheckBox
			// 
			this.BypassModeCheckBox.AutoSize = true;
			this.BypassModeCheckBox.Location = new System.Drawing.Point(101, 65);
			this.BypassModeCheckBox.Name = "BypassModeCheckBox";
			this.BypassModeCheckBox.Size = new System.Drawing.Size(64, 17);
			this.BypassModeCheckBox.TabIndex = 65;
			this.BypassModeCheckBox.Text = "Enabled";
			this.BypassModeCheckBox.UseVisualStyleBackColor = true;
			// 
			// label23
			// 
			this.label23.AutoSize = true;
			this.label23.Location = new System.Drawing.Point(4, 173);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(88, 13);
			this.label23.TabIndex = 58;
			this.label23.Text = "Temperature SS:";
			// 
			// label26
			// 
			this.label26.AutoSize = true;
			this.label26.Location = new System.Drawing.Point(4, 65);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(45, 13);
			this.label26.TabIndex = 64;
			this.label26.Text = "Bypass:";
			// 
			// TempSSModeCheckBox
			// 
			this.TempSSModeCheckBox.AutoSize = true;
			this.TempSSModeCheckBox.Location = new System.Drawing.Point(101, 173);
			this.TempSSModeCheckBox.Name = "TempSSModeCheckBox";
			this.TempSSModeCheckBox.Size = new System.Drawing.Size(64, 17);
			this.TempSSModeCheckBox.TabIndex = 59;
			this.TempSSModeCheckBox.Text = "Enabled";
			this.TempSSModeCheckBox.UseVisualStyleBackColor = true;
			// 
			// PowerModeCheckBox
			// 
			this.PowerModeCheckBox.AutoSize = true;
			this.PowerModeCheckBox.Location = new System.Drawing.Point(101, 38);
			this.PowerModeCheckBox.Name = "PowerModeCheckBox";
			this.PowerModeCheckBox.Size = new System.Drawing.Size(64, 17);
			this.PowerModeCheckBox.TabIndex = 63;
			this.PowerModeCheckBox.Text = "Enabled";
			this.PowerModeCheckBox.UseVisualStyleBackColor = true;
			// 
			// label24
			// 
			this.label24.AutoSize = true;
			this.label24.Location = new System.Drawing.Point(4, 200);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(96, 13);
			this.label24.TabIndex = 60;
			this.label24.Text = "Temperature TCR:";
			// 
			// label25
			// 
			this.label25.AutoSize = true;
			this.label25.Location = new System.Drawing.Point(4, 38);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(41, 13);
			this.label25.TabIndex = 62;
			this.label25.Text = "Power:";
			// 
			// TCRModeCheckBox
			// 
			this.TCRModeCheckBox.AutoSize = true;
			this.TCRModeCheckBox.Location = new System.Drawing.Point(101, 200);
			this.TCRModeCheckBox.Name = "TCRModeCheckBox";
			this.TCRModeCheckBox.Size = new System.Drawing.Size(64, 17);
			this.TCRModeCheckBox.TabIndex = 61;
			this.TCRModeCheckBox.Text = "Enabled";
			this.TCRModeCheckBox.UseVisualStyleBackColor = true;
			// 
			// ControlsTabPage
			// 
			this.ControlsTabPage.Controls.Add(this.label44);
			this.ControlsTabPage.Controls.Add(this.Clicks4ComboBox);
			this.ControlsTabPage.Controls.Add(this.label43);
			this.ControlsTabPage.Controls.Add(this.Clicks3ComboBox);
			this.ControlsTabPage.Controls.Add(this.label42);
			this.ControlsTabPage.Controls.Add(this.Clicks2ComboBox);
			this.ControlsTabPage.Controls.Add(this.WakeUpByPlusMinusCheckBox);
			this.ControlsTabPage.Controls.Add(this.label39);
			this.ControlsTabPage.Location = new System.Drawing.Point(4, 22);
			this.ControlsTabPage.Name = "ControlsTabPage";
			this.ControlsTabPage.Size = new System.Drawing.Size(328, 228);
			this.ControlsTabPage.TabIndex = 3;
			this.ControlsTabPage.Text = "Controls";
			this.ControlsTabPage.UseVisualStyleBackColor = true;
			// 
			// label44
			// 
			this.label44.AutoSize = true;
			this.label44.Location = new System.Drawing.Point(4, 65);
			this.label44.Name = "label44";
			this.label44.Size = new System.Drawing.Size(46, 13);
			this.label44.TabIndex = 86;
			this.label44.Text = "4 Clicks:";
			// 
			// Clicks4ComboBox
			// 
			this.Clicks4ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Clicks4ComboBox.FormattingEnabled = true;
			this.Clicks4ComboBox.Items.AddRange(new object[] {
            "°C",
            "°F"});
			this.Clicks4ComboBox.Location = new System.Drawing.Point(101, 62);
			this.Clicks4ComboBox.Name = "Clicks4ComboBox";
			this.Clicks4ComboBox.Size = new System.Drawing.Size(140, 21);
			this.Clicks4ComboBox.TabIndex = 87;
			// 
			// label43
			// 
			this.label43.AutoSize = true;
			this.label43.Location = new System.Drawing.Point(4, 38);
			this.label43.Name = "label43";
			this.label43.Size = new System.Drawing.Size(46, 13);
			this.label43.TabIndex = 84;
			this.label43.Text = "3 Clicks:";
			// 
			// Clicks3ComboBox
			// 
			this.Clicks3ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Clicks3ComboBox.FormattingEnabled = true;
			this.Clicks3ComboBox.Items.AddRange(new object[] {
            "°C",
            "°F"});
			this.Clicks3ComboBox.Location = new System.Drawing.Point(101, 35);
			this.Clicks3ComboBox.Name = "Clicks3ComboBox";
			this.Clicks3ComboBox.Size = new System.Drawing.Size(140, 21);
			this.Clicks3ComboBox.TabIndex = 85;
			// 
			// label42
			// 
			this.label42.AutoSize = true;
			this.label42.Location = new System.Drawing.Point(4, 11);
			this.label42.Name = "label42";
			this.label42.Size = new System.Drawing.Size(46, 13);
			this.label42.TabIndex = 82;
			this.label42.Text = "2 Clicks:";
			// 
			// Clicks2ComboBox
			// 
			this.Clicks2ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Clicks2ComboBox.FormattingEnabled = true;
			this.Clicks2ComboBox.Items.AddRange(new object[] {
            "°C",
            "°F"});
			this.Clicks2ComboBox.Location = new System.Drawing.Point(101, 8);
			this.Clicks2ComboBox.Name = "Clicks2ComboBox";
			this.Clicks2ComboBox.Size = new System.Drawing.Size(140, 21);
			this.Clicks2ComboBox.TabIndex = 83;
			// 
			// WakeUpByPlusMinusCheckBox
			// 
			this.WakeUpByPlusMinusCheckBox.AutoSize = true;
			this.WakeUpByPlusMinusCheckBox.Location = new System.Drawing.Point(101, 92);
			this.WakeUpByPlusMinusCheckBox.Name = "WakeUpByPlusMinusCheckBox";
			this.WakeUpByPlusMinusCheckBox.Size = new System.Drawing.Size(64, 17);
			this.WakeUpByPlusMinusCheckBox.TabIndex = 81;
			this.WakeUpByPlusMinusCheckBox.Text = "Enabled";
			this.WakeUpByPlusMinusCheckBox.UseVisualStyleBackColor = true;
			// 
			// label39
			// 
			this.label39.AutoSize = true;
			this.label39.Location = new System.Drawing.Point(4, 92);
			this.label39.Name = "label39";
			this.label39.Size = new System.Drawing.Size(96, 13);
			this.label39.TabIndex = 80;
			this.label39.Text = "Wake up by + / - :";
			// 
			// StatsTabPage
			// 
			this.StatsTabPage.Controls.Add(this.label52);
			this.StatsTabPage.Controls.Add(this.PuffsTimeUpDown);
			this.StatsTabPage.Controls.Add(this.PuffsUpDown);
			this.StatsTabPage.Controls.Add(this.label50);
			this.StatsTabPage.Controls.Add(this.label51);
			this.StatsTabPage.Location = new System.Drawing.Point(4, 22);
			this.StatsTabPage.Name = "StatsTabPage";
			this.StatsTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.StatsTabPage.Size = new System.Drawing.Size(328, 228);
			this.StatsTabPage.TabIndex = 4;
			this.StatsTabPage.Text = "Stats";
			this.StatsTabPage.UseVisualStyleBackColor = true;
			// 
			// label52
			// 
			this.label52.AutoSize = true;
			this.label52.Location = new System.Drawing.Point(214, 38);
			this.label52.Name = "label52";
			this.label52.Size = new System.Drawing.Size(12, 13);
			this.label52.TabIndex = 69;
			this.label52.Text = "s";
			// 
			// PuffsTimeUpDown
			// 
			this.PuffsTimeUpDown.DecimalPlaces = 1;
			this.PuffsTimeUpDown.Location = new System.Drawing.Point(101, 35);
			this.PuffsTimeUpDown.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
			this.PuffsTimeUpDown.Name = "PuffsTimeUpDown";
			this.PuffsTimeUpDown.Size = new System.Drawing.Size(106, 21);
			this.PuffsTimeUpDown.TabIndex = 68;
			this.PuffsTimeUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// PuffsUpDown
			// 
			this.PuffsUpDown.Location = new System.Drawing.Point(101, 8);
			this.PuffsUpDown.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
			this.PuffsUpDown.Name = "PuffsUpDown";
			this.PuffsUpDown.Size = new System.Drawing.Size(106, 21);
			this.PuffsUpDown.TabIndex = 67;
			this.PuffsUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label50
			// 
			this.label50.AutoSize = true;
			this.label50.Location = new System.Drawing.Point(4, 11);
			this.label50.Name = "label50";
			this.label50.Size = new System.Drawing.Size(68, 13);
			this.label50.TabIndex = 65;
			this.label50.Text = "Puffs Count:";
			// 
			// label51
			// 
			this.label51.AutoSize = true;
			this.label51.Location = new System.Drawing.Point(4, 38);
			this.label51.Name = "label51";
			this.label51.Size = new System.Drawing.Size(88, 13);
			this.label51.TabIndex = 66;
			this.label51.Text = "Total Puffs Time:";
			// 
			// ScreenTabPage
			// 
			this.ScreenTabPage.Controls.Add(this.tabControl3);
			this.ScreenTabPage.Location = new System.Drawing.Point(4, 22);
			this.ScreenTabPage.Name = "ScreenTabPage";
			this.ScreenTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.ScreenTabPage.Size = new System.Drawing.Size(346, 266);
			this.ScreenTabPage.TabIndex = 1;
			this.ScreenTabPage.Text = "Screen";
			this.ScreenTabPage.UseVisualStyleBackColor = true;
			// 
			// tabControl3
			// 
			this.tabControl3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl3.Controls.Add(this.DisplayTabPage);
			this.tabControl3.Controls.Add(this.LayoutTabPage);
			this.tabControl3.Controls.Add(this.ScreensaverTabPage);
			this.tabControl3.Location = new System.Drawing.Point(6, 6);
			this.tabControl3.Name = "tabControl3";
			this.tabControl3.SelectedIndex = 0;
			this.tabControl3.Size = new System.Drawing.Size(336, 254);
			this.tabControl3.TabIndex = 1;
			// 
			// DisplayTabPage
			// 
			this.DisplayTabPage.Controls.Add(this.label32);
			this.DisplayTabPage.Controls.Add(this.BrightnessPercentLabel);
			this.DisplayTabPage.Controls.Add(this.label31);
			this.DisplayTabPage.Controls.Add(this.label37);
			this.DisplayTabPage.Controls.Add(this.label33);
			this.DisplayTabPage.Controls.Add(this.StealthModeCheckBox);
			this.DisplayTabPage.Controls.Add(this.IdleTimeUpDow);
			this.DisplayTabPage.Controls.Add(this.label36);
			this.DisplayTabPage.Controls.Add(this.BrightnessTrackBar);
			this.DisplayTabPage.Controls.Add(this.FlippedModeCheckBox);
			this.DisplayTabPage.Location = new System.Drawing.Point(4, 22);
			this.DisplayTabPage.Name = "DisplayTabPage";
			this.DisplayTabPage.Size = new System.Drawing.Size(328, 228);
			this.DisplayTabPage.TabIndex = 2;
			this.DisplayTabPage.Text = "Display";
			this.DisplayTabPage.UseVisualStyleBackColor = true;
			// 
			// label32
			// 
			this.label32.AutoSize = true;
			this.label32.Location = new System.Drawing.Point(4, 11);
			this.label32.Name = "label32";
			this.label32.Size = new System.Drawing.Size(61, 13);
			this.label32.TabIndex = 53;
			this.label32.Text = "Brightness:";
			// 
			// BrightnessPercentLabel
			// 
			this.BrightnessPercentLabel.AutoSize = true;
			this.BrightnessPercentLabel.Location = new System.Drawing.Point(213, 11);
			this.BrightnessPercentLabel.Name = "BrightnessPercentLabel";
			this.BrightnessPercentLabel.Size = new System.Drawing.Size(18, 13);
			this.BrightnessPercentLabel.TabIndex = 54;
			this.BrightnessPercentLabel.Text = "%";
			// 
			// label31
			// 
			this.label31.AutoSize = true;
			this.label31.Location = new System.Drawing.Point(213, 38);
			this.label31.Name = "label31";
			this.label31.Size = new System.Drawing.Size(12, 13);
			this.label31.TabIndex = 79;
			this.label31.Text = "s";
			// 
			// label37
			// 
			this.label37.AutoSize = true;
			this.label37.Location = new System.Drawing.Point(4, 65);
			this.label37.Name = "label37";
			this.label37.Size = new System.Drawing.Size(74, 13);
			this.label37.TabIndex = 64;
			this.label37.Text = "Stealth Mode:";
			// 
			// label33
			// 
			this.label33.AutoSize = true;
			this.label33.Location = new System.Drawing.Point(4, 38);
			this.label33.Name = "label33";
			this.label33.Size = new System.Drawing.Size(54, 13);
			this.label33.TabIndex = 78;
			this.label33.Text = "Idle Time:";
			// 
			// StealthModeCheckBox
			// 
			this.StealthModeCheckBox.AutoSize = true;
			this.StealthModeCheckBox.Location = new System.Drawing.Point(101, 65);
			this.StealthModeCheckBox.Name = "StealthModeCheckBox";
			this.StealthModeCheckBox.Size = new System.Drawing.Size(64, 17);
			this.StealthModeCheckBox.TabIndex = 65;
			this.StealthModeCheckBox.Text = "Enabled";
			this.StealthModeCheckBox.UseVisualStyleBackColor = true;
			// 
			// IdleTimeUpDow
			// 
			this.IdleTimeUpDow.Location = new System.Drawing.Point(101, 35);
			this.IdleTimeUpDow.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
			this.IdleTimeUpDow.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
			this.IdleTimeUpDow.Name = "IdleTimeUpDow";
			this.IdleTimeUpDow.Size = new System.Drawing.Size(106, 21);
			this.IdleTimeUpDow.TabIndex = 77;
			this.IdleTimeUpDow.TabStop = false;
			this.IdleTimeUpDow.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.IdleTimeUpDow.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
			// 
			// label36
			// 
			this.label36.AutoSize = true;
			this.label36.Location = new System.Drawing.Point(4, 92);
			this.label36.Name = "label36";
			this.label36.Size = new System.Drawing.Size(45, 13);
			this.label36.TabIndex = 66;
			this.label36.Text = "Flipped:";
			// 
			// BrightnessTrackBar
			// 
			this.BrightnessTrackBar.AutoSize = false;
			this.BrightnessTrackBar.BackColor = System.Drawing.Color.White;
			this.BrightnessTrackBar.Location = new System.Drawing.Point(96, 10);
			this.BrightnessTrackBar.Maximum = 255;
			this.BrightnessTrackBar.Name = "BrightnessTrackBar";
			this.BrightnessTrackBar.Size = new System.Drawing.Size(115, 21);
			this.BrightnessTrackBar.SmallChange = 5;
			this.BrightnessTrackBar.TabIndex = 76;
			this.BrightnessTrackBar.TickFrequency = 16;
			// 
			// FlippedModeCheckBox
			// 
			this.FlippedModeCheckBox.AutoSize = true;
			this.FlippedModeCheckBox.Location = new System.Drawing.Point(101, 92);
			this.FlippedModeCheckBox.Name = "FlippedModeCheckBox";
			this.FlippedModeCheckBox.Size = new System.Drawing.Size(64, 17);
			this.FlippedModeCheckBox.TabIndex = 67;
			this.FlippedModeCheckBox.Text = "Enabled";
			this.FlippedModeCheckBox.UseVisualStyleBackColor = true;
			// 
			// LayoutTabPage
			// 
			this.LayoutTabPage.Controls.Add(this.label53);
			this.LayoutTabPage.Controls.Add(this.UseClassicMenuCheckBox);
			this.LayoutTabPage.Controls.Add(this.label45);
			this.LayoutTabPage.Controls.Add(this.ThirdLineContentComboBox);
			this.LayoutTabPage.Controls.Add(this.label38);
			this.LayoutTabPage.Controls.Add(this.ClockTypeComboBox);
			this.LayoutTabPage.Controls.Add(this.label35);
			this.LayoutTabPage.Controls.Add(this.BatteryPercentsCheckBox);
			this.LayoutTabPage.Controls.Add(this.label34);
			this.LayoutTabPage.Controls.Add(this.ShowLogoCheckBox);
			this.LayoutTabPage.Location = new System.Drawing.Point(4, 22);
			this.LayoutTabPage.Name = "LayoutTabPage";
			this.LayoutTabPage.Size = new System.Drawing.Size(328, 228);
			this.LayoutTabPage.TabIndex = 3;
			this.LayoutTabPage.Text = "Layout";
			this.LayoutTabPage.UseVisualStyleBackColor = true;
			// 
			// label53
			// 
			this.label53.AutoSize = true;
			this.label53.Location = new System.Drawing.Point(4, 119);
			this.label53.Name = "label53";
			this.label53.Size = new System.Drawing.Size(93, 13);
			this.label53.TabIndex = 80;
			this.label53.Text = "Use Classic Menu:";
			// 
			// UseClassicMenuCheckBox
			// 
			this.UseClassicMenuCheckBox.AutoSize = true;
			this.UseClassicMenuCheckBox.Location = new System.Drawing.Point(100, 119);
			this.UseClassicMenuCheckBox.Name = "UseClassicMenuCheckBox";
			this.UseClassicMenuCheckBox.Size = new System.Drawing.Size(64, 17);
			this.UseClassicMenuCheckBox.TabIndex = 81;
			this.UseClassicMenuCheckBox.Text = "Enabled";
			this.UseClassicMenuCheckBox.UseVisualStyleBackColor = true;
			// 
			// label45
			// 
			this.label45.AutoSize = true;
			this.label45.Location = new System.Drawing.Point(4, 11);
			this.label45.Name = "label45";
			this.label45.Size = new System.Drawing.Size(91, 13);
			this.label45.TabIndex = 78;
			this.label45.Text = "3rd Line Content:";
			// 
			// ThirdLineContentComboBox
			// 
			this.ThirdLineContentComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ThirdLineContentComboBox.FormattingEnabled = true;
			this.ThirdLineContentComboBox.Items.AddRange(new object[] {
            "Disabled",
            "Analog",
            "Digital"});
			this.ThirdLineContentComboBox.Location = new System.Drawing.Point(101, 8);
			this.ThirdLineContentComboBox.Name = "ThirdLineContentComboBox";
			this.ThirdLineContentComboBox.Size = new System.Drawing.Size(140, 21);
			this.ThirdLineContentComboBox.TabIndex = 79;
			// 
			// label38
			// 
			this.label38.AutoSize = true;
			this.label38.Location = new System.Drawing.Point(4, 92);
			this.label38.Name = "label38";
			this.label38.Size = new System.Drawing.Size(63, 13);
			this.label38.TabIndex = 76;
			this.label38.Text = "Clock Type:";
			// 
			// ClockTypeComboBox
			// 
			this.ClockTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ClockTypeComboBox.FormattingEnabled = true;
			this.ClockTypeComboBox.Items.AddRange(new object[] {
            "Disabled",
            "Analog",
            "Digital"});
			this.ClockTypeComboBox.Location = new System.Drawing.Point(100, 89);
			this.ClockTypeComboBox.Name = "ClockTypeComboBox";
			this.ClockTypeComboBox.Size = new System.Drawing.Size(140, 21);
			this.ClockTypeComboBox.TabIndex = 77;
			// 
			// label35
			// 
			this.label35.AutoSize = true;
			this.label35.Location = new System.Drawing.Point(4, 38);
			this.label35.Name = "label35";
			this.label35.Size = new System.Drawing.Size(92, 13);
			this.label35.TabIndex = 68;
			this.label35.Text = "Battery Percents:";
			// 
			// BatteryPercentsCheckBox
			// 
			this.BatteryPercentsCheckBox.AutoSize = true;
			this.BatteryPercentsCheckBox.Location = new System.Drawing.Point(100, 38);
			this.BatteryPercentsCheckBox.Name = "BatteryPercentsCheckBox";
			this.BatteryPercentsCheckBox.Size = new System.Drawing.Size(64, 17);
			this.BatteryPercentsCheckBox.TabIndex = 69;
			this.BatteryPercentsCheckBox.Text = "Enabled";
			this.BatteryPercentsCheckBox.UseVisualStyleBackColor = true;
			// 
			// label34
			// 
			this.label34.AutoSize = true;
			this.label34.Location = new System.Drawing.Point(4, 65);
			this.label34.Name = "label34";
			this.label34.Size = new System.Drawing.Size(63, 13);
			this.label34.TabIndex = 70;
			this.label34.Text = "Show Logo:";
			// 
			// ShowLogoCheckBox
			// 
			this.ShowLogoCheckBox.AutoSize = true;
			this.ShowLogoCheckBox.Location = new System.Drawing.Point(100, 65);
			this.ShowLogoCheckBox.Name = "ShowLogoCheckBox";
			this.ShowLogoCheckBox.Size = new System.Drawing.Size(64, 17);
			this.ShowLogoCheckBox.TabIndex = 71;
			this.ShowLogoCheckBox.Text = "Enabled";
			this.ShowLogoCheckBox.UseVisualStyleBackColor = true;
			// 
			// ScreensaverTabPage
			// 
			this.ScreensaverTabPage.Controls.Add(this.ScreenProtectionTimeComboBox);
			this.ScreensaverTabPage.Controls.Add(this.ScreensaverTypeComboBox);
			this.ScreensaverTabPage.Controls.Add(this.label41);
			this.ScreensaverTabPage.Controls.Add(this.label40);
			this.ScreensaverTabPage.Location = new System.Drawing.Point(4, 22);
			this.ScreensaverTabPage.Name = "ScreensaverTabPage";
			this.ScreensaverTabPage.Size = new System.Drawing.Size(328, 228);
			this.ScreensaverTabPage.TabIndex = 4;
			this.ScreensaverTabPage.Text = "Screensaver";
			this.ScreensaverTabPage.UseVisualStyleBackColor = true;
			// 
			// ScreenProtectionTimeComboBox
			// 
			this.ScreenProtectionTimeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ScreenProtectionTimeComboBox.FormattingEnabled = true;
			this.ScreenProtectionTimeComboBox.Items.AddRange(new object[] {
            "Disabled",
            "Analog",
            "Digital"});
			this.ScreenProtectionTimeComboBox.Location = new System.Drawing.Point(101, 35);
			this.ScreenProtectionTimeComboBox.Name = "ScreenProtectionTimeComboBox";
			this.ScreenProtectionTimeComboBox.Size = new System.Drawing.Size(106, 21);
			this.ScreenProtectionTimeComboBox.TabIndex = 81;
			// 
			// ScreensaverTypeComboBox
			// 
			this.ScreensaverTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ScreensaverTypeComboBox.FormattingEnabled = true;
			this.ScreensaverTypeComboBox.Items.AddRange(new object[] {
            "Disabled",
            "Analog",
            "Digital"});
			this.ScreensaverTypeComboBox.Location = new System.Drawing.Point(101, 8);
			this.ScreensaverTypeComboBox.Name = "ScreensaverTypeComboBox";
			this.ScreensaverTypeComboBox.Size = new System.Drawing.Size(106, 21);
			this.ScreensaverTypeComboBox.TabIndex = 79;
			// 
			// label41
			// 
			this.label41.AutoSize = true;
			this.label41.Location = new System.Drawing.Point(4, 38);
			this.label41.Name = "label41";
			this.label41.Size = new System.Drawing.Size(85, 13);
			this.label41.TabIndex = 80;
			this.label41.Text = "Protection Time:";
			// 
			// label40
			// 
			this.label40.AutoSize = true;
			this.label40.Location = new System.Drawing.Point(4, 11);
			this.label40.Name = "label40";
			this.label40.Size = new System.Drawing.Size(98, 13);
			this.label40.TabIndex = 78;
			this.label40.Text = "Screensaver Type:";
			// 
			// DeveloperTabPage
			// 
			this.DeveloperTabPage.Controls.Add(this.tabControl1);
			this.DeveloperTabPage.Location = new System.Drawing.Point(4, 22);
			this.DeveloperTabPage.Name = "DeveloperTabPage";
			this.DeveloperTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.DeveloperTabPage.Size = new System.Drawing.Size(346, 266);
			this.DeveloperTabPage.TabIndex = 2;
			this.DeveloperTabPage.Text = "Developer";
			this.DeveloperTabPage.UseVisualStyleBackColor = true;
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.ExpertTabPage);
			this.tabControl1.Controls.Add(this.ToolsTabPage);
			this.tabControl1.Controls.Add(this.TerminalTabPage);
			this.tabControl1.Location = new System.Drawing.Point(6, 6);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(336, 254);
			this.tabControl1.TabIndex = 40;
			// 
			// ExpertTabPage
			// 
			this.ExpertTabPage.Controls.Add(this.BatteryEditButton);
			this.ExpertTabPage.Controls.Add(this.label56);
			this.ExpertTabPage.Controls.Add(this.BatteryModelComboBox);
			this.ExpertTabPage.Controls.Add(this.label54);
			this.ExpertTabPage.Controls.Add(this.ShuntCorrectionUpDown);
			this.ExpertTabPage.Controls.Add(this.label55);
			this.ExpertTabPage.Location = new System.Drawing.Point(4, 22);
			this.ExpertTabPage.Name = "ExpertTabPage";
			this.ExpertTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.ExpertTabPage.Size = new System.Drawing.Size(328, 228);
			this.ExpertTabPage.TabIndex = 2;
			this.ExpertTabPage.Text = "Expert";
			this.ExpertTabPage.UseVisualStyleBackColor = true;
			// 
			// BatteryEditButton
			// 
			this.BatteryEditButton.Location = new System.Drawing.Point(100, 62);
			this.BatteryEditButton.Name = "BatteryEditButton";
			this.BatteryEditButton.Size = new System.Drawing.Size(122, 23);
			this.BatteryEditButton.TabIndex = 73;
			this.BatteryEditButton.Text = "Edit";
			this.BatteryEditButton.UseVisualStyleBackColor = true;
			this.BatteryEditButton.Visible = false;
			// 
			// label56
			// 
			this.label56.AutoSize = true;
			this.label56.Location = new System.Drawing.Point(4, 38);
			this.label56.Name = "label56";
			this.label56.Size = new System.Drawing.Size(78, 13);
			this.label56.TabIndex = 71;
			this.label56.Text = "Battery Model:";
			// 
			// BatteryModelComboBox
			// 
			this.BatteryModelComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.BatteryModelComboBox.FormattingEnabled = true;
			this.BatteryModelComboBox.Items.AddRange(new object[] {
            "°C",
            "°F"});
			this.BatteryModelComboBox.Location = new System.Drawing.Point(101, 35);
			this.BatteryModelComboBox.Name = "BatteryModelComboBox";
			this.BatteryModelComboBox.Size = new System.Drawing.Size(120, 21);
			this.BatteryModelComboBox.TabIndex = 72;
			// 
			// label54
			// 
			this.label54.AutoSize = true;
			this.label54.Location = new System.Drawing.Point(4, 11);
			this.label54.Name = "label54";
			this.label54.Size = new System.Drawing.Size(92, 13);
			this.label54.TabIndex = 69;
			this.label54.Text = "Shunt Correction:";
			// 
			// ShuntCorrectionUpDown
			// 
			this.ShuntCorrectionUpDown.Location = new System.Drawing.Point(101, 8);
			this.ShuntCorrectionUpDown.Maximum = new decimal(new int[] {
            115,
            0,
            0,
            0});
			this.ShuntCorrectionUpDown.Minimum = new decimal(new int[] {
            85,
            0,
            0,
            0});
			this.ShuntCorrectionUpDown.Name = "ShuntCorrectionUpDown";
			this.ShuntCorrectionUpDown.Size = new System.Drawing.Size(106, 21);
			this.ShuntCorrectionUpDown.TabIndex = 68;
			this.ShuntCorrectionUpDown.TabStop = false;
			this.ShuntCorrectionUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.ShuntCorrectionUpDown.Value = new decimal(new int[] {
            85,
            0,
            0,
            0});
			// 
			// label55
			// 
			this.label55.AutoSize = true;
			this.label55.Location = new System.Drawing.Point(213, 10);
			this.label55.Name = "label55";
			this.label55.Size = new System.Drawing.Size(18, 13);
			this.label55.TabIndex = 70;
			this.label55.Text = "%";
			// 
			// ToolsTabPage
			// 
			this.ToolsTabPage.Controls.Add(this.groupBox4);
			this.ToolsTabPage.Controls.Add(this.groupBox3);
			this.ToolsTabPage.Controls.Add(this.groupBox2);
			this.ToolsTabPage.Controls.Add(this.groupBox1);
			this.ToolsTabPage.Location = new System.Drawing.Point(4, 22);
			this.ToolsTabPage.Name = "ToolsTabPage";
			this.ToolsTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.ToolsTabPage.Size = new System.Drawing.Size(328, 228);
			this.ToolsTabPage.TabIndex = 0;
			this.ToolsTabPage.Text = "Tools";
			this.ToolsTabPage.UseVisualStyleBackColor = true;
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.ResetAndRestartButton);
			this.groupBox4.Controls.Add(this.RestartButton);
			this.groupBox4.Location = new System.Drawing.Point(6, 177);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(318, 48);
			this.groupBox4.TabIndex = 71;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Device manipulations";
			// 
			// ResetAndRestartButton
			// 
			this.ResetAndRestartButton.Location = new System.Drawing.Point(86, 16);
			this.ResetAndRestartButton.Name = "ResetAndRestartButton";
			this.ResetAndRestartButton.Size = new System.Drawing.Size(227, 25);
			this.ResetAndRestartButton.TabIndex = 41;
			this.ResetAndRestartButton.Text = "Reset dafaflash and restart";
			this.ResetAndRestartButton.UseVisualStyleBackColor = true;
			// 
			// RestartButton
			// 
			this.RestartButton.Location = new System.Drawing.Point(7, 16);
			this.RestartButton.Name = "RestartButton";
			this.RestartButton.Size = new System.Drawing.Size(75, 25);
			this.RestartButton.TabIndex = 40;
			this.RestartButton.Text = "Restart";
			this.RestartButton.UseVisualStyleBackColor = true;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.ScreenshotContainerPanel);
			this.groupBox3.Location = new System.Drawing.Point(6, 6);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(74, 165);
			this.groupBox3.TabIndex = 70;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Screen";
			// 
			// ScreenshotContainerPanel
			// 
			this.ScreenshotContainerPanel.BackColor = System.Drawing.Color.Black;
			this.ScreenshotContainerPanel.Controls.Add(this.ScreenshotPictureBox);
			this.ScreenshotContainerPanel.Location = new System.Drawing.Point(3, 20);
			this.ScreenshotContainerPanel.Name = "ScreenshotContainerPanel";
			this.ScreenshotContainerPanel.Size = new System.Drawing.Size(68, 132);
			this.ScreenshotContainerPanel.TabIndex = 1;
			// 
			// ScreenshotPictureBox
			// 
			this.ScreenshotPictureBox.Location = new System.Drawing.Point(2, 2);
			this.ScreenshotPictureBox.Name = "ScreenshotPictureBox";
			this.ScreenshotPictureBox.Size = new System.Drawing.Size(64, 128);
			this.ScreenshotPictureBox.TabIndex = 0;
			this.ScreenshotPictureBox.TabStop = false;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.SaveScreenshotButton);
			this.groupBox2.Controls.Add(this.label46);
			this.groupBox2.Controls.Add(this.label47);
			this.groupBox2.Controls.Add(this.PixelSizeUpDown);
			this.groupBox2.Controls.Add(this.TakeScreenshotBeforeSaveCheckBox);
			this.groupBox2.Location = new System.Drawing.Point(86, 67);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(238, 104);
			this.groupBox2.TabIndex = 69;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Saving screenshots";
			// 
			// SaveScreenshotButton
			// 
			this.SaveScreenshotButton.Image = global::NFirmwareEditor.Properties.Resources.save_screenshot;
			this.SaveScreenshotButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.SaveScreenshotButton.Location = new System.Drawing.Point(5, 20);
			this.SaveScreenshotButton.Name = "SaveScreenshotButton";
			this.SaveScreenshotButton.Size = new System.Drawing.Size(113, 25);
			this.SaveScreenshotButton.TabIndex = 36;
			this.SaveScreenshotButton.Text = "Save screenshot";
			this.SaveScreenshotButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.SaveScreenshotButton.UseVisualStyleBackColor = true;
			// 
			// label46
			// 
			this.label46.AutoSize = true;
			this.label46.Location = new System.Drawing.Point(5, 56);
			this.label46.Name = "label46";
			this.label46.Size = new System.Drawing.Size(99, 13);
			this.label46.TabIndex = 38;
			this.label46.Text = "Pixel size multiplier:";
			// 
			// label47
			// 
			this.label47.AutoSize = true;
			this.label47.Location = new System.Drawing.Point(5, 81);
			this.label47.Name = "label47";
			this.label47.Size = new System.Drawing.Size(95, 13);
			this.label47.TabIndex = 66;
			this.label47.Text = "Take before save:";
			// 
			// PixelSizeUpDown
			// 
			this.PixelSizeUpDown.Location = new System.Drawing.Point(158, 54);
			this.PixelSizeUpDown.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
			this.PixelSizeUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.PixelSizeUpDown.Name = "PixelSizeUpDown";
			this.PixelSizeUpDown.Size = new System.Drawing.Size(60, 21);
			this.PixelSizeUpDown.TabIndex = 37;
			this.PixelSizeUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// TakeScreenshotBeforeSaveCheckBox
			// 
			this.TakeScreenshotBeforeSaveCheckBox.AutoSize = true;
			this.TakeScreenshotBeforeSaveCheckBox.Location = new System.Drawing.Point(158, 81);
			this.TakeScreenshotBeforeSaveCheckBox.Name = "TakeScreenshotBeforeSaveCheckBox";
			this.TakeScreenshotBeforeSaveCheckBox.Size = new System.Drawing.Size(64, 17);
			this.TakeScreenshotBeforeSaveCheckBox.TabIndex = 67;
			this.TakeScreenshotBeforeSaveCheckBox.Text = "Enabled";
			this.TakeScreenshotBeforeSaveCheckBox.UseVisualStyleBackColor = true;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.TakeScreenshotButton);
			this.groupBox1.Controls.Add(this.BroadcastButton);
			this.groupBox1.Location = new System.Drawing.Point(86, 6);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(238, 55);
			this.groupBox1.TabIndex = 68;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Taking screenshots";
			// 
			// TakeScreenshotButton
			// 
			this.TakeScreenshotButton.Image = global::NFirmwareEditor.Properties.Resources.screenshot;
			this.TakeScreenshotButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.TakeScreenshotButton.Location = new System.Drawing.Point(5, 20);
			this.TakeScreenshotButton.Name = "TakeScreenshotButton";
			this.TakeScreenshotButton.Size = new System.Drawing.Size(113, 25);
			this.TakeScreenshotButton.TabIndex = 35;
			this.TakeScreenshotButton.Text = "Take screenshot";
			this.TakeScreenshotButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.TakeScreenshotButton.UseVisualStyleBackColor = true;
			// 
			// BroadcastButton
			// 
			this.BroadcastButton.Image = global::NFirmwareEditor.Properties.Resources.screenshot;
			this.BroadcastButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.BroadcastButton.Location = new System.Drawing.Point(120, 20);
			this.BroadcastButton.Name = "BroadcastButton";
			this.BroadcastButton.Size = new System.Drawing.Size(113, 25);
			this.BroadcastButton.TabIndex = 41;
			this.BroadcastButton.Text = "Start broadcast";
			this.BroadcastButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.BroadcastButton.UseVisualStyleBackColor = true;
			// 
			// TerminalTabPage
			// 
			this.TerminalTabPage.Controls.Add(this.CommandTextBox);
			this.TerminalTabPage.Controls.Add(this.TraceTextBox);
			this.TerminalTabPage.Controls.Add(this.ComDisconnectButton);
			this.TerminalTabPage.Controls.Add(this.ComConnectButton);
			this.TerminalTabPage.Controls.Add(this.PortComboBox);
			this.TerminalTabPage.Controls.Add(this.label49);
			this.TerminalTabPage.Location = new System.Drawing.Point(4, 22);
			this.TerminalTabPage.Name = "TerminalTabPage";
			this.TerminalTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.TerminalTabPage.Size = new System.Drawing.Size(328, 228);
			this.TerminalTabPage.TabIndex = 1;
			this.TerminalTabPage.Text = "Terminal";
			this.TerminalTabPage.UseVisualStyleBackColor = true;
			// 
			// CommandTextBox
			// 
			this.CommandTextBox.Location = new System.Drawing.Point(4, 203);
			this.CommandTextBox.Name = "CommandTextBox";
			this.CommandTextBox.Size = new System.Drawing.Size(320, 21);
			this.CommandTextBox.TabIndex = 84;
			// 
			// TraceTextBox
			// 
			this.TraceTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TraceTextBox.Font = new System.Drawing.Font("Courier New", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.TraceTextBox.Location = new System.Drawing.Point(4, 35);
			this.TraceTextBox.Multiline = true;
			this.TraceTextBox.Name = "TraceTextBox";
			this.TraceTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.TraceTextBox.Size = new System.Drawing.Size(320, 169);
			this.TraceTextBox.TabIndex = 83;
			// 
			// ComDisconnectButton
			// 
			this.ComDisconnectButton.Enabled = false;
			this.ComDisconnectButton.Image = global::NFirmwareEditor.Properties.Resources.disconnect;
			this.ComDisconnectButton.Location = new System.Drawing.Point(266, 6);
			this.ComDisconnectButton.Name = "ComDisconnectButton";
			this.ComDisconnectButton.Size = new System.Drawing.Size(58, 25);
			this.ComDisconnectButton.TabIndex = 82;
			this.ComDisconnectButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.ComDisconnectButton.UseVisualStyleBackColor = true;
			// 
			// ComConnectButton
			// 
			this.ComConnectButton.Image = global::NFirmwareEditor.Properties.Resources.connect;
			this.ComConnectButton.Location = new System.Drawing.Point(209, 6);
			this.ComConnectButton.Name = "ComConnectButton";
			this.ComConnectButton.Size = new System.Drawing.Size(58, 25);
			this.ComConnectButton.TabIndex = 81;
			this.ComConnectButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.ComConnectButton.UseVisualStyleBackColor = true;
			// 
			// PortComboBox
			// 
			this.PortComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.PortComboBox.FormattingEnabled = true;
			this.PortComboBox.Items.AddRange(new object[] {
            "Disabled",
            "Analog",
            "Digital"});
			this.PortComboBox.Location = new System.Drawing.Point(101, 8);
			this.PortComboBox.Name = "PortComboBox";
			this.PortComboBox.Size = new System.Drawing.Size(106, 21);
			this.PortComboBox.TabIndex = 80;
			// 
			// label49
			// 
			this.label49.AutoSize = true;
			this.label49.Location = new System.Drawing.Point(4, 11);
			this.label49.Name = "label49";
			this.label49.Size = new System.Drawing.Size(63, 13);
			this.label49.TabIndex = 54;
			this.label49.Text = "VCOM Port:";
			// 
			// MainErrorProvider
			// 
			this.MainErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
			this.MainErrorProvider.ContainerControl = this;
			// 
			// DeviceConfiguratorWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(374, 477);
			this.Controls.Add(this.MainContainer);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "DeviceConfiguratorWindow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "myEvic NFE Edition - Configuration";
			this.MainContainer.ResumeLayout(false);
			this.WelcomePage.ResumeLayout(false);
			this.WorkspacePage.ResumeLayout(false);
			this.WorkspacePage.PerformLayout();
			this.MainStatusBar.ResumeLayout(false);
			this.MainStatusBar.PerformLayout();
			this.groupPanel1.ResumeLayout(false);
			this.groupPanel1.PerformLayout();
			this.MainTabControl.ResumeLayout(false);
			this.GeneralTabPage.ResumeLayout(false);
			this.tabControl2.ResumeLayout(false);
			this.PowerTabPage.ResumeLayout(false);
			this.PowerTabPage.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.TemperatureUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.PreheatTimeUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.PreheatPowerUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.TCPowerUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.PowerUpDown)).EndInit();
			this.CoilsManagerTabPage.ResumeLayout(false);
			this.CoilsManagerTabPage.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.TCRM3UpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.TCRM2UpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ResistanceNiUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.TCRM1UpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ResistanceTiUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ResistanceSSUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ResistanceTCRUpDown)).EndInit();
			this.ModesTabPage.ResumeLayout(false);
			this.ModesTabPage.PerformLayout();
			this.ControlsTabPage.ResumeLayout(false);
			this.ControlsTabPage.PerformLayout();
			this.StatsTabPage.ResumeLayout(false);
			this.StatsTabPage.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.PuffsTimeUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.PuffsUpDown)).EndInit();
			this.ScreenTabPage.ResumeLayout(false);
			this.tabControl3.ResumeLayout(false);
			this.DisplayTabPage.ResumeLayout(false);
			this.DisplayTabPage.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.IdleTimeUpDow)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.BrightnessTrackBar)).EndInit();
			this.LayoutTabPage.ResumeLayout(false);
			this.LayoutTabPage.PerformLayout();
			this.ScreensaverTabPage.ResumeLayout(false);
			this.ScreensaverTabPage.PerformLayout();
			this.DeveloperTabPage.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.ExpertTabPage.ResumeLayout(false);
			this.ExpertTabPage.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.ShuntCorrectionUpDown)).EndInit();
			this.ToolsTabPage.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.ScreenshotContainerPanel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.ScreenshotPictureBox)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.PixelSizeUpDown)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.TerminalTabPage.ResumeLayout(false);
			this.TerminalTabPage.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.MainErrorProvider)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private UI.MultiPanel MainContainer;
		private UI.MultiPanelPage WelcomePage;
		private UI.MultiPanelPage WorkspacePage;
		private System.Windows.Forms.Label WelcomeLabel;
		private System.Windows.Forms.TabControl MainTabControl;
		private System.Windows.Forms.TabPage GeneralTabPage;
		private System.Windows.Forms.Label DeviceNameLabel;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox BootModeTextBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox FirmwareVersionTextBox;
		private System.Windows.Forms.TextBox HardwareVersionTextBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ComboBox TemperatureTypeComboBox;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.NumericUpDown TCRM3UpDown;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.NumericUpDown TCRM2UpDown;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.NumericUpDown TCRM1UpDown;
		private UI.GroupPanel groupPanel1;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.CheckBox TempNiModeCheckBox;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.CheckBox SmartModeCheckBox;
		private System.Windows.Forms.Label label27;
		private System.Windows.Forms.CheckBox BypassModeCheckBox;
		private System.Windows.Forms.Label label26;
		private System.Windows.Forms.CheckBox PowerModeCheckBox;
		private System.Windows.Forms.Label label25;
		private System.Windows.Forms.CheckBox TCRModeCheckBox;
		private System.Windows.Forms.Label label24;
		private System.Windows.Forms.CheckBox TempSSModeCheckBox;
		private System.Windows.Forms.Label label23;
		private System.Windows.Forms.CheckBox TempTiModeCheckBox;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.ComboBox SelectedModeComboBox;
		private System.Windows.Forms.Label label28;
		private System.Windows.Forms.ComboBox PreheatTypeComboBox;
		private System.Windows.Forms.Label label30;
		private System.Windows.Forms.Label label29;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.CheckBox ResistanceTCRCheckBox;
		private System.Windows.Forms.CheckBox ResistanceSSCheckBox;
		private System.Windows.Forms.CheckBox ResistanceTiCheckBox;
		private System.Windows.Forms.CheckBox ResistanceNiCheckBox;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.NumericUpDown ResistanceTCRUpDown;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.NumericUpDown ResistanceSSUpDown;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.NumericUpDown ResistanceTiUpDown;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.NumericUpDown ResistanceNiUpDown;
		private System.Windows.Forms.CheckBox TemperatureDominantCheckBox;
		private System.Windows.Forms.CheckBox Step1WCheckBox;
		private System.Windows.Forms.Label TCRIndexLabel;
		private System.Windows.Forms.ComboBox SelectedTCRComboBox;
		private System.Windows.Forms.Button DownloadButton;
		private System.Windows.Forms.Button UploadButton;
		private System.Windows.Forms.Button ResetButton;
		private System.Windows.Forms.Label BrightnessPercentLabel;
		private System.Windows.Forms.Label label32;
		private System.Windows.Forms.CheckBox ShowLogoCheckBox;
		private System.Windows.Forms.Label label34;
		private System.Windows.Forms.CheckBox BatteryPercentsCheckBox;
		private System.Windows.Forms.Label label35;
		private System.Windows.Forms.CheckBox FlippedModeCheckBox;
		private System.Windows.Forms.Label label36;
		private System.Windows.Forms.CheckBox StealthModeCheckBox;
		private System.Windows.Forms.Label label37;
		private System.Windows.Forms.TrackBar BrightnessTrackBar;
		private System.Windows.Forms.Label label31;
		private System.Windows.Forms.Label label33;
		private System.Windows.Forms.NumericUpDown IdleTimeUpDow;
		private System.Windows.Forms.CheckBox WakeUpByPlusMinusCheckBox;
		private System.Windows.Forms.Label label39;
		private System.Windows.Forms.TabPage ScreenTabPage;
		private System.Windows.Forms.TabPage DeveloperTabPage;
		private System.Windows.Forms.TabControl tabControl2;
		private System.Windows.Forms.TabPage PowerTabPage;
		private System.Windows.Forms.TabPage CoilsManagerTabPage;
		private System.Windows.Forms.TabPage ModesTabPage;
		private System.Windows.Forms.TabPage ControlsTabPage;
		private System.Windows.Forms.TabControl tabControl3;
		private System.Windows.Forms.TabPage DisplayTabPage;
		private System.Windows.Forms.TabPage LayoutTabPage;
		private System.Windows.Forms.TabPage ScreensaverTabPage;
		private System.Windows.Forms.ErrorProvider MainErrorProvider;
		private System.Windows.Forms.NumericUpDown PowerUpDown;
		private System.Windows.Forms.NumericUpDown PreheatTimeUpDown;
		private System.Windows.Forms.NumericUpDown PreheatPowerUpDown;
		private System.Windows.Forms.NumericUpDown TCPowerUpDown;
		private System.Windows.Forms.NumericUpDown TemperatureUpDown;
		private System.Windows.Forms.Label label38;
		private System.Windows.Forms.ComboBox ClockTypeComboBox;
		private System.Windows.Forms.Label label41;
		private System.Windows.Forms.ComboBox ScreenProtectionTimeComboBox;
		private System.Windows.Forms.Label label40;
		private System.Windows.Forms.ComboBox ScreensaverTypeComboBox;
		private System.Windows.Forms.Label label42;
		private System.Windows.Forms.ComboBox Clicks2ComboBox;
		private System.Windows.Forms.Label label44;
		private System.Windows.Forms.ComboBox Clicks4ComboBox;
		private System.Windows.Forms.Label label43;
		private System.Windows.Forms.ComboBox Clicks3ComboBox;
		private System.Windows.Forms.Label label45;
		private System.Windows.Forms.ComboBox ThirdLineContentComboBox;
		private System.Windows.Forms.Panel ScreenshotContainerPanel;
		private System.Windows.Forms.PictureBox ScreenshotPictureBox;
		private System.Windows.Forms.Button SaveScreenshotButton;
		private System.Windows.Forms.Label label46;
		private System.Windows.Forms.NumericUpDown PixelSizeUpDown;
		private System.Windows.Forms.TextBox BuildTextBox;
		private System.Windows.Forms.Label label48;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage ToolsTabPage;
		private System.Windows.Forms.TabPage TerminalTabPage;
		private System.Windows.Forms.Button TakeScreenshotButton;
		private System.Windows.Forms.Label label49;
		private System.Windows.Forms.ComboBox PortComboBox;
		private System.Windows.Forms.Button ComConnectButton;
		private System.Windows.Forms.Button ComDisconnectButton;
		private System.Windows.Forms.TextBox TraceTextBox;
		private System.Windows.Forms.TabPage StatsTabPage;
		private System.Windows.Forms.NumericUpDown PuffsTimeUpDown;
		private System.Windows.Forms.NumericUpDown PuffsUpDown;
		private System.Windows.Forms.Label label50;
		private System.Windows.Forms.Label label51;
		private System.Windows.Forms.Label label52;
		private System.Windows.Forms.StatusStrip MainStatusBar;
		private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
		private System.Windows.Forms.ToolStripStatusLabel ProgressLabel;
		private System.Windows.Forms.TextBox CommandTextBox;
		private System.Windows.Forms.Button RestartButton;
		private System.Windows.Forms.Button BroadcastButton;
		private System.Windows.Forms.Label label47;
		private System.Windows.Forms.CheckBox TakeScreenshotBeforeSaveCheckBox;
		private System.Windows.Forms.Label label53;
		private System.Windows.Forms.CheckBox UseClassicMenuCheckBox;
		private System.Windows.Forms.TabPage ExpertTabPage;
		private System.Windows.Forms.Label label54;
		private System.Windows.Forms.NumericUpDown ShuntCorrectionUpDown;
		private System.Windows.Forms.Label label55;
		private System.Windows.Forms.Label label56;
		private System.Windows.Forms.ComboBox BatteryModelComboBox;
		private System.Windows.Forms.Button BatteryEditButton;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Button ResetAndRestartButton;
	}
}