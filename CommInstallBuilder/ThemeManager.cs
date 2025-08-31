using System;
using System.Windows;
using System.Windows.Media;

namespace CommInstallBuilder
{
    public class ThemeManager
    {
        private static ThemeManager instance;
        private string currentTheme = "dark";
        
        public static ThemeManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new ThemeManager();
                return instance;
            }
        }
        
        private ThemeManager()
        {
            // Initialize with dark theme by default
            // Don't apply theme here, let App.xaml.cs handle it
        }
        
        public void ApplyTheme(string theme)
        {
            // Şimdilik sadece karanlık tema kullanılıyor
            currentTheme = "dark";
            ApplyDarkTheme();
            
            try
            {
                // Force UI refresh on all windows
                foreach (Window window in Application.Current.Windows)
                {
                    window.InvalidateVisual();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error applying theme: {ex.Message}");
            }
        }
        
        private void ApplyDarkTheme()
        {
            // Dark theme colors
            UpdateResource("WindowBackgroundBrush", Color.FromRgb(0x1F, 0x29, 0x37));
            UpdateResource("SurfaceBrush", Color.FromRgb(0x37, 0x41, 0x51));
            UpdateResource("CardBrush", Color.FromRgb(0x4B, 0x55, 0x63));
            UpdateResource("BorderBrush", Color.FromRgb(0x6B, 0x72, 0x80));
            UpdateResource("TextBrush", Color.FromRgb(0xF9, 0xFA, 0xFB));
            UpdateResource("TextSecondaryBrush", Color.FromRgb(0xD1, 0xD5, 0xDB));
            
            // Keep primary colors (purple)
            UpdateResource("PrimaryBrush", Color.FromRgb(0x7C, 0x3A, 0xED));
            UpdateResource("PrimaryLightBrush", Color.FromRgb(0xA7, 0x8B, 0xFA));
            UpdateResource("PrimaryDarkBrush", Color.FromRgb(0x5B, 0x21, 0xB6));
        }
        
        private void ApplyLightTheme()
        {
            // Light theme colors - more visible contrast
            UpdateResource("WindowBackgroundBrush", Color.FromRgb(0xF0, 0xF0, 0xF0));  // Light gray
            UpdateResource("SurfaceBrush", Color.FromRgb(0xFA, 0xFA, 0xFA));           // Very light gray
            UpdateResource("CardBrush", Color.FromRgb(0xE8, 0xE8, 0xE8));             // Medium light gray
            UpdateResource("BorderBrush", Color.FromRgb(0xC0, 0xC0, 0xC0));           // Medium gray
            UpdateResource("TextBrush", Color.FromRgb(0x10, 0x10, 0x10));             // Very dark text
            UpdateResource("TextSecondaryBrush", Color.FromRgb(0x40, 0x40, 0x40));    // Dark gray text
            
            // Keep primary colors (purple)
            UpdateResource("PrimaryBrush", Color.FromRgb(0x7C, 0x3A, 0xED));
            UpdateResource("PrimaryLightBrush", Color.FromRgb(0xA7, 0x8B, 0xFA));
            UpdateResource("PrimaryDarkBrush", Color.FromRgb(0x5B, 0x21, 0xB6));
        }
        
        private void ApplySystemTheme()
        {
            // Check Windows system theme
            var isDarkMode = IsWindowsDarkMode();
            if (isDarkMode)
            {
                ApplyDarkTheme();
            }
            else
            {
                ApplyLightTheme();
            }
        }
        
        private bool IsWindowsDarkMode()
        {
            try
            {
                // Simple check - in a real app you'd use Windows API
                // For now, default to dark mode
                return true;
            }
            catch
            {
                return true;
            }
        }
        
        private void UpdateResource(string resourceKey, Color color)
        {
            try
            {
                var brush = new SolidColorBrush(color);
                Application.Current.Resources[resourceKey] = brush;
            }
            catch (Exception ex)
            {
                // Log error but don't crash
                System.Diagnostics.Debug.WriteLine($"Failed to update resource {resourceKey}: {ex.Message}");
            }
        }
        
        public string CurrentTheme => currentTheme;
    }
}
