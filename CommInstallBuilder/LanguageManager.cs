using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using Newtonsoft.Json;

namespace CommInstallBuilder
{
    public class LanguageManager
    {
        private static LanguageManager instance;
        private Dictionary<string, Dictionary<string, object>> languages;
        private string currentLanguage = "en";
        private const string LANGUAGES_FOLDER = "Languages";
        
        public static LanguageManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new LanguageManager();
                return instance;
            }
        }
        
        private LanguageManager()
        {
            LoadLanguages();
        }
        
        public void LoadLanguages()
        {
            try
            {
                languages = new Dictionary<string, Dictionary<string, object>>();
                
                // Load languages from JSON files
                var languagesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, LANGUAGES_FOLDER);
                
                if (Directory.Exists(languagesPath))
                {
                    var jsonFiles = Directory.GetFiles(languagesPath, "*.json");
                    
                    foreach (var jsonFile in jsonFiles)
                    {
                        try
                        {
                            var languageCode = Path.GetFileNameWithoutExtension(jsonFile);
                            var jsonContent = File.ReadAllText(jsonFile);
                            var languageData = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonContent);
                            
                            if (languageData != null)
                            {
                                languages[languageCode] = languageData;
                            }
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine($"Error loading language file {jsonFile}: {ex.Message}");
                        }
                    }
                }
                
                // Fallback to hardcoded if no JSON files found
                if (languages.Count == 0)
                {
                    LoadFallbackLanguages();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading languages: {ex.Message}");
                LoadFallbackLanguages();
            }
        }
        
        private void LoadFallbackLanguages()
        {
            try
            {
                // Fallback hardcoded translations
                var enTranslations = new Dictionary<string, object>
                {
                    ["Common"] = new Dictionary<string, object>
                    {
                        ["Save"] = "Save",
                        ["Cancel"] = "Cancel",
                        ["OK"] = "OK",
                        ["Error"] = "Error",
                        ["Success"] = "Success",
                        ["Build"] = "Build",
                        ["SaveProject"] = "Save Project",
                        ["LoadProject"] = "Load Project",
                        ["Settings"] = "Settings",
                        ["CreateProject"] = "Create Project",
                        ["OpenProject"] = "Open Project",
                        ["RecentProjects"] = "Recent Projects",
                        ["NoRecentProjects"] = "No recent projects"
                    },
                    ["MainWindow"] = new Dictionary<string, object>
                    {
                        ["Ready"] = "Ready - Select modules to configure your installer",
                        ["FileInstallation"] = "File Installation Configuration",
                        ["RegistryKeys"] = "Registry Keys Configuration",
                        ["FileModule"] = "File Installation",
                        ["RegistryModule"] = "Registry Keys",
                        ["ExtensionModule"] = "File Extensions",
                        ["ShortcutModule"] = "Shortcuts",
                        ["EulaModule"] = "EULA/License",
                        ["UninstallModule"] = "Uninstall",
                        ["AutoStartModule"] = "Auto-Start"
                    },
                    ["Settings"] = new Dictionary<string, object>
                    {
                        ["Settings"] = "Settings",
                        ["SaveSettings"] = "Save Settings",
                        ["ResetToDefault"] = "Reset to Default",
                        ["Language"] = "Language",
                        ["Theme"] = "Theme",
                        ["AutoSave"] = "Auto Save",
                        ["CheckUpdates"] = "Check for Updates",
                        ["ShowTooltips"] = "Show Tooltips",
                        ["EnableAnimations"] = "Enable Animations",
                        ["GitHubUsername"] = "GitHub Username"
                    },
                    ["ProjectWizard"] = new Dictionary<string, object>
                    {
                        ["ProjectWizard"] = "CommInstall Builder - Project Wizard",
                        ["CreateNewProject"] = "Create New Project",
                        ["OpenExistingProject"] = "Open Existing Project",
                        ["ProjectName"] = "Project Name",
                        ["ProjectPath"] = "Project Path",
                        ["Browse"] = "Browse",
                        ["Create"] = "Create",
                        ["Open"] = "Open"
                    },
                    ["SplashScreen"] = new Dictionary<string, object>
                    {
                        ["Loading"] = "Loading...",
                        ["Initializing"] = "Initializing...",
                        ["Ready"] = "Ready!"
                    }
                };
                
                var trTranslations = new Dictionary<string, object>
                {
                    ["Common"] = new Dictionary<string, object>
                    {
                        ["Save"] = "Kaydet",
                        ["Cancel"] = "İptal",
                        ["OK"] = "Tamam",
                        ["Error"] = "Hata",
                        ["Success"] = "Başarılı",
                        ["Build"] = "Derle",
                        ["SaveProject"] = "Projeyi Kaydet",
                        ["LoadProject"] = "Proje Yükle",
                        ["Settings"] = "Ayarlar",
                        ["CreateProject"] = "Proje Oluştur",
                        ["OpenProject"] = "Proje Aç",
                        ["RecentProjects"] = "Son Projeler",
                        ["NoRecentProjects"] = "Son proje yok"
                    },
                    ["MainWindow"] = new Dictionary<string, object>
                    {
                        ["Ready"] = "Hazır - Kurulum yapılandırmak için modülleri seçin",
                        ["FileInstallation"] = "Dosya Kurulum Yapılandırması",
                        ["RegistryKeys"] = "Kayıt Defteri Anahtarları Yapılandırması",
                        ["FileModule"] = "Dosya Kurulumu",
                        ["RegistryModule"] = "Kayıt Defteri Anahtarları",
                        ["ExtensionModule"] = "Dosya Uzantıları",
                        ["ShortcutModule"] = "Kısayollar",
                        ["EulaModule"] = "EULA/Lisans",
                        ["UninstallModule"] = "Kaldırma",
                        ["AutoStartModule"] = "Otomatik Başlatma"
                    },
                    ["Settings"] = new Dictionary<string, object>
                    {
                        ["Settings"] = "Ayarlar",
                        ["SaveSettings"] = "Ayarları Kaydet",
                        ["ResetToDefault"] = "Varsayılana Sıfırla",
                        ["Language"] = "Dil",
                        ["Theme"] = "Tema",
                        ["AutoSave"] = "Otomatik Kaydet",
                        ["CheckUpdates"] = "Güncellemeleri Kontrol Et",
                        ["ShowTooltips"] = "İpucu Göster",
                        ["EnableAnimations"] = "Animasyonları Etkinleştir",
                        ["GitHubUsername"] = "GitHub Kullanıcı Adı"
                    },
                    ["ProjectWizard"] = new Dictionary<string, object>
                    {
                        ["ProjectWizard"] = "CommInstall Builder - Proje Sihirbazı",
                        ["CreateNewProject"] = "Yeni Proje Oluştur",
                        ["OpenExistingProject"] = "Mevcut Projeyi Aç",
                        ["ProjectName"] = "Proje Adı",
                        ["ProjectPath"] = "Proje Yolu",
                        ["Browse"] = "Gözat",
                        ["Create"] = "Oluştur",
                        ["Open"] = "Aç"
                    },
                    ["SplashScreen"] = new Dictionary<string, object>
                    {
                        ["Loading"] = "Yükleniyor...",
                        ["Initializing"] = "Başlatılıyor...",
                        ["Ready"] = "Hazır!"
                    }
                };
                
                languages["en"] = enTranslations;
                languages["tr"] = trTranslations;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading fallback languages: {ex.Message}");
            }
        }
        
        public void SetLanguage(string languageCode)
        {
            if (languages.ContainsKey(languageCode))
            {
                currentLanguage = languageCode;
                ApplyLanguageToUI();
            }
        }
        
        public string GetText(string key, string section = "Common")
        {
            try
            {
                if (languages.ContainsKey(currentLanguage))
                {
                    var lang = languages[currentLanguage];
                    if (lang.ContainsKey(section))
                    {
                        var sectionData = lang[section] as Dictionary<string, object>;
                        if (sectionData != null && sectionData.ContainsKey(key))
                        {
                            return sectionData[key].ToString();
                        }
                    }
                }
                
                // Fallback to English
                if (languages.ContainsKey("en"))
                {
                    var enLang = languages["en"];
                    if (enLang.ContainsKey(section))
                    {
                        var sectionData = enLang[section] as Dictionary<string, object>;
                        if (sectionData != null && sectionData.ContainsKey(key))
                        {
                            return sectionData[key].ToString();
                        }
                    }
                }
                
                return key; // Return key if translation not found
            }
            catch
            {
                return key;
            }
        }
        
        private void ApplyLanguageToUI()
        {
            // Update all windows that are currently open
            foreach (Window window in Application.Current.Windows)
            {
                if (window is MainWindow mainWindow)
                {
                    UpdateMainWindowLanguage(mainWindow);
                }
                else if (window is SettingsWindow settingsWindow)
                {
                    UpdateSettingsWindowLanguage(settingsWindow);
                }
                else if (window is ProjectWizard projectWizard)
                {
                    UpdateProjectWizardLanguage(projectWizard);
                }
            }
        }
        
        private void UpdateMainWindowLanguage(MainWindow window)
        {
            try
            {
                // Call the UpdateLanguage method in MainWindow
                window.UpdateLanguage();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error updating MainWindow language: {ex.Message}");
            }
        }
        
        private void UpdateSettingsWindowLanguage(SettingsWindow window)
        {
            try
            {
                // Update window title
                window.Title = GetText("Settings", "Settings");
                
                // Update button texts
                if (window.SaveButton != null)
                    window.SaveButton.Content = GetText("SaveSettings", "Settings");
                if (window.ResetButton != null)
                    window.ResetButton.Content = GetText("ResetToDefault", "Settings");
            }
            catch { }
        }
        
        private void UpdateProjectWizardLanguage(ProjectWizard window)
        {
            try
            {
                // Update window title
                window.Title = GetText("ProjectWizard", "ProjectWizard");
            }
            catch { }
        }
        
        public string CurrentLanguage => currentLanguage;
    }
}
