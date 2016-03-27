using System;
using System.IO;
using JetBrains.Annotations;

namespace NFirmware
{
	internal class FirmwareStream
	{
		private readonly MemoryStream m_stream;

		public FirmwareStream([NotNull] byte[] body)
		{
			if (body == null) throw new ArgumentNullException("body");

			m_stream = new MemoryStream();
			m_stream.Write(body, 0, body.Length);
		}

		public byte[] ReadBytes(int offset, int count)
		{
			var result = new byte[count];
			{
				m_stream.Position = offset;
				m_stream.Read(result, 0, count);
			}
			return result;
		}

		public void WriteBytes(int offset, [NotNull] byte[] data)
		{
			if (data == null) throw new ArgumentNullException("data");

			m_stream.Position = offset;
			m_stream.Write(data, 0, data.Length);
		}

		public void WriteByte(int offset, byte data)
		{
			m_stream.Position = offset;
			m_stream.WriteByte(data);
		}

		public byte[] ToArray()
		{
			return m_stream.ToArray();
		}
	}
}
