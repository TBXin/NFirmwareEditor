using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NFirmware.Tests
{
	[TestClass]
	public class FirmwareStreamTests
	{
		[TestMethod]
		public void StreamCanExpandWhenWritingData()
		{
			// Arrange
			var data = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
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
			var data = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
			var offset = 5;
			var stream = new FirmwareStream(data);

			// Act
			stream.WriteByte(offset, null);
			var result = stream.ToArray();

			// Assert
			Assert.AreEqual(offset, result.Length);
		}
	}
}
