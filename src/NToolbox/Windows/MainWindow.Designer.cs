namespace NToolbox.Windows
{
	partial class MainWindow
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
			this.components = new System.ComponentModel.Container();
			this.VersionLabel = new System.Windows.Forms.Label();
			this.ButtonsPanel = new NCore.UI.BorderedPanel();
			this.borderedPanel1 = new NCore.UI.BorderedPanel();
			this.LanguageButton = new System.Windows.Forms.Button();
			this.AboutLinkLabel = new System.Windows.Forms.LinkLabel();
			this.ScreenshooterButton = new NCore.UI.ExtendedButton();
			this.DeviceMonitorButton = new NCore.UI.ExtendedButton();
			this.ArcticFoxConfigurationButton = new NCore.UI.ExtendedButton();
			this.FirmwareUpdaterButton = new NCore.UI.ExtendedButton();
			this.LogoPictureBox = new System.Windows.Forms.PictureBox();
			this.TrayNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.TrayContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.ShowTrayMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.ArcticFoxConfigurationTrayMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.DeviceMonitorTrayMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ScreenshooterTrayMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.FirmwareUpdaterTrayMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.OpenArcticFoxConfigurationTrayMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.CloseArcticFoxConfigurationTrayMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.TimeSyncTrayMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.AutorunTrayMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.ExitTrayMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ButtonsPanel.SuspendLayout();
			this.borderedPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.LogoPictureBox)).BeginInit();
			this.TrayContextMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// VersionLabel
			// 
			this.VersionLabel.AutoSize = true;
			this.VersionLabel.BackColor = System.Drawing.Color.Transparent;
			this.MainLocalizationExtender.SetKey(this.VersionLabel, "");
			this.VersionLabel.Location = new System.Drawing.Point(196, 163);
			this.VersionLabel.Name = "VersionLabel";
			this.VersionLabel.Size = new System.Drawing.Size(29, 13);
			this.VersionLabel.TabIndex = 5;
			this.VersionLabel.Text = "v1.1";
			// 
			// ButtonsPanel
			// 
			this.ButtonsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ButtonsPanel.BackColor = System.Drawing.Color.White;
			this.ButtonsPanel.BorderBottom = false;
			this.ButtonsPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(185)))), ((int)(((byte)(185)))));
			this.ButtonsPanel.BorderLeft = false;
			this.ButtonsPanel.BorderRight = false;
			this.ButtonsPanel.BorderTop = false;
			this.ButtonsPanel.BorderWidth = 1F;
			this.ButtonsPanel.Controls.Add(this.borderedPanel1);
			this.ButtonsPanel.Controls.Add(this.AboutLinkLabel);
			this.ButtonsPanel.Controls.Add(this.ScreenshooterButton);
			this.ButtonsPanel.Controls.Add(this.DeviceMonitorButton);
			this.ButtonsPanel.Controls.Add(this.ArcticFoxConfigurationButton);
			this.ButtonsPanel.Controls.Add(this.FirmwareUpdaterButton);
			this.MainLocalizationExtender.SetKey(this.ButtonsPanel, "");
			this.ButtonsPanel.Location = new System.Drawing.Point(0, 214);
			this.ButtonsPanel.Name = "ButtonsPanel";
			this.ButtonsPanel.Size = new System.Drawing.Size(240, 295);
			this.ButtonsPanel.TabIndex = 4;
			// 
			// borderedPanel1
			// 
			this.borderedPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
			this.borderedPanel1.BorderBottom = false;
			this.borderedPanel1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(185)))), ((int)(((byte)(185)))));
			this.borderedPanel1.BorderLeft = false;
			this.borderedPanel1.BorderRight = false;
			this.borderedPanel1.BorderTop = true;
			this.borderedPanel1.BorderWidth = 1F;
			this.borderedPanel1.Controls.Add(this.LanguageButton);
			this.borderedPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.MainLocalizationExtender.SetKey(this.borderedPanel1, "");
			this.borderedPanel1.Location = new System.Drawing.Point(0, 260);
			this.borderedPanel1.Name = "borderedPanel1";
			this.borderedPanel1.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
			this.borderedPanel1.Size = new System.Drawing.Size(240, 35);
			this.borderedPanel1.TabIndex = 10;
			// 
			// LanguageButton
			// 
			this.LanguageButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.MainLocalizationExtender.SetKey(this.LanguageButton, "");
			this.LanguageButton.Location = new System.Drawing.Point(80, 6);
			this.LanguageButton.Name = "LanguageButton";
			this.LanguageButton.Size = new System.Drawing.Size(80, 24);
			this.LanguageButton.TabIndex = 10;
			this.LanguageButton.TabStop = false;
			this.LanguageButton.Text = "EN";
			this.LanguageButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.LanguageButton.UseVisualStyleBackColor = true;
			// 
			// AboutLinkLabel
			// 
			this.AboutLinkLabel.ActiveLinkColor = System.Drawing.Color.SlateGray;
			this.AboutLinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.AboutLinkLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.MainLocalizationExtender.SetKey(this.AboutLinkLabel, "Toolbox.MainWindow.AboutLink");
			this.AboutLinkLabel.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(129)))), ((int)(((byte)(230)))));
			this.AboutLinkLabel.Location = new System.Drawing.Point(10, 236);
			this.AboutLinkLabel.Name = "AboutLinkLabel";
			this.AboutLinkLabel.Size = new System.Drawing.Size(220, 18);
			this.AboutLinkLabel.TabIndex = 8;
			this.AboutLinkLabel.TabStop = true;
			this.AboutLinkLabel.Text = "About && Links";
			this.AboutLinkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.AboutLinkLabel.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(129)))), ((int)(((byte)(230)))));
			// 
			// ScreenshooterButton
			// 
			this.ScreenshooterButton.AdditionalText = "";
			this.ScreenshooterButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ScreenshooterButton.Image = global::NToolbox.Properties.Resources.device_broadcast;
			this.MainLocalizationExtender.SetKey(this.ScreenshooterButton, "Toolbox.MainWindow.ScreenshooterButton");
			this.ScreenshooterButton.Location = new System.Drawing.Point(10, 122);
			this.ScreenshooterButton.Name = "ScreenshooterButton";
			this.ScreenshooterButton.Size = new System.Drawing.Size(220, 52);
			this.ScreenshooterButton.TabIndex = 4;
			this.ScreenshooterButton.Text = "Screenshooter";
			// 
			// DeviceMonitorButton
			// 
			this.DeviceMonitorButton.AdditionalText = "";
			this.DeviceMonitorButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.DeviceMonitorButton.Image = global::NToolbox.Properties.Resources.device_monitor;
			this.MainLocalizationExtender.SetKey(this.DeviceMonitorButton, "Toolbox.MainWindow.DeviceMonitorButton");
			this.DeviceMonitorButton.Location = new System.Drawing.Point(10, 64);
			this.DeviceMonitorButton.Name = "DeviceMonitorButton";
			this.DeviceMonitorButton.Size = new System.Drawing.Size(220, 52);
			this.DeviceMonitorButton.TabIndex = 2;
			this.DeviceMonitorButton.Text = "Device Monitor";
			// 
			// ArcticFoxConfigurationButton
			// 
			this.ArcticFoxConfigurationButton.AdditionalText = "";
			this.ArcticFoxConfigurationButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ArcticFoxConfigurationButton.Image = global::NToolbox.Properties.Resources.arctic_fox;
			this.MainLocalizationExtender.SetKey(this.ArcticFoxConfigurationButton, "Toolbox.MainWindow.ArcticFoxConfigurationButton");
			this.ArcticFoxConfigurationButton.Location = new System.Drawing.Point(10, 6);
			this.ArcticFoxConfigurationButton.Name = "ArcticFoxConfigurationButton";
			this.ArcticFoxConfigurationButton.Size = new System.Drawing.Size(220, 52);
			this.ArcticFoxConfigurationButton.TabIndex = 0;
			this.ArcticFoxConfigurationButton.Text = "ArcticFox Configuration";
			// 
			// FirmwareUpdaterButton
			// 
			this.FirmwareUpdaterButton.AdditionalText = "";
			this.FirmwareUpdaterButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FirmwareUpdaterButton.Image = global::NToolbox.Properties.Resources.firmware_updater;
			this.MainLocalizationExtender.SetKey(this.FirmwareUpdaterButton, "Toolbox.MainWindow.FirmwareUpdaterButton");
			this.FirmwareUpdaterButton.Location = new System.Drawing.Point(10, 180);
			this.FirmwareUpdaterButton.Name = "FirmwareUpdaterButton";
			this.FirmwareUpdaterButton.Size = new System.Drawing.Size(220, 52);
			this.FirmwareUpdaterButton.TabIndex = 3;
			this.FirmwareUpdaterButton.Text = "Firmware Updater";
			// 
			// LogoPictureBox
			// 
			this.LogoPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.LogoPictureBox.Image = global::NToolbox.Properties.Resources.nfetoolbox;
			this.MainLocalizationExtender.SetKey(this.LogoPictureBox, "");
			this.LogoPictureBox.Location = new System.Drawing.Point(30, 5);
			this.LogoPictureBox.Name = "LogoPictureBox";
			this.LogoPictureBox.Size = new System.Drawing.Size(180, 200);
			this.LogoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.LogoPictureBox.TabIndex = 0;
			this.LogoPictureBox.TabStop = false;
			// 
			// TrayNotifyIcon
			// 
			this.TrayNotifyIcon.ContextMenuStrip = this.TrayContextMenuStrip;
			this.TrayNotifyIcon.Text = "NFE Toolbox";
			this.TrayNotifyIcon.Visible = true;
			// 
			// TrayContextMenuStrip
			// 
			this.TrayContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ShowTrayMenuItem,
            this.toolStripSeparator4,
            this.ArcticFoxConfigurationTrayMenuItem,
            this.DeviceMonitorTrayMenuItem,
            this.ScreenshooterTrayMenuItem,
            this.FirmwareUpdaterTrayMenuItem,
            this.toolStripSeparator2,
            this.OpenArcticFoxConfigurationTrayMenuItem,
            this.CloseArcticFoxConfigurationTrayMenuItem,
            this.toolStripSeparator5,
            this.TimeSyncTrayMenuItem,
            this.toolStripSeparator3,
            this.AutorunTrayMenuItem,
            this.toolStripSeparator1,
            this.ExitTrayMenuItem});
			this.MainLocalizationExtender.SetKey(this.TrayContextMenuStrip, "");
			this.TrayContextMenuStrip.Name = "TrayContextMenuStrip";
			this.TrayContextMenuStrip.Size = new System.Drawing.Size(387, 254);
			// 
			// ShowTrayMenuItem
			// 
			this.ShowTrayMenuItem.Name = "ShowTrayMenuItem";
			this.ShowTrayMenuItem.Size = new System.Drawing.Size(386, 22);
			this.ShowTrayMenuItem.Text = "Show NFE Toolbox window";
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(383, 6);
			// 
			// ArcticFoxConfigurationTrayMenuItem
			// 
			this.ArcticFoxConfigurationTrayMenuItem.Name = "ArcticFoxConfigurationTrayMenuItem";
			this.ArcticFoxConfigurationTrayMenuItem.Size = new System.Drawing.Size(386, 22);
			this.ArcticFoxConfigurationTrayMenuItem.Text = "ArcticFox Configuration";
			// 
			// DeviceMonitorTrayMenuItem
			// 
			this.DeviceMonitorTrayMenuItem.Name = "DeviceMonitorTrayMenuItem";
			this.DeviceMonitorTrayMenuItem.Size = new System.Drawing.Size(386, 22);
			this.DeviceMonitorTrayMenuItem.Text = "Device Monitor";
			// 
			// ScreenshooterTrayMenuItem
			// 
			this.ScreenshooterTrayMenuItem.Name = "ScreenshooterTrayMenuItem";
			this.ScreenshooterTrayMenuItem.Size = new System.Drawing.Size(386, 22);
			this.ScreenshooterTrayMenuItem.Text = "Screenshooter";
			// 
			// FirmwareUpdaterTrayMenuItem
			// 
			this.FirmwareUpdaterTrayMenuItem.Name = "FirmwareUpdaterTrayMenuItem";
			this.FirmwareUpdaterTrayMenuItem.Size = new System.Drawing.Size(386, 22);
			this.FirmwareUpdaterTrayMenuItem.Text = "Firmware Updater";
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(383, 6);
			// 
			// OpenArcticFoxConfigurationTrayMenuItem
			// 
			this.OpenArcticFoxConfigurationTrayMenuItem.CheckOnClick = true;
			this.OpenArcticFoxConfigurationTrayMenuItem.Name = "OpenArcticFoxConfigurationTrayMenuItem";
			this.OpenArcticFoxConfigurationTrayMenuItem.Size = new System.Drawing.Size(386, 22);
			this.OpenArcticFoxConfigurationTrayMenuItem.Text = "Open ArcticFox Configuration when device is connected";
			// 
			// CloseArcticFoxConfigurationTrayMenuItem
			// 
			this.CloseArcticFoxConfigurationTrayMenuItem.CheckOnClick = true;
			this.CloseArcticFoxConfigurationTrayMenuItem.Name = "CloseArcticFoxConfigurationTrayMenuItem";
			this.CloseArcticFoxConfigurationTrayMenuItem.Size = new System.Drawing.Size(386, 22);
			this.CloseArcticFoxConfigurationTrayMenuItem.Text = "Close ArcticFox Configuration when device is disconnected";
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(383, 6);
			// 
			// TimeSyncTrayMenuItem
			// 
			this.TimeSyncTrayMenuItem.CheckOnClick = true;
			this.TimeSyncTrayMenuItem.Name = "TimeSyncTrayMenuItem";
			this.TimeSyncTrayMenuItem.Size = new System.Drawing.Size(386, 22);
			this.TimeSyncTrayMenuItem.Text = "Synchronize time when device is connected";
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(383, 6);
			// 
			// AutorunTrayMenuItem
			// 
			this.AutorunTrayMenuItem.CheckOnClick = true;
			this.AutorunTrayMenuItem.Name = "AutorunTrayMenuItem";
			this.AutorunTrayMenuItem.Size = new System.Drawing.Size(386, 22);
			this.AutorunTrayMenuItem.Text = "Run NFE Toolbox when Windows starts";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(383, 6);
			// 
			// ExitTrayMenuItem
			// 
			this.ExitTrayMenuItem.Name = "ExitTrayMenuItem";
			this.ExitTrayMenuItem.Size = new System.Drawing.Size(386, 22);
			this.ExitTrayMenuItem.Text = "Exit";
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(240, 509);
			this.Controls.Add(this.VersionLabel);
			this.Controls.Add(this.ButtonsPanel);
			this.Controls.Add(this.LogoPictureBox);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MainLocalizationExtender.SetKey(this, "");
			this.MaximizeBox = false;
			this.Name = "MainWindow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "NFE Toolbox";
			this.ButtonsPanel.ResumeLayout(false);
			this.borderedPanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.LogoPictureBox)).EndInit();
			this.TrayContextMenuStrip.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private NCore.UI.ExtendedButton ArcticFoxConfigurationButton;
		private NCore.UI.ExtendedButton DeviceMonitorButton;
		private NCore.UI.ExtendedButton FirmwareUpdaterButton;
		private System.Windows.Forms.PictureBox LogoPictureBox;
		private NCore.UI.BorderedPanel ButtonsPanel;
		private NCore.UI.ExtendedButton ScreenshooterButton;
		private System.Windows.Forms.NotifyIcon TrayNotifyIcon;
		private System.Windows.Forms.ContextMenuStrip TrayContextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem ShowTrayMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem ExitTrayMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem AutorunTrayMenuItem;
		private System.Windows.Forms.ToolStripMenuItem OpenArcticFoxConfigurationTrayMenuItem;
		private System.Windows.Forms.ToolStripMenuItem TimeSyncTrayMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem ArcticFoxConfigurationTrayMenuItem;
		private System.Windows.Forms.ToolStripMenuItem DeviceMonitorTrayMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ScreenshooterTrayMenuItem;
		private System.Windows.Forms.ToolStripMenuItem FirmwareUpdaterTrayMenuItem;
		private System.Windows.Forms.Label VersionLabel;
		private System.Windows.Forms.LinkLabel AboutLinkLabel;
		private System.Windows.Forms.ToolStripMenuItem CloseArcticFoxConfigurationTrayMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private NCore.UI.BorderedPanel borderedPanel1;
		private System.Windows.Forms.Button LanguageButton;
	}
}

