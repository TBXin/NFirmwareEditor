namespace NFirmwareEditor
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
			this.SourceEncryptedTextBox = new System.Windows.Forms.TextBox();
			this.DestinationTextBox = new System.Windows.Forms.TextBox();
			this.SelectEncryptedSourceButton = new System.Windows.Forms.Button();
			this.SelectDestinationButton = new System.Windows.Forms.Button();
			this.EncryptDecryptButton = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.SelectDecryptedSourceButton = new System.Windows.Forms.Button();
			this.SourceDecryptedTextBox = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// SourceEncryptedTextBox
			// 
			this.SourceEncryptedTextBox.Location = new System.Drawing.Point(124, 9);
			this.SourceEncryptedTextBox.Name = "SourceEncryptedTextBox";
			this.SourceEncryptedTextBox.ReadOnly = true;
			this.SourceEncryptedTextBox.Size = new System.Drawing.Size(309, 21);
			this.SourceEncryptedTextBox.TabIndex = 0;
			// 
			// DestinationTextBox
			// 
			this.DestinationTextBox.Location = new System.Drawing.Point(124, 63);
			this.DestinationTextBox.Name = "DestinationTextBox";
			this.DestinationTextBox.ReadOnly = true;
			this.DestinationTextBox.Size = new System.Drawing.Size(309, 21);
			this.DestinationTextBox.TabIndex = 1;
			// 
			// SelectEncryptedSourceButton
			// 
			this.SelectEncryptedSourceButton.Location = new System.Drawing.Point(439, 9);
			this.SelectEncryptedSourceButton.Name = "SelectEncryptedSourceButton";
			this.SelectEncryptedSourceButton.Size = new System.Drawing.Size(75, 21);
			this.SelectEncryptedSourceButton.TabIndex = 2;
			this.SelectEncryptedSourceButton.Text = "Select";
			this.SelectEncryptedSourceButton.UseVisualStyleBackColor = true;
			this.SelectEncryptedSourceButton.Click += new System.EventHandler(this.SelectSourceButton_Click);
			// 
			// SelectDestinationButton
			// 
			this.SelectDestinationButton.Location = new System.Drawing.Point(439, 63);
			this.SelectDestinationButton.Name = "SelectDestinationButton";
			this.SelectDestinationButton.Size = new System.Drawing.Size(75, 21);
			this.SelectDestinationButton.TabIndex = 3;
			this.SelectDestinationButton.Text = "Select";
			this.SelectDestinationButton.UseVisualStyleBackColor = true;
			this.SelectDestinationButton.Click += new System.EventHandler(this.SelectDestinationButton_Click);
			// 
			// EncryptDecryptButton
			// 
			this.EncryptDecryptButton.Location = new System.Drawing.Point(12, 90);
			this.EncryptDecryptButton.Name = "EncryptDecryptButton";
			this.EncryptDecryptButton.Size = new System.Drawing.Size(502, 25);
			this.EncryptDecryptButton.TabIndex = 4;
			this.EncryptDecryptButton.Text = "Encrypt / Decrypt";
			this.EncryptDecryptButton.UseVisualStyleBackColor = true;
			this.EncryptDecryptButton.Click += new System.EventHandler(this.EncryptDecryptButton_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(104, 13);
			this.label1.TabIndex = 5;
			this.label1.Text = "Source (encyrpted):";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 66);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(65, 13);
			this.label2.TabIndex = 6;
			this.label2.Text = "Destination:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 39);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(104, 13);
			this.label3.TabIndex = 9;
			this.label3.Text = "Source (decrypted):";
			// 
			// SelectDecryptedSourceButton
			// 
			this.SelectDecryptedSourceButton.Location = new System.Drawing.Point(439, 36);
			this.SelectDecryptedSourceButton.Name = "SelectDecryptedSourceButton";
			this.SelectDecryptedSourceButton.Size = new System.Drawing.Size(75, 21);
			this.SelectDecryptedSourceButton.TabIndex = 8;
			this.SelectDecryptedSourceButton.Text = "Select";
			this.SelectDecryptedSourceButton.UseVisualStyleBackColor = true;
			this.SelectDecryptedSourceButton.Click += new System.EventHandler(this.SelectDecryptedSourceButton_Click);
			// 
			// SourceDecryptedTextBox
			// 
			this.SourceDecryptedTextBox.Location = new System.Drawing.Point(124, 36);
			this.SourceDecryptedTextBox.Name = "SourceDecryptedTextBox";
			this.SourceDecryptedTextBox.ReadOnly = true;
			this.SourceDecryptedTextBox.Size = new System.Drawing.Size(309, 21);
			this.SourceDecryptedTextBox.TabIndex = 7;
			// 
			// DecryptionWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(527, 124);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.SelectDecryptedSourceButton);
			this.Controls.Add(this.SourceDecryptedTextBox);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.EncryptDecryptButton);
			this.Controls.Add(this.SelectDestinationButton);
			this.Controls.Add(this.SelectEncryptedSourceButton);
			this.Controls.Add(this.DestinationTextBox);
			this.Controls.Add(this.SourceEncryptedTextBox);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DecryptionWindow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Encryption / Decryption";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox SourceEncryptedTextBox;
		private System.Windows.Forms.TextBox DestinationTextBox;
		private System.Windows.Forms.Button SelectEncryptedSourceButton;
		private System.Windows.Forms.Button SelectDestinationButton;
		private System.Windows.Forms.Button EncryptDecryptButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button SelectDecryptedSourceButton;
		private System.Windows.Forms.TextBox SourceDecryptedTextBox;
	}
}