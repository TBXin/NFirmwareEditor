using System.Windows.Forms;

namespace NFirmwareEditor.Windows
{
	internal class EditorDialogWindow : Form
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
