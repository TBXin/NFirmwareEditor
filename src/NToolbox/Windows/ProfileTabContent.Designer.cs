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
			this.label28 = new System.Windows.Forms.Label();
			this.PowerUpDown = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.ResistanceUpDown = new System.Windows.Forms.NumericUpDown();
			this.label13 = new System.Windows.Forms.Label();
			this.ResistanceLockedCheckBox = new System.Windows.Forms.CheckBox();
			this.label21 = new System.Windows.Forms.Label();
			this.MaterialComboBox = new System.Windows.Forms.ComboBox();
			this.label17 = new System.Windows.Forms.Label();
			this.TCRUpDown = new System.Windows.Forms.NumericUpDown();
			this.PreheatDelayUpDown = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
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
			this.ProfileNameTextBox.Location = new System.Drawing.Point(101, 8);
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
			this.label1.Location = new System.Drawing.Point(4, 11);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(38, 13);
			this.label1.TabIndex = 36;
			this.label1.Text = "Name:";
			// 
			// TemperatureUpDown
			// 
			this.TemperatureUpDown.Location = new System.Drawing.Point(101, 287);
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
			this.TemperatureDominantCheckBox.Location = new System.Drawing.Point(101, 314);
			this.TemperatureDominantCheckBox.Name = "TemperatureDominantCheckBox";
			this.TemperatureDominantCheckBox.Size = new System.Drawing.Size(136, 17);
			this.TemperatureDominantCheckBox.TabIndex = 70;
			this.TemperatureDominantCheckBox.Text = "Temperature Dominant";
			this.TemperatureDominantCheckBox.UseVisualStyleBackColor = true;
			// 
			// TemperatureLabel
			// 
			this.TemperatureLabel.AutoSize = true;
			this.TemperatureLabel.Location = new System.Drawing.Point(4, 290);
			this.TemperatureLabel.Name = "TemperatureLabel";
			this.TemperatureLabel.Size = new System.Drawing.Size(73, 13);
			this.TemperatureLabel.TabIndex = 68;
			this.TemperatureLabel.Text = "Temperature:";
			// 
			// TemperatureTypeComboBox
			// 
			this.TemperatureTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.TemperatureTypeComboBox.FormattingEnabled = true;
			this.TemperatureTypeComboBox.Items.AddRange(new object[] {
            "°C",
            "°F"});
			this.TemperatureTypeComboBox.Location = new System.Drawing.Point(213, 287);
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
			this.PreheatTimeUpDown.Location = new System.Drawing.Point(101, 89);
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
			this.PreheatPowerUpDown.Location = new System.Drawing.Point(101, 62);
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
			this.label5.Location = new System.Drawing.Point(213, 92);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(12, 13);
			this.label5.TabIndex = 75;
			this.label5.Text = "s";
			// 
			// label29
			// 
			this.label29.AutoSize = true;
			this.label29.Location = new System.Drawing.Point(4, 92);
			this.label29.Name = "label29";
			this.label29.Size = new System.Drawing.Size(74, 13);
			this.label29.TabIndex = 74;
			this.label29.Text = "Preheat Time:";
			// 
			// PreheatTypeComboBox
			// 
			this.PreheatTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.PreheatTypeComboBox.FormattingEnabled = true;
			this.PreheatTypeComboBox.Items.AddRange(new object[] {
            "W",
            "%"});
			this.PreheatTypeComboBox.Location = new System.Drawing.Point(213, 62);
			this.PreheatTypeComboBox.Name = "PreheatTypeComboBox";
			this.PreheatTypeComboBox.Size = new System.Drawing.Size(42, 21);
			this.PreheatTypeComboBox.TabIndex = 73;
			// 
			// label28
			// 
			this.label28.AutoSize = true;
			this.label28.Location = new System.Drawing.Point(4, 65);
			this.label28.Name = "label28";
			this.label28.Size = new System.Drawing.Size(82, 13);
			this.label28.TabIndex = 72;
			this.label28.Text = "Preheat Power:";
			// 
			// PowerUpDown
			// 
			this.PowerUpDown.DecimalPlaces = 1;
			this.PowerUpDown.Location = new System.Drawing.Point(101, 35);
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
			this.label3.Location = new System.Drawing.Point(4, 38);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(41, 13);
			this.label3.TabIndex = 78;
			this.label3.Text = "Power:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(213, 38);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(17, 13);
			this.label4.TabIndex = 79;
			this.label4.Text = "W";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(4, 220);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(63, 13);
			this.label7.TabIndex = 84;
			this.label7.Text = "Resistance:";
			// 
			// ResistanceUpDown
			// 
			this.ResistanceUpDown.DecimalPlaces = 3;
			this.ResistanceUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
			this.ResistanceUpDown.Location = new System.Drawing.Point(101, 217);
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
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label13.Location = new System.Drawing.Point(213, 219);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(17, 16);
			this.label13.TabIndex = 86;
			this.label13.Text = "Ω";
			// 
			// ResistanceLockedCheckBox
			// 
			this.ResistanceLockedCheckBox.AutoSize = true;
			this.ResistanceLockedCheckBox.Location = new System.Drawing.Point(101, 244);
			this.ResistanceLockedCheckBox.Name = "ResistanceLockedCheckBox";
			this.ResistanceLockedCheckBox.Size = new System.Drawing.Size(59, 17);
			this.ResistanceLockedCheckBox.TabIndex = 85;
			this.ResistanceLockedCheckBox.Text = "Locked";
			this.ResistanceLockedCheckBox.UseVisualStyleBackColor = true;
			// 
			// label21
			// 
			this.label21.AutoSize = true;
			this.label21.Location = new System.Drawing.Point(4, 166);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(69, 13);
			this.label21.TabIndex = 87;
			this.label21.Text = "Coil Material:";
			// 
			// MaterialComboBox
			// 
			this.MaterialComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.MaterialComboBox.FormattingEnabled = true;
			this.MaterialComboBox.Items.AddRange(new object[] {
            "°C",
            "°F"});
			this.MaterialComboBox.Location = new System.Drawing.Point(101, 163);
			this.MaterialComboBox.Name = "MaterialComboBox";
			this.MaterialComboBox.Size = new System.Drawing.Size(106, 21);
			this.MaterialComboBox.TabIndex = 88;
			// 
			// label17
			// 
			this.label17.AutoSize = true;
			this.label17.Location = new System.Drawing.Point(4, 193);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(31, 13);
			this.label17.TabIndex = 92;
			this.label17.Text = "TCR:";
			// 
			// TCRUpDown
			// 
			this.TCRUpDown.Location = new System.Drawing.Point(101, 190);
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
			this.TCRUpDown.Size = new System.Drawing.Size(106, 21);
			this.TCRUpDown.TabIndex = 91;
			this.TCRUpDown.TabStop = false;
			this.TCRUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.TCRUpDown.Value = new decimal(new int[] {
            1,
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
			this.PreheatDelayUpDown.Location = new System.Drawing.Point(101, 116);
			this.PreheatDelayUpDown.Maximum = new decimal(new int[] {
            10,
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
			this.label2.Location = new System.Drawing.Point(213, 119);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(12, 13);
			this.label2.TabIndex = 94;
			this.label2.Text = "s";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(4, 119);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(79, 13);
			this.label8.TabIndex = 93;
			this.label8.Text = "Preheat Delay:";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(213, 11);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(76, 13);
			this.label6.TabIndex = 96;
			this.label6.Text = "0-9, A-Z, \'.\', \' \'";
			// 
			// ProfileTabContent
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.label6);
			this.Controls.Add(this.PreheatDelayUpDown);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label17);
			this.Controls.Add(this.TCRUpDown);
			this.Controls.Add(this.label21);
			this.Controls.Add(this.MaterialComboBox);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.ResistanceUpDown);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.ResistanceLockedCheckBox);
			this.Controls.Add(this.PowerUpDown);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.PreheatTimeUpDown);
			this.Controls.Add(this.PreheatPowerUpDown);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label29);
			this.Controls.Add(this.PreheatTypeComboBox);
			this.Controls.Add(this.label28);
			this.Controls.Add(this.TemperatureUpDown);
			this.Controls.Add(this.TemperatureDominantCheckBox);
			this.Controls.Add(this.TemperatureLabel);
			this.Controls.Add(this.TemperatureTypeComboBox);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.ProfileNameTextBox);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.Name = "ProfileTabContent";
			this.Size = new System.Drawing.Size(328, 340);
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

		public System.Windows.Forms.TextBox ProfileNameTextBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label TemperatureLabel;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label29;
		private System.Windows.Forms.Label label28;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.Label label17;
		public System.Windows.Forms.NumericUpDown TemperatureUpDown;
		public System.Windows.Forms.CheckBox TemperatureDominantCheckBox;
		public System.Windows.Forms.ComboBox TemperatureTypeComboBox;
		public System.Windows.Forms.NumericUpDown PreheatTimeUpDown;
		public System.Windows.Forms.NumericUpDown PreheatPowerUpDown;
		public System.Windows.Forms.ComboBox PreheatTypeComboBox;
		public System.Windows.Forms.NumericUpDown PowerUpDown;
		public System.Windows.Forms.NumericUpDown ResistanceUpDown;
		public System.Windows.Forms.CheckBox ResistanceLockedCheckBox;
		public System.Windows.Forms.ComboBox MaterialComboBox;
		public System.Windows.Forms.NumericUpDown TCRUpDown;
		public System.Windows.Forms.NumericUpDown PreheatDelayUpDown;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label6;
	}
}
