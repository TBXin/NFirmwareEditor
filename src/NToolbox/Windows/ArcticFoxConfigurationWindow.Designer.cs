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
			this.WorkspacePage = new NCore.UI.MultiPanelPage();
			this.WelcomeLabel = new System.Windows.Forms.Label();
			this.MainContainer.SuspendLayout();
			this.WelcomePage.SuspendLayout();
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
			this.MainContainer.Size = new System.Drawing.Size(374, 477);
			this.MainContainer.TabIndex = 0;
			// 
			// WelcomePage
			// 
			this.WelcomePage.Controls.Add(this.WelcomeLabel);
			this.WelcomePage.Description = null;
			this.WelcomePage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.WelcomePage.Location = new System.Drawing.Point(0, 0);
			this.WelcomePage.Name = "WelcomePage";
			this.WelcomePage.Size = new System.Drawing.Size(374, 477);
			this.WelcomePage.TabIndex = 0;
			this.WelcomePage.Text = "WelcomePage";
			// 
			// WorkspacePage
			// 
			this.WorkspacePage.Description = null;
			this.WorkspacePage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.WorkspacePage.Location = new System.Drawing.Point(0, 0);
			this.WorkspacePage.Name = "WorkspacePage";
			this.WorkspacePage.Size = new System.Drawing.Size(374, 477);
			this.WorkspacePage.TabIndex = 1;
			this.WorkspacePage.Text = "Workspace";
			// 
			// WelcomeLabel
			// 
			this.WelcomeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.WelcomeLabel.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.WelcomeLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.WelcomeLabel.Location = new System.Drawing.Point(0, 0);
			this.WelcomeLabel.Name = "WelcomeLabel";
			this.WelcomeLabel.Size = new System.Drawing.Size(374, 477);
			this.WelcomeLabel.TabIndex = 2;
			this.WelcomeLabel.Text = "Waiting for device...";
			this.WelcomeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// ArcticFoxConfigurationWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(374, 477);
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
			this.ResumeLayout(false);

		}

		#endregion

		private NCore.UI.MultiPanel MainContainer;
		private NCore.UI.MultiPanelPage WelcomePage;
		private NCore.UI.MultiPanelPage WorkspacePage;
		private System.Windows.Forms.Label WelcomeLabel;
	}
}