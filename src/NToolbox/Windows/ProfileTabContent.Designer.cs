namespace NToolbox.Windows
{
	partial class ProfileTabContent
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.ProfileNameTextBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.TemperatureUpDown = new System.Windows.Forms.NumericUpDown();
			this.TemperatureDominantCheckBox = new System.Windows.Forms.CheckBox();
			this.TemperatureLabel = new System.Windows.Forms.Label();
			this.TemperatureTypeComboBox = new System.Windows.Forms.ComboBox();
			this.PreheatTimeUpDown = new System.Windows.Forms.NumericUpDown();
			this.PreheatPowerUpDown = new System.Windows.Forms.NumericUpDown();
			this.label5 = new System.Windows.Forms.Label();
			this.label29 = new System.Windows.Forms.Label();
			this.PreheatTypeComboBox = new System.Windows.Forms.ComboBox();
			this.PreheatPowerLabel = new System.Windows.Forms.Label();
			this.PowerUpDown = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.ResistanceLabel = new System.Windows.Forms.Label();
			this.ResistanceUpDown = new System.Windows.Forms.NumericUpDown();
			this.OhmLabel = new System.Windows.Forms.Label();
			this.ResistanceLockedCheckBox = new System.Windows.Forms.CheckBox();
			this.MaterialLabel = new System.Windows.Forms.Label();
			this.MaterialComboBox = new System.Windows.Forms.ComboBox();
			this.TCRUpDown = new System.Windows.Forms.NumericUpDown();
			this.PreheatDelayUpDown = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.ModeComboBox = new System.Windows.Forms.ComboBox();
			this.PowerCurveComboBox = new System.Windows.Forms.ComboBox();
			this.PowerCurveEditButton = new System.Windows.Forms.Button();
			this.TFRCurveEditButton = new System.Windows.Forms.Button();
			this.label7 = new System.Windows.Forms.Label();
			this.PreheatPowerUnitLabel = new System.Windows.Forms.Label();
			this.SetupTempControlButton = new System.Windows.Forms.Button();
			this.label37 = new System.Windows.Forms.Label();
			this.ProfileEnabledCheckBox = new System.Windows.Forms.CheckBox();
			this.ProfileLocalizationExtender = new NCore.UI.LocalizationExtender(this.components);
			((System.ComponentModel.ISupportInitialize)(this.TemperatureUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PreheatTimeUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PreheatPowerUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PowerUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ResistanceUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.TCRUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PreheatDelayUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// ProfileNameTextBox
			// 
			this.ProfileNameTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			this.ProfileLocalizationExtender.SetKey(this.ProfileNameTextBox, "");
			this.ProfileNameTextBox.Location = new System.Drawing.Point(101, 35);
			this.ProfileNameTextBox.MaxLength = 8;
			this.ProfileNameTextBox.Name = "ProfileNameTextBox";
			this.ProfileNameTextBox.Size = new System.Drawing.Size(106, 21);
			this.ProfileNameTextBox.TabIndex = 0;
			this.ProfileNameTextBox.Text = "12345678";
			this.ProfileNameTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.ProfileLocalizationExtender.SetKey(this.label1, "Toolbox.ArcticFoxConfiguration.Profile.NameLabel");
			this.label1.Location = new System.Drawing.Point(4, 38);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(38, 13);
			this.label1.TabIndex = 36;
			this.label1.Text = "Name:";
			// 
			// TemperatureUpDown
			// 
			this.ProfileLocalizationExtender.SetKey(this.TemperatureUpDown, "");
			this.TemperatureUpDown.Location = new System.Drawing.Point(101, 301);
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
			this.TemperatureUpDown.TabIndex = 71;
			this.TemperatureUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.TemperatureUpDown.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
			// 
			// TemperatureDominantCheckBox
			// 
			this.TemperatureDominantCheckBox.AutoSize = true;
			this.ProfileLocalizationExtender.SetKey(this.TemperatureDominantCheckBox, "Toolbox.ArcticFoxConfiguration.Profile.TDomCheckBox");
			this.TemperatureDominantCheckBox.Location = new System.Drawing.Point(101, 328);
			this.TemperatureDominantCheckBox.Name = "TemperatureDominantCheckBox";
			this.TemperatureDominantCheckBox.Size = new System.Drawing.Size(136, 17);
			this.TemperatureDominantCheckBox.TabIndex = 70;
			this.TemperatureDominantCheckBox.Text = "Temperature Dominant";
			this.TemperatureDominantCheckBox.UseVisualStyleBackColor = true;
			// 
			// TemperatureLabel
			// 
			this.TemperatureLabel.AutoSize = true;
			this.ProfileLocalizationExtender.SetKey(this.TemperatureLabel, "Toolbox.ArcticFoxConfiguration.Profile.TemperatureLabel");
			this.TemperatureLabel.Location = new System.Drawing.Point(4, 304);
			this.TemperatureLabel.Name = "TemperatureLabel";
			this.TemperatureLabel.Size = new System.Drawing.Size(73, 13);
			this.TemperatureLabel.TabIndex = 68;
			this.TemperatureLabel.Text = "Temperature:";
			// 
			// TemperatureTypeComboBox
			// 
			this.TemperatureTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.TemperatureTypeComboBox.FormattingEnabled = true;
			this.ProfileLocalizationExtender.SetKey(this.TemperatureTypeComboBox, "");
			this.TemperatureTypeComboBox.Location = new System.Drawing.Point(213, 301);
			this.TemperatureTypeComboBox.Name = "TemperatureTypeComboBox";
			this.TemperatureTypeComboBox.Size = new System.Drawing.Size(42, 21);
			this.TemperatureTypeComboBox.TabIndex = 69;
			// 
			// PreheatTimeUpDown
			// 
			this.PreheatTimeUpDown.DecimalPlaces = 1;
			this.PreheatTimeUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
			this.ProfileLocalizationExtender.SetKey(this.PreheatTimeUpDown, "");
			this.PreheatTimeUpDown.Location = new System.Drawing.Point(101, 143);
			this.PreheatTimeUpDown.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
			this.PreheatTimeUpDown.Name = "PreheatTimeUpDown";
			this.PreheatTimeUpDown.Size = new System.Drawing.Size(106, 21);
			this.PreheatTimeUpDown.TabIndex = 77;
			this.PreheatTimeUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// PreheatPowerUpDown
			// 
			this.PreheatPowerUpDown.DecimalPlaces = 1;
			this.ProfileLocalizationExtender.SetKey(this.PreheatPowerUpDown, "");
			this.PreheatPowerUpDown.Location = new System.Drawing.Point(101, 116);
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
			this.PreheatPowerUpDown.TabIndex = 76;
			this.PreheatPowerUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.PreheatPowerUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.ProfileLocalizationExtender.SetKey(this.label5, "Toolbox.ArcticFoxConfiguration.SecondsLabel");
			this.label5.Location = new System.Drawing.Point(213, 146);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(12, 13);
			this.label5.TabIndex = 75;
			this.label5.Text = "s";
			// 
			// label29
			// 
			this.label29.AutoSize = true;
			this.ProfileLocalizationExtender.SetKey(this.label29, "Toolbox.ArcticFoxConfiguration.Profile.PreheatTimeLabel");
			this.label29.Location = new System.Drawing.Point(4, 146);
			this.label29.Name = "label29";
			this.label29.Size = new System.Drawing.Size(74, 13);
			this.label29.TabIndex = 74;
			this.label29.Text = "Preheat Time:";
			// 
			// PreheatTypeComboBox
			// 
			this.PreheatTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.PreheatTypeComboBox.FormattingEnabled = true;
			this.ProfileLocalizationExtender.SetKey(this.PreheatTypeComboBox, "");
			this.PreheatTypeComboBox.Location = new System.Drawing.Point(101, 88);
			this.PreheatTypeComboBox.Name = "PreheatTypeComboBox";
			this.PreheatTypeComboBox.Size = new System.Drawing.Size(106, 21);
			this.PreheatTypeComboBox.TabIndex = 73;
			// 
			// PreheatPowerLabel
			// 
			this.PreheatPowerLabel.AutoSize = true;
			this.ProfileLocalizationExtender.SetKey(this.PreheatPowerLabel, "Toolbox.ArcticFoxConfiguration.Profile.PreheatPowerLabel");
			this.PreheatPowerLabel.Location = new System.Drawing.Point(4, 119);
			this.PreheatPowerLabel.Name = "PreheatPowerLabel";
			this.PreheatPowerLabel.Size = new System.Drawing.Size(82, 13);
			this.PreheatPowerLabel.TabIndex = 72;
			this.PreheatPowerLabel.Text = "Preheat Power:";
			// 
			// PowerUpDown
			// 
			this.PowerUpDown.DecimalPlaces = 1;
			this.PowerUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
			this.ProfileLocalizationExtender.SetKey(this.PowerUpDown, "");
			this.PowerUpDown.Location = new System.Drawing.Point(101, 62);
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
			this.PowerUpDown.TabIndex = 80;
			this.PowerUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.PowerUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.ProfileLocalizationExtender.SetKey(this.label3, "Toolbox.ArcticFoxConfiguration.Profile.PowerLabel");
			this.label3.Location = new System.Drawing.Point(4, 65);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(41, 13);
			this.label3.TabIndex = 78;
			this.label3.Text = "Power:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.ProfileLocalizationExtender.SetKey(this.label4, "Toolbox.ArcticFoxConfiguration.Profile.WattsLabel");
			this.label4.Location = new System.Drawing.Point(213, 65);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(17, 13);
			this.label4.TabIndex = 79;
			this.label4.Text = "W";
			// 
			// ResistanceLabel
			// 
			this.ResistanceLabel.AutoSize = true;
			this.ProfileLocalizationExtender.SetKey(this.ResistanceLabel, "Toolbox.ArcticFoxConfiguration.Profile.ResistanceLabel");
			this.ResistanceLabel.Location = new System.Drawing.Point(4, 254);
			this.ResistanceLabel.Name = "ResistanceLabel";
			this.ResistanceLabel.Size = new System.Drawing.Size(63, 13);
			this.ResistanceLabel.TabIndex = 84;
			this.ResistanceLabel.Text = "Resistance:";
			// 
			// ResistanceUpDown
			// 
			this.ResistanceUpDown.DecimalPlaces = 3;
			this.ResistanceUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
			this.ProfileLocalizationExtender.SetKey(this.ResistanceUpDown, "");
			this.ResistanceUpDown.Location = new System.Drawing.Point(101, 251);
			this.ResistanceUpDown.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
			this.ResistanceUpDown.Name = "ResistanceUpDown";
			this.ResistanceUpDown.Size = new System.Drawing.Size(106, 21);
			this.ResistanceUpDown.TabIndex = 83;
			this.ResistanceUpDown.TabStop = false;
			this.ResistanceUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.ResistanceUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            131072});
			// 
			// OhmLabel
			// 
			this.OhmLabel.AutoSize = true;
			this.OhmLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ProfileLocalizationExtender.SetKey(this.OhmLabel, "");
			this.OhmLabel.Location = new System.Drawing.Point(213, 253);
			this.OhmLabel.Name = "OhmLabel";
			this.OhmLabel.Size = new System.Drawing.Size(17, 16);
			this.OhmLabel.TabIndex = 86;
			this.OhmLabel.Text = "Ω";
			// 
			// ResistanceLockedCheckBox
			// 
			this.ResistanceLockedCheckBox.AutoSize = true;
			this.ProfileLocalizationExtender.SetKey(this.ResistanceLockedCheckBox, "Toolbox.ArcticFoxConfiguration.Profile.LockedCheckBox");
			this.ResistanceLockedCheckBox.Location = new System.Drawing.Point(101, 278);
			this.ResistanceLockedCheckBox.Name = "ResistanceLockedCheckBox";
			this.ResistanceLockedCheckBox.Size = new System.Drawing.Size(59, 17);
			this.ResistanceLockedCheckBox.TabIndex = 85;
			this.ResistanceLockedCheckBox.Text = "Locked";
			this.ResistanceLockedCheckBox.UseVisualStyleBackColor = true;
			// 
			// MaterialLabel
			// 
			this.MaterialLabel.AutoSize = true;
			this.ProfileLocalizationExtender.SetKey(this.MaterialLabel, "Toolbox.ArcticFoxConfiguration.Profile.CoilMaterialLabel");
			this.MaterialLabel.Location = new System.Drawing.Point(4, 227);
			this.MaterialLabel.Name = "MaterialLabel";
			this.MaterialLabel.Size = new System.Drawing.Size(69, 13);
			this.MaterialLabel.TabIndex = 87;
			this.MaterialLabel.Text = "Coil Material:";
			// 
			// MaterialComboBox
			// 
			this.MaterialComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.MaterialComboBox.FormattingEnabled = true;
			this.ProfileLocalizationExtender.SetKey(this.MaterialComboBox, "");
			this.MaterialComboBox.Location = new System.Drawing.Point(101, 224);
			this.MaterialComboBox.Name = "MaterialComboBox";
			this.MaterialComboBox.Size = new System.Drawing.Size(106, 21);
			this.MaterialComboBox.TabIndex = 88;
			// 
			// TCRUpDown
			// 
			this.ProfileLocalizationExtender.SetKey(this.TCRUpDown, "");
			this.TCRUpDown.Location = new System.Drawing.Point(214, 224);
			this.TCRUpDown.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
			this.TCRUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.TCRUpDown.Name = "TCRUpDown";
			this.TCRUpDown.Size = new System.Drawing.Size(42, 21);
			this.TCRUpDown.TabIndex = 91;
			this.TCRUpDown.TabStop = false;
			this.TCRUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.TCRUpDown.Value = new decimal(new int[] {
            999,
            0,
            0,
            0});
			// 
			// PreheatDelayUpDown
			// 
			this.PreheatDelayUpDown.DecimalPlaces = 1;
			this.PreheatDelayUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
			this.ProfileLocalizationExtender.SetKey(this.PreheatDelayUpDown, "");
			this.PreheatDelayUpDown.Location = new System.Drawing.Point(101, 170);
			this.PreheatDelayUpDown.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
			this.PreheatDelayUpDown.Name = "PreheatDelayUpDown";
			this.PreheatDelayUpDown.Size = new System.Drawing.Size(106, 21);
			this.PreheatDelayUpDown.TabIndex = 95;
			this.PreheatDelayUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.ProfileLocalizationExtender.SetKey(this.label2, "Toolbox.ArcticFoxConfiguration.SecondsLabel");
			this.label2.Location = new System.Drawing.Point(213, 173);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(12, 13);
			this.label2.TabIndex = 94;
			this.label2.Text = "s";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.ProfileLocalizationExtender.SetKey(this.label8, "Toolbox.ArcticFoxConfiguration.Profile.PreheatDelayLabel");
			this.label8.Location = new System.Drawing.Point(4, 173);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(79, 13);
			this.label8.TabIndex = 93;
			this.label8.Text = "Preheat Delay:";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.ProfileLocalizationExtender.SetKey(this.label6, "");
			this.label6.Location = new System.Drawing.Point(213, 38);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(78, 13);
			this.label6.TabIndex = 96;
			this.label6.Text = "0-9 A-Z + - . \' \'";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.ProfileLocalizationExtender.SetKey(this.label9, "Toolbox.ArcticFoxConfiguration.Profile.ModeLabel");
			this.label9.Location = new System.Drawing.Point(4, 200);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(37, 13);
			this.label9.TabIndex = 97;
			this.label9.Text = "Mode:";
			// 
			// ModeComboBox
			// 
			this.ModeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ModeComboBox.FormattingEnabled = true;
			this.ProfileLocalizationExtender.SetKey(this.ModeComboBox, "");
			this.ModeComboBox.Location = new System.Drawing.Point(101, 197);
			this.ModeComboBox.Name = "ModeComboBox";
			this.ModeComboBox.Size = new System.Drawing.Size(106, 21);
			this.ModeComboBox.TabIndex = 98;
			// 
			// PowerCurveComboBox
			// 
			this.PowerCurveComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.PowerCurveComboBox.FormattingEnabled = true;
			this.ProfileLocalizationExtender.SetKey(this.PowerCurveComboBox, "");
			this.PowerCurveComboBox.Location = new System.Drawing.Point(101, 116);
			this.PowerCurveComboBox.Name = "PowerCurveComboBox";
			this.PowerCurveComboBox.Size = new System.Drawing.Size(106, 21);
			this.PowerCurveComboBox.TabIndex = 99;
			this.PowerCurveComboBox.Visible = false;
			// 
			// PowerCurveEditButton
			// 
			this.ProfileLocalizationExtender.SetKey(this.PowerCurveEditButton, "Toolbox.ArcticFoxConfiguration.Profile.PowerCurveEditButton");
			this.PowerCurveEditButton.Location = new System.Drawing.Point(213, 115);
			this.PowerCurveEditButton.Name = "PowerCurveEditButton";
			this.PowerCurveEditButton.Size = new System.Drawing.Size(60, 23);
			this.PowerCurveEditButton.TabIndex = 100;
			this.PowerCurveEditButton.Text = "Edit";
			this.PowerCurveEditButton.UseVisualStyleBackColor = true;
			this.PowerCurveEditButton.Visible = false;
			// 
			// TFRCurveEditButton
			// 
			this.ProfileLocalizationExtender.SetKey(this.TFRCurveEditButton, "Toolbox.ArcticFoxConfiguration.Profile.TFREditButton");
			this.TFRCurveEditButton.Location = new System.Drawing.Point(213, 223);
			this.TFRCurveEditButton.Name = "TFRCurveEditButton";
			this.TFRCurveEditButton.Size = new System.Drawing.Size(60, 23);
			this.TFRCurveEditButton.TabIndex = 101;
			this.TFRCurveEditButton.Text = "Edit";
			this.TFRCurveEditButton.UseVisualStyleBackColor = true;
			this.TFRCurveEditButton.Visible = false;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.ProfileLocalizationExtender.SetKey(this.label7, "Toolbox.ArcticFoxConfiguration.Profile.PreheatTypeLabel");
			this.label7.Location = new System.Drawing.Point(4, 92);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(76, 13);
			this.label7.TabIndex = 102;
			this.label7.Text = "Preheat Type:";
			// 
			// PreheatPowerUnitLabel
			// 
			this.PreheatPowerUnitLabel.AutoSize = true;
			this.ProfileLocalizationExtender.SetKey(this.PreheatPowerUnitLabel, "");
			this.PreheatPowerUnitLabel.Location = new System.Drawing.Point(213, 119);
			this.PreheatPowerUnitLabel.Name = "PreheatPowerUnitLabel";
			this.PreheatPowerUnitLabel.Size = new System.Drawing.Size(18, 13);
			this.PreheatPowerUnitLabel.TabIndex = 103;
			this.PreheatPowerUnitLabel.Text = "%";
			// 
			// SetupTempControlButton
			// 
			this.ProfileLocalizationExtender.SetKey(this.SetupTempControlButton, "Toolbox.ArcticFoxConfiguration.Profile.TCSetupButton");
			this.SetupTempControlButton.Location = new System.Drawing.Point(213, 196);
			this.SetupTempControlButton.Name = "SetupTempControlButton";
			this.SetupTempControlButton.Size = new System.Drawing.Size(60, 23);
			this.SetupTempControlButton.TabIndex = 104;
			this.SetupTempControlButton.Text = "Setup";
			this.SetupTempControlButton.UseVisualStyleBackColor = true;
			this.SetupTempControlButton.Visible = false;
			// 
			// label37
			// 
			this.label37.AutoSize = true;
			this.ProfileLocalizationExtender.SetKey(this.label37, "Toolbox.ArcticFoxConfiguration.Profile.ProfileLabel");
			this.label37.Location = new System.Drawing.Point(4, 11);
			this.label37.Name = "label37";
			this.label37.Size = new System.Drawing.Size(41, 13);
			this.label37.TabIndex = 105;
			this.label37.Text = "Profile:";
			// 
			// ProfileEnabledCheckBox
			// 
			this.ProfileEnabledCheckBox.AutoSize = true;
			this.ProfileLocalizationExtender.SetKey(this.ProfileEnabledCheckBox, "Toolbox.ArcticFoxConfiguration.EnabledCheckBox");
			this.ProfileEnabledCheckBox.Location = new System.Drawing.Point(101, 11);
			this.ProfileEnabledCheckBox.Name = "ProfileEnabledCheckBox";
			this.ProfileEnabledCheckBox.Size = new System.Drawing.Size(64, 17);
			this.ProfileEnabledCheckBox.TabIndex = 106;
			this.ProfileEnabledCheckBox.Text = "Enabled";
			this.ProfileEnabledCheckBox.UseVisualStyleBackColor = true;
			// 
			// ProfileTabContent
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.label37);
			this.Controls.Add(this.ProfileEnabledCheckBox);
			this.Controls.Add(this.SetupTempControlButton);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.TFRCurveEditButton);
			this.Controls.Add(this.PowerCurveEditButton);
			this.Controls.Add(this.PowerCurveComboBox);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.ModeComboBox);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.PreheatDelayUpDown);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.TCRUpDown);
			this.Controls.Add(this.MaterialLabel);
			this.Controls.Add(this.MaterialComboBox);
			this.Controls.Add(this.ResistanceLabel);
			this.Controls.Add(this.ResistanceUpDown);
			this.Controls.Add(this.OhmLabel);
			this.Controls.Add(this.ResistanceLockedCheckBox);
			this.Controls.Add(this.PowerUpDown);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.PreheatTimeUpDown);
			this.Controls.Add(this.PreheatPowerUpDown);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label29);
			this.Controls.Add(this.PreheatTypeComboBox);
			this.Controls.Add(this.PreheatPowerLabel);
			this.Controls.Add(this.TemperatureUpDown);
			this.Controls.Add(this.TemperatureDominantCheckBox);
			this.Controls.Add(this.TemperatureLabel);
			this.Controls.Add(this.TemperatureTypeComboBox);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.ProfileNameTextBox);
			this.Controls.Add(this.PreheatPowerUnitLabel);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.ProfileLocalizationExtender.SetKey(this, "");
			this.Name = "ProfileTabContent";
			this.Size = new System.Drawing.Size(328, 350);
			((System.ComponentModel.ISupportInitialize)(this.TemperatureUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.PreheatTimeUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.PreheatPowerUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.PowerUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ResistanceUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.TCRUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.PreheatDelayUpDown)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label TemperatureLabel;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label29;
		private System.Windows.Forms.Label PreheatPowerLabel;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label ResistanceLabel;
		private System.Windows.Forms.Label OhmLabel;
		private System.Windows.Forms.Label MaterialLabel;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Button PowerCurveEditButton;
		private System.Windows.Forms.Button TFRCurveEditButton;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label PreheatPowerUnitLabel;
		private System.Windows.Forms.Button SetupTempControlButton;
		private System.Windows.Forms.Label label37;
		private System.Windows.Forms.TextBox ProfileNameTextBox;
		private System.Windows.Forms.NumericUpDown TemperatureUpDown;
		private System.Windows.Forms.CheckBox TemperatureDominantCheckBox;
		private System.Windows.Forms.ComboBox TemperatureTypeComboBox;
		private System.Windows.Forms.NumericUpDown PreheatTimeUpDown;
		private System.Windows.Forms.NumericUpDown PreheatPowerUpDown;
		private System.Windows.Forms.ComboBox PreheatTypeComboBox;
		private System.Windows.Forms.NumericUpDown PowerUpDown;
		private System.Windows.Forms.NumericUpDown ResistanceUpDown;
		private System.Windows.Forms.CheckBox ResistanceLockedCheckBox;
		private System.Windows.Forms.ComboBox MaterialComboBox;
		private System.Windows.Forms.NumericUpDown TCRUpDown;
		private System.Windows.Forms.NumericUpDown PreheatDelayUpDown;
		private System.Windows.Forms.ComboBox ModeComboBox;
		private System.Windows.Forms.ComboBox PowerCurveComboBox;
		private System.Windows.Forms.CheckBox ProfileEnabledCheckBox;
		private NCore.UI.LocalizationExtender ProfileLocalizationExtender;
	}
}
