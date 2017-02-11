using System.Drawing;
using NCore.UI;

namespace NFirmwareEditor.Windows
{
	internal partial class HotkeyHelpWindow : EditorDialogWindow
	{
		public HotkeyHelpWindow()
		{
			InitializeComponent();

			MessageTextBox.ReadOnly = true;
			MessageTextBox.BackColor = Color.White;
		}
	}
}
