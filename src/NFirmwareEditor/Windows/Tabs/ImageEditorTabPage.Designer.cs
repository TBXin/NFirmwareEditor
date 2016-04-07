namespace NFirmwareEditor.Windows.Tabs
{
	partial class ImageEditorTabPage
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.Block1ImageRadioButton = new System.Windows.Forms.RadioButton();
			this.Block2ImageRadioButton = new System.Windows.Forms.RadioButton();
			this.ImageListBoxContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.CopyContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.PasteContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.ExportContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ImageEditorToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.BitmapImportButton = new System.Windows.Forms.Button();
			this.ResizeButton = new System.Windows.Forms.Button();
			this.ImageEditorHotkeyInformationButton = new System.Windows.Forms.Button();
			this.FlipVerticalButton = new System.Windows.Forms.Button();
			this.FlipHorizontalButton = new System.Windows.Forms.Button();
			this.PasteButton = new System.Windows.Forms.Button();
			this.CopyButton = new System.Windows.Forms.Button();
			this.InverseButton = new System.Windows.Forms.Button();
			this.ClearAllPixelsButton = new System.Windows.Forms.Button();
			this.ShiftDownButton = new System.Windows.Forms.Button();
			this.ShiftUpButton = new System.Windows.Forms.Button();
			this.ShiftRightButton = new System.Windows.Forms.Button();
			this.ShiftLeftButton = new System.Windows.Forms.Button();
			this.borderedPanel3 = new NFirmwareEditor.UI.BorderedPanel();
			this.label2 = new System.Windows.Forms.Label();
			this.borderedPanel5 = new NFirmwareEditor.UI.BorderedPanel();
			this.ImagePreviewPixelGrid = new NFirmwareEditor.UI.PixelGrid();
			this.borderedPanel2 = new NFirmwareEditor.UI.BorderedPanel();
			this.borderedPanel4 = new NFirmwareEditor.UI.BorderedPanel();
			this.ImagePixelGrid = new NFirmwareEditor.UI.PixelGrid();
			this.ImageEditorStatusStrip = new System.Windows.Forms.StatusStrip();
			this.ImageSizeLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.CursorPositionLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.ShowGridCheckBox = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.GridSizeUpDown = new System.Windows.Forms.NumericUpDown();
			this.borderedPanel1 = new NFirmwareEditor.UI.BorderedPanel();
			this.Block1ImageListBox = new System.Windows.Forms.ListBox();
			this.Block2ImageListBox = new System.Windows.Forms.ListBox();
			this.ImageListBoxContextMenu.SuspendLayout();
			this.borderedPanel3.SuspendLayout();
			this.borderedPanel5.SuspendLayout();
			this.borderedPanel2.SuspendLayout();
			this.borderedPanel4.SuspendLayout();
			this.ImageEditorStatusStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.GridSizeUpDown)).BeginInit();
			this.borderedPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// Block1ImageRadioButton
			// 
			this.Block1ImageRadioButton.Appearance = System.Windows.Forms.Appearance.Button;
			this.Block1ImageRadioButton.Enabled = false;
			this.Block1ImageRadioButton.Location = new System.Drawing.Point(-1, 1);
			this.Block1ImageRadioButton.Name = "Block1ImageRadioButton";
			this.Block1ImageRadioButton.Size = new System.Drawing.Size(71, 30);
			this.Block1ImageRadioButton.TabIndex = 6;
			this.Block1ImageRadioButton.Text = "Block 1";
			this.Block1ImageRadioButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.Block1ImageRadioButton.UseVisualStyleBackColor = true;
			// 
			// Block2ImageRadioButton
			// 
			this.Block2ImageRadioButton.Appearance = System.Windows.Forms.Appearance.Button;
			this.Block2ImageRadioButton.Enabled = false;
			this.Block2ImageRadioButton.Location = new System.Drawing.Point(70, 1);
			this.Block2ImageRadioButton.Name = "Block2ImageRadioButton";
			this.Block2ImageRadioButton.Size = new System.Drawing.Size(71, 30);
			this.Block2ImageRadioButton.TabIndex = 8;
			this.Block2ImageRadioButton.Text = "Block 2";
			this.Block2ImageRadioButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.Block2ImageRadioButton.UseVisualStyleBackColor = true;
			// 
			// ImageListBoxContextMenu
			// 
			this.ImageListBoxContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CopyContextMenuItem,
            this.PasteContextMenuItem,
            this.toolStripSeparator3,
            this.ExportContextMenuItem});
			this.ImageListBoxContextMenu.Name = "ImageListBoxContextMenu";
			this.ImageListBoxContextMenu.Size = new System.Drawing.Size(195, 76);
			// 
			// CopyContextMenuItem
			// 
			this.CopyContextMenuItem.Image = global::NFirmwareEditor.Properties.Resources.copy;
			this.CopyContextMenuItem.Name = "CopyContextMenuItem";
			this.CopyContextMenuItem.Size = new System.Drawing.Size(194, 22);
			this.CopyContextMenuItem.Text = "Copy";
			// 
			// PasteContextMenuItem
			// 
			this.PasteContextMenuItem.Image = global::NFirmwareEditor.Properties.Resources.paste;
			this.PasteContextMenuItem.Name = "PasteContextMenuItem";
			this.PasteContextMenuItem.Size = new System.Drawing.Size(194, 22);
			this.PasteContextMenuItem.Text = "Paste";
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(191, 6);
			// 
			// ExportContextMenuItem
			// 
			this.ExportContextMenuItem.Image = global::NFirmwareEditor.Properties.Resources.image_export;
			this.ExportContextMenuItem.Name = "ExportContextMenuItem";
			this.ExportContextMenuItem.Size = new System.Drawing.Size(194, 22);
			this.ExportContextMenuItem.Text = "Resource Pack - Export";
			// 
			// BitmapImportButton
			// 
			this.BitmapImportButton.Image = global::NFirmwareEditor.Properties.Resources.bitmap_import;
			this.BitmapImportButton.Location = new System.Drawing.Point(308, 3);
			this.BitmapImportButton.Name = "BitmapImportButton";
			this.BitmapImportButton.Size = new System.Drawing.Size(24, 24);
			this.BitmapImportButton.TabIndex = 17;
			this.BitmapImportButton.TabStop = false;
			this.ImageEditorToolTip.SetToolTip(this.BitmapImportButton, "Import bitmap");
			this.BitmapImportButton.UseVisualStyleBackColor = true;
			// 
			// ResizeButton
			// 
			this.ResizeButton.Image = global::NFirmwareEditor.Properties.Resources.image_resize;
			this.ResizeButton.Location = new System.Drawing.Point(232, 3);
			this.ResizeButton.Name = "ResizeButton";
			this.ResizeButton.Size = new System.Drawing.Size(24, 24);
			this.ResizeButton.TabIndex = 16;
			this.ResizeButton.TabStop = false;
			this.ImageEditorToolTip.SetToolTip(this.ResizeButton, "Resize image");
			this.ResizeButton.UseVisualStyleBackColor = true;
			// 
			// ImageEditorHotkeyInformationButton
			// 
			this.ImageEditorHotkeyInformationButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ImageEditorHotkeyInformationButton.Image = global::NFirmwareEditor.Properties.Resources.information;
			this.ImageEditorHotkeyInformationButton.Location = new System.Drawing.Point(599, 3);
			this.ImageEditorHotkeyInformationButton.Name = "ImageEditorHotkeyInformationButton";
			this.ImageEditorHotkeyInformationButton.Size = new System.Drawing.Size(24, 24);
			this.ImageEditorHotkeyInformationButton.TabIndex = 15;
			this.ImageEditorHotkeyInformationButton.TabStop = false;
			this.ImageEditorToolTip.SetToolTip(this.ImageEditorHotkeyInformationButton, "Hotkey\'s list");
			this.ImageEditorHotkeyInformationButton.UseVisualStyleBackColor = true;
			// 
			// FlipVerticalButton
			// 
			this.FlipVerticalButton.Image = global::NFirmwareEditor.Properties.Resources.flip_vertical;
			this.FlipVerticalButton.Location = new System.Drawing.Point(361, 3);
			this.FlipVerticalButton.Name = "FlipVerticalButton";
			this.FlipVerticalButton.Size = new System.Drawing.Size(24, 24);
			this.FlipVerticalButton.TabIndex = 14;
			this.FlipVerticalButton.TabStop = false;
			this.ImageEditorToolTip.SetToolTip(this.FlipVerticalButton, "Flip image vertically");
			this.FlipVerticalButton.UseVisualStyleBackColor = true;
			// 
			// FlipHorizontalButton
			// 
			this.FlipHorizontalButton.Image = global::NFirmwareEditor.Properties.Resources.flip_horizontal;
			this.FlipHorizontalButton.Location = new System.Drawing.Point(338, 3);
			this.FlipHorizontalButton.Name = "FlipHorizontalButton";
			this.FlipHorizontalButton.Size = new System.Drawing.Size(24, 24);
			this.FlipHorizontalButton.TabIndex = 13;
			this.FlipHorizontalButton.TabStop = false;
			this.ImageEditorToolTip.SetToolTip(this.FlipHorizontalButton, "Flip image horizontally");
			this.FlipHorizontalButton.UseVisualStyleBackColor = true;
			// 
			// PasteButton
			// 
			this.PasteButton.Image = global::NFirmwareEditor.Properties.Resources.paste;
			this.PasteButton.Location = new System.Drawing.Point(285, 3);
			this.PasteButton.Name = "PasteButton";
			this.PasteButton.Size = new System.Drawing.Size(24, 24);
			this.PasteButton.TabIndex = 12;
			this.PasteButton.TabStop = false;
			this.ImageEditorToolTip.SetToolTip(this.PasteButton, "Paste image");
			this.PasteButton.UseVisualStyleBackColor = true;
			// 
			// CopyButton
			// 
			this.CopyButton.Image = global::NFirmwareEditor.Properties.Resources.copy;
			this.CopyButton.Location = new System.Drawing.Point(262, 3);
			this.CopyButton.Name = "CopyButton";
			this.CopyButton.Size = new System.Drawing.Size(24, 24);
			this.CopyButton.TabIndex = 11;
			this.CopyButton.TabStop = false;
			this.ImageEditorToolTip.SetToolTip(this.CopyButton, "Copy image");
			this.CopyButton.UseVisualStyleBackColor = true;
			// 
			// InverseButton
			// 
			this.InverseButton.Image = global::NFirmwareEditor.Properties.Resources.inverse;
			this.InverseButton.Location = new System.Drawing.Point(209, 3);
			this.InverseButton.Name = "InverseButton";
			this.InverseButton.Size = new System.Drawing.Size(24, 24);
			this.InverseButton.TabIndex = 10;
			this.InverseButton.TabStop = false;
			this.ImageEditorToolTip.SetToolTip(this.InverseButton, "Inverse image pixels");
			this.InverseButton.UseVisualStyleBackColor = true;
			// 
			// ClearAllPixelsButton
			// 
			this.ClearAllPixelsButton.Image = global::NFirmwareEditor.Properties.Resources._new;
			this.ClearAllPixelsButton.Location = new System.Drawing.Point(186, 3);
			this.ClearAllPixelsButton.Name = "ClearAllPixelsButton";
			this.ClearAllPixelsButton.Size = new System.Drawing.Size(24, 24);
			this.ClearAllPixelsButton.TabIndex = 9;
			this.ClearAllPixelsButton.TabStop = false;
			this.ImageEditorToolTip.SetToolTip(this.ClearAllPixelsButton, "Clear all pixels");
			this.ClearAllPixelsButton.UseVisualStyleBackColor = true;
			// 
			// ShiftDownButton
			// 
			this.ShiftDownButton.Image = global::NFirmwareEditor.Properties.Resources.arrow_down;
			this.ShiftDownButton.Location = new System.Drawing.Point(460, 3);
			this.ShiftDownButton.Name = "ShiftDownButton";
			this.ShiftDownButton.Size = new System.Drawing.Size(24, 24);
			this.ShiftDownButton.TabIndex = 7;
			this.ShiftDownButton.TabStop = false;
			this.ImageEditorToolTip.SetToolTip(this.ShiftDownButton, "Shift image down");
			this.ShiftDownButton.UseVisualStyleBackColor = true;
			// 
			// ShiftUpButton
			// 
			this.ShiftUpButton.Image = global::NFirmwareEditor.Properties.Resources.arrow_up;
			this.ShiftUpButton.Location = new System.Drawing.Point(437, 3);
			this.ShiftUpButton.Name = "ShiftUpButton";
			this.ShiftUpButton.Size = new System.Drawing.Size(24, 24);
			this.ShiftUpButton.TabIndex = 6;
			this.ShiftUpButton.TabStop = false;
			this.ImageEditorToolTip.SetToolTip(this.ShiftUpButton, "Shift image up");
			this.ShiftUpButton.UseVisualStyleBackColor = true;
			// 
			// ShiftRightButton
			// 
			this.ShiftRightButton.Image = global::NFirmwareEditor.Properties.Resources.arrow_right;
			this.ShiftRightButton.Location = new System.Drawing.Point(414, 3);
			this.ShiftRightButton.Name = "ShiftRightButton";
			this.ShiftRightButton.Size = new System.Drawing.Size(24, 24);
			this.ShiftRightButton.TabIndex = 5;
			this.ShiftRightButton.TabStop = false;
			this.ImageEditorToolTip.SetToolTip(this.ShiftRightButton, "Shift image right");
			this.ShiftRightButton.UseVisualStyleBackColor = true;
			// 
			// ShiftLeftButton
			// 
			this.ShiftLeftButton.Image = global::NFirmwareEditor.Properties.Resources.arrow_left;
			this.ShiftLeftButton.Location = new System.Drawing.Point(391, 3);
			this.ShiftLeftButton.Name = "ShiftLeftButton";
			this.ShiftLeftButton.Size = new System.Drawing.Size(24, 24);
			this.ShiftLeftButton.TabIndex = 4;
			this.ShiftLeftButton.TabStop = false;
			this.ImageEditorToolTip.SetToolTip(this.ShiftLeftButton, "Shift image left");
			this.ShiftLeftButton.UseVisualStyleBackColor = true;
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
			this.borderedPanel3.Location = new System.Drawing.Point(143, 2);
			this.borderedPanel3.Name = "borderedPanel3";
			this.borderedPanel3.Padding = new System.Windows.Forms.Padding(1);
			this.borderedPanel3.Size = new System.Drawing.Size(627, 169);
			this.borderedPanel3.TabIndex = 9;
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
			this.borderedPanel5.Controls.Add(this.ImagePreviewPixelGrid);
			this.borderedPanel5.Location = new System.Drawing.Point(1, 29);
			this.borderedPanel5.Name = "borderedPanel5";
			this.borderedPanel5.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
			this.borderedPanel5.Size = new System.Drawing.Size(625, 139);
			this.borderedPanel5.TabIndex = 4;
			this.borderedPanel5.Text = "borderedPanel5";
			// 
			// ImagePreviewPixelGrid
			// 
			this.ImagePreviewPixelGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ImagePreviewPixelGrid.AutoScroll = true;
			this.ImagePreviewPixelGrid.AutoScrollMinSize = new System.Drawing.Size(6, 6);
			this.ImagePreviewPixelGrid.BackColor = System.Drawing.Color.Black;
			this.ImagePreviewPixelGrid.BlockSize = 2;
			this.ImagePreviewPixelGrid.Location = new System.Drawing.Point(2, 3);
			this.ImagePreviewPixelGrid.Margin = new System.Windows.Forms.Padding(2);
			this.ImagePreviewPixelGrid.Name = "ImagePreviewPixelGrid";
			this.ImagePreviewPixelGrid.ReadOnly = true;
			this.ImagePreviewPixelGrid.ShowGrid = false;
			this.ImagePreviewPixelGrid.Size = new System.Drawing.Size(621, 134);
			this.ImagePreviewPixelGrid.TabIndex = 3;
			this.ImagePreviewPixelGrid.Text = "pixelGrid1";
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
			this.borderedPanel2.Controls.Add(this.BitmapImportButton);
			this.borderedPanel2.Controls.Add(this.ResizeButton);
			this.borderedPanel2.Controls.Add(this.ImageEditorHotkeyInformationButton);
			this.borderedPanel2.Controls.Add(this.FlipVerticalButton);
			this.borderedPanel2.Controls.Add(this.FlipHorizontalButton);
			this.borderedPanel2.Controls.Add(this.PasteButton);
			this.borderedPanel2.Controls.Add(this.CopyButton);
			this.borderedPanel2.Controls.Add(this.InverseButton);
			this.borderedPanel2.Controls.Add(this.ClearAllPixelsButton);
			this.borderedPanel2.Controls.Add(this.ShiftDownButton);
			this.borderedPanel2.Controls.Add(this.ShiftUpButton);
			this.borderedPanel2.Controls.Add(this.ShiftRightButton);
			this.borderedPanel2.Controls.Add(this.ShiftLeftButton);
			this.borderedPanel2.Controls.Add(this.borderedPanel4);
			this.borderedPanel2.Controls.Add(this.ShowGridCheckBox);
			this.borderedPanel2.Controls.Add(this.label1);
			this.borderedPanel2.Controls.Add(this.GridSizeUpDown);
			this.borderedPanel2.Location = new System.Drawing.Point(143, 174);
			this.borderedPanel2.Name = "borderedPanel2";
			this.borderedPanel2.Padding = new System.Windows.Forms.Padding(1);
			this.borderedPanel2.Size = new System.Drawing.Size(627, 307);
			this.borderedPanel2.TabIndex = 7;
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
			this.borderedPanel4.Controls.Add(this.ImageEditorStatusStrip);
			this.borderedPanel4.Location = new System.Drawing.Point(1, 29);
			this.borderedPanel4.Name = "borderedPanel4";
			this.borderedPanel4.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
			this.borderedPanel4.Size = new System.Drawing.Size(625, 277);
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
			this.ImagePixelGrid.ReadOnly = false;
			this.ImagePixelGrid.ShowGrid = true;
			this.ImagePixelGrid.Size = new System.Drawing.Size(625, 254);
			this.ImagePixelGrid.TabIndex = 0;
			this.ImagePixelGrid.Text = "pixelGrid1";
			// 
			// ImageEditorStatusStrip
			// 
			this.ImageEditorStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ImageSizeLabel,
            this.CursorPositionLabel});
			this.ImageEditorStatusStrip.Location = new System.Drawing.Point(0, 255);
			this.ImageEditorStatusStrip.Name = "ImageEditorStatusStrip";
			this.ImageEditorStatusStrip.Size = new System.Drawing.Size(625, 22);
			this.ImageEditorStatusStrip.SizingGrip = false;
			this.ImageEditorStatusStrip.TabIndex = 1;
			this.ImageEditorStatusStrip.Text = "statusStrip1";
			// 
			// ImageSizeLabel
			// 
			this.ImageSizeLabel.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ImageSizeLabel.Name = "ImageSizeLabel";
			this.ImageSizeLabel.Size = new System.Drawing.Size(0, 17);
			// 
			// CursorPositionLabel
			// 
			this.CursorPositionLabel.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.CursorPositionLabel.Name = "CursorPositionLabel";
			this.CursorPositionLabel.Size = new System.Drawing.Size(610, 17);
			this.CursorPositionLabel.Spring = true;
			this.CursorPositionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// ShowGridCheckBox
			// 
			this.ShowGridCheckBox.AutoSize = true;
			this.ShowGridCheckBox.Checked = true;
			this.ShowGridCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.ShowGridCheckBox.Location = new System.Drawing.Point(107, 7);
			this.ShowGridCheckBox.Name = "ShowGridCheckBox";
			this.ShowGridCheckBox.Size = new System.Drawing.Size(73, 17);
			this.ShowGridCheckBox.TabIndex = 2;
			this.ShowGridCheckBox.TabStop = false;
			this.ShowGridCheckBox.Text = "Show grid";
			this.ShowGridCheckBox.UseVisualStyleBackColor = true;
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
			this.GridSizeUpDown.TabStop = false;
			this.GridSizeUpDown.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
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
			this.borderedPanel1.Controls.Add(this.Block1ImageListBox);
			this.borderedPanel1.Controls.Add(this.Block2ImageListBox);
			this.borderedPanel1.Location = new System.Drawing.Point(0, 33);
			this.borderedPanel1.Name = "borderedPanel1";
			this.borderedPanel1.Padding = new System.Windows.Forms.Padding(1);
			this.borderedPanel1.Size = new System.Drawing.Size(140, 448);
			this.borderedPanel1.TabIndex = 10;
			this.borderedPanel1.Text = "borderedPanel1";
			// 
			// Block1ImageListBox
			// 
			this.Block1ImageListBox.BackColor = System.Drawing.Color.Black;
			this.Block1ImageListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.Block1ImageListBox.ContextMenuStrip = this.ImageListBoxContextMenu;
			this.Block1ImageListBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Block1ImageListBox.Font = new System.Drawing.Font("Consolas", 8.25F);
			this.Block1ImageListBox.ForeColor = System.Drawing.Color.White;
			this.Block1ImageListBox.FormattingEnabled = true;
			this.Block1ImageListBox.IntegralHeight = false;
			this.Block1ImageListBox.Location = new System.Drawing.Point(1, 1);
			this.Block1ImageListBox.Name = "Block1ImageListBox";
			this.Block1ImageListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.Block1ImageListBox.Size = new System.Drawing.Size(138, 446);
			this.Block1ImageListBox.TabIndex = 0;
			// 
			// Block2ImageListBox
			// 
			this.Block2ImageListBox.BackColor = System.Drawing.Color.Black;
			this.Block2ImageListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.Block2ImageListBox.ContextMenuStrip = this.ImageListBoxContextMenu;
			this.Block2ImageListBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Block2ImageListBox.Font = new System.Drawing.Font("Consolas", 8.25F);
			this.Block2ImageListBox.ForeColor = System.Drawing.Color.White;
			this.Block2ImageListBox.FormattingEnabled = true;
			this.Block2ImageListBox.IntegralHeight = false;
			this.Block2ImageListBox.Location = new System.Drawing.Point(1, 1);
			this.Block2ImageListBox.Name = "Block2ImageListBox";
			this.Block2ImageListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.Block2ImageListBox.Size = new System.Drawing.Size(138, 446);
			this.Block2ImageListBox.TabIndex = 0;
			// 
			// ImageEditorTabPage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.borderedPanel3);
			this.Controls.Add(this.Block1ImageRadioButton);
			this.Controls.Add(this.borderedPanel2);
			this.Controls.Add(this.Block2ImageRadioButton);
			this.Controls.Add(this.borderedPanel1);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.Name = "ImageEditorTabPage";
			this.Size = new System.Drawing.Size(772, 482);
			this.ImageListBoxContextMenu.ResumeLayout(false);
			this.borderedPanel3.ResumeLayout(false);
			this.borderedPanel3.PerformLayout();
			this.borderedPanel5.ResumeLayout(false);
			this.borderedPanel2.ResumeLayout(false);
			this.borderedPanel2.PerformLayout();
			this.borderedPanel4.ResumeLayout(false);
			this.borderedPanel4.PerformLayout();
			this.ImageEditorStatusStrip.ResumeLayout(false);
			this.ImageEditorStatusStrip.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.GridSizeUpDown)).EndInit();
			this.borderedPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListBox Block2ImageListBox;
		private System.Windows.Forms.ListBox Block1ImageListBox;
		private UI.BorderedPanel borderedPanel1;
		private UI.PixelGrid ImagePreviewPixelGrid;
		private UI.BorderedPanel borderedPanel5;
		private System.Windows.Forms.Label label2;
		private UI.BorderedPanel borderedPanel3;
		private System.Windows.Forms.RadioButton Block2ImageRadioButton;
		private UI.PixelGrid ImagePixelGrid;
		private UI.BorderedPanel borderedPanel4;
		private UI.BorderedPanel borderedPanel2;
		private System.Windows.Forms.RadioButton Block1ImageRadioButton;
		private System.Windows.Forms.NumericUpDown GridSizeUpDown;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox ShowGridCheckBox;
		private System.Windows.Forms.Button ShiftLeftButton;
		private System.Windows.Forms.Button ShiftRightButton;
		private System.Windows.Forms.Button ShiftUpButton;
		private System.Windows.Forms.Button ShiftDownButton;
		private System.Windows.Forms.Button ClearAllPixelsButton;
		private System.Windows.Forms.Button InverseButton;
		private System.Windows.Forms.Button CopyButton;
		private System.Windows.Forms.Button PasteButton;
		private System.Windows.Forms.Button FlipHorizontalButton;
		private System.Windows.Forms.Button FlipVerticalButton;
		private System.Windows.Forms.ContextMenuStrip ImageListBoxContextMenu;
		private System.Windows.Forms.ToolStripMenuItem CopyContextMenuItem;
		private System.Windows.Forms.ToolStripMenuItem PasteContextMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem ExportContextMenuItem;
		private System.Windows.Forms.Button ImageEditorHotkeyInformationButton;
		private System.Windows.Forms.StatusStrip ImageEditorStatusStrip;
		private System.Windows.Forms.ToolStripStatusLabel CursorPositionLabel;
		private System.Windows.Forms.ToolStripStatusLabel ImageSizeLabel;
		private System.Windows.Forms.Button ResizeButton;
		private System.Windows.Forms.Button BitmapImportButton;
		private System.Windows.Forms.ToolTip ImageEditorToolTip;
	}
}
