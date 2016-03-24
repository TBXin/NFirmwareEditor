using System;
using System.Collections.Generic;
using System.Drawing;
using JetBrains.Annotations;
using NFirmware;

namespace NFirmwareEditor.Managers
{
	internal static class ImageCacheManager
	{
		private static IDictionary<int, Image> s_block1ImageCache = new Dictionary<int, Image>();
		private static IDictionary<int, Image> s_block2ImageCache = new Dictionary<int, Image>();

		public static Image GetImage(int key, BlockType blockType)
		{
			switch (blockType)
			{
				case BlockType.Block1: return s_block1ImageCache[key];
				case BlockType.Block2: return s_block2ImageCache[key];
				default: throw new ArgumentOutOfRangeException("blockType", blockType, null);
			}
		}

		public static void SetImage(int key, BlockType blockType, [NotNull] Image image)
		{
			if (image == null) throw new ArgumentNullException("image");

			switch (blockType)
			{
				case BlockType.Block1:
				{
					s_block1ImageCache[key] = image;
					break;
				}
				case BlockType.Block2:
				{
					s_block2ImageCache[key] = image;
					break;
				}
				default: throw new ArgumentOutOfRangeException("blockType", blockType, null);
			}
		}

		public static void RebuildImageCache([NotNull] Firmware firmware)
		{
			if (firmware == null) throw new ArgumentNullException("firmware");

			var block1ImageCache = new Dictionary<int, Image>();
			foreach (var imageMetadata in firmware.Block1Images)
			{
				try
				{
					var imageData = firmware.ReadImage(imageMetadata);
					var image = FirmwareImageProcessor.CreateBitmap(imageData);
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
					var image = FirmwareImageProcessor.CreateBitmap(imageData);
					block2ImageCache[imageMetadata.Index] = image;
				}
				catch
				{
					block2ImageCache[imageMetadata.Index] = new Bitmap(1, 1);
				}
			}
			block2ImageCache.Add(0, new Bitmap(1, 16));

			SetCache(BlockType.Block1, block1ImageCache);
			SetCache(BlockType.Block2, block2ImageCache);
		}

		private static void SetCache(BlockType blockType, [NotNull] IDictionary<int, Image> newCache)
		{
			if (newCache == null) throw new ArgumentNullException("newCache");

			switch (blockType)
			{
				case BlockType.Block1:
				{
					UpdateCache(ref s_block1ImageCache, newCache);
					break;
				}
				case BlockType.Block2:
				{
					UpdateCache(ref s_block2ImageCache, newCache);
					break;
				}
				default: throw new ArgumentOutOfRangeException("blockType", blockType, null);
			}
		}

		private static void UpdateCache(ref IDictionary<int, Image> oldCache, IDictionary<int, Image> newCache)
		{
			var previousCache = oldCache;
			oldCache = newCache;

			foreach (var pair in previousCache)
			{
				pair.Value.Dispose();
			}
		}
	}
}
