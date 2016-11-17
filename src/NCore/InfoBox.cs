using System.Windows.Forms;

namespace NCore
{
	public static class InfoBox
	{
		public static void Show(string text)
		{
			MessageBox.Show(text, @"Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		public static DialogResult Show(string text, MessageBoxButtons buttons)
		{
			return MessageBox.Show(text, @"Information", buttons, MessageBoxIcon.Information);
		}

		public static void Show(string format, params object[] args)
		{
			Show(string.Format(format, args));
		}
	}
}
