using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Design;

// ReSharper disable LocalizableElement
namespace NCore.UI
{
	[Designer(typeof(MultiPanelDesigner))]
	public class MultiPanel : Panel
	{
		public event EventHandler SelectedPageChanged;

		public virtual void OnSelectedPageChanged(object sender, EventArgs e)
		{
			var handler = SelectedPageChanged;
			if (handler != null) handler(sender, e);
		}

		private MultiPanelPage m_selectedPage;

		public int SelectedPageN { get; private set; }

		public int TotalPages
		{
			get { return Controls.Count; }
		}

		public MultiPanelPage SelectedPage
		{
			get { return m_selectedPage; }
			set
			{
				m_selectedPage = value;
				if (m_selectedPage == null) return;
				foreach (Control child in Controls)
				{
					if (ReferenceEquals(child, m_selectedPage))
					{
						child.Visible = true;
						SelectedPageN = Controls.IndexOf(child);
						OnSelectedPageChanged(this, new EventArgs());
					}
					else child.Visible = false;
				}
			}
		}

		public void SelectNextPage()
		{
			if (SelectedPageN + 1 < Controls.Count)
			{
				SelectedPageN++;
				SelectedPage = (MultiPanelPage)Controls[SelectedPageN];
			}
		}

		public void SelectPrevPage()
		{
			if (SelectedPageN - 1 >= 0)
			{
				SelectedPageN--;
				SelectedPage = (MultiPanelPage)Controls[SelectedPageN];
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			using (var br = new SolidBrush(BackColor))
			{
				e.Graphics.FillRectangle(br, ClientRectangle);
			}
		}

		protected override ControlCollection CreateControlsInstance()
		{
			return new MultiPanelPagesCollection(this);
		}
	}

	[Designer(typeof(MultiPanelPageDesigner)), ToolboxItem(false)]
	public class MultiPanelPage : ContainerControl
	{
		public MultiPanelPage()
		{
			base.Dock = DockStyle.Fill;
		}

		public override DockStyle Dock
		{
			get { return base.Dock; }
			set { base.Dock = DockStyle.Fill; }
		}

		[Category("Appearance")]
		public string Description { get; set; }

		protected override Control.ControlCollection CreateControlsInstance()
		{
			return new ControlCollection(this);
		}

		public new class ControlCollection : Control.ControlCollection
		{
			public ControlCollection(Control owner) : base(owner)
			{
				if (owner == null) throw new ArgumentNullException("owner", "Tried to create a MultiPanelPage.ControlCollection with a null owner.");
				var c = owner as MultiPanelPage;
				if (c == null) throw new ArgumentException("Tried to create a MultiPanelPage.ControlCollection with a non-MultiPanelPage owner.", "owner");
			}

			public override void Add(Control value)
			{
				if (value == null) throw new ArgumentNullException("value", "Tried to add a null value to the MultiPanelPage.ControlCollection.");
				var p = value as MultiPanelPage;
				if (p != null) throw new ArgumentException("Tried to add a MultiPanelPage control to the MultiPanelPage.ControlCollection.", "value");
				base.Add(value);
			}
		}
	}

	public class MultiPanelPagesCollection : Control.ControlCollection
	{
		public MultiPanelPagesCollection(Control owner) : base(owner)
		{
			if (owner == null) throw new ArgumentNullException("owner", "Tried to create a MultiPanelPagesCollection with a null owner.");
			var owner1 = owner as MultiPanel;
			if (owner1 == null) throw new ArgumentException("Tried to create a MultiPanelPagesCollection with a non-MultiPanel owner.", "owner");
		}

		public override void Add(Control value)
		{
			if (value == null) throw new ArgumentNullException("value", "Tried to add a null value to the MultiPanelPagesCollection.");
			var p = value as MultiPanelPage;
			if (p == null) throw new ArgumentException("Tried to add a non-MultiPanelPage control to the MultiPanelPagesCollection", "value");
			p.SendToBack();
			base.Add(p);
		}

