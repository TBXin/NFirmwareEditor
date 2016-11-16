using System;
using NCore;
using NCore.UI;
using NCore.USB;
using NToolbox.Models;

namespace NToolbox.Windows
{
	public partial class ArcticFoxConfigurationWindow : WindowBase
	{
		private const int MinimumSupportedBuildNumber = 161115;

		private readonly HidConnector m_connector = new HidConnector();

		private bool m_isDeviceWasConnectedOnce;
		private bool m_isDeviceConnected;

		public ArcticFoxConfigurationWindow()
		{
			InitializeComponent();
			Initialize();
		}

		private void Initialize()
		{
			m_connector.DeviceConnected += DeviceConnected;
			Load += (s, e) => m_connector.StartUSBConnectionMonitoring();
			Closing += (s, e) => m_connector.StopUSBConnectionMonitoring();
		}

		private ArcticFoxConfiguration ReadConfiguration()
		{
			byte[] data = null;
			try
			{
				data = m_connector.ReadConfiguration();
			}
			catch (TimeoutException)
			{
			}
			return data != null ? BinaryStructure.Read<ArcticFoxConfiguration>(data) : null;
		}

		private void DeviceConnected(bool isConnected)
		{
			m_isDeviceConnected = isConnected;
			if (m_isDeviceWasConnectedOnce) return;

			if (!m_isDeviceConnected)
			{
				UpdateUI(() =>
				{
					UpdateUI(() => WelcomeLabel.Text = string.Format("Waiting for device with\n\nArctic Fox firmware\n\n{0} or newer", MinimumSupportedBuildNumber));
					MainContainer.SelectedPage = WelcomePage;
				});
				return;
			}

			UpdateUI(() => WelcomeLabel.Text = @"Downloading settings...");
			try
			{
				var configuration = ReadConfiguration();
				if (configuration == null)
				{
					DeviceConnected(false);
					return;
				}

				UpdateUI(() =>
				{
					//InitializeWorkspaceFromDataflash(m_dataflash);
					MainContainer.SelectedPage = WorkspacePage;
					m_isDeviceWasConnectedOnce = true;
				}, false);
			}
			catch (Exception ex)
			{
				//s_logger.Warn(ex);
				UpdateUI(() => WelcomeLabel.Text = @"Unable to download device settings. Reconnect your device.");
			}
		}
	}
}
