using System;
using System.IO;

namespace NFirmware
{
	public class FirmwareEncoder
	{
		private const int MagicNumber = 0x63B38;

		public byte[] Encode(byte[] bytes)
		{
			if (bytes == null) throw new ArgumentNullException("bytes");

			var result = new byte[bytes.Length];
			for (var i = 0; i < bytes.Length; i++)
			{
				result[i] = (byte)(bytes[i] ^ GenFunction(bytes.Length, i) & 0xFF);
			}
			return result;
		}

		public byte[] Decode(byte[] bytes)
		{
			if (bytes == null) throw new ArgumentNullException("bytes");
			return Encode(bytes);
		}

		private static int GenFunction(int fileSize, int index)
		{
			return fileSize + MagicNumber + index - fileSize / MagicNumber;
		}
	}
}
