namespace NFirmwareEditor
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
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.OpenEncryptedMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.SaveEncryptedMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.OpenDecryptedMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.SaveDecryptedMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.ExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.DefinitionsComboBox = new System.Windows.Forms.ComboBox();
			this.borderedPanel3 = new NFirmwareEditor.UI.BorderedPanel();
			this.label2 = new System.Windows.Forms.Label();
			this.borderedPanel5 = new NFirmwareEditor.UI.BorderedPanel();
			this.PreviewPixelGrid = new NFirmwareEditor.UI.PixelGrid();
			this.borderedPanel2 = new NFirmwareEditor.UI.BorderedPanel();
			this.borderedPanel4 = new NFirmwareEditor.UI.BorderedPanel();
			this.ImagePixelGrid = new NFirmwareEditor.UI.PixelGrid();
			this.ShowGridCheckBox = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.GridSizeUpDown = new System.Windows.Forms.NumericUpDown();
			this.borderedPanel1 = new NFirmwareEditor.UI.BorderedPanel();
			this.ImagesListBox = new System.Windows.Forms.ListBox();
			this.menuStrip1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.borderedPanel3.SuspendLayout();
			this.borderedPanel5.SuspendLayout();
			this.borderedPanel2.SuspendLayout();
			this.borderedPanel4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.GridSizeUpDown)).BeginInit();
			this.borderedPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(858, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "MainMenu";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenEncryptedMenuItem,
            this.SaveEncryptedMenuItem,
            this.toolStripSeparator1,
            this.OpenDecryptedMenuItem,
            this.SaveDecryptedMenuItem,
            this.toolStripSeparator2,
            this.ExitMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// OpenEncryptedMenuItem
			// 
			this.OpenEncryptedMenuItem.Name = "OpenEncryptedMenuItem";
			this.OpenEncryptedMenuItem.Size = new System.Drawing.Size(178, 22);
			this.OpenEncryptedMenuItem.Text = "Open Encrypted";
			this.OpenEncryptedMenuItem.Click += new System.EventHandler(this.OpenEncryptedMenuItem_Click);
			// 
			// SaveEncryptedMenuItem
			// 
			this.SaveEncryptedMenuItem.Enabled = false;
			this.SaveEncryptedMenuItem.Name = "SaveEncryptedMenuItem";
			this.SaveEncryptedMenuItem.Size = new System.Drawing.Size(178, 22);
			this.SaveEncryptedMenuItem.Text = "Save Encrypted As...";
			this.SaveEncryptedMenuItem.Click += new System.EventHandler(this.SaveEncryptedMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(175, 6);
			// 
			// OpenDecryptedMenuItem
			// 
			this.OpenDecryptedMenuItem.Name = "OpenDecryptedMenuItem";
			this.OpenDecryptedMenuItem.Size = new System.Drawing.Size(178, 22);
			this.OpenDecryptedMenuItem.Text = "Open Decrypted";
			this.OpenDecryptedMenuItem.Click += new System.EventHandler(this.OpenDecryptedMenuItem_Click);
			// 
			// SaveDecryptedMenuItem
			// 
			this.SaveDecryptedMenuItem.Enabled = false;
			this.SaveDecryptedMenuItem.Name = "SaveDecryptedMenuItem";
			this.SaveDecryptedMenuItem.Size = new System.Drawing.Size(178, 22);
			this.SaveDecryptedMenuItem.Text = "Save Decrypted As...";
			this.SaveDecryptedMenuItem.Click += new System.EventHandler(this.SaveDecryptedMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(175, 6);
			// 
			// ExitMenuItem
			// 
			this.ExitMenuItem.Name = "ExitMenuItem";
			this.ExitMenuItem.Size = new System.Drawing.Size(178, 22);
			this.ExitMenuItem.Text = "Exit";
			this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel});
			this.statusStrip1.Location = new System.Drawing.Point(0, 517);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(858, 22);
			this.statusStrip1.TabIndex = 4;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// StatusLabel
			// 
			this.StatusLabel.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.StatusLabel.Name = "StatusLabel";
			this.StatusLabel.Size = new System.Drawing.Size(0, 17);
			// 
			// DefinitionsComboBox
			// 
			this.DefinitionsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.DefinitionsComboBox.FormattingEnabled = true;
			this.DefinitionsComboBox.Location = new System.Drawing.Point(6, 27);
			this.DefinitionsComboBox.Name = "DefinitionsComboBox";
			this.DefinitionsComboBox.Size = new System.Drawing.Size(140, 21);
			this.DefinitionsComboBox.TabIndex = 4;
			// 
			// borderedPanel3
			// 
			this.borderedPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
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
			this.borderedPanel3.Location = new System.Drawing.Point(151, 27);
			this.borderedPanel3.Name = "borderedPanel3";
			this.borderedPanel3.Padding = new System.Windows.Forms.Padding(1);
			this.borderedPanel3.Size = new System.Drawing.Size(702, 118);
			this.borderedPanel3.TabIndex = 3;
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
			this.borderedPanel5.BackColor = System.Drawing.Color.Transparent;
			this.borderedPanel5.BorderBottom = false;
			this.borderedPanel5.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(185)))), ((int)(((byte)(185)))));
			this.borderedPanel5.BorderLeft = false;
			this.borderedPanel5.BorderRight = false;
			this.borderedPanel5.BorderTop = true;
			this.borderedPanel5.BorderWidth = 1F;
			this.borderedPanel5.Controls.Add(this.PreviewPixelGrid);
			this.borderedPanel5.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.borderedPanel5.Location = new System.Drawing.Point(1, 29);
			this.borderedPanel5.Name = "borderedPanel5";
			this.borderedPanel5.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
			this.borderedPanel5.Size = new System.Drawing.Size(700, 88);
			this.borderedPanel5.TabIndex = 4;
			this.borderedPanel5.Text = "borderedPanel5";
			// 
			// PreviewPixelGrid
			// 
			this.PreviewPixelGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.PreviewPixelGrid.AutoScroll = true;
			this.PreviewPixelGrid.AutoScrollMinSize = new System.Drawing.Size(6, 6);
			this.PreviewPixelGrid.BackColor = System.Drawing.Color.Black;
			this.PreviewPixelGrid.BlockSize = 2;
			this.PreviewPixelGrid.Location = new System.Drawing.Point(1, 2);
			this.PreviewPixelGrid.Margin = new System.Windows.Forms.Padding(2);
			this.PreviewPixelGrid.Name = "PreviewPixelGrid";
			this.PreviewPixelGrid.ShowGrid = false;
			this.PreviewPixelGrid.Size = new System.Drawing.Size(698, 85);
			this.PreviewPixelGrid.TabIndex = 3;
			this.PreviewPixelGrid.Text = "pixelGrid1";
			// 
			// borderedPanel2
			// 
			this.borderedPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.borderedPanel2.BackColor = System.Drawing.Color.White;
			this.borderedPanel2.BorderBottom = true;
			this.borderedPanel2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(185)))), ((int)(((byte)(185)))));
			this.borderedPanel2.BorderLeft = true;
			this.borderedPanel2.BorderRight = true;
			this.borderedPanel2.BorderTop = true;
			this.borderedPanel2.BorderWidth = 1F;
			this.borderedPanel2.Controls.Add(this.borderedPanel4);
			this.borderedPanel2.Controls.Add(this.ShowGridCheckBox);
			this.borderedPanel2.Controls.Add(this.label1);
			this.borderedPanel2.Controls.Add(this.GridSizeUpDown);
			this.borderedPanel2.Location = new System.Drawing.Point(151, 151);
			this.borderedPanel2.Name = "borderedPanel2";
			this.borderedPanel2.Padding = new System.Windows.Forms.Padding(1);
			this.borderedPanel2.Size = new System.Drawing.Size(701, 360);
			this.borderedPanel2.TabIndex = 2;
			this.borderedPanel2.Text = "borderedPanel2";
			// 
			// borderedPanel4
			// 
			this.borderedPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.borderedPanel4.BackColor = System.Drawing.Color.Transparent;
			this.borderedPanel4.BorderBottom = false;
			this.borderedPanel4.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(185)))), ((int)(((byte)(185)))));
			this.borderedPanel4.BorderLeft = false;
			this.borderedPanel4.BorderRight = false;
			this.borderedPanel4.BorderTop = true;
			this.borderedPanel4.BorderWidth = 1F;
			this.borderedPanel4.Controls.Add(this.ImagePixelGrid);
			this.borderedPanel4.Location = new System.Drawing.Point(1, 29);
			this.borderedPanel4.Name = "borderedPanel4";
			this.borderedPanel4.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
			this.borderedPanel4.Size = new System.Drawing.Size(699, 330);
			this.borderedPanel4.TabIndex = 3;
			this.borderedPanel4.Text = "borderedPanel4";
			// 
			// ImagePixelGrid
			// 
			this.ImagePixelGrid.AutoScroll = true;
			this.ImagePixelGrid.AutoScrollMinSize = new System.Drawing.Size(6, 6);
			this.ImagePixelGrid.BackColor = System.Drawing.SystemColors.Control;
			this.ImagePixelGrid.BackgroundImage = global::NFirmwareEditor.Properties.Resources.transparent_bg;
			this.ImagePixelGrid.BlockSize = 16;
			this.ImagePixelGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ImagePixelGrid.Location = new System.Drawing.Point(0, 1);
			this.ImagePixelGrid.Margin = new System.Windows.Forms.Padding(8);
			this.ImagePixelGrid.Name = "ImagePixelGrid";
			this.ImagePixelGrid.ShowGrid = true;
			this.ImagePixelGrid.Size = new System.Drawing.Size(699, 329);
			this.ImagePixelGrid.TabIndex = 0;
			this.ImagePixelGrid.Text = "pixelGrid1";
			this.ImagePixelGrid.DataUpdated += new NFirmwareEditor.UI.PixelGrid.DataUpdatedDelegate(this.ImagePixelGrid_DataUpdated);
			// 
			// ShowGridCheckBox
			// 
			this.ShowGridCheckBox.AutoSize = true;
			this.ShowGridCheckBox.Checked = true;
			this.ShowGridCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.ShowGridCheckBox.Location = new System.Drawing.Point(107, 8);
			this.ShowGridCheckBox.Name = "ShowGridCheckBox";
			this.ShowGridCheckBox.Size = new System.Drawing.Size(73, 17);
			this.ShowGridCheckBox.TabIndex = 2;
			this.ShowGridCheckBox.Text = "Show grid";
			this.ShowGridCheckBox.UseVisualStyleBackColor = true;
			this.ShowGridCheckBox.CheckedChanged += new System.EventHandler(this.ShowGridCheckBox_CheckedChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(4, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(51, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Grid size:";
			// 
			// GridSizeUpDown
			// 
			this.GridSizeUpDown.Location = new System.Drawing.Point(59, 5);
			this.GridSizeUpDown.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.GridSizeUpDown.Name = "GridSizeUpDown";
			this.GridSizeUpDown.Size = new System.Drawing.Size(42, 21);
			this.GridSizeUpDown.TabIndex = 0;
			this.GridSizeUpDown.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
			this.GridSizeUpDown.ValueChanged += new System.EventHandler(this.GridSizeUpDown_ValueChanged);
			// 
			// borderedPanel1
			// 
			this.borderedPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.borderedPanel1.BackColor = System.Drawing.Color.Transparent;
			this.borderedPanel1.BorderBottom = true;
			this.borderedPanel1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(185)))), ((int)(((byte)(185)))));
			this.borderedPanel1.BorderLeft = true;
			this.borderedPanel1.BorderRight = true;
			this.borderedPanel1.BorderTop = true;
			this.borderedPanel1.BorderWidth = 1F;
			this.borderedPanel1.Controls.Add(this.ImagesListBox);
			this.borderedPanel1.Location = new System.Drawing.Point(6, 54);
			this.borderedPanel1.Name = "borderedPanel1";
			this.borderedPanel1.Padding = new System.Windows.Forms.Padding(1);
			this.borderedPanel1.Size = new System.Drawing.Size(140, 457);
			this.borderedPanel1.TabIndex = 1;
			this.borderedPanel1.Text = "borderedPanel1";
			// 
			// ImagesListBox
			// 
			this.ImagesListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.ImagesListBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ImagesListBox.Font = new System.Drawing.Font("Consolas", 8.25F);
			this.ImagesListBox.FormattingEnabled = true;
			this.ImagesListBox.Location = new System.Drawing.Point(1, 1);
			this.ImagesListBox.Name = "ImagesListBox";
			this.ImagesListBox.Size = new System.Drawing.Size(138, 455);
			this.ImagesListBox.TabIndex = 0;
			this.ImagesListBox.SelectedValueChanged += new System.EventHandler(this.ImagesListBox_SelectedValueChanged);
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(858, 539);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.DefinitionsComboBox);
			this.Controls.Add(this.borderedPanel3);
			this.Controls.Add(this.borderedPanel2);
			this.Controls.Add(this.borderedPanel1);
			this.Controls.Add(this.menuStrip1);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "MainWindow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "NFirmwareEditor";
			this.Load += new System.EventHandler(this.MainWindow_Load);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.borderedPanel3.ResumeLayout(false);
			this.borderedPanel3.PerformLayout();
			this.borderedPanel5.ResumeLayout(false);
			this.borderedPanel2.ResumeLayout(false);
			this.borderedPanel2.PerformLayout();
			this.borderedPanel4.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.GridSizeUpDown)).EndInit();
			this.borderedPanel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem OpenEncryptedMenuItem;
		private System.Windows.Forms.ToolStripMenuItem OpenDecryptedMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem ExitMenuItem;
		private System.Windows.Forms.ToolStripMenuItem SaveEncryptedMenuItem;
		private System.Windows.Forms.ToolStripMenuItem SaveDecryptedMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private UI.BorderedPanel borderedPanel1;
		private UI.BorderedPanel borderedPanel2;
		private System.Windows.Forms.ListBox ImagesListBox;
		private UI.PixelGrid ImagePixelGrid;
		private UI.BorderedPanel borderedPanel3;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
		private System.Windows.Forms.NumericUpDown GridSizeUpDown;
		private System.Windows.Forms.CheckBox ShowGridCheckBox;
		private System.Windows.Forms.ComboBox DefinitionsComboBox;
		private UI.PixelGrid PreviewPixelGrid;
		private UI.BorderedPanel borderedPanel4;
		private UI.BorderedPanel borderedPanel5;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
	}
}

