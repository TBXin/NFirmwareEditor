namespace NFirmwareEditor.Core
{
	internal static class Consts
	{
		internal const string ApplicationTitle = "NFirmwareEditor";
		internal const string ApplicationVersion = "3.0";
		internal const string FirmwareFilter = "Firmware file|*.bin";
		internal const string PatchFilter = "Patch file|*.patch";
		internal const string ExportImageFilter = "Exported images|*.images";
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
