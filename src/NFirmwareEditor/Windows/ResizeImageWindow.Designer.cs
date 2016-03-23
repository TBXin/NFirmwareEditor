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
			this.LayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.NewSizeGroupBox = new System.Windows.Forms.GroupBox();
			this.NewHeightUpDown = new System.Windows.Forms.NumericUpDown();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.NewWidthUpDown = new System.Windows.Forms.NumericUpDown();
			this.CurrentSizeGroupBox = new System.Windows.Forms.GroupBox();
			this.CurrentHeightUpDown = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.CurrentWidthUpDown = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.ControlBorderedPanel = new NFirmwareEditor.UI.BorderedPanel();
			this.OkButton = new System.Windows.Forms.Button();
			this.CancelButton = new System.Windows.Forms.Button();
			this.LayoutPanel.SuspendLayout();
			this.NewSizeGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.NewHeightUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.NewWidthUpDown)).BeginInit();
			this.CurrentSizeGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.CurrentHeightUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.CurrentWidthUpDown)).BeginInit();
			this.ControlBorderedPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// LayoutPanel
			// 
			this.LayoutPanel.ColumnCount = 1;
			this.LayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.LayoutPanel.Controls.Add(this.NewSizeGroupBox, 0, 1);
			this.LayoutPanel.Controls.Add(this.CurrentSizeGroupBox, 0, 0);
			this.LayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LayoutPanel.Location = new System.Drawing.Point(0, 0);
			this.LayoutPanel.Name = "LayoutPanel";
			this.LayoutPanel.RowCount = 2;
			this.LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.LayoutPanel.Size = new System.Drawing.Size(214, 102);
			this.LayoutPanel.TabIndex = 1;
			// 
			// NewSizeGroupBox
			// 
			this.NewSizeGroupBox.Controls.Add(this.NewHeightUpDown);
			this.NewSizeGroupBox.Controls.Add(this.label4);
			this.NewSizeGroupBox.Controls.Add(this.label3);
			this.NewSizeGroupBox.Controls.Add(this.NewWidthUpDown);
			this.NewSizeGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.NewSizeGroupBox.Location = new System.Drawing.Point(3, 54);
			this.NewSizeGroupBox.Name = "NewSizeGroupBox";
			this.NewSizeGroupBox.Size = new System.Drawing.Size(208, 45);
			this.NewSizeGroupBox.TabIndex = 0;
			this.NewSizeGroupBox.TabStop = false;
			this.NewSizeGroupBox.Text = "Set new size:";
			// 
			// NewHeightUpDown
			// 
			this.NewHeightUpDown.Location = new System.Drawing.Point(152, 17);
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
			this.label4.Location = new System.Drawing.Point(5, 20);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(39, 13);
			this.label4.TabIndex = 6;
			this.label4.Text = "Width:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(106, 20);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(42, 13);
			this.label3.TabIndex = 7;
			this.label3.Text = "Height:";
			// 
			// NewWidthUpDown
			// 
			this.NewWidthUpDown.Location = new System.Drawing.Point(48, 17);
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
			// CurrentSizeGroupBox
			// 
			this.CurrentSizeGroupBox.Controls.Add(this.CurrentHeightUpDown);
			this.CurrentSizeGroupBox.Controls.Add(this.label2);
			this.CurrentSizeGroupBox.Controls.Add(this.CurrentWidthUpDown);
			this.CurrentSizeGroupBox.Controls.Add(this.label1);
			this.CurrentSizeGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CurrentSizeGroupBox.Location = new System.Drawing.Point(3, 3);
			this.CurrentSizeGroupBox.Name = "CurrentSizeGroupBox";
			this.CurrentSizeGroupBox.Size = new System.Drawing.Size(208, 45);
			this.CurrentSizeGroupBox.TabIndex = 1;
			this.CurrentSizeGroupBox.TabStop = false;
			this.CurrentSizeGroupBox.Text = "Current size:";
			// 
			// CurrentHeightUpDown
			// 
			this.CurrentHeightUpDown.Location = new System.Drawing.Point(152, 17);
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
			this.label2.Location = new System.Drawing.Point(106, 20);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(42, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Height:";
			// 
			// CurrentWidthUpDown
			// 
			this.CurrentWidthUpDown.Location = new System.Drawing.Point(48, 17);
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
			this.label1.Location = new System.Drawing.Point(5, 20);
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
			this.ControlBorderedPanel.Location = new System.Drawing.Point(0, 102);
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
			// ResizeImageWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(214, 141);
			this.Controls.Add(this.LayoutPanel);
			this.Controls.Add(this.ControlBorderedPanel);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ResizeImageWindow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Resize Image";
			this.LayoutPanel.ResumeLayout(false);
			this.NewSizeGroupBox.ResumeLayout(false);
			this.NewSizeGroupBox.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.NewHeightUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.NewWidthUpDown)).EndInit();
			this.CurrentSizeGroupBox.ResumeLayout(false);
			this.CurrentSizeGroupBox.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.CurrentHeightUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.CurrentWidthUpDown)).EndInit();
			this.ControlBorderedPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private UI.BorderedPanel ControlBorderedPanel;
		private System.Windows.Forms.Button CancelButton;
		private System.Windows.Forms.Button OkButton;
		private System.Windows.Forms.TableLayoutPanel LayoutPanel;
		private System.Windows.Forms.GroupBox CurrentSizeGroupBox;
		private System.Windows.Forms.GroupBox NewSizeGroupBox;
		private System.Windows.Forms.NumericUpDown CurrentHeightUpDown;
		private System.Windows.Forms.NumericUpDown CurrentWidthUpDown;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown NewHeightUpDown;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown NewWidthUpDown;
	}
}