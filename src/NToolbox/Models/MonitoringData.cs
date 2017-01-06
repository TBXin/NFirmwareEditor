namespace NToolbox.Models
{
	#pragma warning disable 0649
	internal class MonitoringData
	{
		// X * 100 (seconds)
		public uint Timestamp;

		public bool IsFiring;
		public bool IsCharging;
		public bool IsCelcius;

		// Offsetted by 275, 420 - 275 = value
		public byte Battery1Voltage;
		// Offsetted by 275, 420 - 275 = value
		public byte Battery2Voltage;
		// Offsetted by 275, 420 - 275 = value
		public byte Battery3Voltage;
		// Offsetted by 275, 420 - 275 = value
		public byte Battery4Voltage;

		// X * 10
		public ushort PowerSet;
		public ushort TemperatureSet;
		public ushort Temperature;

		// X * 100
		public ushort OutputVoltage;
		// X * 100
		public ushort OutputCurrent;

		// X * 1000
		public ushort Resistance;
		// X * 1000
		public ushort RealResistance;

		public byte BoardTemperature;
	}
	#pragma warning restore 0649
}
