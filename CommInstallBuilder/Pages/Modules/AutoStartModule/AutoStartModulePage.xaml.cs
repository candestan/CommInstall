using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace CommInstallBuilder.Pages.Modules.AutoStartModule
{
    public partial class AutoStartModulePage : UserControl
    {
        private List<AutoStartEntry> autoStartEntries;

        public AutoStartModulePage()
        {
            InitializeComponent();
            autoStartEntries = new List<AutoStartEntry>();
            UpdateSummary();
        }

        private void UpdateSummary()
        {
            var summary = "📋 Current Configuration:\n\n";
            
            if (EnableAutoStartCheckBox.IsChecked == true)
            {
                summary += "• Auto-start enabled\n";
            }
            
            if (StartWithWindowsCheckBox.IsChecked == true)
            {
                summary += "• Starts with Windows\n";
            }
            
            if (StartMinimizedCheckBox.IsChecked == true)
            {
                summary += "• Starts minimized\n";
            }
            
            if (ShowStartupNotificationCheckBox.IsChecked == true)
            {
                summary += "• Shows startup notification\n";
            }
            
            if (DelayStartupCheckBox.IsChecked == true)
            {
                summary += "• Startup delayed\n";
            }
            
            if (StartOnlyIfUserLoggedInCheckBox.IsChecked == true)
            {
                summary += "• Starts only when user logged in\n";
            }
            
            if (StartForAllUsersCheckBox.IsChecked == true)
            {
                summary += "• Starts for all users\n";
            }
            
            if (StartInSafeModeCheckBox.IsChecked == true)
            {
                summary += "• Starts in safe mode\n";
            }
            
            if (StartWithNetworkCheckBox.IsChecked == true)
            {
                summary += "• Waits for network\n";
            }
            
            summary += "\n";
            
            if (RegistryStartupRadioButton.IsChecked == true)
            {
                summary += "• Uses Registry method\n";
            }
            else if (StartupFolderRadioButton.IsChecked == true)
            {
                summary += "• Uses Startup Folder method\n";
            }
            else if (TaskSchedulerRadioButton.IsChecked == true)
            {
                summary += "• Uses Task Scheduler method\n";
            }
            
            summary += $"• {StartupDelaySlider.Value} second startup delay\n";
            summary += $"• Application: {ApplicationPathTextBox.Text}\n";
            summary += $"• Arguments: {StartupArgumentsTextBox.Text}";
            
            AutoStartSummaryTextBlock.Text = summary;
        }

        private void EnableAutoStartCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            UpdateSummary();
        }

        private void EnableAutoStartCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            UpdateSummary();
        }

        private void StartWithWindowsCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            UpdateSummary();
        }

        private void StartWithWindowsCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            UpdateSummary();
        }

        private void StartMinimizedCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            UpdateSummary();
        }

        private void StartMinimizedCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            UpdateSummary();
        }

        private void ShowStartupNotificationCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            UpdateSummary();
        }

        private void ShowStartupNotificationCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            UpdateSummary();
        }

        private void DelayStartupCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            UpdateSummary();
        }

        private void DelayStartupCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            UpdateSummary();
        }

        private void StartOnlyIfUserLoggedInCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            UpdateSummary();
        }

        private void StartOnlyIfUserLoggedInCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            UpdateSummary();
        }

        private void StartForAllUsersCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            UpdateSummary();
        }

        private void StartForAllUsersCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            UpdateSummary();
        }

        private void StartInSafeModeCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            UpdateSummary();
        }

        private void StartInSafeModeCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            UpdateSummary();
        }

        private void StartWithNetworkCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            UpdateSummary();
        }

        private void StartWithNetworkCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            UpdateSummary();
        }

        private void RegistryStartupRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            UpdateSummary();
        }

        private void StartupFolderRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            UpdateSummary();
        }

        private void TaskSchedulerRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            UpdateSummary();
        }

        private void StartupDelaySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (DelayValueTextBlock != null)
            {
                DelayValueTextBlock.Text = $"{StartupDelaySlider.Value} seconds";
            }
            UpdateSummary();
        }

        private void ApplicationPathTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateSummary();
        }

        private void StartupArgumentsTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateSummary();
        }
    }

    public class AutoStartEntry
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string Arguments { get; set; }
        public string WorkingDirectory { get; set; }
        public int Delay { get; set; }
        public bool StartMinimized { get; set; }
        public bool StartForAllUsers { get; set; }
    }
}
