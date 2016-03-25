namespace NFirmwareEditor.Windows
{
	partial class ImportImageWindow
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
			this.AfterLabel = new System.Windows.Forms.Label();
			this.BeforeLabel = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.borderedPanel1 = new NFirmwareEditor.UI.BorderedPanel();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.RightLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.LeftLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.ControlBorderedPanel = new NFirmwareEditor.UI.BorderedPanel();
			this.ResizeCheckBox = new System.Windows.Forms.CheckBox();
			this.ImportButton = new System.Windows.Forms.Button();
			this.CancelButton = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.borderedPanel1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.ControlBorderedPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// AfterLabel
			// 
			this.AfterLabel.Location = new System.Drawing.Point(218, 17);
			this.AfterLabel.Name = "AfterLabel";
			this.AfterLabel.Size = new System.Drawing.Size(205, 26);
			this.AfterLabel.TabIndex = 8;
			this.AfterLabel.Text = "After:";
			this.AfterLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// BeforeLabel
			// 
			this.BeforeLabel.Location = new System.Drawing.Point(16, 17);
			this.BeforeLabel.Name = "BeforeLabel";
			this.BeforeLabel.Size = new System.Drawing.Size(199, 26);
			this.BeforeLabel.TabIndex = 7;
			this.BeforeLabel.Text = "Before:";
			this.BeforeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.BeforeLabel);
			this.groupBox1.Controls.Add(this.borderedPanel1);
			this.groupBox1.Controls.Add(this.AfterLabel);
			this.groupBox1.Location = new System.Drawing.Point(3, 1);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(428, 418);
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
			this.borderedPanel1.Location = new System.Drawing.Point(6, 45);
			this.borderedPanel1.Name = "borderedPanel1";
			this.borderedPanel1.Padding = new System.Windows.Forms.Padding(1);
			this.borderedPanel1.Size = new System.Drawing.Size(416, 366);
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
			this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 6);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(404, 328);
			this.tableLayoutPanel1.TabIndex = 6;
			// 
			// RightLayoutPanel
			// 
			this.RightLayoutPanel.AutoSize = true;
			this.RightLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.RightLayoutPanel.Location = new System.Drawing.Point(205, 3);
			this.RightLayoutPanel.Name = "RightLayoutPanel";
			this.RightLayoutPanel.Size = new System.Drawing.Size(0, 0);
			this.RightLayoutPanel.TabIndex = 5;
			// 
			// LeftLayoutPanel
			// 
			this.LeftLayoutPanel.AutoSize = true;
			this.LeftLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.LeftLayoutPanel.Location = new System.Drawing.Point(3, 3);
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
			this.ControlBorderedPanel.Controls.Add(this.ResizeCheckBox);
			this.ControlBorderedPanel.Controls.Add(this.ImportButton);
			this.ControlBorderedPanel.Controls.Add(this.CancelButton);
			this.ControlBorderedPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.ControlBorderedPanel.Location = new System.Drawing.Point(0, 422);
			this.ControlBorderedPanel.Name = "ControlBorderedPanel";
			this.ControlBorderedPanel.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
			this.ControlBorderedPanel.Size = new System.Drawing.Size(434, 39);
			this.ControlBorderedPanel.TabIndex = 0;
			this.ControlBorderedPanel.Text = "borderedPanel1";
			// 
			// ResizeCheckBox
			// 
			this.ResizeCheckBox.Location = new System.Drawing.Point(5, 5);
			this.ResizeCheckBox.Name = "ResizeCheckBox";
			this.ResizeCheckBox.Size = new System.Drawing.Size(215, 30);
			this.ResizeCheckBox.TabIndex = 2;
			this.ResizeCheckBox.Text = "Resize original images";
			this.ResizeCheckBox.UseVisualStyleBackColor = true;
			// 
			// ImportButton
			// 
			this.ImportButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.ImportButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.ImportButton.Location = new System.Drawing.Point(224, 5);
			this.ImportButton.Name = "ImportButton";
			this.ImportButton.Size = new System.Drawing.Size(100, 30);
			this.ImportButton.TabIndex = 0;
			this.ImportButton.Text = "Import / Paste";
			this.ImportButton.UseVisualStyleBackColor = true;
			// 
			// CancelButton
			// 
			this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelButton.Location = new System.Drawing.Point(330, 5);
			this.CancelButton.Name = "CancelButton";
			this.CancelButton.Size = new System.Drawing.Size(100, 30);
			this.CancelButton.TabIndex = 1;
			this.CancelButton.Text = "Cancel";
			this.CancelButton.UseVisualStyleBackColor = true;
			// 
			// ImportImageWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(434, 461);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.ControlBorderedPanel);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ImportImageWindow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Import / Paste";
			this.groupBox1.ResumeLayout(false);
			this.borderedPanel1.ResumeLayout(false);
			this.borderedPanel1.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ControlBorderedPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.FlowLayoutPanel LeftLayoutPanel;
		private System.Windows.Forms.FlowLayoutPanel RightLayoutPanel;
		private UI.BorderedPanel borderedPanel1;
		private System.Windows.Forms.Button CancelButton;
		private System.Windows.Forms.Button ImportButton;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label BeforeLabel;
		private System.Windows.Forms.Label AfterLabel;
		private UI.BorderedPanel ControlBorderedPanel;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox ResizeCheckBox;
	}
}