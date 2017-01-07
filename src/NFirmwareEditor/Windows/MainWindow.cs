using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using NCore;
using NFirmware;
using NFirmwareEditor.Core;
using NFirmwareEditor.Managers;
using NFirmwareEditor.Models;
using NFirmwareEditor.Storages;
using NFirmwareEditor.Windows.Tabs;

namespace NFirmwareEditor.Windows
{
	internal sealed partial class MainWindow :  Form, IEditorTabPageHost
	{
		private const int MinimizedWindowLeftTop = -32000;

		private readonly ConfigurationStorage m_configurationStorage = new ConfigurationStorage();
		private readonly FirmwareDefinitionsStorage m_firmwareDefinitionStorage = new FirmwareDefinitionsStorage();
		private readonly ResourcePacksStorage m_resourcePackStorage = new ResourcePacksStorage();
		private readonly PatchManager m_patchManager = new PatchManager();
		private readonly BackupManager m_backupManager = new BackupManager();

		private readonly UpdatesManager m_updatesManager = new UpdatesManager(TimeSpan.FromHours(1));
		private readonly FirmwareLoader m_loader = new FirmwareLoader();
		
		private IList<IEditorTabPage> m_tabPages;
		private IEnumerable<FirmwareDefinition> m_definitions = new List<FirmwareDefinition>();
		private ApplicationConfiguration m_configuration;
		private MruList<string> m_mruFirmwares;
		private Firmware m_firmware;
		private string m_firmwareFile;

		public MainWindow()
		{
			InitializeComponent();
			InitializeApplication();

			Icon = NFEPaths.ApplicationIcon;
			Text = Consts.ApplicationTitle;
			LoadedFirmwareLabel.Text = null;
			StatusLabel.Text = null;
		}

		public MainWindow(string[] args) : this()
		{
			m_firmwareFile = NFEPaths.ValidateInputArgs(args);
			if (!string.IsNullOrEmpty(m_firmwareFile))
			{
				Shown += (s, e) =>
				{
					OpenFirmware(m_firmwareFile, fileName => m_loader.TryLoad(fileName, m_definitions));
				};
			}
		}

		public void ReloadFirmware(IEditorTabPage initiator)
		{
			if (m_firmware == null) return;

			m_tabPages.ForEach(x => x.OnWorkspaceReset());
			m_firmware.ReloadResources(m_loader);
			ImageCacheManager.RebuildCache(m_firmware);
			m_tabPages.ForEach(x => x.OnFirmwareLoaded(m_firmware));
		}

		private void InitializeApplication()
		{
			m_tabPages = new List<IEditorTabPage>
			{
				new ImageEditorTabPage(m_definitions, m_resourcePackStorage) { Dock = DockStyle.Fill },
				new StringEditorTabPage { Dock = DockStyle.Fill },
				new PatchesTabPage(m_patchManager) { Dock = DockStyle.Fill },
				new ResourcePacksTabPage(m_resourcePackStorage) { Dock = DockStyle.Fill }
			};

			InitializeStorages();
			InitializeApplicationWindow();
			InitializeOpenWithSpecifiedDefinitionMenu();
			InitializeMruMenu();
			InitializeTabPages();
			InitializeUpdatesChecking();
		}

		private void InitializeStorages()
		{
			m_configurationStorage.Initialize();
			m_firmwareDefinitionStorage.Initialize();
			m_resourcePackStorage.Initialize();

			m_definitions = m_firmwareDefinitionStorage.LoadAll().ToList();
			m_configuration = m_configurationStorage.TryLoad(NFEPaths.SettingsFile) ?? new ApplicationConfiguration();
			m_mruFirmwares = new MruList<string>(m_configuration.MostRecentlyUsed);
			m_patchManager.InitializeStorage(m_definitions);
			m_updatesManager.SetupInitialData(Consts.ApplicationVersion, m_definitions);
		}

