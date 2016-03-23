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
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.CurrentWidthUpDown = new System.Windows.Forms.NumericUpDown();
			this.CurrentHeightUpDown = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.NewHeightUpDown = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.NewWidthUpDown = new System.Windows.Forms.NumericUpDown();
			this.label4 = new System.Windows.Forms.Label();
			this.borderedPanel1 = new NFirmwareEditor.UI.BorderedPanel();
			this.OkButton = new System.Windows.Forms.Button();
			this.CancelButton = new System.Windows.Forms.Button();
			this.tableLayoutPanel1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.CurrentWidthUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.CurrentHeightUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.NewHeightUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.NewWidthUpDown)).BeginInit();
			this.borderedPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(214, 102);
			this.tableLayoutPanel1.TabIndex = 1;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.NewHeightUpDown);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.NewWidthUpDown);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Location = new System.Drawing.Point(3, 54);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(208, 45);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Set new size:";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.CurrentHeightUpDown);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.CurrentWidthUpDown);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox2.Location = new System.Drawing.Point(3, 3);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(208, 45);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Current size:";
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
			this.CurrentWidthUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
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
			this.CurrentHeightUpDown.Value = new decimal(new int[] {
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
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(106, 20);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(42, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Height:";
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
			this.NewHeightUpDown.TabIndex = 5;
			this.NewHeightUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
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
			this.NewWidthUpDown.TabIndex = 4;
			this.NewWidthUpDown.Value = new decimal(new int[] {
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
			// borderedPanel1
			// 
			this.borderedPanel1.BackColor = System.Drawing.Color.Transparent;
			this.borderedPanel1.BorderBottom = false;
			this.borderedPanel1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(185)))), ((int)(((byte)(185)))));
			this.borderedPanel1.BorderLeft = false;
			this.borderedPanel1.BorderRight = false;
			this.borderedPanel1.BorderTop = true;
			this.borderedPanel1.BorderWidth = 1F;
			this.borderedPanel1.Controls.Add(this.OkButton);
			this.borderedPanel1.Controls.Add(this.CancelButton);
			this.borderedPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.borderedPanel1.Location = new System.Drawing.Point(0, 102);
			this.borderedPanel1.Name = "borderedPanel1";
			this.borderedPanel1.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
			this.borderedPanel1.Size = new System.Drawing.Size(214, 39);
			this.borderedPanel1.TabIndex = 0;
			this.borderedPanel1.Text = "borderedPanel1";
			// 
			// OkButton
			// 
			this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OkButton.Location = new System.Drawing.Point(4, 5);
			this.OkButton.Name = "OkButton";
			this.OkButton.Size = new System.Drawing.Size(100, 30);
			this.OkButton.TabIndex = 2;
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
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.borderedPanel1);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ResizeImageWindow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Resize Image";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.CurrentWidthUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.CurrentHeightUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.NewHeightUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.NewWidthUpDown)).EndInit();
			this.borderedPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private UI.BorderedPanel borderedPanel1;
		private System.Windows.Forms.Button CancelButton;
		private System.Windows.Forms.Button OkButton;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox1;
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