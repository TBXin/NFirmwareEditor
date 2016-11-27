using System;
using System.Windows.Forms;

namespace NCore.UI
{
	public class WindowBase : Form
	{
		public WindowBase()
		{
			if (ApplicationService.IsIconAvailable) Icon = ApplicationService.ApplicationIcon;
		}

		protected bool IgnoreFirstInstanceMessages { get; set; }

		protected void ShowFromTray()
		{
			if (Opacity <= 0) Opacity = 1;

			Visible = true;
			ShowInTaskbar = true;
			Show();
			WindowState = FormWindowState.Normal;
			NativeMethods.SetForegroundWindow(Handle);
		}

		protected void HideToTray()
		{
			Visible = false;
			ShowInTaskbar = false;
			Hide();
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

		protected override void WndProc(ref Message m)
		{
			if (!IgnoreFirstInstanceMessages && m.Msg == SingleInstanceProvider.ShowFirstInstanceMessage)
			{
				ShowFromTray();
			}
			base.WndProc(ref m);
		}
	}
}
