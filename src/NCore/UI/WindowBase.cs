using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace NCore.UI
{
	public class WindowBase : Form
	{
		private IContainer components;
		public LocalizationExtender MainLocalizationExtender;

		public WindowBase()
		{
			InitializeComponent();
			if (ApplicationService.IsIconAvailable) Icon = ApplicationService.ApplicationIcon;

			Load += WindowBase_Load;
		}

		protected void LocalizeSelf()
		{
			var localizableControls = MainLocalizationExtender.GetLocalizableControls();
			#if DEBUG
			LocalizationManager.Instance.RegisterLocalizationKeyValue(localizableControls);
			#endif

			var localizationDictionary = LocalizationManager.Instance.GetLocalizationDictionary();
			if (localizationDictionary == null || localizationDictionary.Count == 0) return;

			foreach (var kvp in localizableControls)
			{
				var control = kvp.Key;
				var key = kvp.Value;

				if (localizationDictionary.ContainsKey(key))
				{
					control.Text = localizationDictionary[key];
				}
			}
			OnLocalization();
		}

		protected virtual void OnLocalization()
		{
		}

		private void WindowBase_Load(object sender, EventArgs e)
		{
			LocalizeSelf();
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
			if (!IgnoreFirstInstanceMessages && m.Msg == CrossApplicationSynchronizer.ShowFirstInstanceMessage)
			{
				ShowFromTray();
			}
			base.WndProc(ref m);
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.MainLocalizationExtender = new NCore.UI.LocalizationExtender(this.components);
			this.SuspendLayout();
			// 
			// WindowBase
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(284, 261);
			this.MainLocalizationExtender.SetKey(this, "");
			this.Name = "WindowBase";
			this.ResumeLayout(false);

		}
	}
}
