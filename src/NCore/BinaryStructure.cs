using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using JetBrains.Annotations;

namespace NCore
{
	/// <summary>
	/// Provides a helper methods to read/write binary structures.
	/// </summary>
	public static class BinaryStructure
	{
		private static readonly Dictionary<Type, TypeConverter> s_convertersCache = new Dictionary<Type, TypeConverter>();

		/// <summary>
		/// Constructs an object from a binary data.
		/// </summary>
		/// <typeparam name="T">Source object type.</typeparam>
		/// <param name="data">Source object.</param>
		[NotNull]
		public static T ReadBinary<T>([NotNull] byte[] data) where T : class, new()
		{
			if (data == null) throw new ArgumentNullException("data");

			using (var br = new BinaryReader(new MemoryStream(data)))
			{
				var result = new T();
				RecursiveRead(result, br);
				return result;
			}
		}

		/// <summary>
		/// Transforms an object to binary data.
		/// </summary>
		/// <typeparam name="T">Source object type.</typeparam>
		/// <param name="data">Source object.</param>
		/// <param name="sourceBuffer">Existed binary buffer, on top of which the object will be written.</param>
		[NotNull]
		public static byte[] WriteBinary<T>([NotNull] T data, byte[] sourceBuffer = null) where T : class
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

		/// <summary>
		/// Fills an existing object with data from key-value collection, where each key is pointing to the object field.
		/// </summary>
		/// <typeparam name="T">Source object type.</typeparam>
		/// <param name="data">Source object.</param>
		/// <param name="kvp">Values collection.</param>
		[NotNull]
		public static T ReadFromDictionary<T>([NotNull] T data, [NotNull] IDictionary<string, string> kvp)
		{
			if (data == null) throw new ArgumentNullException("data");
			if (kvp == null) throw new ArgumentNullException("kvp");

			RecursiveReadFromDictionary(data, "Model", kvp);
			return data;
		}

		/// <summary>
		/// Creates a key-value collection for the each object field, where field path + name is a key, and field value is value.
		/// </summary>
		/// <typeparam name="T">Source object type.</typeparam>
		/// <param name="data">Source object.</param>
		[NotNull]
		public static IDictionary<string, string> WriteToDictionary<T>([NotNull] T data) where T : class
		{
			if (data == null) throw new ArgumentNullException("data");

			var result = new Dictionary<string, string>();
			RecursiveWriteToDictionary(data, "Model", result);
			return result;
		}

		/// <summary>
		/// Creates a copy of the serializable to binary structure.
		/// </summary>
		/// <typeparam name="T">Source object type.</typeparam>
		/// <param name="source">Source object.</param>
		[NotNull]
		public static T Copy<T>([NotNull] T source) where T : class, new()
		{
			if (source == null) throw new ArgumentNullException("source");
			return ReadBinary<T>(WriteBinary(source));
		}

