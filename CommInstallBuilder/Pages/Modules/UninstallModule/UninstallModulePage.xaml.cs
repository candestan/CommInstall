using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace CommInstallBuilder.Pages.Modules.UninstallModule
{
    public partial class UninstallModulePage : UserControl
    {
        private List<string> registryKeys;
        private List<string> filesToRemove;

        public UninstallModulePage()
        {
            InitializeComponent();
            registryKeys = new List<string>();
            filesToRemove = new List<string>();
            UpdateSummary();
        }

        private void UpdateSummary()
        {
            var summary = "📋 Current Configuration:\n\n";
            
            if (CreateUninstallerCheckBox.IsChecked == true)
            {
                summary += "• Uninstaller will be created\n";
            }
            
            if (AddToAddRemoveProgramsCheckBox.IsChecked == true)
            {
                summary += "• Added to Add/Remove Programs\n";
            }
            
            if (RemoveRegistryKeysCheckBox.IsChecked == true)
            {
                summary += "• Registry keys will be cleaned\n";
            }
            
            if (RemoveShortcutsCheckBox.IsChecked == true)
            {
                summary += "• Shortcuts will be removed\n";
            }
            
            if (RemoveFileExtensionsCheckBox.IsChecked == true)
            {
                summary += "• File extensions will be removed\n";
            }
            
            if (ShowUninstallConfirmationCheckBox.IsChecked == true)
            {
                summary += "• Confirmation dialog shown\n";
            }
            
            if (ShowUninstallProgressCheckBox.IsChecked == true)
            {
                summary += "• Progress bar displayed\n";
            }
            
            summary += "\n";
            summary += $"Uninstaller location: {UninstallerLocationTextBox.Text}\n";
            
            UninstallSummaryTextBlock.Text = summary;
        }

        private void CreateUninstallerCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            UpdateSummary();
        }

        private void CreateUninstallerCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            UpdateSummary();
        }

        private void AddToAddRemoveProgramsCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            UpdateSummary();
        }

        private void AddToAddRemoveProgramsCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            UpdateSummary();
        }

        private void RemoveRegistryKeysCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            UpdateSummary();
        }

        private void RemoveRegistryKeysCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            UpdateSummary();
        }

        private void RemoveShortcutsCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            UpdateSummary();
        }

        private void RemoveShortcutsCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            UpdateSummary();
        }

        private void RemoveFileExtensionsCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            UpdateSummary();
        }

        private void RemoveFileExtensionsCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            UpdateSummary();
        }

        private void ShowUninstallConfirmationCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            UpdateSummary();
        }

        private void ShowUninstallConfirmationCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            UpdateSummary();
        }

        private void ShowUninstallProgressCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            UpdateSummary();
        }

        private void ShowUninstallProgressCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            UpdateSummary();
        }

        private void UninstallerLocationTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateSummary();
        }
    }
}
