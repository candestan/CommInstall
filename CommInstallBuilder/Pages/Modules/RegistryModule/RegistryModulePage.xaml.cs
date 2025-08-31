using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CommInstallBuilder.Pages.Modules.RegistryModule
{
    public partial class RegistryModulePage : UserControl
    {
        private ObservableCollection<RegistryKeyItem> registryKeys;
        private ObservableCollection<RegistryValueItem> registryValues;

        public RegistryModulePage()
        {
            InitializeComponent();
            InitializeCollections();
            UpdateCounts();
        }

        private void InitializeCollections()
        {
            registryKeys = new ObservableCollection<RegistryKeyItem>();
            registryValues = new ObservableCollection<RegistryValueItem>();

            KeysListBox.ItemsSource = registryKeys;
            ValuesListBox.ItemsSource = registryValues;
        }

        private void AddKeyButton_Click(object sender, RoutedEventArgs e)
        {
            string keyPath = KeyPathTextBox.Text.Trim();
            if (string.IsNullOrEmpty(keyPath))
            {
                MessageBox.Show("Please enter a registry key path.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (registryKeys.Any(k => k.KeyPath == keyPath))
            {
                MessageBox.Show("This registry key already exists.", "Duplicate Key", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var keyItem = new RegistryKeyItem
            {
                KeyPath = keyPath,
                RootKey = GetSelectedRootKey()
            };

            registryKeys.Add(keyItem);
            UpdateCounts();
            UpdateSummary();
        }

        private void RemoveKey_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is RegistryKeyItem keyItem)
            {
                registryKeys.Remove(keyItem);
                UpdateCounts();
                UpdateSummary();
            }
        }

        private void AddValueButton_Click(object sender, RoutedEventArgs e)
        {
            string valueName = ValueNameTextBox.Text.Trim();
            string valueData = ValueDataTextBox.Text.Trim();
            string valueType = GetSelectedValueType();

            if (string.IsNullOrEmpty(valueName))
            {
                MessageBox.Show("Please enter a value name.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (registryValues.Any(v => v.Name == valueName))
            {
                MessageBox.Show("A value with this name already exists.", "Duplicate Value", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var valueItem = new RegistryValueItem
            {
                Name = valueName,
                Data = valueData,
                Type = valueType
            };

            registryValues.Add(valueItem);
            UpdateCounts();
            UpdateSummary();
        }

        private void RemoveValue_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is RegistryValueItem valueItem)
            {
                registryValues.Remove(valueItem);
                UpdateCounts();
                UpdateSummary();
            }
        }

        private string GetSelectedRootKey()
        {
            if (RootKeyComboBox.SelectedItem is ComboBoxItem selectedItem && selectedItem.Tag is string tag)
            {
                return tag;
            }
            return "HKLM";
        }

        private string GetSelectedValueType()
        {
            if (ValueTypeComboBox.SelectedItem is ComboBoxItem selectedItem && selectedItem.Tag is string tag)
            {
                return tag;
            }
            return "REG_SZ";
        }

        private void UpdateCounts()
        {
            KeyCountText.Text = $"Selected Keys: {registryKeys.Count}";
            ValueCountText.Text = $"Selected Values: {registryValues.Count}";
        }

        private void UpdateSummary()
        {
            if (registryKeys.Count == 0 && registryValues.Count == 0)
            {
                SummaryText.Text = "No registry configuration set.";
                return;
            }

            var summary = new System.Text.StringBuilder();
            summary.AppendLine("Registry Configuration Summary:");
            summary.AppendLine();

            if (registryKeys.Count > 0)
            {
                summary.AppendLine($"Registry Keys ({registryKeys.Count}):");
                foreach (var key in registryKeys)
                {
                    summary.AppendLine($"  • {key.RootKey}\\{key.KeyPath}");
                }
                summary.AppendLine();
            }

            if (registryValues.Count > 0)
            {
                summary.AppendLine($"Registry Values ({registryValues.Count}):");
                foreach (var value in registryValues)
                {
                    summary.AppendLine($"  • {value.Name} ({value.Type}) = {value.Data}");
                }
                summary.AppendLine();
            }

            summary.AppendLine("Options:");
            summary.AppendLine($"  • Create Backup: {(CreateBackupCheckBox.IsChecked ?? false ? "Yes" : "No")}");
            summary.AppendLine($"  • Recursive Delete: {(RecursiveDeleteCheckBox.IsChecked ?? false ? "Yes" : "No")}");
            summary.AppendLine($"  • Force Write: {(ForceWriteCheckBox.IsChecked ?? false ? "Yes" : "No")}");

            SummaryText.Text = summary.ToString();
        }

        public RegistryConfiguration GetConfiguration()
        {
            return new RegistryConfiguration
            {
                Keys = registryKeys.ToList(),
                Values = registryValues.ToList(),
                CreateBackup = CreateBackupCheckBox.IsChecked ?? false,
                RecursiveDelete = RecursiveDeleteCheckBox.IsChecked ?? false,
                ForceWrite = ForceWriteCheckBox.IsChecked ?? false
            };
        }

        public void LoadConfiguration(RegistryConfiguration config)
        {
            if (config == null) return;

            registryKeys.Clear();
            registryValues.Clear();

            foreach (var key in config.Keys)
            {
                registryKeys.Add(key);
            }

            foreach (var value in config.Values)
            {
                registryValues.Add(value);
            }

            CreateBackupCheckBox.IsChecked = config.CreateBackup;
            RecursiveDeleteCheckBox.IsChecked = config.RecursiveDelete;
            ForceWriteCheckBox.IsChecked = config.ForceWrite;

            UpdateCounts();
            UpdateSummary();
        }
    }

    public class RegistryKeyItem
    {
        public string KeyPath { get; set; } = "";
        public string RootKey { get; set; } = "HKLM";
    }

    public class RegistryValueItem
    {
        public string Name { get; set; } = "";
        public string Data { get; set; } = "";
        public string Type { get; set; } = "REG_SZ";
    }

    public class RegistryConfiguration
    {
        public List<RegistryKeyItem> Keys { get; set; } = new List<RegistryKeyItem>();
        public List<RegistryValueItem> Values { get; set; } = new List<RegistryValueItem>();
        public bool CreateBackup { get; set; }
        public bool RecursiveDelete { get; set; }
        public bool ForceWrite { get; set; }
    }
}
