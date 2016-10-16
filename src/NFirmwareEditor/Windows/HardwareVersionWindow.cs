namespace NFirmwareEditor.Windows
{
	internal partial class HardwareVersionWindow : EditorDialogWindow
	{
		public HardwareVersionWindow(int currentHwVersion)
		{
			InitializeComponent();

			CurrentHWUpDown.Minimum = CurrentHWUpDown.Maximum = CurrentHWUpDown.Value = currentHwVersion / 100m;
			NewHWUpDown.Value = currentHwVersion / 100m;
		}

		public int GetNewHWVersion()
		{
			return (int)(NewHWUpDown.Value * 100m);
		}
	}
}
