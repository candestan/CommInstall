using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CommInstallBuilder
{
    /// <summary>
    /// App.xaml etkileşim mantığı
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            // Initialize managers
            var languageManager = LanguageManager.Instance;
            var themeManager = ThemeManager.Instance;
            
            // Load and apply saved settings
            LoadAndApplySettings();
            
            // Show SplashScreen first, then ProjectWizard
            var splashScreen = new SplashScreen();
            splashScreen.Show();
            
            // Show ProjectWizard after a delay (simulating splash screen)
            var timer = new System.Windows.Threading.DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(4) // Match splash screen duration
            };
            
            timer.Tick += (s, args) =>
            {
                timer.Stop();
                splashScreen.Hide();
                
                var projectWizard = new ProjectWizard();
                projectWizard.Show();
                System.Diagnostics.Debug.WriteLine("App: ProjectWizard shown after timer.");
            };
            
            timer.Start();
        }
        
        private void LoadAndApplySettings()
        {
            try
            {
                const string SETTINGS_FILE = "app_settings.xml";
                
                if (System.IO.File.Exists(SETTINGS_FILE))
                {
                    var serializer = new System.Xml.Serialization.XmlSerializer(typeof(AppSettings));
                    using (var reader = new System.IO.StreamReader(SETTINGS_FILE))
                    {
                        var settings = (AppSettings)serializer.Deserialize(reader);
                        if (settings != null)
                        {
                            // Apply saved language
                            if (!string.IsNullOrEmpty(settings.Language))
                            {
                                LanguageManager.Instance.SetLanguage(settings.Language);
                            }
                            
                            // Apply saved theme
                            if (!string.IsNullOrEmpty(settings.Theme))
                            {
                                ThemeManager.Instance.ApplyTheme(settings.Theme);
                            }
                        }
                    }
                }
                
                // If no settings file or no theme setting, apply default dark theme
                if (!System.IO.File.Exists(SETTINGS_FILE) || 
                    !System.IO.File.Exists(SETTINGS_FILE) || 
                    string.IsNullOrEmpty(ThemeManager.Instance.CurrentTheme))
                {
                    ThemeManager.Instance.ApplyTheme("dark");
                }
            }
            catch (Exception ex)
            {
                // Log error but don't crash
                System.Diagnostics.Debug.WriteLine($"Error loading settings: {ex.Message}");
                
                // Apply default dark theme on error
                ThemeManager.Instance.ApplyTheme("dark");
            }
        }
        

    }
}
