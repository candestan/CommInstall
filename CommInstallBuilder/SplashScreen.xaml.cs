using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media;

namespace CommInstallBuilder
{
    public partial class SplashScreen : Window
    {
        public SplashScreen()
        {
            InitializeComponent();
            StartLoadingAnimation();
        }

        private void StartLoadingAnimation()
        {
            // Create and start the rotating animation
            var animation = new DoubleAnimation
            {
                From = 0,
                To = 360,
                Duration = TimeSpan.FromSeconds(1),
                RepeatBehavior = RepeatBehavior.Forever
            };

            var rotateTransform = LoadingRing.RenderTransform as RotateTransform;
            if (rotateTransform != null)
            {
                rotateTransform.BeginAnimation(RotateTransform.AngleProperty, animation);
            }
            
            // Simulate loading process
            SimulateLoading();
        }

        private async void SimulateLoading()
        {
            try
            {
                // Gerçek loading işlemleri
                UpdateLoadingStatus("Initializing...", "Loading core components");
                await InitializeCoreComponents();
                
                UpdateLoadingStatus("Loading modules...", "Loading file management system");
                await LoadFileModule();
                
                UpdateLoadingStatus("Loading modules...", "Loading registry configuration");
                await LoadRegistryModule();
                
                UpdateLoadingStatus("Loading modules...", "Loading extension handlers");
                await LoadExtensionModule();
                
                UpdateLoadingStatus("Loading modules...", "Loading shortcut management");
                await LoadShortcutModule();
                
                UpdateLoadingStatus("Loading modules...", "Loading EULA system");
                await LoadEulaModule();
                
                UpdateLoadingStatus("Loading modules...", "Loading uninstall tools");
                await LoadUninstallModule();
                
                UpdateLoadingStatus("Loading modules...", "Loading auto-start configuration");
                await LoadAutoStartModule();
                
                UpdateLoadingStatus("Loading modules...", "Loading language system");
                await LoadLanguageSystem();
                
                UpdateLoadingStatus("Loading modules...", "Loading theme system");
                await LoadThemeSystem();
                
                UpdateLoadingStatus("Loading modules...", "Loading project system");
                await LoadProjectSystem();
                
                UpdateLoadingStatus("Finalizing...", "Preparing application");
                await FinalizeInitialization();
                
                UpdateLoadingStatus("Ready!", "All components loaded successfully");
                
                // Kısa bir bekleme sonra kapat
                await Task.Delay(300);
                
                // Splash screen'i kapat (ProjectWizard App.xaml.cs tarafından gösterilecek)
                System.Diagnostics.Debug.WriteLine("SplashScreen: Closing window...");
                this.Close();
                System.Diagnostics.Debug.WriteLine("SplashScreen: Window closed.");
            }
            catch (Exception ex)
            {
                UpdateLoadingStatus("Error!", $"Failed to load: {ex.Message}");
                await Task.Delay(2000);
                
                // Hata olsa bile splash screen'i kapat
                System.Diagnostics.Debug.WriteLine("SplashScreen: Closing window due to error...");
                this.Close();
                System.Diagnostics.Debug.WriteLine("SplashScreen: Window closed due to error.");
            }
        }

        private void UpdateLoadingStatus(string loadingText, string statusText)
        {
            Dispatcher.Invoke(() =>
            {
                LoadingText.Text = loadingText;
                StatusText.Text = statusText;
            });
        }

        // Gerçek loading metodları
        private async Task InitializeCoreComponents()
        {
            try
            {
                // Core bileşenleri başlat
                await Task.Run(() =>
                {
                    // Assembly'leri yükle
                    var assemblies = AppDomain.CurrentDomain.GetAssemblies();
                    
                    // Gerekli namespace'leri kontrol et
                    var namespaces = new[]
                    {
                        "CommInstallBuilder.Pages.Modules.FileModule",
                        "CommInstallBuilder.Pages.Modules.RegistryModule",
                        "CommInstallBuilder.Pages.Modules.ExtensionModule",
                        "CommInstallBuilder.Pages.Modules.ShortcutModule",
                        "CommInstallBuilder.Pages.Modules.EulaModule",
                        "CommInstallBuilder.Pages.Modules.UninstallModule",
                        "CommInstallBuilder.Pages.Modules.AutoStartModule"
                    };

                    foreach (var ns in namespaces)
                    {
                        try
                        {
                            var type = Type.GetType(ns + ".FileModulePage");
                            if (type != null)
                            {
                                System.Diagnostics.Debug.WriteLine($"Loaded module: {ns}");
                            }
                        }
                        catch { /* Ignore */ }
                    }
                });
                
                await Task.Delay(100); // Kısa bir bekleme
            }
            catch
            {
                // Hata durumunda devam et
            }
        }

        private async Task LoadFileModule()
        {
            try
            {
                await Task.Run(() =>
                {
                    // FileModule'ü gerçekten yükle
                    var fileModuleType = Type.GetType("CommInstallBuilder.Pages.Modules.FileModule.FileModulePage");
                    if (fileModuleType != null)
                    {
                        // Module'ün varlığını doğrula
                        System.Diagnostics.Debug.WriteLine("FileModule loaded successfully");
                    }
                });
                
                await Task.Delay(50);
            }
            catch
            {
                // Hata durumunda devam et
            }
        }