		private void InitializeApplicationWindow()
		{
			if (m_configuration.MainWindowTop != 0 &&
			    m_configuration.MainWindowLeft != 0 &&
			    m_configuration.MainWindowTop != MinimizedWindowLeftTop &&
			    m_configuration.MainWindowLeft != MinimizedWindowLeftTop)
			{
				StartPosition = FormStartPosition.Manual;
				Location = new Point(m_configuration.MainWindowLeft, m_configuration.MainWindowTop);
			}
			Size = new Size(m_configuration.MainWindowWidth, m_configuration.MainWindowHeight);
			WindowState = m_configuration.MainWindowMaximaged ? FormWindowState.Maximized : FormWindowState.Normal;
		}

		private void InitializeOpenWithSpecifiedDefinitionMenu()
		{
			var hierarchy = FirmwareDefinitionManager.CreateHierarchy(m_definitions);

			OpenUsingSpecifiedDefinitionMenuItem.DropDownItems.Clear();
			foreach (var deviceKvp in hierarchy)
			{
				var deviceMenu = new ToolStripMenuItem(deviceKvp.Key, OpenUsingSpecifiedDefinitionMenuItem.Image);
				foreach (var definitionKvp in deviceKvp.Value)
				{
					var kvp = definitionKvp;
					var definition = kvp.Value;
					deviceMenu.DropDownItems.Add(definitionKvp.Key, deviceMenu.Image, (s, e) =>
					{
						OpenDialogAndReadFirmwareOnOk(definition.Name, fileName => m_loader.TryLoadUsingDefinition(fileName, definition));
					});
				}
				OpenUsingSpecifiedDefinitionMenuItem.DropDownItems.Add(deviceMenu);
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

		private void InitializeTabPages()
		{
			foreach (var tabPage in m_tabPages)
			{
				MainTabControl.TabPages.Add(new TabPage(tabPage.Title) { Controls = { (Control)tabPage } });
				tabPage.Initialize(this, m_configuration);
			}

			MainTabControl.Selected += (s, e) =>
			{
				if (e.TabPage.Controls.Count == 0) return;

				var editorTabPage = e.TabPage.Controls[0] as IEditorTabPage;
				if (editorTabPage == null) return;

				editorTabPage.OnActivate();
			};
		}

		private void InitializeUpdatesChecking()
		{
			if (m_configuration.CheckForApplicationUpdates || m_configuration.CheckForDefinitionsUpdates)
			{
				m_updatesManager.UpdatesAvailable += ShowUpdatesWindow;
				m_updatesManager.StartChecking();
			}
			else
			{
				m_updatesManager.StopChecking();
				m_updatesManager.UpdatesAvailable -= ShowUpdatesWindow;
			}
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

		private void UpdateOpenedFirmwareInfo()
		{
			Text = string.Format("{0} - {1}", Consts.ApplicationTitle, m_firmwareFile);
			LoadedFirmwareLabel.Text = string.Format("{0} [{1}]", m_firmware.Definition.Name, m_firmware.EncryptionType != EncryptionType.None ? "Encrypted" : "Decrypted");
			SaveDefinitionMenuItem.Visible = !m_definitions.Any(x => x.Name.Equals(m_firmware.Definition.Name));
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

				ImageCacheManager.RebuildCache(m_firmware);
				m_tabPages.ForEach(x =>
				{
					x.OnFirmwareLoaded(m_firmware);
					x.IsDirty = false;
				});

				SaveMenuItem.Enabled = true;
				SaveEncryptedMenuItem.Enabled = true;
				SaveDecryptedMenuItem.Enabled = SaveDecryptedMenuItem.Visible = m_firmware.EncryptionType != EncryptionType.ArcticFox;
				StatusLabel.Text = @"Firmware file has been successfully loaded.";

				m_mruFirmwares.Add(firmwareFile);
				UpdateOpenedFirmwareInfo();
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

				m_firmwareFile = firmwareFile;
				UpdateOpenedFirmwareInfo();
			}
			catch (Exception ex)
			{
				InfoBox.Show("Unable to save firmware.\n{0}", ex.Message);
			}
		}

		private void ShowUpdatesWindow(UpdatesInfo updatesInfo)
		{
			this.UpdateUI(() =>
			{
				if (updatesInfo.Release != null)
				{
					using (var updatesWindow = new UpdatesAvailableWindow(updatesInfo.Release))
					{
						updatesWindow.ShowDialog();
						return;
					}
				}
				if (updatesInfo.Definitions != null && updatesInfo.Definitions.Any())
				{
					using (var updatesWindow = new DefinitionUpdatesAvailableWindow(updatesInfo.Definitions))
					{
						if (updatesWindow.ShowDialog() == DialogResult.Cancel) return;

						m_definitions = m_firmwareDefinitionStorage.LoadAll().ToList();
						m_updatesManager.SetupInitialData(Consts.ApplicationVersion, m_definitions);
						InitializeOpenWithSpecifiedDefinitionMenu();
					}
				}
			});
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
			m_configurationStorage.Save(NFEPaths.SettingsFile, m_configuration);
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
				if (m_firmware.EncryptionType != EncryptionType.None)
				{
					m_loader.SaveEncrypted(m_firmwareFile, m_firmware);
				}
				else
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
			OpenDialogAndSaveFirmwareOnOk((filePath, firmware) =>
			{
				if (firmware.EncryptionType == EncryptionType.None)
				{
					firmware.EncryptionType = EncryptionType.Joyetech;
				}
				m_loader.SaveEncrypted(filePath, firmware);
			});
		}

		private void SaveDecryptedMenuItem_Click(object sender, EventArgs e)
		{
			OpenDialogAndSaveFirmwareOnOk((filePath, firmware) =>
			{
				firmware.EncryptionType = EncryptionType.None;
				m_loader.SaveDecrypted(filePath, firmware);
			});
		}

		private void SaveDefinitionMenuItem_Click(object sender, EventArgs e)
		{
			using (var sf = new SaveFileDialog { Filter = Consts.DefinitionFilter })
			{
				if (sf.ShowDialog() != DialogResult.OK) return;

				try
				{
					m_firmwareDefinitionStorage.Save(m_firmware.Definition, sf.FileName);
				}
				catch (Exception ex)
				{
					InfoBox.Show("An error occured during saving definition file.\n" + ex);
				}
			}
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
			using (var sync = new CrossApplicationSynchronizer(CrossApplicationIndentifiers.FirmwareUpdater))
			{
				if (!sync.IsLockObtained)
				{
					InfoBox.Show("\"NFE Toolbox - Firmware Updater\" is already running.\n\nTo continue you need to close it first.");
					return;
				}
				using (var firmwareUpdaterWindow = new FirmwareUpdaterWindow(m_firmware, m_loader))
				{
					firmwareUpdaterWindow.ShowDialog();
				}
			}
		}

		private void OptionsMenuItem_Click(object sender, EventArgs e)
		{
			var checkForUpdates = m_configuration.CheckForApplicationUpdates;
			using (var optionsWindow = new OptionsWindow(m_configuration))
			{
				if (optionsWindow.ShowDialog() != DialogResult.OK) return;

				m_configurationStorage.Save(NFEPaths.SettingsFile, m_configuration);
				m_tabPages.ForEach(x => x.Initialize(this, m_configuration));

				if (checkForUpdates != m_configuration.CheckForApplicationUpdates)
				{
					InitializeUpdatesChecking();
				}
			}
		}

		private void AboutMenuItem_Click(object sender, EventArgs e)
		{
			using (var aboutWindow = new AboutWindow())
			{
				aboutWindow.ShowDialog();
			}
		}

		private void CheckForUpdatesMenuItem_Click(object sender, EventArgs e)
		{
			CheckForUpdatesMenuItem.Enabled = false;
			var checkForUpdatesAction = new Action(() =>
			{
				var releaseInfo = UpdatesManager.CheckForNewRelease(Consts.ApplicationVersion);
				this.UpdateUI(() => CheckForUpdatesMenuItem.Enabled = true);
				if (releaseInfo == null)
				{
					InfoBox.Show("You are using latest version!");
				}
				else
				{
					ShowUpdatesWindow(new UpdatesInfo { Release = releaseInfo });
				}
			});
			checkForUpdatesAction.BeginInvoke(null, null);
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
				if (keyData.HasFlag(Keys.U))
				{
					FirmwareUpdaterMenuItem.PerformClick();
					return true;
				}
			}

			return m_tabPages.Any(tabPage => tabPage.OnHotkey(keyData)) || base.ProcessCmdKey(ref msg, keyData);
		}
	}
}
