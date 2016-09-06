using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;
using JetBrains.Annotations;
using NFirmwareEditor.Models;
using NLog;

namespace NFirmwareEditor.Managers
{
	internal static class GitHubApi
	{
		private const string LatestReleaseUrl = "https://api.github.com/repos/TBXin/NFirmwareEditor/releases/latest";
		private const string RepositoryRootUrl = "https://api.github.com/repos/tbxin/NFirmwareEditor/contents/src/NFirmwareEditor/";

		private static readonly ILogger s_logger = LogManager.GetCurrentClassLogger();

		public static GitHubRelease GetLatestRelease()
		{
			try
			{
				using (var client = new WebClientWithCustomTimeout(TimeSpan.FromSeconds(10)))
				{
					var responseJson = client.DownloadString(LatestReleaseUrl);
					var serializer = new DataContractJsonSerializer(typeof(GitHubRelease));
					var releaseObject = serializer.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(responseJson))) as GitHubRelease;
					return releaseObject;
				}
			}
			catch (Exception ex)
			{
				s_logger.Warn(ex, "An error occurred during checking for updates.");
				return null;
			}
		}

		[CanBeNull]
		public static IEnumerable<GitHubFileInfo> GetFiles(string relativePath)
		{
			try
			{
				using (var client = new WebClientWithCustomTimeout(TimeSpan.FromSeconds(10)))
				{
					var uri = Uri.EscapeUriString(RepositoryRootUrl + relativePath);
					var responseJson = client.DownloadString(uri);
					if (string.IsNullOrEmpty(responseJson)) return null;
					if (responseJson.Contains("Not Found")) return null;

					var serializer = new DataContractJsonSerializer(typeof(List<GitHubFileInfo>));
					var fileInfos = serializer.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(responseJson))) as List<GitHubFileInfo>;
					return fileInfos;
				}
			}
			catch (WebException ex)
			{
				var httpWebresponse = ex.Response as HttpWebResponse;
				if (httpWebresponse == null)
				{
					s_logger.Warn(ex, "An unexpected response received. Path: '{0}'.", relativePath);
				}
				else if (httpWebresponse.StatusCode == HttpStatusCode.NotFound)
				{
					s_logger.Info(ex, "Requested repository directory '{0}' not found.", relativePath);
				}
				return null;
			}
			catch (Exception ex)
			{
				s_logger.Warn(ex, "An error occurred during retrieving rep files from '{0}'.", relativePath);
				return null;
			}
		}


		[NotNull]
		public static IEnumerable<GitHubFileInfo> GetEntityForUpdate
		(
			[NotNull] IEnumerable<GitHubFileInfo> remoteFiles,
			[NotNull] IEnumerable<IUpdatable> localFiles
		)
		{
			if (remoteFiles == null) throw new ArgumentNullException("remoteFiles");
			if (localFiles == null) throw new ArgumentNullException("localFiles");

			var localMap = localFiles.ToDictionary(x => x.FileName, x => x);
			foreach (var remoteFle in remoteFiles)
			{
				IUpdatable local;
				if (localMap.TryGetValue(remoteFle.Name, out local))
				{
					if (!string.Equals(local.Sha, remoteFle.Sha))
					{
						yield return remoteFle;
					}
				}
				else
				{
					yield return remoteFle;
				}
			}
		}

		public static void DownloadFile(string url, string filePath)
		{
			try
			{
				using (var client = new WebClientWithCustomTimeout(TimeSpan.FromSeconds(10)))
				{
					client.DownloadFile(url, filePath);
				}
			}
			catch (Exception ex)
			{
				s_logger.Warn(ex, "An error occurred during patch file download.");
			}
		}

		public static string GetGitSha(string filePath)
		{
			var fileBytes = File.ReadAllBytes(filePath);
			var fileText = Encoding.UTF8.GetString(fileBytes);

			var unixStyleEndingString = fileText.Replace("\r\n", "\n");
			var dataBytes = Encoding.UTF8.GetBytes(unixStyleEndingString);
			var gitBlob = string.Format("blob {0}\0{1}", dataBytes.Length, unixStyleEndingString);
			return GetSha1(gitBlob);
		}

		private static string GetSha1(string data)
		{
			using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(data)))
			using (var sha = new SHA1Managed())
			{
				var checksum = sha.ComputeHash(ms);
				return BitConverter.ToString(checksum).Replace("-", string.Empty).ToLower();
			}
		}

		[DataContract]
		internal class GitHubFileInfo
		{
			[DataMember(Name = "name")]
			public string Name { get; set; }

			[DataMember(Name = "sha")]
			public string Sha { get; set; }

			[DataMember(Name = "download_url")]
			public string DownloadUrl { get; set; }
		}

		[DataContract]
		internal class GitHubRelease
		{
			[DataMember(Name = "tag_name")]
			public string Tag { get; set; }

			[DataMember(Name = "name")]
			public string Name { get; set; }

			[DataMember(Name = "assets")]
			public Asset[] Assets { get; set; }

			[DataMember(Name = "body")]
			public string Description { get; set; }
		}

		[DataContract]
		internal class Asset
		{
			[DataMember(Name = "browser_download_url")]
			public string DownloadUrl { get; set; }
		}

		private class WebClientWithCustomTimeout : WebClient
		{
			private readonly TimeSpan m_requestTimeout;

			public WebClientWithCustomTimeout(TimeSpan requestTimeout)
			{
				m_requestTimeout = requestTimeout;
				Headers["User-Agent"] = "NFirmwareEditor";
			}

			protected override WebRequest GetWebRequest(Uri address)
			{
				var request = base.GetWebRequest(address);
				if (request == null) throw new InvalidOperationException();

				request.Timeout = (int)m_requestTimeout.TotalMilliseconds;
				return request;
			}
		}
	}
}
