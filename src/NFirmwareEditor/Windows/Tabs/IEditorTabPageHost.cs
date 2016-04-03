namespace NFirmwareEditor.Windows.Tabs
{
	internal interface IEditorTabPageHost
	{
		void ReloadFirmware(IEditorTabPage initiator);
	}
}
