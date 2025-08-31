# CommInstall - Modern Installer Builder

[![.NET Framework](https://img.shields.io/badge/.NET%20Framework-4.6.2-blue.svg)](https://dotnet.microsoft.com/download/dotnet-framework)
[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)
[![Platform](https://img.shields.io/badge/Platform-Windows-blue.svg)](https://www.microsoft.com/windows)

[🇹🇷 Türkçe](README_TR.md) | [🇺🇸 English](README.md)

---

## 🚀 Overview

CommInstall is a modern, feature-rich installer builder for Windows applications. Built with WPF and .NET Framework 4.6.2, it provides an intuitive interface for creating professional installers with advanced features.

## ✨ Features

### 🔧 Core Features
- **Modern WPF Interface** - Beautiful, animated UI with dark/light theme support
- **Modular Design** - Configurable installer modules for different needs
- **Multi-language Support** - English and Turkish with community contribution support
- **Project Management** - Create, save, and load installer projects
- **Real-time Preview** - See changes instantly as you configure

### 📦 Installer Modules

| Module | Description | Status |
|--------|-------------|---------|
| 📁 **File Installation** | Install files and folders with advanced options | ✅ Active |
| 🔧 **Registry Keys** | Configure Windows registry modifications | ✅ Active |
| 🔗 **File Extensions** | Associate file types with your application | ✅ Active |
| 📌 **Shortcuts** | Create desktop and start menu shortcuts | ✅ Active |
| 📄 **EULA/License** | Display and manage license agreements | ✅ Active |
| 🗑️ **Uninstall Support** | Configure uninstallation options | ✅ Active |
| 🚀 **Auto-Start** | Set up application auto-start options | ✅ Active |

### 🎨 UI Features
- **Dark/Light Theme** - Switch between themes with settings persistence
- **Custom Title Bar** - Modern, rounded window design
- **Card-based Layout** - Clean, organized module configuration
- **Responsive Design** - Adapts to different screen sizes
- **Smooth Animations** - Professional user experience

## 🛠️ Installation

### Prerequisites
- Windows 10/11 (x64)
- .NET Framework 4.6.2 or later
- Visual Studio 2022 (for development)

### Download
1. Go to [Releases](https://github.com/candestan/CommInstall/releases)
2. Download the latest `CommInstallBuilder.exe`
3. Run the executable

### Build from Source
```bash
git clone https://github.com/candestan/CommInstall.git
cd CommInstall
# Open CommInstall.sln in Visual Studio
# Build and run
```

## 🚀 Quick Start

### 1. Launch Application
- Run `CommInstallBuilder.exe`
- Wait for splash screen to complete
- Project wizard will open

### 2. Create New Project
- Click "Create New Project"
- Enter project details
- Click "Create Project"

### 3. Configure Modules
- Select modules from left panel
- Configure each module's settings
- See real-time preview and summary

### 4. Build Installer
- Click "Build Installer" button
- Choose output location
- Generate your installer

## 📁 Project Structure

```
CommInstall/
├── CommInstallBuilder/          # Main WPF application
│   ├── Pages/Modules/          # Installer modules
│   │   ├── FileModule/         # File installation
│   │   ├── RegistryModule/     # Registry configuration
│   │   ├── ExtensionModule/    # File associations
│   │   ├── ShortcutModule/     # Shortcut creation
│   │   ├── EulaModule/         # License management
│   │   ├── UninstallModule/    # Uninstall options
│   │   └── AutoStartModule/    # Auto-start configuration
│   ├── App.xaml                # Application entry point
│   ├── MainWindow.xaml         # Main application window
│   ├── ProjectWizard.xaml      # Project creation wizard
│   └── SettingsWindow.xaml     # Application settings
├── CommInstallStub/            # Installer stub application
├── Localization/               # Language files
│   └── Languages/
│       ├── en.json             # English translations
│       └── tr.json             # Turkish translations
└── LICENSE                     # MIT License
```

## 🔧 Configuration

### Language Settings
- Navigate to Settings → Language
- Choose between English and Turkish
- Changes apply immediately
- Settings are saved automatically

### Theme Settings
- Navigate to Settings → Theme
- Switch between Dark and Light themes
- Purple accent color with gray backgrounds
- Modern, professional appearance

### Module Configuration
Each module provides:
- **Real-time Summary** - See configuration overview
- **Advanced Options** - Fine-tune installer behavior
- **File Browsing** - Select files and folders
- **Validation** - Ensure proper configuration

## 🌐 Localization

### Supported Languages
- 🇺🇸 **English** - Default language
- 🇹🇷 **Turkish** - Full translation support

### Adding New Languages
1. Fork the repository
2. Create new language file in `Localization/Languages/`
3. Follow JSON structure from existing files
4. Submit pull request

### Language File Structure
```json
{
  "Common": {
    "Save": "Save",
    "Cancel": "Cancel"
  },
  "MainWindow": {
    "Title": "CommInstall Builder"
  }
}
```

## 🎯 Use Cases

### Software Developers
- Create professional installers for applications
- Configure file associations and registry keys
- Set up auto-start and shortcut options

### System Administrators
- Deploy applications across networks
- Standardize installation procedures
- Manage application configurations

### End Users
- Simple, guided installation process
- Clear license agreements
- Easy uninstallation

## 🚧 Development

### Building
```bash
# Debug build
msbuild CommInstall.sln /p:Configuration=Debug

# Release build
msbuild CommInstall.sln /p:Configuration=Release
```

### Dependencies
- **Newtonsoft.Json** - JSON parsing for language files
- **System.Windows.Forms** - File dialogs and system integration
- **WPF** - User interface framework

### Architecture
- **MVVM Pattern** - Clean separation of concerns
- **Modular Design** - Easy to extend with new modules
- **Event-driven** - Responsive user interface
- **Resource Management** - Efficient theme and language switching

## 🤝 Contributing

We welcome contributions! Here's how you can help:

### 🐛 Report Bugs
1. Check existing issues
2. Create new issue with detailed description
3. Include steps to reproduce
4. Attach error logs if available

### 💡 Suggest Features
1. Open feature request issue
2. Describe the use case
3. Provide examples if possible

### 🔧 Submit Code
1. Fork the repository
2. Create feature branch
3. Make your changes
4. Add tests if applicable
5. Submit pull request

### 🌐 Add Translations
1. Create language file
2. Translate all strings
3. Test in application
4. Submit pull request

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🙏 Acknowledgments

- **WPF Community** - UI framework and best practices
- **Newtonsoft.Json** - JSON parsing library
- **Contributors** - Everyone who helps improve CommInstall

## 📞 Support

- **GitHub Issues** - [Report bugs and request features](https://github.com/candestan/CommInstall/issues)
- **Discussions** - [Join community discussions](https://github.com/candestan/CommInstall/discussions)
- **Wiki** - [Documentation and guides](https://github.com/candestan/CommInstall/wiki)

## 🔄 Changelog

### Version 1.0.0
- ✅ Initial release
- ✅ All core modules implemented
- ✅ Dark/light theme support
- ✅ English and Turkish localization
- ✅ Modern WPF interface
- ✅ Project management system

---

**Made with ❤️ by the CommInstall Team**

[⬆️ Back to Top](#comminstall---modern-installer-builder)
