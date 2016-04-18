using System;
using JetBrains.Annotations;

namespace NFirmware
{
	public class FirmwareLoadResult
	{
		internal FirmwareLoadResult([NotNull] Firmware firmware, bool isEncrypted)
		{
			if (firmware == null) throw new ArgumentNullException("firmware");

			Firmware = firmware;
			IsEncrypted = isEncrypted;
		}

		[NotNull]
		public Firmware Firmware { get; private set; }

		public bool IsEncrypted { get; private set; }
	}
}
