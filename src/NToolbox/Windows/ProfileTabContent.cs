using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using NCore.UI;
using NToolbox.Models;

namespace NToolbox.Windows
{
	internal partial class ProfileTabContent : UserControl
	{
		private const int MinimumWatts = 1;
		private static readonly Regex s_blackList = new Regex("(?![A-Z0-9\\+\\-\\.\\s]).", RegexOptions.Compiled);

		private readonly int m_maximumWatts;

		internal ProfileTabContent(int maximumWatts)
		{
			m_maximumWatts = maximumWatts;

			InitializeComponent();
			InitializeControls();
		}

		public void Initialize(ArcticFoxConfiguration.Profile profile)
		{
			ProfileNameTextBox.Text = profile.Name;
			PowerUpDown.Value = Math.Max(PowerUpDown.Minimum, Math.Min(profile.Power / 10m, PowerUpDown.Maximum));
			PreheatTypeComboBox.SelectItem(profile.Flags.IsPreheatInPercents);
			PreheatPowerUpDown.Value = profile.Flags.IsPreheatInPercents
				? profile.PreheatPower
				: Math.Max(PreheatPowerUpDown.Minimum, Math.Min(profile.PreheatPower / 10m, PreheatPowerUpDown.Maximum));
			PreheatTimeUpDown.Value = profile.PreheatTime / 100m;
			PreheatDelayUpDown.Value = profile.PreheatDelay / 10m;

			TemperatureTypeComboBox.SelectItem(profile.Flags.IsCelcius);
			TemperatureUpDown.Value = profile.Temperature;
			TemperatureDominantCheckBox.Checked = profile.Flags.IsTemperatureDominant;

			MaterialComboBox.SelectItem(profile.Flags.Material);
			TCRUpDown.Value = profile.TCR;
			ResistanceUpDown.Value = profile.Resistance / 1000m;
			ResistanceLockedCheckBox.Checked = profile.Flags.IsResistanceLocked;
		}

		public void UpdateTFRNames(ArcticFoxConfiguration.TFRTable[] tables)
		{
			var selectedValue = MaterialComboBox.GetSelectedItem<ArcticFoxConfiguration.Material>();

			for (var i = 0; i < tables.Length; i++)
			{
				var index = 5 + i;
				var item = MaterialComboBox.Items[index] as NamedItemContainer<ArcticFoxConfiguration.Material>;
				if (item == null) continue;

				var material = item.Data;
				MaterialComboBox.BeginUpdate();
				MaterialComboBox.Items.RemoveAt(index);
				MaterialComboBox.Items.Insert(index, new NamedItemContainer<ArcticFoxConfiguration.Material>("[TFR] " + tables[i].Name, material));
				MaterialComboBox.EndUpdate();
			}

			MaterialComboBox.SelectItem(selectedValue);
		}

		public void Save(ArcticFoxConfiguration.Profile profile)
		{
			profile.Name = ProfileNameTextBox.Text;
			profile.Power = (ushort)(PowerUpDown.Value * 10);
			profile.Flags.IsPreheatInPercents = PreheatTypeComboBox.GetSelectedItem<bool>();
			profile.PreheatPower = profile.Flags.IsPreheatInPercents ? (ushort)PreheatPowerUpDown.Value : (ushort)(PreheatPowerUpDown.Value * 10);
			profile.PreheatTime = (byte)(PreheatTimeUpDown.Value * 100);
			profile.PreheatDelay = (byte)(PreheatDelayUpDown.Value * 10);

			profile.Flags.IsCelcius = TemperatureTypeComboBox.GetSelectedItem<bool>();
			profile.Temperature = (ushort)TemperatureUpDown.Value;
			profile.Flags.IsTemperatureDominant = TemperatureDominantCheckBox.Checked;

			profile.Flags.Material = MaterialComboBox.GetSelectedItem<ArcticFoxConfiguration.Material>();
			profile.TCR = (ushort)TCRUpDown.Value;
			profile.Resistance = (ushort)(ResistanceUpDown.Value * 1000);
			profile.Flags.IsResistanceLocked = ResistanceLockedCheckBox.Checked;
		}

