using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using JetBrains.Annotations;
using NCore;
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
		private BlockType m_currentStringBlock = BlockType.Block1;

		public StringEditorTabPage()
		{
			InitializeComponent();
			InitializeControlls();
		}

		[NotNull]
		public ListBox StringListBox
		{
			get
			{
				switch (m_currentStringBlock)
				{
					case BlockType.Block1: return Block1StringListBox;
					case BlockType.Block2: return Block2StringListBox;
					default: throw new ArgumentOutOfRangeException();
				}
			}
		}

		public int LastSelectedStringListBoxItemIndex
		{
			get
			{
				return StringListBox.Items.Count == 0 || StringListBox.SelectedIndices.Count == 0
					? 0
					: StringListBox.SelectedIndices[StringListBox.SelectedIndices.Count - 1];
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

		public BlockType SelectedImageBlock
		{
			get { return (BlockType)BlockTypeComboBox.SelectedItem; }
		}

		public IDictionary<int, FirmwareImageMetadata> CurrentImageBlockForStrings
		{
			get
			{
				switch (SelectedImageBlock)
				{
					case BlockType.Block1: return m_firmware.Block1Images;
					case BlockType.Block2: return m_firmware.Block2Images;
					default: throw new ArgumentOutOfRangeException();
				}
			}
		}

		#region Implementation of IEditorTabPage
		public bool IsDirty { get; set; }

		public string Title
		{
			get { return "Strings"; }
		}

		public void Initialize(IEditorTabPageHost host, ApplicationConfiguration configuration)
		{
		}

		public void OnWorkspaceReset()
		{
			BlockTypeComboBox.SelectedIndex = 0;
			BlockTypeComboBox.Enabled = BlockTypeComboBox.Visible = false;

			Block1StringListBox.Items.Clear();
			Block2StringListBox.Items.Clear();

			RemoveStringEditControls();
			Block1StringRadioButton.Enabled = false;
			Block2StringRadioButton.Enabled = false;
			Block1StringRadioButton.Checked = false;
			Block2StringRadioButton.Checked = false;
			StringPrewviewPixelGrid.Data = new bool[0, 0];
		}

		public void OnFirmwareLoaded(Firmware firmware)
		{
			m_firmware = firmware;

			BlockTypeComboBox.Enabled = BlockTypeComboBox.Visible = firmware.Block2Images.Any();
			StringPreviewGroupPanel.Text = BlockTypeComboBox.Enabled ? "Preview using:" : "Preview:";

			Block1StringRadioButton.Enabled = true;
			Block1StringRadioButton.Checked = true;
			Block2StringRadioButton.Enabled = m_firmware.Block2Strings.Any();

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

		private void InitializeControlls()
		{
			StringStatusLabel.Text = null;

			BlockTypeComboBox.Items.Clear();
			BlockTypeComboBox.Items.AddRange(new object[] { BlockType.Block1, BlockType.Block2 });
			BlockTypeComboBox.SelectedIndex = 0;
			BlockTypeComboBox.SelectedIndexChanged += BlockTypeComboBox_SelectedIndexChanged;

			StringPrewviewPixelGrid.BlockInnerBorderPen = Pens.Transparent;
			StringPrewviewPixelGrid.BlockOuterBorderPen = Pens.Transparent;
			StringPrewviewPixelGrid.ActiveBlockBrush = Brushes.White;
			StringPrewviewPixelGrid.InactiveBlockBrush = Brushes.Black;

			Block1StringRadioButton.CheckedChanged += BlockStringRadioButton_CheckedChanged;
			Block2StringRadioButton.CheckedChanged += BlockStringRadioButton_CheckedChanged;

			Block1StringListBox.SelectedValueChanged += StringListBox_SelectedValueChanged;
			Block1StringListBox.DrawMode = DrawMode.OwnerDrawVariable;
			Block1StringListBox.MeasureItem += StringListBox_MeasureItem;
			Block1StringListBox.DrawItem += StringListBox_DrawItem;

			Block2StringListBox.SelectedValueChanged += StringListBox_SelectedValueChanged;
			Block2StringListBox.DrawMode = DrawMode.OwnerDrawVariable;
			Block2StringListBox.MeasureItem += StringListBox_MeasureItem;
			Block2StringListBox.DrawItem += StringListBox_DrawItem;
		}

		private void BlockTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (m_firmware == null) return;

			StringListBox.Invalidate();
			StringListBox_SelectedValueChanged(StringListBox, EventArgs.Empty);
		}

		private void CreateStringEditControls(IList<short> firmwareString, FirmwareStringMetadata stringMetadata)
		{
			var nullByteFound = false;
			for (var i = 0; i < firmwareString.Count; i++)
			{
				if (i == firmwareString.Count - 1 && firmwareString[i] == 0x00) continue;

				var stringChar = firmwareString[i];
				var nullItem = new ImagedItem<short>(0, 0, "NULL");
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
				foreach (var imageMetadata in CurrentImageBlockForStrings.Values)
				{
					if (!stringMetadata.TwoByteChars && imageMetadata.Index > 0xFF) continue;

					var item = new ImagedItem<short>((short)imageMetadata.Index, imageMetadata.Index, string.Format("0x{0:X2}", imageMetadata.Index));
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

			var stringData = m_firmware.ReadString(LastSelectedStringMetadata);
			var charsData = new Dictionary<int, bool[,]>();
			foreach (var b in stringData)
			{
				if (charsData.ContainsKey(b)) continue;

				FirmwareImageMetadata imageMetadata;
				if (!CurrentImageBlockForStrings.TryGetValue(b, out imageMetadata)) continue;

				charsData[b] = m_firmware.ReadImage(imageMetadata);
			}

			var imageData = FirmwareImageProcessor.GetStringImageData(stringData, charsData,
				m_firmware.Definition.StringsPreviewCorrection != null ? m_firmware.Definition.StringsPreviewCorrection.ForGlyphs : null);
			var imageDataSize = imageData.GetSize();

			StringPrewviewPixelGrid.Data = imageData;
			StringStatusLabel.Text = string.Format
			(
				"String: 0x{0:X2}, Size: {1}x{2}, Data: 0x{3:X4}, Length: {4} bytes",
				LastSelectedStringMetadata.Index,
				imageDataSize.Width,
				imageDataSize.Height,
				LastSelectedStringMetadata.DataOffset,
				LastSelectedStringMetadata.DataLength
			);
		}

		private void BlockStringRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if (sender == Block1StringRadioButton)
			{
				m_currentStringBlock = BlockType.Block1;
				Block1StringListBox.Visible = true;
				Block2StringListBox.Visible = false;
			}
			if (sender == Block2StringRadioButton)
			{
				m_currentStringBlock = BlockType.Block2;
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
				InfoBox.Global.Show("Invalid string data.");
			}
		}

		private void Icb_SelectedValueChanged(object sender, EventArgs e)
		{
			var icb = sender as ComboBox;
			if (icb == null) return;

			var tag = icb.Tag as Tuple<FirmwareStringMetadata, int>;
			var item = icb.SelectedItem as ImagedItem<short>;

			if (tag == null) return;
			if (item == null) return;

			var value = item.Value;
			var idx = CharLayoutPanel.Controls.IndexOf(icb);

			m_firmware.WriteChar(value, tag.Item2, tag.Item1);
			var itemRect = StringListBox.GetItemRectangle(LastSelectedStringListBoxItemIndex);
			StringListBox.Invalidate(itemRect);
			ImageCacheManager.RebuildStringImageCache(m_firmware, BlockType.Block1);
			
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

			IsDirty = true;
		}

		private void Icb_DrawItem(object sender, DrawItemEventArgs e)
		{
			var comboBox = sender as ComboBox;

			if (comboBox == null) return;
			if (e.Index < 0) return;

			var stringMetadata = comboBox.Tag as Tuple<FirmwareStringMetadata, int>;
			if (stringMetadata == null) return;

			var item = comboBox.Items[e.Index] as ImagedItem<short>;
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
				var image = ImageCacheManager.GetGlyphImage(item.ImageCacheIndex, SelectedImageBlock);

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

			var item = comboBox.Items[e.Index] as ImagedItem<short>;
			if (item == null) return;

			try
			{
				var cachedImage = ImageCacheManager.GetGlyphImage(item.ImageCacheIndex, SelectedImageBlock);
				e.ItemHeight = Math.Min(e.ItemHeight, cachedImage.Height + Consts.ImageListBoxItemImageMargin);
			}
			catch (ObjectDisposedException)
			{
				// Ignore
			}
		}

		private void StringListBox_DrawItem(object sender, DrawItemEventArgs e)
		{
			var listBox = sender as ListBox;

			if (listBox == null) return;
			if (e.Index < 0) return;

			var item = listBox.Items[e.Index] as FirmwareStringMetadata;
			if (item == null) return;

			e.Graphics.SmoothingMode = SmoothingMode.None;
			e.Graphics.InterpolationMode = InterpolationMode.Low;
			e.Graphics.CompositingQuality = CompositingQuality.HighSpeed;
			e.DrawBackground();

			//var itemText = item.ToString();

			try
			{
				var imageScale = 1f;
				var image = ImageCacheManager.GetStringImage(item.Index, SelectedImageBlock);

				/*var greatestDimension = Math.Max(image.Width, image.Height);
				if (greatestDimension > Consts.ImageListBoxItemMaxHeight) imageScale = (float)greatestDimension / Consts.ImageListBoxItemMaxHeight;*/

				var resultWidth = image.Width / imageScale;
				var resultHeight = image.Height / imageScale;

				e.Graphics.DrawImage(image, e.Bounds.X + Consts.ImageListBoxItemImageMargin, e.Bounds.Y + (int)(e.Bounds.Height / 2f - resultHeight / 2f), resultWidth, resultHeight);
			}
			catch (ObjectDisposedException)
			{
				// Ignore
			}

			//var stringRectX = e.Bounds.X + Consts.ImageListBoxItemMaxHeight + Consts.ImageListBoxItemImageMargin * 2;
			//e.Graphics.DrawString(itemText, e.Font, new SolidBrush(e.ForeColor), new RectangleF(stringRectX, e.Bounds.Y, e.Bounds.Width - stringRectX - Consts.ImageListBoxItemImageMargin, e.Bounds.Height), m_listBoxStringFormat);
			e.DrawFocusRectangle();
		}

		private void StringListBox_MeasureItem(object sender, MeasureItemEventArgs e)
		{
			e.ItemHeight = Consts.ImageListBoxItemMaxHeight + Consts.ImageListBoxItemImageMargin;

			var listBox = sender as ListBox;

			if (listBox == null) return;
			if (e.Index < 0) return;

			var item = listBox.Items[e.Index] as FirmwareStringMetadata;
			if (item == null) return;

			try
			{
				var cachedImage = ImageCacheManager.GetStringImage(item.Index, SelectedImageBlock);
				e.ItemHeight = Math.Min(e.ItemHeight, cachedImage.Height + Consts.ImageListBoxItemImageMargin);
			}
			catch (ObjectDisposedException)
			{
				// Ignore
			}
		}
	}
}
