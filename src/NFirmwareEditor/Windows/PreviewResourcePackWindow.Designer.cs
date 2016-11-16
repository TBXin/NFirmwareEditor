namespace NFirmwareEditor.Windows
{
	partial class PreviewResourcePackWindow
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
			this.groupBox1 = new NCore.UI.GroupPanel();
			this.borderedPanel1 = new NCore.UI.BorderedPanel();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.RightLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.LeftLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.ControlBorderedPanel = new NCore.UI.BorderedPanel();
			this.ImportButton = new System.Windows.Forms.Button();
			this.CancelButton = new System.Windows.Forms.Button();
			this.ResizeCheckBox = new System.Windows.Forms.CheckBox();
			this.OptionsGroupBox = new NCore.UI.GroupPanel();
			this.label3 = new System.Windows.Forms.Label();
			this.ImportModeComboBox = new System.Windows.Forms.ComboBox();
			this.groupBox1.SuspendLayout();
			this.borderedPanel1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.ControlBorderedPanel.SuspendLayout();
			this.OptionsGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(185)))), ((int)(((byte)(185)))));
			this.groupBox1.Controls.Add(this.borderedPanel1);
			this.groupBox1.HeaderBackColor = System.Drawing.Color.White;
			this.groupBox1.HeaderHeight = 30;
			this.groupBox1.Location = new System.Drawing.Point(3, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(303, 367);
			this.groupBox1.TabIndex = 9;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Preview:";
			// 
			// borderedPanel1
			// 
			this.borderedPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.borderedPanel1.AutoScroll = true;
			this.borderedPanel1.BackColor = System.Drawing.Color.Transparent;
			this.borderedPanel1.BorderBottom = true;
			this.borderedPanel1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(185)))), ((int)(((byte)(185)))));
			this.borderedPanel1.BorderLeft = true;
			this.borderedPanel1.BorderRight = true;
			this.borderedPanel1.BorderTop = true;
			this.borderedPanel1.BorderWidth = 1F;
			this.borderedPanel1.Controls.Add(this.tableLayoutPanel1);
			this.borderedPanel1.Location = new System.Drawing.Point(4, 33);
			this.borderedPanel1.Name = "borderedPanel1";
			this.borderedPanel1.Padding = new System.Windows.Forms.Padding(1);
			this.borderedPanel1.Size = new System.Drawing.Size(295, 330);
			this.borderedPanel1.TabIndex = 3;
			this.borderedPanel1.Text = "borderedPanel1";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.AutoScroll = true;
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.RightLayoutPanel, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.LeftLayoutPanel, 0, 0);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 0);
			this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(289, 328);
			this.tableLayoutPanel1.TabIndex = 6;
			// 
			// RightLayoutPanel
			// 
			this.RightLayoutPanel.AutoSize = true;
			this.RightLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.RightLayoutPanel.Location = new System.Drawing.Point(144, 0);
			this.RightLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
			this.RightLayoutPanel.Name = "RightLayoutPanel";
			this.RightLayoutPanel.Size = new System.Drawing.Size(0, 0);
			this.RightLayoutPanel.TabIndex = 5;
			// 
			// LeftLayoutPanel
			// 
			this.LeftLayoutPanel.AutoSize = true;
			this.LeftLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.LeftLayoutPanel.Location = new System.Drawing.Point(0, 0);
			this.LeftLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
			this.LeftLayoutPanel.Name = "LeftLayoutPanel";
			this.LeftLayoutPanel.Size = new System.Drawing.Size(0, 0);
			this.LeftLayoutPanel.TabIndex = 1;
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
			this.ControlBorderedPanel.Controls.Add(this.ImportButton);
			this.ControlBorderedPanel.Controls.Add(this.CancelButton);
			this.ControlBorderedPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.ControlBorderedPanel.Location = new System.Drawing.Point(0, 437);
			this.ControlBorderedPanel.Name = "ControlBorderedPanel";
			this.ControlBorderedPanel.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
			this.ControlBorderedPanel.Size = new System.Drawing.Size(309, 39);
			this.ControlBorderedPanel.TabIndex = 0;
			this.ControlBorderedPanel.Text = "borderedPanel1";
			// 
			// ImportButton
			// 
			this.ImportButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.ImportButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.ImportButton.Location = new System.Drawing.Point(99, 5);
			this.ImportButton.Name = "ImportButton";
			this.ImportButton.Size = new System.Drawing.Size(100, 30);
			this.ImportButton.TabIndex = 0;
			this.ImportButton.Text = "Import";
			this.ImportButton.UseVisualStyleBackColor = true;
			// 
			// CancelButton
			// 
			this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelButton.Location = new System.Drawing.Point(205, 5);
			this.CancelButton.Name = "CancelButton";
			this.CancelButton.Size = new System.Drawing.Size(100, 30);
			this.CancelButton.TabIndex = 1;
			this.CancelButton.Text = "Cancel";
			this.CancelButton.UseVisualStyleBackColor = true;
			// 
			// ResizeCheckBox
			// 
			this.ResizeCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ResizeCheckBox.AutoSize = true;
			this.ResizeCheckBox.Location = new System.Drawing.Point(163, 36);
			this.ResizeCheckBox.Name = "ResizeCheckBox";
			this.ResizeCheckBox.Size = new System.Drawing.Size(130, 17);
			this.ResizeCheckBox.TabIndex = 2;
			this.ResizeCheckBox.Text = "Resize original images";
			this.ResizeCheckBox.UseVisualStyleBackColor = true;
			// 
			// OptionsGroupBox
			// 
			this.OptionsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.OptionsGroupBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(185)))), ((int)(((byte)(185)))));
			this.OptionsGroupBox.Controls.Add(this.label3);
			this.OptionsGroupBox.Controls.Add(this.ImportModeComboBox);
			this.OptionsGroupBox.Controls.Add(this.ResizeCheckBox);
			this.OptionsGroupBox.HeaderBackColor = System.Drawing.Color.White;
			this.OptionsGroupBox.HeaderHeight = 30;
			this.OptionsGroupBox.Location = new System.Drawing.Point(3, 376);
			this.OptionsGroupBox.Name = "OptionsGroupBox";
			this.OptionsGroupBox.Size = new System.Drawing.Size(303, 58);
			this.OptionsGroupBox.TabIndex = 10;
			this.OptionsGroupBox.TabStop = false;
			this.OptionsGroupBox.Text = "Import options:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 36);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(56, 13);
			this.label3.TabIndex = 15;
			this.label3.Text = "Import to:";
			// 
			// ImportModeComboBox
			// 
			this.ImportModeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ImportModeComboBox.FormattingEnabled = true;
			this.ImportModeComboBox.Location = new System.Drawing.Point(65, 33);
			this.ImportModeComboBox.Name = "ImportModeComboBox";
			this.ImportModeComboBox.Size = new System.Drawing.Size(92, 21);
			this.ImportModeComboBox.TabIndex = 3;
			// 
			// PreviewResourcePackWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(309, 476);
			this.Controls.Add(this.OptionsGroupBox);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.ControlBorderedPanel);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PreviewResourcePackWindow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Import";
			this.groupBox1.ResumeLayout(false);
			this.borderedPanel1.ResumeLayout(false);
			this.borderedPanel1.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ControlBorderedPanel.ResumeLayout(false);
			this.OptionsGroupBox.ResumeLayout(false);
			this.OptionsGroupBox.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.FlowLayoutPanel LeftLayoutPanel;
		private System.Windows.Forms.FlowLayoutPanel RightLayoutPanel;
		private NCore.UI.BorderedPanel borderedPanel1;
		private System.Windows.Forms.Button CancelButton;
		private System.Windows.Forms.Button ImportButton;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private NCore.UI.BorderedPanel ControlBorderedPanel;
		private System.Windows.Forms.CheckBox ResizeCheckBox;
		private System.Windows.Forms.ComboBox ImportModeComboBox;
		private System.Windows.Forms.Label label3;
		private NCore.UI.GroupPanel groupBox1;
		private NCore.UI.GroupPanel OptionsGroupBox;
	}
}