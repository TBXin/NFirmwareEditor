using System.Windows.Forms;
using NFirmware;
using NFirmwareEditor.Models;

namespace NFirmwareEditor.Windows.Tabs
{
	internal partial class ResourcePacksTabPage : UserControl, IEditorTabPage
	{
		public ResourcePacksTabPage()
		{
			InitializeComponent();
		}

		#region Implementation of IEditorTabPage
		public string Title
		{
			get { return "Resource Packs"; }
		}

		public void Initialize(IEditorTabPageHost host, Configuration configuration)
		{

		}

		public void OnWorkspaceReset()
		{
		}

		public void OnFirmwareLoaded(Firmware firmware)
		{
		}

		public void OnActivate()
		{
		}

		public bool OnHotkey(Keys keyData)
		{
			return false;
		}
		#endregion
	}
}
