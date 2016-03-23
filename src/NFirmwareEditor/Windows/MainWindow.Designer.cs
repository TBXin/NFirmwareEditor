namespace NFirmwareEditor.Windows
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
			this.OpenEncryptedManualMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.SaveEncryptedMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.OpenDecryptedMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.OpenDecryptedManualMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.SaveDecryptedMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.ExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.EncryptDecryptMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.AboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.MainStatusStrip = new System.Windows.Forms.StatusStrip();
			this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.CursorPositionLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.MainToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.ImageListBoxContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.CopyContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.PasteContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.ExportContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ImportContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.borderedPanel1 = new NFirmwareEditor.UI.BorderedPanel();
			this.MainTabControl = new System.Windows.Forms.TabControl();
			this.MainMenuStrip.SuspendLayout();
			this.MainStatusStrip.SuspendLayout();
			this.ImageListBoxContextMenu.SuspendLayout();
			this.borderedPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// MainMenuStrip
			// 
			this.MainMenuStrip.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.MainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenuItem,
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
            this.OpenEncryptedManualMenuItem,
            this.SaveEncryptedMenuItem,
            this.toolStripSeparator1,
            this.OpenDecryptedMenuItem,
            this.OpenDecryptedManualMenuItem,
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
			this.OpenEncryptedMenuItem.ShortcutKeyDisplayString = "Ctrl + O";
			this.OpenEncryptedMenuItem.Size = new System.Drawing.Size(258, 22);
			this.OpenEncryptedMenuItem.Text = "Open Encrypted";
			this.OpenEncryptedMenuItem.Click += new System.EventHandler(this.OpenEncryptedMenuItem_Click);
			// 
			// OpenEncryptedManualMenuItem
			// 
			this.OpenEncryptedManualMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("OpenEncryptedManualMenuItem.Image")));
			this.OpenEncryptedManualMenuItem.Name = "OpenEncryptedManualMenuItem";
			this.OpenEncryptedManualMenuItem.ShortcutKeyDisplayString = "";
			this.OpenEncryptedManualMenuItem.Size = new System.Drawing.Size(258, 22);
			this.OpenEncryptedManualMenuItem.Text = "Open Encrypted (Manual)";
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
			this.OpenDecryptedMenuItem.ShortcutKeyDisplayString = "Ctrl + Shift + O";
			this.OpenDecryptedMenuItem.Size = new System.Drawing.Size(258, 22);
			this.OpenDecryptedMenuItem.Text = "Open Decrypted";
			this.OpenDecryptedMenuItem.Click += new System.EventHandler(this.OpenDecryptedMenuItem_Click);
			// 
			// OpenDecryptedManualMenuItem
			// 
			this.OpenDecryptedManualMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("OpenDecryptedManualMenuItem.Image")));
			this.OpenDecryptedManualMenuItem.Name = "OpenDecryptedManualMenuItem";
			this.OpenDecryptedManualMenuItem.ShortcutKeyDisplayString = "";
			this.OpenDecryptedManualMenuItem.Size = new System.Drawing.Size(258, 22);
			this.OpenDecryptedManualMenuItem.Text = "Open Decrypted (Manual)";
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
			// 
			// PasteContextMenuItem
			// 
			this.PasteContextMenuItem.Image = global::NFirmwareEditor.Properties.Resources.paste;
			this.PasteContextMenuItem.Name = "PasteContextMenuItem";
			this.PasteContextMenuItem.Size = new System.Drawing.Size(110, 22);
			this.PasteContextMenuItem.Text = "Paste";
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
			// 
			// ImportContextMenuItem
			// 
			this.ImportContextMenuItem.Image = global::NFirmwareEditor.Properties.Resources.image_import;
			this.ImportContextMenuItem.Name = "ImportContextMenuItem";
			this.ImportContextMenuItem.Size = new System.Drawing.Size(110, 22);
			this.ImportContextMenuItem.Text = "Import";
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
			this.borderedPanel1.Controls.Add(this.MainTabControl);
			this.borderedPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.borderedPanel1.Location = new System.Drawing.Point(0, 24);
			this.borderedPanel1.Name = "borderedPanel1";
			this.borderedPanel1.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
			this.borderedPanel1.Size = new System.Drawing.Size(784, 515);
			this.borderedPanel1.TabIndex = 7;
			this.borderedPanel1.Text = "borderedPanel1";
			// 
			// MainTabControl
			// 
			this.MainTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.MainTabControl.ItemSize = new System.Drawing.Size(100, 20);
			this.MainTabControl.Location = new System.Drawing.Point(4, 5);
			this.MainTabControl.Name = "MainTabControl";
			this.MainTabControl.SelectedIndex = 0;
			this.MainTabControl.Size = new System.Drawing.Size(778, 507);
			this.MainTabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
			this.MainTabControl.TabIndex = 6;
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(784, 561);
			this.Controls.Add(this.borderedPanel1);
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
			this.borderedPanel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip MainMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem FileMenuItem;
		private System.Windows.Forms.ToolStripMenuItem OpenEncryptedManualMenuItem;
		private System.Windows.Forms.ToolStripMenuItem OpenDecryptedManualMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem ExitMenuItem;
		private System.Windows.Forms.ToolStripMenuItem SaveEncryptedMenuItem;
		private System.Windows.Forms.ToolStripMenuItem SaveDecryptedMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.StatusStrip MainStatusStrip;
		private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
		private System.Windows.Forms.ToolTip MainToolTip;
		private System.Windows.Forms.ToolStripMenuItem ToolsMenuItem;
		private System.Windows.Forms.ToolStripMenuItem EncryptDecryptMenuItem;
		private System.Windows.Forms.ToolStripMenuItem AboutMenuItem;
		private System.Windows.Forms.ContextMenuStrip ImageListBoxContextMenu;
		private System.Windows.Forms.ToolStripMenuItem ExportContextMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ImportContextMenuItem;
		private System.Windows.Forms.ToolStripMenuItem CopyContextMenuItem;
		private System.Windows.Forms.ToolStripMenuItem PasteContextMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripStatusLabel CursorPositionLabel;
		private System.Windows.Forms.TabControl MainTabControl;
		private System.Windows.Forms.ToolStripMenuItem OpenEncryptedMenuItem;
		private System.Windows.Forms.ToolStripMenuItem OpenDecryptedMenuItem;
		private UI.BorderedPanel borderedPanel1;
	}
}

