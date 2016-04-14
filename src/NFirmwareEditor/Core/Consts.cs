namespace NFirmwareEditor.Core
{
	internal static class Consts
	{
		internal const string ApplicationVersion = "3.51";
		internal const string ApplicationTitleWoVersion = "NFirmwareEditor";
		internal const string ApplicationTitle = ApplicationTitleWoVersion + " v" + ApplicationVersion;

		internal const string PatchFileExtensionWoAsterisk = ".patch";
		internal const string ResourcePackFileExtensionWoAsterisk = ".respack";
		internal const string PatchFileExtension = "*" + PatchFileExtensionWoAsterisk;
		internal const string ResourcePackFileExtension = "*" + ResourcePackFileExtensionWoAsterisk;

		internal const string FirmwareFilter = "Firmware file|*.bin";
		internal const string PatchFilter = "Patch file|" + PatchFileExtension;
		internal const string ExportResourcePackFilter = "Resource packs|" + ResourcePackFileExtension;
		internal const string BitmapImportFilter = "Common graphic files|*.bmp;*.png;*.jpg;*.jpeg";
		internal const string Encrypted = "encrypted";
		internal const string Decrypted = "decrypted";
		internal const string EncryptedOrDecrypted = Encrypted + " or " + Decrypted;

		internal const int MaximumImageWidthAndHeight = 128;
		internal const int ImageListBoxItemMaxHeight = 32 * 2;
		internal const int ImageListBoxItemImageMargin = 6;

		internal const string PatchXmlNamespace = "urn:NFirmwareEditor";
	}
}
