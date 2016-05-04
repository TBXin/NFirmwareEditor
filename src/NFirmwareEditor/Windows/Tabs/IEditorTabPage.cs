using System.Windows.Forms;
using JetBrains.Annotations;
using NFirmware;
using NFirmwareEditor.Models;

namespace NFirmwareEditor.Windows.Tabs
{
	internal interface IEditorTabPage
	{
		bool IsDirty { get; set; }
		string Title { get; }
		void Initialize([NotNull] IEditorTabPageHost host, [NotNull] ApplicationConfiguration configuration);
		void OnWorkspaceReset();
		void OnFirmwareLoaded([NotNull] Firmware firmware);
		void OnActivate();
		bool OnHotkey(Keys keyData);
	}
}
