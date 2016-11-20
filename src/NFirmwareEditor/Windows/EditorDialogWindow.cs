using System;
using System.Windows.Forms;
using NFirmwareEditor.Core;

namespace NFirmwareEditor.Windows
{
	internal class EditorDialogWindow : Form
	{
		public EditorDialogWindow()
		{
			Icon = NFEPaths.ApplicationIcon;
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (keyData == Keys.Escape)
			{
				DialogResult = DialogResult.Cancel;
				return true;
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}

		protected void UpdateUI(Action action, bool supressExceptions = true)
		{
			if (!supressExceptions)
			{
				Invoke(action);
			}
			else
			{
				try
				{
					Invoke(action);
				}
				catch (Exception)
				{
					// Ignore
				}
			}
		}
	}
}
