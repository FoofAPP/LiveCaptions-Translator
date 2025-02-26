using System.Windows;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;
using LiveCaptionsTranslator.src;

namespace LiveCaptionsTranslator
{
    public partial class MainWindow : FluentWindow
    {
        private Window? subtitleWindow = null;

        private bool isLogOnlyEnabled = false;

        public MainWindow()
        {
            InitializeComponent();
            ApplicationThemeManager.ApplySystemTheme();

            Loaded += (sender, args) =>
            {
                SystemThemeWatcher.Watch(
                    this,                                   // Window class
                    WindowBackdropType.Mica,                // Background type
                    true                                    // Whether to change accents automatically
                );
            };
            Loaded += (sender, args) => RootNavigation.Navigate(typeof(CaptionPage));
        }

        void TopmostButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var symbolIcon = button?.Icon as SymbolIcon;

            if (Topmost)
            {
                Topmost = false;
                symbolIcon.Filled = false;
            }
            else
            {
                Topmost = true;
                symbolIcon.Filled = true;
            }
        }

        void OverlaySubtitleModeButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var symbolIcon = button?.Icon as SymbolIcon;

            if (subtitleWindow == null)
            {
                subtitleWindow = new SubtitleWindow();
                subtitleWindow.Show();
                symbolIcon.Filled = true;
            }
            else
            {
                subtitleWindow.Close();
                subtitleWindow = null;
                symbolIcon.Filled = false;
            }
        }

        private void LogOnly_OnClickButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var symbolIcon = button?.Icon as SymbolIcon;

            if (isLogOnlyEnabled)
            {
                App.Captions.LogOnlyFlag = false;
                symbolIcon.Filled = false;
            }
            else
            {
                App.Captions.LogOnlyFlag = true;
                symbolIcon.Filled = true;
            }
            isLogOnlyEnabled = !isLogOnlyEnabled;
        }
    }
}
