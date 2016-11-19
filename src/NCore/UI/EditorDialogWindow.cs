using System.Windows.Forms;

namespace NCore.UI
{
	public class EditorDialogWindow : WindowBase
	{
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (keyData == Keys.Escape)
			{
				DialogResult = DialogResult.Cancel;
				return true;
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}
	}
}
