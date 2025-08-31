using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CommInstallBuilder.Pages.Modules.FileModule
{
    public partial class FileModulePage : UserControl
    {
        private List<FileItem> selectedFiles;
        private List<string> includeFilters;
        private List<string> excludeFilters;

        public FileModulePage()
        {
            InitializeComponent();
            selectedFiles = new List<FileItem>();
            includeFilters = new List<string> { "*.exe", "*.dll", "*.txt", "*.ini" };
            excludeFilters = new List<string> { "*.tmp", "*.log", "*.bak" };
            UpdateSummary();
        }

        private void UpdateSummary()
        {
            var summary = "📋 Current Configuration:\n\n";
            
            if (string.IsNullOrEmpty(SourceFolderTextBox.Text) || SourceFolderTextBox.Text == "Select source folder...")
            {
                summary += "• Source folder: Not selected\n";
            }
            else
            {
                summary += $"• Source folder: {SourceFolderTextBox.Text}\n";
            }
            
            summary += $"• Target directory: {InstallDirectoryTextBox.Text}\n";
            summary += $"• Create directories: {(CreateDirectoriesCheckBox.IsChecked == true ? "Yes" : "No")}\n";
            summary += $"• Overwrite existing: {(OverwriteExistingCheckBox.IsChecked == true ? "Yes" : "No")}\n";
            summary += $"• File verification: {(VerifyFilesCheckBox.IsChecked == true ? "Yes" : "No")}\n";
            summary += $"• Progress display: {(ShowProgressCheckBox.IsChecked == true ? "Yes" : "No")}\n";
            summary += $"• Install for all users: {(InstallForAllUsersCheckBox.IsChecked == true ? "Yes" : "No")}\n";
            summary += $"• Create log file: {(CreateLogFileCheckBox.IsChecked == true ? "Yes" : "No")}\n";
            
            summary += "\n";
            
            if (selectedFiles.Count == 0)
            {
                summary += "No files configured for installation yet.";
            }
            else
            {
                summary += $"Total files: {selectedFiles.Count}\n";
                summary += $"Total size: {FormatFileSize(selectedFiles.Sum(f => f.Size))}";
            }
            
            FileSummaryTextBlock.Text = summary;
        }

        private string FormatFileSize(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB" };
            double len = bytes;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }
            return $"{len:0.##} {sizes[order]}";
        }

        private void BrowseSourceFolder_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog
            {
                Description = "Select Source Folder",
                ShowNewFolderButton = false
            };

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SourceFolderTextBox.Text = dialog.SelectedPath;
                ScanSourceFolder(dialog.SelectedPath);
                UpdateSummary();
            }
        }

        private void BrowseInstallDirectory_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog
            {
                Description = "Select Installation Directory",
                ShowNewFolderButton = true
            };

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                InstallDirectoryTextBox.Text = dialog.SelectedPath;
                UpdateSummary();
            }
        }

        private void ScanSourceFolder(string folderPath)
        {
            try
            {
                selectedFiles.Clear();
                var files = Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories);
                
                foreach (var file in files)
                {
                    var fileName = Path.GetFileName(file);
                    var relativePath = file.Substring(folderPath.Length + 1);
                    
                    // Check if file matches include/exclude filters
                    if (ShouldIncludeFile(fileName))
                    {
                        var fileInfo = new FileInfo(file);
                        selectedFiles.Add(new FileItem
                        {
                            Name = fileName,
                            Path = relativePath,
                            Size = fileInfo.Length,
                            FullPath = file
                        });
                    }
                }
                
                UpdateFileList();
            }
            catch (Exception ex)
            {
                FileListTextBlock.Text = $"Error scanning folder: {ex.Message}";
            }
        }

        private bool ShouldIncludeFile(string fileName)
        {
            // Check exclude filters first
            foreach (var excludeFilter in excludeFilters)
            {
                if (MatchesFilter(fileName, excludeFilter))
                    return false;
            }
            
            // Check include filters
            foreach (var includeFilter in includeFilters)
            {
                if (MatchesFilter(fileName, includeFilter))
                    return true;
            }
            
            return false;
        }

        private bool MatchesFilter(string fileName, string filter)
        {
            if (filter.Contains("*"))
            {
                var pattern = filter.Replace("*", ".*").Replace("?", ".");
                try
                {
                    return System.Text.RegularExpressions.Regex.IsMatch(fileName, pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return string.Equals(fileName, filter, StringComparison.OrdinalIgnoreCase);
            }
        }

        private void UpdateFileList()
        {
            if (selectedFiles.Count == 0)
            {
                FileListTextBlock.Text = "No files found matching the current filters.";
                return;
            }
            
            var fileList = "📁 Files to be installed:\n\n";
            foreach (var file in selectedFiles.Take(50)) // Show first 50 files
            {
                fileList += $"• {file.Path} ({FormatFileSize(file.Size)})\n";
            }
            
            if (selectedFiles.Count > 50)
            {
                fileList += $"\n... and {selectedFiles.Count - 50} more files";
            }
            
            FileListTextBlock.Text = fileList;
        }

        private void AddIncludeFilter_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(IncludeFilterTextBox.Text))
            {
                var filter = IncludeFilterTextBox.Text.Trim();
                if (!includeFilters.Contains(filter))
                {
                    includeFilters.Add(filter);
                    if (!string.IsNullOrEmpty(SourceFolderTextBox.Text) && SourceFolderTextBox.Text != "Select source folder...")
                    {
                        ScanSourceFolder(SourceFolderTextBox.Text);
                    }
                    UpdateSummary();
                }
            }
        }

        private void AddExcludeFilter_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(ExcludeFilterTextBox.Text))
            {
                var filter = ExcludeFilterTextBox.Text.Trim();
                if (!excludeFilters.Contains(filter))
                {
                    excludeFilters.Add(filter);
                    if (!string.IsNullOrEmpty(SourceFolderTextBox.Text) && SourceFolderTextBox.Text != "Select source folder...")
                    {
                        ScanSourceFolder(SourceFolderTextBox.Text);
                    }
                    UpdateSummary();
                }
            }
        }

        // Event handlers for checkboxes
        private void CreateDirectoriesCheckBox_Checked(object sender, RoutedEventArgs e) => UpdateSummary();
        private void CreateDirectoriesCheckBox_Unchecked(object sender, RoutedEventArgs e) => UpdateSummary();
        private void OverwriteExistingCheckBox_Checked(object sender, RoutedEventArgs e) => UpdateSummary();
        private void OverwriteExistingCheckBox_Unchecked(object sender, RoutedEventArgs e) => UpdateSummary();
        private void PreservePermissionsCheckBox_Checked(object sender, RoutedEventArgs e) => UpdateSummary();
        private void PreservePermissionsCheckBox_Unchecked(object sender, RoutedEventArgs e) => UpdateSummary();
        private void VerifyFilesCheckBox_Checked(object sender, RoutedEventArgs e) => UpdateSummary();
        private void VerifyFilesCheckBox_Unchecked(object sender, RoutedEventArgs e) => UpdateSummary();
        private void CreateBackupCheckBox_Checked(object sender, RoutedEventArgs e) => UpdateSummary();
        private void CreateBackupCheckBox_Unchecked(object sender, RoutedEventArgs e) => UpdateSummary();
        private void InstallForAllUsersCheckBox_Checked(object sender, RoutedEventArgs e) => UpdateSummary();
        private void InstallForAllUsersCheckBox_Unchecked(object sender, RoutedEventArgs e) => UpdateSummary();
        private void CreateLogFileCheckBox_Checked(object sender, RoutedEventArgs e) => UpdateSummary();
        private void CreateLogFileCheckBox_Unchecked(object sender, RoutedEventArgs e) => UpdateSummary();
        private void ShowProgressCheckBox_Checked(object sender, RoutedEventArgs e) => UpdateSummary();
        private void ShowProgressCheckBox_Unchecked(object sender, RoutedEventArgs e) => UpdateSummary();
        private void SkipReadOnlyCheckBox_Checked(object sender, RoutedEventArgs e) => UpdateSummary();
        private void SkipReadOnlyCheckBox_Unchecked(object sender, RoutedEventArgs e) => UpdateSummary();
        private void CreateShortcutsCheckBox_Checked(object sender, RoutedEventArgs e) => UpdateSummary();
        private void CreateShortcutsCheckBox_Unchecked(object sender, RoutedEventArgs e) => UpdateSummary();

        private void InstallDirectoryTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateSummary();
        }
    }

    public class FileItem
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public long Size { get; set; }
        public string FullPath { get; set; }
    }
}

