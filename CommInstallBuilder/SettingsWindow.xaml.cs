using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Xml.Serialization;

namespace CommInstallBuilder
{
    public partial class SettingsWindow : Window
    {
        private AppSettings currentSettings;
        private const string SETTINGS_FILE = "app_settings.xml";
        
        // Dil değişikliği event'i
        public event EventHandler<string> LanguageChanged;

        public SettingsWindow()
        {
            InitializeComponent();
            
            // ComboBox'ların yüklenmesini bekle
            this.Loaded += SettingsWindow_Loaded;
        }

        private void SettingsWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // ComboBox'lar yüklendikten sonra ayarları yükle
            LoadSettings();
            ApplySettingsToUI();
        }

        private void LoadSettings()
        {
            try
            {
                if (File.Exists(SETTINGS_FILE))
                {
                    var serializer = new XmlSerializer(typeof(AppSettings));
                    using (var reader = new StreamReader(SETTINGS_FILE))
                    {
                        currentSettings = (AppSettings)serializer.Deserialize(reader) ?? new AppSettings();
                    }
                }
                else
                {
                    currentSettings = new AppSettings();
                }
            }
            catch
            {
                currentSettings = new AppSettings();
            }
        }

        private void SaveSettings()
        {
            try
            {
                var serializer = new XmlSerializer(typeof(AppSettings));
                using (var writer = new StreamWriter(SETTINGS_FILE))
                {
                    serializer.Serialize(writer, currentSettings);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving settings: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ApplySettingsToUI()
        {
            // ComboBox henüz yüklenmemişse işlem yapma
            if (LanguageComboBox?.Items == null)
                return;

            // Apply language setting
            foreach (ComboBoxItem item in LanguageComboBox.Items)
            {
                if (item.Tag?.ToString() == currentSettings.Language)
                {
                    LanguageComboBox.SelectedItem = item;
                    break;
                }
            }

            // Apply advanced settings
            if (AutoSaveCheckBox != null) AutoSaveCheckBox.IsChecked = currentSettings.AutoSave;
            if (CheckUpdatesCheckBox != null) CheckUpdatesCheckBox.IsChecked = currentSettings.CheckUpdates;
            if (ShowTooltipsCheckBox != null) ShowTooltipsCheckBox.IsChecked = currentSettings.ShowTooltips;
            if (EnableAnimationsCheckBox != null) EnableAnimationsCheckBox.IsChecked = currentSettings.EnableAnimations;

            // Apply GitHub settings
            if (GitHubUsernameTextBox != null) GitHubUsernameTextBox.Text = currentSettings.GitHubUsername;
            
            // Ayarları gerçekten uygula
            ApplyCurrentSettings();
        }
        
        private void ApplyCurrentSettings()
        {
            try
            {
                // Dil ayarını uygula
                if (!string.IsNullOrEmpty(currentSettings.Language))
                {
                    LanguageManager.Instance.SetLanguage(currentSettings.Language);
                }
                
                // Tema ayarını uygula
                if (!string.IsNullOrEmpty(currentSettings.Theme))
                {
                    ThemeManager.Instance.ApplyTheme(currentSettings.Theme);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error applying settings: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LanguageComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // ComboBox henüz yüklenmemişse işlem yapma
            if (LanguageComboBox?.Items == null || LanguageComboBox.Items.Count == 0)
                return;
                
            // currentSettings null kontrolü
            if (currentSettings == null)
                return;
                
            if (LanguageComboBox?.SelectedItem is ComboBoxItem selectedItem && selectedItem.Tag is string languageCode)
            {
                currentSettings.Language = languageCode;
                
                // GitHub username'i dil değişikliğine göre güncelle
                if (languageCode == "tr")
                {
                    currentSettings.GitHubUsername = "candestan";
                    if (GitHubUsernameTextBox != null)
                        GitHubUsernameTextBox.Text = "candestan";
                }
                else
                {
                    currentSettings.GitHubUsername = "candestan";
                    if (GitHubUsernameTextBox != null)
                        GitHubUsernameTextBox.Text = "candestan";
                }
                
                SaveSettings();
                
                // Gerçek dil değişikliğini uygula
                LanguageManager.Instance.SetLanguage(languageCode);
                
                // Dil değişikliği event'ini tetikle
                LanguageChanged?.Invoke(this, languageCode);
            }
        }



        private void CheckUpdates_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Simulate checking for updates
                MessageBox.Show("Checking for updates...\n\nThis feature will be implemented soon!", "Check Updates", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error checking for updates: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Update settings from UI
                currentSettings.AutoSave = AutoSaveCheckBox.IsChecked ?? false;
                currentSettings.CheckUpdates = CheckUpdatesCheckBox.IsChecked ?? false;
                currentSettings.ShowTooltips = ShowTooltipsCheckBox.IsChecked ?? false;
                currentSettings.EnableAnimations = EnableAnimationsCheckBox.IsChecked ?? false;
                currentSettings.GitHubUsername = GitHubUsernameTextBox.Text;

                SaveSettings();
                MessageBox.Show("Settings saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving settings: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = MessageBox.Show("Are you sure you want to reset all settings to default values?", "Reset Settings", MessageBoxButton.YesNo, MessageBoxImage.Question);
                
                if (result == MessageBoxResult.Yes)
                {
                    currentSettings = new AppSettings();
                    ApplySettingsToUI();
                    SaveSettings();
                    MessageBox.Show("Settings reset to default values!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error resetting settings: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }


}
