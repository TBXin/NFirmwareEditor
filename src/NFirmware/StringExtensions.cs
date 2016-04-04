using System;
using System.Globalization;
using System.Linq;
using JetBrains.Annotations;

namespace NFirmware
{
	internal static class StringExtensions
	{
		internal static byte HexStringToByte([NotNull] this string hexNumber)
		{
			if (string.IsNullOrEmpty(hexNumber)) throw new ArgumentNullException("hexNumber");
			return byte.Parse(PrepairHexString(hexNumber), NumberStyles.AllowHexSpecifier);
		}

		internal static int HexStringToInt([NotNull] this string hexNumber)
		{
			if (string.IsNullOrEmpty(hexNumber)) throw new ArgumentNullException("hexNumber");
			return int.Parse(PrepairHexString(hexNumber), NumberStyles.AllowHexSpecifier);
		}

		internal static long HexStringToLong([NotNull] this string hexNumber)
		{
			if (string.IsNullOrEmpty(hexNumber)) throw new ArgumentNullException("hexNumber");
			return long.Parse(PrepairHexString(hexNumber), NumberStyles.AllowHexSpecifier);
		}

		[CanBeNull]
		internal static byte[] HexStringToByteArray([CanBeNull] this string hexNumbers)
		{
			if (string.IsNullOrEmpty(hexNumbers)) return null;

			var numbers = hexNumbers.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
			return numbers.Select(x => x.HexStringToByte()).ToArray();
		}

		private static string PrepairHexString(string hexNumber)
		{
			return hexNumber.StartsWith("0x") ? hexNumber.Substring(2) : hexNumber;
		}
	}
}
