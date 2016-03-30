using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NFirmware.Tests
{
	[TestClass]
	public class FirmwareStreamTests
	{
		private static byte[] GetDummy()
		{
			return new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
		}

		[TestMethod]
		public void StreamReadByteShouldReadByte()
		{
			// Arrange
			var data = GetDummy();
			var stream = new FirmwareStream(data);

			// Act / Assert
			for (var i = 0; i < data.Length; i++)
			{
				Assert.AreEqual(stream.ReadByte(i), data[i]);
			}
		}

		[TestMethod]
		public void StreamReadByteShouldReturnNullWhenTryingToReadOutOfBounds()
		{
			// Arrange
			var data = GetDummy();
			var stream = new FirmwareStream(data);

			// Act
			var result = stream.ReadByte(data.Length);

			// Assert
			Assert.AreEqual(null, result);
		}

		[TestMethod]
		public void StreamReadBytesShouldReadBytes()
		{
			// Arrange
			var data = GetDummy();
			var stream = new FirmwareStream(data);
			var offset = 2;
			var count = 5;

			// Act
			var result = stream.ReadBytes(offset, count);

			// Assert
			Assert.AreEqual(count, result.Length);
			Assert.IsTrue(result.SequenceEqual(data.Skip(offset).Take(count)));
		}

		[TestMethod]
		public void StreamReadBytesShouldDesiredBytesCountEvenIfOutOfBounds()
		{
			// Arrange
			var data = GetDummy();
			var stream = new FirmwareStream(data);
			var offset = data.Length - 2;
			var count = 5;

			// Act
			var result = stream.ReadBytes(offset, count);

			// Assert
			Assert.AreEqual(count, result.Length);
			Assert.AreEqual(data[data.Length - 2], result[0]);
			Assert.AreEqual(data[data.Length - 1], result[1]);
			Assert.IsTrue(result.Skip(2).SequenceEqual(new byte[3]));
		}

		[TestMethod]
		public void StreamReadingDoesNotAffectFirmwareSize()
		{
			// Arrange
			var data = GetDummy();
			var stream = new FirmwareStream(data);

			// Act
			for (var i = 0; i < data.Length + 10; i++)
			{
				stream.ReadByte(i);
			}
			stream.ReadBytes(0, 250);
			var result = stream.ToArray();

			// Assert
			Assert.AreEqual(data.Length, result.Length);
			Assert.IsTrue(result.SequenceEqual(data));
		}

		[TestMethod]
		public void StreamCanExpandWhenWritingData()
		{
			// Arrange
			var data = GetDummy();
			var offset = data.Length;
			var stream = new FirmwareStream(data);

			// Act
			stream.WriteByte(offset, 0xFF);
			var result = stream.ToArray();

			// Assert
			Assert.AreEqual(offset + 1, result.Length);
			Assert.AreEqual(0xFF, result[offset]);
		}

		[TestMethod]
		public void StreamCanShrinkWhenWritingData()
		{
			// Arrange
			var data = GetDummy();
			var offset = data.Length / 2;
			var stream = new FirmwareStream(data);

			// Act
			stream.WriteByte(offset, null);
			var result = stream.ToArray();

			// Assert
			Assert.AreEqual(offset, result.Length);
		}

		[TestMethod]
		public void StreamCanExpandAfterShrink()
		{
			// Arrange
			var data = GetDummy();
			var originalLength = data.Length;
			var shrinkedLength = data.Length / 2;
			var stream = new FirmwareStream(data);

			// Act
			stream.WriteByte(shrinkedLength, null);
			stream.WriteByte(originalLength - 1, 1);
			var result = stream.ToArray();

			// Assert
			Assert.AreEqual(originalLength, result.Length);
		}

		[TestMethod]
		public void StreamDoesNotExpandWhenShrinkingMultipleTimesAtDifferentOffsets()
		{
			// Arrange
			var data = GetDummy();
			var stream = new FirmwareStream(data);

			// Act
			for (var i = 0; i < data.Length; i++)
			{
				stream.WriteByte(i, null);
			}
			var result = stream.ToArray();

			// Assert
			Assert.AreEqual(0, result.Length);
		}
	}
}
