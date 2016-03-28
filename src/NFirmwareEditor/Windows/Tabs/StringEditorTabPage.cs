using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using JetBrains.Annotations;
using NFirmware;
using NFirmwareEditor.Core;
using NFirmwareEditor.Managers;
using NFirmwareEditor.Models;
using NFirmwareEditor.UI;

namespace NFirmwareEditor.Windows.Tabs
{
	internal partial class StringEditorTabPage : UserControl, IEditorTabPage
	{
		private readonly StringFormat m_listBoxStringFormat = new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center };

		private Firmware m_firmware;
		private BlockType m_currentBlock = BlockType.Block1;

		public StringEditorTabPage()
		{
			InitializeComponent();
		}

		[NotNull]
		public ListBox StringListBox
		{
			get
			{
				switch (m_currentBlock)
				{
					case BlockType.Block1: return Block1StringListBox;
					case BlockType.Block2: return Block2StringListBox;
					default: throw new ArgumentOutOfRangeException();
				}
			}
		}

		[CanBeNull]
		public FirmwareStringMetadata LastSelectedStringMetadata
		{
			get
			{
				return StringListBox.Items.Count == 0 || StringListBox.SelectedIndices.Count == 0
					? null
					: StringListBox.Items[StringListBox.SelectedIndices[StringListBox.SelectedIndices.Count - 1]] as FirmwareStringMetadata;
			}
		}

		public IEnumerable<FirmwareImageMetadata> CurrentImageBlockForStrings
		{
			get
			{
				switch (m_currentBlock)
				{
					case BlockType.Block1: return m_firmware.Block1Images;
					case BlockType.Block2: return m_firmware.Block2Images;
					default: throw new ArgumentOutOfRangeException();
				}
			}
		}

		#region Implementation of IEditorTabPage
		public string Title
		{
			get { return "Strings"; }
		}

		public void Initialize(Configuration configuration)
		{
			StringPrewviewPixelGrid.BlockInnerBorderPen = Pens.Transparent;
			StringPrewviewPixelGrid.BlockOuterBorderPen = Pens.Transparent;
			StringPrewviewPixelGrid.ActiveBlockBrush = Brushes.White;
			StringPrewviewPixelGrid.InactiveBlockBrush = Brushes.Black;

			Block1StringRadioButton.CheckedChanged += BlockStringRadioButton_CheckedChanged;
			Block2StringRadioButton.CheckedChanged += BlockStringRadioButton_CheckedChanged;

			Block1StringListBox.SelectedValueChanged += StringListBox_SelectedValueChanged;
			Block2StringListBox.SelectedValueChanged += StringListBox_SelectedValueChanged;
		}

		public void OnWorkspaceReset()
		{
			Block1StringListBox.Items.Clear();
			Block2StringListBox.Items.Clear();
			RemoveStringEditControls();
			Block1StringRadioButton.Checked = true;
			StringPrewviewPixelGrid.Data = new bool[5, 5];
		}

		public void OnFirmwareLoaded(Firmware firmware)
		{
			m_firmware = firmware;

			Block2StringListBox.Fill(m_firmware.Block2Strings, false);
			Block1StringListBox.Fill(m_firmware.Block1Strings, false);
		}

		public void OnActivate()
		{
			StringListBox.Focus();
			UpdateStringPreview();
		}

		public bool OnHotkey(Keys keyData)
		{
			return false;
		}
		#endregion

		private void CreateStringEditControls(byte[] firmwareString, FirmwareStringMetadata stringMetadata)
		{
			var nullByteFound = false;
			for (var i = 0; i < firmwareString.Length; i++)
			{
				if (i == firmwareString.Length - 1 && firmwareString[i] == 0x00) continue;

				var stringChar = firmwareString[i];
				var nullItem = new ImagedItem<byte>(0, 0, "NULL");
				var icb = new ComboBox
				{
					Width = 200,
					ItemHeight = 30,
					DropDownStyle = ComboBoxStyle.DropDownList,
					DrawMode = DrawMode.OwnerDrawVariable,
					BackColor = Color.Black,
					ForeColor = Color.White,
					Font = new Font("Consolas", 8.25f),
					Tag = new Tuple<FirmwareStringMetadata, int>(stringMetadata, i)
				};
				if (i > 0) icb.Items.Add(nullItem);
				var selectedItem = nullItem;
				foreach (var imageMetadata in CurrentImageBlockForStrings)
				{
					var item = new ImagedItem<byte>((byte)imageMetadata.Index, imageMetadata.Index, string.Format("0x{0:X2}", imageMetadata.Index));
					icb.Items.Add(item);
					if (imageMetadata.Index == stringChar)
					{
						selectedItem = item;
					}
				}
				icb.SelectedItem = selectedItem;
				icb.SelectedValueChanged += Icb_SelectedValueChanged;
				icb.MeasureItem += Icb_MeasureItem;
				icb.DrawItem += Icb_DrawItem;
				icb.Enabled = !nullByteFound;
				CharLayoutPanel.Controls.Add(icb);
				nullByteFound = selectedItem.Value == 0x00;
			}
		}

		private void RemoveStringEditControls()
		{
			foreach (var icb in CharLayoutPanel.Controls.OfType<ComboBox>())
			{
				icb.SelectedValueChanged -= Icb_SelectedValueChanged;
				icb.MeasureItem -= Icb_MeasureItem;
				icb.DrawItem -= Icb_DrawItem;
			}
			CharLayoutPanel.Controls.Clear();
		}

