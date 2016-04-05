namespace NFirmwareEditor.Windows.Tabs
{
	partial class ResourcePacksTabPage
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
			this.ReloadResourcePacksButton = new System.Windows.Forms.Button();
			this.PreviewResourcePackButton = new System.Windows.Forms.Button();
			this.DescriptionGroupBox = new System.Windows.Forms.GroupBox();
			this.DescriptionTextBox = new System.Windows.Forms.TextBox();
			this.borderedPanel1 = new NFirmwareEditor.UI.BorderedPanel();
			this.ResourcePackListView = new System.Windows.Forms.ListView();
			this.NameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.VersionColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.DescriptionGroupBox.SuspendLayout();
			this.borderedPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// ReloadResourcePacksButton
			// 
			this.ReloadResourcePacksButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.ReloadResourcePacksButton.Enabled = false;
			this.ReloadResourcePacksButton.Location = new System.Drawing.Point(671, 452);
			this.ReloadResourcePacksButton.Name = "ReloadResourcePacksButton";
			this.ReloadResourcePacksButton.Size = new System.Drawing.Size(100, 30);
			this.ReloadResourcePacksButton.TabIndex = 8;
			this.ReloadResourcePacksButton.Text = "Reload";
			this.ReloadResourcePacksButton.UseVisualStyleBackColor = true;
			// 
			// PreviewResourcePackButton
			// 
			this.PreviewResourcePackButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.PreviewResourcePackButton.Enabled = false;
			this.PreviewResourcePackButton.Location = new System.Drawing.Point(-1, 452);
			this.PreviewResourcePackButton.Name = "PreviewResourcePackButton";
			this.PreviewResourcePackButton.Size = new System.Drawing.Size(201, 30);
			this.PreviewResourcePackButton.TabIndex = 6;
			this.PreviewResourcePackButton.Text = "Preview resource pack";
			this.PreviewResourcePackButton.UseVisualStyleBackColor = true;
			// 
			// DescriptionGroupBox
			// 
			this.DescriptionGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.DescriptionGroupBox.Controls.Add(this.DescriptionTextBox);
			this.DescriptionGroupBox.Location = new System.Drawing.Point(0, 326);
			this.DescriptionGroupBox.Name = "DescriptionGroupBox";
			this.DescriptionGroupBox.Size = new System.Drawing.Size(770, 125);
			this.DescriptionGroupBox.TabIndex = 9;
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
			this.DescriptionTextBox.Size = new System.Drawing.Size(764, 105);
			this.DescriptionTextBox.TabIndex = 0;
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
			this.borderedPanel1.Controls.Add(this.ResourcePackListView);
			this.borderedPanel1.Location = new System.Drawing.Point(0, 2);
			this.borderedPanel1.Name = "borderedPanel1";
			this.borderedPanel1.Padding = new System.Windows.Forms.Padding(1);
			this.borderedPanel1.Size = new System.Drawing.Size(770, 318);
			this.borderedPanel1.TabIndex = 10;
			// 
			// ResourcePackListView
			// 
			this.ResourcePackListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.ResourcePackListView.CheckBoxes = true;
			this.ResourcePackListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.NameColumnHeader,
            this.VersionColumnHeader});
			this.ResourcePackListView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ResourcePackListView.FullRowSelect = true;
			this.ResourcePackListView.GridLines = true;
			this.ResourcePackListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.ResourcePackListView.Location = new System.Drawing.Point(1, 1);
			this.ResourcePackListView.MultiSelect = false;
			this.ResourcePackListView.Name = "ResourcePackListView";
			this.ResourcePackListView.Size = new System.Drawing.Size(768, 316);
			this.ResourcePackListView.TabIndex = 0;
			this.ResourcePackListView.UseCompatibleStateImageBehavior = false;
			this.ResourcePackListView.View = System.Windows.Forms.View.Details;
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
			// ResourcePacksTabPage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.borderedPanel1);
			this.Controls.Add(this.DescriptionGroupBox);
			this.Controls.Add(this.ReloadResourcePacksButton);
			this.Controls.Add(this.PreviewResourcePackButton);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.Name = "ResourcePacksTabPage";
			this.Size = new System.Drawing.Size(772, 482);
			this.DescriptionGroupBox.ResumeLayout(false);
			this.DescriptionGroupBox.PerformLayout();
			this.borderedPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button ReloadResourcePacksButton;
		private System.Windows.Forms.Button PreviewResourcePackButton;
		private System.Windows.Forms.GroupBox DescriptionGroupBox;
		private System.Windows.Forms.TextBox DescriptionTextBox;
		private UI.BorderedPanel borderedPanel1;
		private System.Windows.Forms.ListView ResourcePackListView;
		private System.Windows.Forms.ColumnHeader NameColumnHeader;
		private System.Windows.Forms.ColumnHeader VersionColumnHeader;
	}
}
