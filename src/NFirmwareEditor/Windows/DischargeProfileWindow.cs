using System;
using System.Collections.Generic;
using System.Windows.Forms;
using JetBrains.Annotations;
using NFirmwareEditor.Models;

namespace NFirmwareEditor.Windows
{
	internal partial class DischargeProfileWindow : EditorDialogWindow
	{
		private readonly Dataflash m_dataflash;
		private readonly IEnumerable<PercentVoltsControlGroup> m_curveControls;

		public DischargeProfileWindow([NotNull] Dataflash dataflash)
		{
			if (dataflash == null) throw new ArgumentNullException("dataflash");
			m_dataflash = dataflash;

			InitializeComponent();

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
