#pragma warning disable 108, 114
namespace NFirmwareEditor.Windows
{
	partial class HardwareVersionWindow
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
			this.CurrentHWUpDown = new System.Windows.Forms.NumericUpDown();
			this.ControlBorderedPanel = new NCore.UI.BorderedPanel();
			this.OkButton = new System.Windows.Forms.Button();
			this.CancelButton = new System.Windows.Forms.Button();
			this.NewHWUpDown = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.CurrentHWUpDown)).BeginInit();
			this.ControlBorderedPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.NewHWUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// CurrentHWUpDown
			// 
			this.CurrentHWUpDown.DecimalPlaces = 2;
			this.CurrentHWUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
			this.CurrentHWUpDown.Location = new System.Drawing.Point(160, 10);
			this.CurrentHWUpDown.Maximum = new decimal(new int[] {
            130,
            0,
            0,
            131072});
			this.CurrentHWUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.CurrentHWUpDown.Name = "CurrentHWUpDown";
			this.CurrentHWUpDown.ReadOnly = true;
			this.CurrentHWUpDown.Size = new System.Drawing.Size(50, 21);
			this.CurrentHWUpDown.TabIndex = 0;
			this.CurrentHWUpDown.TabStop = false;
			this.CurrentHWUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
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
			this.ControlBorderedPanel.Location = new System.Drawing.Point(0, 68);
			this.ControlBorderedPanel.Name = "ControlBorderedPanel";
			this.ControlBorderedPanel.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
			this.ControlBorderedPanel.Size = new System.Drawing.Size(214, 39);
			this.ControlBorderedPanel.TabIndex = 10;
			this.ControlBorderedPanel.Text = "borderedPanel1";
			// 
			// OkButton
			// 
			this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OkButton.Location = new System.Drawing.Point(4, 5);
			this.OkButton.Name = "OkButton";
			this.OkButton.Size = new System.Drawing.Size(100, 30);
			this.OkButton.TabIndex = 0;
			this.OkButton.Text = "Ok";
			this.OkButton.UseVisualStyleBackColor = true;
			// 
			// CancelButton
			// 
			this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelButton.Location = new System.Drawing.Point(110, 5);
			this.CancelButton.Name = "CancelButton";
			this.CancelButton.Size = new System.Drawing.Size(100, 30);
			this.CancelButton.TabIndex = 1;
			this.CancelButton.Text = "Cancel";
			this.CancelButton.UseVisualStyleBackColor = true;
			// 
			// NewHWUpDown
			// 
			this.NewHWUpDown.DecimalPlaces = 2;
			this.NewHWUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
			this.NewHWUpDown.Location = new System.Drawing.Point(160, 37);
			this.NewHWUpDown.Maximum = new decimal(new int[] {
            130,
            0,
            0,
            131072});
			this.NewHWUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.NewHWUpDown.Name = "NewHWUpDown";
			this.NewHWUpDown.Size = new System.Drawing.Size(50, 21);
			this.NewHWUpDown.TabIndex = 1;
			this.NewHWUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(106, 13);
			this.label1.TabIndex = 11;
			this.label1.Text = "Current HW Version:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 39);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(90, 13);
			this.label2.TabIndex = 12;
			this.label2.Text = "New HW Version:";
			// 
			// HardwareVersionWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(214, 107);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.NewHWUpDown);
			this.Controls.Add(this.CurrentHWUpDown);
			this.Controls.Add(this.ControlBorderedPanel);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "HardwareVersionWindow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Change HW Version";
			((System.ComponentModel.ISupportInitialize)(this.CurrentHWUpDown)).EndInit();
			this.ControlBorderedPanel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.NewHWUpDown)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.NumericUpDown CurrentHWUpDown;
		private NCore.UI.BorderedPanel ControlBorderedPanel;
		private System.Windows.Forms.Button OkButton;
		private System.Windows.Forms.Button CancelButton;
		private System.Windows.Forms.NumericUpDown NewHWUpDown;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
	}
}