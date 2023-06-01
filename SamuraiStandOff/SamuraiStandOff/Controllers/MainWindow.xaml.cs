using C_2Game_Enemy_Test2;
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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.


namespace SamuraiStandOff.Controllers
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Page
    {
        private Castle castle;
        private MediaPlayer media;
        
        
        public MainWindow()
        {
            this.InitializeComponent();

            //Set the fixed resolution for the application window
            ApplicationView.PreferredLaunchViewSize = new Size(800, 600);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;

            //Set the minimum size of the window to be the same as the maximum size
            media = new MediaPlayer();
            media.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/Audio/X2Download.app - Monster Hunter Rise - Main Menu Theme (128 kbps).mp3"));
            media.Play();

            
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

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            healthIndicator.Visibility = Visibility.Collapsed;
            baseTower.Visibility = Visibility.Collapsed;
        }
    }
}

