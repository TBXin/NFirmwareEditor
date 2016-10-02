using System;
using System.Windows.Forms;
using JetBrains.Annotations;
using NFirmwareEditor.Models;

namespace NFirmwareEditor.Windows
{
	internal partial class DischargeProfileWindow : EditorDialogWindow
	{
		private readonly Dataflash m_dataflash;
		private PercentVoltsControlGroup[] m_curveControls;

		public DischargeProfileWindow([NotNull] Dataflash dataflash)
		{
			if (dataflash == null) throw new ArgumentNullException("dataflash");
			m_dataflash = dataflash;

			InitializeComponent();
			InitializeControls();
			InitializeWorkspaceFromDataflash(m_dataflash);
		}

		private void InitializeControls()
		{
			m_curveControls = new[]
			{
				new PercentVoltsControlGroup(Percents1UpDown, Volts1UpDown),
				new PercentVoltsControlGroup(Percents2UpDown, Volts2UpDown),
				new PercentVoltsControlGroup(Percents3UpDown, Volts3UpDown),
				new PercentVoltsControlGroup(Percents4UpDown, Volts4UpDown),
				new PercentVoltsControlGroup(Percents5UpDown, Volts5UpDown),
				new PercentVoltsControlGroup(Percents6UpDown, Volts6UpDown),
				new PercentVoltsControlGroup(Percents7UpDown, Volts7UpDown),
				new PercentVoltsControlGroup(Percents8UpDown, Volts8UpDown),
				new PercentVoltsControlGroup(Percents9UpDown, Volts9UpDown),
				new PercentVoltsControlGroup(Percents10UpDown, Volts10UpDown),
				new PercentVoltsControlGroup(Percents11UpDown, Volts11UpDown)
			};

			SaveButton.Click += SaveButton_Click;
		}

		private void InitializeWorkspaceFromDataflash([NotNull] Dataflash dataflash)
		{
			if (dataflash == null) throw new ArgumentNullException("dataflash");

			for (var i = 0; i < dataflash.ParamsBlock.CustomBattery.Data.Length; i++)
			{
				var data = dataflash.ParamsBlock.CustomBattery.Data[i];

				m_curveControls[i].PercentsUpDown.Value = Math.Max((ushort)0, Math.Min(data.Percents, (ushort)100));
				m_curveControls[i].VoltsUpDown.Value = Math.Max(3.0m, Math.Min(data.Voltage / 100m, 4.2m));
			}
		}

		private void SaveWorkspaceToDataflash([NotNull] Dataflash dataflash)
		{
			if (dataflash == null) throw new ArgumentNullException("dataflash");

			for (var i = 0; i < dataflash.ParamsBlock.CustomBattery.Data.Length; i++)
			{
				var data = m_curveControls[i];

				dataflash.ParamsBlock.CustomBattery.Data[i].Percents = (ushort)data.PercentsUpDown.Value;
				dataflash.ParamsBlock.CustomBattery.Data[i].Voltage = (ushort)(data.VoltsUpDown.Value * 100);
			}
		}

		private void SaveButton_Click(object sender, EventArgs e)
		{
			SaveWorkspaceToDataflash(m_dataflash);
			DialogResult = DialogResult.OK;
		}

		private class PercentVoltsControlGroup
		{
			public PercentVoltsControlGroup(NumericUpDown percentsUpDown, NumericUpDown voltsUpDown)
			{
				PercentsUpDown = percentsUpDown;
				VoltsUpDown = voltsUpDown;
			}

			public NumericUpDown PercentsUpDown { get; private set; }

			public NumericUpDown VoltsUpDown { get; private set; }
		}
	}
}
