#pragma warning disable 108, 114
namespace NFirmwareEditor.Windows
{
	partial class ImportFontWindow
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
			this.ControlBorderedPanel = new NCore.UI.BorderedPanel();
			this.label3 = new System.Windows.Forms.Label();
			this.OkButton = new System.Windows.Forms.Button();
			this.CancelButton = new System.Windows.Forms.Button();
			this.UpperAZButton = new System.Windows.Forms.Button();
			this.LowerAZButton = new System.Windows.Forms.Button();
			this.ZeroNineButton = new System.Windows.Forms.Button();
			this.UnderlineButton = new System.Windows.Forms.CheckBox();
			this.ItalicButton = new System.Windows.Forms.CheckBox();
			this.BoldButton = new System.Windows.Forms.CheckBox();
			this.LoadFontButton = new System.Windows.Forms.Button();
			this.ShiftDownButton = new System.Windows.Forms.Button();
			this.ShiftUpButton = new System.Windows.Forms.Button();
			this.ShiftRightButton = new System.Windows.Forms.Button();
			this.ShiftLeftButton = new System.Windows.Forms.Button();
			this.FontSizeUpDown = new System.Windows.Forms.NumericUpDown();
			this.LettersTextBox = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.FontComboBox = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.FontPreviewSurface = new NCore.UI.DrawingSurface();
			this.label1 = new System.Windows.Forms.Label();
			this.MainToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.groupPanel1 = new NCore.UI.GroupPanel();
			this.ControlBorderedPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.FontSizeUpDown)).BeginInit();
			this.groupPanel1.SuspendLayout();
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
			this.ControlBorderedPanel.Controls.Add(this.label3);
			this.ControlBorderedPanel.Controls.Add(this.OkButton);
			this.ControlBorderedPanel.Controls.Add(this.CancelButton);
			this.ControlBorderedPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.ControlBorderedPanel.Location = new System.Drawing.Point(0, 317);
			this.ControlBorderedPanel.Name = "ControlBorderedPanel";
			this.ControlBorderedPanel.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
			this.ControlBorderedPanel.Size = new System.Drawing.Size(834, 44);
			this.ControlBorderedPanel.TabIndex = 3;
			this.ControlBorderedPanel.Text = "borderedPanel1";
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label3.BackColor = System.Drawing.Color.Transparent;
			this.label3.Location = new System.Drawing.Point(4, 4);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(614, 36);
			this.label3.TabIndex = 2;
			this.label3.Text = "Ctrl + Left / Right / Up / Down - Shift glyphs\r\nCtrl + Plus / Minus - Change font" +
    " size";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// OkButton
			// 
			this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.OkButton.Enabled = false;
			this.OkButton.Location = new System.Drawing.Point(624, 5);
			this.OkButton.Name = "OkButton";
			this.OkButton.Size = new System.Drawing.Size(100, 35);
			this.OkButton.TabIndex = 0;
			this.OkButton.Text = "Import";
			this.OkButton.UseVisualStyleBackColor = true;
			// 
			// CancelButton
			// 
			this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelButton.Location = new System.Drawing.Point(730, 5);
			this.CancelButton.Name = "CancelButton";
			this.CancelButton.Size = new System.Drawing.Size(100, 35);
			this.CancelButton.TabIndex = 1;
			this.CancelButton.Text = "Cancel";
			this.CancelButton.UseVisualStyleBackColor = true;
			// 
			// UpperAZButton
			// 
			this.UpperAZButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.UpperAZButton.Location = new System.Drawing.Point(790, 3);
			this.UpperAZButton.Name = "UpperAZButton";
			this.UpperAZButton.Size = new System.Drawing.Size(35, 23);
			this.UpperAZButton.TabIndex = 18;
			this.UpperAZButton.Text = "A-Z";
			this.MainToolTip.SetToolTip(this.UpperAZButton, "\'A\' to \'Z\' preset");
			this.UpperAZButton.UseVisualStyleBackColor = true;
			// 
			// LowerAZButton
			// 
			this.LowerAZButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.LowerAZButton.Location = new System.Drawing.Point(756, 3);
			this.LowerAZButton.Name = "LowerAZButton";
			this.LowerAZButton.Size = new System.Drawing.Size(35, 23);
			this.LowerAZButton.TabIndex = 17;
			this.LowerAZButton.Text = "a-z";
			this.MainToolTip.SetToolTip(this.LowerAZButton, "\'a\' to \'z\' preset");
			this.LowerAZButton.UseVisualStyleBackColor = true;
			// 
			// ZeroNineButton
			// 
			this.ZeroNineButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ZeroNineButton.Location = new System.Drawing.Point(722, 3);
			this.ZeroNineButton.Name = "ZeroNineButton";
			this.ZeroNineButton.Size = new System.Drawing.Size(35, 23);
			this.ZeroNineButton.TabIndex = 16;
			this.ZeroNineButton.Text = "0-9";
			this.MainToolTip.SetToolTip(this.ZeroNineButton, "\'0\' to \'9\' preset");
			this.ZeroNineButton.UseVisualStyleBackColor = true;
			// 
			// UnderlineButton
			// 
			this.UnderlineButton.Appearance = System.Windows.Forms.Appearance.Button;
			this.UnderlineButton.Image = global::NFirmwareEditor.Properties.Resources.font_underline;
			this.UnderlineButton.Location = new System.Drawing.Point(349, 3);
			this.UnderlineButton.Name = "UnderlineButton";
			this.UnderlineButton.Size = new System.Drawing.Size(24, 23);
			this.UnderlineButton.TabIndex = 15;
			this.UnderlineButton.TabStop = false;
			this.MainToolTip.SetToolTip(this.UnderlineButton, "Underline font style");
			this.UnderlineButton.UseVisualStyleBackColor = true;
			// 
			// ItalicButton
			// 
			this.ItalicButton.Appearance = System.Windows.Forms.Appearance.Button;
			this.ItalicButton.Image = global::NFirmwareEditor.Properties.Resources.font_italic;
			this.ItalicButton.Location = new System.Drawing.Point(326, 3);
			this.ItalicButton.Name = "ItalicButton";
			this.ItalicButton.Size = new System.Drawing.Size(24, 23);
			this.ItalicButton.TabIndex = 14;
			this.ItalicButton.TabStop = false;
			this.MainToolTip.SetToolTip(this.ItalicButton, "Italic font style");
			this.ItalicButton.UseVisualStyleBackColor = true;
			// 
			// BoldButton
			// 
			this.BoldButton.Appearance = System.Windows.Forms.Appearance.Button;
			this.BoldButton.Image = global::NFirmwareEditor.Properties.Resources.font_bold;
			this.BoldButton.Location = new System.Drawing.Point(303, 3);
			this.BoldButton.Name = "BoldButton";
			this.BoldButton.Size = new System.Drawing.Size(24, 23);
			this.BoldButton.TabIndex = 13;
			this.BoldButton.TabStop = false;
			this.MainToolTip.SetToolTip(this.BoldButton, "Bold font style");
			this.BoldButton.UseVisualStyleBackColor = true;
			// 
			// LoadFontButton
			// 
			this.LoadFontButton.Image = global::NFirmwareEditor.Properties.Resources.font_load;
			this.LoadFontButton.Location = new System.Drawing.Point(273, 3);
			this.LoadFontButton.Name = "LoadFontButton";
			this.LoadFontButton.Size = new System.Drawing.Size(24, 23);
			this.LoadFontButton.TabIndex = 12;
			this.LoadFontButton.TabStop = false;
			this.LoadFontButton.UseVisualStyleBackColor = true;
			// 
			// ShiftDownButton
			// 
			this.ShiftDownButton.Image = global::NFirmwareEditor.Properties.Resources.arrow_down;
			this.ShiftDownButton.Location = new System.Drawing.Point(448, 3);
			this.ShiftDownButton.Name = "ShiftDownButton";
			this.ShiftDownButton.Size = new System.Drawing.Size(24, 23);
			this.ShiftDownButton.TabIndex = 7;
			this.ShiftDownButton.TabStop = false;
			this.MainToolTip.SetToolTip(this.ShiftDownButton, "Shift down");
			this.ShiftDownButton.UseVisualStyleBackColor = true;
			// 
			// ShiftUpButton
			// 
			this.ShiftUpButton.Image = global::NFirmwareEditor.Properties.Resources.arrow_up;
			this.ShiftUpButton.Location = new System.Drawing.Point(425, 3);
			this.ShiftUpButton.Name = "ShiftUpButton";
			this.ShiftUpButton.Size = new System.Drawing.Size(24, 23);
			this.ShiftUpButton.TabIndex = 6;
			this.ShiftUpButton.TabStop = false;
			this.MainToolTip.SetToolTip(this.ShiftUpButton, "Shift up");
			this.ShiftUpButton.UseVisualStyleBackColor = true;
			// 
			// ShiftRightButton
			// 
			this.ShiftRightButton.Image = global::NFirmwareEditor.Properties.Resources.arrow_right;
			this.ShiftRightButton.Location = new System.Drawing.Point(402, 3);
			this.ShiftRightButton.Name = "ShiftRightButton";
			this.ShiftRightButton.Size = new System.Drawing.Size(24, 23);
			this.ShiftRightButton.TabIndex = 5;
			this.ShiftRightButton.TabStop = false;
			this.MainToolTip.SetToolTip(this.ShiftRightButton, "Shift right");
			this.ShiftRightButton.UseVisualStyleBackColor = true;
			// 
			// ShiftLeftButton
			// 
			this.ShiftLeftButton.Image = global::NFirmwareEditor.Properties.Resources.arrow_left;
			this.ShiftLeftButton.Location = new System.Drawing.Point(379, 3);
			this.ShiftLeftButton.Name = "ShiftLeftButton";
			this.ShiftLeftButton.Size = new System.Drawing.Size(24, 23);
			this.ShiftLeftButton.TabIndex = 4;
			this.ShiftLeftButton.TabStop = false;
			this.MainToolTip.SetToolTip(this.ShiftLeftButton, "Shift left");
			this.ShiftLeftButton.UseVisualStyleBackColor = true;
			// 
			// FontSizeUpDown
			// 
			this.FontSizeUpDown.Location = new System.Drawing.Point(56, 4);
			this.FontSizeUpDown.Maximum = new decimal(new int[] {
            72,
            0,
            0,
            0});
			this.FontSizeUpDown.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
			this.FontSizeUpDown.Name = "FontSizeUpDown";
			this.FontSizeUpDown.Size = new System.Drawing.Size(42, 21);
			this.FontSizeUpDown.TabIndex = 1;
			this.FontSizeUpDown.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
			// 
			// LettersTextBox
			// 
			this.LettersTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.LettersTextBox.Location = new System.Drawing.Point(515, 4);
			this.LettersTextBox.Name = "LettersTextBox";
			this.LettersTextBox.Size = new System.Drawing.Size(207, 21);
			this.LettersTextBox.TabIndex = 0;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(472, 7);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(45, 13);
			this.label4.TabIndex = 11;
			this.label4.Text = "Letters:";
			// 
			// FontComboBox
			// 
			this.FontComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.FontComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.FontComboBox.FormattingEnabled = true;
			this.FontComboBox.Location = new System.Drawing.Point(133, 4);
			this.FontComboBox.Name = "FontComboBox";
			this.FontComboBox.Size = new System.Drawing.Size(140, 21);
			this.FontComboBox.TabIndex = 2;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(102, 6);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(33, 13);
			this.label2.TabIndex = 8;
			this.label2.Text = "Font:";
			// 
			// FontPreviewSurface
			// 
			this.FontPreviewSurface.AutoScroll = true;
			this.FontPreviewSurface.BackgroundImage = global::NFirmwareEditor.Properties.Resources.transparent_bg;
			this.FontPreviewSurface.Dock = System.Windows.Forms.DockStyle.Fill;
			this.FontPreviewSurface.Location = new System.Drawing.Point(1, 29);
			this.FontPreviewSurface.Name = "FontPreviewSurface";
			this.FontPreviewSurface.Size = new System.Drawing.Size(826, 281);
			this.FontPreviewSurface.TabIndex = 0;
			this.FontPreviewSurface.Text = "drawingSurface1";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(4, 7);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(54, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Font size:";
			// 
			// groupPanel1
			// 
			this.groupPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupPanel1.BackColor = System.Drawing.Color.White;
			this.groupPanel1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(185)))), ((int)(((byte)(185)))));
			this.groupPanel1.Controls.Add(this.LettersTextBox);
			this.groupPanel1.Controls.Add(this.FontComboBox);
			this.groupPanel1.Controls.Add(this.FontSizeUpDown);
			this.groupPanel1.Controls.Add(this.UpperAZButton);
			this.groupPanel1.Controls.Add(this.FontPreviewSurface);
			this.groupPanel1.Controls.Add(this.LowerAZButton);
			this.groupPanel1.Controls.Add(this.label1);
			this.groupPanel1.Controls.Add(this.ZeroNineButton);
			this.groupPanel1.Controls.Add(this.label2);
			this.groupPanel1.Controls.Add(this.UnderlineButton);
			this.groupPanel1.Controls.Add(this.ItalicButton);
			this.groupPanel1.Controls.Add(this.label4);
			this.groupPanel1.Controls.Add(this.BoldButton);
			this.groupPanel1.Controls.Add(this.LoadFontButton);
			this.groupPanel1.Controls.Add(this.ShiftDownButton);
			this.groupPanel1.Controls.Add(this.ShiftLeftButton);
			this.groupPanel1.Controls.Add(this.ShiftUpButton);
			this.groupPanel1.Controls.Add(this.ShiftRightButton);
			this.groupPanel1.HeaderBackColor = System.Drawing.Color.White;
			this.groupPanel1.HeaderHeight = 29;
			this.groupPanel1.Location = new System.Drawing.Point(3, 3);
			this.groupPanel1.Name = "groupPanel1";
			this.groupPanel1.Padding = new System.Windows.Forms.Padding(1, 0, 1, 1);
			this.groupPanel1.Size = new System.Drawing.Size(828, 311);
			this.groupPanel1.TabIndex = 4;
			this.groupPanel1.TabStop = false;
			// 
			// ImportFontWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(834, 361);
			this.Controls.Add(this.groupPanel1);
			this.Controls.Add(this.ControlBorderedPanel);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(700, 300);
			this.Name = "ImportFontWindow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "NFirmwareEditor - Import Font";
			this.ControlBorderedPanel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.FontSizeUpDown)).EndInit();
			this.groupPanel1.ResumeLayout(false);
			this.groupPanel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private NCore.UI.BorderedPanel ControlBorderedPanel;
		private System.Windows.Forms.Button OkButton;
		private System.Windows.Forms.Button CancelButton;
		private System.Windows.Forms.Button ShiftDownButton;
		private System.Windows.Forms.Button ShiftUpButton;
		private System.Windows.Forms.Button ShiftRightButton;
		private System.Windows.Forms.Button ShiftLeftButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown FontSizeUpDown;
		private System.Windows.Forms.ComboBox FontComboBox;
		private System.Windows.Forms.Label label2;
		private NCore.UI.DrawingSurface FontPreviewSurface;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox LettersTextBox;
		private System.Windows.Forms.Button LoadFontButton;
		private System.Windows.Forms.CheckBox UnderlineButton;
		private System.Windows.Forms.CheckBox ItalicButton;
		private System.Windows.Forms.CheckBox BoldButton;
		private System.Windows.Forms.Button UpperAZButton;
		private System.Windows.Forms.Button LowerAZButton;
		private System.Windows.Forms.Button ZeroNineButton;
		private System.Windows.Forms.ToolTip MainToolTip;
		private System.Windows.Forms.Label label3;
		private NCore.UI.GroupPanel groupPanel1;
	}
}