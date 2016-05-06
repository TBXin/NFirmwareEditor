using System;
using System.Collections.Generic;
using System.Drawing;
using JetBrains.Annotations;
using NFirmware;
using NFirmwareEditor.Core;

namespace NFirmwareEditor.Managers
{
	internal static class ImageCacheManager
	{
		private static readonly IDictionary<BlockType, IDictionary<int, Image>> s_glyphPreviewCache = new Dictionary<BlockType, IDictionary<int, Image>>
		{
			{ BlockType.Block1, new Dictionary<int, Image>() },
			{ BlockType.Block2, new Dictionary<int, Image>() }
		};
		private static IDictionary<int, Image> s_stringPreviewCache = new Dictionary<int, Image>();

		public static Image GetGlyphImage(int key, BlockType blockType)
		{
			return s_glyphPreviewCache[blockType][key];
		}

		public static void SetGlyphImage(int key, BlockType blockType, [NotNull] Image image)
		{
			if (image == null) throw new ArgumentNullException("image");
			s_glyphPreviewCache[blockType][key] = image;
		}

		public static Image GetStringImage(int key)
		{
			return s_stringPreviewCache[key];
		}

		public static void SetStringImage(int key, [NotNull] Image image)
		{
			if (image == null) throw new ArgumentNullException("image");
			s_stringPreviewCache[key] = image;
		}

		public static void RebuildCache([NotNull] Firmware firmware)
		{
			if (firmware == null) throw new ArgumentNullException("firmware");

			RebuildGlyphImageCache(firmware);
			RebuildStringImageCache(firmware, BlockType.Block1);
		}

		public static void RebuildGlyphImageCache([NotNull] Firmware firmware)
		{
			if (firmware == null) throw new ArgumentNullException("firmware");

			var block1ImageCache = new Dictionary<int, Image> { { 0, new Bitmap(1, 1) } };
			foreach (var imageMetadata in firmware.Block1Images.Values)
			{
				try
				{
					var imageData = firmware.ReadImage(imageMetadata);
					var image = BitmapProcessor.CreateBitmapFromRaw(imageData);

					block1ImageCache[imageMetadata.Index] = image;
				}
				catch
				{
					block1ImageCache[imageMetadata.Index] = new Bitmap(1, 1);
				}
			}

			var block2ImageCache = new Dictionary<int, Image> { { 0, new Bitmap(1, 1) } };
			foreach (var imageMetadata in firmware.Block2Images.Values)
			{
				try
				{
					var imageData = firmware.ReadImage(imageMetadata);
					var image = BitmapProcessor.CreateBitmapFromRaw(imageData);
					block2ImageCache[imageMetadata.Index] = image;
				}
				catch
				{
					block2ImageCache[imageMetadata.Index] = new Bitmap(1, 1);
				}
			}

			SetGlyphCache(BlockType.Block1, block1ImageCache);
			SetGlyphCache(BlockType.Block2, block2ImageCache);
		}

		public static void RebuildStringImageCache([NotNull] Firmware firmware, BlockType blockType)
		{
			if (firmware == null) throw new ArgumentNullException("firmware");

			var firmwareImages = blockType == BlockType.Block1
				? firmware.Block1Images
				: firmware.Block2Images;

			var glyphData = new Dictionary<int, bool[,]>();
			foreach (var kvp in firmwareImages)
			{
				glyphData[kvp.Key] = firmware.ReadImage(kvp.Value);
			}

			var stringImageCache = new Dictionary<int, Image>();
			foreach (var stringMetadata in firmware.Block1Strings)
			{
				try
				{
					var stringData = firmware.ReadString(stringMetadata);
					var imageData = FirmwareImageProcessor.GetStringImageData(stringData, glyphData, firmware.Definition.CharsToCorrect);
					var image = BitmapProcessor.CreateBitmapFromRaw(imageData, 1);

					stringImageCache[stringMetadata.Index] = image;
				}
				catch
				{
					stringImageCache[stringMetadata.Index] = new Bitmap(1, 1);
				}
			}
			SetStringCache(stringImageCache);
		}

		private static void SetGlyphCache(BlockType blockType, [NotNull] IDictionary<int, Image> newCache)
		{
			if (newCache == null) throw new ArgumentNullException("newCache");

			var oldCache = s_glyphPreviewCache[blockType];
			s_glyphPreviewCache[blockType] = newCache;

			foreach (var pair in oldCache)
			{
				var image = pair.Value;
				Safe.Execute(() => image.Dispose());
			}
		}

		private static void SetStringCache(IDictionary<int, Image> newCache)
		{
			var oldCache = s_stringPreviewCache;
			s_stringPreviewCache = newCache;

			foreach (var pair in oldCache)
			{
				var image = pair.Value;
				Safe.Execute(() => image.Dispose());
			}
		}
	}
}
