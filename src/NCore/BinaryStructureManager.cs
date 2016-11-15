using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using JetBrains.Annotations;

namespace NCore
{
	public static class BinaryStructureManager
	{
		[NotNull]
		public static T Read<T>(byte[] data) where T : class, new()
		{
			if (data == null) throw new ArgumentNullException("data");

			using (var br = new BinaryReader(new MemoryStream(data)))
			{
				var result = new T();
				RecursiveRead(result, br);
				return result;
			}
		}

		public static byte[] Write<T>(T data, byte[] sourceBuffer = null) where T : class
		{
			if (data == null) throw new ArgumentNullException("data");

			using (var ms = sourceBuffer == null ? new MemoryStream() : new MemoryStream(sourceBuffer))
			{
				using (var bw = new BinaryWriter(ms))
				{
					RecursiveWrite(data, bw);
				}
				return ms.ToArray();
			}
		}

		private static void RecursiveRead(object obj, BinaryReader br)
		{
			foreach (var iterator in obj.GetType().GetFields())
			{
				var field = iterator;
				var fieldType = field.FieldType;

				HandleOffsetAttribute(field, br.BaseStream);

				if (fieldType.IsPrimitive || fieldType.IsEnum)
				{
					var value = ReadValue(fieldType, br);
					field.SetValue(obj, value);
				}
				else if (fieldType.IsArray)
				{
					GetAttribute<BinaryArrayAttribute>(field, true, arrayAttribute =>
					{
						var elType = fieldType.GetElementType();
						var instance = Array.CreateInstance(elType, arrayAttribute.Length);

						for (var i = 0; i < arrayAttribute.Length; i++)
						{
							object value;
							if (elType.IsClass)
							{
								value = Activator.CreateInstance(elType);
								RecursiveRead(value, br);
							}
							else
							{
								var rawValue = ReadValue(elType, br);
								value = elType.IsEnum ? Enum.ToObject(elType, rawValue) : rawValue;
							}
							instance.SetValue(value, i);
						}
						field.SetValue(obj, instance);
					});
				}
				else if (fieldType == typeof(string))
				{
					GetAttribute<BinaryAsciiStringAttribute>(field, true, ascii =>
					{
						var value = Encoding.ASCII.GetString(br.ReadBytes(ascii.Length));
						field.SetValue(obj, value);
					});
				}
				else if (typeof(IBinaryStructure).IsAssignableFrom(fieldType))
				{
					var instance = (IBinaryStructure)Activator.CreateInstance(fieldType);
					instance.Read(br);
					field.SetValue(obj, instance);
				}
				else if (fieldType.IsClass)
				{
					var instance = Activator.CreateInstance(fieldType);
					RecursiveRead(instance, br);
					field.SetValue(obj, instance);
				}
			}
		}

		private static void RecursiveWrite(object obj, BinaryWriter bw)
		{
			foreach (var iterator in obj.GetType().GetFields())
			{
				var field = iterator;
				var filedType = field.FieldType;

				HandleOffsetAttribute(field, bw.BaseStream);

				if (filedType.IsPrimitive || filedType.IsEnum)
				{
					var value = field.GetValue(obj);
					WriteValue(filedType, value, bw);
				}
				else if (filedType.IsArray)
				{
					GetAttribute<BinaryArrayAttribute>(field, true, arrayAttribute =>
					{
						var elType = filedType.GetElementType();
						var array = (Array)field.GetValue(obj);

						for (var i = 0; i < array.Length; i++)
						{
							if (elType.IsClass)
							{
								RecursiveWrite(array.GetValue(i), bw);
							}
							else
							{
								WriteValue(elType, array.GetValue(i), bw);
							}
						}
					});
				}
				else if (filedType == typeof(string))
				{
					GetAttribute<BinaryAsciiStringAttribute>(field, true, ascii =>
					{
						var value = (string)field.GetValue(obj);
						var valueBytes = Encoding.ASCII.GetBytes(value);
						var result = new byte[ascii.Length];
						Buffer.BlockCopy(valueBytes, 0, result, 0, ascii.Length);

						bw.Write(result);
					});
				}
				else if (typeof(IBinaryStructure).IsAssignableFrom(filedType))
				{
					var value = (IBinaryStructure)field.GetValue(obj);
					value.Write(bw);
				}
				else if (filedType.IsClass)
				{
					var value = field.GetValue(obj);
					RecursiveWrite(value, bw);
				}
			}
		}

		private static object ReadValue(Type type, BinaryReader br)
		{
			if (type == typeof(bool)) return br.ReadBoolean();
			if (type == typeof(byte)) return br.ReadByte();
			if (type == typeof(ushort)) return br.ReadUInt16();
			if (type == typeof(uint)) return br.ReadUInt32();
			if (type.IsEnum) return ReadValue(type.GetEnumUnderlyingType(), br);

			throw new InvalidOperationException("Invalid type: " + type);
		}

		private static void WriteValue(Type type, object value, BinaryWriter bw)
		{
			if (type == typeof(bool)) bw.Write((bool)value);
			else if (type == typeof(byte)) bw.Write((byte)value);
			else if (type == typeof(ushort)) bw.Write((ushort)value);
			else if (type == typeof(uint)) bw.Write((uint)value);
			else if (type.IsEnum) WriteValue(type.GetEnumUnderlyingType(), value, bw);
			else throw new InvalidOperationException("Invalid type: " + type);
		}

		private static void GetAttribute<T>(FieldInfo field, bool throwIfNotFound, Action<T> action) where T : class
		{
			var attribute = field.GetCustomAttributes(typeof(T), true).FirstOrDefault() as T;
			if (attribute == null)
			{
				if (throwIfNotFound)
				{
					throw new InvalidOperationException
					(
						string.Format("An obligatory attribute \"{0}\" missing. Property: \"{1}\".", typeof(T).Name, field.Name)
					);
				}
				return;
			}

			action(attribute);
		}

		private static void HandleOffsetAttribute(FieldInfo field, Stream stream)
		{
			GetAttribute<BinaryOffsetAttribute>(field, false, offset =>
			{
				if (offset.IsEmpty) throw new InvalidOperationException("Neither Relative nor Absolute values are defined.");

				if (offset.Relative != 0) stream.Seek(offset.Relative, SeekOrigin.Current);
				if (offset.Absolute != 0) stream.Seek(offset.Absolute, SeekOrigin.Begin);
			});
		}
	}

	public class BinaryOffsetAttribute : Attribute
	{
		public BinaryOffsetAttribute()
		{
			Relative = 0;
			Absolute = 0;
		}

		public uint Relative { get; set; }

		public uint Absolute { get; set; }

		public bool IsEmpty
		{
			get { return Relative == 0 && Absolute == 0; }
		}
	}

	public class BinaryAsciiStringAttribute : Attribute
	{
		public int Length { get; set; }
	}

	public class BinaryArrayAttribute : Attribute
	{
		public int Length { get; set; }
	}

	public interface IBinaryStructure
	{
		void Read(BinaryReader br);
		void Write(BinaryWriter bw);
	}

	public static class BitHelper
	{
		public static bool GetBit(this byte b, int bitNumber)
		{
			return (b & (1 << bitNumber - 1)) != 0;
		}

		public static byte SetBit(this byte b, int bitNumber, bool value)
		{
			var existedBit = GetBit(b, bitNumber);
			if (existedBit == value) return b;

			return (byte)(b ^ (1 << (bitNumber - 1)));
		}
	}
}
