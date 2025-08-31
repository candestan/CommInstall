using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Linq;

namespace CommInstallBuilder
{
    public class GitHubReleaseParser
    {
        private readonly HttpClient httpClient;
        private readonly string repositoryOwner;
        private readonly string repositoryName;

        public GitHubReleaseParser(string owner = "candestan", string repo = "CommInstall")
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", "CommInstall-Builder-App");
            repositoryOwner = owner;
            repositoryName = repo;
        }

        public async Task<List<GitHubRelease>> GetReleasesAsync(int count = 5)
        {
            try
            {
                string url = $"https://api.github.com/repos/{repositoryOwner}/{repositoryName}/releases";
                string response = await httpClient.GetStringAsync(url);
                
                var releases = JsonSerializer.Deserialize<List<GitHubRelease>>(response);
                return releases?.Take(count).ToList() ?? new List<GitHubRelease>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching GitHub releases: {ex.Message}");
                return new List<GitHubRelease>();
            }
        }

        public async Task<GitHubRelease> GetLatestReleaseAsync()
        {
            try
            {
                string url = $"https://api.github.com/repos/{repositoryOwner}/{repositoryName}/releases/latest";
                string response = await httpClient.GetStringAsync(url);
                
                return JsonSerializer.Deserialize<GitHubRelease>(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching latest GitHub release: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> CheckForUpdatesAsync(string currentVersion)
        {
            try
            {
                var latestRelease = await GetLatestReleaseAsync();
                if (latestRelease == null) return false;

                return CompareVersions(latestRelease.TagName, currentVersion) > 0;
            }
            catch
            {
                return false;
            }
        }

        public async Task<string> GetReleaseNotesAsync(string version = null)
        {
            try
            {
                if (string.IsNullOrEmpty(version))
                {
                    var latestRelease = await GetLatestReleaseAsync();
                    return latestRelease?.Body ?? "No release notes available.";
                }

                var releases = await GetReleasesAsync(50);
                var release = releases.FirstOrDefault(r => r.TagName == version);
                return release?.Body ?? "Release notes not found.";
            }
            catch
            {
                return "Error fetching release notes.";
            }
        }

        public async Task<List<GitHubAsset>> GetReleaseAssetsAsync(string version = null)
        {
            try
            {
                if (string.IsNullOrEmpty(version))
                {
                    var latestRelease = await GetLatestReleaseAsync();
                    return latestRelease?.Assets ?? new List<GitHubAsset>();
                }

                var releases = await GetReleasesAsync(50);
                var release = releases.FirstOrDefault(r => r.TagName == version);
                return release?.Assets ?? new List<GitHubAsset>();
            }
            catch
            {
                return new List<GitHubAsset>();
            }
        }

        private int CompareVersions(string version1, string version2)
        {
            try
            {
                // Remove 'v' prefix if present
                version1 = version1.TrimStart('v');
                version2 = version2.TrimStart('v');

                var v1Parts = version1.Split('.').Select(int.Parse).ToArray();
                var v2Parts = version2.Split('.').Select(int.Parse).ToArray();

                int maxLength = Math.Max(v1Parts.Length, v2Parts.Length);
                
                for (int i = 0; i < maxLength; i++)
                {
                    int v1Part = i < v1Parts.Length ? v1Parts[i] : 0;
                    int v2Part = i < v2Parts.Length ? v2Parts[i] : 0;

                    if (v1Part > v2Part) return 1;
                    if (v1Part < v2Part) return -1;
                }

                return 0;
            }
            catch
            {
                return 0;
            }
        }

        public void Dispose()
        {
            httpClient?.Dispose();
        }
    }

    public class GitHubRelease
    {
        public string TagName { get; set; } = "";
        public string Name { get; set; } = "";
        public string Body { get; set; } = "";
        public bool Draft { get; set; }
        public bool Prerelease { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime PublishedAt { get; set; }
        public string HtmlUrl { get; set; } = "";
        public List<GitHubAsset> Assets { get; set; } = new List<GitHubAsset>();
    }

    public class GitHubAsset
    {
        public string Name { get; set; } = "";
        public string BrowserDownloadUrl { get; set; } = "";
        public long Size { get; set; }
        public string ContentType { get; set; } = "";
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class UpdateInfo
    {
        public bool UpdateAvailable { get; set; }
        public string CurrentVersion { get; set; } = "";
        public string LatestVersion { get; set; } = "";
        public string ReleaseNotes { get; set; } = "";
        public string DownloadUrl { get; set; } = "";
        public DateTime ReleaseDate { get; set; }
        public bool IsPrerelease { get; set; }
    }
}