		private static void RecursiveRead(object obj, BinaryReader br)
		{
			foreach (var iterator in obj.GetType().GetFields())
			{
				var field = iterator;
				var fieldType = field.FieldType;
				var value = (object)null;

				HandleOffsetAttribute(field, br.BaseStream);

				if (fieldType.IsPrimitive || fieldType.IsEnum)
				{
					value = ReadValue(fieldType, br);
				}
				else if (fieldType.IsArray)
				{
					var arrayAttribute = GetAttribute<BinaryArrayAttribute>(field, true);
					var elType = fieldType.GetElementType();
					var instance = Array.CreateInstance(elType, arrayAttribute.Length);

					for (var i = 0; i < arrayAttribute.Length; i++)
					{
						object elementValue;
						if (elType.IsClass)
						{
							elementValue = Activator.CreateInstance(elType);
							RecursiveRead(elementValue, br);
						}
						else
						{
							var rawValue = ReadValue(elType, br);
							elementValue = elType.IsEnum ? Enum.ToObject(elType, rawValue) : rawValue;
						}
						instance.SetValue(elementValue, i);
					}
					value = instance;
				}
				else if (fieldType == typeof(string))
				{
					var ascii = GetAttribute<BinaryAsciiStringAttribute>(field, true);
					value = Encoding.ASCII.GetString(br.ReadBytes(ascii.Length)).TrimEnd('\0');
				}
				else if (typeof(IBinaryStructure).IsAssignableFrom(fieldType))
				{
					var instance = (IBinaryStructure)Activator.CreateInstance(fieldType);
					instance.Read(br);
					value = instance;
				}
				else if (fieldType.IsClass)
				{
					var instance = Activator.CreateInstance(fieldType);
					RecursiveRead(instance, br);
					value = instance;
				}

				field.SetValue(obj, value);
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
					var arrayAttribute = GetAttribute<BinaryArrayAttribute>(field, true);
					var elType = filedType.GetElementType();
					var array = (Array)field.GetValue(obj);

					for (var i = 0; i < arrayAttribute.Length; i++)
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
				}
				else if (filedType == typeof(string))
				{
					var ascii = GetAttribute<BinaryAsciiStringAttribute>(field, true);
					var value = (string)field.GetValue(obj);
					var valueBytes = Encoding.ASCII.GetBytes(value);
					var result = new byte[ascii.Length];
					Buffer.BlockCopy(valueBytes, 0, result, 0, valueBytes.Length);

					bw.Write(result);
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

		private static void RecursiveReadFromDictionary(object obj, string path, IDictionary<string, string> kvp)
		{
			foreach (var iterator in obj.GetType().GetFields())
			{
				var field = iterator;
				var filedType = field.FieldType;
				var key = path + "." + field.Name;

				if (filedType.IsPrimitive || filedType.IsEnum)
				{
					if (kvp.ContainsKey(key))
					{
						field.SetValue(obj, GetTypeConverter(filedType).ConvertFromString(kvp[key]));
					}
				}
				else if (filedType.IsArray)
				{
					var elType = filedType.GetElementType();
					var array = (Array)field.GetValue(obj);

					for (var i = 0; i < array.Length; i++)
					{
						var arrayKey = path + "." + field.Name + "[" + i + "]";
						if (elType.IsClass)
						{
							RecursiveReadFromDictionary(array.GetValue(i), arrayKey, kvp);
						}
						else
						{
							if (kvp.ContainsKey(arrayKey))
							{
								array.SetValue(GetTypeConverter(elType).ConvertFromString(kvp[arrayKey]), i);
							}
						}
					}
				}
				else if (filedType == typeof(string))
				{
					if (kvp.ContainsKey(key))
					{
						field.SetValue(obj, kvp[key]);
					}
				}
				else if (typeof(IBinaryStructure).IsAssignableFrom(filedType))
				{
					RecursiveReadFromDictionary(field.GetValue(obj), key, kvp);
				}
				else if (filedType.IsClass)
				{
					RecursiveReadFromDictionary(field.GetValue(obj), key, kvp);
				}
			}
		}

		private static void RecursiveWriteToDictionary(object obj, string path, IDictionary<string, string> result)
		{
			foreach (var iterator in obj.GetType().GetFields())
			{
				var field = iterator;
				var filedType = field.FieldType;
				var key = path + "." + field.Name;

				if (filedType.IsPrimitive || filedType.IsEnum)
				{
					var value = field.GetValue(obj);
					result[key] = Convert.ToString(value);
				}
				else if (filedType.IsArray)
				{
					var elType = filedType.GetElementType();
					var array = (Array)field.GetValue(obj);

					for (var i = 0; i < array.Length; i++)
					{
						var arrayKey = path + "." + field.Name + "[" + i + "]";
						if (elType.IsClass)
						{
							RecursiveWriteToDictionary(array.GetValue(i), arrayKey, result);
						}
						else
						{
							result[arrayKey] = Convert.ToString(array.GetValue(i));
						}
					}
				}
				else if (filedType == typeof(string))
				{
					result[key] = (string)field.GetValue(obj);
				}
				else if (typeof(IBinaryStructure).IsAssignableFrom(filedType))
				{
					RecursiveWriteToDictionary(field.GetValue(obj), key, result);
				}
				else if (filedType.IsClass)
				{
					RecursiveWriteToDictionary(field.GetValue(obj), key, result);
				}
			}
		}

		private static TypeConverter GetTypeConverter(Type type)
		{
			if (s_convertersCache.ContainsKey(type))
			{
				return s_convertersCache[type];
			}

			var result = TypeDescriptor.GetConverter(type);
			s_convertersCache[type] = result;
			return result;
		}

		private static object ReadValue(Type type, BinaryReader br)
		{
			if (type == typeof(bool)) return br.ReadBoolean();
			if (type == typeof(byte)) return br.ReadByte();
			if (type == typeof(sbyte)) return br.ReadSByte();
			if (type == typeof(ushort)) return br.ReadUInt16();
			if (type == typeof(uint)) return br.ReadUInt32();
			if (type.IsEnum) return ReadValue(type.GetEnumUnderlyingType(), br);

			throw new InvalidOperationException("Invalid type: " + type);
		}

		private static void WriteValue(Type type, object value, BinaryWriter bw)
		{
			if (type == typeof(bool)) bw.Write((bool)value);
			else if (type == typeof(byte)) bw.Write((byte)value);
			else if (type == typeof(sbyte)) bw.Write((sbyte)value);
			else if (type == typeof(ushort)) bw.Write((ushort)value);
			else if (type == typeof(uint)) bw.Write((uint)value);
			else if (type.IsEnum) WriteValue(type.GetEnumUnderlyingType(), value, bw);
			else throw new InvalidOperationException("Invalid type: " + type);
		}

		private static T GetAttribute<T>(FieldInfo field, bool throwIfNotFound) where T : class
		{
			var attribute = field.GetCustomAttributes(typeof(T), true).FirstOrDefault() as T;
			if (attribute == null && throwIfNotFound)
			{
				throw new InvalidOperationException
				(
					string.Format("An obligatory attribute \"{0}\" missing. Property: \"{1}\".", typeof(T).Name, field.Name)
				);
			}
			return attribute;
		}

		private static void HandleOffsetAttribute(FieldInfo field, Stream stream)
		{
			var offset = GetAttribute<BinaryOffsetAttribute>(field, false);

			if (offset == null) return;
			if (offset.IsEmpty) throw new InvalidOperationException("Neither Relative nor Absolute values are defined.");

			if (offset.Relative != 0) stream.Seek(offset.Relative, SeekOrigin.Current);
			if (offset.Absolute != 0) stream.Seek(offset.Absolute, SeekOrigin.Begin);
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
