using System.Text.RegularExpressions;
using System.Windows.Forms;
using NCore.UI;
using NToolbox.Models;

namespace NToolbox.Windows
{
	public partial class ProfileTabContent : UserControl
	{
		private const int MinimumWatts = 1;
		private const int MaximumWatts = 250;

		private static readonly Regex s_blackList = new Regex("(?![A-Z0-9\\.\\s]).", RegexOptions.Compiled);

		public ProfileTabContent()
		{
			InitializeComponent();
			Initialize();
		}

		private void Initialize()
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
