using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using NFirmware;
using NFirmwareEditor.Core;
using NFirmwareEditor.Managers;
using NFirmwareEditor.Models;
using NFirmwareEditor.Windows.Tabs;

namespace NFirmwareEditor.Windows
{
	internal sealed partial class MainWindow :  Form, IEditorTabPageHost
	{
		private readonly IEnumerable<IEditorTabPage> m_tabPages;
		private readonly ConfigurationManager m_configurationManager = new ConfigurationManager();
		private readonly PatchManager m_patchManager = new PatchManager();
		private readonly ResourcePackManager m_resourcePackManager = new ResourcePackManager();
		private readonly FirmwareDefinitionManager m_firmwareDefinitionManager = new FirmwareDefinitionManager();
		private readonly FirmwareLoader m_loader = new FirmwareLoader(new FirmwareEncoder());
		private readonly BackupManager m_backupManager = new BackupManager();

		private Configuration m_configuration;
		private IEnumerable<FirmwareDefinition> m_definitions;
		private Firmware m_firmware;
		private string m_firmwareFile;

		public MainWindow()
		{
			m_tabPages = new List<IEditorTabPage>
			{
				new ImageEditorTabPage(m_resourcePackManager) { Dock = DockStyle.Fill },
				new StringEditorTabPage { Dock = DockStyle.Fill },
				new PatchesTabPage(m_patchManager) { Dock = DockStyle.Fill },
				new ResourcePacksTabPage(m_resourcePackManager) { Dock = DockStyle.Fill }
			};

			InitializeComponent();
			InitializeControls();
			LoadSettings();

			Icon = Paths.ApplicationIcon;
			Text = Consts.ApplicationTitle;
		}

		public void ReloadFirmware(IEditorTabPage initiator)
		{
			if (m_firmware == null) return;

			m_tabPages.ForEach(x => x.OnWorkspaceReset());
			m_firmware.ReloadResources(m_loader);
			ImageCacheManager.RebuildImageCache(m_firmware);
			m_tabPages.ForEach(x => x.OnFirmwareLoaded(m_firmware));
		}

		private void InitializeControls()
		{
			foreach (var tabPage in m_tabPages)
			{
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

			LoadedFirmwareLabel.Text = null;
			MainTabControl.SelectedIndex = 0;

			SaveMenuItem.Enabled = false;
			SaveEncryptedMenuItem.Enabled = false;
			SaveDecryptedMenuItem.Enabled = false;
		}

		private void LoadSettings()
		{
			m_configuration = m_configurationManager.Load();

			Size = new Size(m_configuration.MainWindowWidth, m_configuration.MainWindowHeight);
			WindowState = m_configuration.MainWindowMaximaged ? FormWindowState.Maximized : FormWindowState.Normal;

			LoadTabSettings();
			m_definitions = m_firmwareDefinitionManager.Load();
			foreach (var definition in m_definitions)
			{
				var firmwareDefinition = definition;
				OpenUsingSpecifiedDefinitionMenuItem.DropDownItems.Add(definition.Name, OpenUsingSpecifiedDefinitionMenuItem.Image, (s, e) =>
				{
					OpenDialogAndReadFirmwareOnOk(firmwareDefinition.Name, fileName => m_loader.TryLoadUsingDefinition(fileName, firmwareDefinition));
				});
			}
		}

		private void LoadTabSettings()
		{
			m_tabPages.ForEach(x => x.Initialize(this, m_configuration));
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
				var firmware = readFirmwareDelegate(firmwareFile);
				if (firmware == null)
				{
					throw new InvalidOperationException("No one definition is not appropriate for the selected firmware file.");
				}

				m_firmware = firmware;
				m_firmwareFile = firmwareFile;

				ImageCacheManager.RebuildImageCache(m_firmware);
				m_tabPages.ForEach(x =>
				{
					x.OnFirmwareLoaded(m_firmware);
					x.IsDirty = false;
				});

				SaveMenuItem.Enabled = true;
				SaveEncryptedMenuItem.Enabled = true;
				SaveDecryptedMenuItem.Enabled = true;

				Text = string.Format("{0} - {1}", Consts.ApplicationTitle, firmwareFile);
				LoadedFirmwareLabel.Text = string.Format("Loaded firmware: {0} {1}", firmware.IsEncrypted ? Consts.Encrypted : Consts.Decrypted, m_firmware.Definition.Name);
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
				m_tabPages.ForEach(x => x.IsDirty = false);
			}
			catch (Exception ex)
			{
				InfoBox.Show("Unable to save firmware.\n{0}", ex.Message);
			}
		}

		private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (m_tabPages.Any(x => x.IsDirty))
			{
				if (InfoBox.Show("You have unsaved changes, are you sure that you want to close the application?", MessageBoxButtons.YesNo) == DialogResult.No)
				{
					e.Cancel = true;
					return;
				}
			}
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

		private void OpenMenuItem_Click(object sender, EventArgs e)
		{
			OpenDialogAndReadFirmwareOnOk(Consts.EncryptedOrDecrypted, fileName => m_loader.TryLoad(fileName, m_definitions));
		}


		private void SaveMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				m_backupManager.CreateBackup(m_firmwareFile, m_configuration.BackupCreationMode);
				if (m_firmware.IsEncrypted)
				{
					m_loader.SaveEncrypted(m_firmwareFile, m_firmware);
				}
				else if (m_firmware.IsEncrypted == false)
				{
					m_loader.SaveDecrypted(m_firmwareFile, m_firmware);
				}
				m_tabPages.ForEach(x => x.IsDirty = false);
			}
			catch (Exception ex)
			{
				InfoBox.Show("Unable to save firmware.\n{0}", ex.Message);
			}
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
			using (var patchCreatorWindow = new PatchCreatorWindow(m_loader, m_patchManager, m_definitions))
			{
				patchCreatorWindow.ShowDialog();
			}
		}

		private void FirmwareUpdaterMenuItem_Click(object sender, EventArgs e)
		{
			using (var firmwareUpdaterWindow = new FirmwareUpdaterWindow(m_firmware, m_loader))
			{
				firmwareUpdaterWindow.ShowDialog();
			}
		}

		private void OptionsMenuItem_Click(object sender, EventArgs e)
		{
			using (var optionsWindow = new OptionsWindow(m_configuration))
			{
				if (optionsWindow.ShowDialog() != DialogResult.OK) return;

				m_configurationManager.Save(m_configuration);
				LoadTabSettings();
			}
		}

		private void AboutMenuItem_Click(object sender, EventArgs e)
		{
			using (var aboutWindow = new AboutWindow())
			{
				aboutWindow.ShowDialog();
			}
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (keyData.HasFlag(Keys.Control))
			{
				if (keyData.HasFlag(Keys.O))
				{
					OpenMenuItem.PerformClick();
					return true;
				}
				if (keyData.HasFlag(Keys.Alt) && keyData.HasFlag(Keys.Shift) && keyData.HasFlag(Keys.S))
				{
					SaveDecryptedMenuItem.PerformClick();
					return true;
				}
				if (keyData.HasFlag(Keys.Shift) && keyData.HasFlag(Keys.S))
				{
					SaveEncryptedMenuItem.PerformClick();
					return true;
				}
				if (keyData.HasFlag(Keys.S))
				{
					SaveMenuItem.PerformClick();
					return true;
				}
			}

			return m_tabPages.Any(tabPage => tabPage.OnHotkey(keyData)) || base.ProcessCmdKey(ref msg, keyData);
		}
	}
}
