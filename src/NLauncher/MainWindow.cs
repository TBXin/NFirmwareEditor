using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using NCore;
using NCore.UI;

namespace NLauncher
{
	internal partial class MainWindow : EditorDialogWindow
	{
		private const string EditorExecutableName = "NFirmwareEditor.exe";
		private const string ToolboxExecutableName = "NToolbox.exe";

		private readonly string m_firmwarePath;

		public MainWindow(string[] args)
		{
			if (args == null || args.Length == 0) throw new ArgumentNullException(nameof(args));
			m_firmwarePath = args[0];

			InitializeComponent();
			EditorButton.Click += EditorButton_Click;
			FirmwareUpdaterButton.Click += FirmwareUpdaterButton_Click;
		}

		private void EditorButton_Click(object sender, EventArgs e)
		{
			StartApplication(EditorExecutableName);
		}

		private void FirmwareUpdaterButton_Click(object sender, EventArgs e)
		{
			StartApplication(ToolboxExecutableName, "/updater");
		}

		private void StartApplication(string executableName, string advancedArg = null)
		{
			var path = CreateExecutablePathAndValidate(executableName);
			if (path == null) return;

			var arguments = string.IsNullOrEmpty(advancedArg) ? m_firmwarePath : advancedArg + " " + m_firmwarePath;
			var process = Process.Start(path, arguments);
			if (process == null)
			{
				InfoBox.Show("Something strange happened.");
				return;
			}
			process.WaitForInputIdle();
			Application.Exit();
		}

		private string CreateExecutablePathAndValidate(string executableName)
		{
			var executablePath = Path.Combine(ApplicationService.ApplicationDirectory, executableName);
			if (File.Exists(executablePath)) return executablePath;

			InfoBox.Show("{0} is not found.\n\nMake sure that the \"{0}\" is in the same folder as the \"NLauncher.exe\".", executableName);
			return null;
		}
	}
}
