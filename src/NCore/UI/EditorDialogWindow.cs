using System.Windows.Forms;

namespace NCore.UI
{
	public class EditorDialogWindow : WindowBase
	{
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (keyData == Keys.Escape)
			{
				DialogResult = DialogResult.Cancel;
				return true;
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}

		private void InitializeComponent()
		{
			this.SuspendLayout();
			// 
			// EditorDialogWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(284, 261);
			this.MainLocalizationExtender.SetKey(this, "");
			this.Name = "EditorDialogWindow";
			this.ResumeLayout(false);

		}
	}
}
