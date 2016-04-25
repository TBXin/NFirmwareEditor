namespace NFirmwareEditor.Windows
{
	partial class ImageConvertorWindow
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
			this.label1 = new System.Windows.Forms.Label();
			this.SelectSourceButton = new System.Windows.Forms.Button();
			this.SourceTextBox = new System.Windows.Forms.TextBox();
			this.JoyetechSizeButton = new System.Windows.Forms.Button();
			this.NewHeightUpDown = new System.Windows.Forms.NumericUpDown();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.NewWidthUpDown = new System.Windows.Forms.NumericUpDown();
			this.borderedPanel3 = new NFirmwareEditor.UI.BorderedPanel();
			this.label2 = new System.Windows.Forms.Label();
			this.borderedPanel5 = new NFirmwareEditor.UI.BorderedPanel();
			this.ImagePreviewSurface = new NFirmwareEditor.UI.DrawingSurface();
			this.borderedPanel1 = new NFirmwareEditor.UI.BorderedPanel();
			this.label5 = new System.Windows.Forms.Label();
			this.ResizeContainerPanel = new NFirmwareEditor.UI.BorderedPanel();
			this.borderedPanel4 = new NFirmwareEditor.UI.BorderedPanel();
			this.label6 = new System.Windows.Forms.Label();
			this.borderedPanel6 = new NFirmwareEditor.UI.BorderedPanel();
			this.ControlBorderedPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.NewHeightUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.NewWidthUpDown)).BeginInit();
			this.borderedPanel3.SuspendLayout();
			this.borderedPanel5.SuspendLayout();
			this.borderedPanel1.SuspendLayout();
			this.ResizeContainerPanel.SuspendLayout();
			this.borderedPanel4.SuspendLayout();
			this.borderedPanel6.SuspendLayout();
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
			this.ControlBorderedPanel.Location = new System.Drawing.Point(0, 505);
			this.ControlBorderedPanel.Name = "ControlBorderedPanel";
			this.ControlBorderedPanel.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
			this.ControlBorderedPanel.Size = new System.Drawing.Size(434, 39);
			this.ControlBorderedPanel.TabIndex = 3;
			this.ControlBorderedPanel.Text = "borderedPanel1";
			// 
			// OkButton
			// 
			this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.OkButton.Enabled = false;
			this.OkButton.Location = new System.Drawing.Point(224, 5);
			this.OkButton.Name = "OkButton";
			this.OkButton.Size = new System.Drawing.Size(100, 30);
			this.OkButton.TabIndex = 7;
			this.OkButton.Text = "Save bitmap";
			this.OkButton.UseVisualStyleBackColor = true;
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
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(9, 7);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(60, 13);
			this.label1.TabIndex = 14;
			this.label1.Text = "Bitmap file:";
			// 
			// SelectSourceButton
			// 
			this.SelectSourceButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.SelectSourceButton.Location = new System.Drawing.Point(348, 4);
			this.SelectSourceButton.Name = "SelectSourceButton";
			this.SelectSourceButton.Size = new System.Drawing.Size(75, 21);
			this.SelectSourceButton.TabIndex = 13;
			this.SelectSourceButton.Text = "Select";
			this.SelectSourceButton.UseVisualStyleBackColor = true;
			// 
			// SourceTextBox
			// 
			this.SourceTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.SourceTextBox.Location = new System.Drawing.Point(106, 4);
			this.SourceTextBox.Name = "SourceTextBox";
			this.SourceTextBox.ReadOnly = true;
			this.SourceTextBox.Size = new System.Drawing.Size(236, 21);
			this.SourceTextBox.TabIndex = 12;
			this.SourceTextBox.TabStop = false;
			// 
			// JoyetechSizeButton
			// 
			this.JoyetechSizeButton.Location = new System.Drawing.Point(212, 3);
			this.JoyetechSizeButton.Name = "JoyetechSizeButton";
			this.JoyetechSizeButton.Size = new System.Drawing.Size(75, 23);
			this.JoyetechSizeButton.TabIndex = 12;
			this.JoyetechSizeButton.Text = "64x40";
			this.JoyetechSizeButton.UseVisualStyleBackColor = true;
			// 
			// NewHeightUpDown
			// 
			this.NewHeightUpDown.Location = new System.Drawing.Point(156, 4);
			this.NewHeightUpDown.Maximum = new decimal(new int[] {
            2048,
            0,
            0,
            0});
			this.NewHeightUpDown.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            0});
			this.NewHeightUpDown.Name = "NewHeightUpDown";
			this.NewHeightUpDown.Size = new System.Drawing.Size(50, 21);
			this.NewHeightUpDown.TabIndex = 9;
			this.NewHeightUpDown.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(9, 7);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(39, 13);
			this.label4.TabIndex = 10;
			this.label4.Text = "Width:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(110, 7);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(42, 13);
			this.label3.TabIndex = 11;
			this.label3.Text = "Height:";
			// 
			// NewWidthUpDown
			// 
			this.NewWidthUpDown.Location = new System.Drawing.Point(52, 4);
			this.NewWidthUpDown.Maximum = new decimal(new int[] {
            2048,
            0,
            0,
            0});
			this.NewWidthUpDown.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            0});
			this.NewWidthUpDown.Name = "NewWidthUpDown";
			this.NewWidthUpDown.Size = new System.Drawing.Size(50, 21);
			this.NewWidthUpDown.TabIndex = 8;
			this.NewWidthUpDown.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
			// 
			// borderedPanel3
			// 
			this.borderedPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.borderedPanel3.BackColor = System.Drawing.Color.White;
			this.borderedPanel3.BorderBottom = true;
			this.borderedPanel3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(185)))), ((int)(((byte)(185)))));
			this.borderedPanel3.BorderLeft = true;
			this.borderedPanel3.BorderRight = true;
			this.borderedPanel3.BorderTop = true;
			this.borderedPanel3.BorderWidth = 1F;
			this.borderedPanel3.Controls.Add(this.label2);
			this.borderedPanel3.Controls.Add(this.borderedPanel5);
			this.borderedPanel3.Location = new System.Drawing.Point(3, 133);
			this.borderedPanel3.Name = "borderedPanel3";
			this.borderedPanel3.Padding = new System.Windows.Forms.Padding(1);
			this.borderedPanel3.Size = new System.Drawing.Size(428, 369);
			this.borderedPanel3.TabIndex = 10;
			this.borderedPanel3.Text = "borderedPanel3";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(4, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(49, 13);
			this.label2.TabIndex = 5;
			this.label2.Text = "Preview:";
			// 
			// borderedPanel5
			// 
			this.borderedPanel5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.borderedPanel5.BackColor = System.Drawing.Color.Transparent;
			this.borderedPanel5.BorderBottom = false;
			this.borderedPanel5.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(185)))), ((int)(((byte)(185)))));
			this.borderedPanel5.BorderLeft = false;
			this.borderedPanel5.BorderRight = false;
			this.borderedPanel5.BorderTop = true;
			this.borderedPanel5.BorderWidth = 1F;
			this.borderedPanel5.Controls.Add(this.ImagePreviewSurface);
			this.borderedPanel5.Location = new System.Drawing.Point(1, 29);
			this.borderedPanel5.Name = "borderedPanel5";
			this.borderedPanel5.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
			this.borderedPanel5.Size = new System.Drawing.Size(426, 339);
			this.borderedPanel5.TabIndex = 4;
			this.borderedPanel5.Text = "borderedPanel5";
			// 
			// ImagePreviewSurface
			// 
			this.ImagePreviewSurface.BackgroundImage = global::NFirmwareEditor.Properties.Resources.transparent_bg;
			this.ImagePreviewSurface.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ImagePreviewSurface.Location = new System.Drawing.Point(0, 1);
			this.ImagePreviewSurface.Name = "ImagePreviewSurface";
			this.ImagePreviewSurface.Size = new System.Drawing.Size(426, 338);
			this.ImagePreviewSurface.TabIndex = 0;
			this.ImagePreviewSurface.Text = "drawingSurface1";
			// 
			// borderedPanel1
			// 
			this.borderedPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.borderedPanel1.BackColor = System.Drawing.Color.White;
			this.borderedPanel1.BorderBottom = true;
			this.borderedPanel1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(185)))), ((int)(((byte)(185)))));
			this.borderedPanel1.BorderLeft = true;
			this.borderedPanel1.BorderRight = true;
			this.borderedPanel1.BorderTop = true;
			this.borderedPanel1.BorderWidth = 1F;
			this.borderedPanel1.Controls.Add(this.label5);
			this.borderedPanel1.Controls.Add(this.ResizeContainerPanel);
			this.borderedPanel1.Location = new System.Drawing.Point(3, 68);
			this.borderedPanel1.Name = "borderedPanel1";
			this.borderedPanel1.Padding = new System.Windows.Forms.Padding(1);
			this.borderedPanel1.Size = new System.Drawing.Size(428, 59);
			this.borderedPanel1.TabIndex = 11;
			this.borderedPanel1.Text = "borderedPanel1";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(4, 8);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(42, 13);
			this.label5.TabIndex = 5;
			this.label5.Text = "Resize:";
			// 
			// ResizeContainerPanel
			// 
			this.ResizeContainerPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ResizeContainerPanel.BackColor = System.Drawing.SystemColors.Control;
			this.ResizeContainerPanel.BorderBottom = false;
			this.ResizeContainerPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(185)))), ((int)(((byte)(185)))));
			this.ResizeContainerPanel.BorderLeft = false;
			this.ResizeContainerPanel.BorderRight = false;
			this.ResizeContainerPanel.BorderTop = true;
			this.ResizeContainerPanel.BorderWidth = 1F;
			this.ResizeContainerPanel.Controls.Add(this.JoyetechSizeButton);
			this.ResizeContainerPanel.Controls.Add(this.label4);
			this.ResizeContainerPanel.Controls.Add(this.NewWidthUpDown);
			this.ResizeContainerPanel.Controls.Add(this.NewHeightUpDown);
			this.ResizeContainerPanel.Controls.Add(this.label3);
			this.ResizeContainerPanel.Enabled = false;
			this.ResizeContainerPanel.Location = new System.Drawing.Point(1, 29);
			this.ResizeContainerPanel.Name = "ResizeContainerPanel";
			this.ResizeContainerPanel.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
			this.ResizeContainerPanel.Size = new System.Drawing.Size(426, 29);
			this.ResizeContainerPanel.TabIndex = 4;
			this.ResizeContainerPanel.Text = "borderedPanel2";
			// 
			// borderedPanel4
			// 
			this.borderedPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.borderedPanel4.BackColor = System.Drawing.Color.White;
			this.borderedPanel4.BorderBottom = true;
			this.borderedPanel4.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(185)))), ((int)(((byte)(185)))));
			this.borderedPanel4.BorderLeft = true;
			this.borderedPanel4.BorderRight = true;
			this.borderedPanel4.BorderTop = true;
			this.borderedPanel4.BorderWidth = 1F;
			this.borderedPanel4.Controls.Add(this.label6);
			this.borderedPanel4.Controls.Add(this.borderedPanel6);
			this.borderedPanel4.Location = new System.Drawing.Point(3, 3);
			this.borderedPanel4.Name = "borderedPanel4";
			this.borderedPanel4.Padding = new System.Windows.Forms.Padding(1);
			this.borderedPanel4.Size = new System.Drawing.Size(428, 59);
			this.borderedPanel4.TabIndex = 12;
			this.borderedPanel4.Text = "borderedPanel4";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(4, 8);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(44, 13);
			this.label6.TabIndex = 5;
			this.label6.Text = "Source:";
			// 
			// borderedPanel6
			// 
			this.borderedPanel6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.borderedPanel6.BackColor = System.Drawing.SystemColors.Control;
			this.borderedPanel6.BorderBottom = false;
			this.borderedPanel6.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(185)))), ((int)(((byte)(185)))));
			this.borderedPanel6.BorderLeft = false;
			this.borderedPanel6.BorderRight = false;
			this.borderedPanel6.BorderTop = true;
			this.borderedPanel6.BorderWidth = 1F;
			this.borderedPanel6.Controls.Add(this.label1);
			this.borderedPanel6.Controls.Add(this.SelectSourceButton);
			this.borderedPanel6.Controls.Add(this.SourceTextBox);
			this.borderedPanel6.Location = new System.Drawing.Point(1, 29);
			this.borderedPanel6.Name = "borderedPanel6";
			this.borderedPanel6.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
			this.borderedPanel6.Size = new System.Drawing.Size(426, 29);
			this.borderedPanel6.TabIndex = 4;
			this.borderedPanel6.Text = "borderedPanel6";
			// 
			// ImageConvertorWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(434, 544);
			this.Controls.Add(this.borderedPanel4);
			this.Controls.Add(this.borderedPanel1);
			this.Controls.Add(this.borderedPanel3);
			this.Controls.Add(this.ControlBorderedPanel);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(450, 450);
			this.Name = "ImageConvertorWindow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "NFirmwareEditor - Image Converter";
			this.ControlBorderedPanel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.NewHeightUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.NewWidthUpDown)).EndInit();
			this.borderedPanel3.ResumeLayout(false);
			this.borderedPanel3.PerformLayout();
			this.borderedPanel5.ResumeLayout(false);
			this.borderedPanel1.ResumeLayout(false);
			this.borderedPanel1.PerformLayout();
			this.ResizeContainerPanel.ResumeLayout(false);
			this.ResizeContainerPanel.PerformLayout();
			this.borderedPanel4.ResumeLayout(false);
			this.borderedPanel4.PerformLayout();
			this.borderedPanel6.ResumeLayout(false);
			this.borderedPanel6.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private UI.BorderedPanel ControlBorderedPanel;
		private System.Windows.Forms.Button OkButton;
		private System.Windows.Forms.Button CancelButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button SelectSourceButton;
		private System.Windows.Forms.TextBox SourceTextBox;
		private UI.BorderedPanel borderedPanel3;
		private System.Windows.Forms.Label label2;
		private UI.BorderedPanel borderedPanel5;
		private System.Windows.Forms.NumericUpDown NewHeightUpDown;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown NewWidthUpDown;
		private UI.DrawingSurface ImagePreviewSurface;
		private System.Windows.Forms.Button JoyetechSizeButton;
		private UI.BorderedPanel borderedPanel1;
		private System.Windows.Forms.Label label5;
		private UI.BorderedPanel ResizeContainerPanel;
		private UI.BorderedPanel borderedPanel4;
		private System.Windows.Forms.Label label6;
		private UI.BorderedPanel borderedPanel6;
	}
}