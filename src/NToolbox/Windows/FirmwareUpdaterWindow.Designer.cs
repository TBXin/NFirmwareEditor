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
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.ControlBorderedPanel = new NCore.UI.BorderedPanel();
			this.CancelButton = new System.Windows.Forms.Button();
			this.borderedPanel1 = new NCore.UI.BorderedPanel();
			this.borderedPanel2 = new NCore.UI.BorderedPanel();
			this.borderedPanel3 = new NCore.UI.BorderedPanel();
			this.borderedPanel4 = new NCore.UI.BorderedPanel();
			this.panel4 = new System.Windows.Forms.Panel();
			this.CommonLinkLabel = new System.Windows.Forms.LinkLabel();
			this.DataflashLinkLabel = new System.Windows.Forms.LinkLabel();
			this.AdvancedLinkLabel = new System.Windows.Forms.LinkLabel();
			this.multiPanel1 = new NCore.UI.MultiPanel();
			this.CommonPage = new NCore.UI.MultiPanelPage();
			this.UpdateFromFileButton = new System.Windows.Forms.LinkLabel();
			this.DataflashPage = new NCore.UI.MultiPanelPage();
			this.ResetDataflashButton = new System.Windows.Forms.LinkLabel();
			this.WriteDataflashButton = new System.Windows.Forms.LinkLabel();
			this.ReadDataflashButton = new System.Windows.Forms.LinkLabel();
			this.AdvancedPage = new NCore.UI.MultiPanelPage();
			this.ChangeBootModeButton = new System.Windows.Forms.LinkLabel();
			this.ChangeHWButton = new System.Windows.Forms.LinkLabel();
			this.ControlBorderedPanel.SuspendLayout();
			this.borderedPanel1.SuspendLayout();
			this.borderedPanel2.SuspendLayout();
			this.borderedPanel3.SuspendLayout();
			this.borderedPanel4.SuspendLayout();
			this.multiPanel1.SuspendLayout();
			this.CommonPage.SuspendLayout();
			this.DataflashPage.SuspendLayout();
			this.AdvancedPage.SuspendLayout();
			this.SuspendLayout();
			// 
			// UpdateStatusLabel
			// 
			this.UpdateStatusLabel.AutoEllipsis = true;
			this.UpdateStatusLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(167)))), ((int)(((byte)(169)))));
			this.MainLocalizationExtender.SetKey(this.UpdateStatusLabel, "");
			this.UpdateStatusLabel.Location = new System.Drawing.Point(9, 91);
			this.UpdateStatusLabel.Name = "UpdateStatusLabel";
			this.UpdateStatusLabel.Size = new System.Drawing.Size(336, 20);
			this.UpdateStatusLabel.TabIndex = 5;
			this.UpdateStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// UpdateProgressBar
			// 
			this.MainLocalizationExtender.SetKey(this.UpdateProgressBar, "");
			this.UpdateProgressBar.Location = new System.Drawing.Point(9, 115);
			this.UpdateProgressBar.Name = "UpdateProgressBar";
			this.UpdateProgressBar.Size = new System.Drawing.Size(336, 30);
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
			this.BootModeTextBox.Size = new System.Drawing.Size(38, 14);
			this.BootModeTextBox.TabIndex = 25;
			this.BootModeTextBox.TabStop = false;
			this.BootModeTextBox.Text = "APROM";
			this.BootModeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// HardwareVersionTextBox
			// 
			this.HardwareVersionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.HardwareVersionTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.MainLocalizationExtender.SetKey(this.HardwareVersionTextBox, "");
			this.HardwareVersionTextBox.Location = new System.Drawing.Point(4, 5);
			this.HardwareVersionTextBox.Name = "HardwareVersionTextBox";
			this.HardwareVersionTextBox.Size = new System.Drawing.Size(105, 14);
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
			this.FirmwareVersionTextBox.Size = new System.Drawing.Size(157, 14);
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
			this.DeviceNameTextBox.Size = new System.Drawing.Size(328, 14);
			this.DeviceNameTextBox.TabIndex = 19;
			this.DeviceNameTextBox.TabStop = false;
			this.DeviceNameTextBox.Text = "[W057] Vapor Shark SwitchBox RX";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(167)))), ((int)(((byte)(169)))));
			this.MainLocalizationExtender.SetKey(this.label5, "Toolbox.FirmwareUpdater.HardwareVersionLabel");
			this.label5.Location = new System.Drawing.Point(177, 48);
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
			this.label4.Location = new System.Drawing.Point(6, 48);
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
			this.ControlBorderedPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.ControlBorderedPanel.BorderLeft = false;
			this.ControlBorderedPanel.BorderRight = false;
			this.ControlBorderedPanel.BorderTop = true;
			this.ControlBorderedPanel.BorderWidth = 1F;
			this.ControlBorderedPanel.Controls.Add(this.CancelButton);
			this.ControlBorderedPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.MainLocalizationExtender.SetKey(this.ControlBorderedPanel, "");
			this.ControlBorderedPanel.Location = new System.Drawing.Point(0, 219);
			this.ControlBorderedPanel.Name = "ControlBorderedPanel";
			this.ControlBorderedPanel.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
			this.ControlBorderedPanel.Size = new System.Drawing.Size(354, 44);
			this.ControlBorderedPanel.TabIndex = 3;
			this.ControlBorderedPanel.Text = "borderedPanel1";
			// 
			// CancelButton
			// 
			this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.MainLocalizationExtender.SetKey(this.CancelButton, "Toolbox.FirmwareUpdater.CancelButton");
			this.CancelButton.Location = new System.Drawing.Point(250, 5);
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
			this.borderedPanel1.Location = new System.Drawing.Point(9, 22);
			this.borderedPanel1.Name = "borderedPanel1";
			this.borderedPanel1.Padding = new System.Windows.Forms.Padding(1);
			this.borderedPanel1.Size = new System.Drawing.Size(336, 23);
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
			this.borderedPanel2.Location = new System.Drawing.Point(9, 64);
			this.borderedPanel2.Name = "borderedPanel2";
			this.borderedPanel2.Padding = new System.Windows.Forms.Padding(1);
			this.borderedPanel2.Size = new System.Drawing.Size(165, 23);
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
			this.borderedPanel3.Location = new System.Drawing.Point(299, 64);
			this.borderedPanel3.Name = "borderedPanel3";
			this.borderedPanel3.Padding = new System.Windows.Forms.Padding(1);
			this.borderedPanel3.Size = new System.Drawing.Size(46, 23);
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
			this.borderedPanel4.Location = new System.Drawing.Point(180, 64);
			this.borderedPanel4.Name = "borderedPanel4";
			this.borderedPanel4.Padding = new System.Windows.Forms.Padding(1);
			this.borderedPanel4.Size = new System.Drawing.Size(113, 23);
			this.borderedPanel4.TabIndex = 29;
			// 
			// panel4
			// 
			this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.MainLocalizationExtender.SetKey(this.panel4, "");
			this.panel4.Location = new System.Drawing.Point(9, 169);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(336, 1);
			this.panel4.TabIndex = 32;
			// 
			// CommonLinkLabel
			// 
			this.CommonLinkLabel.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(146)))), ((int)(((byte)(221)))));
			this.CommonLinkLabel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.MainLocalizationExtender.SetKey(this.CommonLinkLabel, "Toolbox.FirmwareUpdater.CommonTab");
			this.CommonLinkLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
			this.CommonLinkLabel.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(146)))), ((int)(((byte)(221)))));
			this.CommonLinkLabel.Location = new System.Drawing.Point(10, 153);
			this.CommonLinkLabel.Name = "CommonLinkLabel";
			this.CommonLinkLabel.Size = new System.Drawing.Size(100, 13);
			this.CommonLinkLabel.TabIndex = 47;
			this.CommonLinkLabel.TabStop = true;
			this.CommonLinkLabel.Text = "Common";
			this.CommonLinkLabel.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(146)))), ((int)(((byte)(221)))));
			// 
			// DataflashLinkLabel
			// 
			this.DataflashLinkLabel.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(146)))), ((int)(((byte)(221)))));
			this.DataflashLinkLabel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.MainLocalizationExtender.SetKey(this.DataflashLinkLabel, "Toolbox.FirmwareUpdater.DatalfashTab");
			this.DataflashLinkLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
			this.DataflashLinkLabel.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(110)))), ((int)(((byte)(110)))));
			this.DataflashLinkLabel.Location = new System.Drawing.Point(128, 153);
			this.DataflashLinkLabel.Name = "DataflashLinkLabel";
			this.DataflashLinkLabel.Size = new System.Drawing.Size(100, 13);
			this.DataflashLinkLabel.TabIndex = 48;
			this.DataflashLinkLabel.TabStop = true;
			this.DataflashLinkLabel.Text = "Dataflash";
			this.DataflashLinkLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.DataflashLinkLabel.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(146)))), ((int)(((byte)(221)))));
			// 
			// AdvancedLinkLabel
			// 
			this.AdvancedLinkLabel.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(146)))), ((int)(((byte)(221)))));
			this.AdvancedLinkLabel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.MainLocalizationExtender.SetKey(this.AdvancedLinkLabel, "Toolbox.FirmwareUpdater.AdvancedTab");
			this.AdvancedLinkLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
			this.AdvancedLinkLabel.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(110)))), ((int)(((byte)(110)))));
			this.AdvancedLinkLabel.Location = new System.Drawing.Point(245, 153);
			this.AdvancedLinkLabel.Name = "AdvancedLinkLabel";
			this.AdvancedLinkLabel.Size = new System.Drawing.Size(100, 13);
			this.AdvancedLinkLabel.TabIndex = 49;
			this.AdvancedLinkLabel.TabStop = true;
			this.AdvancedLinkLabel.Text = "Advanced";
			this.AdvancedLinkLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.AdvancedLinkLabel.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(146)))), ((int)(((byte)(221)))));
			// 
			// multiPanel1
			// 
			this.multiPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.multiPanel1.Controls.Add(this.CommonPage);
			this.multiPanel1.Controls.Add(this.DataflashPage);
			this.multiPanel1.Controls.Add(this.AdvancedPage);
			this.MainLocalizationExtender.SetKey(this.multiPanel1, "");
			this.multiPanel1.Location = new System.Drawing.Point(9, 175);
			this.multiPanel1.Name = "multiPanel1";
			this.multiPanel1.SelectedPage = this.DataflashPage;
			this.multiPanel1.Size = new System.Drawing.Size(336, 40);
			this.multiPanel1.TabIndex = 50;
			// 
			// CommonPage
			// 
			this.CommonPage.Controls.Add(this.UpdateFromFileButton);
			this.CommonPage.Description = null;
			this.CommonPage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MainLocalizationExtender.SetKey(this.CommonPage, "");
			this.CommonPage.Location = new System.Drawing.Point(0, 0);
			this.CommonPage.Name = "CommonPage";
			this.CommonPage.Size = new System.Drawing.Size(336, 40);
			this.CommonPage.TabIndex = 0;
			this.CommonPage.Text = "Common";
			// 
			// UpdateFromFileButton
			// 
			this.UpdateFromFileButton.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(146)))), ((int)(((byte)(221)))));
			this.UpdateFromFileButton.DisabledLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(167)))), ((int)(((byte)(169)))));
			this.UpdateFromFileButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.UpdateFromFileButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(167)))), ((int)(((byte)(169)))));
			this.MainLocalizationExtender.SetKey(this.UpdateFromFileButton, "Toolbox.FirmwareUpdater.UpdateButton");
			this.UpdateFromFileButton.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
			this.UpdateFromFileButton.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(146)))), ((int)(((byte)(221)))));
			this.UpdateFromFileButton.Location = new System.Drawing.Point(4, 8);
			this.UpdateFromFileButton.Name = "UpdateFromFileButton";
			this.UpdateFromFileButton.Size = new System.Drawing.Size(328, 20);
			this.UpdateFromFileButton.TabIndex = 42;
			this.UpdateFromFileButton.TabStop = true;
			this.UpdateFromFileButton.Text = "Update from file";
			this.UpdateFromFileButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.UpdateFromFileButton.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(146)))), ((int)(((byte)(221)))));
			// 
			// DataflashPage
			// 
			this.DataflashPage.Controls.Add(this.ResetDataflashButton);
			this.DataflashPage.Controls.Add(this.WriteDataflashButton);
			this.DataflashPage.Controls.Add(this.ReadDataflashButton);
			this.DataflashPage.Description = null;
			this.DataflashPage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MainLocalizationExtender.SetKey(this.DataflashPage, "");
			this.DataflashPage.Location = new System.Drawing.Point(0, 0);
			this.DataflashPage.Name = "DataflashPage";
			this.DataflashPage.Size = new System.Drawing.Size(336, 40);
			this.DataflashPage.TabIndex = 1;
			this.DataflashPage.Text = "Dataflash";
			// 
			// ResetDataflashButton
			// 
			this.ResetDataflashButton.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(146)))), ((int)(((byte)(221)))));
			this.ResetDataflashButton.DisabledLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(167)))), ((int)(((byte)(169)))));
			this.ResetDataflashButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ResetDataflashButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(167)))), ((int)(((byte)(169)))));
			this.MainLocalizationExtender.SetKey(this.ResetDataflashButton, "Toolbox.FirmwareUpdater.ResetDataFlashButton");
			this.ResetDataflashButton.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
			this.ResetDataflashButton.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(146)))), ((int)(((byte)(221)))));
			this.ResetDataflashButton.Location = new System.Drawing.Point(224, 11);
			this.ResetDataflashButton.Name = "ResetDataflashButton";
			this.ResetDataflashButton.Size = new System.Drawing.Size(108, 16);
			this.ResetDataflashButton.TabIndex = 43;
			this.ResetDataflashButton.TabStop = true;
			this.ResetDataflashButton.Text = "Reset dataflash";
			this.ResetDataflashButton.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.ResetDataflashButton.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(146)))), ((int)(((byte)(221)))));
			// 
			// WriteDataflashButton
			// 
			this.WriteDataflashButton.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(146)))), ((int)(((byte)(221)))));
			this.WriteDataflashButton.DisabledLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(167)))), ((int)(((byte)(169)))));
			this.WriteDataflashButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.WriteDataflashButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(167)))), ((int)(((byte)(169)))));
			this.MainLocalizationExtender.SetKey(this.WriteDataflashButton, "Toolbox.FirmwareUpdater.WriteDataflashButton");
			this.WriteDataflashButton.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
			this.WriteDataflashButton.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(146)))), ((int)(((byte)(221)))));
			this.WriteDataflashButton.Location = new System.Drawing.Point(116, 11);
			this.WriteDataflashButton.Name = "WriteDataflashButton";
			this.WriteDataflashButton.Size = new System.Drawing.Size(108, 16);
			this.WriteDataflashButton.TabIndex = 42;
			this.WriteDataflashButton.TabStop = true;
			this.WriteDataflashButton.Text = "Write dataflash";
			this.WriteDataflashButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.WriteDataflashButton.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(146)))), ((int)(((byte)(221)))));
			// 
			// ReadDataflashButton
			// 
			this.ReadDataflashButton.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(146)))), ((int)(((byte)(221)))));
			this.ReadDataflashButton.DisabledLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(167)))), ((int)(((byte)(169)))));
			this.ReadDataflashButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ReadDataflashButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(167)))), ((int)(((byte)(169)))));
			this.MainLocalizationExtender.SetKey(this.ReadDataflashButton, "Toolbox.FirmwareUpdater.ReadDataflashButton");
			this.ReadDataflashButton.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
			this.ReadDataflashButton.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(146)))), ((int)(((byte)(221)))));
			this.ReadDataflashButton.Location = new System.Drawing.Point(4, 11);
			this.ReadDataflashButton.Name = "ReadDataflashButton";
			this.ReadDataflashButton.Size = new System.Drawing.Size(108, 16);
			this.ReadDataflashButton.TabIndex = 41;
			this.ReadDataflashButton.TabStop = true;
			this.ReadDataflashButton.Text = "Read dataflash";
			this.ReadDataflashButton.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(146)))), ((int)(((byte)(221)))));
			// 
			// AdvancedPage
			// 
			this.AdvancedPage.Controls.Add(this.ChangeBootModeButton);
			this.AdvancedPage.Controls.Add(this.ChangeHWButton);
			this.AdvancedPage.Description = null;
			this.AdvancedPage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MainLocalizationExtender.SetKey(this.AdvancedPage, "");
			this.AdvancedPage.Location = new System.Drawing.Point(0, 0);
			this.AdvancedPage.Name = "AdvancedPage";
			this.AdvancedPage.Size = new System.Drawing.Size(336, 40);
			this.AdvancedPage.TabIndex = 2;
			this.AdvancedPage.Text = "Advanced";
			// 
			// ChangeBootModeButton
			// 
			this.ChangeBootModeButton.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(146)))), ((int)(((byte)(221)))));
			this.ChangeBootModeButton.DisabledLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(167)))), ((int)(((byte)(169)))));
			this.ChangeBootModeButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ChangeBootModeButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(167)))), ((int)(((byte)(169)))));
			this.MainLocalizationExtender.SetKey(this.ChangeBootModeButton, "Toolbox.FirmwareUpdater.SwitchBootModeButton");
			this.ChangeBootModeButton.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
			this.ChangeBootModeButton.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(146)))), ((int)(((byte)(221)))));
			this.ChangeBootModeButton.Location = new System.Drawing.Point(174, 11);
			this.ChangeBootModeButton.Name = "ChangeBootModeButton";
			this.ChangeBootModeButton.Size = new System.Drawing.Size(158, 16);
			this.ChangeBootModeButton.TabIndex = 44;
			this.ChangeBootModeButton.TabStop = true;
			this.ChangeBootModeButton.Text = "Switch boot mode";
			this.ChangeBootModeButton.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.ChangeBootModeButton.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(146)))), ((int)(((byte)(221)))));
			// 
			// ChangeHWButton
			// 
			this.ChangeHWButton.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(146)))), ((int)(((byte)(221)))));
			this.ChangeHWButton.DisabledLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(167)))), ((int)(((byte)(169)))));
			this.ChangeHWButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ChangeHWButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(167)))), ((int)(((byte)(169)))));
			this.MainLocalizationExtender.SetKey(this.ChangeHWButton, "Toolbox.FirmwareUpdater.ChangeHWVerButton");
			this.ChangeHWButton.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
			this.ChangeHWButton.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(146)))), ((int)(((byte)(221)))));
			this.ChangeHWButton.Location = new System.Drawing.Point(4, 11);
			this.ChangeHWButton.Name = "ChangeHWButton";
			this.ChangeHWButton.Size = new System.Drawing.Size(158, 16);
			this.ChangeHWButton.TabIndex = 43;
			this.ChangeHWButton.TabStop = true;
			this.ChangeHWButton.Text = "Change HW Version";
			this.ChangeHWButton.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(146)))), ((int)(((byte)(221)))));
			// 
			// FirmwareUpdaterWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(354, 263);
			this.Controls.Add(this.multiPanel1);
			this.Controls.Add(this.UpdateStatusLabel);
			this.Controls.Add(this.AdvancedLinkLabel);
			this.Controls.Add(this.DataflashLinkLabel);
			this.Controls.Add(this.CommonLinkLabel);
			this.Controls.Add(this.panel4);
			this.Controls.Add(this.ControlBorderedPanel);
			this.Controls.Add(this.borderedPanel4);
			this.Controls.Add(this.borderedPanel3);
			this.Controls.Add(this.borderedPanel2);
			this.Controls.Add(this.borderedPanel1);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.UpdateProgressBar);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MainLocalizationExtender.SetKey(this, "");
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FirmwareUpdaterWindow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "NToolbox - Updater";
			this.Load += new System.EventHandler(this.FirmwareUpdaterWindow_Load);
			this.ControlBorderedPanel.ResumeLayout(false);
			this.borderedPanel1.ResumeLayout(false);
			this.borderedPanel1.PerformLayout();
			this.borderedPanel2.ResumeLayout(false);
			this.borderedPanel2.PerformLayout();
			this.borderedPanel3.ResumeLayout(false);
			this.borderedPanel3.PerformLayout();
			this.borderedPanel4.ResumeLayout(false);
			this.borderedPanel4.PerformLayout();
			this.multiPanel1.ResumeLayout(false);
			this.CommonPage.ResumeLayout(false);
			this.DataflashPage.ResumeLayout(false);
			this.AdvancedPage.ResumeLayout(false);
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
		private System.Windows.Forms.TextBox BootModeTextBox;
		private NCore.UI.BorderedPanel borderedPanel1;
		private NCore.UI.BorderedPanel borderedPanel2;
		private NCore.UI.BorderedPanel borderedPanel3;
		private NCore.UI.BorderedPanel borderedPanel4;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.LinkLabel CommonLinkLabel;
		private System.Windows.Forms.LinkLabel DataflashLinkLabel;
		private System.Windows.Forms.LinkLabel AdvancedLinkLabel;
		private NCore.UI.MultiPanel multiPanel1;
		private NCore.UI.MultiPanelPage CommonPage;
		private NCore.UI.MultiPanelPage DataflashPage;
		private NCore.UI.MultiPanelPage AdvancedPage;
		private System.Windows.Forms.LinkLabel ResetDataflashButton;
		private System.Windows.Forms.LinkLabel WriteDataflashButton;
		private System.Windows.Forms.LinkLabel ReadDataflashButton;
		private System.Windows.Forms.LinkLabel UpdateFromFileButton;
		private System.Windows.Forms.LinkLabel ChangeBootModeButton;
		private System.Windows.Forms.LinkLabel ChangeHWButton;
	}
}