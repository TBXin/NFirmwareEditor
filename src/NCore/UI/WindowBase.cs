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

			InfoBox = new InfoBox(this);
			Load += WindowBase_Load;
		}

		protected InfoBox InfoBox { get; private set; }

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

		protected override void WndProc(ref Message m)
		{
			if (!IgnoreFirstInstanceMessages && m.Msg == CrossApplicationSynchronizer.ShowFirstInstanceMessage)
			{
				ShowFromTray();
			}
			base.WndProc(ref m);
		}

		protected internal void UpdateUI(Action action, bool supressExceptions = true)
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

		internal T UpdateUI<T>(Func<T> action)
		{
			var result = default(T);
			Invoke(new Action(() => result = action()));
			return result;
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
