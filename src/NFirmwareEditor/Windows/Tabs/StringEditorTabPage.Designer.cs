namespace NFirmwareEditor.Windows.Tabs
{
	partial class StringEditorTabPage
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
			this.Block1StringRadioButton = new System.Windows.Forms.RadioButton();
			this.Block2StringRadioButton = new System.Windows.Forms.RadioButton();
			this.StringPreviewGroupPanel = new NFirmwareEditor.UI.GroupPanel();
			this.BlockTypeComboBox = new System.Windows.Forms.ComboBox();
			this.StringPrewviewPixelGrid = new NFirmwareEditor.UI.PixelGrid();
			this.StringEditGroupPanel = new NFirmwareEditor.UI.GroupPanel();
			this.borderedPanel10 = new NFirmwareEditor.UI.BorderedPanel();
			this.CharLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.StringStatusStrip = new System.Windows.Forms.StatusStrip();
			this.StringStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.borderedPanel6 = new NFirmwareEditor.UI.BorderedPanel();
			this.Block1StringListBox = new System.Windows.Forms.ListBox();
			this.Block2StringListBox = new System.Windows.Forms.ListBox();
			this.StringPreviewGroupPanel.SuspendLayout();
			this.StringEditGroupPanel.SuspendLayout();
			this.borderedPanel10.SuspendLayout();
			this.StringStatusStrip.SuspendLayout();
			this.borderedPanel6.SuspendLayout();
			this.SuspendLayout();
			// 
			// Block1StringRadioButton
			// 
			this.Block1StringRadioButton.Appearance = System.Windows.Forms.Appearance.Button;
			this.Block1StringRadioButton.Enabled = false;
			this.Block1StringRadioButton.Location = new System.Drawing.Point(-1, 1);
			this.Block1StringRadioButton.Name = "Block1StringRadioButton";
			this.Block1StringRadioButton.Size = new System.Drawing.Size(71, 30);
			this.Block1StringRadioButton.TabIndex = 11;
			this.Block1StringRadioButton.Text = "Block 1";
			this.Block1StringRadioButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.Block1StringRadioButton.UseVisualStyleBackColor = true;
			// 
			// Block2StringRadioButton
			// 
			this.Block2StringRadioButton.Appearance = System.Windows.Forms.Appearance.Button;
			this.Block2StringRadioButton.Enabled = false;
			this.Block2StringRadioButton.Location = new System.Drawing.Point(70, 1);
			this.Block2StringRadioButton.Name = "Block2StringRadioButton";
			this.Block2StringRadioButton.Size = new System.Drawing.Size(71, 30);
			this.Block2StringRadioButton.TabIndex = 12;
			this.Block2StringRadioButton.Text = "Block 2";
			this.Block2StringRadioButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.Block2StringRadioButton.UseVisualStyleBackColor = true;
			// 
			// StringPreviewGroupPanel
			// 
			this.StringPreviewGroupPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.StringPreviewGroupPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(185)))), ((int)(((byte)(185)))));
			this.StringPreviewGroupPanel.Controls.Add(this.BlockTypeComboBox);
			this.StringPreviewGroupPanel.Controls.Add(this.StringPrewviewPixelGrid);
			this.StringPreviewGroupPanel.HeaderBackColor = System.Drawing.Color.White;
			this.StringPreviewGroupPanel.HeaderHeight = 30;
			this.StringPreviewGroupPanel.Location = new System.Drawing.Point(143, 2);
			this.StringPreviewGroupPanel.Name = "StringPreviewGroupPanel";
			this.StringPreviewGroupPanel.Size = new System.Drawing.Size(627, 118);
			this.StringPreviewGroupPanel.TabIndex = 2;
			this.StringPreviewGroupPanel.TabStop = false;
			this.StringPreviewGroupPanel.Text = "Preview using:";
			// 
			// BlockTypeComboBox
			// 
			this.BlockTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.BlockTypeComboBox.FormattingEnabled = true;
			this.BlockTypeComboBox.Location = new System.Drawing.Point(80, 5);
			this.BlockTypeComboBox.Name = "BlockTypeComboBox";
			this.BlockTypeComboBox.Size = new System.Drawing.Size(92, 21);
			this.BlockTypeComboBox.TabIndex = 7;
			// 
			// StringPrewviewPixelGrid
			// 
			this.StringPrewviewPixelGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.StringPrewviewPixelGrid.AutoScroll = true;
			this.StringPrewviewPixelGrid.AutoScrollMinSize = new System.Drawing.Size(6, 6);
			this.StringPrewviewPixelGrid.BackColor = System.Drawing.Color.Black;
			this.StringPrewviewPixelGrid.BlockSize = 2;
			this.StringPrewviewPixelGrid.Location = new System.Drawing.Point(3, 32);
			this.StringPrewviewPixelGrid.Margin = new System.Windows.Forms.Padding(2);
			this.StringPrewviewPixelGrid.Name = "StringPrewviewPixelGrid";
			this.StringPrewviewPixelGrid.ReadOnly = true;
			this.StringPrewviewPixelGrid.ShowGrid = false;
			this.StringPrewviewPixelGrid.SingleMouseButtonMode = false;
			this.StringPrewviewPixelGrid.Size = new System.Drawing.Size(621, 83);
			this.StringPrewviewPixelGrid.TabIndex = 3;
			this.StringPrewviewPixelGrid.Text = "pixelGrid1";
			// 
			// StringEditGroupPanel
			// 
			this.StringEditGroupPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.StringEditGroupPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(185)))), ((int)(((byte)(185)))));
			this.StringEditGroupPanel.Controls.Add(this.borderedPanel10);
			this.StringEditGroupPanel.Controls.Add(this.StringStatusStrip);
			this.StringEditGroupPanel.HeaderBackColor = System.Drawing.Color.White;
			this.StringEditGroupPanel.HeaderHeight = 30;
			this.StringEditGroupPanel.Location = new System.Drawing.Point(143, 123);
			this.StringEditGroupPanel.Name = "StringEditGroupPanel";
			this.StringEditGroupPanel.Size = new System.Drawing.Size(627, 358);
			this.StringEditGroupPanel.TabIndex = 16;
			this.StringEditGroupPanel.TabStop = false;
			this.StringEditGroupPanel.Text = "Edit:";
			// 
			// borderedPanel10
			// 
			this.borderedPanel10.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.borderedPanel10.AutoScroll = true;
			this.borderedPanel10.BackColor = System.Drawing.Color.Transparent;
			this.borderedPanel10.BorderBottom = false;
			this.borderedPanel10.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(185)))), ((int)(((byte)(185)))));
			this.borderedPanel10.BorderLeft = false;
			this.borderedPanel10.BorderRight = false;
			this.borderedPanel10.BorderTop = false;
			this.borderedPanel10.BorderWidth = 1F;
			this.borderedPanel10.Controls.Add(this.CharLayoutPanel);
			this.borderedPanel10.Location = new System.Drawing.Point(3, 33);
			this.borderedPanel10.Name = "borderedPanel10";
			this.borderedPanel10.Size = new System.Drawing.Size(620, 297);
			this.borderedPanel10.TabIndex = 3;
			this.borderedPanel10.Text = "borderedPanel10";
			// 
			// CharLayoutPanel
			// 
			this.CharLayoutPanel.AutoSize = true;
			this.CharLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.CharLayoutPanel.Location = new System.Drawing.Point(6, 5);
			this.CharLayoutPanel.Name = "CharLayoutPanel";
			this.CharLayoutPanel.Size = new System.Drawing.Size(155, 84);
			this.CharLayoutPanel.TabIndex = 1;
			// 
			// StringStatusStrip
			// 
			this.StringStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StringStatusLabel});
			this.StringStatusStrip.Location = new System.Drawing.Point(3, 333);
			this.StringStatusStrip.Name = "StringStatusStrip";
			this.StringStatusStrip.Size = new System.Drawing.Size(621, 22);
			this.StringStatusStrip.TabIndex = 2;
			this.StringStatusStrip.Text = "statusStrip1";
			// 
			// StringStatusLabel
			// 
			this.StringStatusLabel.BackColor = System.Drawing.SystemColors.Control;
			this.StringStatusLabel.Font = new System.Drawing.Font("Consolas", 9.75F);
			this.StringStatusLabel.Name = "StringStatusLabel";
			this.StringStatusLabel.Size = new System.Drawing.Size(126, 17);
			this.StringStatusLabel.Text = "StringStatusLabel";
			// 
			// borderedPanel6
			// 
			this.borderedPanel6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.borderedPanel6.BackColor = System.Drawing.Color.Transparent;
			this.borderedPanel6.BorderBottom = true;
			this.borderedPanel6.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(185)))), ((int)(((byte)(185)))));
			this.borderedPanel6.BorderLeft = true;
			this.borderedPanel6.BorderRight = true;
			this.borderedPanel6.BorderTop = true;
			this.borderedPanel6.BorderWidth = 1F;
			this.borderedPanel6.Controls.Add(this.Block1StringListBox);
			this.borderedPanel6.Controls.Add(this.Block2StringListBox);
			this.borderedPanel6.Location = new System.Drawing.Point(0, 33);
			this.borderedPanel6.Name = "borderedPanel6";
			this.borderedPanel6.Padding = new System.Windows.Forms.Padding(1);
			this.borderedPanel6.Size = new System.Drawing.Size(140, 448);
			this.borderedPanel6.TabIndex = 13;
			this.borderedPanel6.Text = "borderedPanel6";
			// 
			// Block1StringListBox
			// 
			this.Block1StringListBox.BackColor = System.Drawing.Color.Black;
			this.Block1StringListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.Block1StringListBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Block1StringListBox.Font = new System.Drawing.Font("Consolas", 8.25F);
			this.Block1StringListBox.ForeColor = System.Drawing.Color.White;
			this.Block1StringListBox.FormattingEnabled = true;
			this.Block1StringListBox.IntegralHeight = false;
			this.Block1StringListBox.Location = new System.Drawing.Point(1, 1);
			this.Block1StringListBox.Name = "Block1StringListBox";
			this.Block1StringListBox.Size = new System.Drawing.Size(138, 446);
			this.Block1StringListBox.TabIndex = 0;
			// 
			// Block2StringListBox
			// 
			this.Block2StringListBox.BackColor = System.Drawing.Color.Black;
			this.Block2StringListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.Block2StringListBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Block2StringListBox.Font = new System.Drawing.Font("Consolas", 8.25F);
			this.Block2StringListBox.ForeColor = System.Drawing.Color.White;
			this.Block2StringListBox.FormattingEnabled = true;
			this.Block2StringListBox.IntegralHeight = false;
			this.Block2StringListBox.Location = new System.Drawing.Point(1, 1);
			this.Block2StringListBox.Name = "Block2StringListBox";
			this.Block2StringListBox.Size = new System.Drawing.Size(138, 446);
			this.Block2StringListBox.TabIndex = 0;
			// 
			// StringEditorTabPage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.StringPreviewGroupPanel);
			this.Controls.Add(this.StringEditGroupPanel);
			this.Controls.Add(this.Block1StringRadioButton);
			this.Controls.Add(this.Block2StringRadioButton);
			this.Controls.Add(this.borderedPanel6);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.Name = "StringEditorTabPage";
			this.Size = new System.Drawing.Size(772, 482);
			this.StringPreviewGroupPanel.ResumeLayout(false);
			this.StringEditGroupPanel.ResumeLayout(false);
			this.StringEditGroupPanel.PerformLayout();
			this.borderedPanel10.ResumeLayout(false);
			this.borderedPanel10.PerformLayout();
			this.StringStatusStrip.ResumeLayout(false);
			this.StringStatusStrip.PerformLayout();
			this.borderedPanel6.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private UI.BorderedPanel borderedPanel10;
		private System.Windows.Forms.FlowLayoutPanel CharLayoutPanel;
		private UI.PixelGrid StringPrewviewPixelGrid;
		private System.Windows.Forms.RadioButton Block1StringRadioButton;
		private System.Windows.Forms.RadioButton Block2StringRadioButton;
		private UI.BorderedPanel borderedPanel6;
		private System.Windows.Forms.ListBox Block1StringListBox;
		private System.Windows.Forms.ListBox Block2StringListBox;
		private System.Windows.Forms.ComboBox BlockTypeComboBox;
		private System.Windows.Forms.StatusStrip StringStatusStrip;
		private System.Windows.Forms.ToolStripStatusLabel StringStatusLabel;
		private UI.GroupPanel StringEditGroupPanel;
		private UI.GroupPanel StringPreviewGroupPanel;
	}
}
