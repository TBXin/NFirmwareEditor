namespace NFirmwareEditor.Windows
{
	partial class FirmwareUpdaterWindow
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
			this.ControlBorderedPanel = new NFirmwareEditor.UI.BorderedPanel();
			this.OkButton = new System.Windows.Forms.Button();
			this.CancelButton = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label5 = new System.Windows.Forms.Label();
			this.HardwareVersionTextBox = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.FirmwareVersionTextBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.DeviceNameTextBox = new System.Windows.Forms.TextBox();
			this.UpdateProgressBar = new System.Windows.Forms.ProgressBar();
			this.UpdateStatusLabel = new System.Windows.Forms.Label();
			this.ControlBorderedPanel.SuspendLayout();
			this.groupBox1.SuspendLayout();
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
			this.ControlBorderedPanel.Controls.Add(this.OkButton);
			this.ControlBorderedPanel.Controls.Add(this.CancelButton);
			this.ControlBorderedPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.ControlBorderedPanel.Location = new System.Drawing.Point(0, 162);
			this.ControlBorderedPanel.Name = "ControlBorderedPanel";
			this.ControlBorderedPanel.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
			this.ControlBorderedPanel.Size = new System.Drawing.Size(384, 39);
			this.ControlBorderedPanel.TabIndex = 2;
			this.ControlBorderedPanel.Text = "borderedPanel1";
			// 
			// OkButton
			// 
			this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.OkButton.Enabled = false;
			this.OkButton.Location = new System.Drawing.Point(174, 5);
			this.OkButton.Name = "OkButton";
			this.OkButton.Size = new System.Drawing.Size(100, 30);
			this.OkButton.TabIndex = 0;
			this.OkButton.Text = "Update";
			this.OkButton.UseVisualStyleBackColor = true;
			// 
			// CancelButton
			// 
			this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelButton.Location = new System.Drawing.Point(280, 5);
			this.CancelButton.Name = "CancelButton";
			this.CancelButton.Size = new System.Drawing.Size(100, 30);
			this.CancelButton.TabIndex = 1;
			this.CancelButton.Text = "Cancel";
			this.CancelButton.UseVisualStyleBackColor = true;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.HardwareVersionTextBox);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.FirmwareVersionTextBox);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.DeviceNameTextBox);
			this.groupBox1.Location = new System.Drawing.Point(3, 1);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(378, 101);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Device info:";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(9, 77);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(96, 13);
			this.label5.TabIndex = 24;
			this.label5.Text = "Hardware version:";
			// 
			// HardwareVersionTextBox
			// 
			this.HardwareVersionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.HardwareVersionTextBox.Location = new System.Drawing.Point(106, 74);
			this.HardwareVersionTextBox.Name = "HardwareVersionTextBox";
			this.HardwareVersionTextBox.Size = new System.Drawing.Size(267, 21);
			this.HardwareVersionTextBox.TabIndex = 21;
			this.HardwareVersionTextBox.TabStop = false;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(9, 50);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(93, 13);
			this.label4.TabIndex = 23;
			this.label4.Text = "Firmware version:";
			// 
			// FirmwareVersionTextBox
			// 
			this.FirmwareVersionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.FirmwareVersionTextBox.Location = new System.Drawing.Point(106, 47);
			this.FirmwareVersionTextBox.Name = "FirmwareVersionTextBox";
			this.FirmwareVersionTextBox.Size = new System.Drawing.Size(267, 21);
			this.FirmwareVersionTextBox.TabIndex = 20;
			this.FirmwareVersionTextBox.TabStop = false;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(9, 23);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(72, 13);
			this.label3.TabIndex = 22;
			this.label3.Text = "Device name:";
			// 
			// DeviceNameTextBox
			// 
			this.DeviceNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.DeviceNameTextBox.Location = new System.Drawing.Point(106, 20);
			this.DeviceNameTextBox.Name = "DeviceNameTextBox";
			this.DeviceNameTextBox.Size = new System.Drawing.Size(267, 21);
			this.DeviceNameTextBox.TabIndex = 19;
			this.DeviceNameTextBox.TabStop = false;
			// 
			// UpdateProgressBar
			// 
			this.UpdateProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.UpdateProgressBar.Location = new System.Drawing.Point(3, 128);
			this.UpdateProgressBar.Name = "UpdateProgressBar";
			this.UpdateProgressBar.Size = new System.Drawing.Size(378, 30);
			this.UpdateProgressBar.TabIndex = 4;
			// 
			// UpdateStatusLabel
			// 
			this.UpdateStatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.UpdateStatusLabel.Location = new System.Drawing.Point(3, 105);
			this.UpdateStatusLabel.Name = "UpdateStatusLabel";
			this.UpdateStatusLabel.Size = new System.Drawing.Size(378, 20);
			this.UpdateStatusLabel.TabIndex = 5;
			this.UpdateStatusLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// FirmwareUpdaterWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(384, 201);
			this.ControlBox = false;
			this.Controls.Add(this.UpdateStatusLabel);
			this.Controls.Add(this.UpdateProgressBar);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.ControlBorderedPanel);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FirmwareUpdaterWindow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "NFirmwareEditor - Updater";
			this.ControlBorderedPanel.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private UI.BorderedPanel ControlBorderedPanel;
		private System.Windows.Forms.Button OkButton;
		private System.Windows.Forms.Button CancelButton;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox HardwareVersionTextBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox FirmwareVersionTextBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox DeviceNameTextBox;
		private System.Windows.Forms.ProgressBar UpdateProgressBar;
		private System.Windows.Forms.Label UpdateStatusLabel;
	}
}