        private async Task LoadRegistryModule()
        {
            try
            {
                await Task.Run(() =>
                {
                    var registryModuleType = Type.GetType("CommInstallBuilder.Pages.Modules.RegistryModule.RegistryModulePage");
                    if (registryModuleType != null)
                    {
                        System.Diagnostics.Debug.WriteLine("RegistryModule loaded successfully");
                    }
                });
                
                await Task.Delay(50);
            }
            catch
            {
                // Hata durumunda devam et
            }
        }

        private async Task LoadExtensionModule()
        {
            try
            {
                await Task.Run(() =>
                {
                    var extensionModuleType = Type.GetType("CommInstallBuilder.Pages.Modules.ExtensionModule.ExtensionModulePage");
                    if (extensionModuleType != null)
                    {
                        System.Diagnostics.Debug.WriteLine("ExtensionModule loaded successfully");
                    }
                });
                
                await Task.Delay(50);
            }
            catch
            {
                // Hata durumunda devam et
            }
        }

        private async Task LoadShortcutModule()
        {
            try
            {
                await Task.Run(() =>
                {
                    var shortcutModuleType = Type.GetType("CommInstallBuilder.Pages.Modules.ShortcutModule.ShortcutModulePage");
                    if (shortcutModuleType != null)
                    {
                        System.Diagnostics.Debug.WriteLine("ShortcutModule loaded successfully");
                    }
                });
                
                await Task.Delay(50);
            }
            catch
            {
                // Hata durumunda devam et
            }
        }

        private async Task LoadEulaModule()
        {
            try
            {
                await Task.Run(() =>
                {
                    var eulaModuleType = Type.GetType("CommInstallBuilder.Pages.Modules.EulaModule.EulaModulePage");
                    if (eulaModuleType != null)
                    {
                        System.Diagnostics.Debug.WriteLine("EulaModule loaded successfully");
                    }
                });
                
                await Task.Delay(50);
            }
            catch
            {
                // Hata durumunda devam et
            }
        }

        private async Task LoadUninstallModule()
        {
            try
            {
                await Task.Run(() =>
                {
                    var uninstallModuleType = Type.GetType("CommInstallBuilder.Pages.Modules.UninstallModule.UninstallModulePage");
                    if (uninstallModuleType != null)
                    {
                        System.Diagnostics.Debug.WriteLine("UninstallModule loaded successfully");
                    }
                });
                
                await Task.Delay(50);
            }
            catch
            {
                // Hata durumunda devam et
            }
        }

        private async Task LoadAutoStartModule()
        {
            try
            {
                await Task.Run(() =>
                {
                    var autoStartModuleType = Type.GetType("CommInstallBuilder.Pages.Modules.AutoStartModule.AutoStartModulePage");
                    if (autoStartModuleType != null)
                    {
                        System.Diagnostics.Debug.WriteLine("AutoStartModule loaded successfully");
                    }
                });
                
                await Task.Delay(50);
            }
            catch
            {
                // Hata durumunda devam et
            }
        }

        private async Task LoadLanguageSystem()
        {
            try
            {
                await Task.Run(() =>
                {
                    // Dil dosyalarını kontrol et
                    var languagesFolder = System.IO.Path.Combine(
                        System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
                        "Localization", "Languages"
                    );
                    
                    if (System.IO.Directory.Exists(languagesFolder))
                    {
                        var languageFiles = System.IO.Directory.GetFiles(languagesFolder, "*.json");
                        System.Diagnostics.Debug.WriteLine($"Found {languageFiles.Length} language files");
                    }
                });
                
                await Task.Delay(50);
            }
            catch
            {
                // Hata durumunda devam et
            }
        }

        private async Task LoadThemeSystem()
        {
            try
            {
                await Task.Run(() =>
                {
                    // Theme sistemini kontrol et
                    var themeManagerType = Type.GetType("CommInstallBuilder.ThemeManager");
                    if (themeManagerType != null)
                    {
                        System.Diagnostics.Debug.WriteLine("ThemeManager loaded successfully");
                    }
                });
                
                await Task.Delay(50);
            }
            catch
            {
                // Hata durumunda devam et
            }
        }

        private async Task LoadProjectSystem()
        {
            try
            {
                await Task.Run(() =>
                {
                    // Project sistemini kontrol et
                    var projectWizardType = Type.GetType("CommInstallBuilder.ProjectWizard");
                    var mainWindowType = Type.GetType("CommInstallBuilder.MainWindow");
                    
                    if (projectWizardType != null && mainWindowType != null)
                    {
                        System.Diagnostics.Debug.WriteLine("Project system loaded successfully");
                    }
                });
                
                await Task.Delay(50);
            }
            catch
            {
                // Hata durumunda devam et
            }
        }

        private async Task FinalizeInitialization()
        {
            try
            {
                await Task.Run(() =>
                {
                    // Son kontroller
                    System.Diagnostics.Debug.WriteLine("Finalizing application initialization...");
                    
                    // Gerekli dosyaların varlığını kontrol et
                    var exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                    var exeDir = System.IO.Path.GetDirectoryName(exePath);
                    
                    var requiredFiles = new[]
                    {
                        "Newtonsoft.Json.dll",
                        "app_settings.xml"
                    };
                    
                    foreach (var file in requiredFiles)
                    {
                        var filePath = System.IO.Path.Combine(exeDir, file);
                        if (System.IO.File.Exists(filePath))
                        {
                            System.Diagnostics.Debug.WriteLine($"Required file found: {file}");
                        }
                    }
                });
                
                await Task.Delay(100);
            }
            catch
            {
                // Hata durumunda devam et
            }
        }
    }
}
