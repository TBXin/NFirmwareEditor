namespace NFirmwareEditor.Windows.Tabs
{
	partial class PatchesTabPage
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
			this.DescriptionGroupBox = new System.Windows.Forms.GroupBox();
			this.DescriptionTextBox = new System.Windows.Forms.TextBox();
			this.ApplyPatchesButton = new System.Windows.Forms.Button();
			this.RollbackPatchesButton = new System.Windows.Forms.Button();
			this.ReloadPatchesButton = new System.Windows.Forms.Button();
			this.ConflictsGroupBox = new System.Windows.Forms.GroupBox();
			this.ConflictsTextBox = new System.Windows.Forms.TextBox();
			this.borderedPanel1 = new NFirmwareEditor.UI.BorderedPanel();
			this.PatchListView = new System.Windows.Forms.ListView();
			this.NameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.VersionColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.InstalledColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.CompatibleColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.DescriptionGroupBox.SuspendLayout();
			this.ConflictsGroupBox.SuspendLayout();
			this.borderedPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// DescriptionGroupBox
			// 
			this.DescriptionGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.DescriptionGroupBox.Controls.Add(this.DescriptionTextBox);
			this.DescriptionGroupBox.Location = new System.Drawing.Point(0, 326);
			this.DescriptionGroupBox.Name = "DescriptionGroupBox";
			this.DescriptionGroupBox.Size = new System.Drawing.Size(525, 125);
			this.DescriptionGroupBox.TabIndex = 1;
			this.DescriptionGroupBox.TabStop = false;
			this.DescriptionGroupBox.Text = "Description:";
			// 
			// DescriptionTextBox
			// 
			this.DescriptionTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.DescriptionTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DescriptionTextBox.Location = new System.Drawing.Point(3, 17);
			this.DescriptionTextBox.Multiline = true;
			this.DescriptionTextBox.Name = "DescriptionTextBox";
			this.DescriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.DescriptionTextBox.Size = new System.Drawing.Size(519, 105);
			this.DescriptionTextBox.TabIndex = 0;
			// 
			// ApplyPatchesButton
			// 
			this.ApplyPatchesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.ApplyPatchesButton.Enabled = false;
			this.ApplyPatchesButton.Location = new System.Drawing.Point(-1, 452);
			this.ApplyPatchesButton.Name = "ApplyPatchesButton";
			this.ApplyPatchesButton.Size = new System.Drawing.Size(100, 30);
			this.ApplyPatchesButton.TabIndex = 3;
			this.ApplyPatchesButton.Text = "Apply patches";
			this.ApplyPatchesButton.UseVisualStyleBackColor = true;
			// 
			// RollbackPatchesButton
			// 
			this.RollbackPatchesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.RollbackPatchesButton.Enabled = false;
			this.RollbackPatchesButton.Location = new System.Drawing.Point(100, 452);
			this.RollbackPatchesButton.Name = "RollbackPatchesButton";
			this.RollbackPatchesButton.Size = new System.Drawing.Size(100, 30);
			this.RollbackPatchesButton.TabIndex = 4;
			this.RollbackPatchesButton.Text = "Rollback patches";
			this.RollbackPatchesButton.UseVisualStyleBackColor = true;
			// 
			// ReloadPatchesButton
			// 
			this.ReloadPatchesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.ReloadPatchesButton.Enabled = false;
			this.ReloadPatchesButton.Location = new System.Drawing.Point(671, 452);
			this.ReloadPatchesButton.Name = "ReloadPatchesButton";
			this.ReloadPatchesButton.Size = new System.Drawing.Size(100, 30);
			this.ReloadPatchesButton.TabIndex = 5;
			this.ReloadPatchesButton.Text = "Reload patches";
			this.ReloadPatchesButton.UseVisualStyleBackColor = true;
			// 
			// ConflictsGroupBox
			// 
			this.ConflictsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.ConflictsGroupBox.Controls.Add(this.ConflictsTextBox);
			this.ConflictsGroupBox.Location = new System.Drawing.Point(528, 326);
			this.ConflictsGroupBox.Name = "ConflictsGroupBox";
			this.ConflictsGroupBox.Size = new System.Drawing.Size(242, 125);
			this.ConflictsGroupBox.TabIndex = 2;
			this.ConflictsGroupBox.TabStop = false;
			this.ConflictsGroupBox.Text = "Incompatibility with patches:";
			// 
			// ConflictsTextBox
			// 
			this.ConflictsTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.ConflictsTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ConflictsTextBox.Location = new System.Drawing.Point(3, 17);
			this.ConflictsTextBox.Multiline = true;
			this.ConflictsTextBox.Name = "ConflictsTextBox";
			this.ConflictsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.ConflictsTextBox.Size = new System.Drawing.Size(236, 105);
			this.ConflictsTextBox.TabIndex = 0;
			// 
			// borderedPanel1
			// 
			this.borderedPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.borderedPanel1.BackColor = System.Drawing.Color.Transparent;
			this.borderedPanel1.BorderBottom = true;
			this.borderedPanel1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(185)))), ((int)(((byte)(185)))));
			this.borderedPanel1.BorderLeft = true;
			this.borderedPanel1.BorderRight = true;
			this.borderedPanel1.BorderTop = true;
			this.borderedPanel1.BorderWidth = 1F;
			this.borderedPanel1.Controls.Add(this.PatchListView);
			this.borderedPanel1.Location = new System.Drawing.Point(0, 2);
			this.borderedPanel1.Name = "borderedPanel1";
			this.borderedPanel1.Padding = new System.Windows.Forms.Padding(1);
			this.borderedPanel1.Size = new System.Drawing.Size(770, 318);
			this.borderedPanel1.TabIndex = 2;
			// 
			// PatchListView
			// 
			this.PatchListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.PatchListView.CheckBoxes = true;
			this.PatchListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.NameColumnHeader,
            this.VersionColumnHeader,
            this.InstalledColumnHeader,
            this.CompatibleColumnHeader});
			this.PatchListView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PatchListView.FullRowSelect = true;
			this.PatchListView.GridLines = true;
			this.PatchListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.PatchListView.Location = new System.Drawing.Point(1, 1);
			this.PatchListView.MultiSelect = false;
			this.PatchListView.Name = "PatchListView";
			this.PatchListView.Size = new System.Drawing.Size(768, 316);
			this.PatchListView.TabIndex = 0;
			this.PatchListView.UseCompatibleStateImageBehavior = false;
			this.PatchListView.View = System.Windows.Forms.View.Details;
			// 
			// NameColumnHeader
			// 
			this.NameColumnHeader.Text = "Name";
			this.NameColumnHeader.Width = 127;
			// 
			// VersionColumnHeader
			// 
			this.VersionColumnHeader.Text = "Version";
			this.VersionColumnHeader.Width = 55;
			// 
			// InstalledColumnHeader
			// 
			this.InstalledColumnHeader.Text = "Installed";
			this.InstalledColumnHeader.Width = 55;
			// 
			// CompatibleColumnHeader
			// 
			this.CompatibleColumnHeader.Text = "Compatible";
			this.CompatibleColumnHeader.Width = 70;
			// 
			// PatchesTabPage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.ConflictsGroupBox);
			this.Controls.Add(this.ReloadPatchesButton);
			this.Controls.Add(this.RollbackPatchesButton);
			this.Controls.Add(this.ApplyPatchesButton);
			this.Controls.Add(this.borderedPanel1);
			this.Controls.Add(this.DescriptionGroupBox);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "PatchesTabPage";
			this.Size = new System.Drawing.Size(772, 482);
			this.DescriptionGroupBox.ResumeLayout(false);
			this.DescriptionGroupBox.PerformLayout();
			this.ConflictsGroupBox.ResumeLayout(false);
			this.ConflictsGroupBox.PerformLayout();
			this.borderedPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.ColumnHeader NameColumnHeader;
		private System.Windows.Forms.ColumnHeader VersionColumnHeader;
		private System.Windows.Forms.ColumnHeader InstalledColumnHeader;
		private System.Windows.Forms.GroupBox DescriptionGroupBox;
		private UI.BorderedPanel borderedPanel1;
		private System.Windows.Forms.ListView PatchListView;
		private System.Windows.Forms.Button ApplyPatchesButton;
		private System.Windows.Forms.Button RollbackPatchesButton;
		private System.Windows.Forms.TextBox DescriptionTextBox;
		private System.Windows.Forms.ColumnHeader CompatibleColumnHeader;
		private System.Windows.Forms.Button ReloadPatchesButton;
		private System.Windows.Forms.GroupBox ConflictsGroupBox;
		private System.Windows.Forms.TextBox ConflictsTextBox;
	}
}
