using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Serialization;

namespace NFirmwareEditor.Models
{
	public class Patch
	{
		private IEnumerable<PatchModificationData> m_data;

		[XmlAttribute]
		public string Name { get; set; }

		[XmlAttribute]
		public string Author { get; set; }

		[XmlAttribute]
		public string Definition { get; set; }

		[XmlElement]
		public string Description { get; set; }

		[XmlElement("Data")]
		public string DataString { get; set; }

		[XmlIgnore]
		internal IEnumerable<PatchModificationData> Data
		{
			get { return m_data ?? (m_data = Read(DataString)); }
		}

		private static IEnumerable<PatchModificationData> Read(string dataString)
		{
			if (string.IsNullOrEmpty(dataString)) return new List<PatchModificationData>();

			var lines = dataString.Trim().Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
			var result = new List<PatchModificationData>();
			foreach (var line in lines.Where(x => string.IsNullOrEmpty(x) || !x.StartsWith("#")).Select(x => x.Replace(" ", string.Empty)))
			{
				var offsetAndData = line.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
				if (offsetAndData.Length != 2) continue;

				var offset = long.Parse(offsetAndData[0], NumberStyles.AllowHexSpecifier);
				var data = offsetAndData[1];
				if (data.IndexOf(';') != -1) data = data.Substring(0, data.IndexOf(';'));

				var originalPatchedData = data.Split(new[] { "->" }, StringSplitOptions.RemoveEmptyEntries);
				if (originalPatchedData.Length != 2) continue;

				var originalByte = originalPatchedData[0].Equals("null", StringComparison.OrdinalIgnoreCase)
					? (byte?)null
					: byte.Parse(originalPatchedData[0], NumberStyles.AllowHexSpecifier);
				var patchedByte = byte.Parse(originalPatchedData[1], NumberStyles.AllowHexSpecifier);

				result.Add(new PatchModificationData(offset, originalByte, patchedByte));
			}
			return result;
		}
	}
}
