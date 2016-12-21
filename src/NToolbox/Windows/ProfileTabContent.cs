using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using JetBrains.Annotations;
using NCore;
using NCore.UI;
using NToolbox.Models;

namespace NToolbox.Windows
{
	internal partial class ProfileTabContent : UserControl
	{
		private readonly ArcticFoxConfigurationWindow m_host;
		private const int MinimumWatts = 1;
		private static readonly Regex s_blackList = new Regex("(?![A-Z0-9\\+\\-\\.\\s]).", RegexOptions.Compiled);

		private ArcticFoxConfiguration m_configuration;
		private ArcticFoxConfiguration.Profile m_profile;

		internal ProfileTabContent([NotNull] ArcticFoxConfigurationWindow host)
		{
			if (host == null) throw new ArgumentNullException("host");
			m_host = host;

			InitializeComponent();
			InitializeControls();
		}

		public void Initialize([NotNull] ArcticFoxConfiguration configuration, int profileIndex)
		{
			if (configuration == null) throw new ArgumentNullException("configuration");

			m_configuration = configuration;
			m_profile = configuration.General.Profiles[profileIndex];

			ProfileNameTextBox.Text = m_profile.Name;
			PowerUpDown.Maximum = configuration.Info.MaxPower / 10m;
			PowerUpDown.SetValue(m_profile.Power / 10m);
			PreheatTypeComboBox.SelectItem(m_profile.PreheatType);
			PowerCurveComboBox.SelectItem(m_profile.SelectedCurve);
			PreheatTimeUpDown.SetValue(m_profile.PreheatTime / 100m);
			PreheatDelayUpDown.SetValue(m_profile.PreheatDelay / 10m);

			TemperatureTypeComboBox.SelectItem(m_profile.Flags.IsCelcius);
			TemperatureUpDown.SetValue(m_profile.Temperature);
			TemperatureDominantCheckBox.Checked = m_profile.Flags.IsTemperatureDominant;

			if (m_profile.Flags.Material == ArcticFoxConfiguration.Material.VariWatt)
			{
				MaterialComboBox.SelectedIndex = 0;
				ModeComboBox.SelectItem(Mode.Power);
			}
			else
			{
				MaterialComboBox.SelectItem(m_profile.Flags.Material);
				ModeComboBox.SelectItem(Mode.TemperatureControl);
			}

			TCRUpDown.SetValue(m_profile.TCR);
			ResistanceUpDown.SetValue(m_profile.Resistance / 1000m);
			ResistanceLockedCheckBox.Checked = m_profile.Flags.IsResistanceLocked;
		}

		public void UpdatePowerCurveNames(ArcticFoxConfiguration.PowerCurve[] curves)
		{
			var selectedValue = PowerCurveComboBox.GetSelectedItem<byte>();
			PowerCurveComboBox.BeginUpdate();
			for (byte i = 0; i < curves.Length; i++)
			{
				// 4 : Default Non-TFR materials, Ni, Ti, SS, TCR
				var item = PowerCurveComboBox.Items[i] as NamedItemContainer<byte>;
				if (item == null) continue;

				PowerCurveComboBox.Items.RemoveAt(i);
				PowerCurveComboBox.Items.Insert(i, new NamedItemContainer<byte>(curves[i].Name, i));
			}
			PowerCurveComboBox.EndUpdate();
			PowerCurveComboBox.SelectItem(selectedValue);
		}

		public void UpdateTFRNames(ArcticFoxConfiguration.TFRTable[] tables)
		{
			var selectedValue = MaterialComboBox.GetSelectedItem<ArcticFoxConfiguration.Material>();
			MaterialComboBox.BeginUpdate();
			for (var i = 0; i < tables.Length; i++)
			{
				// 4 : Default Non-TFR materials, Ni, Ti, SS, TCR
				var index = 4 + i;
				var item = MaterialComboBox.Items[index] as NamedItemContainer<ArcticFoxConfiguration.Material>;
				if (item == null) continue;

				var material = item.Data;
				MaterialComboBox.Items.RemoveAt(index);
				MaterialComboBox.Items.Insert(index, new NamedItemContainer<ArcticFoxConfiguration.Material>("[TFR] " + tables[i].Name, material));
			}
			MaterialComboBox.EndUpdate();
			MaterialComboBox.SelectItem(selectedValue);
		}

