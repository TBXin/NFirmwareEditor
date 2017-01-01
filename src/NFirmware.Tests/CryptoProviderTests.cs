using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NCore;

namespace NFirmware.Tests
{
	[TestClass]
	public class CryptoProviderTests
	{
		[TestMethod]
		public void ShouldEncryptAndDecrypt()
		{
			// Arrange
			var rnd = new Random();
			var data = new List<byte[]>();
			for (var i = 0; i < rnd.Next(1000, 1500); i++)
			{
				var buffer = new byte[rnd.Next(200, 2000)];
				rnd.NextBytes(buffer);
				data.Add(buffer);
			}
			var encryption = new ArcticFoxEncryption();

			// Act & Assert
			foreach (var buffer in data)
			{
				var encrypted = encryption.Encode(buffer, rnd.Next());
				var decrypted = encryption.Decode(encrypted);

				Assert.AreEqual(buffer.Length, decrypted.Length);
				CollectionAssert.AreEqual(buffer, decrypted);
			}
		}
	}
}
