namespace NFirmwareEditor.Core
{
	internal static class Consts
	{
		internal const string ApplicationVersion = "6.3";
		internal const string ApplicationTitleWoVersion = "NFirmwareEditor";
		internal const string ApplicationTitle = ApplicationTitleWoVersion + " v" + ApplicationVersion;

		internal const string DefinitionFileExtensionWoAsterisk = ".xml";
		internal const string PatchFileExtensionWoAsterisk = ".patch";
		internal const string ResourcePackFileExtensionWoAsterisk = ".respack";
		internal const string BitmapFileExtensionWoAsterisk = ".bmp";

		internal const string DefinitionFileExtension = "*" + DefinitionFileExtensionWoAsterisk;
		internal const string PatchFileExtension = "*" + PatchFileExtensionWoAsterisk;
		internal const string ResourcePackFileExtension = "*" + ResourcePackFileExtensionWoAsterisk;

		internal const string FirmwareFilter = "Firmware file|*.bin";
		internal const string DataFlashFilter = "Dataflash file|*.dataflash";
		internal const string DefinitionFilter = "Definition file|*.xml";
		internal const string PatchFilter = "Patch file|" + PatchFileExtension;
		internal const string ExportResourcePackFilter = "Resource packs|" + ResourcePackFileExtension;
		internal const string BitmapImportFilter = "Common graphic files|*.bmp;*.png;*.jpg;*.jpeg;*.gif";
		internal const string BitmapExportFilter = "Common graphic files|*.bmp";
		internal const string PngExportFilter = "Portable Network Graphics|*.png";
		internal const string FontImportFilter = "Font files|*.ttf;*.otf";
		internal const string CsvFilter = "Comma-separated values|*.csv";

		internal const string SimpleBackupFileNameFormat = "{0}_backup";
		internal const string ExtendedBackupDirectoryName = "Backups";
		internal const string ExtendedBackupFileNameFormat = "{0}_{1:yyyy.MM.dd HH.mm.ss.fff}";

		internal const string Encrypted = "encrypted";
		internal const string Decrypted = "decrypted";
		internal const string EncryptedOrDecrypted = Encrypted + " or " + Decrypted;

		internal const int MaximumImageWidthAndHeight = 128;
		internal const int ImageListBoxItemMaxHeight = 32 * 2;
		internal const int ImageListBoxItemImageMargin = 6;

		internal const string HomePage = "http://www.ecigtalk.ru/forum/f16/t101098.html";
		internal const string ReleasesPage = "https://github.com/TBXin/NFirmwareEditor/releases";
	}
}
