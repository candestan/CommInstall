using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace CommInstallBuilder.Pages.Modules.ExtensionModule
{
    public partial class ExtensionModulePage : UserControl
    {
        private List<FileExtension> extensions;

        public ExtensionModulePage()
        {
            InitializeComponent();
            extensions = new List<FileExtension>();
            UpdateSummary();
        }

        private void UpdateSummary()
        {
            if (extensions.Count == 0)
            {
                SummaryTextBlock.Text = "No extensions configured yet. Add extensions above to see the summary here.";
            }
            else
            {
                var summary = "📋 Configured Extensions:\n\n";
                foreach (var ext in extensions)
                {
                    summary += $"• {ext.Extension} → {ext.Program}\n";
                    summary += $"  Description: {ext.Description}\n";
                    summary += $"  Icon: {ext.Icon}\n\n";
                }
                SummaryTextBlock.Text = summary;
            }
        }

        private void AddExtension_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ExtensionTextBox.Text))
            {
                MessageBox.Show("Please enter an extension.", "Extension Required", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var extension = new FileExtension
            {
                Extension = ExtensionTextBox.Text,
                Program = ProgramTextBox.Text,
                Description = DescriptionTextBox.Text,
                Icon = IconTextBox.Text
            };

            extensions.Add(extension);
            UpdateSummary();

            // Clear inputs
            ExtensionTextBox.Text = "";
            ProgramTextBox.Text = "notepad.exe";
            DescriptionTextBox.Text = "Text Document";
            IconTextBox.Text = "shell32.dll,0";
        }

        private void BrowseProgram_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                Title = "Select Program",
                Filter = "Executable files (*.exe)|*.exe|All files (*.*)|*.*"
            };

            if (dialog.ShowDialog() == true)
            {
                ProgramTextBox.Text = dialog.FileName;
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
                IconTextBox.Text = dialog.FileName;
            }
        }
    }

    public class FileExtension
    {
        public string Extension { get; set; }
        public string Program { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
    }
}
