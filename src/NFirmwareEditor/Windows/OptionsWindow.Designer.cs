namespace NFirmwareEditor.Windows
{
	partial class OptionsWindow
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
			this.OkButton = new System.Windows.Forms.Button();
			this.CancelButton = new System.Windows.Forms.Button();
			this.SettingsTabControl = new System.Windows.Forms.TabControl();
			this.GeneralTabPage = new System.Windows.Forms.TabPage();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.ImageEditorModeComboBox = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.ControlBorderedPanel.SuspendLayout();
			this.SettingsTabControl.SuspendLayout();
			this.GeneralTabPage.SuspendLayout();
			this.groupBox1.SuspendLayout();
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
			this.ControlBorderedPanel.Controls.Add(this.OkButton);
			this.ControlBorderedPanel.Controls.Add(this.CancelButton);
			this.ControlBorderedPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.ControlBorderedPanel.Location = new System.Drawing.Point(0, 82);
			this.ControlBorderedPanel.Name = "ControlBorderedPanel";
			this.ControlBorderedPanel.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
			this.ControlBorderedPanel.Size = new System.Drawing.Size(384, 39);
			this.ControlBorderedPanel.TabIndex = 1;
			this.ControlBorderedPanel.Text = "borderedPanel1";
			// 
			// OkButton
			// 
			this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OkButton.Location = new System.Drawing.Point(174, 5);
			this.OkButton.Name = "OkButton";
			this.OkButton.Size = new System.Drawing.Size(100, 30);
			this.OkButton.TabIndex = 0;
			this.OkButton.Text = "Save";
			this.OkButton.UseVisualStyleBackColor = true;
			// 
			// CancelButton
			// 
			this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelButton.Location = new System.Drawing.Point(280, 5);
			this.CancelButton.Name = "CancelButton";
			this.CancelButton.Size = new System.Drawing.Size(100, 30);
			this.CancelButton.TabIndex = 1;
			this.CancelButton.Text = "Cancel";
			this.CancelButton.UseVisualStyleBackColor = true;
			// 
			// SettingsTabControl
			// 
			this.SettingsTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.SettingsTabControl.Controls.Add(this.GeneralTabPage);
			this.SettingsTabControl.Location = new System.Drawing.Point(3, 3);
			this.SettingsTabControl.Name = "SettingsTabControl";
			this.SettingsTabControl.SelectedIndex = 0;
			this.SettingsTabControl.Size = new System.Drawing.Size(380, 76);
			this.SettingsTabControl.TabIndex = 2;
			// 
			// GeneralTabPage
			// 
			this.GeneralTabPage.Controls.Add(this.groupBox1);
			this.GeneralTabPage.Location = new System.Drawing.Point(4, 22);
			this.GeneralTabPage.Name = "GeneralTabPage";
			this.GeneralTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.GeneralTabPage.Size = new System.Drawing.Size(372, 50);
			this.GeneralTabPage.TabIndex = 0;
			this.GeneralTabPage.Text = "Application";
			this.GeneralTabPage.UseVisualStyleBackColor = true;
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.ImageEditorModeComboBox);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Location = new System.Drawing.Point(3, 1);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(365, 47);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Image Editor:";
			// 
			// ImageEditorModeComboBox
			// 
			this.ImageEditorModeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ImageEditorModeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ImageEditorModeComboBox.FormattingEnabled = true;
			this.ImageEditorModeComboBox.Location = new System.Drawing.Point(106, 20);
			this.ImageEditorModeComboBox.Name = "ImageEditorModeComboBox";
			this.ImageEditorModeComboBox.Size = new System.Drawing.Size(254, 21);
			this.ImageEditorModeComboBox.TabIndex = 21;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(9, 23);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(37, 13);
			this.label6.TabIndex = 22;
			this.label6.Text = "Mode:";
			// 
			// OptionsWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(384, 121);
			this.Controls.Add(this.SettingsTabControl);
			this.Controls.Add(this.ControlBorderedPanel);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "OptionsWindow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "NFirmwareEditor - Options";
			this.ControlBorderedPanel.ResumeLayout(false);
			this.SettingsTabControl.ResumeLayout(false);
			this.GeneralTabPage.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private UI.BorderedPanel ControlBorderedPanel;
		private System.Windows.Forms.Button OkButton;
		private System.Windows.Forms.Button CancelButton;
		private System.Windows.Forms.TabControl SettingsTabControl;
		private System.Windows.Forms.TabPage GeneralTabPage;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ComboBox ImageEditorModeComboBox;
		private System.Windows.Forms.Label label6;
	}
}