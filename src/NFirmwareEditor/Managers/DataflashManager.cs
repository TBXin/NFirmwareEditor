using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using NFirmwareEditor.Models;

namespace NFirmwareEditor.Managers
{
	internal class DataflashManager
	{
		public Dataflash Read(byte[] dataflashBytes)
		{
			if (dataflashBytes == null) throw new ArgumentNullException("dataflashBytes");

			using (var br = new BinaryReader(new MemoryStream(dataflashBytes)))
			{
				var result = new Dataflash();
				RecursiveRead(result, br);
				return result;
			}
		}

		public void Write(Dataflash dataflash, byte[] dataflashBytes)
		{
			if (dataflash == null) throw new ArgumentNullException("dataflash");
			if (dataflashBytes == null) throw new ArgumentNullException("dataflashBytes");

			using (var bw = new BinaryWriter(new MemoryStream(dataflashBytes)))
			{
				RecursiveWrite(dataflash, bw);
			}
		}

		public static bool GetBit(byte b, int bitNumber)
		{
			return (b & (1 << bitNumber - 1)) != 0;
		}

		public static byte SetBit(byte b, int bitNumber, bool value)
		{
			var existedBit = GetBit(b, bitNumber);
			if (existedBit == value) return b;

			return (byte)(b ^ (1 << (bitNumber - 1)));
		}

		private void RecursiveRead(object obj, BinaryReader br)
		{
			foreach (var iterator in obj.GetType().GetProperties())
			{
				var property = iterator;
				var propertyType = property.PropertyType;

				HandleOffsetAttribute(property, br.BaseStream);

				if (propertyType.IsPrimitive || propertyType.IsEnum)
				{
					var value = ReadValue(propertyType, br);
					property.SetValue(obj, value, null);
				}
				else if (propertyType.IsArray)
				{
					GetAttribute<BinaryArrayAttribute>(property, true, arrayAttribute =>
					{
						var elType = propertyType.GetElementType();
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
						property.SetValue(obj, instance, null);
					});
				}
				else if (propertyType == typeof(string))
				{
					GetAttribute<AsciiString>(property, true, ascii =>
					{
						var value = Encoding.ASCII.GetString(br.ReadBytes(ascii.Length));
						property.SetValue(obj, value, null);
					});
				}
				else if (typeof(IBinaryReaderWriter).IsAssignableFrom(propertyType))
				{
					var instance = (IBinaryReaderWriter)Activator.CreateInstance(propertyType);
					instance.Read(br);
					property.SetValue(obj, instance, null);
				}
				else if (propertyType.IsClass)
				{
					var instance = Activator.CreateInstance(propertyType);
					RecursiveRead(instance, br);
					property.SetValue(obj, instance, null);
				}
			}
		}

		private void RecursiveWrite(object obj, BinaryWriter bw)
		{
			foreach (var iterator in obj.GetType().GetProperties())
			{
				var property = iterator;
				var propertyType = property.PropertyType;

				HandleOffsetAttribute(property, bw.BaseStream);

				if (propertyType.IsPrimitive || propertyType.IsEnum)
				{
					var value = property.GetValue(obj, null);
					WriteValue(propertyType, value, bw);
				}
				else if (propertyType.IsArray)
				{
					GetAttribute<BinaryArrayAttribute>(property, true, arrayAttribute =>
					{
						var elType = propertyType.GetElementType();
						var array = (Array)property.GetValue(obj, null);

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
				else if (propertyType == typeof(string))
				{
					GetAttribute<AsciiString>(property, true, ascii =>
					{
						var value = (string)property.GetValue(obj, null);
						var valueBytes = Encoding.ASCII.GetBytes(value);
						var result = new byte[ascii.Length];
						Buffer.BlockCopy(valueBytes, 0, result, 0, ascii.Length);

						bw.Write(result);
					});
				}
				else if (typeof(IBinaryReaderWriter).IsAssignableFrom(propertyType))
				{
					var value = (IBinaryReaderWriter)property.GetValue(obj, null);
					value.Write(bw);
				}
				else if (propertyType.IsClass)
				{
					var value = property.GetValue(obj, null);
					RecursiveWrite(value, bw);
				}
			}
		}

		private void HandleOffsetAttribute(PropertyInfo property, Stream stream)
		{
			GetAttribute<BinaryOffsetAttribute>(property, false, offset =>
			{
				if (offset.IsEmpty) throw new InvalidOperationException("Neither Relative nor Absolute values are defined.");

				if (offset.Relative != 0) stream.Seek(offset.Relative, SeekOrigin.Current);
				if (offset.Absolute != 0) stream.Seek(offset.Absolute, SeekOrigin.Begin);
			});
		}

		private object ReadValue(Type type, BinaryReader br)
		{
			if (type == typeof(bool)) return br.ReadBoolean();
			if (type == typeof(byte)) return br.ReadByte();
			if (type == typeof(ushort)) return br.ReadUInt16();
			if (type == typeof(uint)) return br.ReadUInt32();
			if (type.IsEnum) return ReadValue(type.GetEnumUnderlyingType(), br);

			throw new InvalidOperationException("Invalid type: " + type);
		}

		private void WriteValue(Type type, object value, BinaryWriter bw)
		{
			if (type == typeof(bool)) bw.Write((bool)value);
			else if (type == typeof(byte)) bw.Write((byte)value);
			else if (type == typeof(ushort)) bw.Write((ushort)value);
			else if (type == typeof(uint)) bw.Write((uint)value);
			else if (type.IsEnum) WriteValue(type.GetEnumUnderlyingType(), value, bw);
			else throw new InvalidOperationException("Invalid type: " + type);
		}

		private void GetAttribute<T>(PropertyInfo property, bool throwIfNotFound, Action<T> action) where T : class
		{
			var attribute = property.GetCustomAttributes(typeof(T), true).FirstOrDefault() as T;
			if (attribute == null)
			{
				if (throwIfNotFound) throw new InvalidOperationException(string.Format("An obligatory attribute \"{0}\" missing. Property: \"{1}\".", typeof(T).Name, property.Name));
				return;
			}

			action(attribute);
		}
	}

	internal class AsciiString : Attribute
	{
		public int Length { get; set; }
	}

	internal class BinaryArrayAttribute : Attribute
	{
		public uint Length { get; set; }
	}

	internal class BinaryOffsetAttribute : Attribute
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

	internal interface IBinaryReaderWriter
	{
		void Read(BinaryReader r);

		void Write(BinaryWriter r);
	}
}
