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
		private const int MinimizedWindowLeftTop = -32000;

		private readonly ConfigurationManager m_configurationManager = new ConfigurationManager();
		private readonly PatchManager m_patchManager = new PatchManager();
		private readonly ResourcePackManager m_resourcePackManager = new ResourcePackManager();
		private readonly FirmwareDefinitionManager m_firmwareDefinitionManager = new FirmwareDefinitionManager();
		private readonly FirmwareLoader m_loader = new FirmwareLoader(new FirmwareEncoder());
		private readonly BackupManager m_backupManager = new BackupManager();

		private IList<IEditorTabPage> m_tabPages;
		private IList<FirmwareDefinition> m_definitions = new List<FirmwareDefinition>();
		private ApplicationConfiguration m_configuration;
		private MruList<string> m_mruFirmwares;
		private Firmware m_firmware;
		private string m_firmwareFile;

		public MainWindow()
		{
			InitializeComponent();
			InitializeApplication();

			Icon = Paths.ApplicationIcon;
			Text = Consts.ApplicationTitle;
			LoadedFirmwareLabel.Text = null;
			StatusLabel.Text = null;
		}

		public void ReloadFirmware(IEditorTabPage initiator)
		{
			if (m_firmware == null) return;

			m_tabPages.ForEach(x => x.OnWorkspaceReset());
			m_firmware.ReloadResources(m_loader);
			ImageCacheManager.RebuildImageCache(m_firmware);
			m_tabPages.ForEach(x => x.OnFirmwareLoaded(m_firmware));
		}

		private void InitializeApplication()
		{
			m_tabPages = new List<IEditorTabPage>
			{
				new ImageEditorTabPage(m_definitions, m_resourcePackManager) { Dock = DockStyle.Fill },
				new StringEditorTabPage { Dock = DockStyle.Fill },
				new PatchesTabPage(m_patchManager) { Dock = DockStyle.Fill },
				new ResourcePacksTabPage(m_resourcePackManager) { Dock = DockStyle.Fill }
			};

			m_definitions = m_firmwareDefinitionManager.Load();
			m_configuration = m_configurationManager.Load();
			m_mruFirmwares = new MruList<string>(m_configuration.MostRecentlyUsed);

			InitializeApplicationWindow();
			InitializeOpenWithSpecifiedDefinitionMenu();
			InitializeMruMenu();
			InitializeTabPages();
		}

		private void InitializeApplicationWindow()
		{
			if (m_configuration.MainWindowTop != MinimizedWindowLeftTop && m_configuration.MainWindowLeft != MinimizedWindowLeftTop)
			{
				StartPosition = FormStartPosition.Manual;
				Location = new Point(m_configuration.MainWindowLeft, m_configuration.MainWindowTop);
			}
			Size = new Size(m_configuration.MainWindowWidth, m_configuration.MainWindowHeight);
			WindowState = m_configuration.MainWindowMaximaged ? FormWindowState.Maximized : FormWindowState.Normal;
		}

		private void InitializeTabPages()
		{
			foreach (var tabPage in m_tabPages)
			{
				MainTabControl.TabPages.Add(new TabPage(tabPage.Title) { Controls = { (Control)tabPage } });
			}
			m_tabPages.ForEach(x => x.Initialize(this, m_configuration));


			MainTabControl.Selected += (s, e) =>
			{
				if (e.TabPage.Controls.Count == 0) return;

				var editorTabPage = e.TabPage.Controls[0] as IEditorTabPage;
				if (editorTabPage == null) return;

				editorTabPage.OnActivate();
			};
		}

		private void InitializeOpenWithSpecifiedDefinitionMenu()
		{
			OpenUsingSpecifiedDefinitionMenuItem.DropDownItems.Clear();
			foreach (var definition in m_definitions)
			{
				var firmwareDefinition = definition;
				OpenUsingSpecifiedDefinitionMenuItem.DropDownItems.Add(definition.Name, OpenUsingSpecifiedDefinitionMenuItem.Image, (s, e) =>
				{
					OpenDialogAndReadFirmwareOnOk(firmwareDefinition.Name, fileName => m_loader.TryLoadUsingDefinition(fileName, firmwareDefinition));
				});
			}
		}

		private void InitializeMruMenu()
		{
			RecentFirmwaresMenuItem.DropDownItems.Clear();
			var counter = 1;
			foreach (var item in m_mruFirmwares.Items)
			{
				var mruItem = item;
				RecentFirmwaresMenuItem.DropDownItems.Add(counter++ + ". " + mruItem, OpenMenuItem.Image, (s, e) =>
				{
					OpenFirmware(mruItem, fileName => m_loader.TryLoad(fileName, m_definitions));
				});
			}
			RecentFirmwaresMenuItem.Enabled = RecentFirmwaresMenuItem.DropDownItems.Count > 0;
		}

		private void ResetWorkspace()
		{
			m_tabPages.ForEach(x => x.OnWorkspaceReset());

			StatusLabel.Text = null;
			LoadedFirmwareLabel.Text = null;

			MainTabControl.SelectedIndex = 0;

			SaveMenuItem.Enabled = false;
			SaveEncryptedMenuItem.Enabled = false;
			SaveDecryptedMenuItem.Enabled = false;
		}

		private void OpenFirmware(string firmwareFile, Func<string, Firmware> readFirmwareDelegate)
		{
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
				LoadedFirmwareLabel.Text = string.Format("{0} [{1}]", m_firmware.Definition.Name, firmware.IsEncrypted ? "Encrypted" : "Decrypted");
				StatusLabel.Text = @"Firmware file has been successfully loaded.";

				m_mruFirmwares.Add(firmwareFile);	
			}
			catch (Exception ex)
			{
				m_mruFirmwares.Remove(firmwareFile);
				InfoBox.Show("Unable to load firmware.\n{0}", ex.Message);
			}
			finally
			{
				InitializeMruMenu();
			}
		}

		private void OpenDialogAndReadFirmwareOnOk(string firmwareName, Func<string, Firmware> readFirmwareDelegate)
		{
			using (var op = new OpenFileDialog { Title = string.Format("Select \"{0}\" firmware file ...", firmwareName), Filter = Consts.FirmwareFilter })
			{
				if (op.ShowDialog() != DialogResult.OK) return;
				OpenFirmware(op.FileName, readFirmwareDelegate);
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
				StatusLabel.Text = @"Firmware file saved to " + firmwareFile;
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
			m_configuration.MostRecentlyUsed = m_mruFirmwares.Items;
			m_configurationManager.Save(m_configuration);
		}

		private void MainWindow_Move(object sender, EventArgs e)
		{
			if (WindowState != FormWindowState.Normal) return;

			m_configuration.MainWindowLeft = Location.X;
			m_configuration.MainWindowTop = Location.Y;
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
				StatusLabel.Text = @"Firmware file saved.";
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

		private void ImageConverterMenuItem_Click(object sender, EventArgs e)
		{
			using (var imageConvertorWindow = new ImageConverterWindow())
			{
				imageConvertorWindow.ShowDialog();
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
				InitializeTabPages();
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
