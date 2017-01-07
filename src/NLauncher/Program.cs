using System;
using System.Windows.Forms;
using NCore;

namespace NLauncher
{
	internal static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			if (args != null && args.Length == 1)
			{
				Application.Run(new MainWindow(args));
			}
			else
			{
				InfoBox.Show("Path to the *.bin file is required.\n\nPass it as argument, e.g. \"NLauncher.exe path\\to\\the\\firmware.bin\".");
			}
		}
	}
}
