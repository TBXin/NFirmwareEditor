using System;
using JetBrains.Annotations;

namespace NCore.USB
{
	public class HidDeviceInfo
	{
		private readonly bool m_canUploadLogo;

		public HidDeviceInfo(string name)
		{
			if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");

			Name = name;
			m_canUploadLogo = false;
		}

		public HidDeviceInfo([NotNull] string name, byte logoWidth, byte logoHeight)
		{
			if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");

			Name = name;
			LogoWidth = logoWidth;
			LogoHeight = logoHeight;
			m_canUploadLogo = true;
		}

		public string Name { get; private set; }

		public bool CanUploadLogo
		{
			get { return m_canUploadLogo; }
		}

		public byte LogoWidth { get; private set; }

		public byte LogoHeight { get; private set; }
	}
}