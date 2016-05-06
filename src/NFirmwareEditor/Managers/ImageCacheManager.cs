using System;
using System.Collections.Generic;
using System.Drawing;
using JetBrains.Annotations;
using NFirmware;

namespace NFirmwareEditor.Managers
{
	internal static class ImageCacheManager
	{
		private static readonly IDictionary<BlockType, IDictionary<int, Image>> s_glyphPreviewCache = new Dictionary<BlockType, IDictionary<int, Image>>
		{
			{ BlockType.Block1, new Dictionary<int, Image>() },
			{ BlockType.Block2, new Dictionary<int, Image>() }
		};

		private static readonly IDictionary<int, Image> s_stringPreviewCache = new Dictionary<int, Image>();

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

		public static void RebuildImageCache([NotNull] Firmware firmware)
		{
			if (firmware == null) throw new ArgumentNullException("firmware");

			var block1RawImages = new Dictionary<int, bool[,]>();

			var block1ImageCache = new Dictionary<int, Image>();
			foreach (var imageMetadata in firmware.Block1Images)
			{
				try
				{
					var imageData = firmware.ReadImage(imageMetadata);
					var image = BitmapProcessor.CreateBitmapFromRaw(imageData);

					block1RawImages[imageMetadata.Index] = imageData;
					block1ImageCache[imageMetadata.Index] = image;
				}
				catch
				{
					block1ImageCache[imageMetadata.Index] = new Bitmap(1, 1);
				}
			}
			block1ImageCache.Add(0, new Bitmap(1, 16));

			var block2ImageCache = new Dictionary<int, Image>();
			foreach (var imageMetadata in firmware.Block2Images)
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
			block2ImageCache.Add(0, new Bitmap(1, 16));

			SetGlyphCache(BlockType.Block1, block1ImageCache);
			SetGlyphCache(BlockType.Block2, block2ImageCache);

			foreach (var stringMetadata in firmware.Block1Strings)
			{
				try
				{
					var stringData = firmware.ReadString(stringMetadata);
					var imageData = FirmwareImageProcessor.GetStringImageData(stringData, block1RawImages, firmware.Definition.CharsToCorrect);
					var image = BitmapProcessor.CreateBitmapFromRaw(imageData, 1);

					SetStringImage(stringMetadata.Index, image);
				}
				catch
				{
					SetStringImage(stringMetadata.Index, new Bitmap(1, 1));
				}
			}
		}

		private static void SetGlyphCache(BlockType blockType, [NotNull] IDictionary<int, Image> newCache)
		{
			if (newCache == null) throw new ArgumentNullException("newCache");

			var oldCache = s_glyphPreviewCache[blockType];
			s_glyphPreviewCache[blockType] = newCache;

			foreach (var pair in oldCache)
			{
				pair.Value.Dispose();
			}
		}
	}
}
