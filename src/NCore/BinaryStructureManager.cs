using System;
using System.IO;

namespace NCore
{
	public static class BinaryStructureManager
	{
		public static T Read<T>(byte[] data) where T : class
		{
			return null;
		}

		public static byte[] Write<T>(T data) where T : class
		{
			return null;
		}

		public static byte[] Overwrite<T>(T data, byte[] sourceBytes) where T : class
		{
			return null;
		}
	}

	public class BinaryAsciiString : Attribute
	{
		public int Length { get; set; }
	}

	public class BinaryArray : Attribute
	{
		public int ItemsCount { get; set; }
	}

	public interface IBinaryStructure
	{
		void Read(BinaryReader br);
		void Write(BinaryWriter bw);
	}
}
