namespace NToolbox.Windows
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
			this.SensorsGroupPanel = new NCore.UI.GroupPanel();
			this.BatteryPackPanel = new System.Windows.Forms.Panel();
			this.BatteryPackVoltageLabel = new System.Windows.Forms.Label();
			this.BatteryPackCheckBox = new System.Windows.Forms.CheckBox();
			this.Battery3Panel = new System.Windows.Forms.Panel();
			this.Battery3VoltageLabel = new System.Windows.Forms.Label();
			this.Battery3CheckBox = new System.Windows.Forms.CheckBox();
			this.Battery2Panel = new System.Windows.Forms.Panel();
			this.Battery2VoltageLabel = new System.Windows.Forms.Label();
			this.Battery2CheckBox = new System.Windows.Forms.CheckBox();
			this.TemperatureSetPanel = new System.Windows.Forms.Panel();
			this.TemperatureSetLabel = new System.Windows.Forms.Label();
			this.TemperatureSetCheckBox = new System.Windows.Forms.CheckBox();
			this.PowerSetPanel = new System.Windows.Forms.Panel();
			this.PowerSetLabel = new System.Windows.Forms.Label();
			this.PowerSetCheckBox = new System.Windows.Forms.CheckBox();
			this.BoardTemperaturePanel = new System.Windows.Forms.Panel();
			this.TemperaturePanel = new System.Windows.Forms.Panel();
			this.RealResistancePanel = new System.Windows.Forms.Panel();
			this.ResistancePanel = new System.Windows.Forms.Panel();
			this.OutputCurrentPanel = new System.Windows.Forms.Panel();
			this.OutputVoltagePanel = new System.Windows.Forms.Panel();
			this.PowerPanel = new System.Windows.Forms.Panel();
			this.Battery1Panel = new System.Windows.Forms.Panel();
			this.BoardTemperatureCheckBox = new System.Windows.Forms.CheckBox();
			this.BoardTemperatureLabel = new System.Windows.Forms.Label();
			this.TemperatureLabel = new System.Windows.Forms.Label();
			this.RealResistanceLabel = new System.Windows.Forms.Label();
			this.ResistanceLabel = new System.Windows.Forms.Label();
			this.OutputCurrentLabel = new System.Windows.Forms.Label();
			this.OutputVoltageLabel = new System.Windows.Forms.Label();
			this.PowerLabel = new System.Windows.Forms.Label();
			this.Battery1VoltageLabel = new System.Windows.Forms.Label();
			this.TemperatureCheckBox = new System.Windows.Forms.CheckBox();
			this.RealResistanceCheckBox = new System.Windows.Forms.CheckBox();
			this.ResistanceCheckBox = new System.Windows.Forms.CheckBox();
			this.OutputCurrentCheckBox = new System.Windows.Forms.CheckBox();
			this.OutputVoltageCheckBox = new System.Windows.Forms.CheckBox();
			this.Battery1CheckBox = new System.Windows.Forms.CheckBox();
			this.PowerCheckBox = new System.Windows.Forms.CheckBox();
			this.LineVewGroupPanel = new NCore.UI.GroupPanel();
			this.TrackingButton = new System.Windows.Forms.Button();
			this.MainChartHorizontalScrollBar = new System.Windows.Forms.HScrollBar();
			this.MainChartVerticalScrollBar = new System.Windows.Forms.VScrollBar();
			this.ControlGroupPanel = new NCore.UI.GroupPanel();
			this.ShowPuffsBoundariesCheckBox = new System.Windows.Forms.CheckBox();
			this.SetYScaleButton = new System.Windows.Forms.Button();
			this.RecordButton = new System.Windows.Forms.Button();
			this.PauseButton = new System.Windows.Forms.Button();
			this.SetXScaleButton = new System.Windows.Forms.Button();
			this.PuffButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.MainChart)).BeginInit();
			this.SensorsGroupPanel.SuspendLayout();
			this.LineVewGroupPanel.SuspendLayout();
			this.ControlGroupPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// MainChart
			// 
			this.MainChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.MainChart.Location = new System.Drawing.Point(1, 30);
			this.MainChart.Name = "MainChart";
			this.MainChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Pastel;
			this.MainChart.Size = new System.Drawing.Size(572, 543);
			this.MainChart.TabIndex = 0;
			// 
			// SensorsGroupPanel
			// 
			this.SensorsGroupPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.SensorsGroupPanel.BackColor = System.Drawing.Color.White;
			this.SensorsGroupPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(185)))), ((int)(((byte)(185)))));
			this.SensorsGroupPanel.Controls.Add(this.BatteryPackPanel);
			this.SensorsGroupPanel.Controls.Add(this.BatteryPackVoltageLabel);
			this.SensorsGroupPanel.Controls.Add(this.BatteryPackCheckBox);
			this.SensorsGroupPanel.Controls.Add(this.Battery3Panel);
			this.SensorsGroupPanel.Controls.Add(this.Battery3VoltageLabel);
			this.SensorsGroupPanel.Controls.Add(this.Battery3CheckBox);
			this.SensorsGroupPanel.Controls.Add(this.Battery2Panel);
			this.SensorsGroupPanel.Controls.Add(this.Battery2VoltageLabel);
			this.SensorsGroupPanel.Controls.Add(this.Battery2CheckBox);
			this.SensorsGroupPanel.Controls.Add(this.TemperatureSetPanel);
			this.SensorsGroupPanel.Controls.Add(this.TemperatureSetLabel);
			this.SensorsGroupPanel.Controls.Add(this.TemperatureSetCheckBox);
			this.SensorsGroupPanel.Controls.Add(this.PowerSetPanel);
			this.SensorsGroupPanel.Controls.Add(this.PowerSetLabel);
			this.SensorsGroupPanel.Controls.Add(this.PowerSetCheckBox);
			this.SensorsGroupPanel.Controls.Add(this.BoardTemperaturePanel);
			this.SensorsGroupPanel.Controls.Add(this.TemperaturePanel);
			this.SensorsGroupPanel.Controls.Add(this.RealResistancePanel);
			this.SensorsGroupPanel.Controls.Add(this.ResistancePanel);
			this.SensorsGroupPanel.Controls.Add(this.OutputCurrentPanel);
			this.SensorsGroupPanel.Controls.Add(this.OutputVoltagePanel);
			this.SensorsGroupPanel.Controls.Add(this.PowerPanel);
			this.SensorsGroupPanel.Controls.Add(this.Battery1Panel);
			this.SensorsGroupPanel.Controls.Add(this.BoardTemperatureCheckBox);
			this.SensorsGroupPanel.Controls.Add(this.BoardTemperatureLabel);
			this.SensorsGroupPanel.Controls.Add(this.TemperatureLabel);
			this.SensorsGroupPanel.Controls.Add(this.RealResistanceLabel);
			this.SensorsGroupPanel.Controls.Add(this.ResistanceLabel);
			this.SensorsGroupPanel.Controls.Add(this.OutputCurrentLabel);
			this.SensorsGroupPanel.Controls.Add(this.OutputVoltageLabel);
			this.SensorsGroupPanel.Controls.Add(this.PowerLabel);
			this.SensorsGroupPanel.Controls.Add(this.Battery1VoltageLabel);
			this.SensorsGroupPanel.Controls.Add(this.TemperatureCheckBox);
			this.SensorsGroupPanel.Controls.Add(this.RealResistanceCheckBox);
			this.SensorsGroupPanel.Controls.Add(this.ResistanceCheckBox);
			this.SensorsGroupPanel.Controls.Add(this.OutputCurrentCheckBox);
			this.SensorsGroupPanel.Controls.Add(this.OutputVoltageCheckBox);
			this.SensorsGroupPanel.Controls.Add(this.Battery1CheckBox);
			this.SensorsGroupPanel.Controls.Add(this.PowerCheckBox);
			this.SensorsGroupPanel.HeaderBackColor = System.Drawing.Color.White;
			this.SensorsGroupPanel.HeaderHeight = 30;
			this.SensorsGroupPanel.Location = new System.Drawing.Point(3, 3);
			this.SensorsGroupPanel.Name = "SensorsGroupPanel";
			this.SensorsGroupPanel.Size = new System.Drawing.Size(200, 377);
			this.SensorsGroupPanel.TabIndex = 1;
			this.SensorsGroupPanel.TabStop = false;
			this.SensorsGroupPanel.Text = "Sensors:";
			// 
			// BatteryPackPanel
			// 
			this.BatteryPackPanel.BackColor = System.Drawing.Color.Black;
			this.BatteryPackPanel.Location = new System.Drawing.Point(5, 122);
			this.BatteryPackPanel.Name = "BatteryPackPanel";
			this.BatteryPackPanel.Size = new System.Drawing.Size(188, 2);
			this.BatteryPackPanel.TabIndex = 37;
			// 
			// BatteryPackVoltageLabel
			// 
			this.BatteryPackVoltageLabel.Location = new System.Drawing.Point(126, 106);
			this.BatteryPackVoltageLabel.Name = "BatteryPackVoltageLabel";
			this.BatteryPackVoltageLabel.Size = new System.Drawing.Size(70, 17);
			this.BatteryPackVoltageLabel.TabIndex = 36;
			this.BatteryPackVoltageLabel.Text = "?";
			this.BatteryPackVoltageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// BatteryPackCheckBox
			// 
			this.BatteryPackCheckBox.AutoSize = true;
			this.BatteryPackCheckBox.Location = new System.Drawing.Point(5, 106);
			this.BatteryPackCheckBox.Name = "BatteryPackCheckBox";
			this.BatteryPackCheckBox.Size = new System.Drawing.Size(91, 17);
			this.BatteryPackCheckBox.TabIndex = 35;
			this.BatteryPackCheckBox.Text = "Battery Pack:";
			this.BatteryPackCheckBox.UseVisualStyleBackColor = true;
			// 
			// Battery3Panel
			// 
			this.Battery3Panel.BackColor = System.Drawing.Color.Black;
			this.Battery3Panel.Location = new System.Drawing.Point(5, 98);
			this.Battery3Panel.Name = "Battery3Panel";
			this.Battery3Panel.Size = new System.Drawing.Size(188, 2);
			this.Battery3Panel.TabIndex = 34;
			// 
			// Battery3VoltageLabel
			// 
			this.Battery3VoltageLabel.Location = new System.Drawing.Point(126, 82);
			this.Battery3VoltageLabel.Name = "Battery3VoltageLabel";
			this.Battery3VoltageLabel.Size = new System.Drawing.Size(70, 17);
			this.Battery3VoltageLabel.TabIndex = 33;
			this.Battery3VoltageLabel.Text = "?";
			this.Battery3VoltageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// Battery3CheckBox
			// 
			this.Battery3CheckBox.AutoSize = true;
			this.Battery3CheckBox.Location = new System.Drawing.Point(5, 82);
			this.Battery3CheckBox.Name = "Battery3CheckBox";
			this.Battery3CheckBox.Size = new System.Drawing.Size(72, 17);
			this.Battery3CheckBox.TabIndex = 32;
			this.Battery3CheckBox.Text = "Battery3:";
			this.Battery3CheckBox.UseVisualStyleBackColor = true;
			// 
			// Battery2Panel
			// 
			this.Battery2Panel.BackColor = System.Drawing.Color.Black;
			this.Battery2Panel.Location = new System.Drawing.Point(5, 74);
			this.Battery2Panel.Name = "Battery2Panel";
			this.Battery2Panel.Size = new System.Drawing.Size(188, 2);
			this.Battery2Panel.TabIndex = 31;
			// 
			// Battery2VoltageLabel
			// 
			this.Battery2VoltageLabel.Location = new System.Drawing.Point(126, 58);
			this.Battery2VoltageLabel.Name = "Battery2VoltageLabel";
			this.Battery2VoltageLabel.Size = new System.Drawing.Size(70, 17);
			this.Battery2VoltageLabel.TabIndex = 30;
			this.Battery2VoltageLabel.Text = "?";
			this.Battery2VoltageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// Battery2CheckBox
			// 
			this.Battery2CheckBox.AutoSize = true;
			this.Battery2CheckBox.Location = new System.Drawing.Point(5, 58);
			this.Battery2CheckBox.Name = "Battery2CheckBox";
			this.Battery2CheckBox.Size = new System.Drawing.Size(72, 17);
			this.Battery2CheckBox.TabIndex = 29;
			this.Battery2CheckBox.Text = "Battery2:";
			this.Battery2CheckBox.UseVisualStyleBackColor = true;
			// 
			// TemperatureSetPanel
			// 
			this.TemperatureSetPanel.BackColor = System.Drawing.Color.Black;
			this.TemperatureSetPanel.Location = new System.Drawing.Point(6, 242);
			this.TemperatureSetPanel.Name = "TemperatureSetPanel";
			this.TemperatureSetPanel.Size = new System.Drawing.Size(188, 2);
			this.TemperatureSetPanel.TabIndex = 28;
			// 
			// TemperatureSetLabel
			// 
			this.TemperatureSetLabel.Location = new System.Drawing.Point(127, 226);
			this.TemperatureSetLabel.Name = "TemperatureSetLabel";
			this.TemperatureSetLabel.Size = new System.Drawing.Size(70, 17);
			this.TemperatureSetLabel.TabIndex = 27;
			this.TemperatureSetLabel.Text = "?";
			this.TemperatureSetLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// TemperatureSetCheckBox
			// 
			this.TemperatureSetCheckBox.AutoSize = true;
			this.TemperatureSetCheckBox.Location = new System.Drawing.Point(6, 226);
			this.TemperatureSetCheckBox.Name = "TemperatureSetCheckBox";
			this.TemperatureSetCheckBox.Size = new System.Drawing.Size(111, 17);
			this.TemperatureSetCheckBox.TabIndex = 26;
			this.TemperatureSetCheckBox.Text = "Temperature Set:";
			this.TemperatureSetCheckBox.UseVisualStyleBackColor = true;
			// 
			// PowerSetPanel
			// 
			this.PowerSetPanel.BackColor = System.Drawing.Color.Black;
			this.PowerSetPanel.Location = new System.Drawing.Point(6, 194);
			this.PowerSetPanel.Name = "PowerSetPanel";
			this.PowerSetPanel.Size = new System.Drawing.Size(188, 2);
			this.PowerSetPanel.TabIndex = 25;
			// 
			// PowerSetLabel
			// 
			this.PowerSetLabel.Location = new System.Drawing.Point(127, 178);
			this.PowerSetLabel.Name = "PowerSetLabel";
			this.PowerSetLabel.Size = new System.Drawing.Size(70, 17);
			this.PowerSetLabel.TabIndex = 24;
			this.PowerSetLabel.Text = "?";
			this.PowerSetLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// PowerSetCheckBox
			// 
			this.PowerSetCheckBox.AutoSize = true;
			this.PowerSetCheckBox.Location = new System.Drawing.Point(6, 178);
			this.PowerSetCheckBox.Name = "PowerSetCheckBox";
			this.PowerSetCheckBox.Size = new System.Drawing.Size(79, 17);
			this.PowerSetCheckBox.TabIndex = 23;
			this.PowerSetCheckBox.Text = "Power Set:";
			this.PowerSetCheckBox.UseVisualStyleBackColor = true;
			// 
			// BoardTemperaturePanel
			// 
			this.BoardTemperaturePanel.BackColor = System.Drawing.Color.Black;
			this.BoardTemperaturePanel.Location = new System.Drawing.Point(6, 361);
			this.BoardTemperaturePanel.Name = "BoardTemperaturePanel";
			this.BoardTemperaturePanel.Size = new System.Drawing.Size(188, 2);
			this.BoardTemperaturePanel.TabIndex = 22;
			// 
			// TemperaturePanel
			// 
			this.TemperaturePanel.BackColor = System.Drawing.Color.Black;
			this.TemperaturePanel.Location = new System.Drawing.Point(6, 218);
			this.TemperaturePanel.Name = "TemperaturePanel";
			this.TemperaturePanel.Size = new System.Drawing.Size(188, 2);
			this.TemperaturePanel.TabIndex = 21;
			// 
			// RealResistancePanel
			// 
			this.RealResistancePanel.BackColor = System.Drawing.Color.Black;
			this.RealResistancePanel.Location = new System.Drawing.Point(6, 337);
			this.RealResistancePanel.Name = "RealResistancePanel";
			this.RealResistancePanel.Size = new System.Drawing.Size(188, 2);
			this.RealResistancePanel.TabIndex = 20;
			// 
			// ResistancePanel
			// 
			this.ResistancePanel.BackColor = System.Drawing.Color.Black;
			this.ResistancePanel.Location = new System.Drawing.Point(6, 314);
			this.ResistancePanel.Name = "ResistancePanel";
			this.ResistancePanel.Size = new System.Drawing.Size(188, 2);
			this.ResistancePanel.TabIndex = 18;
			// 
			// OutputCurrentPanel
			// 
			this.OutputCurrentPanel.BackColor = System.Drawing.Color.Black;
			this.OutputCurrentPanel.Location = new System.Drawing.Point(6, 266);
			this.OutputCurrentPanel.Name = "OutputCurrentPanel";
			this.OutputCurrentPanel.Size = new System.Drawing.Size(188, 2);
			this.OutputCurrentPanel.TabIndex = 19;
			// 
			// OutputVoltagePanel
			// 
			this.OutputVoltagePanel.BackColor = System.Drawing.Color.Black;
			this.OutputVoltagePanel.Location = new System.Drawing.Point(6, 290);
			this.OutputVoltagePanel.Name = "OutputVoltagePanel";
			this.OutputVoltagePanel.Size = new System.Drawing.Size(188, 2);
			this.OutputVoltagePanel.TabIndex = 18;
			// 
			// PowerPanel
			// 
			this.PowerPanel.BackColor = System.Drawing.Color.Black;
			this.PowerPanel.Location = new System.Drawing.Point(6, 170);
			this.PowerPanel.Name = "PowerPanel";
			this.PowerPanel.Size = new System.Drawing.Size(188, 2);
			this.PowerPanel.TabIndex = 17;
			// 
			// Battery1Panel
			// 
			this.Battery1Panel.BackColor = System.Drawing.Color.Black;
			this.Battery1Panel.Location = new System.Drawing.Point(5, 50);
			this.Battery1Panel.Name = "Battery1Panel";
			this.Battery1Panel.Size = new System.Drawing.Size(188, 2);
			this.Battery1Panel.TabIndex = 16;
			// 
			// BoardTemperatureCheckBox
			// 
			this.BoardTemperatureCheckBox.AutoSize = true;
			this.BoardTemperatureCheckBox.BackColor = System.Drawing.Color.Transparent;
			this.BoardTemperatureCheckBox.Location = new System.Drawing.Point(6, 345);
			this.BoardTemperatureCheckBox.Name = "BoardTemperatureCheckBox";
			this.BoardTemperatureCheckBox.Size = new System.Drawing.Size(123, 17);
			this.BoardTemperatureCheckBox.TabIndex = 7;
			this.BoardTemperatureCheckBox.Text = "Board Temperature:";
			this.BoardTemperatureCheckBox.UseVisualStyleBackColor = false;
			// 
			// BoardTemperatureLabel
			// 
			this.BoardTemperatureLabel.Location = new System.Drawing.Point(127, 345);
			this.BoardTemperatureLabel.Name = "BoardTemperatureLabel";
			this.BoardTemperatureLabel.Size = new System.Drawing.Size(70, 17);
			this.BoardTemperatureLabel.TabIndex = 15;
			this.BoardTemperatureLabel.Text = "?";
			this.BoardTemperatureLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// TemperatureLabel
			// 
			this.TemperatureLabel.Location = new System.Drawing.Point(127, 202);
			this.TemperatureLabel.Name = "TemperatureLabel";
			this.TemperatureLabel.Size = new System.Drawing.Size(70, 17);
			this.TemperatureLabel.TabIndex = 14;
			this.TemperatureLabel.Text = "?";
			this.TemperatureLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// RealResistanceLabel
			// 
			this.RealResistanceLabel.Location = new System.Drawing.Point(127, 321);
			this.RealResistanceLabel.Name = "RealResistanceLabel";
			this.RealResistanceLabel.Size = new System.Drawing.Size(70, 17);
			this.RealResistanceLabel.TabIndex = 13;
			this.RealResistanceLabel.Text = "?";
			this.RealResistanceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// ResistanceLabel
			// 
			this.ResistanceLabel.Location = new System.Drawing.Point(127, 298);
			this.ResistanceLabel.Name = "ResistanceLabel";
			this.ResistanceLabel.Size = new System.Drawing.Size(70, 17);
			this.ResistanceLabel.TabIndex = 12;
			this.ResistanceLabel.Text = "?";
			this.ResistanceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// OutputCurrentLabel
			// 
			this.OutputCurrentLabel.Location = new System.Drawing.Point(127, 250);
			this.OutputCurrentLabel.Name = "OutputCurrentLabel";
			this.OutputCurrentLabel.Size = new System.Drawing.Size(70, 17);
			this.OutputCurrentLabel.TabIndex = 11;
			this.OutputCurrentLabel.Text = "?";
			this.OutputCurrentLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// OutputVoltageLabel
			// 
			this.OutputVoltageLabel.Location = new System.Drawing.Point(127, 274);
			this.OutputVoltageLabel.Name = "OutputVoltageLabel";
			this.OutputVoltageLabel.Size = new System.Drawing.Size(70, 17);
			this.OutputVoltageLabel.TabIndex = 10;
			this.OutputVoltageLabel.Text = "?";
			this.OutputVoltageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// PowerLabel
			// 
			this.PowerLabel.Location = new System.Drawing.Point(127, 154);
			this.PowerLabel.Name = "PowerLabel";
			this.PowerLabel.Size = new System.Drawing.Size(70, 17);
			this.PowerLabel.TabIndex = 9;
			this.PowerLabel.Text = "?";
			this.PowerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// Battery1VoltageLabel
			// 
			this.Battery1VoltageLabel.Location = new System.Drawing.Point(126, 34);
			this.Battery1VoltageLabel.Name = "Battery1VoltageLabel";
			this.Battery1VoltageLabel.Size = new System.Drawing.Size(70, 17);
			this.Battery1VoltageLabel.TabIndex = 8;
			this.Battery1VoltageLabel.Text = "?";
			this.Battery1VoltageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// TemperatureCheckBox
			// 
			this.TemperatureCheckBox.AutoSize = true;
			this.TemperatureCheckBox.Location = new System.Drawing.Point(6, 202);
			this.TemperatureCheckBox.Name = "TemperatureCheckBox";
			this.TemperatureCheckBox.Size = new System.Drawing.Size(92, 17);
			this.TemperatureCheckBox.TabIndex = 6;
			this.TemperatureCheckBox.Text = "Temperature:";
			this.TemperatureCheckBox.UseVisualStyleBackColor = true;
			// 
			// RealResistanceCheckBox
			// 
			this.RealResistanceCheckBox.AutoSize = true;
			this.RealResistanceCheckBox.Location = new System.Drawing.Point(6, 321);
			this.RealResistanceCheckBox.Name = "RealResistanceCheckBox";
			this.RealResistanceCheckBox.Size = new System.Drawing.Size(104, 17);
			this.RealResistanceCheckBox.TabIndex = 5;
			this.RealResistanceCheckBox.Text = "Live Resistance:";
			this.RealResistanceCheckBox.UseVisualStyleBackColor = true;
			// 
			// ResistanceCheckBox
			// 
			this.ResistanceCheckBox.AutoSize = true;
			this.ResistanceCheckBox.Location = new System.Drawing.Point(6, 298);
			this.ResistanceCheckBox.Name = "ResistanceCheckBox";
			this.ResistanceCheckBox.Size = new System.Drawing.Size(106, 17);
			this.ResistanceCheckBox.TabIndex = 4;
			this.ResistanceCheckBox.Text = "Cold Resistance:";
			this.ResistanceCheckBox.UseVisualStyleBackColor = true;
			// 
			// OutputCurrentCheckBox
			// 
			this.OutputCurrentCheckBox.AutoSize = true;
			this.OutputCurrentCheckBox.Location = new System.Drawing.Point(6, 250);
			this.OutputCurrentCheckBox.Name = "OutputCurrentCheckBox";
			this.OutputCurrentCheckBox.Size = new System.Drawing.Size(104, 17);
			this.OutputCurrentCheckBox.TabIndex = 3;
			this.OutputCurrentCheckBox.Text = "Output Current:";
			this.OutputCurrentCheckBox.UseVisualStyleBackColor = true;
			// 
			// OutputVoltageCheckBox
			// 
			this.OutputVoltageCheckBox.AutoSize = true;
			this.OutputVoltageCheckBox.Location = new System.Drawing.Point(6, 274);
			this.OutputVoltageCheckBox.Name = "OutputVoltageCheckBox";
			this.OutputVoltageCheckBox.Size = new System.Drawing.Size(103, 17);
			this.OutputVoltageCheckBox.TabIndex = 2;
			this.OutputVoltageCheckBox.Text = "Output Voltage:";
			this.OutputVoltageCheckBox.UseVisualStyleBackColor = true;
			// 
			// Battery1CheckBox
			// 
			this.Battery1CheckBox.AutoSize = true;
			this.Battery1CheckBox.Location = new System.Drawing.Point(5, 34);
			this.Battery1CheckBox.Name = "Battery1CheckBox";
			this.Battery1CheckBox.Size = new System.Drawing.Size(72, 17);
			this.Battery1CheckBox.TabIndex = 1;
			this.Battery1CheckBox.Text = "Battery1:";
			this.Battery1CheckBox.UseVisualStyleBackColor = true;
			// 
			// PowerCheckBox
			// 
			this.PowerCheckBox.AutoSize = true;
			this.PowerCheckBox.Location = new System.Drawing.Point(6, 154);
			this.PowerCheckBox.Name = "PowerCheckBox";
			this.PowerCheckBox.Size = new System.Drawing.Size(60, 17);
			this.PowerCheckBox.TabIndex = 0;
			this.PowerCheckBox.Text = "Power:";
			this.PowerCheckBox.UseVisualStyleBackColor = true;
			// 
			// LineVewGroupPanel
			// 
			this.LineVewGroupPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.LineVewGroupPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(185)))), ((int)(((byte)(185)))));
			this.LineVewGroupPanel.Controls.Add(this.TrackingButton);
			this.LineVewGroupPanel.Controls.Add(this.MainChart);
			this.LineVewGroupPanel.Controls.Add(this.MainChartHorizontalScrollBar);
			this.LineVewGroupPanel.Controls.Add(this.MainChartVerticalScrollBar);
			this.LineVewGroupPanel.HeaderBackColor = System.Drawing.Color.White;
			this.LineVewGroupPanel.HeaderHeight = 30;
			this.LineVewGroupPanel.Location = new System.Drawing.Point(206, 3);
			this.LineVewGroupPanel.Name = "LineVewGroupPanel";
			this.LineVewGroupPanel.Padding = new System.Windows.Forms.Padding(1, 0, 1, 1);
			this.LineVewGroupPanel.Size = new System.Drawing.Size(591, 591);
			this.LineVewGroupPanel.TabIndex = 2;
			this.LineVewGroupPanel.TabStop = false;
			this.LineVewGroupPanel.Text = "Line view:";
			// 
			// TrackingButton
			// 
			this.TrackingButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.TrackingButton.Enabled = false;
			this.TrackingButton.Location = new System.Drawing.Point(517, 4);
			this.TrackingButton.Name = "TrackingButton";
			this.TrackingButton.Size = new System.Drawing.Size(70, 22);
			this.TrackingButton.TabIndex = 2;
			this.TrackingButton.Text = "Follow";
			this.TrackingButton.UseVisualStyleBackColor = true;
			// 
			// MainChartHorizontalScrollBar
			// 
			this.MainChartHorizontalScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.MainChartHorizontalScrollBar.LargeChange = 1;
			this.MainChartHorizontalScrollBar.Location = new System.Drawing.Point(1, 573);
			this.MainChartHorizontalScrollBar.Maximum = 0;
			this.MainChartHorizontalScrollBar.Name = "MainChartHorizontalScrollBar";
			this.MainChartHorizontalScrollBar.Size = new System.Drawing.Size(572, 17);
			this.MainChartHorizontalScrollBar.TabIndex = 1;
			// 
			// MainChartVerticalScrollBar
			// 
			this.MainChartVerticalScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.MainChartVerticalScrollBar.LargeChange = 1;
			this.MainChartVerticalScrollBar.Location = new System.Drawing.Point(573, 30);
			this.MainChartVerticalScrollBar.Maximum = 0;
			this.MainChartVerticalScrollBar.Name = "MainChartVerticalScrollBar";
			this.MainChartVerticalScrollBar.Size = new System.Drawing.Size(17, 543);
			this.MainChartVerticalScrollBar.TabIndex = 3;
			// 
			// ControlGroupPanel
			// 
			this.ControlGroupPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.ControlGroupPanel.BackColor = System.Drawing.Color.White;
			this.ControlGroupPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(185)))), ((int)(((byte)(185)))));
			this.ControlGroupPanel.Controls.Add(this.ShowPuffsBoundariesCheckBox);
			this.ControlGroupPanel.Controls.Add(this.SetYScaleButton);
			this.ControlGroupPanel.Controls.Add(this.RecordButton);
			this.ControlGroupPanel.Controls.Add(this.PauseButton);
			this.ControlGroupPanel.Controls.Add(this.SetXScaleButton);
			this.ControlGroupPanel.Controls.Add(this.PuffButton);
			this.ControlGroupPanel.HeaderBackColor = System.Drawing.Color.White;
			this.ControlGroupPanel.HeaderHeight = 30;
			this.ControlGroupPanel.Location = new System.Drawing.Point(3, 383);
			this.ControlGroupPanel.Name = "ControlGroupPanel";
			this.ControlGroupPanel.Size = new System.Drawing.Size(200, 211);
			this.ControlGroupPanel.TabIndex = 3;
			this.ControlGroupPanel.TabStop = false;
			this.ControlGroupPanel.Text = "Control:";
			// 
			// ShowPuffsBoundariesCheckBox
			// 
			this.ShowPuffsBoundariesCheckBox.AutoSize = true;
			this.ShowPuffsBoundariesCheckBox.Location = new System.Drawing.Point(6, 149);
			this.ShowPuffsBoundariesCheckBox.Name = "ShowPuffsBoundariesCheckBox";
			this.ShowPuffsBoundariesCheckBox.Size = new System.Drawing.Size(136, 17);
			this.ShowPuffsBoundariesCheckBox.TabIndex = 5;
			this.ShowPuffsBoundariesCheckBox.Text = "Show Puffs Boundaries";
			this.ShowPuffsBoundariesCheckBox.UseVisualStyleBackColor = true;
			// 
			// SetYScaleButton
			// 
			this.SetYScaleButton.Location = new System.Drawing.Point(101, 91);
			this.SetYScaleButton.Name = "SetYScaleButton";
			this.SetYScaleButton.Size = new System.Drawing.Size(94, 23);
			this.SetYScaleButton.TabIndex = 4;
			this.SetYScaleButton.Text = "Set Y Scale";
			this.SetYScaleButton.UseVisualStyleBackColor = true;
			// 
			// RecordButton
			// 
			this.RecordButton.Location = new System.Drawing.Point(5, 62);
			this.RecordButton.Name = "RecordButton";
			this.RecordButton.Size = new System.Drawing.Size(190, 23);
			this.RecordButton.TabIndex = 3;
			this.RecordButton.Text = "Record...";
			this.RecordButton.UseVisualStyleBackColor = true;
			// 
			// PauseButton
			// 
			this.PauseButton.Location = new System.Drawing.Point(5, 34);
			this.PauseButton.Name = "PauseButton";
			this.PauseButton.Size = new System.Drawing.Size(190, 23);
			this.PauseButton.TabIndex = 2;
			this.PauseButton.Text = "Pause";
			this.PauseButton.UseVisualStyleBackColor = true;
			// 
			// SetXScaleButton
			// 
			this.SetXScaleButton.Location = new System.Drawing.Point(5, 91);
			this.SetXScaleButton.Name = "SetXScaleButton";
			this.SetXScaleButton.Size = new System.Drawing.Size(94, 23);
			this.SetXScaleButton.TabIndex = 1;
			this.SetXScaleButton.Text = "Set X Scale";
			this.SetXScaleButton.UseVisualStyleBackColor = true;
			// 
			// PuffButton
			// 
			this.PuffButton.Location = new System.Drawing.Point(5, 120);
			this.PuffButton.Name = "PuffButton";
			this.PuffButton.Size = new System.Drawing.Size(190, 23);
			this.PuffButton.TabIndex = 0;
			this.PuffButton.Text = "Puff...";
			this.PuffButton.UseVisualStyleBackColor = true;
			// 
			// DeviceMonitorWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 597);
			this.Controls.Add(this.ControlGroupPanel);
			this.Controls.Add(this.LineVewGroupPanel);
			this.Controls.Add(this.SensorsGroupPanel);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(816, 539);
			this.Name = "DeviceMonitorWindow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "NFE Toolbox - Device Monitor";
			((System.ComponentModel.ISupportInitialize)(this.MainChart)).EndInit();
			this.SensorsGroupPanel.ResumeLayout(false);
			this.SensorsGroupPanel.PerformLayout();
			this.LineVewGroupPanel.ResumeLayout(false);
			this.ControlGroupPanel.ResumeLayout(false);
			this.ControlGroupPanel.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataVisualization.Charting.Chart MainChart;
		private NCore.UI.GroupPanel SensorsGroupPanel;
		private NCore.UI.GroupPanel LineVewGroupPanel;
		private System.Windows.Forms.CheckBox PowerCheckBox;
		private System.Windows.Forms.CheckBox Battery1CheckBox;
		private System.Windows.Forms.CheckBox OutputVoltageCheckBox;
		private System.Windows.Forms.CheckBox OutputCurrentCheckBox;
		private System.Windows.Forms.CheckBox ResistanceCheckBox;
		private System.Windows.Forms.CheckBox TemperatureCheckBox;
		private System.Windows.Forms.CheckBox RealResistanceCheckBox;
		private System.Windows.Forms.CheckBox BoardTemperatureCheckBox;
		private System.Windows.Forms.Label Battery1VoltageLabel;
		private System.Windows.Forms.Label BoardTemperatureLabel;
		private System.Windows.Forms.Label TemperatureLabel;
		private System.Windows.Forms.Label RealResistanceLabel;
		private System.Windows.Forms.Label ResistanceLabel;
		private System.Windows.Forms.Label OutputCurrentLabel;
		private System.Windows.Forms.Label OutputVoltageLabel;
		private System.Windows.Forms.Label PowerLabel;
		private System.Windows.Forms.Panel Battery1Panel;
		private System.Windows.Forms.Panel PowerPanel;
		private System.Windows.Forms.Panel BoardTemperaturePanel;
		private System.Windows.Forms.Panel TemperaturePanel;
		private System.Windows.Forms.Panel RealResistancePanel;
		private System.Windows.Forms.Panel ResistancePanel;
		private System.Windows.Forms.Panel OutputCurrentPanel;
		private System.Windows.Forms.Panel OutputVoltagePanel;
		private System.Windows.Forms.Panel TemperatureSetPanel;
		private System.Windows.Forms.Label TemperatureSetLabel;
		private System.Windows.Forms.CheckBox TemperatureSetCheckBox;
		private System.Windows.Forms.Panel PowerSetPanel;
		private System.Windows.Forms.Label PowerSetLabel;
		private System.Windows.Forms.CheckBox PowerSetCheckBox;
		private NCore.UI.GroupPanel ControlGroupPanel;
		private System.Windows.Forms.Button PuffButton;
		private System.Windows.Forms.Button SetXScaleButton;
		private System.Windows.Forms.Button PauseButton;
		private System.Windows.Forms.HScrollBar MainChartHorizontalScrollBar;
		private System.Windows.Forms.Button TrackingButton;
		private System.Windows.Forms.Button RecordButton;
		private System.Windows.Forms.Panel BatteryPackPanel;
		private System.Windows.Forms.Label BatteryPackVoltageLabel;
		private System.Windows.Forms.CheckBox BatteryPackCheckBox;
		private System.Windows.Forms.Panel Battery3Panel;
		private System.Windows.Forms.Label Battery3VoltageLabel;
		private System.Windows.Forms.CheckBox Battery3CheckBox;
		private System.Windows.Forms.Panel Battery2Panel;
		private System.Windows.Forms.Label Battery2VoltageLabel;
		private System.Windows.Forms.CheckBox Battery2CheckBox;
		private System.Windows.Forms.VScrollBar MainChartVerticalScrollBar;
		private System.Windows.Forms.Button SetYScaleButton;
		private System.Windows.Forms.CheckBox ShowPuffsBoundariesCheckBox;
	}
}