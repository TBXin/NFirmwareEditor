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
			this.UpdateStatusLabel = new System.Windows.Forms.Label();
			this.UpdateProgressBar = new System.Windows.Forms.ProgressBar();
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
			this.borderedPanel1 = new NCore.UI.BorderedPanel();
			this.borderedPanel2 = new NCore.UI.BorderedPanel();
			this.borderedPanel3 = new NCore.UI.BorderedPanel();
			this.borderedPanel4 = new NCore.UI.BorderedPanel();
			this.label2 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.label6 = new System.Windows.Forms.Label();
			this.panel3 = new System.Windows.Forms.Panel();
			this.label7 = new System.Windows.Forms.Label();
			this.UpdateFromFileButton = new NCore.UI.ExtendedButton();
			this.ReadDataflashButton = new NCore.UI.ExtendedButton();
			this.WriteDataflashButton = new NCore.UI.ExtendedButton();
			this.ResetDataflashButton = new NCore.UI.ExtendedButton();
			this.ChangeHWButton = new NCore.UI.ExtendedButton();
			this.ChangeBootModeButton = new NCore.UI.ExtendedButton();
			this.ConnectionPictureBox = new System.Windows.Forms.PictureBox();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.ControlBorderedPanel.SuspendLayout();
			this.borderedPanel1.SuspendLayout();
			this.borderedPanel2.SuspendLayout();
			this.borderedPanel3.SuspendLayout();
			this.borderedPanel4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ConnectionPictureBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
			this.SuspendLayout();
			// 
			// UpdateStatusLabel
			// 
			this.UpdateStatusLabel.AutoEllipsis = true;
			this.UpdateStatusLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(167)))), ((int)(((byte)(169)))));
			this.MainLocalizationExtender.SetKey(this.UpdateStatusLabel, "");
			this.UpdateStatusLabel.Location = new System.Drawing.Point(6, 12);
			this.UpdateStatusLabel.Name = "UpdateStatusLabel";
			this.UpdateStatusLabel.Size = new System.Drawing.Size(362, 20);
			this.UpdateStatusLabel.TabIndex = 5;
			this.UpdateStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// UpdateProgressBar
			// 
			this.MainLocalizationExtender.SetKey(this.UpdateProgressBar, "");
			this.UpdateProgressBar.Location = new System.Drawing.Point(9, 242);
			this.UpdateProgressBar.Name = "UpdateProgressBar";
			this.UpdateProgressBar.Size = new System.Drawing.Size(200, 30);
			this.UpdateProgressBar.TabIndex = 4;
			// 
			// BootModeTextBox
			// 
			this.BootModeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.BootModeTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.MainLocalizationExtender.SetKey(this.BootModeTextBox, "");
			this.BootModeTextBox.Location = new System.Drawing.Point(4, 5);
			this.BootModeTextBox.Name = "BootModeTextBox";
			this.BootModeTextBox.Size = new System.Drawing.Size(192, 14);
			this.BootModeTextBox.TabIndex = 25;
			this.BootModeTextBox.TabStop = false;
			this.BootModeTextBox.Text = "APROM";
			// 
			// HardwareVersionTextBox
			// 
			this.HardwareVersionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.HardwareVersionTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.MainLocalizationExtender.SetKey(this.HardwareVersionTextBox, "");
			this.HardwareVersionTextBox.Location = new System.Drawing.Point(4, 5);
			this.HardwareVersionTextBox.Name = "HardwareVersionTextBox";
			this.HardwareVersionTextBox.Size = new System.Drawing.Size(192, 14);
			this.HardwareVersionTextBox.TabIndex = 21;
			this.HardwareVersionTextBox.TabStop = false;
			this.HardwareVersionTextBox.Text = "1.00";
			// 
			// FirmwareVersionTextBox
			// 
			this.FirmwareVersionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.FirmwareVersionTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.MainLocalizationExtender.SetKey(this.FirmwareVersionTextBox, "");
			this.FirmwareVersionTextBox.Location = new System.Drawing.Point(4, 5);
			this.FirmwareVersionTextBox.Name = "FirmwareVersionTextBox";
			this.FirmwareVersionTextBox.Size = new System.Drawing.Size(192, 14);
			this.FirmwareVersionTextBox.TabIndex = 20;
			this.FirmwareVersionTextBox.TabStop = false;
			this.FirmwareVersionTextBox.Text = "1.00";
			// 
			// DeviceNameTextBox
			// 
			this.DeviceNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.DeviceNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.MainLocalizationExtender.SetKey(this.DeviceNameTextBox, "");
			this.DeviceNameTextBox.Location = new System.Drawing.Point(4, 5);
			this.DeviceNameTextBox.Name = "DeviceNameTextBox";
			this.DeviceNameTextBox.Size = new System.Drawing.Size(192, 14);
			this.DeviceNameTextBox.TabIndex = 19;
			this.DeviceNameTextBox.TabStop = false;
			this.DeviceNameTextBox.Text = "eVic VTC Mini";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(167)))), ((int)(((byte)(169)))));
			this.MainLocalizationExtender.SetKey(this.label1, "Toolbox.FirmwareUpdater.BootModeLabel");
			this.label1.Location = new System.Drawing.Point(6, 140);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(71, 13);
			this.label1.TabIndex = 26;
			this.label1.Text = "Boot mode:";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(167)))), ((int)(((byte)(169)))));
			this.MainLocalizationExtender.SetKey(this.label5, "Toolbox.FirmwareUpdater.HardwareVersionLabel");
			this.label5.Location = new System.Drawing.Point(6, 98);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(110, 13);
			this.label5.TabIndex = 24;
			this.label5.Text = "Hardware version:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(167)))), ((int)(((byte)(169)))));
			this.MainLocalizationExtender.SetKey(this.label4, "Toolbox.FirmwareUpdater.FirmwareVersionLabel");
			this.label4.Location = new System.Drawing.Point(6, 52);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(108, 13);
			this.label4.TabIndex = 23;
			this.label4.Text = "Firmware version:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(167)))), ((int)(((byte)(169)))));
			this.MainLocalizationExtender.SetKey(this.label3, "Toolbox.FirmwareUpdater.DeviceNameLabel");
			this.label3.Location = new System.Drawing.Point(6, 6);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(83, 13);
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
			this.ControlBorderedPanel.Controls.Add(this.UpdateStatusLabel);
			this.ControlBorderedPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.MainLocalizationExtender.SetKey(this.ControlBorderedPanel, "");
			this.ControlBorderedPanel.Location = new System.Drawing.Point(0, 295);
			this.ControlBorderedPanel.Name = "ControlBorderedPanel";
			this.ControlBorderedPanel.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
			this.ControlBorderedPanel.Size = new System.Drawing.Size(478, 44);
			this.ControlBorderedPanel.TabIndex = 3;
			this.ControlBorderedPanel.Text = "borderedPanel1";
			// 
			// CancelButton
			// 
			this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.MainLocalizationExtender.SetKey(this.CancelButton, "Toolbox.FirmwareUpdater.CancelButton");
			this.CancelButton.Location = new System.Drawing.Point(374, 5);
			this.CancelButton.Name = "CancelButton";
			this.CancelButton.Size = new System.Drawing.Size(100, 35);
			this.CancelButton.TabIndex = 3;
			this.CancelButton.Text = "Cancel";
			this.CancelButton.UseVisualStyleBackColor = true;
			// 
			// borderedPanel1
			// 
			this.borderedPanel1.BackColor = System.Drawing.Color.Transparent;
			this.borderedPanel1.BorderBottom = true;
			this.borderedPanel1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.borderedPanel1.BorderLeft = true;
			this.borderedPanel1.BorderRight = true;
			this.borderedPanel1.BorderTop = true;
			this.borderedPanel1.BorderWidth = 1F;
			this.borderedPanel1.Controls.Add(this.DeviceNameTextBox);
			this.MainLocalizationExtender.SetKey(this.borderedPanel1, "");
			this.borderedPanel1.Location = new System.Drawing.Point(9, 26);
			this.borderedPanel1.Name = "borderedPanel1";
			this.borderedPanel1.Padding = new System.Windows.Forms.Padding(1);
			this.borderedPanel1.Size = new System.Drawing.Size(200, 23);
			this.borderedPanel1.TabIndex = 27;
			// 
			// borderedPanel2
			// 
			this.borderedPanel2.BackColor = System.Drawing.Color.Transparent;
			this.borderedPanel2.BorderBottom = true;
			this.borderedPanel2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.borderedPanel2.BorderLeft = true;
			this.borderedPanel2.BorderRight = true;
			this.borderedPanel2.BorderTop = true;
			this.borderedPanel2.BorderWidth = 1F;
			this.borderedPanel2.Controls.Add(this.FirmwareVersionTextBox);
			this.MainLocalizationExtender.SetKey(this.borderedPanel2, "");
			this.borderedPanel2.Location = new System.Drawing.Point(9, 68);
			this.borderedPanel2.Name = "borderedPanel2";
			this.borderedPanel2.Padding = new System.Windows.Forms.Padding(1);
			this.borderedPanel2.Size = new System.Drawing.Size(200, 23);
			this.borderedPanel2.TabIndex = 28;
			// 
			// borderedPanel3
			// 
			this.borderedPanel3.BackColor = System.Drawing.Color.Transparent;
			this.borderedPanel3.BorderBottom = true;
			this.borderedPanel3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.borderedPanel3.BorderLeft = true;
			this.borderedPanel3.BorderRight = true;
			this.borderedPanel3.BorderTop = true;
			this.borderedPanel3.BorderWidth = 1F;
			this.borderedPanel3.Controls.Add(this.BootModeTextBox);
			this.MainLocalizationExtender.SetKey(this.borderedPanel3, "");
			this.borderedPanel3.Location = new System.Drawing.Point(9, 156);
			this.borderedPanel3.Name = "borderedPanel3";
			this.borderedPanel3.Padding = new System.Windows.Forms.Padding(1);
			this.borderedPanel3.Size = new System.Drawing.Size(200, 23);
			this.borderedPanel3.TabIndex = 29;
			// 
			// borderedPanel4
			// 
			this.borderedPanel4.BackColor = System.Drawing.Color.Transparent;
			this.borderedPanel4.BorderBottom = true;
			this.borderedPanel4.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.borderedPanel4.BorderLeft = true;
			this.borderedPanel4.BorderRight = true;
			this.borderedPanel4.BorderTop = true;
			this.borderedPanel4.BorderWidth = 1F;
			this.borderedPanel4.Controls.Add(this.HardwareVersionTextBox);
			this.MainLocalizationExtender.SetKey(this.borderedPanel4, "");
			this.borderedPanel4.Location = new System.Drawing.Point(9, 114);
			this.borderedPanel4.Name = "borderedPanel4";
			this.borderedPanel4.Padding = new System.Windows.Forms.Padding(1);
			this.borderedPanel4.Size = new System.Drawing.Size(200, 23);
			this.borderedPanel4.TabIndex = 29;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(167)))), ((int)(((byte)(169)))));
			this.MainLocalizationExtender.SetKey(this.label2, "Toolbox.FirmwareUpdater.CommonTab");
			this.label2.Location = new System.Drawing.Point(239, 6);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(57, 13);
			this.label2.TabIndex = 30;
			this.label2.Text = "Common";
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.MainLocalizationExtender.SetKey(this.panel1, "");
			this.panel1.Location = new System.Drawing.Point(242, 22);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(200, 1);
			this.panel1.TabIndex = 31;
			// 
			// panel2
			// 
			this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.MainLocalizationExtender.SetKey(this.panel2, "");
			this.panel2.Location = new System.Drawing.Point(242, 91);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(200, 1);
			this.panel2.TabIndex = 33;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(167)))), ((int)(((byte)(169)))));
			this.MainLocalizationExtender.SetKey(this.label6, "Toolbox.FirmwareUpdater.DatalfashTab");
			this.label6.Location = new System.Drawing.Point(239, 75);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(61, 13);
			this.label6.TabIndex = 32;
			this.label6.Text = "Dataflash";
			// 
			// panel3
			// 
			this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.MainLocalizationExtender.SetKey(this.panel3, "");
			this.panel3.Location = new System.Drawing.Point(242, 217);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(200, 1);
			this.panel3.TabIndex = 35;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(167)))), ((int)(((byte)(169)))));
			this.MainLocalizationExtender.SetKey(this.label7, "Toolbox.FirmwareUpdater.AdvancedTab");
			this.label7.Location = new System.Drawing.Point(239, 201);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(63, 13);
			this.label7.TabIndex = 34;
			this.label7.Text = "Advanced";
			// 
			// UpdateFromFileButton
			// 
			this.UpdateFromFileButton.Cursor = System.Windows.Forms.Cursors.Hand;
			this.UpdateFromFileButton.DrawBorders = false;
			this.UpdateFromFileButton.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.UpdateFromFileButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(146)))), ((int)(((byte)(221)))));
			this.MainLocalizationExtender.SetKey(this.UpdateFromFileButton, "Toolbox.FirmwareUpdater.UpdateButton");
			this.UpdateFromFileButton.Location = new System.Drawing.Point(240, 29);
			this.UpdateFromFileButton.MouserDownPrimaryTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(93)))), ((int)(((byte)(187)))));
			this.UpdateFromFileButton.MouserOverPrimaryTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(120)))), ((int)(((byte)(206)))));
			this.UpdateFromFileButton.Name = "UpdateFromFileButton";
			this.UpdateFromFileButton.PrimaryTextDisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(167)))), ((int)(((byte)(169)))));
			this.UpdateFromFileButton.Size = new System.Drawing.Size(230, 23);
			this.UpdateFromFileButton.TabIndex = 37;
			this.UpdateFromFileButton.Text = "Update from file";
			// 
			// ReadDataflashButton
			// 
			this.ReadDataflashButton.Cursor = System.Windows.Forms.Cursors.Hand;
			this.ReadDataflashButton.DrawBorders = false;
			this.ReadDataflashButton.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ReadDataflashButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(146)))), ((int)(((byte)(221)))));
			this.MainLocalizationExtender.SetKey(this.ReadDataflashButton, "Toolbox.FirmwareUpdater.ReadDataflashButton");
			this.ReadDataflashButton.Location = new System.Drawing.Point(240, 98);
			this.ReadDataflashButton.MouserDownPrimaryTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(93)))), ((int)(((byte)(187)))));
			this.ReadDataflashButton.MouserOverPrimaryTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(120)))), ((int)(((byte)(206)))));
			this.ReadDataflashButton.Name = "ReadDataflashButton";
			this.ReadDataflashButton.PrimaryTextDisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(167)))), ((int)(((byte)(169)))));
			this.ReadDataflashButton.Size = new System.Drawing.Size(230, 23);
			this.ReadDataflashButton.TabIndex = 38;
			this.ReadDataflashButton.Text = "Read dataflash";
			// 
			// WriteDataflashButton
			// 
			this.WriteDataflashButton.Cursor = System.Windows.Forms.Cursors.Hand;
			this.WriteDataflashButton.DrawBorders = false;
			this.WriteDataflashButton.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.WriteDataflashButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(146)))), ((int)(((byte)(221)))));
			this.MainLocalizationExtender.SetKey(this.WriteDataflashButton, "Toolbox.FirmwareUpdater.WriteDataflashButton");
			this.WriteDataflashButton.Location = new System.Drawing.Point(240, 127);
			this.WriteDataflashButton.MouserDownPrimaryTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(93)))), ((int)(((byte)(187)))));
			this.WriteDataflashButton.MouserOverPrimaryTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(120)))), ((int)(((byte)(206)))));
			this.WriteDataflashButton.Name = "WriteDataflashButton";
			this.WriteDataflashButton.PrimaryTextDisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(167)))), ((int)(((byte)(169)))));
			this.WriteDataflashButton.Size = new System.Drawing.Size(230, 23);
			this.WriteDataflashButton.TabIndex = 39;
			this.WriteDataflashButton.Text = "Write dataflash";
			// 
			// ResetDataflashButton
			// 
			this.ResetDataflashButton.Cursor = System.Windows.Forms.Cursors.Hand;
			this.ResetDataflashButton.DrawBorders = false;
			this.ResetDataflashButton.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ResetDataflashButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(146)))), ((int)(((byte)(221)))));
			this.MainLocalizationExtender.SetKey(this.ResetDataflashButton, "Toolbox.FirmwareUpdater.ResetDataFlashButton");
			this.ResetDataflashButton.Location = new System.Drawing.Point(240, 156);
			this.ResetDataflashButton.MouserDownPrimaryTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(93)))), ((int)(((byte)(187)))));
			this.ResetDataflashButton.MouserOverPrimaryTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(120)))), ((int)(((byte)(206)))));
			this.ResetDataflashButton.Name = "ResetDataflashButton";
			this.ResetDataflashButton.PrimaryTextDisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(167)))), ((int)(((byte)(169)))));
			this.ResetDataflashButton.Size = new System.Drawing.Size(230, 23);
			this.ResetDataflashButton.TabIndex = 40;
			this.ResetDataflashButton.Text = "Reset dataflash";
			// 
			// ChangeHWButton
			// 
			this.ChangeHWButton.Cursor = System.Windows.Forms.Cursors.Hand;
			this.ChangeHWButton.DrawBorders = false;
			this.ChangeHWButton.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ChangeHWButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(146)))), ((int)(((byte)(221)))));
			this.MainLocalizationExtender.SetKey(this.ChangeHWButton, "Toolbox.FirmwareUpdater.ChangeHWVerButton");
			this.ChangeHWButton.Location = new System.Drawing.Point(240, 224);
			this.ChangeHWButton.MouserDownPrimaryTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(93)))), ((int)(((byte)(187)))));
			this.ChangeHWButton.MouserOverPrimaryTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(120)))), ((int)(((byte)(206)))));
			this.ChangeHWButton.Name = "ChangeHWButton";
			this.ChangeHWButton.PrimaryTextDisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(167)))), ((int)(((byte)(169)))));
			this.ChangeHWButton.Size = new System.Drawing.Size(230, 23);
			this.ChangeHWButton.TabIndex = 41;
			this.ChangeHWButton.Text = "Change HW Version";
			// 
			// ChangeBootModeButton
			// 
			this.ChangeBootModeButton.Cursor = System.Windows.Forms.Cursors.Hand;
			this.ChangeBootModeButton.DrawBorders = false;
			this.ChangeBootModeButton.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ChangeBootModeButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(146)))), ((int)(((byte)(221)))));
			this.MainLocalizationExtender.SetKey(this.ChangeBootModeButton, "Toolbox.FirmwareUpdater.SwitchBootModeButton");
			this.ChangeBootModeButton.Location = new System.Drawing.Point(240, 253);
			this.ChangeBootModeButton.MouserDownPrimaryTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(93)))), ((int)(((byte)(187)))));
			this.ChangeBootModeButton.MouserOverPrimaryTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(120)))), ((int)(((byte)(206)))));
			this.ChangeBootModeButton.Name = "ChangeBootModeButton";
			this.ChangeBootModeButton.PrimaryTextDisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(167)))), ((int)(((byte)(169)))));
			this.ChangeBootModeButton.Size = new System.Drawing.Size(230, 23);
			this.ChangeBootModeButton.TabIndex = 42;
			this.ChangeBootModeButton.Text = "Switch boot mode";
			// 
			// ConnectionPictureBox
			// 
			this.ConnectionPictureBox.BackgroundImage = global::NToolbox.Properties.Resources.connection_inactive;
			this.ConnectionPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.MainLocalizationExtender.SetKey(this.ConnectionPictureBox, "");
			this.ConnectionPictureBox.Location = new System.Drawing.Point(93, 193);
			this.ConnectionPictureBox.Name = "ConnectionPictureBox";
			this.ConnectionPictureBox.Size = new System.Drawing.Size(32, 32);
			this.ConnectionPictureBox.TabIndex = 43;
			this.ConnectionPictureBox.TabStop = false;
			// 
			// pictureBox2
			// 
			this.pictureBox2.BackgroundImage = global::NToolbox.Properties.Resources.gray_white_separator;
			this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.MainLocalizationExtender.SetKey(this.pictureBox2, "");
			this.pictureBox2.Location = new System.Drawing.Point(225, 0);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(1, 300);
			this.pictureBox2.TabIndex = 14;
			this.pictureBox2.TabStop = false;
			// 
			// FirmwareUpdaterWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(478, 339);
			this.Controls.Add(this.ConnectionPictureBox);
			this.Controls.Add(this.ControlBorderedPanel);
			this.Controls.Add(this.ChangeBootModeButton);
			this.Controls.Add(this.ChangeHWButton);
			this.Controls.Add(this.ResetDataflashButton);
			this.Controls.Add(this.WriteDataflashButton);
			this.Controls.Add(this.ReadDataflashButton);
			this.Controls.Add(this.UpdateFromFileButton);
			this.Controls.Add(this.panel3);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.borderedPanel4);
			this.Controls.Add(this.borderedPanel3);
			this.Controls.Add(this.borderedPanel2);
			this.Controls.Add(this.borderedPanel1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.UpdateProgressBar);
			this.Controls.Add(this.pictureBox2);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MainLocalizationExtender.SetKey(this, "");
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FirmwareUpdaterWindow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "NToolbox - Updater";
			this.ControlBorderedPanel.ResumeLayout(false);
			this.borderedPanel1.ResumeLayout(false);
			this.borderedPanel1.PerformLayout();
			this.borderedPanel2.ResumeLayout(false);
			this.borderedPanel2.PerformLayout();
			this.borderedPanel3.ResumeLayout(false);
			this.borderedPanel3.PerformLayout();
			this.borderedPanel4.ResumeLayout(false);
			this.borderedPanel4.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.ConnectionPictureBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private NCore.UI.BorderedPanel ControlBorderedPanel;
		private System.Windows.Forms.Button CancelButton;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox HardwareVersionTextBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox FirmwareVersionTextBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox DeviceNameTextBox;
		private System.Windows.Forms.ProgressBar UpdateProgressBar;
		private System.Windows.Forms.Label UpdateStatusLabel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox BootModeTextBox;
		private System.Windows.Forms.PictureBox pictureBox2;
		private NCore.UI.BorderedPanel borderedPanel1;
		private NCore.UI.BorderedPanel borderedPanel2;
		private NCore.UI.BorderedPanel borderedPanel3;
		private NCore.UI.BorderedPanel borderedPanel4;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Label label7;
		private NCore.UI.ExtendedButton UpdateFromFileButton;
		private NCore.UI.ExtendedButton ReadDataflashButton;
		private NCore.UI.ExtendedButton WriteDataflashButton;
		private NCore.UI.ExtendedButton ResetDataflashButton;
		private NCore.UI.ExtendedButton ChangeHWButton;
		private NCore.UI.ExtendedButton ChangeBootModeButton;
		private System.Windows.Forms.PictureBox ConnectionPictureBox;
	}
}