using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JetBrains.Annotations;
using NFirmware;
using NFirmwareEditor.Core;
using NFirmwareEditor.Managers;

namespace NFirmwareEditor.Windows
{
	internal partial class ImportFontWindow : EditorDialogWindow
	{
		private class LetterBox
		{
			public LetterBox(FirmwareImageMetadata metadata, Rectangle rect)
			{
				Metadata = metadata;
				Rect = rect;
			}

			public FirmwareImageMetadata Metadata { get; private set; }

			public Rectangle Rect { get; private set; }

			public string Char { get; set; }
		}

		private const int PixelMultiplier = 10;
		private const int Offset = 2;

		private readonly IList<LetterBox> m_letters = new List<LetterBox>();

		private TextureBrush m_fontPreviewBackgroundBrush;
		private Font m_font = new Font("Tahoma", 8);
		private FontFamily m_externalFontFamily;
		private string m_fontName;
		private float m_fontSize;
		private FontStyle m_fontStyle;
		private int m_letterOffsetX;
		private int m_letterOffsetY;

		public ImportFontWindow()
		{
			InitializeComponent();
			InitializeControls();
		}

		public ImportFontWindow([NotNull] IEnumerable<FirmwareImageMetadata> imageMetadatas) : this()
		{
			if (imageMetadatas == null) throw new ArgumentNullException("imageMetadatas");

			var firmwareImageMetadatas = imageMetadatas as IList<FirmwareImageMetadata> ?? imageMetadatas.ToList();
			var offsetX = Offset;
			var maxHeight = firmwareImageMetadatas.Max(x => x.Height);
			foreach (var metadata in firmwareImageMetadatas)
			{
				var offsetY = maxHeight - metadata.Height;
				var rect = new Rectangle(offsetX, Offset + offsetY, metadata.Width + 1, metadata.Height + 1);
				offsetX += rect.Width + Offset;
				m_letters.Add(new LetterBox(metadata, rect));
			}
		}

		public IList<Tuple<FirmwareImageMetadata, bool[,]>> GetImportedData()
		{
			var result = new List<Tuple<FirmwareImageMetadata, bool[,]>>();
			using (var surface = DrawSurfaceInRealScale())
			{
				foreach (var letterBox in m_letters)
				{
					var metadata = letterBox.Metadata;
					var rect = letterBox.Rect;

					var cropRect = new Rectangle(rect.X + 1, rect.Y + 1, rect.Width - 1, rect.Height - 1);
					using (var croppedLetter = FirmwareImageProcessor.CropImage(surface, cropRect))
					{
						var imageData = FirmwareImageProcessor.ImportBitmap(croppedLetter, 0x00);
						result.Add(new Tuple<FirmwareImageMetadata, bool[,]>(metadata, imageData));
					}
				}
			}
			return result;
		}

		private void InitializeControls()
		{
			foreach (var font in FontFamily.Families)
			{
				FontComboBox.Items.Add(font.Name);
			}
			var tahomaIndex = FontComboBox.Items.IndexOf("Tahoma");
			if (tahomaIndex != -1)
			{
				FontComboBox.SelectedIndex = tahomaIndex;
			}
			else if (FontComboBox.Items.Count > 0)
			{
				FontComboBox.SelectedIndex = 0;
			}

			FontSizeUpDown.ValueChanged += (s, e) =>
			{
				m_fontSize = (float)FontSizeUpDown.Value;
				m_font = CreateFont();
				FontPreviewSurface.Invalidate();
			};
			FontComboBox.TextChanged += (s, e) =>
			{
				m_fontName = FontComboBox.Text;
				m_externalFontFamily = null;
				m_font = CreateFont();
				FontPreviewSurface.Invalidate();
			};
			LettersTextBox.TextChanged += LettersTextBox_TextChanged;

			LoadFontButton.Click += LoadFontButton_Click;

			BoldButton.CheckedChanged += FontStyleButton_Click;
			ItalicButton.CheckedChanged += FontStyleButton_Click;
			UnderlineButton.CheckedChanged += FontStyleButton_Click;

			ShiftUpButton.Click += AdjustFontPosition;
			ShiftDownButton.Click += AdjustFontPosition;
			ShiftLeftButton.Click += AdjustFontPosition;
			ShiftRightButton.Click += AdjustFontPosition;

			ZeroNineButton.Click += (s, e) => LettersTextBox.Text = GetLetters('0', '9');
			LowerAZButton.Click += (s, e) => LettersTextBox.Text = GetLetters('a', 'z');
			UpperAZButton.Click += (s, e) => LettersTextBox.Text = GetLetters('A', 'Z');

			OkButton.Click += OkButton_Click;
			FontPreviewSurface.Paint += FontPreviewSurface_Paint;
			FontPreviewSurface.Resize += (s, e) => FontPreviewSurface.Invalidate();

			m_fontName = FontComboBox.Text;
			m_fontSize = (float)FontSizeUpDown.Value;
			m_font = CreateFont();
		}

		private Font CreateFont()
		{
			return m_externalFontFamily != null
				? new Font(m_externalFontFamily, m_fontSize, m_fontStyle)
				: new Font(m_fontName, m_fontSize, m_fontStyle);
		}

		private string GetLetters(char fromChar, char toChar)
		{
			var sb = new StringBuilder();
			for (var i = (int)fromChar; i <= toChar; i++)
			{
				sb.Append((char)i);
			}
			return sb.ToString();
		}

