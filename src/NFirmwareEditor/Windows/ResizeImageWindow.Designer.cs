namespace NFirmwareEditor.Windows
{
	partial class ResizeImageWindow
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
			this.NewHeightUpDown = new System.Windows.Forms.NumericUpDown();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.NewWidthUpDown = new System.Windows.Forms.NumericUpDown();
			this.CurrentHeightUpDown = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.CurrentWidthUpDown = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.ControlBorderedPanel = new NCore.UI.BorderedPanel();
			this.OkButton = new System.Windows.Forms.Button();
			this.CancelButton = new System.Windows.Forms.Button();
			this.groupPanel1 = new NCore.UI.GroupPanel();
			this.groupPanel2 = new NCore.UI.GroupPanel();
			((System.ComponentModel.ISupportInitialize)(this.NewHeightUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.NewWidthUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.CurrentHeightUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.CurrentWidthUpDown)).BeginInit();
			this.ControlBorderedPanel.SuspendLayout();
			this.groupPanel1.SuspendLayout();
			this.groupPanel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// NewHeightUpDown
			// 
			this.NewHeightUpDown.Location = new System.Drawing.Point(154, 33);
			this.NewHeightUpDown.Maximum = new decimal(new int[] {
            128,
            0,
            0,
            0});
			this.NewHeightUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.NewHeightUpDown.Name = "NewHeightUpDown";
			this.NewHeightUpDown.Size = new System.Drawing.Size(50, 21);
			this.NewHeightUpDown.TabIndex = 1;
			this.NewHeightUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(9, 36);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(39, 13);
			this.label4.TabIndex = 6;
			this.label4.Text = "Width:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(108, 36);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(42, 13);
			this.label3.TabIndex = 7;
			this.label3.Text = "Height:";
			// 
			// NewWidthUpDown
			// 
			this.NewWidthUpDown.Location = new System.Drawing.Point(52, 33);
			this.NewWidthUpDown.Maximum = new decimal(new int[] {
            128,
            0,
            0,
            0});
			this.NewWidthUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.NewWidthUpDown.Name = "NewWidthUpDown";
			this.NewWidthUpDown.Size = new System.Drawing.Size(50, 21);
			this.NewWidthUpDown.TabIndex = 0;
			this.NewWidthUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// CurrentHeightUpDown
			// 
			this.CurrentHeightUpDown.Location = new System.Drawing.Point(154, 33);
			this.CurrentHeightUpDown.Maximum = new decimal(new int[] {
            128,
            0,
            0,
            0});
			this.CurrentHeightUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.CurrentHeightUpDown.Name = "CurrentHeightUpDown";
			this.CurrentHeightUpDown.ReadOnly = true;
			this.CurrentHeightUpDown.Size = new System.Drawing.Size(50, 21);
			this.CurrentHeightUpDown.TabIndex = 1;
			this.CurrentHeightUpDown.TabStop = false;
			this.CurrentHeightUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(108, 36);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(42, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Height:";
			// 
			// CurrentWidthUpDown
			// 
			this.CurrentWidthUpDown.Location = new System.Drawing.Point(51, 33);
			this.CurrentWidthUpDown.Maximum = new decimal(new int[] {
            128,
            0,
            0,
            0});
			this.CurrentWidthUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.CurrentWidthUpDown.Name = "CurrentWidthUpDown";
			this.CurrentWidthUpDown.ReadOnly = true;
			this.CurrentWidthUpDown.Size = new System.Drawing.Size(50, 21);
			this.CurrentWidthUpDown.TabIndex = 0;
			this.CurrentWidthUpDown.TabStop = false;
			this.CurrentWidthUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(9, 36);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(39, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Width:";
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
			this.ControlBorderedPanel.Location = new System.Drawing.Point(0, 125);
			this.ControlBorderedPanel.Name = "ControlBorderedPanel";
			this.ControlBorderedPanel.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
			this.ControlBorderedPanel.Size = new System.Drawing.Size(214, 39);
			this.ControlBorderedPanel.TabIndex = 0;
			this.ControlBorderedPanel.Text = "borderedPanel1";
			// 
			// OkButton
			// 
			this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OkButton.Location = new System.Drawing.Point(4, 5);
			this.OkButton.Name = "OkButton";
			this.OkButton.Size = new System.Drawing.Size(100, 30);
			this.OkButton.TabIndex = 0;
			this.OkButton.Text = "Ok";
			this.OkButton.UseVisualStyleBackColor = true;
			// 
			// CancelButton
			// 
			this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelButton.Location = new System.Drawing.Point(110, 5);
			this.CancelButton.Name = "CancelButton";
			this.CancelButton.Size = new System.Drawing.Size(100, 30);
			this.CancelButton.TabIndex = 1;
			this.CancelButton.Text = "Cancel";
			this.CancelButton.UseVisualStyleBackColor = true;
			// 
			// groupPanel1
			// 
			this.groupPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupPanel1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(185)))), ((int)(((byte)(185)))));
			this.groupPanel1.Controls.Add(this.CurrentWidthUpDown);
			this.groupPanel1.Controls.Add(this.label1);
			this.groupPanel1.Controls.Add(this.CurrentHeightUpDown);
			this.groupPanel1.Controls.Add(this.label2);
			this.groupPanel1.HeaderBackColor = System.Drawing.Color.White;
			this.groupPanel1.HeaderHeight = 30;
			this.groupPanel1.Location = new System.Drawing.Point(3, 3);
			this.groupPanel1.Name = "groupPanel1";
			this.groupPanel1.Size = new System.Drawing.Size(208, 58);
			this.groupPanel1.TabIndex = 8;
			this.groupPanel1.TabStop = false;
			this.groupPanel1.Text = "Current size:";
			// 
			// groupPanel2
			// 
			this.groupPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupPanel2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(185)))), ((int)(((byte)(185)))));
			this.groupPanel2.Controls.Add(this.label4);
			this.groupPanel2.Controls.Add(this.NewWidthUpDown);
			this.groupPanel2.Controls.Add(this.NewHeightUpDown);
			this.groupPanel2.Controls.Add(this.label3);
			this.groupPanel2.HeaderBackColor = System.Drawing.Color.White;
			this.groupPanel2.HeaderHeight = 30;
			this.groupPanel2.Location = new System.Drawing.Point(3, 64);
			this.groupPanel2.Name = "groupPanel2";
			this.groupPanel2.Size = new System.Drawing.Size(208, 58);
			this.groupPanel2.TabIndex = 9;
			this.groupPanel2.TabStop = false;
			this.groupPanel2.Text = "New size:";
			// 
			// ResizeImageWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(214, 164);
			this.Controls.Add(this.groupPanel2);
			this.Controls.Add(this.groupPanel1);
			this.Controls.Add(this.ControlBorderedPanel);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ResizeImageWindow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Resize Image";
			((System.ComponentModel.ISupportInitialize)(this.NewHeightUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.NewWidthUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.CurrentHeightUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.CurrentWidthUpDown)).EndInit();
			this.ControlBorderedPanel.ResumeLayout(false);
			this.groupPanel1.ResumeLayout(false);
			this.groupPanel1.PerformLayout();
			this.groupPanel2.ResumeLayout(false);
			this.groupPanel2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private NCore.UI.BorderedPanel ControlBorderedPanel;
		private System.Windows.Forms.Button CancelButton;
		private System.Windows.Forms.Button OkButton;
		private System.Windows.Forms.NumericUpDown CurrentHeightUpDown;
		private System.Windows.Forms.NumericUpDown CurrentWidthUpDown;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown NewHeightUpDown;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown NewWidthUpDown;
		private NCore.UI.GroupPanel groupPanel1;
		private NCore.UI.GroupPanel groupPanel2;
	}
}