		public override void AddRange(Control[] controls)
		{
			foreach (var p in controls.Cast<MultiPanelPage>())
			{
				Add(p);
			}
		}

		public override int IndexOfKey(string key)
		{
			var ctrl = base[key];
			return GetChildIndex(ctrl);
		}
	}

	[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
	public class MultiPanelDesigner : ParentControlDesigner
	{
		private MultiPanel m_mpanel;
		private DesignerVerbCollection m_verbs;

		public override void Initialize(IComponent component)
		{
			m_mpanel = component as MultiPanel;
			if (m_mpanel == null)
			{
				DisplayError(new ArgumentException("Tried to use the MultiPanelControlDesign with a class that does not inherit from MultiPanel.", "component"));
				return;
			}
			base.Initialize(component);
			var iccs = (IComponentChangeService)GetService(typeof(IComponentChangeService));
			if (iccs != null)
			{
				iccs.ComponentRemoved += ComponentRemoved;
			}
			var s = (ISelectionService)GetService(typeof(ISelectionService));
			if (s != null)
			{
				s.SelectionChanged += s_SelectionChanged;
			}
		}

		/// <summary>
		/// Overridden. Inherited from <see cref="IDesigner.DoDefaultAction()"/>.
		/// </summary>
		public override void DoDefaultAction()
		{
		}

		/// <summary>
		/// Overridden. Inherited from <see cref="ControlDesigner"/>.
		/// </summary>
		public override DesignerVerbCollection Verbs
		{
			get
			{
				return m_verbs ?? (m_verbs = new DesignerVerbCollection
				{
				    new DesignerVerb("Add Page", AddPage),
				    new DesignerVerb("Remove Page", RemovePage),
				    new DesignerVerb("Next Page", NextPage),
				    new DesignerVerb("Prev Page", PrevPage)
				});
			}
		}

		public override ICollection AssociatedComponents
		{
			get { return m_mpanel.Controls; }
		}

		private void AddPage(object sender, EventArgs ea)
		{
			var dh = (IDesignerHost)GetService(typeof(IDesignerHost));
			if (dh == null) return;

			var dt = dh.CreateTransaction("Added new page");
			var before = m_mpanel.SelectedPage;
			var name = GetNewPageName();

			var ytp = dh.CreateComponent(typeof(MultiPanelPage), name) as MultiPanelPage;
			ytp.Text = name;
			m_mpanel.Controls.Add(ytp);
			m_mpanel.SelectedPage = ytp;

			RaiseComponentChanging(TypeDescriptor.GetProperties(Control)["SelectedPage"]);
			RaiseComponentChanged(TypeDescriptor.GetProperties(Control)["SelectedPage"], before, ytp);

			dt.Commit();
		}

		private void RemovePage(object sender, EventArgs ea)
		{
			IDesignerHost dh = (IDesignerHost)GetService(typeof(IDesignerHost));
			if (dh != null)
			{
				DesignerTransaction dt = dh.CreateTransaction("Removed page");

				MultiPanelPage page = m_mpanel.SelectedPage;
				if (page != null)
				{
					MultiPanelPage ytp = m_mpanel.SelectedPage;
					m_mpanel.Controls.Remove(ytp);
					dh.DestroyComponent(ytp);

					if (m_mpanel.Controls.Count > 0)
					{
						m_mpanel.SelectedPage = (MultiPanelPage)m_mpanel.Controls[m_mpanel.Controls.Count - 1];
					}
					else
						m_mpanel.SelectedPage = null;

					RaiseComponentChanging(TypeDescriptor.GetProperties(Control)["SelectedPage"]);
					RaiseComponentChanged(TypeDescriptor.GetProperties(Control)["SelectedPage"], ytp, m_mpanel.SelectedPage);
				}

				dt.Commit();
			}
		}

		private void NextPage(object sender, EventArgs ea)
		{
			IDesignerHost dh = (IDesignerHost)GetService(typeof(IDesignerHost));
			if (dh != null)
			{
				DesignerTransaction dt = dh.CreateTransaction("Selected Next Page");

				m_mpanel.SelectNextPage();

				dt.Commit();
			}
		}

		private void PrevPage(object sender, EventArgs ea)
		{
			IDesignerHost dh = (IDesignerHost)GetService(typeof(IDesignerHost));
			if (dh != null)
			{
				DesignerTransaction dt = dh.CreateTransaction("Selected Prev Page");

				m_mpanel.SelectPrevPage();

				dt.Commit();
			}
		}

		private string GetNewPageName()
		{
			var i = 1;
			var h = new Hashtable(m_mpanel.Controls.Count);
			foreach (Control c in m_mpanel.Controls)
			{
				h[c.Name] = null;
			}
			while (h.ContainsKey("Page_" + i))
			{
				i++;
			}
			return "Page_" + i;
		}

		private void s_SelectionChanged(object sender, EventArgs e)
		{
			var s = (ISelectionService)GetService(typeof(ISelectionService));
			if (s == null) return;
			if (s.PrimarySelection == null) return;

			var page = GetMultiPanelPage((Control)s.PrimarySelection);
			if (page != null) m_mpanel.SelectedPage = page;
		}

		private MultiPanelPage GetMultiPanelPage(Control ctrl)
		{
			var page = ctrl as MultiPanelPage;
			if (page != null)
			{
				return ReferenceEquals(m_mpanel, page.Parent) ? page : null;
			}

			return ctrl.Parent != null 
				? GetMultiPanelPage(ctrl.Parent) 
				: null;
		}

		private void ComponentRemoved(object sender, ComponentEventArgs cea)
		{
		}
	}

	public class MultiPanelPageDesigner : ScrollableControlDesigner
	{
		private readonly Font m_font = new Font("Courier New", 8F, FontStyle.Bold);
		private readonly StringFormat m_rightfmt = new StringFormat(StringFormatFlags.NoWrap | StringFormatFlags.DirectionRightToLeft);

		private MultiPanelPage m_page;

		public string Text
		{
			get { return m_page.Text; }
			set
			{
				m_page.Text = value;
				var iccs = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
				if (iccs == null) return;

				var ytc = m_page.Parent as MultiPanel;
				if (ytc != null) ytc.Refresh();
			}
		}

		protected override void OnPaintAdornments(PaintEventArgs pea)
		{
			base.OnPaintAdornments(pea);

			using (var p = new Pen(SystemColors.ControlDark, 1))
			{
				p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
				pea.Graphics.DrawRectangle(p, 0, 0, m_page.Width - 1, m_page.Height - 1);
			}

			using (Brush b = new SolidBrush(Color.FromArgb(100, Color.Black)))
			{
				var fh = m_font.GetHeight(pea.Graphics);
				var tleft = new RectangleF(0, 0, m_page.Width / 2f, fh);
				var bleft = new RectangleF(0, m_page.Height - fh, m_page.Width / 2f, fh);
				var tright = new RectangleF(m_page.Width / 2f, 0, m_page.Width / 2f, fh);
				var bright = new RectangleF(m_page.Width / 2f, m_page.Height - fh, m_page.Width / 2f, fh);
				pea.Graphics.DrawString(m_page.Text, m_font, b, tleft);
				pea.Graphics.DrawString(m_page.Text, m_font, b, bleft);
				pea.Graphics.DrawString(m_page.Text, m_font, b, tright, m_rightfmt);
				pea.Graphics.DrawString(m_page.Text, m_font, b, bright, m_rightfmt);
			}
		}

		public override void Initialize(IComponent component)
		{
			m_page = component as MultiPanelPage;
			if (m_page == null) DisplayError(new Exception("You attempted to use a MultiPanelPageDesigner with a class that does not inherit from MultiPanelPage."));
			base.Initialize(component);
		}

		protected override void PreFilterProperties(IDictionary properties)
		{
			base.PreFilterProperties(properties);
			properties["Text"] = TypeDescriptor.CreateProperty(typeof(MultiPanelPageDesigner), (PropertyDescriptor)properties["Text"]);
		}
	}
}
