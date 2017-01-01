using System;
using NCore;

namespace NFirmware
{
	public class JoyetechEncryption : IEncryption
	{
		private const int MagicNumber = 0x63B38;

		#region Implementation of IEncryption
		public EncryptionType Type
		{
			get { return EncryptionType.Joyetech; }
		}

		public byte[] Encode(byte[] source)
		{
			if (source == null) throw new ArgumentNullException("source");

			var result = new byte[source.Length];
			for (var i = 0; i < source.Length; i++)
			{
				result[i] = (byte)(source[i] ^ GenFunction(source.Length, i) & 0xFF);
			}
			return result;
		}

		public byte[] Decode(byte[] source)
		{
			if (source == null) throw new ArgumentNullException("source");
			return Encode(source);
		}
		#endregion

		private static int GenFunction(int fileSize, int index)
		{
			return fileSize + MagicNumber + index - fileSize / MagicNumber;
		}
	}
}
