using System;
using System.Drawing;
using JetBrains.Annotations;
using NFirmware;
using NFirmwareEditor.Managers;

namespace NFirmwareEditor.Core
{
	internal static class Extensions
	{
		internal static Size GetSize([NotNull] this bool[,] imageData)
		{
			if (imageData == null) throw new ArgumentNullException("imageData");
			return FirmwareImageProcessor.GetImageSize(imageData);
		}

		internal static bool[,] CreateImage([NotNull] this FirmwareImageMetadata imageMetadata)
		{
			if (imageMetadata == null) throw new ArgumentNullException("imageMetadata");
			return new bool[imageMetadata.Width, imageMetadata.Height];
		}
	}
}
