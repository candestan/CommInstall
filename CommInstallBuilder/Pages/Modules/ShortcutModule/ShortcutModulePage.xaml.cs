using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace CommInstallBuilder.Pages.Modules.ShortcutModule
{
    public partial class ShortcutModulePage : UserControl
    {
        private List<Shortcut> shortcuts;

        public ShortcutModulePage()
        {
            InitializeComponent();
            shortcuts = new List<Shortcut>();
            UpdateSummary();
        }

        private void UpdateSummary()
        {
            var summary = "📋 Shortcuts to be created:\n\n";
            
            if (CreateDesktopShortcutCheckBox.IsChecked == true)
            {
                summary += $"• Desktop: {DesktopShortcutNameTextBox.Text}\n";
            }
            
            if (CreateStartMenuShortcutCheckBox.IsChecked == true)
            {
                summary += $"• Start Menu: CommInstall ({StartMenuGroupTextBox.Text} group)\n";
            }
            
            if (CreateQuickLaunchShortcutCheckBox.IsChecked == true)
            {
                summary += "• Quick Launch: CommInstall\n";
            }
            
            if (shortcuts.Count == 0)
            {
                summary += "\nNo custom shortcuts configured yet.";
            }
            else
            {
                summary += "\nCustom shortcuts:\n";
                foreach (var shortcut in shortcuts)
                {
                    summary += $"• {shortcut.Name} → {shortcut.Target}\n";
                }
            }
            
            ShortcutsSummaryTextBlock.Text = summary;
        }

        private void CreateDesktopShortcutCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            UpdateSummary();
        }

        private void CreateStartMenuShortcutCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            UpdateSummary();
        }

        private void CreateQuickLaunchShortcutCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            UpdateSummary();
        }

        private void CreateDesktopShortcutCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            UpdateSummary();
        }

        private void CreateStartMenuShortcutCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            UpdateSummary();
        }

        private void CreateQuickLaunchShortcutCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            UpdateSummary();
        }

        private void DesktopShortcutNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateSummary();
        }

        private void StartMenuGroupTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateSummary();
        }
    }

    public class Shortcut
    {
        public string Name { get; set; }
        public string Target { get; set; }
        public string Arguments { get; set; }
        public string WorkingDirectory { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
    }
}
