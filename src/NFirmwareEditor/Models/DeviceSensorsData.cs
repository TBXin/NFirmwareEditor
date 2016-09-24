using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace NFirmwareEditor.Models
{
	internal static class SensorKeys
	{
		internal const string BatteryVoltage = "BATT";
		internal const string Charging = "CHG";
		internal const string BoardTemperature = "BRD";
		internal const string Power = "PWR";
		internal const string Temperature = "TEMP";
		internal const string Resistance = "RES";
		internal const string RealResistance = "RESM";
		internal const string Celcius = "CELS";
		internal const string OutputVoltage = "VOUT";
		internal const string OutputCurrent = "CUR";

		internal static readonly string[] AllKeys =
		{
			BatteryVoltage, Charging, BoardTemperature, Power, Temperature, Resistance, RealResistance, Celcius, OutputVoltage, OutputCurrent
		};
	}

	internal class DeviceSensorsData
	{
		internal const string FiringKey = "FIRING";
		internal const string StandbyKey = "STANDBY";

		public float BatteryVoltage { get; set; }

		public bool IsCharging { get; set; }

		public float BoardTemperature { get; set; }

		public float Resistance { get; set; }

		public float RealResistance { get; set; }

		public float Power { get; set; }

		public float Temperature { get; set; }

		public bool IsCelcius { get; set; }

		public float OutputVoltage { get; set; }

		public float OutputCurrent { get; set; }

		public static DeviceSensorsData Parse([NotNull] string message)
		{
			if (string.IsNullOrEmpty(message)) throw new ArgumentNullException("message");

			var isStandby = message.StartsWith(StandbyKey, StringComparison.OrdinalIgnoreCase);
			var isFiring = message.StartsWith(FiringKey, StringComparison.OrdinalIgnoreCase);

			if (!isFiring && !isStandby) return null;

			if (isFiring) message = message.Substring(FiringKey.Length + 1);
			if (isStandby) message = message.Substring(StandbyKey.Length + 1);

			var sensors = CreateEmptySensorsData();
			var rawPairs = message.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
			foreach (var rawData in rawPairs)
			{
				var pair = rawData.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
				if (pair.Length != 2) continue;

				sensors[pair[0]] = int.Parse(pair[1]);
			}

			var result = new DeviceSensorsData
			{
				BatteryVoltage = sensors[SensorKeys.BatteryVoltage] / 100f,
				IsCharging = sensors[SensorKeys.Charging] == 1,
				BoardTemperature = sensors[SensorKeys.BoardTemperature],
				Resistance = sensors[SensorKeys.Resistance] / 100f,
				RealResistance = sensors[SensorKeys.RealResistance] / 1000f,
				Power = sensors[SensorKeys.Power] / 10f,
				Temperature = sensors[SensorKeys.Temperature],
				IsCelcius = sensors[SensorKeys.Celcius] == 1,
				OutputVoltage = sensors[SensorKeys.OutputVoltage] / 10f,
				OutputCurrent = sensors[SensorKeys.OutputCurrent] / 10f
			};
			return result;
		}

		private static Dictionary<string, int> CreateEmptySensorsData()
		{
			return new Dictionary<string, int>
			{
				{ SensorKeys.BatteryVoltage, 0 },
				{ SensorKeys.Charging, 0 },
				{ SensorKeys.BoardTemperature, 0 },
				{ SensorKeys.Resistance, 0 },
				{ SensorKeys.RealResistance, 0 },
				{ SensorKeys.Power, 0 },
				{ SensorKeys.Temperature, 0 },
				{ SensorKeys.Celcius, 0 },
				{ SensorKeys.OutputVoltage, 0 },
				{ SensorKeys.OutputCurrent, 0 }
			};
		}
	}
}
