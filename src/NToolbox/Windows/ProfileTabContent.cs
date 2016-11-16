using System.Collections.Generic;
using System.Windows.Forms;
using NCore.UI;
using NToolbox.Models;

namespace NToolbox.Windows
{
	public partial class ProfileTabContent : UserControl
	{
		private const int MinimumWatts = 1;
		private const int MaximumWatts = 250;

		private static readonly List<char> s_nameCharWhiteList = new List<char>();

		static ProfileTabContent()
		{
			// 0..9
			for (var i = 48; i <= 57; i++)
			{
				s_nameCharWhiteList.Add((char)i);
			}

			// A...Z
			for (var i = 65; i <= 90; i++)
			{
				s_nameCharWhiteList.Add((char)i);
			}

			s_nameCharWhiteList.Add('.');
			s_nameCharWhiteList.Add(' ');
		}

		public ProfileTabContent()
		{
			InitializeComponent();
			Initialize();
		}

		private void Initialize()
		{
			ProfileNameTextBox.TextChanged += (s, e) =>
			{
				var input = ProfileNameTextBox.Text;
				for (var i = 0; i < input.Length; i++)
				{
					if (s_nameCharWhiteList.Contains(input[i])) continue;

					input = input.Remove(i);
				}
				ProfileNameTextBox.Text = input;
			};

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
					PreheatPowerUpDown.Maximum = MaximumWatts;
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
				new NamedItemContainer<ArcticFoxConfiguration.Material>("Nickel", ArcticFoxConfiguration.Material.Nickel),
				new NamedItemContainer<ArcticFoxConfiguration.Material>("Titanium", ArcticFoxConfiguration.Material.Titanium),
				new NamedItemContainer<ArcticFoxConfiguration.Material>("SS316", ArcticFoxConfiguration.Material.StainlessSteel),
				new NamedItemContainer<ArcticFoxConfiguration.Material>("TCR", ArcticFoxConfiguration.Material.TCR)
			});
		}
	}
}
