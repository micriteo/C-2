using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using Windows.Foundation;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.UI.ViewManagement;
using Microsoft.UI.Xaml.Shapes;
using System.IO;
using System.Numerics;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Linq;
using Microsoft.UI.Windowing;
using Microsoft.UI;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.


namespace SamuraiStandOff.Controllers
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Page
    {
        private MediaPlayer media;
        
        
        public MainWindow()
        {
            this.InitializeComponent();

            AppWindow m_appWindow = GetAppWindowForCurrentWindow();
            if (m_appWindow != null)
            {
                m_appWindow.SetPresenter(AppWindowPresenterKind.FullScreen);
            }
            else
            {
                Console.WriteLine("m_appWindow is null");
            }

            //Set the fixed resolution for the application window
            ApplicationView.PreferredLaunchViewSize = new Size(800, 600);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;

            //Set the minimum size of the window to be the same as the maximum size
            media = new MediaPlayer();
            media.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/Audio/X2Download.app - Monster Hunter Rise - Main Menu Theme (128 kbps).mp3"));
            media.Play();
        }

        private AppWindow GetAppWindowForCurrentWindow()
        {
            try
            {
                IntPtr hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
                WindowId myWndId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hWnd);
                AppWindow appWindow = AppWindow.GetFromWindowId(myWndId);

                return appWindow;
            }
            catch (InvalidCastException ex)
            {
               
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(typeof(PlayScreen));
            media.Source = null;
            startButton.Visibility = Visibility.Collapsed;
            copyrightText.Visibility = Visibility.Collapsed;
            shogunStandOffTextBlack1.Visibility = Visibility.Collapsed;
            shogunStandOffTextBlack2.Visibility = Visibility.Collapsed;
            shogunStandOffTextBlack3.Visibility = Visibility.Collapsed;
            shogunStandOffTextBlack4.Visibility = Visibility.Collapsed;
            shogunStandOffTextWhite.Visibility = Visibility.Collapsed;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e) { }
    }
}

