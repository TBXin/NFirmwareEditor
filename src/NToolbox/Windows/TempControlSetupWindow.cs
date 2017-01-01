using System;
using System.Windows.Forms;
using JetBrains.Annotations;
using NCore;
using NCore.UI;
using NToolbox.Models;

namespace NToolbox.Windows
{
	internal partial class TempControlSetupWindow : EditorDialogWindow
	{
		private readonly ArcticFoxConfiguration.PIRegulator m_regulator;

		public TempControlSetupWindow([NotNull] ArcticFoxConfiguration.PIRegulator regulator)
		{
			if (regulator == null) throw new ArgumentNullException("regulator");
			m_regulator = regulator;

			InitializeComponent();
			InitializeWorkspace();

			PValueUpDown.ValueChanged += (s, e) => PValueUpDown.Value = (int)PValueUpDown.Value / 10 * 10;
			IValueUpDown.ValueChanged += (s, e) => IValueUpDown.Value = (int)IValueUpDown.Value / 10 * 10;

			SaveButton.Click += (s, e) => DialogResult = DialogResult.OK;
		}

		private void InitializeWorkspace()
		{
			EnabledCheckBox.Checked = m_regulator.IsEnabled;
			RangeUpDown.SetValue(m_regulator.Range);
			PValueUpDown.SetValue(m_regulator.PValue);
			IValueUpDown.SetValue(m_regulator.IValue);
		}

		public ArcticFoxConfiguration.PIRegulator SaveWorkspace()
		{
			return new ArcticFoxConfiguration.PIRegulator
			{
				IsEnabled = EnabledCheckBox.Checked,
				Range = (byte)RangeUpDown.Value,
				PValue = (ushort)PValueUpDown.Value,
				IValue = (ushort)IValueUpDown.Value
			};
		}
	}
}