		private void InitializeControls()
		{
			ProfileNameTextBox.TextChanged += (s, e) =>
			{
				var position = ProfileNameTextBox.SelectionStart;
				var input = ProfileNameTextBox.Text;
				var matches = s_blackList.Matches(input);
				foreach (Match match in matches)
				{
					if (!match.Success) continue;
					input = input.Replace(match.Value, string.Empty);
				}
				ProfileNameTextBox.Text = input;
				ProfileNameTextBox.SelectionStart = position;
			};

			PowerUpDown.Minimum = MinimumWatts;
			PowerUpDown.Maximum = m_maximumWatts;

			PreheatTypeComboBox.Items.Clear();
			PreheatTypeComboBox.Items.AddRange(new object[]
			{
				new NamedItemContainer<bool>("%", true),
				new NamedItemContainer<bool>("W", false)
			});
			PreheatTypeComboBox.SelectedValueChanged += (s, e) =>
			{
				var isPercents = PreheatTypeComboBox.GetSelectedItem<bool>();
				if (isPercents)
				{
					PreheatPowerUpDown.DecimalPlaces = 0;
					PreheatPowerUpDown.Increment = 1;
					PreheatPowerUpDown.Minimum = 100;
					PreheatPowerUpDown.Maximum = 250;
				}
				else
				{
					PreheatPowerUpDown.DecimalPlaces = 1;
					PreheatPowerUpDown.Increment = 0.1m;
					PreheatPowerUpDown.Minimum = MinimumWatts;
					PreheatPowerUpDown.Maximum = m_maximumWatts;
				}
			};

			TemperatureTypeComboBox.Items.Clear();
			TemperatureTypeComboBox.Items.AddRange(new object[]
			{
				new NamedItemContainer<bool>("°F", false),
				new NamedItemContainer<bool>("°C", true)
			});
			TemperatureTypeComboBox.SelectedValueChanged += (s, e) =>
			{
				var isCelcius = TemperatureTypeComboBox.GetSelectedItem<bool>();
				if (isCelcius)
				{
					TemperatureUpDown.Minimum = 100;
					TemperatureUpDown.Maximum = 315;
				}
				else
				{
					TemperatureUpDown.Minimum = 200;
					TemperatureUpDown.Maximum = 600;
				}
			};

			MaterialComboBox.Items.Clear();
			MaterialComboBox.Items.AddRange(new object[]
			{
				new NamedItemContainer<ArcticFoxConfiguration.Material>("VariWatt", ArcticFoxConfiguration.Material.VariWatt),
				new NamedItemContainer<ArcticFoxConfiguration.Material>("Nickel 200", ArcticFoxConfiguration.Material.Nickel),
				new NamedItemContainer<ArcticFoxConfiguration.Material>("Titanium 1", ArcticFoxConfiguration.Material.Titanium),
				new NamedItemContainer<ArcticFoxConfiguration.Material>("SS 316", ArcticFoxConfiguration.Material.StainlessSteel),
				new NamedItemContainer<ArcticFoxConfiguration.Material>("TCR", ArcticFoxConfiguration.Material.TCR),

				new NamedItemContainer<ArcticFoxConfiguration.Material>("TFR1", ArcticFoxConfiguration.Material.TFR1),
				new NamedItemContainer<ArcticFoxConfiguration.Material>("TFR2", ArcticFoxConfiguration.Material.TFR2),
				new NamedItemContainer<ArcticFoxConfiguration.Material>("TFR3", ArcticFoxConfiguration.Material.TFR3),
				new NamedItemContainer<ArcticFoxConfiguration.Material>("TFR4", ArcticFoxConfiguration.Material.TFR4),
				new NamedItemContainer<ArcticFoxConfiguration.Material>("TFR5", ArcticFoxConfiguration.Material.TFR5),
				new NamedItemContainer<ArcticFoxConfiguration.Material>("TFR6", ArcticFoxConfiguration.Material.TFR6),
				new NamedItemContainer<ArcticFoxConfiguration.Material>("TFR7", ArcticFoxConfiguration.Material.TFR7),
				new NamedItemContainer<ArcticFoxConfiguration.Material>("TFR8", ArcticFoxConfiguration.Material.TFR8)
			});
			MaterialComboBox.SelectedValueChanged += (s, e) =>
			{
				if (MaterialComboBox.SelectedItem == null) return;

				var selectedMaterial = MaterialComboBox.GetSelectedItem<ArcticFoxConfiguration.Material>();
				var enableTemperatureEditing = selectedMaterial != ArcticFoxConfiguration.Material.VariWatt;
				{
					ResistanceLockedCheckBox.Visible = enableTemperatureEditing;
					TemperatureLabel.Visible = enableTemperatureEditing;
					TemperatureUpDown.Visible = enableTemperatureEditing;
					TemperatureTypeComboBox.Visible = enableTemperatureEditing;
					TemperatureDominantCheckBox.Visible = enableTemperatureEditing;
				}
				TCRUpDown.Visible = selectedMaterial == ArcticFoxConfiguration.Material.TCR;
			};
		}
	}
}
