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
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.SettingsTabPage = new System.Windows.Forms.TabPage();
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
			this.TCRTabPage = new System.Windows.Forms.TabPage();
			this.label2 = new System.Windows.Forms.Label();
			this.DeviceNameLabel = new System.Windows.Forms.Label();
			this.BootModeTextBox = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.FirmwareVersionTextBox = new System.Windows.Forms.TextBox();
			this.HardwareVersionTextBox = new System.Windows.Forms.TextBox();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.MainContainer.SuspendLayout();
			this.WelcomePage.SuspendLayout();
			this.WorkspacePage.SuspendLayout();
			this.MainTabControl.SuspendLayout();
			this.GeneralTabPage.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.SettingsTabPage.SuspendLayout();
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
			this.MainContainer.Size = new System.Drawing.Size(468, 531);
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
			this.WorkspacePage.Size = new System.Drawing.Size(468, 531);
			this.WorkspacePage.TabIndex = 1;
			this.WorkspacePage.Text = "WorkspacePage";
			// 
			// MainTabControl
			// 
			this.MainTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.MainTabControl.Controls.Add(this.GeneralTabPage);
			this.MainTabControl.Controls.Add(this.tabPage2);
			this.MainTabControl.Location = new System.Drawing.Point(12, 12);
			this.MainTabControl.Name = "MainTabControl";
			this.MainTabControl.SelectedIndex = 0;
			this.MainTabControl.Size = new System.Drawing.Size(444, 507);
			this.MainTabControl.TabIndex = 0;
			// 
			// GeneralTabPage
			// 
			this.GeneralTabPage.Controls.Add(this.tabControl1);
			this.GeneralTabPage.Controls.Add(this.label2);
			this.GeneralTabPage.Controls.Add(this.DeviceNameLabel);
			this.GeneralTabPage.Controls.Add(this.BootModeTextBox);
			this.GeneralTabPage.Controls.Add(this.label4);
			this.GeneralTabPage.Controls.Add(this.label5);
			this.GeneralTabPage.Controls.Add(this.FirmwareVersionTextBox);
			this.GeneralTabPage.Controls.Add(this.HardwareVersionTextBox);
			this.GeneralTabPage.Location = new System.Drawing.Point(4, 22);
			this.GeneralTabPage.Name = "GeneralTabPage";
			this.GeneralTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.GeneralTabPage.Size = new System.Drawing.Size(436, 481);
			this.GeneralTabPage.TabIndex = 0;
			this.GeneralTabPage.Text = "General";
			this.GeneralTabPage.UseVisualStyleBackColor = true;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.SettingsTabPage);
			this.tabControl1.Controls.Add(this.TCRTabPage);
			this.tabControl1.ItemSize = new System.Drawing.Size(70, 18);
			this.tabControl1.Location = new System.Drawing.Point(11, 121);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(414, 352);
			this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
			this.tabControl1.TabIndex = 33;
			// 
			// SettingsTabPage
			// 
			this.SettingsTabPage.Controls.Add(this.label16);
			this.SettingsTabPage.Controls.Add(this.label15);
			this.SettingsTabPage.Controls.Add(this.label14);
			this.SettingsTabPage.Controls.Add(this.label13);
			this.SettingsTabPage.Controls.Add(this.label12);
			this.SettingsTabPage.Controls.Add(this.label11);
			this.SettingsTabPage.Controls.Add(this.ResistanceTCRCheckBox);
			this.SettingsTabPage.Controls.Add(this.ResistanceSSCheckBox);
			this.SettingsTabPage.Controls.Add(this.ResistanceTiCheckBox);
			this.SettingsTabPage.Controls.Add(this.ResistanceNiCheckBox);
			this.SettingsTabPage.Controls.Add(this.label10);
			this.SettingsTabPage.Controls.Add(this.ResistanceTCRTextBox);
			this.SettingsTabPage.Controls.Add(this.label9);
			this.SettingsTabPage.Controls.Add(this.ResistanceSSTextBox);
			this.SettingsTabPage.Controls.Add(this.label8);
			this.SettingsTabPage.Controls.Add(this.ResistanceTiTextBox);
			this.SettingsTabPage.Controls.Add(this.label7);
			this.SettingsTabPage.Controls.Add(this.ResistanceNiTextBox);
			this.SettingsTabPage.Controls.Add(this.TemperatureComboBox);
			this.SettingsTabPage.Controls.Add(this.label6);
			this.SettingsTabPage.Controls.Add(this.TemperatureTextBox);
			this.SettingsTabPage.Controls.Add(this.label3);
			this.SettingsTabPage.Controls.Add(this.TCPowerTextBox);
			this.SettingsTabPage.Controls.Add(this.label1);
			this.SettingsTabPage.Controls.Add(this.PowerTextBox);
			this.SettingsTabPage.Location = new System.Drawing.Point(4, 22);
			this.SettingsTabPage.Name = "SettingsTabPage";
			this.SettingsTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.SettingsTabPage.Size = new System.Drawing.Size(406, 326);
			this.SettingsTabPage.TabIndex = 0;
			this.SettingsTabPage.Text = "Settings";
			this.SettingsTabPage.UseVisualStyleBackColor = true;
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label16.Location = new System.Drawing.Point(313, 172);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(17, 16);
			this.label16.TabIndex = 54;
			this.label16.Text = "Ω";
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label15.Location = new System.Drawing.Point(313, 145);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(17, 16);
			this.label15.TabIndex = 54;
			this.label15.Text = "Ω";
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label14.Location = new System.Drawing.Point(313, 118);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(17, 16);
			this.label14.TabIndex = 54;
			this.label14.Text = "Ω";
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label13.Location = new System.Drawing.Point(313, 91);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(17, 16);
			this.label13.TabIndex = 53;
			this.label13.Text = "Ω";
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(313, 38);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(17, 13);
			this.label12.TabIndex = 52;
			this.label12.Text = "W";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(313, 11);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(17, 13);
			this.label11.TabIndex = 51;
			this.label11.Text = "W";
			// 
			// ResistanceTCRCheckBox
			// 
			this.ResistanceTCRCheckBox.AutoSize = true;
			this.ResistanceTCRCheckBox.Location = new System.Drawing.Point(342, 173);
			this.ResistanceTCRCheckBox.Name = "ResistanceTCRCheckBox";
			this.ResistanceTCRCheckBox.Size = new System.Drawing.Size(59, 17);
			this.ResistanceTCRCheckBox.TabIndex = 50;
			this.ResistanceTCRCheckBox.Text = "Locked";
			this.ResistanceTCRCheckBox.UseVisualStyleBackColor = true;
			// 
			// ResistanceSSCheckBox
			// 
			this.ResistanceSSCheckBox.AutoSize = true;
			this.ResistanceSSCheckBox.Location = new System.Drawing.Point(342, 146);
			this.ResistanceSSCheckBox.Name = "ResistanceSSCheckBox";
			this.ResistanceSSCheckBox.Size = new System.Drawing.Size(59, 17);
			this.ResistanceSSCheckBox.TabIndex = 50;
			this.ResistanceSSCheckBox.Text = "Locked";
			this.ResistanceSSCheckBox.UseVisualStyleBackColor = true;
			// 
			// ResistanceTiCheckBox
			// 
			this.ResistanceTiCheckBox.AutoSize = true;
			this.ResistanceTiCheckBox.Location = new System.Drawing.Point(342, 119);
			this.ResistanceTiCheckBox.Name = "ResistanceTiCheckBox";
			this.ResistanceTiCheckBox.Size = new System.Drawing.Size(59, 17);
			this.ResistanceTiCheckBox.TabIndex = 50;
			this.ResistanceTiCheckBox.Text = "Locked";
			this.ResistanceTiCheckBox.UseVisualStyleBackColor = true;
			// 
			// ResistanceNiCheckBox
			// 
			this.ResistanceNiCheckBox.AutoSize = true;
			this.ResistanceNiCheckBox.Location = new System.Drawing.Point(342, 92);
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
			this.ResistanceTCRTextBox.Size = new System.Drawing.Size(206, 21);
			this.ResistanceTCRTextBox.TabIndex = 47;
			this.ResistanceTCRTextBox.TabStop = false;
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
			this.ResistanceSSTextBox.Size = new System.Drawing.Size(206, 21);
			this.ResistanceSSTextBox.TabIndex = 45;
			this.ResistanceSSTextBox.TabStop = false;
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
			this.ResistanceTiTextBox.Size = new System.Drawing.Size(206, 21);
			this.ResistanceTiTextBox.TabIndex = 43;
			this.ResistanceTiTextBox.TabStop = false;
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
			this.ResistanceNiTextBox.Size = new System.Drawing.Size(206, 21);
			this.ResistanceNiTextBox.TabIndex = 41;
			this.ResistanceNiTextBox.TabStop = false;
			// 
			// TemperatureComboBox
			// 
			this.TemperatureComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.TemperatureComboBox.FormattingEnabled = true;
			this.TemperatureComboBox.Items.AddRange(new object[] {
            "°C",
            "°F"});
			this.TemperatureComboBox.Location = new System.Drawing.Point(313, 62);
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
			this.TemperatureTextBox.Size = new System.Drawing.Size(206, 21);
			this.TemperatureTextBox.TabIndex = 38;
			this.TemperatureTextBox.TabStop = false;
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
			this.TCPowerTextBox.Size = new System.Drawing.Size(206, 21);
			this.TCPowerTextBox.TabIndex = 36;
			this.TCPowerTextBox.TabStop = false;
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
			this.PowerTextBox.Size = new System.Drawing.Size(206, 21);
			this.PowerTextBox.TabIndex = 34;
			this.PowerTextBox.TabStop = false;
			// 
			// TCRTabPage
			// 
			this.TCRTabPage.Location = new System.Drawing.Point(4, 22);
			this.TCRTabPage.Name = "TCRTabPage";
			this.TCRTabPage.Size = new System.Drawing.Size(406, 326);
			this.TCRTabPage.TabIndex = 4;
			this.TCRTabPage.Text = "TCR";
			this.TCRTabPage.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(9, 95);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(62, 13);
			this.label2.TabIndex = 32;
			this.label2.Text = "Boot mode:";
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
			// BootModeTextBox
			// 
			this.BootModeTextBox.Location = new System.Drawing.Point(106, 92);
			this.BootModeTextBox.Name = "BootModeTextBox";
			this.BootModeTextBox.Size = new System.Drawing.Size(216, 21);
			this.BootModeTextBox.TabIndex = 31;
			this.BootModeTextBox.TabStop = false;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(9, 41);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(93, 13);
			this.label4.TabIndex = 29;
			this.label4.Text = "Firmware version:";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(9, 68);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(96, 13);
			this.label5.TabIndex = 30;
			this.label5.Text = "Hardware version:";
			// 
			// FirmwareVersionTextBox
			// 
			this.FirmwareVersionTextBox.Location = new System.Drawing.Point(106, 38);
			this.FirmwareVersionTextBox.Name = "FirmwareVersionTextBox";
			this.FirmwareVersionTextBox.Size = new System.Drawing.Size(216, 21);
			this.FirmwareVersionTextBox.TabIndex = 27;
			this.FirmwareVersionTextBox.TabStop = false;
			// 
			// HardwareVersionTextBox
			// 
			this.HardwareVersionTextBox.Location = new System.Drawing.Point(106, 65);
			this.HardwareVersionTextBox.Name = "HardwareVersionTextBox";
			this.HardwareVersionTextBox.Size = new System.Drawing.Size(216, 21);
			this.HardwareVersionTextBox.TabIndex = 28;
			this.HardwareVersionTextBox.TabStop = false;
			// 
			// tabPage2
			// 
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(436, 481);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "tabPage2";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// DeviceConfiguratorWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(468, 531);
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
			this.tabControl1.ResumeLayout(false);
			this.SettingsTabPage.ResumeLayout(false);
			this.SettingsTabPage.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private UI.MultiPanel MainContainer;
		private UI.MultiPanelPage WelcomePage;
		private UI.MultiPanelPage WorkspacePage;
		private System.Windows.Forms.Label WelcomeLabel;
		private System.Windows.Forms.TabControl MainTabControl;
		private System.Windows.Forms.TabPage GeneralTabPage;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.Label DeviceNameLabel;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox BootModeTextBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox FirmwareVersionTextBox;
		private System.Windows.Forms.TextBox HardwareVersionTextBox;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage SettingsTabPage;
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
	}
}