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

			#if DEBUG
			if (!m_registeredDictionary.ContainsKey(key)) m_registeredDictionary[key] = defaultValue;
			#endif

			return m_localizationDictionary.ContainsKey(key) ? m_localizationDictionary[key] : defaultValue;
		}

		#if DEBUG
		public void RegisterLocalizationKeyValue(IDictionary<Control, string> localizableControls)
		{
			foreach (var kvp in localizableControls)
			{
				if (!m_registeredDictionary.ContainsKey(kvp.Value)) m_registeredDictionary[kvp.Value] = kvp.Key.Text;
			}
		}

		public string GetLanguagePack()
		{
			try
			{
				var sb = new StringBuilder();
				var rootNode = new Node();
				foreach (var item in m_registeredDictionary.Keys)
				{
					RecursiveCreateTree(item.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries), rootNode);
				}

				foreach (var key in RecursiveNodeIterator(rootNode))
				{
					sb.AppendLine(key + "=" + m_registeredDictionary[key].Replace("\r", "\\r").Replace("\n", "\\n"));
				}
				return sb.ToString();
			}
			catch (Exception ex)
			{
				ex = ex;
				throw;
			}
		}

		private void RecursiveCreateTree(IList<string> parts, Node node)
		{
			if (parts.Count == 1)
			{
				node.Items.Add(parts[0]);
			}
			else
			{
				Node parent;
				if (node.Nodes.ContainsKey(parts[0]))
				{
					parent = node.Nodes[parts[0]];
				}
				else
				{
					parent = new Node();
					node.Nodes[parts[0]] = parent;
				}
				RecursiveCreateTree(parts.Skip(1).ToArray(), parent);
			}
		}

		private IEnumerable<string> RecursiveNodeIterator(Node node, string path = null)
		{
			foreach (var item in node.Items.OrderBy(x => x))
			{
				yield return !string.IsNullOrEmpty(path) ? path + "." + item : item;
			}

			foreach (var kvp in node.Nodes.OrderBy(n => n.Key))
			{
				foreach (var iterator in RecursiveNodeIterator(kvp.Value, !string.IsNullOrEmpty(path) ? path + "." + kvp.Key : kvp.Key))
				{
					yield return iterator;
				}
			}
		}

		private class Node
		{
			public Node()
			{
				Nodes = new Dictionary<string, Node>();
				Items = new List<string>();
			}

			internal Dictionary<string, Node> Nodes { get; private set; }

			internal List<string> Items { get; private set; }
		}
		#endif
	}
}
