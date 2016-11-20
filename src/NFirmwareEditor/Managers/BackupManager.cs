using System;
using System.IO;
using JetBrains.Annotations;
using NCore;
using NFirmwareEditor.Core;
using NFirmwareEditor.Models;

namespace NFirmwareEditor.Managers
{
	internal class BackupManager
	{
		public void CreateBackup([NotNull] string fileName, BackupCreationMode mode)
		{
			if (string.IsNullOrEmpty(fileName)) throw new ArgumentNullException("fileName");

			switch (mode)
			{
				case BackupCreationMode.Disabled: return;
				case BackupCreationMode.Simple: CreateSimpleBackup(fileName); return;
				case BackupCreationMode.Extended: CreateExtendedBackup(fileName); return;
				default: throw new ArgumentOutOfRangeException("mode", mode, null);
			}
		}

		private static void CreateSimpleBackup([NotNull] string fileName)
		{
			var fi = new FileInfo(fileName);
			var originalFileName = Path.GetFileNameWithoutExtension(fileName);
			var backupFileName = string.Format(Consts.SimpleBackupFileNameFormat, originalFileName) + fi.Extension;
			var backupFilePath = Path.Combine(fi.DirectoryName.Nvl(string.Empty), backupFileName);

			File.Copy(fileName, backupFilePath, true);
		}

		private static void CreateExtendedBackup([NotNull] string fileName)
		{
			var fi = new FileInfo(fileName);
			var directoryPath = Path.GetDirectoryName(fileName);
			var originalFileName = Path.GetFileNameWithoutExtension(fileName);
			var backupFileName = string.Format(Consts.ExtendedBackupFileNameFormat, originalFileName, DateTime.Now) + fi.Extension;

			var backupDirectory = Path.Combine(directoryPath.Nvl(string.Empty), Consts.ExtendedBackupDirectoryName);
			var backupFilePath = Path.Combine(backupDirectory, backupFileName);

			if (!Directory.Exists(backupDirectory))
			{
				Directory.CreateDirectory(backupDirectory);
			}
			File.Copy(fileName, backupFilePath);
		}
	}
}
