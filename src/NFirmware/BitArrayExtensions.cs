using System;
using System.Collections;

namespace NFirmware
{
	public static class BitArrayExtensions
	{
		public static bool[] ToBoolArray(this BitArray bitArray)
		{
			if (bitArray == null) throw new ArgumentNullException("bitArray");

			var result = new bool[bitArray.Length];
			for (var i = 0; i < result.Length; i++)
			{
				result[i] = bitArray[i];
			}
			return result;
		}

		public static byte ToByte(this BitArray bitArray)
		{
			if (bitArray == null) throw new ArgumentNullException("bitArray");

			var bytes = new byte[1];
			bitArray.CopyTo(bytes, 0);
			return bytes[0];
		}
	}
}
