using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace NFirmwareEditor.Models
{
	internal static class SensorsKeys
	{
		internal const string FiringKey = "FIRING";
		internal const string StandbyKey = "STANDBY";

		internal const string BatteryVoltage = "BATT";
		internal const string Charging = "CHG";
		internal const string BoardTemperature = "BRD";
		internal const string PowerSet = "SPWR";
		internal const string TemperatureSet = "STEMP";
		internal const string Power = "RPWR";
		internal const string Temperature = "RTEMP";
		internal const string Resistance = "RES";
		internal const string RealResistance = "RESM";
		internal const string Celcius = "CELS";
		internal const string OutputVoltage = "VOUT";
		internal const string OutputCurrent = "CUR";
	}

	internal class DeviceSensorsData
	{
		public static IDictionary<string, float> Parse([NotNull] string message)
		{
			if (string.IsNullOrEmpty(message)) throw new ArgumentNullException("message");

			var isStandby = message.StartsWith(SensorsKeys.StandbyKey, StringComparison.OrdinalIgnoreCase);
			var isFiring = message.StartsWith(SensorsKeys.FiringKey, StringComparison.OrdinalIgnoreCase);

			if (!isFiring && !isStandby) return null;

			if (isFiring) message = message.Substring(SensorsKeys.FiringKey.Length + 1);
			if (isStandby) message = message.Substring(SensorsKeys.StandbyKey.Length + 1);

			var sensors = CreateEmptySensorsData();
			var rawPairs = message.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
			foreach (var rawData in rawPairs)
			{
				var pair = rawData.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
				if (pair.Length != 2) continue;

				sensors[pair[0]] = int.Parse(pair[1]);
			}

			sensors[SensorsKeys.BatteryVoltage] /= 100f;
			sensors[SensorsKeys.Resistance] /= 100f;
			sensors[SensorsKeys.RealResistance] /= 1000f;
			sensors[SensorsKeys.PowerSet] /= 10f;
			sensors[SensorsKeys.Power] /= 10f;
			sensors[SensorsKeys.OutputVoltage] /= 100f;
			sensors[SensorsKeys.OutputCurrent] /= 10f;
			return sensors;
		}

		private static Dictionary<string, float> CreateEmptySensorsData()
		{
			return new Dictionary<string, float>
			{
				{ SensorsKeys.BatteryVoltage, 0 },
				{ SensorsKeys.Charging, 0 },
				{ SensorsKeys.BoardTemperature, 0 },
				{ SensorsKeys.Resistance, 0 },
				{ SensorsKeys.RealResistance, 0 },
				{ SensorsKeys.PowerSet, 0 },
				{ SensorsKeys.Power, 0 },
				{ SensorsKeys.TemperatureSet, 0 },
				{ SensorsKeys.Temperature, 0 },
				{ SensorsKeys.Celcius, 0 },
				{ SensorsKeys.OutputVoltage, 0 },
				{ SensorsKeys.OutputCurrent, 0 }
			};
		}
	}
}
