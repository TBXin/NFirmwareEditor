namespace NToolbox.Windows
{
	partial class TempControlSetupWindow
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
			this.ControlBorderedPanel = new NCore.UI.BorderedPanel();
			this.SaveButton = new System.Windows.Forms.Button();
			this.CancelButton = new System.Windows.Forms.Button();
			this.groupPanel1 = new NCore.UI.GroupPanel();
			this.ResistanceLabel = new System.Windows.Forms.Label();
			this.ResistanceLockedCheckBox = new System.Windows.Forms.CheckBox();
			this.PowerUpDown = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.ControlBorderedPanel.SuspendLayout();
			this.groupPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.PowerUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
			this.SuspendLayout();
			// 
			// ControlBorderedPanel
			// 
			this.ControlBorderedPanel.BackColor = System.Drawing.Color.Transparent;
			this.ControlBorderedPanel.BorderBottom = false;
			this.ControlBorderedPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(185)))), ((int)(((byte)(185)))));
			this.ControlBorderedPanel.BorderLeft = false;
			this.ControlBorderedPanel.BorderRight = false;
			this.ControlBorderedPanel.BorderTop = true;
			this.ControlBorderedPanel.BorderWidth = 1F;
			this.ControlBorderedPanel.Controls.Add(this.SaveButton);
			this.ControlBorderedPanel.Controls.Add(this.CancelButton);
			this.ControlBorderedPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.ControlBorderedPanel.Location = new System.Drawing.Point(0, 148);
			this.ControlBorderedPanel.Name = "ControlBorderedPanel";
			this.ControlBorderedPanel.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
			this.ControlBorderedPanel.Size = new System.Drawing.Size(254, 44);
			this.ControlBorderedPanel.TabIndex = 1;
			this.ControlBorderedPanel.Text = "borderedPanel1";
			// 
			// SaveButton
			// 
			this.SaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.SaveButton.Location = new System.Drawing.Point(44, 5);
			this.SaveButton.Name = "SaveButton";
			this.SaveButton.Size = new System.Drawing.Size(100, 35);
			this.SaveButton.TabIndex = 0;
			this.SaveButton.Text = "Save";
			this.SaveButton.UseVisualStyleBackColor = true;
			// 
			// CancelButton
			// 
			this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelButton.Location = new System.Drawing.Point(150, 5);
			this.CancelButton.Name = "CancelButton";
			this.CancelButton.Size = new System.Drawing.Size(100, 35);
			this.CancelButton.TabIndex = 2;
			this.CancelButton.Text = "Cancel";
			this.CancelButton.UseVisualStyleBackColor = true;
			// 
			// groupPanel1
			// 
			this.groupPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupPanel1.BackColor = System.Drawing.Color.White;
			this.groupPanel1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(185)))), ((int)(((byte)(185)))));
			this.groupPanel1.Controls.Add(this.label4);
			this.groupPanel1.Controls.Add(this.numericUpDown2);
			this.groupPanel1.Controls.Add(this.label2);
			this.groupPanel1.Controls.Add(this.numericUpDown1);
			this.groupPanel1.Controls.Add(this.label1);
			this.groupPanel1.Controls.Add(this.PowerUpDown);
			this.groupPanel1.Controls.Add(this.label3);
			this.groupPanel1.Controls.Add(this.ResistanceLabel);
			this.groupPanel1.Controls.Add(this.ResistanceLockedCheckBox);
			this.groupPanel1.HeaderBackColor = System.Drawing.Color.White;
			this.groupPanel1.HeaderHeight = 30;
			this.groupPanel1.Location = new System.Drawing.Point(3, 3);
			this.groupPanel1.Name = "groupPanel1";
			this.groupPanel1.Size = new System.Drawing.Size(248, 142);
			this.groupPanel1.TabIndex = 0;
			this.groupPanel1.TabStop = false;
			this.groupPanel1.Text = "PI-Regulator Setup:";
			// 
			// ResistanceLabel
			// 
			this.ResistanceLabel.AutoSize = true;
			this.ResistanceLabel.Location = new System.Drawing.Point(4, 36);
			this.ResistanceLabel.Name = "ResistanceLabel";
			this.ResistanceLabel.Size = new System.Drawing.Size(93, 13);
			this.ResistanceLabel.TabIndex = 86;
			this.ResistanceLabel.Text = "Use PI-Regulator:";
			// 
			// ResistanceLockedCheckBox
			// 
			this.ResistanceLockedCheckBox.AutoSize = true;
			this.ResistanceLockedCheckBox.Location = new System.Drawing.Point(101, 36);
			this.ResistanceLockedCheckBox.Name = "ResistanceLockedCheckBox";
			this.ResistanceLockedCheckBox.Size = new System.Drawing.Size(64, 17);
			this.ResistanceLockedCheckBox.TabIndex = 0;
			this.ResistanceLockedCheckBox.Text = "Enabled";
			this.ResistanceLockedCheckBox.UseVisualStyleBackColor = true;
			// 
			// PowerUpDown
			// 
			this.PowerUpDown.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.PowerUpDown.Location = new System.Drawing.Point(101, 87);
			this.PowerUpDown.Maximum = new decimal(new int[] {
            6000,
            0,
            0,
            0});
			this.PowerUpDown.Name = "PowerUpDown";
			this.PowerUpDown.Size = new System.Drawing.Size(106, 21);
			this.PowerUpDown.TabIndex = 2;
			this.PowerUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(4, 90);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(46, 13);
			this.label3.TabIndex = 88;
			this.label3.Text = "P Value:";
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.numericUpDown1.Location = new System.Drawing.Point(101, 114);
			this.numericUpDown1.Maximum = new decimal(new int[] {
            6000,
            0,
            0,
            0});
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(106, 21);
			this.numericUpDown1.TabIndex = 3;
			this.numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(4, 117);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(44, 13);
			this.label1.TabIndex = 91;
			this.label1.Text = "I Value:";
			// 
			// numericUpDown2
			// 
			this.numericUpDown2.Location = new System.Drawing.Point(101, 60);
			this.numericUpDown2.Name = "numericUpDown2";
			this.numericUpDown2.Size = new System.Drawing.Size(106, 21);
			this.numericUpDown2.TabIndex = 1;
			this.numericUpDown2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(4, 63);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(42, 13);
			this.label2.TabIndex = 93;
			this.label2.Text = "Range:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(213, 63);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(18, 13);
			this.label4.TabIndex = 95;
			this.label4.Text = "%";
			// 
			// TempControlSetupWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(254, 192);
			this.Controls.Add(this.groupPanel1);
			this.Controls.Add(this.ControlBorderedPanel);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "TempControlSetupWindow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "ArcticFox - TC Setup";
			this.ControlBorderedPanel.ResumeLayout(false);
			this.groupPanel1.ResumeLayout(false);
			this.groupPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.PowerUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private NCore.UI.BorderedPanel ControlBorderedPanel;
		private System.Windows.Forms.Button SaveButton;
		private System.Windows.Forms.Button CancelButton;
		private NCore.UI.GroupPanel groupPanel1;
		private System.Windows.Forms.Label ResistanceLabel;
		public System.Windows.Forms.CheckBox ResistanceLockedCheckBox;
		public System.Windows.Forms.NumericUpDown PowerUpDown;
		private System.Windows.Forms.Label label3;
		public System.Windows.Forms.NumericUpDown numericUpDown1;
		private System.Windows.Forms.Label label1;
		public System.Windows.Forms.NumericUpDown numericUpDown2;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label4;
	}
}