using System;
using System.Collections;

namespace NFirmwareEditor.Core
{
	internal static class BitArrayExtensions
	{
		internal static bool[] ToBoolArray(this BitArray bitArray)
		{
			if (bitArray == null) throw new ArgumentNullException("bitArray");

			var result = new bool[bitArray.Length];
			for (var i = 0; i < result.Length; i++)
			{
				result[i] = bitArray[i];
			}
			return result;
		}

		internal static byte ToByte(this BitArray bitArray)
		{
			if (bitArray == null) throw new ArgumentNullException("bitArray");
			if (bitArray.Count != 8) throw new ArgumentException("bits");

			var bytes = new byte[1];
			bitArray.CopyTo(bytes, 0);
			return bytes[0];
		}
	}
}
