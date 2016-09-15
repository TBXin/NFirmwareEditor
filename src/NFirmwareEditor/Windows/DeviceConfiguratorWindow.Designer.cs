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
			this.MainContainer = new NFirmwareEditor.UI.MultiPanel();
			this.WelcomePage = new NFirmwareEditor.UI.MultiPanelPage();
			this.WelcomeLabel = new System.Windows.Forms.Label();
			this.WorkspacePage = new NFirmwareEditor.UI.MultiPanelPage();
			this.MainTabControl = new System.Windows.Forms.TabControl();
			this.GeneralTabPage = new System.Windows.Forms.TabPage();
			this.groupPanel1 = new NFirmwareEditor.UI.GroupPanel();
			this.FirmwareVersionTextBox = new System.Windows.Forms.TextBox();
			this.HardwareVersionTextBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.BootModeTextBox = new System.Windows.Forms.TextBox();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.PwrResTabPage = new System.Windows.Forms.TabPage();
			this.label30 = new System.Windows.Forms.Label();
			this.label29 = new System.Windows.Forms.Label();
			this.PreheatTimeTextBox = new System.Windows.Forms.TextBox();
			this.PreheatTypeComboBox = new System.Windows.Forms.ComboBox();
			this.label28 = new System.Windows.Forms.Label();
			this.PreheatPowerTextBox = new System.Windows.Forms.TextBox();
			this.label16 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.ResistanceTCRCheckBox = new System.Windows.Forms.CheckBox();
			this.ResistanceSSCheckBox = new System.Windows.Forms.CheckBox();
			this.ResistanceTiCheckBox = new System.Windows.Forms.CheckBox();
			this.ResistanceNiCheckBox = new System.Windows.Forms.CheckBox();
			this.label10 = new System.Windows.Forms.Label();
			this.ResistanceTCRTextBox = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.ResistanceSSTextBox = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.ResistanceTiTextBox = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.ResistanceNiTextBox = new System.Windows.Forms.TextBox();
			this.TemperatureComboBox = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.TemperatureTextBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.TCPowerTextBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.PowerTextBox = new System.Windows.Forms.TextBox();
			this.ModesTabPage = new System.Windows.Forms.TabPage();
			this.CurrentModeComboBox = new System.Windows.Forms.ComboBox();
			this.SmartModeCheckBox = new System.Windows.Forms.CheckBox();
			this.label27 = new System.Windows.Forms.Label();
			this.BypassModeCheckBox = new System.Windows.Forms.CheckBox();
			this.label26 = new System.Windows.Forms.Label();
			this.PowerModeCheckBox = new System.Windows.Forms.CheckBox();
			this.label25 = new System.Windows.Forms.Label();
			this.TCRModeCheckBox = new System.Windows.Forms.CheckBox();
			this.label24 = new System.Windows.Forms.Label();
			this.TempSSModeCheckBox = new System.Windows.Forms.CheckBox();
			this.label23 = new System.Windows.Forms.Label();
			this.TempTiModeCheckBox = new System.Windows.Forms.CheckBox();
			this.label22 = new System.Windows.Forms.Label();
			this.TempNiModeCheckBox = new System.Windows.Forms.CheckBox();
			this.label20 = new System.Windows.Forms.Label();
			this.label21 = new System.Windows.Forms.Label();
			this.TCRTabPage = new System.Windows.Forms.TabPage();
			this.label19 = new System.Windows.Forms.Label();
			this.M3TextBox = new System.Windows.Forms.TextBox();
			this.label18 = new System.Windows.Forms.Label();
			this.M2TextBox = new System.Windows.Forms.TextBox();
			this.label17 = new System.Windows.Forms.Label();
			this.M1TextBox = new System.Windows.Forms.TextBox();
			this.DeviceNameLabel = new System.Windows.Forms.Label();
			this.MainContainer.SuspendLayout();
			this.WelcomePage.SuspendLayout();
			this.WorkspacePage.SuspendLayout();
			this.MainTabControl.SuspendLayout();
			this.GeneralTabPage.SuspendLayout();
			this.groupPanel1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.PwrResTabPage.SuspendLayout();
			this.ModesTabPage.SuspendLayout();
			this.TCRTabPage.SuspendLayout();
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
			this.MainContainer.Size = new System.Drawing.Size(377, 496);
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
			this.WelcomePage.Size = new System.Drawing.Size(825, 599);
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
			this.WelcomeLabel.Size = new System.Drawing.Size(825, 599);
			this.WelcomeLabel.TabIndex = 0;
			this.WelcomeLabel.Text = "Waiting for device...";
			this.WelcomeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// WorkspacePage
			// 
			this.WorkspacePage.Controls.Add(this.MainTabControl);
			this.WorkspacePage.Description = null;
			this.WorkspacePage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.WorkspacePage.Location = new System.Drawing.Point(0, 0);
			this.WorkspacePage.Name = "WorkspacePage";
			this.WorkspacePage.Size = new System.Drawing.Size(377, 496);
			this.WorkspacePage.TabIndex = 1;
			this.WorkspacePage.Text = "WorkspacePage";
			// 
			// MainTabControl
			// 
			this.MainTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.MainTabControl.Controls.Add(this.GeneralTabPage);
			this.MainTabControl.Location = new System.Drawing.Point(12, 12);
			this.MainTabControl.Name = "MainTabControl";
			this.MainTabControl.SelectedIndex = 0;
			this.MainTabControl.Size = new System.Drawing.Size(353, 472);
			this.MainTabControl.TabIndex = 0;
			// 
			// GeneralTabPage
			// 
			this.GeneralTabPage.Controls.Add(this.groupPanel1);
			this.GeneralTabPage.Controls.Add(this.tabControl1);
			this.GeneralTabPage.Controls.Add(this.DeviceNameLabel);
			this.GeneralTabPage.Location = new System.Drawing.Point(4, 22);
			this.GeneralTabPage.Name = "GeneralTabPage";
			this.GeneralTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.GeneralTabPage.Size = new System.Drawing.Size(345, 446);
			this.GeneralTabPage.TabIndex = 0;
			this.GeneralTabPage.Text = "General";
			this.GeneralTabPage.UseVisualStyleBackColor = true;
			// 
			// groupPanel1
			// 
			this.groupPanel1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(185)))), ((int)(((byte)(185)))));
			this.groupPanel1.Controls.Add(this.FirmwareVersionTextBox);
			this.groupPanel1.Controls.Add(this.HardwareVersionTextBox);
			this.groupPanel1.Controls.Add(this.label2);
			this.groupPanel1.Controls.Add(this.label5);
			this.groupPanel1.Controls.Add(this.label4);
			this.groupPanel1.Controls.Add(this.BootModeTextBox);
			this.groupPanel1.HeaderBackColor = System.Drawing.Color.White;
			this.groupPanel1.HeaderHeight = 30;
			this.groupPanel1.Location = new System.Drawing.Point(11, 39);
			this.groupPanel1.Name = "groupPanel1";
			this.groupPanel1.Size = new System.Drawing.Size(322, 112);
			this.groupPanel1.TabIndex = 34;
			this.groupPanel1.TabStop = false;
			this.groupPanel1.Text = "Device info:";
			// 
			// FirmwareVersionTextBox
			// 
			this.FirmwareVersionTextBox.Location = new System.Drawing.Point(106, 33);
			this.FirmwareVersionTextBox.Name = "FirmwareVersionTextBox";
			this.FirmwareVersionTextBox.Size = new System.Drawing.Size(212, 21);
			this.FirmwareVersionTextBox.TabIndex = 27;
			this.FirmwareVersionTextBox.TabStop = false;
			// 
			// HardwareVersionTextBox
			// 
			this.HardwareVersionTextBox.Location = new System.Drawing.Point(106, 60);
			this.HardwareVersionTextBox.Name = "HardwareVersionTextBox";
			this.HardwareVersionTextBox.Size = new System.Drawing.Size(212, 21);
			this.HardwareVersionTextBox.TabIndex = 28;
			this.HardwareVersionTextBox.TabStop = false;
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
			this.BootModeTextBox.Location = new System.Drawing.Point(106, 87);
			this.BootModeTextBox.Name = "BootModeTextBox";
			this.BootModeTextBox.Size = new System.Drawing.Size(212, 21);
			this.BootModeTextBox.TabIndex = 31;
			this.BootModeTextBox.TabStop = false;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.PwrResTabPage);
			this.tabControl1.Controls.Add(this.ModesTabPage);
			this.tabControl1.Controls.Add(this.TCRTabPage);
			this.tabControl1.ItemSize = new System.Drawing.Size(70, 18);
			this.tabControl1.Location = new System.Drawing.Point(11, 157);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(324, 279);
			this.tabControl1.TabIndex = 33;
			// 
			// PwrResTabPage
			// 
			this.PwrResTabPage.Controls.Add(this.label30);
			this.PwrResTabPage.Controls.Add(this.label29);
			this.PwrResTabPage.Controls.Add(this.PreheatTimeTextBox);
			this.PwrResTabPage.Controls.Add(this.PreheatTypeComboBox);
			this.PwrResTabPage.Controls.Add(this.label28);
			this.PwrResTabPage.Controls.Add(this.PreheatPowerTextBox);
			this.PwrResTabPage.Controls.Add(this.label16);
			this.PwrResTabPage.Controls.Add(this.label15);
			this.PwrResTabPage.Controls.Add(this.label14);
			this.PwrResTabPage.Controls.Add(this.label13);
			this.PwrResTabPage.Controls.Add(this.label12);
			this.PwrResTabPage.Controls.Add(this.label11);
			this.PwrResTabPage.Controls.Add(this.ResistanceTCRCheckBox);
			this.PwrResTabPage.Controls.Add(this.ResistanceSSCheckBox);
			this.PwrResTabPage.Controls.Add(this.ResistanceTiCheckBox);
			this.PwrResTabPage.Controls.Add(this.ResistanceNiCheckBox);
			this.PwrResTabPage.Controls.Add(this.label10);
			this.PwrResTabPage.Controls.Add(this.ResistanceTCRTextBox);
			this.PwrResTabPage.Controls.Add(this.label9);
			this.PwrResTabPage.Controls.Add(this.ResistanceSSTextBox);
			this.PwrResTabPage.Controls.Add(this.label8);
			this.PwrResTabPage.Controls.Add(this.ResistanceTiTextBox);
			this.PwrResTabPage.Controls.Add(this.label7);
			this.PwrResTabPage.Controls.Add(this.ResistanceNiTextBox);
			this.PwrResTabPage.Controls.Add(this.TemperatureComboBox);
			this.PwrResTabPage.Controls.Add(this.label6);
			this.PwrResTabPage.Controls.Add(this.TemperatureTextBox);
			this.PwrResTabPage.Controls.Add(this.label3);
			this.PwrResTabPage.Controls.Add(this.TCPowerTextBox);
			this.PwrResTabPage.Controls.Add(this.label1);
			this.PwrResTabPage.Controls.Add(this.PowerTextBox);
			this.PwrResTabPage.Location = new System.Drawing.Point(4, 22);
			this.PwrResTabPage.Name = "PwrResTabPage";
			this.PwrResTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.PwrResTabPage.Size = new System.Drawing.Size(316, 253);
			this.PwrResTabPage.TabIndex = 0;
			this.PwrResTabPage.Text = "Power & Resistance  ";
			this.PwrResTabPage.UseVisualStyleBackColor = true;
			// 
			// label30
			// 
			this.label30.AutoSize = true;
			this.label30.Location = new System.Drawing.Point(213, 227);
			this.label30.Name = "label30";
			this.label30.Size = new System.Drawing.Size(12, 13);
			this.label30.TabIndex = 60;
			this.label30.Text = "s";
			// 
			// label29
			// 
			this.label29.AutoSize = true;
			this.label29.Location = new System.Drawing.Point(4, 227);
			this.label29.Name = "label29";
			this.label29.Size = new System.Drawing.Size(74, 13);
			this.label29.TabIndex = 59;
			this.label29.Text = "Preheat Time:";
			// 
			// PreheatTimeTextBox
			// 
			this.PreheatTimeTextBox.Location = new System.Drawing.Point(101, 224);
			this.PreheatTimeTextBox.Name = "PreheatTimeTextBox";
			this.PreheatTimeTextBox.Size = new System.Drawing.Size(106, 21);
			this.PreheatTimeTextBox.TabIndex = 58;
			this.PreheatTimeTextBox.TabStop = false;
			this.PreheatTimeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// PreheatTypeComboBox
			// 
			this.PreheatTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.PreheatTypeComboBox.FormattingEnabled = true;
			this.PreheatTypeComboBox.Items.AddRange(new object[] {
            "W",
            "%"});
			this.PreheatTypeComboBox.Location = new System.Drawing.Point(213, 197);
			this.PreheatTypeComboBox.Name = "PreheatTypeComboBox";
			this.PreheatTypeComboBox.Size = new System.Drawing.Size(42, 21);
			this.PreheatTypeComboBox.TabIndex = 57;
			// 
			// label28
			// 
			this.label28.AutoSize = true;
			this.label28.Location = new System.Drawing.Point(4, 200);
			this.label28.Name = "label28";
			this.label28.Size = new System.Drawing.Size(82, 13);
			this.label28.TabIndex = 56;
			this.label28.Text = "Preheat Power:";
			// 
			// PreheatPowerTextBox
			// 
			this.PreheatPowerTextBox.Location = new System.Drawing.Point(101, 197);
			this.PreheatPowerTextBox.Name = "PreheatPowerTextBox";
			this.PreheatPowerTextBox.Size = new System.Drawing.Size(106, 21);
			this.PreheatPowerTextBox.TabIndex = 55;
			this.PreheatPowerTextBox.TabStop = false;
			this.PreheatPowerTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label16.Location = new System.Drawing.Point(213, 172);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(17, 16);
			this.label16.TabIndex = 54;
			this.label16.Text = "Ω";
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label15.Location = new System.Drawing.Point(213, 145);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(17, 16);
			this.label15.TabIndex = 54;
			this.label15.Text = "Ω";
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label14.Location = new System.Drawing.Point(213, 118);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(17, 16);
			this.label14.TabIndex = 54;
			this.label14.Text = "Ω";
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label13.Location = new System.Drawing.Point(213, 91);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(17, 16);
			this.label13.TabIndex = 53;
			this.label13.Text = "Ω";
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
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(213, 11);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(17, 13);
			this.label11.TabIndex = 51;
			this.label11.Text = "W";
			// 
			// ResistanceTCRCheckBox
			// 
			this.ResistanceTCRCheckBox.AutoSize = true;
			this.ResistanceTCRCheckBox.Location = new System.Drawing.Point(242, 173);
			this.ResistanceTCRCheckBox.Name = "ResistanceTCRCheckBox";
			this.ResistanceTCRCheckBox.Size = new System.Drawing.Size(59, 17);
			this.ResistanceTCRCheckBox.TabIndex = 50;
			this.ResistanceTCRCheckBox.Text = "Locked";
			this.ResistanceTCRCheckBox.UseVisualStyleBackColor = true;
			// 
			// ResistanceSSCheckBox
			// 
			this.ResistanceSSCheckBox.AutoSize = true;
			this.ResistanceSSCheckBox.Location = new System.Drawing.Point(242, 146);
			this.ResistanceSSCheckBox.Name = "ResistanceSSCheckBox";
			this.ResistanceSSCheckBox.Size = new System.Drawing.Size(59, 17);
			this.ResistanceSSCheckBox.TabIndex = 50;
			this.ResistanceSSCheckBox.Text = "Locked";
			this.ResistanceSSCheckBox.UseVisualStyleBackColor = true;
			// 
			// ResistanceTiCheckBox
			// 
			this.ResistanceTiCheckBox.AutoSize = true;
			this.ResistanceTiCheckBox.Location = new System.Drawing.Point(242, 119);
			this.ResistanceTiCheckBox.Name = "ResistanceTiCheckBox";
			this.ResistanceTiCheckBox.Size = new System.Drawing.Size(59, 17);
			this.ResistanceTiCheckBox.TabIndex = 50;
			this.ResistanceTiCheckBox.Text = "Locked";
			this.ResistanceTiCheckBox.UseVisualStyleBackColor = true;
			// 
			// ResistanceNiCheckBox
			// 
			this.ResistanceNiCheckBox.AutoSize = true;
			this.ResistanceNiCheckBox.Location = new System.Drawing.Point(242, 92);
			this.ResistanceNiCheckBox.Name = "ResistanceNiCheckBox";
			this.ResistanceNiCheckBox.Size = new System.Drawing.Size(59, 17);
			this.ResistanceNiCheckBox.TabIndex = 49;
			this.ResistanceNiCheckBox.Text = "Locked";
			this.ResistanceNiCheckBox.UseVisualStyleBackColor = true;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(4, 173);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(86, 13);
			this.label10.TabIndex = 48;
			this.label10.Text = "Resistance TCR:";
			// 
			// ResistanceTCRTextBox
			// 
			this.ResistanceTCRTextBox.Location = new System.Drawing.Point(101, 170);
			this.ResistanceTCRTextBox.Name = "ResistanceTCRTextBox";
			this.ResistanceTCRTextBox.Size = new System.Drawing.Size(106, 21);
			this.ResistanceTCRTextBox.TabIndex = 47;
			this.ResistanceTCRTextBox.TabStop = false;
			this.ResistanceTCRTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(4, 146);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(78, 13);
			this.label9.TabIndex = 46;
			this.label9.Text = "Resistance SS:";
			// 
			// ResistanceSSTextBox
			// 
			this.ResistanceSSTextBox.Location = new System.Drawing.Point(101, 143);
			this.ResistanceSSTextBox.Name = "ResistanceSSTextBox";
			this.ResistanceSSTextBox.Size = new System.Drawing.Size(106, 21);
			this.ResistanceSSTextBox.TabIndex = 45;
			this.ResistanceSSTextBox.TabStop = false;
			this.ResistanceSSTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(4, 119);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(74, 13);
			this.label8.TabIndex = 44;
			this.label8.Text = "Resistance Ti:";
			// 
			// ResistanceTiTextBox
			// 
			this.ResistanceTiTextBox.Location = new System.Drawing.Point(101, 116);
			this.ResistanceTiTextBox.Name = "ResistanceTiTextBox";
			this.ResistanceTiTextBox.Size = new System.Drawing.Size(106, 21);
			this.ResistanceTiTextBox.TabIndex = 43;
			this.ResistanceTiTextBox.TabStop = false;
			this.ResistanceTiTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(4, 92);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(75, 13);
			this.label7.TabIndex = 42;
			this.label7.Text = "Resistance Ni:";
			// 
			// ResistanceNiTextBox
			// 
			this.ResistanceNiTextBox.Location = new System.Drawing.Point(101, 89);
			this.ResistanceNiTextBox.Name = "ResistanceNiTextBox";
			this.ResistanceNiTextBox.Size = new System.Drawing.Size(106, 21);
			this.ResistanceNiTextBox.TabIndex = 41;
			this.ResistanceNiTextBox.TabStop = false;
			this.ResistanceNiTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// TemperatureComboBox
			// 
			this.TemperatureComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.TemperatureComboBox.FormattingEnabled = true;
			this.TemperatureComboBox.Items.AddRange(new object[] {
            "°C",
            "°F"});
			this.TemperatureComboBox.Location = new System.Drawing.Point(213, 62);
			this.TemperatureComboBox.Name = "TemperatureComboBox";
			this.TemperatureComboBox.Size = new System.Drawing.Size(42, 21);
			this.TemperatureComboBox.TabIndex = 40;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(4, 65);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(73, 13);
			this.label6.TabIndex = 39;
			this.label6.Text = "Temperature:";
			// 
			// TemperatureTextBox
			// 
			this.TemperatureTextBox.Location = new System.Drawing.Point(101, 62);
			this.TemperatureTextBox.Name = "TemperatureTextBox";
			this.TemperatureTextBox.Size = new System.Drawing.Size(106, 21);
			this.TemperatureTextBox.TabIndex = 38;
			this.TemperatureTextBox.TabStop = false;
			this.TemperatureTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
			// TCPowerTextBox
			// 
			this.TCPowerTextBox.Location = new System.Drawing.Point(101, 35);
			this.TCPowerTextBox.Name = "TCPowerTextBox";
			this.TCPowerTextBox.Size = new System.Drawing.Size(106, 21);
			this.TCPowerTextBox.TabIndex = 36;
			this.TCPowerTextBox.TabStop = false;
			this.TCPowerTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
			// PowerTextBox
			// 
			this.PowerTextBox.Location = new System.Drawing.Point(101, 8);
			this.PowerTextBox.Name = "PowerTextBox";
			this.PowerTextBox.Size = new System.Drawing.Size(106, 21);
			this.PowerTextBox.TabIndex = 34;
			this.PowerTextBox.TabStop = false;
			this.PowerTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// ModesTabPage
			// 
			this.ModesTabPage.Controls.Add(this.CurrentModeComboBox);
			this.ModesTabPage.Controls.Add(this.SmartModeCheckBox);
			this.ModesTabPage.Controls.Add(this.label27);
			this.ModesTabPage.Controls.Add(this.BypassModeCheckBox);
			this.ModesTabPage.Controls.Add(this.label26);
			this.ModesTabPage.Controls.Add(this.PowerModeCheckBox);
			this.ModesTabPage.Controls.Add(this.label25);
			this.ModesTabPage.Controls.Add(this.TCRModeCheckBox);
			this.ModesTabPage.Controls.Add(this.label24);
			this.ModesTabPage.Controls.Add(this.TempSSModeCheckBox);
			this.ModesTabPage.Controls.Add(this.label23);
			this.ModesTabPage.Controls.Add(this.TempTiModeCheckBox);
			this.ModesTabPage.Controls.Add(this.label22);
			this.ModesTabPage.Controls.Add(this.TempNiModeCheckBox);
			this.ModesTabPage.Controls.Add(this.label20);
			this.ModesTabPage.Controls.Add(this.label21);
			this.ModesTabPage.Location = new System.Drawing.Point(4, 22);
			this.ModesTabPage.Name = "ModesTabPage";
			this.ModesTabPage.Size = new System.Drawing.Size(316, 253);
			this.ModesTabPage.TabIndex = 5;
			this.ModesTabPage.Text = "Modes";
			this.ModesTabPage.UseVisualStyleBackColor = true;
			// 
			// CurrentModeComboBox
			// 
			this.CurrentModeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CurrentModeComboBox.FormattingEnabled = true;
			this.CurrentModeComboBox.Items.AddRange(new object[] {
            "°C",
            "°F"});
			this.CurrentModeComboBox.Location = new System.Drawing.Point(101, 8);
			this.CurrentModeComboBox.Name = "CurrentModeComboBox";
			this.CurrentModeComboBox.Size = new System.Drawing.Size(211, 21);
			this.CurrentModeComboBox.TabIndex = 68;
			// 
			// SmartModeCheckBox
			// 
			this.SmartModeCheckBox.AutoSize = true;
			this.SmartModeCheckBox.Location = new System.Drawing.Point(101, 200);
			this.SmartModeCheckBox.Name = "SmartModeCheckBox";
			this.SmartModeCheckBox.Size = new System.Drawing.Size(64, 17);
			this.SmartModeCheckBox.TabIndex = 67;
			this.SmartModeCheckBox.Text = "Enabled";
			this.SmartModeCheckBox.UseVisualStyleBackColor = true;
			// 
			// label27
			// 
			this.label27.AutoSize = true;
			this.label27.Location = new System.Drawing.Point(4, 200);
			this.label27.Name = "label27";
			this.label27.Size = new System.Drawing.Size(73, 13);
			this.label27.TabIndex = 66;
			this.label27.Text = "Smart / Start:";
			// 
			// BypassModeCheckBox
			// 
			this.BypassModeCheckBox.AutoSize = true;
			this.BypassModeCheckBox.Location = new System.Drawing.Point(101, 173);
			this.BypassModeCheckBox.Name = "BypassModeCheckBox";
			this.BypassModeCheckBox.Size = new System.Drawing.Size(64, 17);
			this.BypassModeCheckBox.TabIndex = 65;
			this.BypassModeCheckBox.Text = "Enabled";
			this.BypassModeCheckBox.UseVisualStyleBackColor = true;
			// 
			// label26
			// 
			this.label26.AutoSize = true;
			this.label26.Location = new System.Drawing.Point(4, 173);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(45, 13);
			this.label26.TabIndex = 64;
			this.label26.Text = "Bypass:";
			// 
			// PowerModeCheckBox
			// 
			this.PowerModeCheckBox.AutoSize = true;
			this.PowerModeCheckBox.Location = new System.Drawing.Point(101, 146);
			this.PowerModeCheckBox.Name = "PowerModeCheckBox";
			this.PowerModeCheckBox.Size = new System.Drawing.Size(64, 17);
			this.PowerModeCheckBox.TabIndex = 63;
			this.PowerModeCheckBox.Text = "Enabled";
			this.PowerModeCheckBox.UseVisualStyleBackColor = true;
			// 
			// label25
			// 
			this.label25.AutoSize = true;
			this.label25.Location = new System.Drawing.Point(4, 146);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(41, 13);
			this.label25.TabIndex = 62;
			this.label25.Text = "Power:";
			// 
			// TCRModeCheckBox
			// 
			this.TCRModeCheckBox.AutoSize = true;
			this.TCRModeCheckBox.Location = new System.Drawing.Point(101, 119);
			this.TCRModeCheckBox.Name = "TCRModeCheckBox";
			this.TCRModeCheckBox.Size = new System.Drawing.Size(64, 17);
			this.TCRModeCheckBox.TabIndex = 61;
			this.TCRModeCheckBox.Text = "Enabled";
			this.TCRModeCheckBox.UseVisualStyleBackColor = true;
			// 
			// label24
			// 
			this.label24.AutoSize = true;
			this.label24.Location = new System.Drawing.Point(4, 119);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(96, 13);
			this.label24.TabIndex = 60;
			this.label24.Text = "Temperature TCR:";
			// 
			// TempSSModeCheckBox
			// 
			this.TempSSModeCheckBox.AutoSize = true;
			this.TempSSModeCheckBox.Location = new System.Drawing.Point(101, 92);
			this.TempSSModeCheckBox.Name = "TempSSModeCheckBox";
			this.TempSSModeCheckBox.Size = new System.Drawing.Size(64, 17);
			this.TempSSModeCheckBox.TabIndex = 59;
			this.TempSSModeCheckBox.Text = "Enabled";
			this.TempSSModeCheckBox.UseVisualStyleBackColor = true;
			// 
			// label23
			// 
			this.label23.AutoSize = true;
			this.label23.Location = new System.Drawing.Point(4, 92);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(88, 13);
			this.label23.TabIndex = 58;
			this.label23.Text = "Temperature SS:";
			// 
			// TempTiModeCheckBox
			// 
			this.TempTiModeCheckBox.AutoSize = true;
			this.TempTiModeCheckBox.Location = new System.Drawing.Point(101, 65);
			this.TempTiModeCheckBox.Name = "TempTiModeCheckBox";
			this.TempTiModeCheckBox.Size = new System.Drawing.Size(64, 17);
			this.TempTiModeCheckBox.TabIndex = 57;
			this.TempTiModeCheckBox.Text = "Enabled";
			this.TempTiModeCheckBox.UseVisualStyleBackColor = true;
			// 
			// label22
			// 
			this.label22.AutoSize = true;
			this.label22.Location = new System.Drawing.Point(4, 65);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(84, 13);
			this.label22.TabIndex = 56;
			this.label22.Text = "Temperature Ti:";
			// 
			// TempNiModeCheckBox
			// 
			this.TempNiModeCheckBox.AutoSize = true;
			this.TempNiModeCheckBox.Location = new System.Drawing.Point(101, 38);
			this.TempNiModeCheckBox.Name = "TempNiModeCheckBox";
			this.TempNiModeCheckBox.Size = new System.Drawing.Size(64, 17);
			this.TempNiModeCheckBox.TabIndex = 55;
			this.TempNiModeCheckBox.Text = "Enabled";
			this.TempNiModeCheckBox.UseVisualStyleBackColor = true;
			// 
			// label20
			// 
			this.label20.AutoSize = true;
			this.label20.Location = new System.Drawing.Point(4, 38);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(85, 13);
			this.label20.TabIndex = 54;
			this.label20.Text = "Temperature Ni:";
			// 
			// label21
			// 
			this.label21.AutoSize = true;
			this.label21.Location = new System.Drawing.Point(4, 11);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(77, 13);
			this.label21.TabIndex = 53;
			this.label21.Text = "Current Mode:";
			// 
			// TCRTabPage
			// 
			this.TCRTabPage.Controls.Add(this.label19);
			this.TCRTabPage.Controls.Add(this.M3TextBox);
			this.TCRTabPage.Controls.Add(this.label18);
			this.TCRTabPage.Controls.Add(this.M2TextBox);
			this.TCRTabPage.Controls.Add(this.label17);
			this.TCRTabPage.Controls.Add(this.M1TextBox);
			this.TCRTabPage.Location = new System.Drawing.Point(4, 22);
			this.TCRTabPage.Name = "TCRTabPage";
			this.TCRTabPage.Size = new System.Drawing.Size(316, 253);
			this.TCRTabPage.TabIndex = 4;
			this.TCRTabPage.Text = "TCR";
			this.TCRTabPage.UseVisualStyleBackColor = true;
			// 
			// label19
			// 
			this.label19.AutoSize = true;
			this.label19.Location = new System.Drawing.Point(4, 65);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(48, 13);
			this.label19.TabIndex = 41;
			this.label19.Text = "TCR M3:";
			// 
			// M3TextBox
			// 
			this.M3TextBox.Location = new System.Drawing.Point(101, 62);
			this.M3TextBox.Name = "M3TextBox";
			this.M3TextBox.Size = new System.Drawing.Size(106, 21);
			this.M3TextBox.TabIndex = 40;
			this.M3TextBox.TabStop = false;
			this.M3TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label18
			// 
			this.label18.AutoSize = true;
			this.label18.Location = new System.Drawing.Point(4, 38);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(48, 13);
			this.label18.TabIndex = 39;
			this.label18.Text = "TCR M2:";
			// 
			// M2TextBox
			// 
			this.M2TextBox.Location = new System.Drawing.Point(101, 35);
			this.M2TextBox.Name = "M2TextBox";
			this.M2TextBox.Size = new System.Drawing.Size(106, 21);
			this.M2TextBox.TabIndex = 38;
			this.M2TextBox.TabStop = false;
			this.M2TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label17
			// 
			this.label17.AutoSize = true;
			this.label17.Location = new System.Drawing.Point(4, 11);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(48, 13);
			this.label17.TabIndex = 37;
			this.label17.Text = "TCR M1:";
			// 
			// M1TextBox
			// 
			this.M1TextBox.Location = new System.Drawing.Point(101, 8);
			this.M1TextBox.Name = "M1TextBox";
			this.M1TextBox.Size = new System.Drawing.Size(106, 21);
			this.M1TextBox.TabIndex = 36;
			this.M1TextBox.TabStop = false;
			this.M1TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// DeviceNameLabel
			// 
			this.DeviceNameLabel.AutoSize = true;
			this.DeviceNameLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.DeviceNameLabel.Location = new System.Drawing.Point(7, 9);
			this.DeviceNameLabel.Name = "DeviceNameLabel";
			this.DeviceNameLabel.Size = new System.Drawing.Size(123, 19);
			this.DeviceNameLabel.TabIndex = 0;
			this.DeviceNameLabel.Text = "LostVape Triade";
			// 
			// DeviceConfiguratorWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(377, 496);
			this.Controls.Add(this.MainContainer);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "DeviceConfiguratorWindow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "NFirmwareEditor - Device Configurator ";
			this.MainContainer.ResumeLayout(false);
			this.WelcomePage.ResumeLayout(false);
			this.WorkspacePage.ResumeLayout(false);
			this.MainTabControl.ResumeLayout(false);
			this.GeneralTabPage.ResumeLayout(false);
			this.GeneralTabPage.PerformLayout();
			this.groupPanel1.ResumeLayout(false);
			this.groupPanel1.PerformLayout();
			this.tabControl1.ResumeLayout(false);
			this.PwrResTabPage.ResumeLayout(false);
			this.PwrResTabPage.PerformLayout();
			this.ModesTabPage.ResumeLayout(false);
			this.ModesTabPage.PerformLayout();
			this.TCRTabPage.ResumeLayout(false);
			this.TCRTabPage.PerformLayout();
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
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage PwrResTabPage;
		private System.Windows.Forms.TabPage TCRTabPage;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox PowerTextBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox TCPowerTextBox;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox TemperatureTextBox;
		private System.Windows.Forms.ComboBox TemperatureComboBox;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox ResistanceNiTextBox;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox ResistanceTiTextBox;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox ResistanceSSTextBox;
		private System.Windows.Forms.CheckBox ResistanceNiCheckBox;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox ResistanceTCRTextBox;
		private System.Windows.Forms.CheckBox ResistanceTCRCheckBox;
		private System.Windows.Forms.CheckBox ResistanceSSCheckBox;
		private System.Windows.Forms.CheckBox ResistanceTiCheckBox;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.TextBox M3TextBox;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.TextBox M2TextBox;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.TextBox M1TextBox;
		private UI.GroupPanel groupPanel1;
		private System.Windows.Forms.TabPage ModesTabPage;
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
		private System.Windows.Forms.ComboBox CurrentModeComboBox;
		private System.Windows.Forms.Label label28;
		private System.Windows.Forms.TextBox PreheatPowerTextBox;
		private System.Windows.Forms.ComboBox PreheatTypeComboBox;
		private System.Windows.Forms.Label label30;
		private System.Windows.Forms.Label label29;
		private System.Windows.Forms.TextBox PreheatTimeTextBox;
	}
}