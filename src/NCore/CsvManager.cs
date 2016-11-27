using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using JetBrains.Annotations;

namespace NCore
{
	public static class CsvManager
	{
		[CanBeNull]
		public static CsvFile Read([NotNull] string filePath, bool withHeader = true)
		{
			if (string.IsNullOrEmpty(filePath)) throw new ArgumentNullException("filePath");

			var fileLines = File.ReadAllLines(filePath);
			if (fileLines.Length == 0) return null;

			var headers = new string[0];
			var csvLines = new List<string[]>();
			if (withHeader)
			{
				headers = LineSplitter(fileLines[0]);
			}

			for (var i = withHeader ? 1 : 0; i < fileLines.Length; i++)
			{
				csvLines.Add(LineSplitter(fileLines[i]));
			}

			return new CsvFile(headers, csvLines);
		}

		public static void Write([NotNull] CsvFile csv, [NotNull] string filePath)
		{
			if (csv == null) throw new ArgumentNullException("csv");
			if (string.IsNullOrEmpty(filePath)) throw new ArgumentNullException("filePath");

			using (var fs = File.Open(filePath, FileMode.Create))
			{
				using (var sw = new StreamWriter(fs))
				{
					if (csv.Headers.Length != 0)
					{
						sw.WriteLine(CreateCsvLine(csv.Headers));
					}

					foreach (var line in csv.Lines)
					{
						sw.WriteLine(CreateCsvLine(line));
					}
				}
			}
		}

		private static string[] LineSplitter(string line)
		{
			return line.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
			           .Select(x => x.Replace("\"", string.Empty))
			           .ToArray();
		}

		private static string CreateCsvLine(IEnumerable<string> data)
		{
			return string.Join(",", data.Select(x => x.Contains(" ") ? "\"" + x + "\"" : x));
		}
	}

	public class CsvFile
	{
		public CsvFile([NotNull] string[] headers, [NotNull] List<string[]> lines)
		{
			if (headers == null) throw new ArgumentNullException("headers");
			if (lines == null) throw new ArgumentNullException("lines");

			Headers = headers;
			Lines = lines;
		}

		public string[] Headers { get; private set; }

		public List<string[]> Lines { get; private set; }
	}
}
