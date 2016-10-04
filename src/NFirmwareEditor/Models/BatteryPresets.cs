using System.Collections.Generic;

namespace NFirmwareEditor.Models
{
	internal static class BatteryPresets
	{
		private static readonly PercentsVoltage[] s_genericTable =
		{
			new PercentsVoltage(0, 310),
			new PercentsVoltage(10, 330),
			new PercentsVoltage(20, 342),
			new PercentsVoltage(30, 350),
			new PercentsVoltage(40, 358),
			new PercentsVoltage(50, 363),
			new PercentsVoltage(60, 368),
			new PercentsVoltage(70, 379),
			new PercentsVoltage(80, 389),
			new PercentsVoltage(90, 400),
			new PercentsVoltage(100, 410)
		};

		private static readonly PercentsVoltage[] s_samsung25RTable =
		{
			new PercentsVoltage(0, 302),
			new PercentsVoltage(1, 310),
			new PercentsVoltage(5, 327),
			new PercentsVoltage(9, 341),
			new PercentsVoltage(25, 357),
			new PercentsVoltage(39, 365),
			new PercentsVoltage(70, 391),
			new PercentsVoltage(84, 405),
			new PercentsVoltage(93, 409),
			new PercentsVoltage(97, 417),
			new PercentsVoltage(100, 418)
		};

		private static readonly PercentsVoltage[] s_lgHg2Table =
		{
			new PercentsVoltage(0, 312),
			new PercentsVoltage(4, 326),
			new PercentsVoltage(9, 338),
			new PercentsVoltage(13, 345),
			new PercentsVoltage(26, 356),
			new PercentsVoltage(38, 364),
			new PercentsVoltage(79, 401),
			new PercentsVoltage(96, 411),
			new PercentsVoltage(98, 414),
			new PercentsVoltage(99, 417),
			new PercentsVoltage(100, 418)
		};

		private static readonly PercentsVoltage[] s_lgHe4RTable =
		{
			new PercentsVoltage(0, 310),
			new PercentsVoltage(5, 317),
			new PercentsVoltage(15, 329),
			new PercentsVoltage(20, 334),
			new PercentsVoltage(25, 340),
			new PercentsVoltage(30, 346),
			new PercentsVoltage(40, 357),
			new PercentsVoltage(50, 368),
			new PercentsVoltage(60, 380),
			new PercentsVoltage(80, 402),
			new PercentsVoltage(100, 418)
		};

		private static readonly PercentsVoltage[] s_samsung30QTable =
		{
			new PercentsVoltage(0, 312),
			new PercentsVoltage(15, 340),
			new PercentsVoltage(23, 352),
			new PercentsVoltage(54, 381),
			new PercentsVoltage(68, 391),
			new PercentsVoltage(75, 400),
			new PercentsVoltage(81, 403),
			new PercentsVoltage(94, 408),
			new PercentsVoltage(97, 411),
			new PercentsVoltage(99, 416),
			new PercentsVoltage(100, 418)
		};

		private static readonly PercentsVoltage[] s_sonyVtc4Table =
		{
			new PercentsVoltage(0, 314),
			new PercentsVoltage(1, 321),
			new PercentsVoltage(2, 331),
			new PercentsVoltage(4, 339),
			new PercentsVoltage(6, 343),
			new PercentsVoltage(14, 351),
			new PercentsVoltage(22, 356),
			new PercentsVoltage(49, 367),
			new PercentsVoltage(66, 380),
			new PercentsVoltage(99, 417),
			new PercentsVoltage(100, 418)
		};

		private static readonly PercentsVoltage[] s_sonyVtc5Table =
		{
			new PercentsVoltage(0, 305),
			new PercentsVoltage(1, 310),
			new PercentsVoltage(2, 320),
			new PercentsVoltage(4, 329),
			new PercentsVoltage(14, 342),
			new PercentsVoltage(23, 351),
			new PercentsVoltage(45, 365),
			new PercentsVoltage(79, 396),
			new PercentsVoltage(95, 411),
			new PercentsVoltage(99, 417),
			new PercentsVoltage(100, 418)
		};

		internal static readonly IDictionary<string, CustomBattery> Presets = new Dictionary<string, CustomBattery>
		{
			{ "Generic", new CustomBattery(s_genericTable, 280) },
			{ "Samsung 25R", new CustomBattery(s_samsung25RTable, 280) },
			{ "Samsung 30Q", new CustomBattery(s_samsung30QTable, 280) },
			{ "LG HG2", new CustomBattery(s_lgHg2Table, 280) },
			{ "LG HE4", new CustomBattery(s_lgHe4RTable, 280) },
			{ "Sony VTC4", new CustomBattery(s_sonyVtc4Table, 280) },
			{ "Sony VTC5", new CustomBattery(s_sonyVtc5Table, 280) }
		};
	}
}
