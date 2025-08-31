using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CommInstallBuilder.Pages.Modules.RegistryModule;
using CommInstallBuilder.Pages.Modules.FileModule;
using CommInstallBuilder.Pages.Modules.ExtensionModule;
using CommInstallBuilder.Pages.Modules.ShortcutModule;
using CommInstallBuilder.Pages.Modules.EulaModule;
using CommInstallBuilder.Pages.Modules.UninstallModule;
using CommInstallBuilder.Pages.Modules.AutoStartModule;

namespace CommInstallBuilder
{
    /// <summary>
    /// MainWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            InitializeComponent();
            UpdateStatus(LanguageManager.Instance.GetText("Ready", "MainWindow"));
        }



        private void ModuleCard_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border moduleCard)
            {
                // Highlight selected module
                HighlightSelectedModule(moduleCard);
                
                // Show module configuration
                ShowModuleConfiguration(moduleCard.Name);
            }
        }

        private void HighlightSelectedModule(Border selectedCard)
        {
            // Reset all module cards
            var allCards = new[] { FileModuleCard, RegistryModuleCard, ExtensionModuleCard, 
                                  ShortcutModuleCard, EulaModuleCard, UninstallModuleCard, AutoStartModuleCard };
            
            foreach (var card in allCards)
            {
                if (card != null)
                {
                    card.BorderBrush = Application.Current.Resources["BorderBrush"] as SolidColorBrush;
                    card.BorderThickness = new Thickness(1);
                }
            }
            
            // Highlight selected card
            selectedCard.BorderBrush = Application.Current.Resources["PrimaryBrush"] as SolidColorBrush;
            selectedCard.BorderThickness = new Thickness(2);
        }

        private void ShowModuleConfiguration(string moduleName)
        {
            try
            {
                // Clear current content
                ModuleContentPanel.Children.Clear();
                
                // Update module title and show appropriate module page
                switch (moduleName)
                {
                    case "FileModuleCard":
                        ModuleTitleTextBlock.Text = LanguageManager.Instance.GetText("FileModule", "MainWindow");
                        var filePage = new Pages.Modules.FileModule.FileModulePage();
                        ModuleContentPanel.Children.Add(filePage);
                        break;
                    case "RegistryModuleCard":
                        ModuleTitleTextBlock.Text = LanguageManager.Instance.GetText("RegistryModule", "MainWindow");
                        var registryModule = new Pages.Modules.RegistryModule.RegistryModulePage();
                        ModuleContentPanel.Children.Add(registryModule);
                        break;
                    case "ExtensionModuleCard":
                        ModuleTitleTextBlock.Text = LanguageManager.Instance.GetText("ExtensionModule", "MainWindow");
                        var extensionPage = new Pages.Modules.ExtensionModule.ExtensionModulePage();
                        ModuleContentPanel.Children.Add(extensionPage);
                        break;
                    case "ShortcutModuleCard":
                        ModuleTitleTextBlock.Text = LanguageManager.Instance.GetText("ShortcutModule", "MainWindow");
                        var shortcutPage = new Pages.Modules.ShortcutModule.ShortcutModulePage();
                        ModuleContentPanel.Children.Add(shortcutPage);
                        break;
                    case "EulaModuleCard":
                        ModuleTitleTextBlock.Text = LanguageManager.Instance.GetText("EulaModule", "MainWindow");
                        var eulaPage = new Pages.Modules.EulaModule.EulaModulePage();
                        ModuleContentPanel.Children.Add(eulaPage);
                        break;
                    case "UninstallModuleCard":
                        ModuleTitleTextBlock.Text = LanguageManager.Instance.GetText("UninstallModule", "MainWindow");
                        var uninstallPage = new Pages.Modules.UninstallModule.UninstallModulePage();
                        ModuleContentPanel.Children.Add(uninstallPage);
                        break;
                    case "AutoStartModuleCard":
                        ModuleTitleTextBlock.Text = LanguageManager.Instance.GetText("AutoStartModule", "MainWindow");
                        var autoStartPage = new Pages.Modules.AutoStartModule.AutoStartModulePage();
                        ModuleContentPanel.Children.Add(autoStartPage);
                        break;
                    default:
                        UpdateStatus($"Unknown module: {moduleName}");
                        return;
                }
                
                UpdateStatus($"Configuring {ModuleTitleTextBlock.Text}");
            }
            catch (Exception ex)
            {
                UpdateStatus($"Error loading module {moduleName}: {ex.Message}");
                MessageBox.Show($"Error loading module {moduleName}:\n\n{ex.Message}\n\nStack Trace:\n{ex.StackTrace}", 
                    "Module Loading Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateStatus(LanguageManager.Instance.GetText("SaveProject", "Common") + " - Feature coming soon");
            MessageBox.Show("Save Project feature will be implemented soon!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateStatus(LanguageManager.Instance.GetText("LoadProject", "Common") + " - Feature coming soon");
            MessageBox.Show("Load Project feature will be implemented soon!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BuildButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateStatus(LanguageManager.Instance.GetText("Build", "Common") + " Installer - Feature coming soon");
            MessageBox.Show("Build Installer feature will be implemented soon!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var settingsWindow = new SettingsWindow();
                
                // Dil değişikliğini dinle
                settingsWindow.LanguageChanged += (s, languageCode) =>
                {
                    UpdateStatus($"Language changed to: {languageCode}");
                    // Dil değişikliği zaten LanguageManager tarafından uygulandı
                };
                

                
                settingsWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                UpdateStatus($"Error opening settings: {ex.Message}");
                MessageBox.Show($"Error opening settings: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateStatus(string message)
        {
            if (StatusTextBlock != null)
            {
                StatusTextBlock.Text = message;
            }
        }
        
        public void UpdateLanguage()
        {
            try
            {
                // Update status text
                UpdateStatus(LanguageManager.Instance.GetText("Ready", "MainWindow"));
                
                // Update window title if needed
                if (Title != null)
                {
                    Title = "CommInstall Builder";
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error updating language in MainWindow: {ex.Message}");
            }
        }

        // Title Bar Event Handlers
        private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (e.ChangedButton == MouseButton.Left)
                {
                    DragMove();
                }
            }
            catch (Exception ex)
            {
                UpdateStatus($"Drag error: {ex.Message}");
            }
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
                MaximizeButton.Content = "□";
            }
            else
            {
                WindowState = WindowState.Maximized;
                MaximizeButton.Content = "❐";
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public void LoadProject(string projectPath)
        {
            try
            {
                UpdateStatus($"Loading project: {projectPath}");
                // TODO: Implement project loading logic
                MessageBox.Show($"Project loaded: {projectPath}", "Project Loaded", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                UpdateStatus($"Error loading project: {ex.Message}");
                MessageBox.Show($"Error loading project: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        // LanguageManager için property'ler
        public TextBlock StatusTextBlockProperty => this.StatusTextBlock;
        public TextBlock ModuleTitleTextBlockProperty => this.ModuleTitleTextBlock;
    }
}
