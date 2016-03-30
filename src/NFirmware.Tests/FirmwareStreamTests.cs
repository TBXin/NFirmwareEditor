using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NFirmware.Tests
{
	[TestClass]
	public class FirmwareStreamTests
	{
		private byte[] GetDummy()
		{
			return new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
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
