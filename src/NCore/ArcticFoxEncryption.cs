using System;
using JetBrains.Annotations;

namespace NCore
{
	public class ArcticFoxEncryption : IEncryption
	{
		private readonly byte m_keyKey;
		private readonly int m_tableLength;

		public ArcticFoxEncryption(byte keyKey = 0x17, int tableLength = 9)
		{
			m_keyKey = keyKey;
			m_tableLength = tableLength;
		}

		#region Implementation of IEncryption
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

		private int ReadKey(byte[] keyBytes)
		{
			var decyptedKeyBytes = new byte[keyBytes.Length];
			for (var i = 0; i < decyptedKeyBytes.Length; i++)
			{
				decyptedKeyBytes[i] = (byte)(keyBytes[i] ^ m_keyKey);
			}
			return BitConverter.ToInt32(decyptedKeyBytes, 0);
		}

		private byte[] WriteKey(int key)
		{
			var result =  BitConverter.GetBytes(key);
			for (var i = 0; i < result.Length; i++)
			{
				result[i] ^= m_keyKey;
			}
			return result;
		}

		private byte[] CreateTable(int seed)
		{
			var result = new byte[m_tableLength];
			new Random(seed).NextBytes(result);
			return result;
		}
	}
}
