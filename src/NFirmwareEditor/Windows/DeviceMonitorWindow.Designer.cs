namespace NFirmwareEditor.Windows
{
	partial class DeviceMonitorWindow
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
			this.MainChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
			((System.ComponentModel.ISupportInitialize)(this.MainChart)).BeginInit();
			this.SuspendLayout();
			// 
			// MainChart
			// 
			this.MainChart.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MainChart.Location = new System.Drawing.Point(0, 0);
			this.MainChart.Name = "MainChart";
			this.MainChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Pastel;
			this.MainChart.Size = new System.Drawing.Size(787, 543);
			this.MainChart.TabIndex = 0;
			// 
			// DeviceMonitorWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(787, 543);
			this.Controls.Add(this.MainChart);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "DeviceMonitorWindow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "DeviceMonitorWindow";
			((System.ComponentModel.ISupportInitialize)(this.MainChart)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataVisualization.Charting.Chart MainChart;
	}
}