		public void Save(ArcticFoxConfiguration.Profile profile)
		{
			profile.Name = ProfileNameTextBox.Text;
			profile.Power = (ushort)(PowerUpDown.Value * 10);
			profile.PreheatType = PreheatTypeComboBox.GetSelectedItem<ArcticFoxConfiguration.PreheatType>();
			if (profile.PreheatType == ArcticFoxConfiguration.PreheatType.Watts)
			{
				profile.PreheatPower = (ushort)(PreheatPowerUpDown.Value * 10);
			}
			else if (profile.PreheatType == ArcticFoxConfiguration.PreheatType.Percents)
			{
				profile.PreheatPower = (ushort)PreheatPowerUpDown.Value;
			}
			else if (profile.PreheatType == ArcticFoxConfiguration.PreheatType.Curve)
			{
				profile.SelectedCurve = (byte)PowerCurveComboBox.SelectedIndex;
			}
			profile.PreheatTime = (byte)(PreheatTimeUpDown.Value * 100);
			profile.PreheatDelay = (byte)(PreheatDelayUpDown.Value * 10);

			profile.Flags.IsCelcius = TemperatureTypeComboBox.GetSelectedItem<bool>();
			profile.Temperature = (ushort)TemperatureUpDown.Value;
			profile.Flags.IsTemperatureDominant = TemperatureDominantCheckBox.Checked;

			var mode = ModeComboBox.GetSelectedItem<Mode>();
			profile.Flags.Material = mode == Mode.TemperatureControl
				? MaterialComboBox.GetSelectedItem<ArcticFoxConfiguration.Material>()
				: ArcticFoxConfiguration.Material.VariWatt;

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
			PowerUpDown.Maximum = 60;

			PreheatTypeComboBox.Items.Clear();
			PreheatTypeComboBox.Items.AddRange(new object[]
			{
				new NamedItemContainer<ArcticFoxConfiguration.PreheatType>("Absolute (W)", ArcticFoxConfiguration.PreheatType.Watts),
				new NamedItemContainer<ArcticFoxConfiguration.PreheatType>("Relative (%)", ArcticFoxConfiguration.PreheatType.Percents),
				new NamedItemContainer<ArcticFoxConfiguration.PreheatType>("Curve", ArcticFoxConfiguration.PreheatType.Curve)
			});
			PreheatTypeComboBox.SelectedValueChanged += (s, e) =>
			{
				var type = PreheatTypeComboBox.GetSelectedItem<ArcticFoxConfiguration.PreheatType>();
				if (type == ArcticFoxConfiguration.PreheatType.Watts)
				{
					PreheatPowerUpDown.DecimalPlaces = 1;
					PreheatPowerUpDown.Increment = 0.1m;
					PreheatPowerUpDown.Minimum = MinimumWatts;
					PreheatPowerUpDown.Maximum = m_configuration.Info.MaxPower / 10m;
					PreheatPowerUpDown.SetValue(m_profile.PreheatPower / 10m);
					PreheatPowerUnitLabel.Text = @"W";
				}
				else if (type == ArcticFoxConfiguration.PreheatType.Percents)
				{
					PreheatPowerUpDown.DecimalPlaces = 0;
					PreheatPowerUpDown.Increment = 1;
					PreheatPowerUpDown.Minimum = 100;
					PreheatPowerUpDown.Maximum = 250;
					PreheatPowerUpDown.SetValue(m_profile.PreheatPower);
					PreheatPowerUnitLabel.Text = @"%";
				}

				if (type == ArcticFoxConfiguration.PreheatType.Curve)
				{
					PreheatPowerLabel.Text = @"Preheat Curve:";
					PowerCurveComboBox.Visible = true;
					PowerCurveEditButton.Visible = true;

					PreheatTimeUpDown.Enabled = false;
					PreheatDelayUpDown.Enabled = false;
					PreheatPowerUnitLabel.Text = string.Empty;
				}
				else
				{
					PreheatPowerLabel.Text = @"Preheat Power:";
					PowerCurveComboBox.Visible = false;
					PowerCurveEditButton.Visible = false;

					PreheatTimeUpDown.Enabled = true;
					PreheatDelayUpDown.Enabled = true;
				}
			};

			PowerCurveComboBox.Items.Clear();
			PowerCurveComboBox.Items.AddRange(new object[]
			{
			    new NamedItemContainer<byte>("Curve 1", 0),
			    new NamedItemContainer<byte>("Curve 2", 1),
			    new NamedItemContainer<byte>("Curve 3", 2),
			    new NamedItemContainer<byte>("Curve 4", 3),
			    new NamedItemContainer<byte>("Curve 5", 4),
			    new NamedItemContainer<byte>("Curve 6", 5),
			    new NamedItemContainer<byte>("Curve 7", 6),
			    new NamedItemContainer<byte>("Curve 8", 7),
			});

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

			ModeComboBox.Items.Clear();
			ModeComboBox.Items.AddRange(new object[]
			{
			    new NamedItemContainer<Mode>("Power", Mode.Power),
			    new NamedItemContainer<Mode>("Temp. Control", Mode.TemperatureControl)
			});
			ModeComboBox.SelectedValueChanged += (s, e) =>
			{
				var isTemperatureSensing = ModeComboBox.GetSelectedItem<Mode>() == Mode.TemperatureControl;

				MaterialComboBox.Visible = isTemperatureSensing;
				MaterialLabel.Visible = isTemperatureSensing;

				ResistanceLabel.Visible = isTemperatureSensing;
				ResistanceUpDown.Visible = isTemperatureSensing;
				ResistanceLockedCheckBox.Visible = isTemperatureSensing;
				OhmLabel.Visible = isTemperatureSensing;

				TemperatureLabel.Visible = isTemperatureSensing;
				TemperatureUpDown.Visible = isTemperatureSensing;
				TemperatureTypeComboBox.Visible = isTemperatureSensing;
				TemperatureDominantCheckBox.Visible = isTemperatureSensing;

				var selectedMaterial = MaterialComboBox.GetSelectedItem<ArcticFoxConfiguration.Material>();
				TCRUpDown.Visible = isTemperatureSensing && selectedMaterial == ArcticFoxConfiguration.Material.TCR;
				TFRCurveEditButton.Visible = isTemperatureSensing &&
				                             (int)selectedMaterial >= (int)ArcticFoxConfiguration.Material.TFR1 &&
				                             (int)selectedMaterial <= (int)ArcticFoxConfiguration.Material.TFR8;
			};

			MaterialComboBox.Items.Clear();
			MaterialComboBox.Items.AddRange(new object[]
			{
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
				TCRUpDown.Visible = selectedMaterial == ArcticFoxConfiguration.Material.TCR;
				TFRCurveEditButton.Visible = (int)selectedMaterial >= (int)ArcticFoxConfiguration.Material.TFR1 &&
				                             (int)selectedMaterial <= (int)ArcticFoxConfiguration.Material.TFR8;
			};

			PowerCurveEditButton.Click += PowerCurveEditButton_Click;
			TFRCurveEditButton.Click += TFRCurveEditButton_Click;
		}

		private void PowerCurveEditButton_Click(object sender, EventArgs e)
		{
			var curveIndex = PowerCurveComboBox.SelectedIndex;
			var curve = m_configuration.Advanced.PowerCurves[curveIndex];

			using (var editor = new PowerCurveProfileWindow(curve))
			{
				if (editor.ShowDialog() != DialogResult.OK) return;
				m_host.UpdatePowerCurveNames();
			}
		}

		private void TFRCurveEditButton_Click(object sender, EventArgs e)
		{
			var curveIndex = (int)MaterialComboBox.GetSelectedItem<ArcticFoxConfiguration.Material>() - 5;
			var tfrTable = m_configuration.Advanced.TFRTables[curveIndex];

			using (var editor = new TFRProfileWindow(tfrTable))
			{
				if (editor.ShowDialog() != DialogResult.OK) return;
				m_host.UpdateTFRCurveNames();
			}
		}

		private enum Mode
		{
			Power,
			TemperatureControl
		}
	}
}