		private void UpdateStringPreview()
		{
			if (LastSelectedStringMetadata == null) return;

			var firmwareString = m_firmware.ReadString(LastSelectedStringMetadata);
			var charMetadatas = new List<FirmwareImageMetadata>();
			foreach (var charIndex in firmwareString)
			{
				var metadata = CurrentImageBlockForStrings.FirstOrDefault(x => x.Index == charIndex);
				if (metadata != null) charMetadatas.Add(metadata);
			}
			var images = m_firmware.ReadImages(charMetadatas).ToList();
			var data = FirmwareImageProcessor.MergeImages(images);

			StringPrewviewPixelGrid.Data = data;
		}

		private void BlockStringRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if (sender == Block1StringRadioButton)
			{
				m_currentBlock = BlockType.Block1;
				Block1StringListBox.Visible = true;
				Block2StringListBox.Visible = false;
			}
			if (sender == Block2StringRadioButton)
			{
				m_currentBlock = BlockType.Block2;
				Block1StringListBox.Visible = false;
				Block2StringListBox.Visible = true;
			}

			StringListBox.Focus();
		}

		private void StringListBox_SelectedValueChanged(object sender, EventArgs e)
		{
			if (LastSelectedStringMetadata == null) return;

			try
			{
				var firmwareString = m_firmware.ReadString(LastSelectedStringMetadata);

				CharLayoutPanel.Visible = false;
				RemoveStringEditControls();
				CreateStringEditControls(firmwareString, LastSelectedStringMetadata);
				CharLayoutPanel.Visible = true;
				UpdateStringPreview();
			}
			catch
			{
				InfoBox.Show("Invalid string data.");
			}
		}

		private void Icb_SelectedValueChanged(object sender, EventArgs e)
		{
			var icb = sender as ComboBox;
			if (icb == null) return;

			var tag = icb.Tag as Tuple<FirmwareStringMetadata, int>;
			var item = icb.SelectedItem as ImagedItem<byte>;

			if (tag == null) return;
			if (item == null) return;

			var value = item.Value;
			var idx = CharLayoutPanel.Controls.IndexOf(icb);

			m_firmware.WriteChar(value, tag.Item2, tag.Item1);
			UpdateStringPreview();

			if (value == 0x00)
			{
				for (var i = idx + 1; i < CharLayoutPanel.Controls.Count; i++)
				{
					var relatedIcb = CharLayoutPanel.Controls[i] as ComboBox;
					if (relatedIcb == null) continue;

					relatedIcb.SelectedIndex = 0;
					relatedIcb.Enabled = false;
				}
			}
			else if (idx + 1 < CharLayoutPanel.Controls.Count)
			{
				var relatedIcb = CharLayoutPanel.Controls[idx + 1] as ComboBox;
				if (relatedIcb == null) return;

				if (relatedIcb.SelectedIndex == 0 && !relatedIcb.Enabled)
				{
					relatedIcb.Enabled = true;
				}
			}
		}

		private void Icb_DrawItem(object sender, DrawItemEventArgs e)
		{
			var comboBox = sender as ComboBox;

			if (comboBox == null) return;
			if (e.Index < 0) return;

			var stringMetadata = comboBox.Tag as Tuple<FirmwareStringMetadata, int>;
			if (stringMetadata == null) return;

			var item = comboBox.Items[e.Index] as ImagedItem<byte>;
			if (item == null) return;

			if (e.Index < 0) return;

			e.Graphics.SmoothingMode = SmoothingMode.None;
			e.Graphics.InterpolationMode = InterpolationMode.Low;
			e.Graphics.CompositingQuality = CompositingQuality.HighSpeed;
			e.DrawBackground();

			var itemText = item.ToString();

			try
			{
				var imageScale = 1f;
				var image = ImageCacheManager.GetImage(item.ImageCacheIndex, stringMetadata.Item1.BlockType);

				var greatestDimension = Math.Max(image.Width, image.Height);
				if (greatestDimension > Consts.ImageListBoxItemMaxHeight) imageScale = (float)greatestDimension / Consts.ImageListBoxItemMaxHeight;

				var resultWidth = image.Width / imageScale;
				var resultHeight = image.Height / imageScale;

				e.Graphics.DrawImage(image, e.Bounds.X + Consts.ImageListBoxItemImageMargin, e.Bounds.Y + (int)(e.Bounds.Height / 2f - resultHeight / 2f), resultWidth, resultHeight);
			}
			catch (ObjectDisposedException)
			{
				// Ignore
			}

			var stringRectX = e.Bounds.X + Consts.ImageListBoxItemMaxHeight + Consts.ImageListBoxItemImageMargin * 2;
			e.Graphics.DrawString
			(
				itemText,
				e.Font,
				new SolidBrush(e.ForeColor),
				new RectangleF(stringRectX, e.Bounds.Y, e.Bounds.Width - stringRectX - Consts.ImageListBoxItemImageMargin, e.Bounds.Height),
				m_listBoxStringFormat
			);
			e.DrawFocusRectangle();
		}

		private void Icb_MeasureItem(object sender, MeasureItemEventArgs e)
		{
			e.ItemHeight = Consts.ImageListBoxItemMaxHeight + Consts.ImageListBoxItemImageMargin;

			var comboBox = sender as ComboBox;

			if (comboBox == null) return;
			if (e.Index < 0) return;

			var stringMetadata = comboBox.Tag as Tuple<FirmwareStringMetadata, int>;
			if (stringMetadata == null) return;

			var item = comboBox.Items[e.Index] as ImagedItem<byte>;
			if (item == null) return;

			try
			{
				var cachedImage = ImageCacheManager.GetImage(item.ImageCacheIndex, stringMetadata.Item1.BlockType);
				e.ItemHeight = Math.Min(e.ItemHeight, cachedImage.Height + Consts.ImageListBoxItemImageMargin);
			}
			catch (ObjectDisposedException)
			{
				// Ignore
			}
		}
	}
}
