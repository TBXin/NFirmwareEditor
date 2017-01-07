namespace NLauncher
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
			this.EditorButton = new NCore.UI.ExtendedButton();
			this.FirmwareUpdaterButton = new NCore.UI.ExtendedButton();
			this.SuspendLayout();
			// 
			// EditorButton
			// 
			this.EditorButton.AdditionalText = "";
			this.EditorButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.EditorButton.Image = global::NLauncher.Properties.Resources.configuration;
			this.EditorButton.Location = new System.Drawing.Point(12, 12);
			this.EditorButton.Name = "EditorButton";
			this.EditorButton.Size = new System.Drawing.Size(220, 52);
			this.EditorButton.TabIndex = 4;
			this.EditorButton.Text = "NFirmwareEditor";
			// 
			// FirmwareUpdaterButton
			// 
			this.FirmwareUpdaterButton.AdditionalText = "";
			this.FirmwareUpdaterButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.FirmwareUpdaterButton.Image = global::NLauncher.Properties.Resources.firmware_updater;
			this.FirmwareUpdaterButton.Location = new System.Drawing.Point(12, 70);
			this.FirmwareUpdaterButton.Name = "FirmwareUpdaterButton";
			this.FirmwareUpdaterButton.Size = new System.Drawing.Size(220, 52);
			this.FirmwareUpdaterButton.TabIndex = 5;
			this.FirmwareUpdaterButton.Text = "Firmware Updater";
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(244, 134);
			this.Controls.Add(this.EditorButton);
			this.Controls.Add(this.FirmwareUpdaterButton);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainWindow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "NFE Launcher";
			this.ResumeLayout(false);

		}

		#endregion

		private NCore.UI.ExtendedButton EditorButton;
		private NCore.UI.ExtendedButton FirmwareUpdaterButton;
	}
}

