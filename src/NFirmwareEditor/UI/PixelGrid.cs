using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace NFirmwareEditor.UI
{
	public sealed class PixelGrid : ScrollableControl
	{
		private const int StackBufferMaxSize = 128;
		private readonly ConcurrentStack<bool[,]> m_undoBuffer = new ConcurrentStack<bool[,]>();
		private readonly ConcurrentStack<bool[,]> m_redoBuffer = new ConcurrentStack<bool[,]>();

		private bool[,] m_data = new bool[0, 0];
		private int m_blockSize = 16;
		private int m_colsCount;
		private int m_rowsCount;
		private Pen m_blockOuterBorderPen = Pens.DarkGray;
		private Pen m_blockInnerBorderPen = new Pen(Color.LightGray) { DashStyle = DashStyle.Dash };
		private Brush m_activeBlockBrush = new SolidBrush(Color.Black);
		private Brush m_inactiveBlockBrush = new SolidBrush(Color.White);
		private TextureBrush m_backgroundBrush;
		private Point? m_lastCursorPosition;

		private Rectangle m_clientArea;
		private bool m_showGrid;

		private bool[,] m_previousData;

		public PixelGrid()
		{
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
			SetStyle(ControlStyles.Selectable, false);
			UpdateStyles();
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool[,] Data
		{
			get { return m_data; }
			set
			{
				m_data = value;
				m_colsCount = Data.GetLength(0);
				m_rowsCount = Data.GetLength(1);
				CalculateSurfaceArea();
				Invalidate();
			}
		}

		public int BlockSize
		{
			get { return m_blockSize; }
			set
			{
				m_blockSize = value;
				CalculateSurfaceArea();
				Invalidate();
			}
		}

		public bool ShowGrid
		{
			get { return m_showGrid; }
			set
			{
				m_showGrid = value;
				Invalidate();
			}
		}

		public bool SingleMouseButtonMode { get; set; }

		public bool ReadOnly { get; set; }

		public Pen BlockOuterBorderPen
		{
			get { return m_blockOuterBorderPen; }
			set
			{
				m_blockOuterBorderPen = value;
				Invalidate();
			}
		}

		public Pen BlockInnerBorderPen
		{
			get { return m_blockInnerBorderPen; }
			set
			{
				m_blockInnerBorderPen = value;
				Invalidate();
			}
		}

		public Brush ActiveBlockBrush
		{
			get { return m_activeBlockBrush; }
			set
			{
				m_activeBlockBrush = value;
				Invalidate();
			}
		}

		public Brush InactiveBlockBrush
		{
			get { return m_inactiveBlockBrush; }
			set
			{
				m_inactiveBlockBrush = value;
				Invalidate();
			}
		}

		public event DataUpdatedDelegate DataUpdated;

		public event CursorPositionChangedDelegate CursorPositionChanged;

		public void ClearHistory()
		{
			m_undoBuffer.Clear();
			m_redoBuffer.Clear();
			m_previousData = null;
		}

		public void CreateUndo()
		{
			Push(m_undoBuffer, m_data);
		}

		public void Undo()
		{
			if (m_undoBuffer.Count <= 0) return;

			m_previousData = (bool[,])m_data.Clone();
			if (m_undoBuffer.TryPop(out m_data))
			{
				Push(m_redoBuffer, m_previousData);
				OnDataUpdated(m_data);
				Invalidate();
			}
		}

		public void Redo()
		{
			if (m_redoBuffer.Count <= 0) return;

			m_previousData = (bool[,])m_data.Clone();
			if (m_redoBuffer.TryPop(out m_data))
			{
				Push(m_undoBuffer, m_previousData);
				OnDataUpdated(m_data);
				Invalidate();
			}
		}

		private void Push(ConcurrentStack<bool[,]> stack, bool[,] data)
		{
			if (data == null) return;

			while (stack.Count == StackBufferMaxSize)
			{
				bool[,] trash;
				stack.TryPop(out trash);
			}
			stack.Push(data);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			var gfx = e.Graphics;
			{
				gfx.Clear(BackColor);
				gfx.TranslateTransform(-HorizontalScroll.Value, -VerticalScroll.Value);
				gfx.TranslateTransform(Margin.Left, Margin.Top);

				DrawBackground(gfx);
				DrawBlocks(gfx);
				DrawGrid(gfx);
				//DrawSelection(gfx);
				DrawBorder(gfx);

				gfx.ResetTransform();
			}
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			Invalidate();
		}

		protected override void OnScroll(ScrollEventArgs se)
		{
			Invalidate();
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (ReadOnly) return;

			m_previousData = (bool[,])m_data.Clone();
			SetPixels(e);
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			Push(m_undoBuffer, m_previousData);
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (ReadOnly) return;

			var blockAtPoint = TryGetBlockFromPoint(e.Location);
			if (m_lastCursorPosition == blockAtPoint) return;

			m_lastCursorPosition = blockAtPoint;
			OnCursorPositionChanged(m_lastCursorPosition);

			SetPixels(e);
		}

		private void SetPixels(MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Left && e.Button != MouseButtons.Right) return;
			if (ModifierKeys == Keys.Control || ModifierKeys == Keys.Shift)
			{
				var block = TryGetBlockFromPoint(e.Location);
				if (block == null) return;

				var width = m_colsCount;
				var height = m_rowsCount;
				var to = ModifierKeys == Keys.Control ? width : height;

				for (var i = 0; i < to; i++)
				{
					if (ModifierKeys == Keys.Control) m_data[i, block.Value.Y] = e.Button == MouseButtons.Left;
					if (ModifierKeys == Keys.Shift) m_data[block.Value.X, i] = e.Button == MouseButtons.Left;
				}

				OnDataUpdated(m_data);
			}
			else if (TrySetBlockValue(e))
			{
				Invalidate();
			}
		}

		private void DrawBackground(Graphics gfx)
		{
			if (BackgroundImage == null) return;
			if (m_backgroundBrush == null)
			{
				m_backgroundBrush = new TextureBrush(BackgroundImage) { WrapMode = WrapMode.Tile };
			}

			var rect = new Rectangle(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);
			{
				rect.Offset(-HorizontalScroll.Value - Margin.Left, -VerticalScroll.Value - Margin.Top);
				rect.Inflate(HorizontalScroll.Value * 2 + Margin.Left, VerticalScroll.Value * 2 + Margin.Top);
			}
			gfx.FillRectangle(m_backgroundBrush, rect);
		}

		private void DrawBlocks(Graphics gfx)
		{
			for (var col = 0; col < m_colsCount; col++)
			{
				for (var row = 0; row < m_rowsCount; row++)
				{
					var blockRect = new Rectangle(col * BlockSize, row * BlockSize, BlockSize, BlockSize);
					gfx.FillRectangle(Data[col, row] ? m_activeBlockBrush : m_inactiveBlockBrush, blockRect);
				}
			}
		}

		private void DrawGrid(Graphics gfx)
		{
			if (!m_showGrid || m_blockInnerBorderPen.Color == Color.Transparent) return;

			for (var row = 0; row < m_rowsCount; row++)
			{
				gfx.DrawLine(m_blockInnerBorderPen, 0, row * BlockSize, m_clientArea.Width - 1, row * BlockSize);
			}
			for (var col = 0; col < m_colsCount; col++)
			{
				gfx.DrawLine(m_blockInnerBorderPen, col * BlockSize, 0, col * BlockSize, m_clientArea.Height - 1);
			}
		}

		/*private void DrawSelection(Graphics gfx)
		{
			if (!m_lastCursorPosition.HasValue) return;

			var blockRect = new Rectangle(m_lastCursorPosition.Value.X * BlockSize, m_lastCursorPosition.Value.Y * BlockSize, BlockSize, BlockSize);
			var horizontalRect = new Rectangle(0, m_lastCursorPosition.Value.Y * BlockSize, m_clientArea.Width, BlockSize);
			var verticalRect = new Rectangle(m_lastCursorPosition.Value.X * BlockSize, 0, BlockSize, m_clientArea.Height);

			gfx.FillRectangle(new SolidBrush(Color.FromArgb(0x20, 0x11, 0xAE, 0xDB)), horizontalRect);
			gfx.FillRectangle(new SolidBrush(Color.FromArgb(0x20, 0x11, 0xAE, 0xDB)), verticalRect);
			gfx.DrawRectangle(new Pen(Color.FromArgb(0xFF, 0x00, 0xAE, 0xDB)), blockRect);
		}*/

		private void DrawBorder(Graphics gfx)
		{
			if (m_blockOuterBorderPen.Color == Color.Transparent) return;
			gfx.DrawRectangle(m_blockOuterBorderPen, 0, 0, m_clientArea.Width, m_clientArea.Height);
		}

		private void CalculateSurfaceArea()
		{
			m_clientArea = new Rectangle(0, 0, m_colsCount * BlockSize, m_rowsCount * BlockSize);
			AutoScrollMinSize = new Size(m_clientArea.Width + Margin.Left + Margin.Right, m_clientArea.Height + Margin.Top + Margin.Bottom);
		}

		private Point GetRelativeMouseLocation(Point location)
		{
			return new Point(location.X - Margin.Left + HorizontalScroll.Value, location.Y - Margin.Top + VerticalScroll.Value);
		}

		private Point? TryGetBlockFromPoint(Point location)
		{
			var pos = GetRelativeMouseLocation(location);

			var blockCol = (int)Math.Floor((float)pos.X / BlockSize);
			var blockRow = (int)Math.Floor((float)pos.Y / BlockSize);

			if (blockCol > m_colsCount - 1 || blockCol < 0) return null;
			if (blockRow > m_rowsCount - 1 || blockRow < 0) return null;

			return new Point(blockCol, blockRow);
		}

		private bool TrySetBlockValue(MouseEventArgs e)
		{
			if (ReadOnly) return false;
			if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
			{
				var block = TryGetBlockFromPoint(e.Location);
				if (block == null) return false;

				Data[block.Value.X, block.Value.Y] = SingleMouseButtonMode
					? !Data[block.Value.X, block.Value.Y]
					: e.Button == MouseButtons.Left;
				OnDataUpdated(m_data);
				return true;
			}
			return false;
		}

		internal bool[,] Clone(bool[,] imageData)
		{
			var width = m_colsCount;
			var height = m_rowsCount;
			var result = new bool[width, height];
			for (var col = 0; col < width; col++)
			{
				for (var row = 0; row < height; row++)
				{
					result[col, row] = imageData[col, row];
				}
			}
			return result;
		}

		public delegate void DataUpdatedDelegate(bool[,] data);

		public delegate void CursorPositionChangedDelegate(Point? location);

		private void OnDataUpdated(bool[,] data)
		{
			var handler = DataUpdated;
			if (handler != null) handler(data);
		}

		private void OnCursorPositionChanged(Point? location)
		{
			m_lastCursorPosition = location;
			var handler = CursorPositionChanged;
			if (handler != null) handler(location);
		}
	}
}