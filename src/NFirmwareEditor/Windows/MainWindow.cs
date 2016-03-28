using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using JetBrains.Annotations;
using NFirmware;
using NFirmwareEditor.Core;
using NFirmwareEditor.Managers;
using NFirmwareEditor.Models;
using NFirmwareEditor.Properties;
using NFirmwareEditor.Windows.Tabs;

namespace NFirmwareEditor.Windows
{
	public partial class MainWindow : Form
	{
		private readonly IEnumerable<IEditorTabPage> m_tabPages = new List<IEditorTabPage>
		{
			new ImageEditorTabPage { Dock = DockStyle.Fill },
			new StringEditorTabPage { Dock = DockStyle.Fill },
			new PatchesTabPage { Dock = DockStyle.Fill }
		};

		private readonly ConfigurationManager m_configurationManager = new ConfigurationManager();
		private readonly FirmwareDefinitionManager m_firmwareDefinitionManager = new FirmwareDefinitionManager();
		private readonly FirmwareLoader m_loader = new FirmwareLoader(new FirmwareEncoder());

		private Configuration m_configuration;
		private IEnumerable<FirmwareDefinition> m_definitions;
		private Firmware m_firmware;

		public MainWindow()
		{
			Icon = Paths.ApplicationIcon;

			InitializeComponent();
			LoadSettings();
			InitializeControls();
		}

		private void InitializeControls()
		{
			foreach (var tabPage in m_tabPages)
			{
				tabPage.Initialize(m_configuration);
				MainTabControl.TabPages.Add(new TabPage(tabPage.Title) { Controls = { (Control)tabPage } });
			}

			MainTabControl.Selected += (s, e) =>
			{
				if (e.TabPage.Controls.Count == 0) return;

				var editorTabPage = e.TabPage.Controls[0] as IEditorTabPage;
				if (editorTabPage == null) return;

				editorTabPage.OnActivate();
			};
		}

		private void ResetWorkspace()
		{
			m_tabPages.ForEach(x => x.OnWorkspaceReset());

			StatusLabel.Text = null;
			MainTabControl.SelectedIndex = 0;

			SaveEncryptedMenuItem.Enabled = false;
			SaveDecryptedMenuItem.Enabled = false;
		}

		private void LoadSettings()
		{
			m_configuration = m_configurationManager.Load();

			Size = new Size(m_configuration.MainWindowWidth, m_configuration.MainWindowHeight);
			WindowState = m_configuration.MainWindowMaximaged ? FormWindowState.Maximized : FormWindowState.Normal;

			m_definitions = m_firmwareDefinitionManager.Load();
			foreach (var definition in m_definitions)
			{
				var firmwareDefinition = definition;
				OpenEncryptedManualMenuItem.DropDownItems.Add(definition.Name, OpenEncryptedManualMenuItem.Image, (s, e) =>
				{
					OpenDialogAndReadFirmwareOnOk(firmwareDefinition.Name, fileName => m_loader.LoadEncrypted(fileName, firmwareDefinition));
				});
				OpenDecryptedManualMenuItem.DropDownItems.Add(definition.Name, OpenDecryptedManualMenuItem.Image, (s, e) =>
				{
					OpenDialogAndReadFirmwareOnOk(firmwareDefinition.Name, fileName => m_loader.LoadDecrypted(fileName, firmwareDefinition));
				});
			}
		}

		private void OpenDialogAndReadFirmwareOnOk(string firmwareName, Func<string, Firmware> readFirmwareDelegate)
		{
			string firmwareFile;
			using (var op = new OpenFileDialog { Title = string.Format("Select \"{0}\" firmware file ...", firmwareName), Filter = Consts.FirmwareFilter })
			{
				if (op.ShowDialog() != DialogResult.OK) return;
				firmwareFile = op.FileName;
			}

			ResetWorkspace();
			try
			{
				m_firmware = readFirmwareDelegate(firmwareFile);
				if (m_firmware == null)
				{
					throw new InvalidOperationException("No one definition is not appropriate for the selected firmware file.");
				}

				ImageCacheManager.RebuildImageCache(m_firmware);
				m_tabPages.ForEach(x => x.OnFirmwareLoaded(m_firmware));

				SaveEncryptedMenuItem.Enabled = true;
				SaveDecryptedMenuItem.Enabled = true;

				Text = string.Format("{0} - {1}", Consts.ApplicationTitle, firmwareFile);
				StatusLabel.Text = string.Format("Firmware loaded successfully. Used definition: {0}.", m_firmware.Definition.Name);
			}
			catch (Exception ex)
			{
				InfoBox.Show("Unable to load firmware.\n{0}", ex.Message);
			}
		}

