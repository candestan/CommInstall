using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace CommInstallBuilder.Pages.Modules.EulaModule
{
    public partial class EulaModulePage : UserControl
    {
        private List<EulaLanguage> eulaLanguages;

        public EulaModulePage()
        {
            InitializeComponent();
            eulaLanguages = new List<EulaLanguage>();
            UpdateSummary();
        }

        private void UpdateSummary()
        {
            var summary = "📋 Current Configuration:\n\n";
            
            if (ShowEulaCheckBox.IsChecked == true)
            {
                summary += "• EULA will be shown during installation\n";
            }
            
            if (RequireAcceptanceCheckBox.IsChecked == true)
            {
                summary += "• User acceptance required\n";
            }
            
            if (ShowEulaBeforeInstallCheckBox.IsChecked == true)
            {
                summary += "• EULA shown before file installation\n";
            }
            
            if (SaveEulaAcceptanceCheckBox.IsChecked == true)
            {
                summary += "• EULA acceptance saved in registry\n";
            }
            
            if (MultiLanguageEulaCheckBox.IsChecked == true)
            {
                summary += "• Multi-language support enabled\n";
            }
            
            summary += "\n";
            
            if (eulaLanguages.Count == 0)
            {
                summary += "No EULA file selected yet.";
            }
            else
            {
                summary += "EULA files:\n";
                foreach (var lang in eulaLanguages)
                {
                    summary += $"• {lang.Language}: {lang.FilePath}\n";
                }
            }
            
            EulaSummaryTextBlock.Text = summary;
        }

        private void Browse_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                Title = "Select EULA File",
                Filter = "Text files (*.txt)|*.txt|Rich text files (*.rtf)|*.rtf|All files (*.*)|*.*"
            };

            if (dialog.ShowDialog() == true)
            {
                EulaFileTextBox.Text = dialog.FileName;
                LoadEulaPreview(dialog.FileName);
            }
        }

        private void LoadEulaPreview(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    var content = File.ReadAllText(filePath);
                    EulaPreviewTextBlock.Text = content;
                }
            }
            catch (Exception ex)
            {
                EulaPreviewTextBlock.Text = $"Error loading file: {ex.Message}";
            }
        }

        private void SelectIcon_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                Title = "Select Icon",
                Filter = "Icon files (*.ico)|*.ico|Executable files (*.exe)|*.exe|DLL files (*.dll)|*.dll|All files (*.*)|*.*"
            };

            if (dialog.ShowDialog() == true)
            {
                // IconTextBox doesn't exist in current XAML, so we'll just show a message
                MessageBox.Show($"Icon file selected: {dialog.FileName}", "Icon Selected", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void AddLanguage_Click(object sender, RoutedEventArgs e)
        {
            if (LanguageComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a language.", "Language Required", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var selectedItem = LanguageComboBox.SelectedItem as ComboBoxItem;
            var languageCode = selectedItem.Tag.ToString();
            var languageName = selectedItem.Content.ToString();

            var eulaLang = new EulaLanguage
            {
                Language = languageName,
                LanguageCode = languageCode,
                FilePath = EulaFileTextBox.Text
            };

            eulaLanguages.Add(eulaLang);
            UpdateSummary();
        }

        private void ShowEulaCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            UpdateSummary();
        }

        private void ShowEulaCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            UpdateSummary();
        }

        private void RequireAcceptanceCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            UpdateSummary();
        }

        private void RequireAcceptanceCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            UpdateSummary();
        }

        private void ShowEulaBeforeInstallCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            UpdateSummary();
        }

        private void ShowEulaBeforeInstallCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            UpdateSummary();
        }

        private void SaveEulaAcceptanceCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            UpdateSummary();
        }

        private void SaveEulaAcceptanceCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            UpdateSummary();
        }

        private void MultiLanguageEulaCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            UpdateSummary();
        }

        private void MultiLanguageEulaCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            UpdateSummary();
        }
    }

    public class EulaLanguage
    {
        public string Language { get; set; }
        public string LanguageCode { get; set; }
        public string FilePath { get; set; }
        public string Content { get; set; }
    }
}
