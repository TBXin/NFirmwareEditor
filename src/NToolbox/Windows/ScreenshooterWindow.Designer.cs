namespace NToolbox.Windows
{
	partial class ScreenshooterWindow
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
			this.ScreenBordersPanel = new System.Windows.Forms.Panel();
			this.ScreenPictureBox = new System.Windows.Forms.PictureBox();
			this.label46 = new System.Windows.Forms.Label();
			this.label47 = new System.Windows.Forms.Label();
			this.PixelSizeUpDown = new System.Windows.Forms.NumericUpDown();
			this.TakeScreenshotBeforeSaveCheckBox = new System.Windows.Forms.CheckBox();
			this.ControlBorderedPanel = new NCore.UI.BorderedPanel();
			this.CancelButton = new System.Windows.Forms.Button();
			this.cardPanel1 = new NCore.UI.CardPanel();
			this.TabMultiPanel = new NCore.UI.MultiPanel();
			this.ControlPage = new NCore.UI.MultiPanelPage();
			this.BroadcastButton = new System.Windows.Forms.LinkLabel();
			this.SaveScreenshotButton = new System.Windows.Forms.LinkLabel();
			this.TakeScreenshotButton = new System.Windows.Forms.LinkLabel();
			this.OptionsPage = new NCore.UI.MultiPanelPage();
			this.ControlLinkLabel = new System.Windows.Forms.LinkLabel();
			this.OptionsLinkLabel = new System.Windows.Forms.LinkLabel();
			this.ActiveTabPanel = new System.Windows.Forms.Panel();
			this.InactiveTabPanel = new System.Windows.Forms.Panel();
			this.ScreenBordersPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ScreenPictureBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PixelSizeUpDown)).BeginInit();
			this.ControlBorderedPanel.SuspendLayout();
			this.cardPanel1.SuspendLayout();
			this.TabMultiPanel.SuspendLayout();
			this.ControlPage.SuspendLayout();
			this.OptionsPage.SuspendLayout();
			this.SuspendLayout();
			// 
			// ScreenBordersPanel
			// 
			this.ScreenBordersPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ScreenBordersPanel.BackColor = System.Drawing.Color.Black;
			this.ScreenBordersPanel.Controls.Add(this.ScreenPictureBox);
			this.MainLocalizationExtender.SetKey(this.ScreenBordersPanel, "");
			this.ScreenBordersPanel.Location = new System.Drawing.Point(21, 11);
			this.ScreenBordersPanel.Name = "ScreenBordersPanel";
			this.ScreenBordersPanel.Size = new System.Drawing.Size(260, 132);
			this.ScreenBordersPanel.TabIndex = 1;
			// 
			// ScreenPictureBox
			// 
			this.ScreenPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.ScreenPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MainLocalizationExtender.SetKey(this.ScreenPictureBox, "");
			this.ScreenPictureBox.Location = new System.Drawing.Point(0, 0);
			this.ScreenPictureBox.Name = "ScreenPictureBox";
			this.ScreenPictureBox.Size = new System.Drawing.Size(260, 132);
			this.ScreenPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.ScreenPictureBox.TabIndex = 0;
			this.ScreenPictureBox.TabStop = false;
			// 
			// label46
			// 
			this.label46.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.label46.AutoSize = true;
			this.label46.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.label46.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(164)))), ((int)(((byte)(164)))));
			this.MainLocalizationExtender.SetKey(this.label46, "Toolbox.Screenshooter.PixelSizeLabel");
			this.label46.Location = new System.Drawing.Point(12, 8);
			this.label46.Name = "label46";
			this.label46.Size = new System.Drawing.Size(119, 13);
			this.label46.TabIndex = 69;
			this.label46.Text = "Pixel size multiplier:";
			// 
			// label47
			// 
			this.label47.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.label47.AutoSize = true;
			this.label47.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.label47.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(164)))), ((int)(((byte)(164)))));
			this.MainLocalizationExtender.SetKey(this.label47, "Toolbox.Screenshooter.TakeBeforeSaveLabel");
			this.label47.Location = new System.Drawing.Point(12, 34);
			this.label47.Name = "label47";
			this.label47.Size = new System.Drawing.Size(108, 13);
			this.label47.TabIndex = 70;
			this.label47.Text = "Take before save:";
			// 
			// PixelSizeUpDown
			// 
			this.PixelSizeUpDown.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.MainLocalizationExtender.SetKey(this.PixelSizeUpDown, "");
			this.PixelSizeUpDown.Location = new System.Drawing.Point(205, 6);
			this.PixelSizeUpDown.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
			this.PixelSizeUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.PixelSizeUpDown.Name = "PixelSizeUpDown";
			this.PixelSizeUpDown.Size = new System.Drawing.Size(60, 21);
			this.PixelSizeUpDown.TabIndex = 68;
			this.PixelSizeUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// TakeScreenshotBeforeSaveCheckBox
			// 
			this.TakeScreenshotBeforeSaveCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.TakeScreenshotBeforeSaveCheckBox.AutoSize = true;
			this.MainLocalizationExtender.SetKey(this.TakeScreenshotBeforeSaveCheckBox, "Toolbox.Screenshooter.TakeBeforeSaveCheckbox");
			this.TakeScreenshotBeforeSaveCheckBox.Location = new System.Drawing.Point(205, 34);
			this.TakeScreenshotBeforeSaveCheckBox.Name = "TakeScreenshotBeforeSaveCheckBox";
			this.TakeScreenshotBeforeSaveCheckBox.Size = new System.Drawing.Size(64, 17);
			this.TakeScreenshotBeforeSaveCheckBox.TabIndex = 71;
			this.TakeScreenshotBeforeSaveCheckBox.Text = "Enabled";
			this.TakeScreenshotBeforeSaveCheckBox.UseVisualStyleBackColor = true;
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
			this.ControlBorderedPanel.Location = new System.Drawing.Point(0, 258);
			this.ControlBorderedPanel.Name = "ControlBorderedPanel";
			this.ControlBorderedPanel.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
			this.ControlBorderedPanel.Size = new System.Drawing.Size(304, 44);
			this.ControlBorderedPanel.TabIndex = 2;
			this.ControlBorderedPanel.Text = "borderedPanel1";
			// 
			// CancelButton
			// 
			this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.MainLocalizationExtender.SetKey(this.CancelButton, "Toolbox.Screenshooter.CancelButton");
			this.CancelButton.Location = new System.Drawing.Point(200, 5);
			this.CancelButton.Name = "CancelButton";
			this.CancelButton.Size = new System.Drawing.Size(100, 35);
			this.CancelButton.TabIndex = 2;
			this.CancelButton.Text = "Cancel";
			this.CancelButton.UseVisualStyleBackColor = true;
			// 
			// cardPanel1
			// 
			this.cardPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cardPanel1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
			this.cardPanel1.Controls.Add(this.TabMultiPanel);
			this.cardPanel1.Controls.Add(this.ControlLinkLabel);
			this.cardPanel1.Controls.Add(this.OptionsLinkLabel);
			this.cardPanel1.Controls.Add(this.ScreenBordersPanel);
			this.cardPanel1.Controls.Add(this.ActiveTabPanel);
			this.cardPanel1.Controls.Add(this.InactiveTabPanel);
			this.MainLocalizationExtender.SetKey(this.cardPanel1, "");
			this.cardPanel1.Location = new System.Drawing.Point(1, 6);
			this.cardPanel1.Name = "cardPanel1";
			this.cardPanel1.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.cardPanel1.ShadowDensity = ((byte)(100));
			this.cardPanel1.ShadowLength = 10;
			this.cardPanel1.Size = new System.Drawing.Size(302, 250);
			this.cardPanel1.TabIndex = 3;
			// 
			// TabMultiPanel
			// 
			this.TabMultiPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TabMultiPanel.Controls.Add(this.ControlPage);
			this.TabMultiPanel.Controls.Add(this.OptionsPage);
			this.MainLocalizationExtender.SetKey(this.TabMultiPanel, "");
			this.TabMultiPanel.Location = new System.Drawing.Point(11, 182);
			this.TabMultiPanel.Name = "TabMultiPanel";
			this.TabMultiPanel.SelectedPage = this.OptionsPage;
			this.TabMultiPanel.Size = new System.Drawing.Size(280, 54);
			this.TabMultiPanel.TabIndex = 53;
			// 
			// ControlPage
			// 
			this.ControlPage.Controls.Add(this.BroadcastButton);
			this.ControlPage.Controls.Add(this.SaveScreenshotButton);
			this.ControlPage.Controls.Add(this.TakeScreenshotButton);
			this.ControlPage.Description = null;
			this.ControlPage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MainLocalizationExtender.SetKey(this.ControlPage, "");
			this.ControlPage.Location = new System.Drawing.Point(0, 0);
			this.ControlPage.Name = "ControlPage";
			this.ControlPage.Size = new System.Drawing.Size(280, 54);
			this.ControlPage.TabIndex = 0;
			this.ControlPage.Text = "ControlPage";
			// 
			// BroadcastButton
			// 
			this.BroadcastButton.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(146)))), ((int)(((byte)(221)))));
			this.BroadcastButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.BroadcastButton.DisabledLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(167)))), ((int)(((byte)(169)))));
			this.BroadcastButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.BroadcastButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(167)))), ((int)(((byte)(169)))));
			this.MainLocalizationExtender.SetKey(this.BroadcastButton, "Toolbox.Screenshooter.StartBroadcastButton");
			this.BroadcastButton.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
			this.BroadcastButton.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(146)))), ((int)(((byte)(221)))));
			this.BroadcastButton.Location = new System.Drawing.Point(0, 32);
			this.BroadcastButton.Name = "BroadcastButton";
			this.BroadcastButton.Size = new System.Drawing.Size(280, 16);
			this.BroadcastButton.TabIndex = 47;
			this.BroadcastButton.TabStop = true;
			this.BroadcastButton.Text = "Start broadcast";
			this.BroadcastButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.BroadcastButton.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(146)))), ((int)(((byte)(221)))));
			// 
			// SaveScreenshotButton
			// 
			this.SaveScreenshotButton.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(146)))), ((int)(((byte)(221)))));
			this.SaveScreenshotButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.SaveScreenshotButton.DisabledLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(167)))), ((int)(((byte)(169)))));
			this.SaveScreenshotButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.SaveScreenshotButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(167)))), ((int)(((byte)(169)))));
			this.MainLocalizationExtender.SetKey(this.SaveScreenshotButton, "Toolbox.Screenshooter.SaveScreenshotButton");
			this.SaveScreenshotButton.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
			this.SaveScreenshotButton.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(146)))), ((int)(((byte)(221)))));
			this.SaveScreenshotButton.Location = new System.Drawing.Point(140, 6);
			this.SaveScreenshotButton.Name = "SaveScreenshotButton";
			this.SaveScreenshotButton.Size = new System.Drawing.Size(140, 16);
			this.SaveScreenshotButton.TabIndex = 46;
			this.SaveScreenshotButton.TabStop = true;
			this.SaveScreenshotButton.Text = "Save screenshot";
			this.SaveScreenshotButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.SaveScreenshotButton.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(146)))), ((int)(((byte)(221)))));
			// 
			// TakeScreenshotButton
			// 
			this.TakeScreenshotButton.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(146)))), ((int)(((byte)(221)))));
			this.TakeScreenshotButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.TakeScreenshotButton.DisabledLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(167)))), ((int)(((byte)(169)))));
			this.TakeScreenshotButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.TakeScreenshotButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(167)))), ((int)(((byte)(169)))));
			this.MainLocalizationExtender.SetKey(this.TakeScreenshotButton, "Toolbox.Screenshooter.TakeScreenshotButton");
			this.TakeScreenshotButton.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
			this.TakeScreenshotButton.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(146)))), ((int)(((byte)(221)))));
			this.TakeScreenshotButton.Location = new System.Drawing.Point(0, 6);
			this.TakeScreenshotButton.Name = "TakeScreenshotButton";
			this.TakeScreenshotButton.Size = new System.Drawing.Size(140, 16);
			this.TakeScreenshotButton.TabIndex = 45;
			this.TakeScreenshotButton.TabStop = true;
			this.TakeScreenshotButton.Text = "Take screenshot";
			this.TakeScreenshotButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.TakeScreenshotButton.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(146)))), ((int)(((byte)(221)))));
			// 
			// OptionsPage
			// 
			this.OptionsPage.Controls.Add(this.label47);
			this.OptionsPage.Controls.Add(this.TakeScreenshotBeforeSaveCheckBox);
			this.OptionsPage.Controls.Add(this.label46);
			this.OptionsPage.Controls.Add(this.PixelSizeUpDown);
			this.OptionsPage.Description = null;
			this.OptionsPage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MainLocalizationExtender.SetKey(this.OptionsPage, "");
			this.OptionsPage.Location = new System.Drawing.Point(0, 0);
			this.OptionsPage.Name = "OptionsPage";
			this.OptionsPage.Size = new System.Drawing.Size(280, 54);
			this.OptionsPage.TabIndex = 1;
			this.OptionsPage.Text = "OptionsPage";
			// 
			// ControlLinkLabel
			// 
			this.ControlLinkLabel.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(164)))), ((int)(((byte)(164)))));
			this.ControlLinkLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.ControlLinkLabel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.MainLocalizationExtender.SetKey(this.ControlLinkLabel, "Toolbox.Screenshooter.ControlTab");
			this.ControlLinkLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
			this.ControlLinkLabel.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(146)))), ((int)(((byte)(221)))));
			this.ControlLinkLabel.Location = new System.Drawing.Point(11, 158);
			this.ControlLinkLabel.Name = "ControlLinkLabel";
			this.ControlLinkLabel.Size = new System.Drawing.Size(140, 13);
			this.ControlLinkLabel.TabIndex = 51;
			this.ControlLinkLabel.TabStop = true;
			this.ControlLinkLabel.Text = "Control";
			this.ControlLinkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.ControlLinkLabel.VisitedLinkColor = System.Drawing.Color.White;
			// 
			// OptionsLinkLabel
			// 
			this.OptionsLinkLabel.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(164)))), ((int)(((byte)(164)))));
			this.OptionsLinkLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.OptionsLinkLabel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.MainLocalizationExtender.SetKey(this.OptionsLinkLabel, "Toolbox.Screenshooter.OptionsTab");
			this.OptionsLinkLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
			this.OptionsLinkLabel.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
			this.OptionsLinkLabel.Location = new System.Drawing.Point(151, 158);
			this.OptionsLinkLabel.Name = "OptionsLinkLabel";
			this.OptionsLinkLabel.Size = new System.Drawing.Size(140, 13);
			this.OptionsLinkLabel.TabIndex = 52;
			this.OptionsLinkLabel.TabStop = true;
			this.OptionsLinkLabel.Text = "Options";
			this.OptionsLinkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.OptionsLinkLabel.VisitedLinkColor = System.Drawing.Color.White;
			// 
			// ActiveTabPanel
			// 
			this.ActiveTabPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.ActiveTabPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(146)))), ((int)(((byte)(221)))));
			this.MainLocalizationExtender.SetKey(this.ActiveTabPanel, "");
			this.ActiveTabPanel.Location = new System.Drawing.Point(11, 177);
			this.ActiveTabPanel.Name = "ActiveTabPanel";
			this.ActiveTabPanel.Size = new System.Drawing.Size(140, 3);
			this.ActiveTabPanel.TabIndex = 49;
			// 
			// InactiveTabPanel
			// 
			this.InactiveTabPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.InactiveTabPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
			this.MainLocalizationExtender.SetKey(this.InactiveTabPanel, "");
			this.InactiveTabPanel.Location = new System.Drawing.Point(11, 177);
			this.InactiveTabPanel.Name = "InactiveTabPanel";
			this.InactiveTabPanel.Size = new System.Drawing.Size(280, 3);
			this.InactiveTabPanel.TabIndex = 50;
			// 
			// ScreenshooterWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(304, 302);
			this.Controls.Add(this.cardPanel1);
			this.Controls.Add(this.ControlBorderedPanel);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MainLocalizationExtender.SetKey(this, "");
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ScreenshooterWindow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "NToolbox - Screenshooter";
			this.ScreenBordersPanel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.ScreenPictureBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.PixelSizeUpDown)).EndInit();
			this.ControlBorderedPanel.ResumeLayout(false);
			this.cardPanel1.ResumeLayout(false);
			this.TabMultiPanel.ResumeLayout(false);
			this.ControlPage.ResumeLayout(false);
			this.OptionsPage.ResumeLayout(false);
			this.OptionsPage.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.PictureBox ScreenPictureBox;
		private System.Windows.Forms.Panel ScreenBordersPanel;
		private System.Windows.Forms.Label label46;
		private System.Windows.Forms.Label label47;
		private System.Windows.Forms.NumericUpDown PixelSizeUpDown;
		private System.Windows.Forms.CheckBox TakeScreenshotBeforeSaveCheckBox;
		private NCore.UI.BorderedPanel ControlBorderedPanel;
		private System.Windows.Forms.Button CancelButton;
		private NCore.UI.CardPanel cardPanel1;
		private System.Windows.Forms.LinkLabel ControlLinkLabel;
		private System.Windows.Forms.LinkLabel OptionsLinkLabel;
		private System.Windows.Forms.Panel ActiveTabPanel;
		private System.Windows.Forms.Panel InactiveTabPanel;
		private NCore.UI.MultiPanel TabMultiPanel;
		private NCore.UI.MultiPanelPage ControlPage;
		private NCore.UI.MultiPanelPage OptionsPage;
		private System.Windows.Forms.LinkLabel BroadcastButton;
		private System.Windows.Forms.LinkLabel SaveScreenshotButton;
		private System.Windows.Forms.LinkLabel TakeScreenshotButton;
	}
}