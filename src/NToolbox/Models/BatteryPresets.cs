using System.Collections.Generic;

namespace NToolbox.Models
{
	internal static class BatteryPresets
	{
		private static readonly ArcticFoxConfiguration.PercentsVoltage[] s_genericTable =
		{
			new ArcticFoxConfiguration.PercentsVoltage(0, 310),
			new ArcticFoxConfiguration.PercentsVoltage(10, 330),
			new ArcticFoxConfiguration.PercentsVoltage(20, 342),
			new ArcticFoxConfiguration.PercentsVoltage(30, 350),
			new ArcticFoxConfiguration.PercentsVoltage(40, 358),
			new ArcticFoxConfiguration.PercentsVoltage(50, 363),
			new ArcticFoxConfiguration.PercentsVoltage(60, 368),
			new ArcticFoxConfiguration.PercentsVoltage(70, 379),
			new ArcticFoxConfiguration.PercentsVoltage(80, 389),
			new ArcticFoxConfiguration.PercentsVoltage(90, 400),
			new ArcticFoxConfiguration.PercentsVoltage(100, 410)
		};

		private static readonly ArcticFoxConfiguration.PercentsVoltage[] s_samsung25RTable =
		{
			new ArcticFoxConfiguration.PercentsVoltage(0, 302),
			new ArcticFoxConfiguration.PercentsVoltage(1, 310),
			new ArcticFoxConfiguration.PercentsVoltage(5, 327),
			new ArcticFoxConfiguration.PercentsVoltage(9, 341),
			new ArcticFoxConfiguration.PercentsVoltage(25, 357),
			new ArcticFoxConfiguration.PercentsVoltage(39, 365),
			new ArcticFoxConfiguration.PercentsVoltage(70, 391),
			new ArcticFoxConfiguration.PercentsVoltage(84, 405),
			new ArcticFoxConfiguration.PercentsVoltage(93, 409),
			new ArcticFoxConfiguration.PercentsVoltage(97, 417),
			new ArcticFoxConfiguration.PercentsVoltage(100, 418)
		};

		private static readonly ArcticFoxConfiguration.PercentsVoltage[] s_lgHg2Table =
		{
			new ArcticFoxConfiguration.PercentsVoltage(0, 312),
			new ArcticFoxConfiguration.PercentsVoltage(4, 326),
			new ArcticFoxConfiguration.PercentsVoltage(9, 338),
			new ArcticFoxConfiguration.PercentsVoltage(13, 345),
			new ArcticFoxConfiguration.PercentsVoltage(26, 356),
			new ArcticFoxConfiguration.PercentsVoltage(38, 364),
			new ArcticFoxConfiguration.PercentsVoltage(79, 401),
			new ArcticFoxConfiguration.PercentsVoltage(96, 411),
			new ArcticFoxConfiguration.PercentsVoltage(98, 414),
			new ArcticFoxConfiguration.PercentsVoltage(99, 417),
			new ArcticFoxConfiguration.PercentsVoltage(100, 418)
		};

		private static readonly ArcticFoxConfiguration.PercentsVoltage[] s_lgHe4RTable =
		{
			new ArcticFoxConfiguration.PercentsVoltage(0, 310),
			new ArcticFoxConfiguration.PercentsVoltage(5, 317),
			new ArcticFoxConfiguration.PercentsVoltage(15, 329),
			new ArcticFoxConfiguration.PercentsVoltage(20, 334),
			new ArcticFoxConfiguration.PercentsVoltage(25, 340),
			new ArcticFoxConfiguration.PercentsVoltage(30, 346),
			new ArcticFoxConfiguration.PercentsVoltage(40, 357),
			new ArcticFoxConfiguration.PercentsVoltage(50, 368),
			new ArcticFoxConfiguration.PercentsVoltage(60, 380),
			new ArcticFoxConfiguration.PercentsVoltage(80, 402),
			new ArcticFoxConfiguration.PercentsVoltage(100, 418)
		};

