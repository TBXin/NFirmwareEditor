namespace NFirmwareEditor.Windows
{
	partial class PatchCreatorWindow
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.CreateDiffLink = new System.Windows.Forms.LinkLabel();
			this.label8 = new System.Windows.Forms.Label();
			this.DataTextBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.SelectSource2Button = new System.Windows.Forms.Button();
			this.Source2TextBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.SelectSource1Button = new System.Windows.Forms.Button();
			this.Source1TextBox = new System.Windows.Forms.TextBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.DefinitionComboBox = new System.Windows.Forms.ComboBox();
			this.label7 = new System.Windows.Forms.Label();
			this.DescriptionTextBox = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.AuthorTextBox = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.VersionTextBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.NameTextBox = new System.Windows.Forms.TextBox();
			this.ControlBorderedPanel = new NFirmwareEditor.UI.BorderedPanel();
			this.OkButton = new System.Windows.Forms.Button();
			this.CancelButton = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.ControlBorderedPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.CreateDiffLink);
			this.groupBox1.Controls.Add(this.label8);
			this.groupBox1.Controls.Add(this.DataTextBox);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.SelectSource2Button);
			this.groupBox1.Controls.Add(this.Source2TextBox);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.SelectSource1Button);
			this.groupBox1.Controls.Add(this.Source1TextBox);
			this.groupBox1.Location = new System.Drawing.Point(3, 228);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(428, 338);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Patch data:";
			// 
			// CreateDiffLink
			// 
			this.CreateDiffLink.AutoSize = true;
			this.CreateDiffLink.Enabled = false;
			this.CreateDiffLink.Location = new System.Drawing.Point(61, 77);
			this.CreateDiffLink.Name = "CreateDiffLink";
			this.CreateDiffLink.Size = new System.Drawing.Size(41, 13);
			this.CreateDiffLink.TabIndex = 26;
			this.CreateDiffLink.TabStop = true;
			this.CreateDiffLink.Text = "update";
			this.CreateDiffLink.VisitedLinkColor = System.Drawing.Color.Blue;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(9, 77);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(55, 13);
			this.label8.TabIndex = 25;
			this.label8.Text = "Diffrence:";
			// 
			// DataTextBox
			// 
			this.DataTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.DataTextBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.DataTextBox.Location = new System.Drawing.Point(106, 74);
			this.DataTextBox.Multiline = true;
			this.DataTextBox.Name = "DataTextBox";
			this.DataTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.DataTextBox.Size = new System.Drawing.Size(317, 235);
			this.DataTextBox.TabIndex = 0;
			this.DataTextBox.TabStop = false;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(9, 50);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(94, 13);
			this.label2.TabIndex = 11;
			this.label2.Text = "Source (Patched):";
			// 
			// SelectSource2Button
			// 
			this.SelectSource2Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.SelectSource2Button.Location = new System.Drawing.Point(348, 47);
			this.SelectSource2Button.Name = "SelectSource2Button";
			this.SelectSource2Button.Size = new System.Drawing.Size(75, 21);
			this.SelectSource2Button.TabIndex = 6;
			this.SelectSource2Button.Text = "Select";
			this.SelectSource2Button.UseVisualStyleBackColor = true;
			// 
			// Source2TextBox
			// 
			this.Source2TextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.Source2TextBox.Location = new System.Drawing.Point(106, 47);
			this.Source2TextBox.Name = "Source2TextBox";
			this.Source2TextBox.ReadOnly = true;
			this.Source2TextBox.Size = new System.Drawing.Size(236, 21);
			this.Source2TextBox.TabIndex = 0;
			this.Source2TextBox.TabStop = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(9, 23);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(91, 13);
			this.label1.TabIndex = 8;
			this.label1.Text = "Source (Original):";
			// 
			// SelectSource1Button
			// 
			this.SelectSource1Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.SelectSource1Button.Location = new System.Drawing.Point(348, 20);
			this.SelectSource1Button.Name = "SelectSource1Button";
			this.SelectSource1Button.Size = new System.Drawing.Size(75, 21);
			this.SelectSource1Button.TabIndex = 5;
			this.SelectSource1Button.Text = "Select";
			this.SelectSource1Button.UseVisualStyleBackColor = true;
			// 
			// Source1TextBox
			// 
			this.Source1TextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.Source1TextBox.Location = new System.Drawing.Point(106, 20);
			this.Source1TextBox.Name = "Source1TextBox";
			this.Source1TextBox.ReadOnly = true;
			this.Source1TextBox.Size = new System.Drawing.Size(236, 21);
			this.Source1TextBox.TabIndex = 0;
			this.Source1TextBox.TabStop = false;
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.DefinitionComboBox);
			this.groupBox2.Controls.Add(this.label7);
			this.groupBox2.Controls.Add(this.DescriptionTextBox);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.AuthorTextBox);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.VersionTextBox);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.NameTextBox);
			this.groupBox2.Location = new System.Drawing.Point(3, 1);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(428, 221);
			this.groupBox2.TabIndex = 0;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Patch info:";
			// 
			// DefinitionComboBox
			// 
			this.DefinitionComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.DefinitionComboBox.FormattingEnabled = true;
			this.DefinitionComboBox.Location = new System.Drawing.Point(106, 101);
			this.DefinitionComboBox.Name = "DefinitionComboBox";
			this.DefinitionComboBox.Size = new System.Drawing.Size(317, 21);
			this.DefinitionComboBox.TabIndex = 3;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(9, 131);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(64, 13);
			this.label7.TabIndex = 22;
			this.label7.Text = "Description:";
			// 
			// DescriptionTextBox
			// 
			this.DescriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.DescriptionTextBox.Location = new System.Drawing.Point(106, 128);
			this.DescriptionTextBox.Multiline = true;
			this.DescriptionTextBox.Name = "DescriptionTextBox";
			this.DescriptionTextBox.Size = new System.Drawing.Size(317, 87);
			this.DescriptionTextBox.TabIndex = 4;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(9, 104);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(56, 13);
			this.label6.TabIndex = 20;
			this.label6.Text = "Definition:";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(9, 77);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(44, 13);
			this.label5.TabIndex = 18;
			this.label5.Text = "Author:";
			// 
			// AuthorTextBox
			// 
			this.AuthorTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.AuthorTextBox.Location = new System.Drawing.Point(106, 74);
			this.AuthorTextBox.Name = "AuthorTextBox";
			this.AuthorTextBox.Size = new System.Drawing.Size(317, 21);
			this.AuthorTextBox.TabIndex = 2;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(9, 50);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(46, 13);
			this.label4.TabIndex = 16;
			this.label4.Text = "Version:";
			// 
			// VersionTextBox
			// 
			this.VersionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.VersionTextBox.Location = new System.Drawing.Point(106, 47);
			this.VersionTextBox.Name = "VersionTextBox";
			this.VersionTextBox.Size = new System.Drawing.Size(317, 21);
			this.VersionTextBox.TabIndex = 1;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(9, 23);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(38, 13);
			this.label3.TabIndex = 14;
			this.label3.Text = "Name:";
			// 
			// NameTextBox
			// 
			this.NameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.NameTextBox.Location = new System.Drawing.Point(106, 20);
			this.NameTextBox.Name = "NameTextBox";
			this.NameTextBox.Size = new System.Drawing.Size(317, 21);
			this.NameTextBox.TabIndex = 0;
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
			this.ControlBorderedPanel.Controls.Add(this.OkButton);
			this.ControlBorderedPanel.Controls.Add(this.CancelButton);
			this.ControlBorderedPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.ControlBorderedPanel.Location = new System.Drawing.Point(0, 572);
			this.ControlBorderedPanel.Name = "ControlBorderedPanel";
			this.ControlBorderedPanel.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
			this.ControlBorderedPanel.Size = new System.Drawing.Size(434, 39);
			this.ControlBorderedPanel.TabIndex = 2;
			this.ControlBorderedPanel.Text = "borderedPanel1";
			// 
			// OkButton
			// 
			this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.OkButton.Enabled = false;
			this.OkButton.Location = new System.Drawing.Point(224, 5);
			this.OkButton.Name = "OkButton";
			this.OkButton.Size = new System.Drawing.Size(100, 30);
			this.OkButton.TabIndex = 7;
			this.OkButton.Text = "Save Patch";
			this.OkButton.UseVisualStyleBackColor = true;
			// 
			// CancelButton
			// 
			this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelButton.Location = new System.Drawing.Point(330, 5);
			this.CancelButton.Name = "CancelButton";
			this.CancelButton.Size = new System.Drawing.Size(100, 30);
			this.CancelButton.TabIndex = 8;
			this.CancelButton.Text = "Cancel";
			this.CancelButton.UseVisualStyleBackColor = true;
			// 
			// PatchCreatorWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(434, 611);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.ControlBorderedPanel);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PatchCreatorWindow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "NFirmwareEditor - Patch Creator";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ControlBorderedPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private UI.BorderedPanel ControlBorderedPanel;
		private System.Windows.Forms.Button OkButton;
		private System.Windows.Forms.Button CancelButton;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button SelectSource1Button;
		private System.Windows.Forms.TextBox Source1TextBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button SelectSource2Button;
		private System.Windows.Forms.TextBox Source2TextBox;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox NameTextBox;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox AuthorTextBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox VersionTextBox;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox DescriptionTextBox;
		private System.Windows.Forms.ComboBox DefinitionComboBox;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox DataTextBox;
		private System.Windows.Forms.LinkLabel CreateDiffLink;
	}
}