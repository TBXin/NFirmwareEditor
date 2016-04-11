using System;
using JetBrains.Annotations;
using NFirmwareEditor.Core;
using NFirmwareEditor.Models;

namespace NFirmwareEditor.Windows
{
	internal partial class OptionsWindow : EditorDialogWindow
	{
		private readonly Configuration m_configuration;

		public OptionsWindow()
		{
			InitializeComponent();
			Icon = Paths.ApplicationIcon;
		}

		public OptionsWindow([NotNull] Configuration configuration) : this()
		{
			if (configuration == null) throw new ArgumentNullException("configuration");
			m_configuration = configuration;
		}
	}
}
