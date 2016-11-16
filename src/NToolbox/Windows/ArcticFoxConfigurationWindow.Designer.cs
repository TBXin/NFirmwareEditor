namespace NToolbox.Windows
{
	partial class ArcticFoxConfigurationWindow
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
			this.MainContainer = new NCore.UI.MultiPanel();
			this.WelcomePage = new NCore.UI.MultiPanelPage();
			this.WelcomeLabel = new System.Windows.Forms.Label();
			this.WorkspacePage = new NCore.UI.MultiPanelPage();
			this.MainTabControl = new System.Windows.Forms.TabControl();
			this.ProfilesTabPage = new System.Windows.Forms.TabPage();
			this.ProfilesTabControl = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.GeneralTabPage = new System.Windows.Forms.TabPage();
			this.groupPanel1 = new NCore.UI.GroupPanel();
			this.BuildTextBox = new System.Windows.Forms.TextBox();
			this.label48 = new System.Windows.Forms.Label();
			this.FirmwareVersionTextBox = new System.Windows.Forms.TextBox();
			this.HardwareVersionTextBox = new System.Windows.Forms.TextBox();
			this.DeviceNameLabel = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.MainContainer.SuspendLayout();
			this.WelcomePage.SuspendLayout();
			this.WorkspacePage.SuspendLayout();
			this.MainTabControl.SuspendLayout();
			this.ProfilesTabPage.SuspendLayout();
			this.ProfilesTabControl.SuspendLayout();
			this.groupPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// MainContainer
			// 
			this.MainContainer.Controls.Add(this.WelcomePage);
			this.MainContainer.Controls.Add(this.WorkspacePage);
			this.MainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MainContainer.Location = new System.Drawing.Point(0, 0);
			this.MainContainer.Name = "MainContainer";
			this.MainContainer.SelectedPage = this.WelcomePage;
			this.MainContainer.Size = new System.Drawing.Size(374, 516);
			this.MainContainer.TabIndex = 0;
			// 
			// WelcomePage
			// 
			this.WelcomePage.Controls.Add(this.pictureBox1);
			this.WelcomePage.Controls.Add(this.WelcomeLabel);
			this.WelcomePage.Description = null;
			this.WelcomePage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.WelcomePage.Location = new System.Drawing.Point(0, 0);
			this.WelcomePage.Name = "WelcomePage";
			this.WelcomePage.Size = new System.Drawing.Size(374, 516);
			this.WelcomePage.TabIndex = 0;
			this.WelcomePage.Text = "WelcomePage";
			// 
			// WelcomeLabel
			// 
			this.WelcomeLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.WelcomeLabel.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.WelcomeLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.WelcomeLabel.Location = new System.Drawing.Point(0, 191);
			this.WelcomeLabel.Name = "WelcomeLabel";
			this.WelcomeLabel.Size = new System.Drawing.Size(374, 325);
			this.WelcomeLabel.TabIndex = 2;
			this.WelcomeLabel.Text = "Waiting for device...";
			this.WelcomeLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// WorkspacePage
			// 
			this.WorkspacePage.Controls.Add(this.MainTabControl);
			this.WorkspacePage.Controls.Add(this.groupPanel1);
			this.WorkspacePage.Description = null;
			this.WorkspacePage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.WorkspacePage.Location = new System.Drawing.Point(0, 0);
			this.WorkspacePage.Name = "WorkspacePage";
			this.WorkspacePage.Size = new System.Drawing.Size(374, 516);
			this.WorkspacePage.TabIndex = 1;
			this.WorkspacePage.Text = "Workspace";
			// 
			// MainTabControl
			// 
			this.MainTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.MainTabControl.Controls.Add(this.ProfilesTabPage);
			this.MainTabControl.Controls.Add(this.GeneralTabPage);
			this.MainTabControl.Location = new System.Drawing.Point(11, 105);
			this.MainTabControl.Name = "MainTabControl";
			this.MainTabControl.SelectedIndex = 0;
			this.MainTabControl.Size = new System.Drawing.Size(354, 399);
			this.MainTabControl.TabIndex = 36;
			// 
			// ProfilesTabPage
			// 
			this.ProfilesTabPage.Controls.Add(this.ProfilesTabControl);
			this.ProfilesTabPage.Location = new System.Drawing.Point(4, 22);
			this.ProfilesTabPage.Name = "ProfilesTabPage";
			this.ProfilesTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.ProfilesTabPage.Size = new System.Drawing.Size(346, 373);
			this.ProfilesTabPage.TabIndex = 0;
			this.ProfilesTabPage.Text = "Profiles";
			this.ProfilesTabPage.UseVisualStyleBackColor = true;
			// 
			// ProfilesTabControl
			// 
			this.ProfilesTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ProfilesTabControl.Controls.Add(this.tabPage1);
			this.ProfilesTabControl.Controls.Add(this.tabPage2);
			this.ProfilesTabControl.ItemSize = new System.Drawing.Size(40, 18);
			this.ProfilesTabControl.Location = new System.Drawing.Point(5, 6);
			this.ProfilesTabControl.Name = "ProfilesTabControl";
			this.ProfilesTabControl.SelectedIndex = 0;
			this.ProfilesTabControl.Size = new System.Drawing.Size(336, 361);
			this.ProfilesTabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
			this.ProfilesTabControl.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(328, 335);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "P1";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// tabPage2
			// 
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(328, 335);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "P2";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// GeneralTabPage
			// 
			this.GeneralTabPage.Location = new System.Drawing.Point(4, 22);
			this.GeneralTabPage.Name = "GeneralTabPage";
			this.GeneralTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.GeneralTabPage.Size = new System.Drawing.Size(346, 373);
			this.GeneralTabPage.TabIndex = 1;
			this.GeneralTabPage.Text = "General";
			this.GeneralTabPage.UseVisualStyleBackColor = true;
			// 
			// groupPanel1
			// 
			this.groupPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupPanel1.BackColor = System.Drawing.SystemColors.Control;
			this.groupPanel1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(185)))), ((int)(((byte)(185)))));
			this.groupPanel1.Controls.Add(this.BuildTextBox);
			this.groupPanel1.Controls.Add(this.label48);
			this.groupPanel1.Controls.Add(this.FirmwareVersionTextBox);
			this.groupPanel1.Controls.Add(this.HardwareVersionTextBox);
			this.groupPanel1.Controls.Add(this.DeviceNameLabel);
			this.groupPanel1.Controls.Add(this.label5);
			this.groupPanel1.Controls.Add(this.label4);
			this.groupPanel1.HeaderBackColor = System.Drawing.Color.White;
			this.groupPanel1.HeaderHeight = 30;
			this.groupPanel1.Location = new System.Drawing.Point(11, 41);
			this.groupPanel1.Name = "groupPanel1";
			this.groupPanel1.Size = new System.Drawing.Size(352, 58);
			this.groupPanel1.TabIndex = 35;
			this.groupPanel1.TabStop = false;
			// 
			// BuildTextBox
			// 
			this.BuildTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BuildTextBox.Location = new System.Drawing.Point(165, 33);
			this.BuildTextBox.Name = "BuildTextBox";
			this.BuildTextBox.Size = new System.Drawing.Size(60, 21);
			this.BuildTextBox.TabIndex = 34;
			this.BuildTextBox.TabStop = false;
			this.BuildTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label48
			// 
			this.label48.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label48.AutoSize = true;
			this.label48.Location = new System.Drawing.Point(126, 36);
			this.label48.Name = "label48";
			this.label48.Size = new System.Drawing.Size(33, 13);
			this.label48.TabIndex = 33;
			this.label48.Text = "Build:";
			// 
			// FirmwareVersionTextBox
			// 
			this.FirmwareVersionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.FirmwareVersionTextBox.Location = new System.Drawing.Point(60, 33);
			this.FirmwareVersionTextBox.Name = "FirmwareVersionTextBox";
			this.FirmwareVersionTextBox.Size = new System.Drawing.Size(60, 21);
			this.FirmwareVersionTextBox.TabIndex = 27;
			this.FirmwareVersionTextBox.TabStop = false;
			this.FirmwareVersionTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// HardwareVersionTextBox
			// 
			this.HardwareVersionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.HardwareVersionTextBox.Location = new System.Drawing.Point(284, 33);
			this.HardwareVersionTextBox.Name = "HardwareVersionTextBox";
			this.HardwareVersionTextBox.Size = new System.Drawing.Size(60, 21);
			this.HardwareVersionTextBox.TabIndex = 28;
			this.HardwareVersionTextBox.TabStop = false;
			this.HardwareVersionTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// DeviceNameLabel
			// 
			this.DeviceNameLabel.AutoSize = true;
			this.DeviceNameLabel.BackColor = System.Drawing.Color.White;
			this.DeviceNameLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.DeviceNameLabel.Location = new System.Drawing.Point(6, 5);
			this.DeviceNameLabel.Name = "DeviceNameLabel";
			this.DeviceNameLabel.Size = new System.Drawing.Size(123, 19);
			this.DeviceNameLabel.TabIndex = 0;
			this.DeviceNameLabel.Text = "LostVape Triade";
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(231, 36);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(47, 13);
			this.label5.TabIndex = 30;
			this.label5.Text = "HW Ver:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(8, 36);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(46, 13);
			this.label4.TabIndex = 29;
			this.label4.Text = "FW Ver:";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.pictureBox1.BackgroundImage = global::NToolbox.Properties.Resources.arctic_fox_logo;
			this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.pictureBox1.Location = new System.Drawing.Point(117, 60);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(128, 128);
			this.pictureBox1.TabIndex = 3;
			this.pictureBox1.TabStop = false;
			// 
			// ArcticFoxConfigurationWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(374, 516);
			this.Controls.Add(this.MainContainer);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ArcticFoxConfigurationWindow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Arctic Fox Configuration";
			this.MainContainer.ResumeLayout(false);
			this.WelcomePage.ResumeLayout(false);
			this.WorkspacePage.ResumeLayout(false);
			this.MainTabControl.ResumeLayout(false);
			this.ProfilesTabPage.ResumeLayout(false);
			this.ProfilesTabControl.ResumeLayout(false);
			this.groupPanel1.ResumeLayout(false);
			this.groupPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private NCore.UI.MultiPanel MainContainer;
		private NCore.UI.MultiPanelPage WelcomePage;
		private NCore.UI.MultiPanelPage WorkspacePage;
		private System.Windows.Forms.Label WelcomeLabel;
		private NCore.UI.GroupPanel groupPanel1;
		private System.Windows.Forms.TextBox BuildTextBox;
		private System.Windows.Forms.Label label48;
		private System.Windows.Forms.TextBox FirmwareVersionTextBox;
		private System.Windows.Forms.TextBox HardwareVersionTextBox;
		private System.Windows.Forms.Label DeviceNameLabel;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TabControl MainTabControl;
		private System.Windows.Forms.TabPage ProfilesTabPage;
		private System.Windows.Forms.TabPage GeneralTabPage;
		private System.Windows.Forms.TabControl ProfilesTabControl;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.PictureBox pictureBox1;
	}
}