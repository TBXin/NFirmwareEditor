using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace NCore.USB
{
	public class HidDeviceInfo
	{
		private static readonly HidDeviceInfo s_unknownHidDevice = new HidDeviceInfo("Unknown device");

		private static readonly IDictionary<string, HidDeviceInfo> s_supportedDevices = new Dictionary<string, HidDeviceInfo>
		{
			{ "E052", new HidDeviceInfo("Joyetech eVic VTC Mini", 64, 40) },
			{ "E043", new HidDeviceInfo("Joyetech eVic VTwo", 64, 40) },
			{ "E115", new HidDeviceInfo("Joyetech eVic VTwo Mini", 64, 40) },
			{ "E079", new HidDeviceInfo("Joyetech eVic VTC Dual", 64, 40) },
			{ "E150", new HidDeviceInfo("Joyetech eVic Basic", 64, 40) },
			{ "E092", new HidDeviceInfo("Joyetech eVic AIO", 64, 40) },

			{ "E060", new HidDeviceInfo("Joyetech Cuboid", 64, 40) },
			{ "E056", new HidDeviceInfo("Joyetech Cuboid Mini", 64, 40) },

			{ "E083", new HidDeviceInfo("Joyetech eGrip II", 64, 40) },

			{ "M972", new HidDeviceInfo("Eleaf iStick TC200W", 96, 16) },
			{ "M011", new HidDeviceInfo("Eleaf iStick TC100W", 96, 16) },
			{ "M041", new HidDeviceInfo("Eleaf iStick Pico", 96, 16) },
			{ "M045", new HidDeviceInfo("Eleaf iStick Pico Mega", 96, 16) },
			{ "M046", new HidDeviceInfo("Eleaf iStick Power", 96, 16) },
			{ "M037", new HidDeviceInfo("Eleaf ASTER", 96, 16) },

			{ "W007", new HidDeviceInfo("Wismec Presa TC75W", 64, 48) },
			{ "W017", new HidDeviceInfo("Wismec Presa TC100W", 64, 48) },

			{ "W018", new HidDeviceInfo("Wismec Reuleaux RX2/3", 64, 48) },
			{ "W014", new HidDeviceInfo("Wismec Reuleaux RX200", 96, 16) },
			{ "W033", new HidDeviceInfo("Wismec Reuleaux RX200S", 64, 48) },
			{ "W026", new HidDeviceInfo("Wismec Reuleaux RX75", 64, 48) },

			{ "W010", new HidDeviceInfo("Vaporflask Classic") },
			{ "W011", new HidDeviceInfo("Vaporflask Lite") },
			{ "W013", new HidDeviceInfo("Vaporflask Stout") },

			{ "W016", new HidDeviceInfo("Beyondvape Centurion") }
		};

		[NotNull]
		public static HidDeviceInfo UnknownDevice
		{
			get { return s_unknownHidDevice; }
		}

		[NotNull]
		public static HidDeviceInfo Get([CanBeNull] string productId)
		{
			return string.IsNullOrEmpty(productId) || !s_supportedDevices.ContainsKey(productId)
				? s_unknownHidDevice
				: s_supportedDevices[productId];
		}

		public HidDeviceInfo(string name)
		{
			if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");

			Name = name;
			CanUploadLogo = false;
		}

		public HidDeviceInfo([NotNull] string name, byte logoWidth, byte logoHeight)
		{
			if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");

			Name = name;
			LogoWidth = logoWidth;
			LogoHeight = logoHeight;
			CanUploadLogo = true;
		}

		public string Name { get; private set; }

		public bool CanUploadLogo { get; private set; }

		public byte LogoWidth { get; private set; }

		public byte LogoHeight { get; private set; }
	}
}