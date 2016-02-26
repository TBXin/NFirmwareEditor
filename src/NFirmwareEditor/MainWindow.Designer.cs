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
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.OpenEncryptedMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.SaveEncryptedMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.OpenDecryptedMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.SaveDecryptedMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.ExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.EditMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.clearAllPixelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.invertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.shiftUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.shiftDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.shiftLeftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.shiftRightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.encryptDecryptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.AboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.MainStatusStrip = new System.Windows.Forms.StatusStrip();
			this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.DefinitionsComboBox = new System.Windows.Forms.ComboBox();
			this.MainToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.PasteButton = new System.Windows.Forms.Button();
			this.CopyButton = new System.Windows.Forms.Button();
			this.InverseButton = new System.Windows.Forms.Button();
			this.ClearAllPixelsButton = new System.Windows.Forms.Button();
			this.ShiftDownButton = new System.Windows.Forms.Button();
			this.ShiftUpButton = new System.Windows.Forms.Button();
			this.ShiftRightButton = new System.Windows.Forms.Button();
			this.ShiftLeftButton = new System.Windows.Forms.Button();
			this.borderedPanel1 = new NFirmwareEditor.UI.BorderedPanel();
			this.Block1ImagesListBox = new System.Windows.Forms.ListBox();
			this.Block2ImagesListBox = new System.Windows.Forms.ListBox();
			this.Block2CheckBox = new System.Windows.Forms.CheckBox();
			this.Block1CheckBox = new System.Windows.Forms.CheckBox();
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
			this.MainMenuStrip.SuspendLayout();
			this.MainStatusStrip.SuspendLayout();
			this.borderedPanel1.SuspendLayout();
			this.borderedPanel3.SuspendLayout();
			this.borderedPanel5.SuspendLayout();
			this.borderedPanel2.SuspendLayout();
			this.borderedPanel4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.GridSizeUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// MainMenuStrip
			// 
			this.MainMenuStrip.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.MainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.EditMenuItem,
            this.toolsToolStripMenuItem,
            this.AboutMenuItem});
			this.MainMenuStrip.Location = new System.Drawing.Point(0, 0);
			this.MainMenuStrip.Name = "MainMenuStrip";
			this.MainMenuStrip.Size = new System.Drawing.Size(784, 24);
			this.MainMenuStrip.TabIndex = 0;
			this.MainMenuStrip.Text = "MainMenu";
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
			this.OpenEncryptedMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("OpenEncryptedMenuItem.Image")));
			this.OpenEncryptedMenuItem.Name = "OpenEncryptedMenuItem";
			this.OpenEncryptedMenuItem.ShortcutKeyDisplayString = "Ctrl + O";
			this.OpenEncryptedMenuItem.Size = new System.Drawing.Size(258, 22);
			this.OpenEncryptedMenuItem.Text = "Open Encrypted";
			this.OpenEncryptedMenuItem.Click += new System.EventHandler(this.OpenEncryptedMenuItem_Click);
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
			this.OpenDecryptedMenuItem.ShortcutKeyDisplayString = "Ctrl + E";
			this.OpenDecryptedMenuItem.Size = new System.Drawing.Size(258, 22);
			this.OpenDecryptedMenuItem.Text = "Open Decrypted";
			this.OpenDecryptedMenuItem.Click += new System.EventHandler(this.OpenDecryptedMenuItem_Click);
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
            this.clearAllPixelsToolStripMenuItem,
            this.invertToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.shiftUpToolStripMenuItem,
            this.shiftDownToolStripMenuItem,
            this.shiftLeftToolStripMenuItem,
            this.shiftRightToolStripMenuItem});
			this.EditMenuItem.Enabled = false;
			this.EditMenuItem.Name = "EditMenuItem";
			this.EditMenuItem.Size = new System.Drawing.Size(37, 20);
			this.EditMenuItem.Text = "Edit";
			// 
			// clearAllPixelsToolStripMenuItem
			// 
			this.clearAllPixelsToolStripMenuItem.Image = global::NFirmwareEditor.Properties.Resources._new;
			this.clearAllPixelsToolStripMenuItem.Name = "clearAllPixelsToolStripMenuItem";
			this.clearAllPixelsToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl + N";
			this.clearAllPixelsToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
			this.clearAllPixelsToolStripMenuItem.Text = "Clear All Pixels";
			this.clearAllPixelsToolStripMenuItem.Click += new System.EventHandler(this.ClearAllPixelsMenuItem_Click);
			// 
			// invertToolStripMenuItem
			// 
			this.invertToolStripMenuItem.Image = global::NFirmwareEditor.Properties.Resources.inverse;
			this.invertToolStripMenuItem.Name = "invertToolStripMenuItem";
			this.invertToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl + I";
			this.invertToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
			this.invertToolStripMenuItem.Text = "Invert";
			this.invertToolStripMenuItem.Click += new System.EventHandler(this.InvertMenuItem_Click);
			// 
			// copyToolStripMenuItem
			// 
			this.copyToolStripMenuItem.Image = global::NFirmwareEditor.Properties.Resources.copy;
			this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			this.copyToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl + C";
			this.copyToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
			this.copyToolStripMenuItem.Text = "Copy";
			this.copyToolStripMenuItem.Click += new System.EventHandler(this.CopyMenuItem_Click);
			// 
			// pasteToolStripMenuItem
			// 
			this.pasteToolStripMenuItem.Image = global::NFirmwareEditor.Properties.Resources.paste;
			this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
			this.pasteToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl + P";
			this.pasteToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
			this.pasteToolStripMenuItem.Text = "Paste";
			this.pasteToolStripMenuItem.Click += new System.EventHandler(this.PasteMenuItem_Click);
			// 
			// shiftUpToolStripMenuItem
			// 
			this.shiftUpToolStripMenuItem.Image = global::NFirmwareEditor.Properties.Resources.arrow_up;
			this.shiftUpToolStripMenuItem.Name = "shiftUpToolStripMenuItem";
			this.shiftUpToolStripMenuItem.ShortcutKeyDisplayString = "Up";
			this.shiftUpToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
			this.shiftUpToolStripMenuItem.Text = "Shift Up";
			this.shiftUpToolStripMenuItem.Click += new System.EventHandler(this.ShiftUpMenuItem_Click);
			// 
			// shiftDownToolStripMenuItem
			// 
			this.shiftDownToolStripMenuItem.Image = global::NFirmwareEditor.Properties.Resources.arrow_down;
			this.shiftDownToolStripMenuItem.Name = "shiftDownToolStripMenuItem";
			this.shiftDownToolStripMenuItem.ShortcutKeyDisplayString = "Down";
			this.shiftDownToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
			this.shiftDownToolStripMenuItem.Text = "Shift Down";
			this.shiftDownToolStripMenuItem.Click += new System.EventHandler(this.ShiftDownMenuItem_Click);
			// 
			// shiftLeftToolStripMenuItem
			// 
			this.shiftLeftToolStripMenuItem.Image = global::NFirmwareEditor.Properties.Resources.arrow_left;
			this.shiftLeftToolStripMenuItem.Name = "shiftLeftToolStripMenuItem";
			this.shiftLeftToolStripMenuItem.ShortcutKeyDisplayString = "Left";
			this.shiftLeftToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
			this.shiftLeftToolStripMenuItem.Text = "Shift Left";
			this.shiftLeftToolStripMenuItem.Click += new System.EventHandler(this.ShiftLeftMenuItem_Click);
			// 
			// shiftRightToolStripMenuItem
			// 
			this.shiftRightToolStripMenuItem.Image = global::NFirmwareEditor.Properties.Resources.arrow_right;
			this.shiftRightToolStripMenuItem.Name = "shiftRightToolStripMenuItem";
			this.shiftRightToolStripMenuItem.ShortcutKeyDisplayString = "Right";
			this.shiftRightToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
			this.shiftRightToolStripMenuItem.Text = "Shift Right";
			this.shiftRightToolStripMenuItem.Click += new System.EventHandler(this.ShiftRightMenuItem_Click);
			// 
			// toolsToolStripMenuItem
			// 
			this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.encryptDecryptToolStripMenuItem});
			this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
			this.toolsToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.toolsToolStripMenuItem.Text = "Tools";
			// 
			// encryptDecryptToolStripMenuItem
			// 
			this.encryptDecryptToolStripMenuItem.Image = global::NFirmwareEditor.Properties.Resources.toolbox;
			this.encryptDecryptToolStripMenuItem.Name = "encryptDecryptToolStripMenuItem";
			this.encryptDecryptToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
			this.encryptDecryptToolStripMenuItem.Text = "Encrypt / Decrypt";
			this.encryptDecryptToolStripMenuItem.Click += new System.EventHandler(this.encryptDecryptToolStripMenuItem_Click);
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
            this.StatusLabel});
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
			// DefinitionsComboBox
			// 
			this.DefinitionsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.DefinitionsComboBox.FormattingEnabled = true;
			this.DefinitionsComboBox.Location = new System.Drawing.Point(6, 27);
			this.DefinitionsComboBox.Name = "DefinitionsComboBox";
			this.DefinitionsComboBox.Size = new System.Drawing.Size(140, 21);
			this.DefinitionsComboBox.TabIndex = 4;
			this.MainToolTip.SetToolTip(this.DefinitionsComboBox, "Firmware definitions");
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
			this.ShiftDownButton.Location = new System.Drawing.Point(361, 3);
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
			this.ShiftUpButton.Location = new System.Drawing.Point(338, 3);
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
			this.ShiftRightButton.Location = new System.Drawing.Point(315, 3);
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
			this.ShiftLeftButton.Location = new System.Drawing.Point(292, 3);
			this.ShiftLeftButton.Name = "ShiftLeftButton";
			this.ShiftLeftButton.Size = new System.Drawing.Size(24, 24);
			this.ShiftLeftButton.TabIndex = 4;
			this.MainToolTip.SetToolTip(this.ShiftLeftButton, "Shift left");
			this.ShiftLeftButton.UseVisualStyleBackColor = true;
			this.ShiftLeftButton.Click += new System.EventHandler(this.ShiftLeftButton_Click);
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
			this.borderedPanel1.Location = new System.Drawing.Point(6, 91);
			this.borderedPanel1.Name = "borderedPanel1";
			this.borderedPanel1.Padding = new System.Windows.Forms.Padding(1);
			this.borderedPanel1.Size = new System.Drawing.Size(140, 442);
			this.borderedPanel1.TabIndex = 5;
			this.borderedPanel1.Text = "borderedPanel1";
			// 
			// Block1ImagesListBox
			// 
			this.Block1ImagesListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.Block1ImagesListBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Block1ImagesListBox.Font = new System.Drawing.Font("Consolas", 8.25F);
			this.Block1ImagesListBox.FormattingEnabled = true;
			this.Block1ImagesListBox.IntegralHeight = false;
			this.Block1ImagesListBox.Location = new System.Drawing.Point(1, 1);
			this.Block1ImagesListBox.Name = "Block1ImagesListBox";
			this.Block1ImagesListBox.Size = new System.Drawing.Size(138, 440);
			this.Block1ImagesListBox.TabIndex = 0;
			this.Block1ImagesListBox.SelectedValueChanged += new System.EventHandler(this.BlockImagesListBox_SelectedValueChanged);
			// 
			// Block2ImagesListBox
			// 
			this.Block2ImagesListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.Block2ImagesListBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Block2ImagesListBox.Font = new System.Drawing.Font("Consolas", 8.25F);
			this.Block2ImagesListBox.FormattingEnabled = true;
			this.Block2ImagesListBox.IntegralHeight = false;
			this.Block2ImagesListBox.Location = new System.Drawing.Point(1, 1);
			this.Block2ImagesListBox.Name = "Block2ImagesListBox";
			this.Block2ImagesListBox.Size = new System.Drawing.Size(138, 440);
			this.Block2ImagesListBox.TabIndex = 0;
			this.Block2ImagesListBox.SelectedValueChanged += new System.EventHandler(this.BlockImagesListBox_SelectedValueChanged);
			// 
			// Block2CheckBox
			// 
			this.Block2CheckBox.Appearance = System.Windows.Forms.Appearance.Button;
			this.Block2CheckBox.Location = new System.Drawing.Point(76, 55);
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
			this.Block1CheckBox.Location = new System.Drawing.Point(6, 55);
			this.Block1CheckBox.Name = "Block1CheckBox";
			this.Block1CheckBox.Size = new System.Drawing.Size(71, 30);
			this.Block1CheckBox.TabIndex = 1;
			this.Block1CheckBox.Text = "Block 1";
			this.Block1CheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.Block1CheckBox.UseVisualStyleBackColor = true;
			this.Block1CheckBox.CheckedChanged += new System.EventHandler(this.BlockCheckBox_CheckedChanged);
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
			this.borderedPanel3.Size = new System.Drawing.Size(628, 118);
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
			this.borderedPanel5.Size = new System.Drawing.Size(626, 88);
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
			this.PreviewPixelGrid.ShowGrid = false;
			this.PreviewPixelGrid.Size = new System.Drawing.Size(622, 83);
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
			this.borderedPanel2.Location = new System.Drawing.Point(151, 151);
			this.borderedPanel2.Name = "borderedPanel2";
			this.borderedPanel2.Padding = new System.Windows.Forms.Padding(1);
			this.borderedPanel2.Size = new System.Drawing.Size(627, 382);
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
			this.borderedPanel4.Size = new System.Drawing.Size(625, 352);
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
			this.ImagePixelGrid.Size = new System.Drawing.Size(625, 351);
			this.ImagePixelGrid.TabIndex = 0;
			this.ImagePixelGrid.Text = "pixelGrid1";
			this.ImagePixelGrid.DataUpdated += new NFirmwareEditor.UI.PixelGrid.DataUpdatedDelegate(this.ImagePixelGrid_DataUpdated);
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
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(784, 561);
			this.Controls.Add(this.Block2CheckBox);
			this.Controls.Add(this.borderedPanel1);
			this.Controls.Add(this.Block1CheckBox);
			this.Controls.Add(this.MainStatusStrip);
			this.Controls.Add(this.DefinitionsComboBox);
			this.Controls.Add(this.borderedPanel3);
			this.Controls.Add(this.borderedPanel2);
			this.Controls.Add(this.MainMenuStrip);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.MinimumSize = new System.Drawing.Size(800, 600);
			this.Name = "MainWindow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "NFirmwareEditor";
			this.Load += new System.EventHandler(this.MainWindow_Load);
			this.MainMenuStrip.ResumeLayout(false);
			this.MainMenuStrip.PerformLayout();
			this.MainStatusStrip.ResumeLayout(false);
			this.MainStatusStrip.PerformLayout();
			this.borderedPanel1.ResumeLayout(false);
			this.borderedPanel3.ResumeLayout(false);
			this.borderedPanel3.PerformLayout();
			this.borderedPanel5.ResumeLayout(false);
			this.borderedPanel2.ResumeLayout(false);
			this.borderedPanel2.PerformLayout();
			this.borderedPanel4.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.GridSizeUpDown)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip MainMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
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
		private System.Windows.Forms.ComboBox DefinitionsComboBox;
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
		private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem encryptDecryptToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem clearAllPixelsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem invertToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem shiftUpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem shiftDownToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem shiftLeftToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem shiftRightToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem AboutMenuItem;
		private System.Windows.Forms.ListBox Block2ImagesListBox;
		private UI.BorderedPanel borderedPanel1;
		private System.Windows.Forms.CheckBox Block1CheckBox;
		private System.Windows.Forms.CheckBox Block2CheckBox;
	}
}

