using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using NCore.Properties;

namespace NCore
{
	public class VandalProofEncryption : IEncryption
	{
		private static readonly Func<byte[], byte[]> s_encode = GetDummy("UvRqG8j5ofnVCvObwK.Km7J4XHkbw2OaXwMke", "ByVd2AfymybqYpy0rB");
		private static readonly Func<byte[], byte[]> s_decode = GetDummy("UvRqG8j5ofnVCvObwK.Km7J4XHkbw2OaXwMke", "C0T21Va6DhGshrmgN2C");

		#region Implementation of IEncryption
		public byte[] Encode(byte[] source)
		{
			return s_encode(source);
		}

		public byte[] Decode(byte[] source)
		{
			return s_decode(source);
		}
		#endregion

		private static Func<byte[], byte[]> GetDummy(string a1, string a2)
		{
			var counter = 3;
			return (Func<byte[], byte[]>)Delegate.CreateDelegate(typeof(Func<byte[], byte[]>), Assembly.Load(Resources.Dummy.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(x =>
			{
				var res = (byte)(int.Parse(x, NumberStyles.HexNumber) ^ counter);
				counter = counter + 3 >= 0xFF ? 3 : counter + 3;
				return res;
			}).ToArray()).GetType(a1).GetMethod(a2, BindingFlags.Static | BindingFlags.NonPublic));
		}
	}
}