		private void OpenDialogAndSaveFirmwareOnOk(Action<string, Firmware> writeFirmwareDelegate)
		{
			if (m_firmware == null) return;

			string firmwareFile;
			using (var sf = new SaveFileDialog { Filter = Consts.FirmwareFilter })
			{
				if (sf.ShowDialog() != DialogResult.OK) return;
				firmwareFile = sf.FileName;
			}

			try
			{
				writeFirmwareDelegate(firmwareFile, m_firmware);
				StatusLabel.Text = @"Firmware successfully saved to the file: " + firmwareFile;
			}
			catch (Exception ex)
			{
				InfoBox.Show("Unable to save firmware.\n{0}", ex.Message);
			}
		}

		private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
		{
			m_configurationManager.Save(m_configuration);
		}

		private void MainWindow_SizeChanged(object sender, EventArgs e)
		{
			if (WindowState == FormWindowState.Maximized)
			{
				m_configuration.MainWindowMaximaged = true;
			}
			else if (WindowState == FormWindowState.Normal)
			{
				m_configuration.MainWindowMaximaged = false;
				m_configuration.MainWindowWidth = Width;
				m_configuration.MainWindowHeight = Height;
			}
		}

		private void OpenEncryptedMenuItem_Click(object sender, EventArgs e)
		{
			OpenDialogAndReadFirmwareOnOk(Consts.Encrypted, fileName => m_loader.TryLoadEncrypted(fileName, m_definitions));
		}

		private void OpenDecryptedMenuItem_Click(object sender, EventArgs e)
		{
			OpenDialogAndReadFirmwareOnOk(Consts.Decrypted, fileName => m_loader.TryLoadDecrypted(fileName, m_definitions));
		}

		private void SaveEncryptedMenuItem_Click(object sender, EventArgs e)
		{
			OpenDialogAndSaveFirmwareOnOk((filePath, firmware) => m_loader.SaveEncrypted(filePath, firmware));
		}

		private void SaveDecryptedMenuItem_Click(object sender, EventArgs e)
		{
			OpenDialogAndSaveFirmwareOnOk((filePath, firmware) => m_loader.SaveDecrypted(filePath, firmware));
		}

		private void ExitMenuItem_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void EncryptDecryptToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (var decryptionWindow = new DecryptionWindow())
			{
				decryptionWindow.ShowDialog();
			}
		}

		private void PatchCreatorMenuItem_Click(object sender, EventArgs e)
		{
			using (var patchCreatorWindow = new PatchCreatorWindow(m_definitions.Select(x => x.Name)))
			{
				patchCreatorWindow.ShowDialog();
			}
		}

		private void AboutMenuItem_Click(object sender, EventArgs e)
		{
			InfoBox.Show(Resources.AboutMessage, Consts.ApplicationVersion);
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (keyData.HasFlag(Keys.Control))
			{
				if (keyData.HasFlag(Keys.Shift) && keyData.HasFlag(Keys.O))
				{
					OpenDecryptedMenuItem.PerformClick();
					return true;
				}
				if (keyData.HasFlag(Keys.O))
				{
					OpenEncryptedMenuItem.PerformClick();
					return true;
				}

				if (keyData.HasFlag(Keys.Shift) && keyData.HasFlag(Keys.S))
				{
					SaveDecryptedMenuItem.PerformClick();
					return true;
				}
				if (keyData.HasFlag(Keys.S))
				{
					SaveEncryptedMenuItem.PerformClick();
					return true;
				}
			}

			return m_tabPages.Any(tabPage => tabPage.OnHotkey(keyData)) || base.ProcessCmdKey(ref msg, keyData);
		}
	}
}
