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
			this.LanguageMenuButton = new NCore.UI.MenuButton();
			this.AboutLinkLabel = new System.Windows.Forms.LinkLabel();
			this.ScreenshooterButton = new NCore.UI.ExtendedButton();
			this.DeviceMonitorButton = new NCore.UI.ExtendedButton();
			this.ArcticFoxConfigurationButton = new NCore.UI.ExtendedButton();
			this.FirmwareUpdaterButton = new NCore.UI.ExtendedButton();
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
			this.TrayNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.TaglineLabel = new System.Windows.Forms.Label();
			this.TitleLabel = new System.Windows.Forms.Label();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.LogoPictureBox = new System.Windows.Forms.PictureBox();
			this.TrayContextMenuStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.LogoPictureBox)).BeginInit();
			this.SuspendLayout();
			// 
			// VersionLabel
			// 
			this.VersionLabel.BackColor = System.Drawing.Color.Transparent;
			this.VersionLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.VersionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(167)))), ((int)(((byte)(169)))));
			this.MainLocalizationExtender.SetKey(this.VersionLabel, "");
			this.VersionLabel.Location = new System.Drawing.Point(7, 191);
			this.VersionLabel.Name = "VersionLabel";
			this.VersionLabel.Size = new System.Drawing.Size(176, 18);
			this.VersionLabel.TabIndex = 5;
			this.VersionLabel.Text = "v1.1";
			this.VersionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// LanguageMenuButton
			// 
			this.LanguageMenuButton.AutoEllipsis = true;
			this.LanguageMenuButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.MainLocalizationExtender.SetKey(this.LanguageMenuButton, "");
			this.LanguageMenuButton.Location = new System.Drawing.Point(55, 255);
			this.LanguageMenuButton.Name = "LanguageMenuButton";
			this.LanguageMenuButton.Padding = new System.Windows.Forms.Padding(0, 0, 8, 0);
			this.LanguageMenuButton.Size = new System.Drawing.Size(80, 24);
			this.LanguageMenuButton.TabIndex = 11;
			this.LanguageMenuButton.Text = "EN";
			this.LanguageMenuButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.LanguageMenuButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.LanguageMenuButton.UseVisualStyleBackColor = true;
			// 
			// AboutLinkLabel
			// 
			this.AboutLinkLabel.ActiveLinkColor = System.Drawing.Color.SlateGray;
			this.AboutLinkLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.MainLocalizationExtender.SetKey(this.AboutLinkLabel, "Toolbox.MainWindow.AboutLink");
			this.AboutLinkLabel.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(146)))), ((int)(((byte)(221)))));
			this.AboutLinkLabel.Location = new System.Drawing.Point(7, 211);
			this.AboutLinkLabel.Name = "AboutLinkLabel";
			this.AboutLinkLabel.Size = new System.Drawing.Size(176, 36);
			this.AboutLinkLabel.TabIndex = 8;
			this.AboutLinkLabel.TabStop = true;
			this.AboutLinkLabel.Text = "About && Links";
			this.AboutLinkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.AboutLinkLabel.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(146)))), ((int)(((byte)(221)))));
			// 
			// ScreenshooterButton
			// 
			this.ScreenshooterButton.AdditionalText = "Share screenshots of your device";
			this.ScreenshooterButton.AdditionalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(167)))), ((int)(((byte)(169)))));
			this.ScreenshooterButton.AdditionalTextFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ScreenshooterButton.Cursor = System.Windows.Forms.Cursors.Hand;
			this.ScreenshooterButton.DrawBorders = false;
			this.ScreenshooterButton.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
			this.ScreenshooterButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(146)))), ((int)(((byte)(221)))));
			this.MainLocalizationExtender.SetKey(this.ScreenshooterButton, "Toolbox.MainWindow.ScreenshooterButton");
			this.ScreenshooterButton.Location = new System.Drawing.Point(200, 182);
			this.ScreenshooterButton.MouserDownPrimaryTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(93)))), ((int)(((byte)(187)))));
			this.ScreenshooterButton.MouserOverPrimaryTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(120)))), ((int)(((byte)(206)))));
			this.ScreenshooterButton.Name = "ScreenshooterButton";
			this.ScreenshooterButton.Size = new System.Drawing.Size(350, 50);
			this.ScreenshooterButton.TabIndex = 4;
			this.ScreenshooterButton.Text = "Screenshooter";
			// 
			// DeviceMonitorButton
			// 
			this.DeviceMonitorButton.AdditionalText = "Gain full control over all sensors readings";
			this.DeviceMonitorButton.AdditionalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(167)))), ((int)(((byte)(169)))));
			this.DeviceMonitorButton.AdditionalTextFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.DeviceMonitorButton.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DeviceMonitorButton.DrawBorders = false;
			this.DeviceMonitorButton.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
			this.DeviceMonitorButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(146)))), ((int)(((byte)(221)))));
			this.MainLocalizationExtender.SetKey(this.DeviceMonitorButton, "Toolbox.MainWindow.DeviceMonitorButton");
			this.DeviceMonitorButton.Location = new System.Drawing.Point(200, 124);
			this.DeviceMonitorButton.MouserDownPrimaryTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(93)))), ((int)(((byte)(187)))));
			this.DeviceMonitorButton.MouserOverPrimaryTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(120)))), ((int)(((byte)(206)))));
			this.DeviceMonitorButton.Name = "DeviceMonitorButton";
			this.DeviceMonitorButton.Size = new System.Drawing.Size(350, 50);
			this.DeviceMonitorButton.TabIndex = 2;
			this.DeviceMonitorButton.Text = "Device Monitor";
			// 
			// ArcticFoxConfigurationButton
			// 
			this.ArcticFoxConfigurationButton.AdditionalText = "Configure your device powered by ArcticFox firmware";
			this.ArcticFoxConfigurationButton.AdditionalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(167)))), ((int)(((byte)(169)))));
			this.ArcticFoxConfigurationButton.AdditionalTextFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ArcticFoxConfigurationButton.Cursor = System.Windows.Forms.Cursors.Hand;
			this.ArcticFoxConfigurationButton.DrawBorders = false;
			this.ArcticFoxConfigurationButton.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
			this.ArcticFoxConfigurationButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(146)))), ((int)(((byte)(221)))));
			this.ArcticFoxConfigurationButton.ImageSize = new System.Drawing.Size(48, 48);
			this.MainLocalizationExtender.SetKey(this.ArcticFoxConfigurationButton, "Toolbox.MainWindow.ArcticFoxConfigurationButton");
			this.ArcticFoxConfigurationButton.Location = new System.Drawing.Point(200, 66);
			this.ArcticFoxConfigurationButton.MouserDownPrimaryTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(93)))), ((int)(((byte)(187)))));
			this.ArcticFoxConfigurationButton.MouserOverPrimaryTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(120)))), ((int)(((byte)(206)))));
			this.ArcticFoxConfigurationButton.Name = "ArcticFoxConfigurationButton";
			this.ArcticFoxConfigurationButton.Size = new System.Drawing.Size(350, 50);
			this.ArcticFoxConfigurationButton.TabIndex = 0;
			this.ArcticFoxConfigurationButton.Text = "ArcticFox Configuration";
			// 
			// FirmwareUpdaterButton
			// 
			this.FirmwareUpdaterButton.AdditionalText = "Install a new firmware or upgrade existing one";
			this.FirmwareUpdaterButton.AdditionalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(167)))), ((int)(((byte)(169)))));
			this.FirmwareUpdaterButton.AdditionalTextFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FirmwareUpdaterButton.Cursor = System.Windows.Forms.Cursors.Hand;
			this.FirmwareUpdaterButton.DrawBorders = false;
			this.FirmwareUpdaterButton.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
			this.FirmwareUpdaterButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(146)))), ((int)(((byte)(221)))));
			this.MainLocalizationExtender.SetKey(this.FirmwareUpdaterButton, "Toolbox.MainWindow.FirmwareUpdaterButton");
			this.FirmwareUpdaterButton.Location = new System.Drawing.Point(200, 240);
			this.FirmwareUpdaterButton.MouserDownPrimaryTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(93)))), ((int)(((byte)(187)))));
			this.FirmwareUpdaterButton.MouserOverPrimaryTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(120)))), ((int)(((byte)(206)))));
			this.FirmwareUpdaterButton.Name = "FirmwareUpdaterButton";
			this.FirmwareUpdaterButton.Size = new System.Drawing.Size(350, 50);
			this.FirmwareUpdaterButton.TabIndex = 3;
			this.FirmwareUpdaterButton.Text = "Firmware Updater";
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
			// TrayNotifyIcon
			// 
			this.TrayNotifyIcon.ContextMenuStrip = this.TrayContextMenuStrip;
			this.TrayNotifyIcon.Text = "NFE Toolbox";
			this.TrayNotifyIcon.Visible = true;
			// 
			// TaglineLabel
			// 
			this.TaglineLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.TaglineLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(167)))), ((int)(((byte)(169)))));
			this.MainLocalizationExtender.SetKey(this.TaglineLabel, "Toolbox.MainWindow.TaglineLabel");
			this.TaglineLabel.Location = new System.Drawing.Point(205, 36);
			this.TaglineLabel.Name = "TaglineLabel";
			this.TaglineLabel.Size = new System.Drawing.Size(350, 20);
			this.TaglineLabel.TabIndex = 15;
			this.TaglineLabel.Text = "your access to almost limitless possibilities";
			// 
			// TitleLabel
			// 
			this.TitleLabel.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.TitleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(91)))), ((int)(((byte)(108)))));
			this.MainLocalizationExtender.SetKey(this.TitleLabel, "Toolbox.MainWindow.TitleLabel");
			this.TitleLabel.Location = new System.Drawing.Point(202, 6);
			this.TitleLabel.Name = "TitleLabel";
			this.TitleLabel.Size = new System.Drawing.Size(350, 30);
			this.TitleLabel.TabIndex = 14;
			this.TitleLabel.Text = "Welcome to NToolbox";
			// 
			// pictureBox2
			// 
			this.pictureBox2.BackgroundImage = global::NToolbox.Properties.Resources.gray_white_separator;
			this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.MainLocalizationExtender.SetKey(this.pictureBox2, "");
			this.pictureBox2.Location = new System.Drawing.Point(180, 0);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(1, 300);
			this.pictureBox2.TabIndex = 13;
			this.pictureBox2.TabStop = false;
			// 
			// LogoPictureBox
			// 
			this.LogoPictureBox.BackgroundImage = global::NToolbox.Properties.Resources.ntoolbox_logo;
			this.LogoPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.MainLocalizationExtender.SetKey(this.LogoPictureBox, "");
			this.LogoPictureBox.Location = new System.Drawing.Point(11, 10);
			this.LogoPictureBox.Name = "LogoPictureBox";
			this.LogoPictureBox.Size = new System.Drawing.Size(160, 180);
			this.LogoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.LogoPictureBox.TabIndex = 0;
			this.LogoPictureBox.TabStop = false;
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(558, 301);
			this.Controls.Add(this.TaglineLabel);
			this.Controls.Add(this.TitleLabel);
			this.Controls.Add(this.pictureBox2);
			this.Controls.Add(this.LanguageMenuButton);
			this.Controls.Add(this.AboutLinkLabel);
			this.Controls.Add(this.ScreenshooterButton);
			this.Controls.Add(this.DeviceMonitorButton);
			this.Controls.Add(this.VersionLabel);
			this.Controls.Add(this.ArcticFoxConfigurationButton);
			this.Controls.Add(this.FirmwareUpdaterButton);
			this.Controls.Add(this.LogoPictureBox);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MainLocalizationExtender.SetKey(this, "");
			this.MaximizeBox = false;
			this.Name = "MainWindow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "NToolbox";
			this.TrayContextMenuStrip.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.LogoPictureBox)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private NCore.UI.ExtendedButton ArcticFoxConfigurationButton;
		private NCore.UI.ExtendedButton DeviceMonitorButton;
		private NCore.UI.ExtendedButton FirmwareUpdaterButton;
		private System.Windows.Forms.PictureBox LogoPictureBox;
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
		private NCore.UI.MenuButton LanguageMenuButton;
		private System.Windows.Forms.PictureBox pictureBox2;
		private System.Windows.Forms.Label TaglineLabel;
		private System.Windows.Forms.Label TitleLabel;
	}
}

