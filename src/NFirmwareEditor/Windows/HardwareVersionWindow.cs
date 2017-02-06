using NCore.UI;

namespace NFirmwareEditor.Windows
{
	internal partial class HardwareVersionWindow : EditorDialogWindow
	{
		public HardwareVersionWindow(int currentHwVersion)
		{
			InitializeComponent();

			var version = currentHwVersion / 100m;
			if (version < CurrentHWUpDown.Minimum || version > CurrentHWUpDown.Maximum)
			{
				CurrentHWUpDown.Minimum = 0;
				CurrentHWUpDown.Maximum = 0;
				CurrentHWUpDown.Value = 0;
			}
			else
			{
				CurrentHWUpDown.Minimum = CurrentHWUpDown.Maximum = version;
				NewHWUpDown.Value = version;
			}
		}

		public int GetNewHWVersion()
		{
			return (int)(NewHWUpDown.Value * 100m);
		}
	}
}
