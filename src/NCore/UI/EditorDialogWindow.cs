using System;
using System.Windows.Forms;

namespace NCore.UI
{
	public class EditorDialogWindow : Form
	{
		public EditorDialogWindow()
		{
			//Icon = Paths.ApplicationIcon;
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
