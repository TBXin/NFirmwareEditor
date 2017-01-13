using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JetBrains.Annotations;

namespace NCore.UI
{
	public class LocalizationManager
	{
		private static readonly LocalizationManager s_instance = new LocalizationManager();

		public static LocalizationManager Instance
		{
			get { return s_instance; }
		}

		private readonly IDictionary<string, string> m_registeredDictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
		private IDictionary<string, string> m_localizationDictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

		public IDictionary<string, string> GetLocalizationDictionary()
		{
			return m_localizationDictionary;
		}

		[NotNull]
		public static List<NamedItemContainer<string>> GetAvailableLanguages()
		{
			var result = new List<NamedItemContainer<string>>();
			try
			{
				var files = Directory.GetFiles(ApplicationService.LanguagePacksDirectory, FileFilters.LanguagePackExtension);
				foreach (var file in files)
				{
					var fileName = Path.GetFileNameWithoutExtension(file);
					string lpName;
					if (string.IsNullOrEmpty(fileName))
					{
						lpName = file;
					}
					else
					{
						var dotIndex = fileName.IndexOf(".", StringComparison.InvariantCulture);
						lpName = dotIndex == -1 ? fileName : fileName.Substring(0, dotIndex);
					}
					result.Add(new NamedItemContainer<string>(lpName, file));
				}
			}
			catch (Exception ex)
			{
				Trace.Warn(ex);
			}
			return result;
		}

		public void InitializeLanguagePack([NotNull] string filePath)
		{
			if (string.IsNullOrEmpty(filePath)) throw new ArgumentNullException("filePath");

			try
			{
				var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
				var content = File.ReadAllLines(filePath);
				foreach (var line in content)
				{
					if (line.StartsWith("//")) continue;

					var separatorIndex = line.IndexOf('=', 0);
					if (separatorIndex == -1) continue;

					var key = line.Substring(0, separatorIndex);
					var value = line.Substring(separatorIndex + 1, line.Length - 1 - separatorIndex)
					                .Replace("\\r", "\r")
					                .Replace("\\n", "\n");

					result[key] = value;
				}

				m_localizationDictionary = result;
			}
			catch (Exception ex)
			{
				ex = ex;
			}
		}

		[NotNull]
		public string GetLocalizedString([NotNull] string key, [NotNull] string defaultValue)
		{
			if (string.IsNullOrEmpty(key)) throw new ArgumentNullException("key");
			if (string.IsNullOrEmpty(defaultValue)) throw new ArgumentNullException("defaultValue");

			return m_localizationDictionary.ContainsKey(key) ? m_localizationDictionary[key] : defaultValue;
		}

		public void RegisterLocalizationKeyValue(IDictionary<Control, string> localizableControls)
		{
			foreach (var kvp in localizableControls)
			{
				m_registeredDictionary[kvp.Value] = kvp.Key.Text;
			}
		}

		private string GetLanguagePack
		{
			get
			{
				var sb = new StringBuilder();
				foreach (var kvp in m_registeredDictionary.OrderBy(x => x.Key))
				{
					sb.AppendLine(kvp.Key + "=" + kvp.Value.Replace("\\r", "\\\r").Replace("\\n", "\\\n"));
				}
				return sb.ToString();
			}
		}
	}
}
