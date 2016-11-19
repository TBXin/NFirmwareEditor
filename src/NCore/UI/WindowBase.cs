using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace NCore.UI
{
	public class WindowBase : Form
	{
		public WindowBase()
		{
			if (IconProvider.IsIconAvailable) Icon = IconProvider.ApplicationIcon;
		}

		protected void UpdateUI(Action action, bool supressExceptions = true)
		{
			if (!supressExceptions)
			{
				Invoke(action);
			}
			else
			{
				try
				{
					Invoke(action);
				}
				catch (Exception)
				{
					// Ignore
				}
			}
		}

		private static class IconProvider
		{
			static IconProvider()
			{
				try
				{
					var assemblyLocation = Assembly.GetEntryAssembly().Location;
					ApplicationIcon = Icon.ExtractAssociatedIcon(assemblyLocation);
					IsIconAvailable = true;
				}
				catch
				{
					IsIconAvailable = false;
				}
			}

			public static bool IsIconAvailable { get; private set; }

			public static Icon ApplicationIcon { get; private set; }
		}
	}
}
