using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using Microsoft.Win32;
using System.Xml.Serialization;

namespace CommInstallBuilder
{
    public partial class ProjectWizard : Window
    {
        private List<RecentProject> recentProjects;
        private const string RECENT_PROJECTS_FILE = "recent_projects.xml";
        private const string PROJECTS_FOLDER = "Projects";

        public ProjectWizard()
        {
            InitializeComponent();
            LoadRecentProjects();
            UpdateRecentProjectsDisplay();
        }

        private void LoadRecentProjects()
        {
            try
            {
                if (File.Exists(RECENT_PROJECTS_FILE))
                {
                    var serializer = new XmlSerializer(typeof(List<RecentProject>));
                    using (var reader = new StreamReader(RECENT_PROJECTS_FILE))
                    {
                        recentProjects = (List<RecentProject>)serializer.Deserialize(reader) ?? new List<RecentProject>();
                    }
                }
                else
                {
                    recentProjects = new List<RecentProject>();
                }
            }
            catch
            {
                recentProjects = new List<RecentProject>();
            }
        }

        private void SaveRecentProjects()
        {
            try
            {
                var serializer = new XmlSerializer(typeof(List<RecentProject>));
                using (var writer = new StreamWriter(RECENT_PROJECTS_FILE))
                {
                    serializer.Serialize(writer, recentProjects);
                }
            }
            catch { /* Ignore errors */ }
        }

        private void UpdateRecentProjectsDisplay()
        {
            if (recentProjects.Count > 0)
            {
                RecentProjectsList.ItemsSource = recentProjects;
                RecentProjectsList.Visibility = System.Windows.Visibility.Visible;
                NoRecentProjectsText.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                RecentProjectsList.Visibility = System.Windows.Visibility.Collapsed;
                NoRecentProjectsText.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void AddRecentProject(string name, string path)
        {
            var project = new RecentProject(
                name, 
                path, 
                DateTime.Now.ToString("yyyy-MM-dd HH:mm")
            );

            // Remove if already exists
            recentProjects.RemoveAll(p => p.Path == path);
            
            // Add to beginning
            recentProjects.Insert(0, project);
            
            // Keep only last 10 projects
            if (recentProjects.Count > 10)
                recentProjects = recentProjects.Take(10).ToList();
            
            SaveRecentProjects();
            UpdateRecentProjectsDisplay();
        }

        private void NewProject_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CreateNewProject();
        }

        private void NewProject_Click(object sender, RoutedEventArgs e)
        {
            CreateNewProject();
        }

        private void CreateNewProject()
        {
            try
            {
                var saveDialog = new SaveFileDialog
                {
                    Title = "Create New Project",
                    Filter = "CommInstall Project (*.cip)|*.cip|All Files (*.*)|*.*",
                    DefaultExt = "cip",
                    FileName = "NewProject.cip"
                };

                if (saveDialog.ShowDialog() == true)
                {
                    // Create project file
                    var project = new ProjectData
                    {
                        Name = Path.GetFileNameWithoutExtension(saveDialog.FileName),
                        CreatedDate = DateTime.Now,
                        LastModified = DateTime.Now,
                        Version = "1.0.0"
                    };

                    var serializer = new XmlSerializer(typeof(ProjectData));
                    using (var writer = new StreamWriter(saveDialog.FileName))
                    {
                        serializer.Serialize(writer, project);
                    }

                    // Add to recent projects
                    AddRecentProject(project.Name, saveDialog.FileName);

                    // Open main window with project
                    var mainWindow = new MainWindow();
                    mainWindow.LoadProject(saveDialog.FileName);
                    mainWindow.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating project: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OpenProject_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OpenExistingProject();
        }

        private void OpenProject_Click(object sender, RoutedEventArgs e)
        {
            OpenExistingProject();
        }

        private void OpenExistingProject()
        {
            try
            {
                var openDialog = new OpenFileDialog
                {
                    Title = "Open Project",
                    Filter = "CommInstall Project (*.cip)|*.cip|All Files (*.*)|*.*",
                    DefaultExt = "cip"
                };

                if (openDialog.ShowDialog() == true)
                {
                    // Add to recent projects
                    string projectName = Path.GetFileNameWithoutExtension(openDialog.FileName);
                    AddRecentProject(projectName, openDialog.FileName);

                    // Open main window with project
                    var mainWindow = new MainWindow();
                    mainWindow.LoadProject(openDialog.FileName);
                    mainWindow.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening project: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RecentProject_Click(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border border && border.DataContext is RecentProject project)
            {
                OpenRecentProject(project.Path);
            }
        }

        private void OpenRecentProject_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is string path)
            {
                OpenRecentProject(path);
            }
        }

        private void OpenRecentProject(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    // Update last modified
                    var project = recentProjects.FirstOrDefault(p => p.Path == path);
                    if (project != null)
                    {
                        project.LastModified = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                        SaveRecentProjects();
                        UpdateRecentProjectsDisplay();
                    }

                    // Open main window with project
                    var mainWindow = new MainWindow();
                    mainWindow.LoadProject(path);
                    mainWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Project file not found. It may have been moved or deleted.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    
                    // Remove from recent projects
                    recentProjects.RemoveAll(p => p.Path == path);
                    SaveRecentProjects();
                    UpdateRecentProjectsDisplay();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening project: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
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
            Application.Current.Shutdown();
        }
    }

    public class RecentProject
    {
        public string Name { get; set; } = "";
        public string Path { get; set; } = "";
        public string LastModified { get; set; } = "";
        
        // XML serialization için default constructor gerekli
        public RecentProject() { }
        
        public RecentProject(string name, string path, string lastModified)
        {
            Name = name;
            Path = path;
            LastModified = lastModified;
        }
    }

    public class ProjectData
    {
        public string Name { get; set; } = "";
        public DateTime CreatedDate { get; set; }
        public DateTime LastModified { get; set; }
        public string Version { get; set; } = "1.0.0";
        
        // XML serialization için basit string listesi kullanıyoruz
        public List<string> ModuleNames { get; set; } = new List<string>();
        
        // Modules dictionary'sini kaldırdık çünkü XML serialization ile uyumlu değil
        // İleride daha gelişmiş bir serialization sistemi ekleyebiliriz
    }
}
