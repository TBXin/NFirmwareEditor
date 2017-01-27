#pragma warning disable 108, 114
namespace NToolbox.Windows
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
			this.groupPanel3 = new NCore.UI.GroupPanel();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.CommonTabPage = new System.Windows.Forms.TabPage();
			this.UpdateFromFileButton = new System.Windows.Forms.Button();
			this.DataflashTabPage = new System.Windows.Forms.TabPage();
			this.WriteDataflashButton = new System.Windows.Forms.Button();
			this.ReadDataflashButton = new System.Windows.Forms.Button();
			this.ResetDataflashButton = new System.Windows.Forms.Button();
			this.AdvancedTabPage = new System.Windows.Forms.TabPage();
			this.ChangeBootModeButton = new System.Windows.Forms.Button();
			this.ChangeHWButton = new System.Windows.Forms.Button();
			this.groupPanel2 = new NCore.UI.GroupPanel();
			this.UpdateStatusLabel = new System.Windows.Forms.Label();
			this.UpdateProgressBar = new System.Windows.Forms.ProgressBar();
			this.groupPanel1 = new NCore.UI.GroupPanel();
			this.BootModeTextBox = new System.Windows.Forms.TextBox();
			this.HardwareVersionTextBox = new System.Windows.Forms.TextBox();
			this.FirmwareVersionTextBox = new System.Windows.Forms.TextBox();
			this.DeviceNameTextBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.ControlBorderedPanel = new NCore.UI.BorderedPanel();
			this.CancelButton = new System.Windows.Forms.Button();
			this.groupPanel3.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.CommonTabPage.SuspendLayout();
			this.DataflashTabPage.SuspendLayout();
			this.AdvancedTabPage.SuspendLayout();
			this.groupPanel2.SuspendLayout();
			this.groupPanel1.SuspendLayout();
			this.ControlBorderedPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupPanel3
			// 
			this.groupPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupPanel3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(185)))), ((int)(((byte)(185)))));
			this.groupPanel3.Controls.Add(this.tabControl1);
			this.groupPanel3.HeaderBackColor = System.Drawing.Color.White;
			this.groupPanel3.HeaderHeight = 30;
			this.MainLocalizationExtender.SetKey(this.groupPanel3, "Toolbox.FirmwareUpdater.OperationsGroupLabel");
			this.groupPanel3.Location = new System.Drawing.Point(3, 244);
			this.groupPanel3.Name = "groupPanel3";
			this.groupPanel3.Size = new System.Drawing.Size(326, 98);
			this.groupPanel3.TabIndex = 2;
			this.groupPanel3.TabStop = false;
			this.groupPanel3.Text = "Operations:";
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.CommonTabPage);
			this.tabControl1.Controls.Add(this.DataflashTabPage);
			this.tabControl1.Controls.Add(this.AdvancedTabPage);
			this.tabControl1.ItemSize = new System.Drawing.Size(105, 18);
			this.MainLocalizationExtender.SetKey(this.tabControl1, "");
			this.tabControl1.Location = new System.Drawing.Point(4, 33);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(320, 62);
			this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
			this.tabControl1.TabIndex = 0;
			// 
			// CommonTabPage
			// 
			this.CommonTabPage.Controls.Add(this.UpdateFromFileButton);
			this.MainLocalizationExtender.SetKey(this.CommonTabPage, "Toolbox.FirmwareUpdater.CommonTab");
			this.CommonTabPage.Location = new System.Drawing.Point(4, 22);
			this.CommonTabPage.Name = "CommonTabPage";
			this.CommonTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.CommonTabPage.Size = new System.Drawing.Size(312, 36);
			this.CommonTabPage.TabIndex = 0;
			this.CommonTabPage.Text = "Common";
			this.CommonTabPage.UseVisualStyleBackColor = true;
			// 
			// UpdateFromFileButton
			// 
			this.UpdateFromFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.UpdateFromFileButton.Enabled = false;
			this.MainLocalizationExtender.SetKey(this.UpdateFromFileButton, "Toolbox.FirmwareUpdater.UpdateButton");
			this.UpdateFromFileButton.Location = new System.Drawing.Point(-1, 1);
			this.UpdateFromFileButton.Name = "UpdateFromFileButton";
			this.UpdateFromFileButton.Size = new System.Drawing.Size(312, 35);
			this.UpdateFromFileButton.TabIndex = 2;
			this.UpdateFromFileButton.Text = "Update from file";
			this.UpdateFromFileButton.UseVisualStyleBackColor = true;
			// 
			// DataflashTabPage
			// 
			this.DataflashTabPage.Controls.Add(this.WriteDataflashButton);
			this.DataflashTabPage.Controls.Add(this.ReadDataflashButton);
			this.DataflashTabPage.Controls.Add(this.ResetDataflashButton);
			this.MainLocalizationExtender.SetKey(this.DataflashTabPage, "Toolbox.FirmwareUpdater.DatalfashTab");
			this.DataflashTabPage.Location = new System.Drawing.Point(4, 22);
			this.DataflashTabPage.Name = "DataflashTabPage";
			this.DataflashTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.DataflashTabPage.Size = new System.Drawing.Size(312, 36);
			this.DataflashTabPage.TabIndex = 1;
			this.DataflashTabPage.Text = "Dataflash";
			this.DataflashTabPage.UseVisualStyleBackColor = true;
			// 
			// WriteDataflashButton
			// 
			this.WriteDataflashButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.WriteDataflashButton.Enabled = false;
			this.MainLocalizationExtender.SetKey(this.WriteDataflashButton, "Toolbox.FirmwareUpdater.WriteDataflashButton");
			this.WriteDataflashButton.Location = new System.Drawing.Point(105, 1);
			this.WriteDataflashButton.Name = "WriteDataflashButton";
			this.WriteDataflashButton.Size = new System.Drawing.Size(100, 35);
			this.WriteDataflashButton.TabIndex = 2;
			this.WriteDataflashButton.Text = "Write dataflash";
			this.WriteDataflashButton.UseVisualStyleBackColor = true;
			// 
			// ReadDataflashButton
			// 
			this.ReadDataflashButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ReadDataflashButton.Enabled = false;
			this.MainLocalizationExtender.SetKey(this.ReadDataflashButton, "Toolbox.FirmwareUpdater.ReadDataflashButton");
			this.ReadDataflashButton.Location = new System.Drawing.Point(-1, 1);
			this.ReadDataflashButton.Name = "ReadDataflashButton";
			this.ReadDataflashButton.Size = new System.Drawing.Size(100, 35);
			this.ReadDataflashButton.TabIndex = 1;
			this.ReadDataflashButton.Text = "Read dataflash";
			this.ReadDataflashButton.UseVisualStyleBackColor = true;
			// 
			// ResetDataflashButton
			// 
			this.ResetDataflashButton.Enabled = false;
			this.MainLocalizationExtender.SetKey(this.ResetDataflashButton, "Toolbox.FirmwareUpdater.ResetDataFlashButton");
			this.ResetDataflashButton.Location = new System.Drawing.Point(211, 1);
			this.ResetDataflashButton.Name = "ResetDataflashButton";
			this.ResetDataflashButton.Size = new System.Drawing.Size(100, 35);
			this.ResetDataflashButton.TabIndex = 0;
			this.ResetDataflashButton.Text = "Reset dataflash";
			this.ResetDataflashButton.UseVisualStyleBackColor = true;
			// 
			// AdvancedTabPage
			// 
			this.AdvancedTabPage.Controls.Add(this.ChangeBootModeButton);
			this.AdvancedTabPage.Controls.Add(this.ChangeHWButton);
			this.MainLocalizationExtender.SetKey(this.AdvancedTabPage, "Toolbox.FirmwareUpdater.AdvancedTab");
			this.AdvancedTabPage.Location = new System.Drawing.Point(4, 22);
			this.AdvancedTabPage.Name = "AdvancedTabPage";
			this.AdvancedTabPage.Size = new System.Drawing.Size(312, 36);
			this.AdvancedTabPage.TabIndex = 2;
			this.AdvancedTabPage.Text = "Advanced";
			this.AdvancedTabPage.UseVisualStyleBackColor = true;
			// 
			// ChangeBootModeButton
			// 
			this.ChangeBootModeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ChangeBootModeButton.Enabled = false;
			this.MainLocalizationExtender.SetKey(this.ChangeBootModeButton, "Toolbox.FirmwareUpdater.SwitchBootModeButton");
			this.ChangeBootModeButton.Location = new System.Drawing.Point(158, 1);
			this.ChangeBootModeButton.Name = "ChangeBootModeButton";
			this.ChangeBootModeButton.Size = new System.Drawing.Size(153, 35);
			this.ChangeBootModeButton.TabIndex = 3;
			this.ChangeBootModeButton.Text = "Switch boot mode";
			this.ChangeBootModeButton.UseVisualStyleBackColor = true;
			// 
			// ChangeHWButton
			// 
			this.ChangeHWButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ChangeHWButton.Enabled = false;
			this.MainLocalizationExtender.SetKey(this.ChangeHWButton, "Toolbox.FirmwareUpdater.ChangeHWVerButton");
			this.ChangeHWButton.Location = new System.Drawing.Point(-1, 1);
			this.ChangeHWButton.Name = "ChangeHWButton";
			this.ChangeHWButton.Size = new System.Drawing.Size(153, 35);
			this.ChangeHWButton.TabIndex = 1;
			this.ChangeHWButton.Text = "Change HW Version";
			this.ChangeHWButton.UseVisualStyleBackColor = true;
			// 
			// groupPanel2
			// 
			this.groupPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupPanel2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(185)))), ((int)(((byte)(185)))));
			this.groupPanel2.Controls.Add(this.UpdateStatusLabel);
			this.groupPanel2.Controls.Add(this.UpdateProgressBar);
			this.groupPanel2.HeaderBackColor = System.Drawing.Color.White;
			this.groupPanel2.HeaderHeight = 30;
			this.MainLocalizationExtender.SetKey(this.groupPanel2, "Toolbox.FirmwareUpdater.ProgressGroupLabel");
			this.groupPanel2.Location = new System.Drawing.Point(3, 148);
			this.groupPanel2.Name = "groupPanel2";
			this.groupPanel2.Size = new System.Drawing.Size(326, 90);
			this.groupPanel2.TabIndex = 1;
			this.groupPanel2.TabStop = false;
			this.groupPanel2.Text = "Progress:";
			// 
			// UpdateStatusLabel
			// 
			this.UpdateStatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.MainLocalizationExtender.SetKey(this.UpdateStatusLabel, "");
			this.UpdateStatusLabel.Location = new System.Drawing.Point(4, 33);
			this.UpdateStatusLabel.Name = "UpdateStatusLabel";
			this.UpdateStatusLabel.Size = new System.Drawing.Size(318, 20);
			this.UpdateStatusLabel.TabIndex = 5;
			this.UpdateStatusLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// UpdateProgressBar
			// 
			this.UpdateProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.MainLocalizationExtender.SetKey(this.UpdateProgressBar, "");
			this.UpdateProgressBar.Location = new System.Drawing.Point(4, 56);
			this.UpdateProgressBar.Name = "UpdateProgressBar";
			this.UpdateProgressBar.Size = new System.Drawing.Size(318, 30);
			this.UpdateProgressBar.TabIndex = 4;
			// 
			// groupPanel1
			// 
			this.groupPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupPanel1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(185)))), ((int)(((byte)(185)))));
			this.groupPanel1.Controls.Add(this.BootModeTextBox);
			this.groupPanel1.Controls.Add(this.HardwareVersionTextBox);
			this.groupPanel1.Controls.Add(this.FirmwareVersionTextBox);
			this.groupPanel1.Controls.Add(this.DeviceNameTextBox);
			this.groupPanel1.Controls.Add(this.label1);
			this.groupPanel1.Controls.Add(this.label5);
			this.groupPanel1.Controls.Add(this.label4);
			this.groupPanel1.Controls.Add(this.label3);
			this.groupPanel1.HeaderBackColor = System.Drawing.Color.White;
			this.groupPanel1.HeaderHeight = 30;
			this.MainLocalizationExtender.SetKey(this.groupPanel1, "Toolbox.FirmwareUpdater.DeviceInfoGroupLabel");
			this.groupPanel1.Location = new System.Drawing.Point(3, 3);
			this.groupPanel1.Name = "groupPanel1";
			this.groupPanel1.Size = new System.Drawing.Size(326, 139);
			this.groupPanel1.TabIndex = 0;
			this.groupPanel1.TabStop = false;
			this.groupPanel1.Text = "Device info:";
			// 
			// BootModeTextBox
			// 
			this.BootModeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.MainLocalizationExtender.SetKey(this.BootModeTextBox, "");
			this.BootModeTextBox.Location = new System.Drawing.Point(106, 114);
			this.BootModeTextBox.Name = "BootModeTextBox";
			this.BootModeTextBox.Size = new System.Drawing.Size(216, 21);
			this.BootModeTextBox.TabIndex = 25;
			this.BootModeTextBox.TabStop = false;
			// 
			// HardwareVersionTextBox
			// 
			this.HardwareVersionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.MainLocalizationExtender.SetKey(this.HardwareVersionTextBox, "");
			this.HardwareVersionTextBox.Location = new System.Drawing.Point(106, 87);
			this.HardwareVersionTextBox.Name = "HardwareVersionTextBox";
			this.HardwareVersionTextBox.Size = new System.Drawing.Size(216, 21);
			this.HardwareVersionTextBox.TabIndex = 21;
			this.HardwareVersionTextBox.TabStop = false;
			// 
			// FirmwareVersionTextBox
			// 
			this.FirmwareVersionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.MainLocalizationExtender.SetKey(this.FirmwareVersionTextBox, "");
			this.FirmwareVersionTextBox.Location = new System.Drawing.Point(106, 60);
			this.FirmwareVersionTextBox.Name = "FirmwareVersionTextBox";
			this.FirmwareVersionTextBox.Size = new System.Drawing.Size(216, 21);
			this.FirmwareVersionTextBox.TabIndex = 20;
			this.FirmwareVersionTextBox.TabStop = false;
			// 
			// DeviceNameTextBox
			// 
			this.DeviceNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.MainLocalizationExtender.SetKey(this.DeviceNameTextBox, "");
			this.DeviceNameTextBox.Location = new System.Drawing.Point(106, 33);
			this.DeviceNameTextBox.Name = "DeviceNameTextBox";
			this.DeviceNameTextBox.Size = new System.Drawing.Size(216, 21);
			this.DeviceNameTextBox.TabIndex = 19;
			this.DeviceNameTextBox.TabStop = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.MainLocalizationExtender.SetKey(this.label1, "Toolbox.FirmwareUpdater.BootModeLabel");
			this.label1.Location = new System.Drawing.Point(9, 117);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(62, 13);
			this.label1.TabIndex = 26;
			this.label1.Text = "Boot mode:";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.MainLocalizationExtender.SetKey(this.label5, "Toolbox.FirmwareUpdater.HardwareVersionLabel");
			this.label5.Location = new System.Drawing.Point(9, 90);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(96, 13);
			this.label5.TabIndex = 24;
			this.label5.Text = "Hardware version:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.MainLocalizationExtender.SetKey(this.label4, "Toolbox.FirmwareUpdater.FirmwareVersionLabel");
			this.label4.Location = new System.Drawing.Point(9, 63);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(93, 13);
			this.label4.TabIndex = 23;
			this.label4.Text = "Firmware version:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.MainLocalizationExtender.SetKey(this.label3, "Toolbox.FirmwareUpdater.DeviceNameLabel");
			this.label3.Location = new System.Drawing.Point(9, 36);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(72, 13);
			this.label3.TabIndex = 22;
			this.label3.Text = "Device name:";
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
			this.ControlBorderedPanel.Controls.Add(this.CancelButton);
			this.ControlBorderedPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.MainLocalizationExtender.SetKey(this.ControlBorderedPanel, "");
			this.ControlBorderedPanel.Location = new System.Drawing.Point(0, 348);
			this.ControlBorderedPanel.Name = "ControlBorderedPanel";
			this.ControlBorderedPanel.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
			this.ControlBorderedPanel.Size = new System.Drawing.Size(332, 44);
			this.ControlBorderedPanel.TabIndex = 3;
			this.ControlBorderedPanel.Text = "borderedPanel1";
			// 
			// CancelButton
			// 
			this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.MainLocalizationExtender.SetKey(this.CancelButton, "Toolbox.FirmwareUpdater.CancelButton");
			this.CancelButton.Location = new System.Drawing.Point(228, 5);
			this.CancelButton.Name = "CancelButton";
			this.CancelButton.Size = new System.Drawing.Size(100, 35);
			this.CancelButton.TabIndex = 3;
			this.CancelButton.Text = "Cancel";
			this.CancelButton.UseVisualStyleBackColor = true;
			// 
			// FirmwareUpdaterWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(332, 392);
			this.Controls.Add(this.groupPanel3);
			this.Controls.Add(this.groupPanel2);
			this.Controls.Add(this.groupPanel1);
			this.Controls.Add(this.ControlBorderedPanel);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MainLocalizationExtender.SetKey(this, "");
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FirmwareUpdaterWindow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "NFE Toolbox - Updater";
			this.groupPanel3.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.CommonTabPage.ResumeLayout(false);
			this.DataflashTabPage.ResumeLayout(false);
			this.AdvancedTabPage.ResumeLayout(false);
			this.groupPanel2.ResumeLayout(false);
			this.groupPanel1.ResumeLayout(false);
			this.groupPanel1.PerformLayout();
			this.ControlBorderedPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private NCore.UI.BorderedPanel ControlBorderedPanel;
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
		private System.Windows.Forms.Button ResetDataflashButton;
		private NCore.UI.GroupPanel groupPanel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox BootModeTextBox;
		private NCore.UI.GroupPanel groupPanel2;
		private NCore.UI.GroupPanel groupPanel3;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage CommonTabPage;
		private System.Windows.Forms.TabPage DataflashTabPage;
		private System.Windows.Forms.Button ReadDataflashButton;
		private System.Windows.Forms.Button WriteDataflashButton;
		private System.Windows.Forms.TabPage AdvancedTabPage;
		private System.Windows.Forms.Button ChangeHWButton;
		private System.Windows.Forms.Button ChangeBootModeButton;
	}
}