		private void FontStyleButton_Click(object sender, EventArgs eventArgs)
		{
			var newStyle = FontStyle.Regular;

			if (BoldButton.Checked) newStyle |= FontStyle.Bold;
			if (ItalicButton.Checked) newStyle |= FontStyle.Italic;
			if (UnderlineButton.Checked) newStyle |= FontStyle.Underline;

			m_fontStyle = newStyle;
			m_font = CreateFont();
			FontPreviewSurface.Invalidate();
		}

		private void LoadFontButton_Click(object sender, EventArgs e)
		{
			string fileName;
			using (var op = new OpenFileDialog { Title = @"Select font file...", Filter = Consts.FontImportFilter })
			{
				if (op.ShowDialog() != DialogResult.OK) return;
				fileName = op.FileName;
			}

			try
			{
				var pfc = new PrivateFontCollection();
				var fontBytes = File.ReadAllBytes(fileName);
				unsafe
				{
					fixed (byte* pFontData = fontBytes)
					{
						pfc.AddMemoryFont((IntPtr)pFontData, fontBytes.Length);
					}
				}
				m_externalFontFamily = pfc.Families.First(x => x.IsStyleAvailable(FontStyle.Regular));
				m_font = CreateFont();
				FontPreviewSurface.Invalidate();
			}
			catch (Exception ex)
			{
				InfoBox.Show("An error occured during loading font file.\n" + ex.Message);
			}
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			base.ProcessCmdKey(ref msg, keyData);
			if (!ModifierKeys.HasFlag(Keys.Control)) return false;

			var key = keyData & ~Keys.Control;
			switch (key)
			{
				case Keys.Up:
					AdjustFontPosition(ShiftUpButton, EventArgs.Empty);
					return true;
				case Keys.Down:
					AdjustFontPosition(ShiftDownButton, EventArgs.Empty);
					return true;
				case Keys.Left:
					AdjustFontPosition(ShiftLeftButton, EventArgs.Empty);
					return true;
				case Keys.Right:
					AdjustFontPosition(ShiftRightButton, EventArgs.Empty);
					return true;
				case Keys.Oemplus:
				case Keys.Add:
					FontSizeUpDown.Value += 1;
					return true;
				case Keys.OemMinus:
				case Keys.Subtract:
					FontSizeUpDown.Value -= 1;
					return true;
			}
			return false;
		}

		private void LettersTextBox_TextChanged(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(LettersTextBox.Text))
			{
				m_letters.ForEach(x => x.Char = string.Empty);
				OkButton.Enabled = false;
			}
			else
			{
				var letters = LettersTextBox.Text;
				var minCount = Math.Min(m_letters.Count, letters.Length);
				for (var i = 0; i < minCount; i++)
				{
					m_letters[i].Char = letters[i].ToString();
				}

				if (letters.Length < m_letters.Count)
				{
					m_letters.Skip(letters.Length).ForEach(x => x.Char = string.Empty);
				}

				OkButton.Enabled = true;
			}
			FontPreviewSurface.Invalidate();
		}

		private void AdjustFontPosition(object sender, EventArgs e)
		{
			if (sender == ShiftUpButton) m_letterOffsetY--;
			if (sender == ShiftDownButton) m_letterOffsetY++;
			if (sender == ShiftLeftButton) m_letterOffsetX--;
			if (sender == ShiftRightButton) m_letterOffsetX++;

			FontPreviewSurface.Invalidate();
		}

		private Image DrawSurfaceInRealScale()
		{
			var buffer = new Bitmap(m_letters.Sum(x => x.Rect.Width) + (m_letters.Count + 1) * Offset, m_letters.Max(x => x.Rect.Height) + Offset * 2);
			{
				using (var gfx = Graphics.FromImage(buffer))
				{
					gfx.Clear(Color.Transparent);
					gfx.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;

					foreach (var letterBox in m_letters)
					{
						gfx.FillRectangle(Brushes.White, letterBox.Rect);
						gfx.DrawRectangle(Pens.LightGray, letterBox.Rect);

						if (string.IsNullOrEmpty(letterBox.Char)) continue;
						try
						{
							gfx.DrawString(letterBox.Char, m_font, Brushes.Black, letterBox.Rect.X + m_letterOffsetX, letterBox.Rect.Y + m_letterOffsetY, StringFormat.GenericTypographic);
						}
						catch
						{
							// Ignore
						}
					}
				}
				return buffer;
			}
		}

		private void OkButton_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
		}

		private void FontPreviewSurface_Paint(object sender, PaintEventArgs e)
		{
			using (var buffer = DrawSurfaceInRealScale())
			{
				var newSize = new Size(buffer.Width * PixelMultiplier, buffer.Height * PixelMultiplier);
				if (FontPreviewSurface.AutoScrollMinSize != newSize) FontPreviewSurface.AutoScrollMinSize = newSize;

				e.Graphics.TranslateTransform(-FontPreviewSurface.HorizontalScroll.Value, -FontPreviewSurface.VerticalScroll.Value);

				if (FontPreviewSurface.BackgroundImage != null)
				{
					if (m_fontPreviewBackgroundBrush == null)
					{
						m_fontPreviewBackgroundBrush = new TextureBrush(FontPreviewSurface.BackgroundImage) { WrapMode = WrapMode.Tile };
					}
					e.Graphics.FillRectangle
					(
						m_fontPreviewBackgroundBrush,
						0,
						0,
						FontPreviewSurface.Width + FontPreviewSurface.HorizontalScroll.Value,
						FontPreviewSurface.Height + FontPreviewSurface.VerticalScroll.Value
					);
				}

				e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
				e.Graphics.DrawImage(buffer, 0, 0, buffer.Width * PixelMultiplier, buffer.Height * PixelMultiplier);
			}
		}
	}
}
