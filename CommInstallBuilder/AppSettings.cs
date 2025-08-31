using System;

namespace CommInstallBuilder
{
    public class AppSettings
    {
        public string Language { get; set; } = "en";
        public string Theme { get; set; } = "dark";
        public bool AutoSave { get; set; } = true;
        public bool CheckUpdates { get; set; } = true;
        public bool ShowTooltips { get; set; } = true;
        public bool EnableAnimations { get; set; } = true;
        public string GitHubUsername { get; set; } = "CommInstall";
        
        public AppSettings() { }
    }
}
