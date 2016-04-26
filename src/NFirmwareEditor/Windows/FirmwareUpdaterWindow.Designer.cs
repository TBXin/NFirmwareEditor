using NFirmwareEditor.UI;

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
			this.UpdateProgressBar = new System.Windows.Forms.ProgressBar();
			this.UpdateStatusLabel = new System.Windows.Forms.Label();
			this.groupPanel1 = new NFirmwareEditor.UI.GroupPanel();
			this.label5 = new System.Windows.Forms.Label();
			this.HardwareVersionTextBox = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.FirmwareVersionTextBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.DeviceNameTextBox = new System.Windows.Forms.TextBox();
			this.ControlBorderedPanel = new NFirmwareEditor.UI.BorderedPanel();
			this.ResetDataFlashButton = new System.Windows.Forms.Button();
			this.UpdateButton = new System.Windows.Forms.Button();
			this.UpdateFromFileButton = new System.Windows.Forms.Button();
			this.CancelButton = new System.Windows.Forms.Button();
			this.groupPanel1.SuspendLayout();
			this.ControlBorderedPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// UpdateProgressBar
			// 
			this.UpdateProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.UpdateProgressBar.Location = new System.Drawing.Point(3, 141);
			this.UpdateProgressBar.Name = "UpdateProgressBar";
			this.UpdateProgressBar.Size = new System.Drawing.Size(419, 30);
			this.UpdateProgressBar.TabIndex = 4;
			// 
			// UpdateStatusLabel
			// 
			this.UpdateStatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.UpdateStatusLabel.Location = new System.Drawing.Point(3, 118);
			this.UpdateStatusLabel.Name = "UpdateStatusLabel";
			this.UpdateStatusLabel.Size = new System.Drawing.Size(419, 20);
			this.UpdateStatusLabel.TabIndex = 5;
			this.UpdateStatusLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// groupPanel1
			// 
			this.groupPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupPanel1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(185)))), ((int)(((byte)(185)))));
			this.groupPanel1.Controls.Add(this.label5);
			this.groupPanel1.Controls.Add(this.HardwareVersionTextBox);
			this.groupPanel1.Controls.Add(this.label4);
			this.groupPanel1.Controls.Add(this.FirmwareVersionTextBox);
			this.groupPanel1.Controls.Add(this.label3);
			this.groupPanel1.Controls.Add(this.DeviceNameTextBox);
			this.groupPanel1.HeaderBackColor = System.Drawing.Color.White;
			this.groupPanel1.HeaderHeight = 30;
			this.groupPanel1.Location = new System.Drawing.Point(3, 3);
			this.groupPanel1.Name = "groupPanel1";
			this.groupPanel1.Size = new System.Drawing.Size(419, 112);
			this.groupPanel1.TabIndex = 3;
			this.groupPanel1.TabStop = false;
			this.groupPanel1.Text = "Device info:";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(9, 90);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(96, 13);
			this.label5.TabIndex = 24;
			this.label5.Text = "Hardware version:";
			// 
			// HardwareVersionTextBox
			// 
			this.HardwareVersionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.HardwareVersionTextBox.Location = new System.Drawing.Point(106, 87);
			this.HardwareVersionTextBox.Name = "HardwareVersionTextBox";
			this.HardwareVersionTextBox.Size = new System.Drawing.Size(309, 21);
			this.HardwareVersionTextBox.TabIndex = 21;
			this.HardwareVersionTextBox.TabStop = false;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(9, 63);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(93, 13);
			this.label4.TabIndex = 23;
			this.label4.Text = "Firmware version:";
			// 
			// FirmwareVersionTextBox
			// 
			this.FirmwareVersionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.FirmwareVersionTextBox.Location = new System.Drawing.Point(106, 60);
			this.FirmwareVersionTextBox.Name = "FirmwareVersionTextBox";
			this.FirmwareVersionTextBox.Size = new System.Drawing.Size(309, 21);
			this.FirmwareVersionTextBox.TabIndex = 20;
			this.FirmwareVersionTextBox.TabStop = false;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(9, 36);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(72, 13);
			this.label3.TabIndex = 22;
			this.label3.Text = "Device name:";
			// 
			// DeviceNameTextBox
			// 
			this.DeviceNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.DeviceNameTextBox.Location = new System.Drawing.Point(106, 33);
			this.DeviceNameTextBox.Name = "DeviceNameTextBox";
			this.DeviceNameTextBox.Size = new System.Drawing.Size(309, 21);
			this.DeviceNameTextBox.TabIndex = 19;
			this.DeviceNameTextBox.TabStop = false;
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
			this.ControlBorderedPanel.Controls.Add(this.ResetDataFlashButton);
			this.ControlBorderedPanel.Controls.Add(this.UpdateButton);
			this.ControlBorderedPanel.Controls.Add(this.UpdateFromFileButton);
			this.ControlBorderedPanel.Controls.Add(this.CancelButton);
			this.ControlBorderedPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.ControlBorderedPanel.Location = new System.Drawing.Point(0, 174);
			this.ControlBorderedPanel.Name = "ControlBorderedPanel";
			this.ControlBorderedPanel.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
			this.ControlBorderedPanel.Size = new System.Drawing.Size(425, 44);
			this.ControlBorderedPanel.TabIndex = 2;
			this.ControlBorderedPanel.Text = "borderedPanel1";
			// 
			// ResetDataFlashButton
			// 
			this.ResetDataFlashButton.Enabled = false;
			this.ResetDataFlashButton.Location = new System.Drawing.Point(3, 5);
			this.ResetDataFlashButton.Name = "ResetDataFlashButton";
			this.ResetDataFlashButton.Size = new System.Drawing.Size(100, 35);
			this.ResetDataFlashButton.TabIndex = 0;
			this.ResetDataFlashButton.Text = "Reset dataflash";
			this.ResetDataFlashButton.UseVisualStyleBackColor = true;
			// 
			// UpdateButton
			// 
			this.UpdateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.UpdateButton.Enabled = false;
			this.UpdateButton.Location = new System.Drawing.Point(109, 5);
			this.UpdateButton.Name = "UpdateButton";
			this.UpdateButton.Size = new System.Drawing.Size(100, 35);
			this.UpdateButton.TabIndex = 1;
			this.UpdateButton.Text = "Update";
			this.UpdateButton.UseVisualStyleBackColor = true;
			// 
			// UpdateFromFileButton
			// 
			this.UpdateFromFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.UpdateFromFileButton.Enabled = false;
			this.UpdateFromFileButton.Location = new System.Drawing.Point(215, 5);
			this.UpdateFromFileButton.Name = "UpdateFromFileButton";
			this.UpdateFromFileButton.Size = new System.Drawing.Size(100, 35);
			this.UpdateFromFileButton.TabIndex = 2;
			this.UpdateFromFileButton.Text = "Update from file";
			this.UpdateFromFileButton.UseVisualStyleBackColor = true;
			// 
			// CancelButton
			// 
			this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelButton.Location = new System.Drawing.Point(321, 5);
			this.CancelButton.Name = "CancelButton";
			this.CancelButton.Size = new System.Drawing.Size(100, 35);
			this.CancelButton.TabIndex = 3;
			this.CancelButton.Text = "Cancel";
			this.CancelButton.UseVisualStyleBackColor = true;
			// 
			// FirmwareUpdaterWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(425, 218);
			this.ControlBox = false;
			this.Controls.Add(this.UpdateStatusLabel);
			this.Controls.Add(this.UpdateProgressBar);
			this.Controls.Add(this.groupPanel1);
			this.Controls.Add(this.ControlBorderedPanel);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FirmwareUpdaterWindow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "NFirmwareEditor - Updater";
			this.groupPanel1.ResumeLayout(false);
			this.groupPanel1.PerformLayout();
			this.ControlBorderedPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private UI.BorderedPanel ControlBorderedPanel;
		private System.Windows.Forms.Button UpdateFromFileButton;
		private System.Windows.Forms.Button CancelButton;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox HardwareVersionTextBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox FirmwareVersionTextBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox DeviceNameTextBox;
		private System.Windows.Forms.ProgressBar UpdateProgressBar;
		private System.Windows.Forms.Label UpdateStatusLabel;
		private System.Windows.Forms.Button UpdateButton;
		private System.Windows.Forms.Button ResetDataFlashButton;
		private GroupPanel groupPanel1;
	}
}