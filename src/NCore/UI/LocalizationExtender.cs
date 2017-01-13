using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace NCore.UI
{
	[ProvideProperty("Key", typeof(Control))]
	public class LocalizationExtender : Component, IExtenderProvider
	{
		private readonly IDictionary<Control, string> m_controls = new Dictionary<Control, string>();

		public LocalizationExtender(IContainer container)
		{
			container.Add(this);
		}

		[Category("Localization")]
		[Description("This is used by some code somewhere to do something")]
		public string GetKey(Control control)
		{
			return control != null && m_controls.ContainsKey(control) ? m_controls[control] : string.Empty;
		}

		public void SetKey(Control control, string value)
		{
			if (control == null || string.IsNullOrEmpty(value)) return;
			m_controls[control] = value;
		}

		public IDictionary<Control, string> GetLocalizableControls()
		{
			return m_controls;
		}

		#region Implementation of IExtenderProvider
		public bool CanExtend(object extendee)
		{
			return true;
		}
		#endregion
	}
}
