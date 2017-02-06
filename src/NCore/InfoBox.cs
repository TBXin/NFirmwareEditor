using System;
using System.Windows.Forms;
using JetBrains.Annotations;
using NCore.UI;

namespace NCore
{
	public class InfoBox
	{
		private static readonly InfoBox s_global = new InfoBox(null);
		private readonly WindowBase m_owner;

		public InfoBox([CanBeNull] WindowBase owner)
		{
			m_owner = owner;
		}

		public static InfoBox Global
		{
			get { return s_global; }
		}

		public void Show([NotNull] string text)
		{
			if (string.IsNullOrEmpty(text)) throw new ArgumentNullException("text");

			if (m_owner == null)
			{
				MessageBox.Show(text, @"Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
			m_owner.UpdateUI(() => MessageBox.Show(m_owner, text, @"Information", MessageBoxButtons.OK, MessageBoxIcon.Information));
		}

		public void Show(string format, params object[] args)
		{
			if (string.IsNullOrEmpty(format)) throw new ArgumentNullException("format");

			Show(string.Format(format, args));
		}

		public DialogResult Show(string text, MessageBoxButtons buttons)
		{
			if (string.IsNullOrEmpty(text)) throw new ArgumentNullException("text");

			if (m_owner == null)
			{
				return MessageBox.Show(text, @"Information", buttons, MessageBoxIcon.Information);
			}
			return m_owner.UpdateUI(() => MessageBox.Show(m_owner, text, @"Information", buttons, MessageBoxIcon.Information));
		}
	}
}
