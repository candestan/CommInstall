using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media;

namespace CommInstallBuilder
{
    public partial class SplashScreen : Window
    {
        public SplashScreen()
        {
            InitializeComponent();
            StartLoadingAnimation();
        }

        private void StartLoadingAnimation()
        {
            // Create and start the rotating animation
            var animation = new DoubleAnimation
            {
                From = 0,
                To = 360,
                Duration = TimeSpan.FromSeconds(1),
                RepeatBehavior = RepeatBehavior.Forever
            };

            var rotateTransform = LoadingRing.RenderTransform as RotateTransform;
            if (rotateTransform != null)
            {
                rotateTransform.BeginAnimation(RotateTransform.AngleProperty, animation);
            }
            
            // Simulate loading process
            SimulateLoading();
        }

        private async void SimulateLoading()
        {
            try
            {
                // Simulate loading steps
                await Task.Delay(500);
                UpdateLoadingStatus("Loading modules...", "Initializing core components");
                
                await Task.Delay(800);
                UpdateLoadingStatus("Loading modules...", "Loading file management system");
                
                await Task.Delay(600);
                UpdateLoadingStatus("Loading modules...", "Loading registry configuration");
                
                await Task.Delay(700);
                UpdateLoadingStatus("Loading modules...", "Loading extension handlers");
                
                await Task.Delay(500);
                UpdateLoadingStatus("Loading modules...", "Loading shortcut management");
                
                await Task.Delay(600);
                UpdateLoadingStatus("Loading modules...", "Loading EULA system");
                
                await Task.Delay(500);
                UpdateLoadingStatus("Loading modules...", "Loading uninstall tools");
                
                await Task.Delay(400);
                UpdateLoadingStatus("Loading modules...", "Loading auto-start configuration");
                
                await Task.Delay(300);
                UpdateLoadingStatus("Loading modules...", "Finalizing initialization");
                
                await Task.Delay(200);
                UpdateLoadingStatus("Ready!", "All modules loaded successfully");
                
                // Wait a bit more then close
                await Task.Delay(500);
                
                // Close splash screen (ProjectWizard will be shown by App.xaml.cs)
                System.Diagnostics.Debug.WriteLine("SplashScreen: Closing window...");
                this.Close();
                System.Diagnostics.Debug.WriteLine("SplashScreen: Window closed.");
            }
            catch (Exception ex)
            {
                UpdateLoadingStatus("Error!", $"Failed to load: {ex.Message}");
                await Task.Delay(2000);
                
                // Close splash screen even if there was an error
                System.Diagnostics.Debug.WriteLine("SplashScreen: Closing window due to error...");
                this.Close();
                System.Diagnostics.Debug.WriteLine("SplashScreen: Window closed due to error.");
            }
        }

        private void UpdateLoadingStatus(string loadingText, string statusText)
        {
            Dispatcher.Invoke(() =>
            {
                LoadingText.Text = loadingText;
                StatusText.Text = statusText;
            });
        }
    }
}
