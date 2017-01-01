using System;
using JetBrains.Annotations;

namespace NCore
{
	public class ArcticFoxEncryption : IEncryption
	{
		#region Implementation of IEncryption
		public EncryptionType Type
		{
			get { return EncryptionType.ArcticFox; }
		}

		public byte[] Encode(byte[] source)
		{
			return Encode(source, null);
		}

		public byte[] Decode([NotNull] byte[] source)
		{
			if (source == null || source.Length <= 4) throw new ArgumentNullException("source");

			var keyBytes = new byte[4];
			Buffer.BlockCopy(source, 0, keyBytes, 0, keyBytes.Length);
			var initialKey = ReadKey(keyBytes);
			var table = CreateTable(initialKey);
			var result = new byte[source.Length - keyBytes.Length];
			for (var i = 0; i < result.Length; i++)
			{
				var outerIndex = i + keyBytes.Length;
				result[i] = (byte)(source[outerIndex] ^ table[i % table.Length]);
			}
			return result;
		}
		#endregion

		public byte[] Encode([NotNull] byte[] source, int? key)
		{
			if (source == null) throw new ArgumentNullException("source");

			var initialKey = key ?? CreateKey();
			var table = CreateTable(initialKey);
			var keyBytes = WriteKey(initialKey);
			var result = new byte[source.Length + keyBytes.Length];
			for (var i = keyBytes.Length; i < result.Length; i++)
			{
				var outerIndex = i - keyBytes.Length;
				result[i] = (byte)(source[outerIndex] ^ table[outerIndex % table.Length]);
			}
			Buffer.BlockCopy(keyBytes, 0, result, 0, keyBytes.Length);
			return result;
		}

		private static int CreateKey()
		{
			return (int)DateTime.UtcNow.ToBinary();
		}

		private static int ReadKey(byte[] keyBytes)
		{
			var decyptedKeyBytes = new byte[keyBytes.Length];
			for (var i = 0; i < decyptedKeyBytes.Length; i++)
			{
				decyptedKeyBytes[i] = (byte)(keyBytes[i] ^ 0x17);
			}
			return BitConverter.ToInt32(decyptedKeyBytes, 0);
		}

		private static byte[] WriteKey(int key)
		{
			var result =  BitConverter.GetBytes(key);
			for (var i = 0; i < result.Length; i++)
			{
				result[i] ^= 0x17;
			}
			return result;
		}

		private static byte[] CreateTable(int seed)
		{
			var result = new byte[9];
			new Random(seed).NextBytes(result);
			return result;
		}
	}
}
