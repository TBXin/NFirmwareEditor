using System;
using System.Windows.Forms;
using NCore;
using NToolbox.Windows;

namespace NToolbox
{
	internal static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			using (var spi = new SingleInstanceProvider("NFE Toolbox © Reiko Kitsune"))
			{
				if (spi.IsCreated)
				{
					MessageBox.Show("NFE Toolbox already running!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}

				Application.Run(new MainWindow());
			}
		}
	}
}
