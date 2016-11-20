using System.Drawing;
using NFirmwareEditor.Core;

namespace NFirmwareEditor.Windows
{
	internal partial class HotkeyHelpWindow : EditorDialogWindow
	{
		public HotkeyHelpWindow()
		{
			InitializeComponent();
			Icon = NFEPaths.ApplicationIcon;

			MessageTextBox.ReadOnly = true;
			MessageTextBox.BackColor = Color.White;
		}
	}
}