		private static readonly ArcticFoxConfiguration.PercentsVoltage[] s_samsung30QTable =
		{
			new ArcticFoxConfiguration.PercentsVoltage(0, 312),
			new ArcticFoxConfiguration.PercentsVoltage(15, 340),
			new ArcticFoxConfiguration.PercentsVoltage(23, 352),
			new ArcticFoxConfiguration.PercentsVoltage(54, 381),
			new ArcticFoxConfiguration.PercentsVoltage(68, 391),
			new ArcticFoxConfiguration.PercentsVoltage(75, 400),
			new ArcticFoxConfiguration.PercentsVoltage(81, 403),
			new ArcticFoxConfiguration.PercentsVoltage(94, 408),
			new ArcticFoxConfiguration.PercentsVoltage(97, 411),
			new ArcticFoxConfiguration.PercentsVoltage(99, 416),
			new ArcticFoxConfiguration.PercentsVoltage(100, 418)
		};

		private static readonly ArcticFoxConfiguration.PercentsVoltage[] s_sonyVtc4Table =
		{
			new ArcticFoxConfiguration.PercentsVoltage(0, 314),
			new ArcticFoxConfiguration.PercentsVoltage(1, 321),
			new ArcticFoxConfiguration.PercentsVoltage(2, 331),
			new ArcticFoxConfiguration.PercentsVoltage(4, 339),
			new ArcticFoxConfiguration.PercentsVoltage(6, 343),
			new ArcticFoxConfiguration.PercentsVoltage(14, 351),
			new ArcticFoxConfiguration.PercentsVoltage(22, 356),
			new ArcticFoxConfiguration.PercentsVoltage(49, 367),
			new ArcticFoxConfiguration.PercentsVoltage(66, 380),
			new ArcticFoxConfiguration.PercentsVoltage(99, 417),
			new ArcticFoxConfiguration.PercentsVoltage(100, 418)
		};

		private static readonly ArcticFoxConfiguration.PercentsVoltage[] s_sonyVtc5Table =
		{
			new ArcticFoxConfiguration.PercentsVoltage(0, 305),
			new ArcticFoxConfiguration.PercentsVoltage(1, 310),
			new ArcticFoxConfiguration.PercentsVoltage(2, 320),
			new ArcticFoxConfiguration.PercentsVoltage(4, 329),
			new ArcticFoxConfiguration.PercentsVoltage(14, 342),
			new ArcticFoxConfiguration.PercentsVoltage(23, 351),
			new ArcticFoxConfiguration.PercentsVoltage(45, 365),
			new ArcticFoxConfiguration.PercentsVoltage(79, 396),
			new ArcticFoxConfiguration.PercentsVoltage(95, 411),
			new ArcticFoxConfiguration.PercentsVoltage(99, 417),
			new ArcticFoxConfiguration.PercentsVoltage(100, 418)
		};

		internal static readonly IDictionary<string, ArcticFoxConfiguration.CustomBattery> Presets = new Dictionary<string, ArcticFoxConfiguration.CustomBattery>
		{
			{ "Generic", new ArcticFoxConfiguration.CustomBattery(s_genericTable, 280) },
			{ "Samsung 25R", new ArcticFoxConfiguration.CustomBattery(s_samsung25RTable, 280) },
			{ "Samsung 30Q", new ArcticFoxConfiguration.CustomBattery(s_samsung30QTable, 280) },
			{ "LG HG2", new ArcticFoxConfiguration.CustomBattery(s_lgHg2Table, 280) },
			{ "LG HE4", new ArcticFoxConfiguration.CustomBattery(s_lgHe4RTable, 280) },
			{ "Sony VTC4", new ArcticFoxConfiguration.CustomBattery(s_sonyVtc4Table, 280) },
			{ "Sony VTC5", new ArcticFoxConfiguration.CustomBattery(s_sonyVtc5Table, 280) }
		};
	}
}
