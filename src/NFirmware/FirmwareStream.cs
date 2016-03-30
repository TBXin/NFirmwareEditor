using System;
using System.IO;
using JetBrains.Annotations;

namespace NFirmware
{
	public class FirmwareStream
	{
		private readonly MemoryStream m_stream;

		public FirmwareStream([NotNull] byte[] body)
		{
			if (body == null) throw new ArgumentNullException("body");

			m_stream = new MemoryStream();
			m_stream.Write(body, 0, body.Length);
		}

		public byte? ReadByte(long offset)
		{
			if (m_stream.Length <= offset) return null;

			m_stream.Position = offset;
			return (byte)m_stream.ReadByte();
		}

		public byte[] ReadBytes(long offset, int count)
		{
			var result = new byte[count];
			{
				m_stream.Position = offset;
				m_stream.Read(result, 0, count);
			}
			return result;
		}

		public void WriteBytes(long offset, [NotNull] byte[] data)
		{
			if (data == null) throw new ArgumentNullException("data");

			m_stream.Position = offset;
			m_stream.Write(data, 0, data.Length);
		}

		public void WriteByte(long offset, byte? data)
		{
			if (data.HasValue)
			{
				m_stream.Position = offset;
				m_stream.WriteByte(data.Value);
			}
			else if (m_stream.Length > offset)
			{
				m_stream.SetLength(offset);
			}
		}

		public byte[] ToArray()
		{
			return m_stream.ToArray();
		}
	}
}
