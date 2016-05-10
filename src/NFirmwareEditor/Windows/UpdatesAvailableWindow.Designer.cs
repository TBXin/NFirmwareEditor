namespace NFirmwareEditor.Windows
{
	partial class UpdatesAvailableWindow
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
			this.DownloadButton = new System.Windows.Forms.Button();
			this.CancelButton = new System.Windows.Forms.Button();
			this.AvailableLable = new System.Windows.Forms.Label();
			this.NewVersionLabel = new System.Windows.Forms.Label();
			this.ChangesTextBox = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.ViewAllReleasesLinkLabel = new System.Windows.Forms.LinkLabel();
			this.ViewHomePageLinkLabel = new System.Windows.Forms.LinkLabel();
			this.label1 = new System.Windows.Forms.Label();
			this.ControlBorderedPanel.SuspendLayout();
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
			this.ControlBorderedPanel.Controls.Add(this.DownloadButton);
			this.ControlBorderedPanel.Controls.Add(this.CancelButton);
			this.ControlBorderedPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.ControlBorderedPanel.Location = new System.Drawing.Point(0, 322);
			this.ControlBorderedPanel.Name = "ControlBorderedPanel";
			this.ControlBorderedPanel.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
			this.ControlBorderedPanel.Size = new System.Drawing.Size(414, 39);
			this.ControlBorderedPanel.TabIndex = 2;
			this.ControlBorderedPanel.Text = "borderedPanel1";
			// 
			// DownloadButton
			// 
			this.DownloadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.DownloadButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.DownloadButton.Location = new System.Drawing.Point(204, 5);
			this.DownloadButton.Name = "DownloadButton";
			this.DownloadButton.Size = new System.Drawing.Size(100, 30);
			this.DownloadButton.TabIndex = 0;
			this.DownloadButton.Text = "Download";
			this.DownloadButton.UseVisualStyleBackColor = true;
			// 
			// CancelButton
			// 
			this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelButton.Location = new System.Drawing.Point(310, 5);
			this.CancelButton.Name = "CancelButton";
			this.CancelButton.Size = new System.Drawing.Size(100, 30);
			this.CancelButton.TabIndex = 1;
			this.CancelButton.Text = "Cancel";
			this.CancelButton.UseVisualStyleBackColor = true;
			// 
			// AvailableLable
			// 
			this.AvailableLable.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.AvailableLable.Location = new System.Drawing.Point(12, 12);
			this.AvailableLable.Name = "AvailableLable";
			this.AvailableLable.Size = new System.Drawing.Size(249, 30);
			this.AvailableLable.TabIndex = 4;
			this.AvailableLable.Text = "New version available:";
			// 
			// NewVersionLabel
			// 
			this.NewVersionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.NewVersionLabel.BackColor = System.Drawing.Color.Transparent;
			this.NewVersionLabel.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.NewVersionLabel.Location = new System.Drawing.Point(205, 12);
			this.NewVersionLabel.Name = "NewVersionLabel";
			this.NewVersionLabel.Size = new System.Drawing.Size(197, 30);
			this.NewVersionLabel.TabIndex = 5;
			this.NewVersionLabel.Text = "3.41";
			// 
			// ChangesTextBox
			// 
			this.ChangesTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ChangesTextBox.Location = new System.Drawing.Point(14, 62);
			this.ChangesTextBox.Multiline = true;
			this.ChangesTextBox.Name = "ChangesTextBox";
			this.ChangesTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.ChangesTextBox.Size = new System.Drawing.Size(386, 246);
			this.ChangesTextBox.TabIndex = 6;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(14, 46);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(65, 13);
			this.label5.TabIndex = 12;
			this.label5.Text = "Change log:";
			// 
			// ViewAllReleasesLinkLabel
			// 
			this.ViewAllReleasesLinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ViewAllReleasesLinkLabel.AutoSize = true;
			this.ViewAllReleasesLinkLabel.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(129)))), ((int)(((byte)(230)))));
			this.ViewAllReleasesLinkLabel.Location = new System.Drawing.Point(317, 46);
			this.ViewAllReleasesLinkLabel.Name = "ViewAllReleasesLinkLabel";
			this.ViewAllReleasesLinkLabel.Size = new System.Drawing.Size(85, 13);
			this.ViewAllReleasesLinkLabel.TabIndex = 13;
			this.ViewAllReleasesLinkLabel.TabStop = true;
			this.ViewAllReleasesLinkLabel.Text = "View all releases";
			this.ViewAllReleasesLinkLabel.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(129)))), ((int)(((byte)(230)))));
			// 
			// ViewHomePageLinkLabel
			// 
			this.ViewHomePageLinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ViewHomePageLinkLabel.AutoSize = true;
			this.ViewHomePageLinkLabel.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(129)))), ((int)(((byte)(230)))));
			this.ViewHomePageLinkLabel.Location = new System.Drawing.Point(228, 46);
			this.ViewHomePageLinkLabel.Name = "ViewHomePageLinkLabel";
			this.ViewHomePageLinkLabel.Size = new System.Drawing.Size(85, 13);
			this.ViewHomePageLinkLabel.TabIndex = 14;
			this.ViewHomePageLinkLabel.TabStop = true;
			this.ViewHomePageLinkLabel.Text = "View home page";
			this.ViewHomePageLinkLabel.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(129)))), ((int)(((byte)(230)))));
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(309, 46);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(11, 13);
			this.label1.TabIndex = 15;
			this.label1.Text = "|";
			// 
			// UpdatesAvailableWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(414, 361);
			this.Controls.Add(this.ViewHomePageLinkLabel);
			this.Controls.Add(this.ViewAllReleasesLinkLabel);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.ChangesTextBox);
			this.Controls.Add(this.NewVersionLabel);
			this.Controls.Add(this.AvailableLable);
			this.Controls.Add(this.ControlBorderedPanel);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "UpdatesAvailableWindow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "NFirmwareEditor - Updates";
			this.ControlBorderedPanel.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private UI.BorderedPanel ControlBorderedPanel;
		private System.Windows.Forms.Button CancelButton;
		private System.Windows.Forms.Label AvailableLable;
		private System.Windows.Forms.Label NewVersionLabel;
		private System.Windows.Forms.Button DownloadButton;
		private System.Windows.Forms.TextBox ChangesTextBox;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.LinkLabel ViewAllReleasesLinkLabel;
		private System.Windows.Forms.LinkLabel ViewHomePageLinkLabel;
		private System.Windows.Forms.Label label1;
	}
}