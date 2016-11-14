using NFirmwareEditor.UI;

namespace NToolbox
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
			this.panel1 = new NFirmwareEditor.UI.BorderedPanel();
			this.panel2 = new NFirmwareEditor.UI.BorderedPanel();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.extendedButton2 = new NToolbox.ExtendedButton();
			this.extendedButton3 = new NToolbox.ExtendedButton();
			this.extendedButton1 = new NToolbox.ExtendedButton();
			this.extendedButton4 = new NToolbox.ExtendedButton();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.White;
			this.panel1.BorderBottom = false;
			this.panel1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(185)))), ((int)(((byte)(185)))));
			this.panel1.BorderLeft = false;
			this.panel1.BorderRight = false;
			this.panel1.BorderTop = false;
			this.panel1.BorderWidth = 1F;
			this.panel1.Controls.Add(this.panel2);
			this.panel1.Controls.Add(this.pictureBox1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(218, 415);
			this.panel1.TabIndex = 4;
			// 
			// panel2
			// 
			this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel2.BackColor = System.Drawing.Color.White;
			this.panel2.BorderBottom = false;
			this.panel2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(185)))), ((int)(((byte)(185)))));
			this.panel2.BorderLeft = false;
			this.panel2.BorderRight = false;
			this.panel2.BorderTop = false;
			this.panel2.BorderWidth = 1F;
			this.panel2.Controls.Add(this.extendedButton2);
			this.panel2.Controls.Add(this.extendedButton3);
			this.panel2.Controls.Add(this.extendedButton1);
			this.panel2.Controls.Add(this.extendedButton4);
			this.panel2.Location = new System.Drawing.Point(0, 214);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(218, 201);
			this.panel2.TabIndex = 4;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.pictureBox1.BackgroundImage = global::NToolbox.Properties.Resources.nfetoolbox;
			this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.pictureBox1.Location = new System.Drawing.Point(17, 3);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(184, 205);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// extendedButton2
			// 
			this.extendedButton2.AdditionalText = "";
			this.extendedButton2.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.extendedButton2.Image = ((System.Drawing.Image)(resources.GetObject("extendedButton2.Image")));
			this.extendedButton2.Location = new System.Drawing.Point(9, 54);
			this.extendedButton2.Name = "extendedButton2";
			this.extendedButton2.Size = new System.Drawing.Size(200, 42);
			this.extendedButton2.TabIndex = 1;
			this.extendedButton2.Text = "myEvic Configuration";
			// 
			// extendedButton3
			// 
			this.extendedButton3.AdditionalText = "";
			this.extendedButton3.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.extendedButton3.Image = global::NToolbox.Properties.Resources.device_monitor;
			this.extendedButton3.Location = new System.Drawing.Point(9, 102);
			this.extendedButton3.Name = "extendedButton3";
			this.extendedButton3.Size = new System.Drawing.Size(200, 42);
			this.extendedButton3.TabIndex = 2;
			this.extendedButton3.Text = "Device Monitor";
			// 
			// extendedButton1
			// 
			this.extendedButton1.AdditionalText = "";
			this.extendedButton1.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.extendedButton1.Image = ((System.Drawing.Image)(resources.GetObject("extendedButton1.Image")));
			this.extendedButton1.Location = new System.Drawing.Point(9, 6);
			this.extendedButton1.Name = "extendedButton1";
			this.extendedButton1.Size = new System.Drawing.Size(200, 42);
			this.extendedButton1.TabIndex = 0;
			this.extendedButton1.Text = "Arctic Fox Configuration";
			// 
			// extendedButton4
			// 
			this.extendedButton4.AdditionalText = "";
			this.extendedButton4.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.extendedButton4.Image = global::NToolbox.Properties.Resources.firmware_updater;
			this.extendedButton4.Location = new System.Drawing.Point(9, 150);
			this.extendedButton4.Name = "extendedButton4";
			this.extendedButton4.Size = new System.Drawing.Size(200, 42);
			this.extendedButton4.TabIndex = 3;
			this.extendedButton4.Text = "Firmware Updater";
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(218, 415);
			this.Controls.Add(this.panel1);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainWindow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "NFE Toolbox";
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private ExtendedButton extendedButton1;
		private ExtendedButton extendedButton2;
		private ExtendedButton extendedButton3;
		private ExtendedButton extendedButton4;
		private NFirmwareEditor.UI.BorderedPanel panel1;
		private System.Windows.Forms.PictureBox pictureBox1;
		private NFirmwareEditor.UI.BorderedPanel panel2;
	}
}

