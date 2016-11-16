using System;
using System.Windows.Forms;

namespace NCore.UI
{
	public class WindowBase : Form
	{
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
	}
}
