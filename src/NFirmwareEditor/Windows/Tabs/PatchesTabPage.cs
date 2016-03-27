using System.Windows.Forms;
using JetBrains.Annotations;
using NFirmware;
using NFirmwareEditor.Models;

namespace NFirmwareEditor.Windows.Tabs
{
	public partial class PatchesTabPage : UserControl, IEditorTabPage
	{
		public PatchesTabPage()
		{
			InitializeComponent();
		}

		public string Title
		{
			get { return "Patches"; }
		}

		public void Initialize([NotNull] Configuration configuration)
		{
		}

		public void OnActivate()
		{
		}

		public void OnFirmwareLoaded([NotNull] Firmware firmware)
		{
		}

		public bool OnHotkey(Keys keyData)
		{
			return false;
		}

		public void OnWorkspaceReset()
		{
		}
	}
}
