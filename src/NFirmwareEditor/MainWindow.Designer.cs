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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
			this.MainMenuStrip = new System.Windows.Forms.MenuStrip();
			this.FileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.OpenEncryptedMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.SaveEncryptedMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.OpenDecryptedMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.SaveDecryptedMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.ExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.EditMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.CopyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.PasteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ClearAllPixelsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.InvertMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.FlipHorizontalMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.FlipVerticalMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.ShiftUpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ShiftDownMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ShiftLeftMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ShiftRightMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.EncryptDecryptMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.AboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.MainStatusStrip = new System.Windows.Forms.StatusStrip();
			this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.CursorPositionLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.MainToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.Block2CheckBox = new System.Windows.Forms.CheckBox();
			this.Block1CheckBox = new System.Windows.Forms.CheckBox();
			this.ImageListBoxContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.CopyContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.PasteContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.ExportContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ImportContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.ImagesTabPage = new System.Windows.Forms.TabPage();
			this.StringsTabPage = new System.Windows.Forms.TabPage();
			this.borderedPanel2 = new NFirmwareEditor.UI.BorderedPanel();
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
			this.borderedPanel4 = new NFirmwareEditor.UI.BorderedPanel();
			this.ImagePixelGrid = new NFirmwareEditor.UI.PixelGrid();
			this.ShowGridCheckBox = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.GridSizeUpDown = new System.Windows.Forms.NumericUpDown();
			this.borderedPanel3 = new NFirmwareEditor.UI.BorderedPanel();
			this.label2 = new System.Windows.Forms.Label();
			this.borderedPanel5 = new NFirmwareEditor.UI.BorderedPanel();
			this.PreviewPixelGrid = new NFirmwareEditor.UI.PixelGrid();
			this.borderedPanel1 = new NFirmwareEditor.UI.BorderedPanel();
			this.Block1ImagesListBox = new System.Windows.Forms.ListBox();
			this.Block2ImagesListBox = new System.Windows.Forms.ListBox();
			this.MainMenuStrip.SuspendLayout();
			this.MainStatusStrip.SuspendLayout();
			this.ImageListBoxContextMenu.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.ImagesTabPage.SuspendLayout();
			this.borderedPanel2.SuspendLayout();
			this.borderedPanel4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.GridSizeUpDown)).BeginInit();
			this.borderedPanel3.SuspendLayout();
			this.borderedPanel5.SuspendLayout();
			this.borderedPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// MainMenuStrip
			// 
			this.MainMenuStrip.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.MainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenuItem,
            this.EditMenuItem,
            this.ToolsMenuItem,
            this.AboutMenuItem});
			this.MainMenuStrip.Location = new System.Drawing.Point(0, 0);
			this.MainMenuStrip.Name = "MainMenuStrip";
			this.MainMenuStrip.Size = new System.Drawing.Size(784, 24);
			this.MainMenuStrip.TabIndex = 0;
			this.MainMenuStrip.Text = "MainMenu";
			// 
			// FileMenuItem
			// 
			this.FileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenEncryptedMenuItem,
            this.SaveEncryptedMenuItem,
            this.toolStripSeparator1,
            this.OpenDecryptedMenuItem,
            this.SaveDecryptedMenuItem,
            this.toolStripSeparator2,
            this.ExitMenuItem});
			this.FileMenuItem.Name = "FileMenuItem";
			this.FileMenuItem.Size = new System.Drawing.Size(35, 20);
			this.FileMenuItem.Text = "File";
			// 
			// OpenEncryptedMenuItem
			// 
			this.OpenEncryptedMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("OpenEncryptedMenuItem.Image")));
			this.OpenEncryptedMenuItem.Name = "OpenEncryptedMenuItem";
			this.OpenEncryptedMenuItem.ShortcutKeyDisplayString = "";
			this.OpenEncryptedMenuItem.Size = new System.Drawing.Size(258, 22);
			this.OpenEncryptedMenuItem.Text = "Open Encrypted";
			// 
			// SaveEncryptedMenuItem
			// 
			this.SaveEncryptedMenuItem.Enabled = false;
			this.SaveEncryptedMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("SaveEncryptedMenuItem.Image")));
			this.SaveEncryptedMenuItem.Name = "SaveEncryptedMenuItem";
			this.SaveEncryptedMenuItem.ShortcutKeyDisplayString = "Ctrl + S";
			this.SaveEncryptedMenuItem.Size = new System.Drawing.Size(258, 22);
			this.SaveEncryptedMenuItem.Text = "Save Encrypted As...";
			this.SaveEncryptedMenuItem.Click += new System.EventHandler(this.SaveEncryptedMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(255, 6);
			// 
			// OpenDecryptedMenuItem
			// 
			this.OpenDecryptedMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("OpenDecryptedMenuItem.Image")));
			this.OpenDecryptedMenuItem.Name = "OpenDecryptedMenuItem";
			this.OpenDecryptedMenuItem.ShortcutKeyDisplayString = "";
			this.OpenDecryptedMenuItem.Size = new System.Drawing.Size(258, 22);
			this.OpenDecryptedMenuItem.Text = "Open Decrypted";
			// 
			// SaveDecryptedMenuItem
			// 
			this.SaveDecryptedMenuItem.Enabled = false;
			this.SaveDecryptedMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("SaveDecryptedMenuItem.Image")));
			this.SaveDecryptedMenuItem.Name = "SaveDecryptedMenuItem";
			this.SaveDecryptedMenuItem.ShortcutKeyDisplayString = "Ctrl + Shift + S";
			this.SaveDecryptedMenuItem.Size = new System.Drawing.Size(258, 22);
			this.SaveDecryptedMenuItem.Text = "Save Decrypted As...";
			this.SaveDecryptedMenuItem.Click += new System.EventHandler(this.SaveDecryptedMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(255, 6);
			// 
			// ExitMenuItem
			// 
			this.ExitMenuItem.Image = global::NFirmwareEditor.Properties.Resources.exit;
			this.ExitMenuItem.Name = "ExitMenuItem";
			this.ExitMenuItem.Size = new System.Drawing.Size(258, 22);
			this.ExitMenuItem.Text = "Exit";
			this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
			// 
			// EditMenuItem
			// 
			this.EditMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CopyMenuItem,
            this.PasteMenuItem,
            this.ClearAllPixelsMenuItem,
            this.toolStripSeparator4,
            this.InvertMenuItem,
            this.FlipHorizontalMenuItem,
            this.FlipVerticalMenuItem,
            this.toolStripSeparator5,
            this.ShiftUpMenuItem,
            this.ShiftDownMenuItem,
            this.ShiftLeftMenuItem,
            this.ShiftRightMenuItem});
			this.EditMenuItem.Enabled = false;
			this.EditMenuItem.Name = "EditMenuItem";
			this.EditMenuItem.Size = new System.Drawing.Size(37, 20);
			this.EditMenuItem.Text = "Edit";
			// 
			// CopyMenuItem
			// 
			this.CopyMenuItem.Image = global::NFirmwareEditor.Properties.Resources.copy;
			this.CopyMenuItem.Name = "CopyMenuItem";
			this.CopyMenuItem.ShortcutKeyDisplayString = "Ctrl + C";
			this.CopyMenuItem.Size = new System.Drawing.Size(191, 22);
			this.CopyMenuItem.Text = "Copy";
			this.CopyMenuItem.Click += new System.EventHandler(this.CopyMenuItem_Click);
			// 
			// PasteMenuItem
			// 
			this.PasteMenuItem.Image = global::NFirmwareEditor.Properties.Resources.paste;
			this.PasteMenuItem.Name = "PasteMenuItem";
			this.PasteMenuItem.ShortcutKeyDisplayString = "Ctrl + P";
			this.PasteMenuItem.Size = new System.Drawing.Size(191, 22);
			this.PasteMenuItem.Text = "Paste";
			this.PasteMenuItem.Click += new System.EventHandler(this.PasteMenuItem_Click);
			// 
			// ClearAllPixelsMenuItem
			// 
			this.ClearAllPixelsMenuItem.Image = global::NFirmwareEditor.Properties.Resources._new;
			this.ClearAllPixelsMenuItem.Name = "ClearAllPixelsMenuItem";
			this.ClearAllPixelsMenuItem.ShortcutKeyDisplayString = "Ctrl + N";
			this.ClearAllPixelsMenuItem.Size = new System.Drawing.Size(191, 22);
			this.ClearAllPixelsMenuItem.Text = "Clear";
			this.ClearAllPixelsMenuItem.Click += new System.EventHandler(this.ClearAllPixelsMenuItem_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(188, 6);
			// 
			// InvertMenuItem
			// 
			this.InvertMenuItem.Image = global::NFirmwareEditor.Properties.Resources.inverse;
			this.InvertMenuItem.Name = "InvertMenuItem";
			this.InvertMenuItem.ShortcutKeyDisplayString = "Ctrl + I";
			this.InvertMenuItem.Size = new System.Drawing.Size(191, 22);
			this.InvertMenuItem.Text = "Invert";
			this.InvertMenuItem.Click += new System.EventHandler(this.InvertMenuItem_Click);
			// 
			// FlipHorizontalMenuItem
			// 
			this.FlipHorizontalMenuItem.Image = global::NFirmwareEditor.Properties.Resources.flip_horizontal;
			this.FlipHorizontalMenuItem.Name = "FlipHorizontalMenuItem";
			this.FlipHorizontalMenuItem.Size = new System.Drawing.Size(191, 22);
			this.FlipHorizontalMenuItem.Text = "Flip Horizontal";
			this.FlipHorizontalMenuItem.Click += new System.EventHandler(this.FlipHorizontalMenuItem_Click);
			// 
			// FlipVerticalMenuItem
			// 
			this.FlipVerticalMenuItem.Image = global::NFirmwareEditor.Properties.Resources.flip_vertical;
			this.FlipVerticalMenuItem.Name = "FlipVerticalMenuItem";
			this.FlipVerticalMenuItem.Size = new System.Drawing.Size(191, 22);
			this.FlipVerticalMenuItem.Text = "Flip Vertical";
			this.FlipVerticalMenuItem.Click += new System.EventHandler(this.FlipVerticalMenuItem_Click);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(188, 6);
			// 
			// ShiftUpMenuItem
			// 
			this.ShiftUpMenuItem.Image = global::NFirmwareEditor.Properties.Resources.arrow_up;
			this.ShiftUpMenuItem.Name = "ShiftUpMenuItem";
			this.ShiftUpMenuItem.ShortcutKeyDisplayString = "Ctrl + Up";
			this.ShiftUpMenuItem.Size = new System.Drawing.Size(191, 22);
			this.ShiftUpMenuItem.Text = "Shift Up";
			this.ShiftUpMenuItem.Click += new System.EventHandler(this.ShiftUpMenuItem_Click);
			// 
			// ShiftDownMenuItem
			// 
			this.ShiftDownMenuItem.Image = global::NFirmwareEditor.Properties.Resources.arrow_down;
			this.ShiftDownMenuItem.Name = "ShiftDownMenuItem";
			this.ShiftDownMenuItem.ShortcutKeyDisplayString = "Ctrl + Down";
			this.ShiftDownMenuItem.Size = new System.Drawing.Size(191, 22);
			this.ShiftDownMenuItem.Text = "Shift Down";
			this.ShiftDownMenuItem.Click += new System.EventHandler(this.ShiftDownMenuItem_Click);
			// 
			// ShiftLeftMenuItem
			// 
			this.ShiftLeftMenuItem.Image = global::NFirmwareEditor.Properties.Resources.arrow_left;
			this.ShiftLeftMenuItem.Name = "ShiftLeftMenuItem";
			this.ShiftLeftMenuItem.ShortcutKeyDisplayString = "Ctrl + Left";
			this.ShiftLeftMenuItem.Size = new System.Drawing.Size(191, 22);
			this.ShiftLeftMenuItem.Text = "Shift Left";
			this.ShiftLeftMenuItem.Click += new System.EventHandler(this.ShiftLeftMenuItem_Click);
			// 
			// ShiftRightMenuItem
			// 
			this.ShiftRightMenuItem.Image = global::NFirmwareEditor.Properties.Resources.arrow_right;
			this.ShiftRightMenuItem.Name = "ShiftRightMenuItem";
			this.ShiftRightMenuItem.ShortcutKeyDisplayString = "Ctrl + Right";
			this.ShiftRightMenuItem.Size = new System.Drawing.Size(191, 22);
			this.ShiftRightMenuItem.Text = "Shift Right";
			this.ShiftRightMenuItem.Click += new System.EventHandler(this.ShiftRightMenuItem_Click);
			// 
			// ToolsMenuItem
			// 
			this.ToolsMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EncryptDecryptMenuItem});
			this.ToolsMenuItem.Name = "ToolsMenuItem";
			this.ToolsMenuItem.Size = new System.Drawing.Size(44, 20);
			this.ToolsMenuItem.Text = "Tools";
			// 
			// EncryptDecryptMenuItem
			// 
			this.EncryptDecryptMenuItem.Image = global::NFirmwareEditor.Properties.Resources.toolbox;
			this.EncryptDecryptMenuItem.Name = "EncryptDecryptMenuItem";
			this.EncryptDecryptMenuItem.Size = new System.Drawing.Size(159, 22);
			this.EncryptDecryptMenuItem.Text = "Encrypt / Decrypt";
			this.EncryptDecryptMenuItem.Click += new System.EventHandler(this.EncryptDecryptToolStripMenuItem_Click);
			// 
			// AboutMenuItem
			// 
			this.AboutMenuItem.Name = "AboutMenuItem";
			this.AboutMenuItem.Size = new System.Drawing.Size(24, 20);
			this.AboutMenuItem.Text = "?";
			this.AboutMenuItem.Click += new System.EventHandler(this.AboutMenuItem_Click);
			// 
			// MainStatusStrip
			// 
			this.MainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel,
            this.CursorPositionLabel});
			this.MainStatusStrip.Location = new System.Drawing.Point(0, 539);
			this.MainStatusStrip.Name = "MainStatusStrip";
			this.MainStatusStrip.Size = new System.Drawing.Size(784, 22);
			this.MainStatusStrip.TabIndex = 4;
			this.MainStatusStrip.Text = "statusStrip1";
			// 
			// StatusLabel
			// 
			this.StatusLabel.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.StatusLabel.Name = "StatusLabel";
			this.StatusLabel.Size = new System.Drawing.Size(0, 17);
			// 
			// CursorPositionLabel
			// 
			this.CursorPositionLabel.Name = "CursorPositionLabel";
			this.CursorPositionLabel.Size = new System.Drawing.Size(0, 17);
			// 
			// Block2CheckBox
			// 
			this.Block2CheckBox.Appearance = System.Windows.Forms.Appearance.Button;
			this.Block2CheckBox.Location = new System.Drawing.Point(70, 1);
			this.Block2CheckBox.Name = "Block2CheckBox";
			this.Block2CheckBox.Size = new System.Drawing.Size(71, 30);
			this.Block2CheckBox.TabIndex = 2;
			this.Block2CheckBox.Text = "Block 2";
			this.Block2CheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.Block2CheckBox.UseVisualStyleBackColor = true;
			this.Block2CheckBox.CheckedChanged += new System.EventHandler(this.BlockCheckBox_CheckedChanged);
			// 
			// Block1CheckBox
			// 
			this.Block1CheckBox.Appearance = System.Windows.Forms.Appearance.Button;
			this.Block1CheckBox.Checked = true;
			this.Block1CheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.Block1CheckBox.Location = new System.Drawing.Point(-1, 1);
			this.Block1CheckBox.Name = "Block1CheckBox";
			this.Block1CheckBox.Size = new System.Drawing.Size(71, 30);
			this.Block1CheckBox.TabIndex = 1;
			this.Block1CheckBox.Text = "Block 1";
			this.Block1CheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.Block1CheckBox.UseVisualStyleBackColor = true;
			this.Block1CheckBox.CheckedChanged += new System.EventHandler(this.BlockCheckBox_CheckedChanged);
			// 
			// ImageListBoxContextMenu
			// 
			this.ImageListBoxContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CopyContextMenuItem,
            this.PasteContextMenuItem,
            this.toolStripSeparator3,
            this.ExportContextMenuItem,
            this.ImportContextMenuItem});
			this.ImageListBoxContextMenu.Name = "ImageListBoxContextMenu";
			this.ImageListBoxContextMenu.Size = new System.Drawing.Size(111, 98);
			// 
			// CopyContextMenuItem
			// 
			this.CopyContextMenuItem.Image = global::NFirmwareEditor.Properties.Resources.copy;
			this.CopyContextMenuItem.Name = "CopyContextMenuItem";
			this.CopyContextMenuItem.Size = new System.Drawing.Size(110, 22);
			this.CopyContextMenuItem.Text = "Copy";
			this.CopyContextMenuItem.Click += new System.EventHandler(this.CopyContextMenuItem_Click);
			// 
			// PasteContextMenuItem
			// 
			this.PasteContextMenuItem.Image = global::NFirmwareEditor.Properties.Resources.paste;
			this.PasteContextMenuItem.Name = "PasteContextMenuItem";
			this.PasteContextMenuItem.Size = new System.Drawing.Size(110, 22);
			this.PasteContextMenuItem.Text = "Paste";
			this.PasteContextMenuItem.Click += new System.EventHandler(this.PasteContextMenuItem_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(107, 6);
			// 
			// ExportContextMenuItem
			// 
			this.ExportContextMenuItem.Image = global::NFirmwareEditor.Properties.Resources.image_export;
			this.ExportContextMenuItem.Name = "ExportContextMenuItem";
			this.ExportContextMenuItem.Size = new System.Drawing.Size(110, 22);
			this.ExportContextMenuItem.Text = "Export";
			this.ExportContextMenuItem.Click += new System.EventHandler(this.ExportContextMenuItem_Click);
			// 
			// ImportContextMenuItem
			// 
			this.ImportContextMenuItem.Image = global::NFirmwareEditor.Properties.Resources.image_import;
			this.ImportContextMenuItem.Name = "ImportContextMenuItem";
			this.ImportContextMenuItem.Size = new System.Drawing.Size(110, 22);
			this.ImportContextMenuItem.Text = "Import";
			this.ImportContextMenuItem.Click += new System.EventHandler(this.ImportContextMenuItem_Click);
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.ImagesTabPage);
			this.tabControl1.Controls.Add(this.StringsTabPage);
			this.tabControl1.ItemSize = new System.Drawing.Size(100, 20);
			this.tabControl1.Location = new System.Drawing.Point(3, 27);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(780, 510);
			this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
			this.tabControl1.TabIndex = 6;
			// 
			// ImagesTabPage
			// 
			this.ImagesTabPage.Controls.Add(this.Block1CheckBox);
			this.ImagesTabPage.Controls.Add(this.borderedPanel2);
			this.ImagesTabPage.Controls.Add(this.Block2CheckBox);
			this.ImagesTabPage.Controls.Add(this.borderedPanel3);
			this.ImagesTabPage.Controls.Add(this.borderedPanel1);
			this.ImagesTabPage.Location = new System.Drawing.Point(4, 24);
			this.ImagesTabPage.Name = "ImagesTabPage";
			this.ImagesTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.ImagesTabPage.Size = new System.Drawing.Size(772, 482);
			this.ImagesTabPage.TabIndex = 0;
			this.ImagesTabPage.Text = "Images";
			this.ImagesTabPage.UseVisualStyleBackColor = true;
			// 
			// StringsTabPage
			// 
			this.StringsTabPage.Location = new System.Drawing.Point(4, 22);
			this.StringsTabPage.Name = "StringsTabPage";
			this.StringsTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.StringsTabPage.Size = new System.Drawing.Size(772, 484);
			this.StringsTabPage.TabIndex = 1;
			this.StringsTabPage.Text = "Strings";
			this.StringsTabPage.UseVisualStyleBackColor = true;
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
			this.borderedPanel2.Location = new System.Drawing.Point(143, 123);
			this.borderedPanel2.Name = "borderedPanel2";
			this.borderedPanel2.Padding = new System.Windows.Forms.Padding(1);
			this.borderedPanel2.Size = new System.Drawing.Size(627, 358);
			this.borderedPanel2.TabIndex = 2;
			this.borderedPanel2.Text = "borderedPanel2";
			// 
			// FlipVerticalButton
			// 
			this.FlipVerticalButton.Image = global::NFirmwareEditor.Properties.Resources.flip_vertical;
			this.FlipVerticalButton.Location = new System.Drawing.Point(315, 3);
			this.FlipVerticalButton.Name = "FlipVerticalButton";
			this.FlipVerticalButton.Size = new System.Drawing.Size(24, 24);
			this.FlipVerticalButton.TabIndex = 14;
			this.MainToolTip.SetToolTip(this.FlipVerticalButton, "Flip Vertical");
			this.FlipVerticalButton.UseVisualStyleBackColor = true;
			this.FlipVerticalButton.Click += new System.EventHandler(this.FlipVerticalButton_Click);
			// 
			// FlipHorizontalButton
			// 
			this.FlipHorizontalButton.Image = global::NFirmwareEditor.Properties.Resources.flip_horizontal;
			this.FlipHorizontalButton.Location = new System.Drawing.Point(292, 3);
			this.FlipHorizontalButton.Name = "FlipHorizontalButton";
			this.FlipHorizontalButton.Size = new System.Drawing.Size(24, 24);
			this.FlipHorizontalButton.TabIndex = 13;
			this.MainToolTip.SetToolTip(this.FlipHorizontalButton, "Flip Horizontal");
			this.FlipHorizontalButton.UseVisualStyleBackColor = true;
			this.FlipHorizontalButton.Click += new System.EventHandler(this.FlipHorizontalButton_Click);
			// 
			// PasteButton
			// 
			this.PasteButton.Image = global::NFirmwareEditor.Properties.Resources.paste;
			this.PasteButton.Location = new System.Drawing.Point(262, 3);
			this.PasteButton.Name = "PasteButton";
			this.PasteButton.Size = new System.Drawing.Size(24, 24);
			this.PasteButton.TabIndex = 12;
			this.MainToolTip.SetToolTip(this.PasteButton, "Paste");
			this.PasteButton.UseVisualStyleBackColor = true;
			this.PasteButton.Click += new System.EventHandler(this.PasteButton_Click);
			// 
			// CopyButton
			// 
			this.CopyButton.Image = global::NFirmwareEditor.Properties.Resources.copy;
			this.CopyButton.Location = new System.Drawing.Point(239, 3);
			this.CopyButton.Name = "CopyButton";
			this.CopyButton.Size = new System.Drawing.Size(24, 24);
			this.CopyButton.TabIndex = 11;
			this.MainToolTip.SetToolTip(this.CopyButton, "Copy");
			this.CopyButton.UseVisualStyleBackColor = true;
			this.CopyButton.Click += new System.EventHandler(this.CopyButton_Click);
			// 
			// InverseButton
			// 
			this.InverseButton.Image = global::NFirmwareEditor.Properties.Resources.inverse;
			this.InverseButton.Location = new System.Drawing.Point(209, 3);
			this.InverseButton.Name = "InverseButton";
			this.InverseButton.Size = new System.Drawing.Size(24, 24);
			this.InverseButton.TabIndex = 10;
			this.MainToolTip.SetToolTip(this.InverseButton, "Invert");
			this.InverseButton.UseVisualStyleBackColor = true;
			this.InverseButton.Click += new System.EventHandler(this.InvertButton_Click);
			// 
			// ClearAllPixelsButton
			// 
			this.ClearAllPixelsButton.Image = global::NFirmwareEditor.Properties.Resources._new;
			this.ClearAllPixelsButton.Location = new System.Drawing.Point(186, 3);
			this.ClearAllPixelsButton.Name = "ClearAllPixelsButton";
			this.ClearAllPixelsButton.Size = new System.Drawing.Size(24, 24);
			this.ClearAllPixelsButton.TabIndex = 9;
			this.MainToolTip.SetToolTip(this.ClearAllPixelsButton, "Clear all pixels");
			this.ClearAllPixelsButton.UseVisualStyleBackColor = true;
			this.ClearAllPixelsButton.Click += new System.EventHandler(this.ClearAllPixelsButton_Click);
			// 
			// ShiftDownButton
			// 
			this.ShiftDownButton.Image = global::NFirmwareEditor.Properties.Resources.arrow_down;
			this.ShiftDownButton.Location = new System.Drawing.Point(414, 3);
			this.ShiftDownButton.Name = "ShiftDownButton";
			this.ShiftDownButton.Size = new System.Drawing.Size(24, 24);
			this.ShiftDownButton.TabIndex = 7;
			this.MainToolTip.SetToolTip(this.ShiftDownButton, "Shift down");
			this.ShiftDownButton.UseVisualStyleBackColor = true;
			this.ShiftDownButton.Click += new System.EventHandler(this.ShiftDownButton_Click);
			// 
			// ShiftUpButton
			// 
			this.ShiftUpButton.Image = global::NFirmwareEditor.Properties.Resources.arrow_up;
			this.ShiftUpButton.Location = new System.Drawing.Point(391, 3);
			this.ShiftUpButton.Name = "ShiftUpButton";
			this.ShiftUpButton.Size = new System.Drawing.Size(24, 24);
			this.ShiftUpButton.TabIndex = 6;
			this.MainToolTip.SetToolTip(this.ShiftUpButton, "Shift up");
			this.ShiftUpButton.UseVisualStyleBackColor = true;
			this.ShiftUpButton.Click += new System.EventHandler(this.ShiftUpButton_Click);
			// 
			// ShiftRightButton
			// 
			this.ShiftRightButton.Image = global::NFirmwareEditor.Properties.Resources.arrow_right;
			this.ShiftRightButton.Location = new System.Drawing.Point(368, 3);
			this.ShiftRightButton.Name = "ShiftRightButton";
			this.ShiftRightButton.Size = new System.Drawing.Size(24, 24);
			this.ShiftRightButton.TabIndex = 5;
			this.MainToolTip.SetToolTip(this.ShiftRightButton, "Shift right");
			this.ShiftRightButton.UseVisualStyleBackColor = true;
			this.ShiftRightButton.Click += new System.EventHandler(this.ShiftRightButton_Click);
			// 
			// ShiftLeftButton
			// 
			this.ShiftLeftButton.Image = global::NFirmwareEditor.Properties.Resources.arrow_left;
			this.ShiftLeftButton.Location = new System.Drawing.Point(345, 3);
			this.ShiftLeftButton.Name = "ShiftLeftButton";
			this.ShiftLeftButton.Size = new System.Drawing.Size(24, 24);
			this.ShiftLeftButton.TabIndex = 4;
			this.MainToolTip.SetToolTip(this.ShiftLeftButton, "Shift left");
			this.ShiftLeftButton.UseVisualStyleBackColor = true;
			this.ShiftLeftButton.Click += new System.EventHandler(this.ShiftLeftButton_Click);
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
			this.borderedPanel4.Size = new System.Drawing.Size(625, 328);
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
			this.ImagePixelGrid.Size = new System.Drawing.Size(625, 327);
			this.ImagePixelGrid.TabIndex = 0;
			this.ImagePixelGrid.Text = "pixelGrid1";
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
			this.borderedPanel3.Size = new System.Drawing.Size(627, 118);
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
			this.borderedPanel5.Size = new System.Drawing.Size(625, 88);
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
			this.PreviewPixelGrid.Location = new System.Drawing.Point(2, 3);
			this.PreviewPixelGrid.Margin = new System.Windows.Forms.Padding(2);
			this.PreviewPixelGrid.Name = "PreviewPixelGrid";
			this.PreviewPixelGrid.ReadOnly = true;
			this.PreviewPixelGrid.ShowGrid = false;
			this.PreviewPixelGrid.Size = new System.Drawing.Size(621, 83);
			this.PreviewPixelGrid.TabIndex = 3;
			this.PreviewPixelGrid.Text = "pixelGrid1";
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
			this.borderedPanel1.Controls.Add(this.Block1ImagesListBox);
			this.borderedPanel1.Controls.Add(this.Block2ImagesListBox);
			this.borderedPanel1.Location = new System.Drawing.Point(0, 33);
			this.borderedPanel1.Name = "borderedPanel1";
			this.borderedPanel1.Padding = new System.Windows.Forms.Padding(1);
			this.borderedPanel1.Size = new System.Drawing.Size(140, 448);
			this.borderedPanel1.TabIndex = 5;
			this.borderedPanel1.Text = "borderedPanel1";
			// 
			// Block1ImagesListBox
			// 
			this.Block1ImagesListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.Block1ImagesListBox.ContextMenuStrip = this.ImageListBoxContextMenu;
			this.Block1ImagesListBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Block1ImagesListBox.Font = new System.Drawing.Font("Consolas", 8.25F);
			this.Block1ImagesListBox.FormattingEnabled = true;
			this.Block1ImagesListBox.IntegralHeight = false;
			this.Block1ImagesListBox.Location = new System.Drawing.Point(1, 1);
			this.Block1ImagesListBox.Name = "Block1ImagesListBox";
			this.Block1ImagesListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.Block1ImagesListBox.Size = new System.Drawing.Size(138, 446);
			this.Block1ImagesListBox.TabIndex = 0;
			this.Block1ImagesListBox.SelectedValueChanged += new System.EventHandler(this.BlockImagesListBox_SelectedValueChanged);
			// 
			// Block2ImagesListBox
			// 
			this.Block2ImagesListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.Block2ImagesListBox.ContextMenuStrip = this.ImageListBoxContextMenu;
			this.Block2ImagesListBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Block2ImagesListBox.Font = new System.Drawing.Font("Consolas", 8.25F);
			this.Block2ImagesListBox.FormattingEnabled = true;
			this.Block2ImagesListBox.IntegralHeight = false;
			this.Block2ImagesListBox.Location = new System.Drawing.Point(1, 1);
			this.Block2ImagesListBox.Name = "Block2ImagesListBox";
			this.Block2ImagesListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.Block2ImagesListBox.Size = new System.Drawing.Size(138, 446);
			this.Block2ImagesListBox.TabIndex = 0;
			this.Block2ImagesListBox.SelectedValueChanged += new System.EventHandler(this.BlockImagesListBox_SelectedValueChanged);
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(784, 561);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.MainStatusStrip);
			this.Controls.Add(this.MainMenuStrip);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.MinimumSize = new System.Drawing.Size(800, 600);
			this.Name = "MainWindow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "NFirmwareEditor";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
			this.SizeChanged += new System.EventHandler(this.MainWindow_SizeChanged);
			this.MainMenuStrip.ResumeLayout(false);
			this.MainMenuStrip.PerformLayout();
			this.MainStatusStrip.ResumeLayout(false);
			this.MainStatusStrip.PerformLayout();
			this.ImageListBoxContextMenu.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.ImagesTabPage.ResumeLayout(false);
			this.borderedPanel2.ResumeLayout(false);
			this.borderedPanel2.PerformLayout();
			this.borderedPanel4.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.GridSizeUpDown)).EndInit();
			this.borderedPanel3.ResumeLayout(false);
			this.borderedPanel3.PerformLayout();
			this.borderedPanel5.ResumeLayout(false);
			this.borderedPanel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip MainMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem FileMenuItem;
		private System.Windows.Forms.ToolStripMenuItem OpenEncryptedMenuItem;
		private System.Windows.Forms.ToolStripMenuItem OpenDecryptedMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem ExitMenuItem;
		private System.Windows.Forms.ToolStripMenuItem SaveEncryptedMenuItem;
		private System.Windows.Forms.ToolStripMenuItem SaveDecryptedMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private UI.BorderedPanel borderedPanel2;
		private System.Windows.Forms.ListBox Block1ImagesListBox;
		private UI.PixelGrid ImagePixelGrid;
		private UI.BorderedPanel borderedPanel3;
		private System.Windows.Forms.StatusStrip MainStatusStrip;
		private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
		private System.Windows.Forms.NumericUpDown GridSizeUpDown;
		private System.Windows.Forms.CheckBox ShowGridCheckBox;
		private UI.PixelGrid PreviewPixelGrid;
		private UI.BorderedPanel borderedPanel4;
		private UI.BorderedPanel borderedPanel5;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button ShiftLeftButton;
		private System.Windows.Forms.Button ShiftRightButton;
		private System.Windows.Forms.Button ShiftUpButton;
		private System.Windows.Forms.Button ShiftDownButton;
		private System.Windows.Forms.Button ClearAllPixelsButton;
		private System.Windows.Forms.Button InverseButton;
		private System.Windows.Forms.ToolTip MainToolTip;
		private System.Windows.Forms.Button PasteButton;
		private System.Windows.Forms.Button CopyButton;
		private System.Windows.Forms.ToolStripMenuItem EditMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ToolsMenuItem;
		private System.Windows.Forms.ToolStripMenuItem EncryptDecryptMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ClearAllPixelsMenuItem;
		private System.Windows.Forms.ToolStripMenuItem InvertMenuItem;
		private System.Windows.Forms.ToolStripMenuItem CopyMenuItem;
		private System.Windows.Forms.ToolStripMenuItem PasteMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ShiftUpMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ShiftDownMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ShiftLeftMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ShiftRightMenuItem;
		private System.Windows.Forms.ToolStripMenuItem AboutMenuItem;
		private System.Windows.Forms.ListBox Block2ImagesListBox;
		private UI.BorderedPanel borderedPanel1;
		private System.Windows.Forms.CheckBox Block1CheckBox;
		private System.Windows.Forms.CheckBox Block2CheckBox;
		private System.Windows.Forms.ContextMenuStrip ImageListBoxContextMenu;
		private System.Windows.Forms.ToolStripMenuItem ExportContextMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ImportContextMenuItem;
		private System.Windows.Forms.ToolStripMenuItem CopyContextMenuItem;
		private System.Windows.Forms.ToolStripMenuItem PasteContextMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripStatusLabel CursorPositionLabel;
		private System.Windows.Forms.Button FlipHorizontalButton;
		private System.Windows.Forms.Button FlipVerticalButton;
		private System.Windows.Forms.ToolStripMenuItem FlipHorizontalMenuItem;
		private System.Windows.Forms.ToolStripMenuItem FlipVerticalMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage ImagesTabPage;
		private System.Windows.Forms.TabPage StringsTabPage;
	}
}

