using System.Collections.Generic;
using System.Linq;
using System.Text;
using NFirmware;

namespace NFirmwareEditor.Managers
{
	internal static class ArcticFoxResourceFileBuilder
	{
		internal static string CreateResourceFile(Firmware firmware, IEnumerable<FirmwareImageMetadata> imageMetadatas)
		{
			var sb = new StringBuilder();
			{
				// Header
				sb.AppendLine(".section .rodata");
				sb.AppendLine(".global font");
				sb.AppendLine(".global font_end");
				sb.AppendLine();

				var fontSectionBuilder = new StringBuilder();

				// Data section
				foreach (var metadata in imageMetadatas)
				{
					var glyphName = "byte_CH" + metadata.Index.ToString("X4");
					fontSectionBuilder.AppendLine("        .long " + glyphName);

					var imageBytes = firmware.ReadImageAsByteArray(metadata);
					var bytesText = string.Join(", ", imageBytes.Select(x => "0x" + x.ToString("X2")));
					sb.AppendLine(string.Format("{0}: .byte {1}", glyphName, bytesText));
				}
				sb.AppendLine();

				// Font section
				sb.AppendLine(".balign 4,0");
				sb.AppendLine("font:");
				sb.Append(fontSectionBuilder);
				sb.AppendLine("font_end:");
			}
			return sb.ToString();
		}
	}
}
