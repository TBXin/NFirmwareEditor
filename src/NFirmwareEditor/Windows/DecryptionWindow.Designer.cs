namespace NFirmwareEditor.Windows
{
	partial class DecryptionWindow
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
			this.EncryptDecryptButton = new System.Windows.Forms.Button();
			this.CancelButton = new System.Windows.Forms.Button();
			this.SourceGroupBox = new NFirmwareEditor.UI.GroupPanel();
			this.label1 = new System.Windows.Forms.Label();
			this.SelectSourceButton = new System.Windows.Forms.Button();
			this.SourceTextBox = new System.Windows.Forms.TextBox();
			this.DestinationGroupBox = new NFirmwareEditor.UI.GroupPanel();
			this.label2 = new System.Windows.Forms.Label();
			this.SelectDestinationButton = new System.Windows.Forms.Button();
			this.DestinationTextBox = new System.Windows.Forms.TextBox();
			this.ControlBorderedPanel.SuspendLayout();
			this.SourceGroupBox.SuspendLayout();
			this.DestinationGroupBox.SuspendLayout();
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
			this.ControlBorderedPanel.Controls.Add(this.EncryptDecryptButton);
			this.ControlBorderedPanel.Controls.Add(this.CancelButton);
			this.ControlBorderedPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.ControlBorderedPanel.Location = new System.Drawing.Point(0, 128);
			this.ControlBorderedPanel.Name = "ControlBorderedPanel";
			this.ControlBorderedPanel.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
			this.ControlBorderedPanel.Size = new System.Drawing.Size(434, 39);
			this.ControlBorderedPanel.TabIndex = 10;
			this.ControlBorderedPanel.Text = "borderedPanel1";
			// 
			// EncryptDecryptButton
			// 
			this.EncryptDecryptButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.EncryptDecryptButton.Enabled = false;
			this.EncryptDecryptButton.Location = new System.Drawing.Point(224, 5);
			this.EncryptDecryptButton.Name = "EncryptDecryptButton";
			this.EncryptDecryptButton.Size = new System.Drawing.Size(100, 30);
			this.EncryptDecryptButton.TabIndex = 7;
			this.EncryptDecryptButton.Text = "Encrypt / decrypt";
			this.EncryptDecryptButton.UseVisualStyleBackColor = true;
			// 
			// CancelButton
			// 
			this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelButton.Location = new System.Drawing.Point(330, 5);
			this.CancelButton.Name = "CancelButton";
			this.CancelButton.Size = new System.Drawing.Size(100, 30);
			this.CancelButton.TabIndex = 8;
			this.CancelButton.Text = "Cancel";
			this.CancelButton.UseVisualStyleBackColor = true;
			// 
			// SourceGroupBox
			// 
			this.SourceGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.SourceGroupBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(185)))), ((int)(((byte)(185)))));
			this.SourceGroupBox.Controls.Add(this.label1);
			this.SourceGroupBox.Controls.Add(this.SelectSourceButton);
			this.SourceGroupBox.Controls.Add(this.SourceTextBox);
			this.SourceGroupBox.HeaderBackColor = System.Drawing.Color.White;
			this.SourceGroupBox.HeaderHeight = 30;
			this.SourceGroupBox.Location = new System.Drawing.Point(3, 3);
			this.SourceGroupBox.Name = "SourceGroupBox";
			this.SourceGroupBox.Size = new System.Drawing.Size(428, 58);
			this.SourceGroupBox.TabIndex = 11;
			this.SourceGroupBox.TabStop = false;
			this.SourceGroupBox.Text = "Source:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(9, 36);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(72, 13);
			this.label1.TabIndex = 11;
			this.label1.Text = "Firmware file:";
			// 
			// SelectSourceButton
			// 
			this.SelectSourceButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.SelectSourceButton.Location = new System.Drawing.Point(348, 32);
			this.SelectSourceButton.Name = "SelectSourceButton";
			this.SelectSourceButton.Size = new System.Drawing.Size(75, 23);
			this.SelectSourceButton.TabIndex = 10;
			this.SelectSourceButton.Text = "Select";
			this.SelectSourceButton.UseVisualStyleBackColor = true;
			// 
			// SourceTextBox
			// 
			this.SourceTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.SourceTextBox.Location = new System.Drawing.Point(106, 33);
			this.SourceTextBox.Name = "SourceTextBox";
			this.SourceTextBox.ReadOnly = true;
			this.SourceTextBox.Size = new System.Drawing.Size(236, 21);
			this.SourceTextBox.TabIndex = 9;
			this.SourceTextBox.TabStop = false;
			// 
			// DestinationGroupBox
			// 
			this.DestinationGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.DestinationGroupBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(185)))), ((int)(((byte)(185)))));
			this.DestinationGroupBox.Controls.Add(this.label2);
			this.DestinationGroupBox.Controls.Add(this.SelectDestinationButton);
			this.DestinationGroupBox.Controls.Add(this.DestinationTextBox);
			this.DestinationGroupBox.HeaderBackColor = System.Drawing.Color.White;
			this.DestinationGroupBox.HeaderHeight = 30;
			this.DestinationGroupBox.Location = new System.Drawing.Point(3, 67);
			this.DestinationGroupBox.Name = "DestinationGroupBox";
			this.DestinationGroupBox.Size = new System.Drawing.Size(428, 58);
			this.DestinationGroupBox.TabIndex = 12;
			this.DestinationGroupBox.TabStop = false;
			this.DestinationGroupBox.Text = "Destination:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(9, 36);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(72, 13);
			this.label2.TabIndex = 11;
			this.label2.Text = "Firmware file:";
			// 
			// SelectDestinationButton
			// 
			this.SelectDestinationButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.SelectDestinationButton.Enabled = false;
			this.SelectDestinationButton.Location = new System.Drawing.Point(348, 32);
			this.SelectDestinationButton.Name = "SelectDestinationButton";
			this.SelectDestinationButton.Size = new System.Drawing.Size(75, 23);
			this.SelectDestinationButton.TabIndex = 10;
			this.SelectDestinationButton.Text = "Select";
			this.SelectDestinationButton.UseVisualStyleBackColor = true;
			// 
			// DestinationTextBox
			// 
			this.DestinationTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.DestinationTextBox.Location = new System.Drawing.Point(106, 33);
			this.DestinationTextBox.Name = "DestinationTextBox";
			this.DestinationTextBox.ReadOnly = true;
			this.DestinationTextBox.Size = new System.Drawing.Size(236, 21);
			this.DestinationTextBox.TabIndex = 9;
			this.DestinationTextBox.TabStop = false;
			// 
			// DecryptionWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(434, 167);
			this.Controls.Add(this.DestinationGroupBox);
			this.Controls.Add(this.SourceGroupBox);
			this.Controls.Add(this.ControlBorderedPanel);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DecryptionWindow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "NFirmwareEditor - Firmware Encryption / Decryption";
			this.ControlBorderedPanel.ResumeLayout(false);
			this.SourceGroupBox.ResumeLayout(false);
			this.SourceGroupBox.PerformLayout();
			this.DestinationGroupBox.ResumeLayout(false);
			this.DestinationGroupBox.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private UI.BorderedPanel ControlBorderedPanel;
		private System.Windows.Forms.Button EncryptDecryptButton;
		private System.Windows.Forms.Button CancelButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button SelectSourceButton;
		private System.Windows.Forms.TextBox SourceTextBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button SelectDestinationButton;
		private System.Windows.Forms.TextBox DestinationTextBox;
		private UI.GroupPanel SourceGroupBox;
		private UI.GroupPanel DestinationGroupBox;
	}
}