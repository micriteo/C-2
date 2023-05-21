using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.Playback;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.


namespace SamuraiStandOff
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private Castle castle;

        public MainWindow()
        {
            this.InitializeComponent();
            castle = new Castle(20);

            var mediaPlayer = new MediaPlayer();
            mediaPlayer.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/Audio/X2Download.app - Monster Hunter Rise - Main Menu Theme (128 kbps).mp3"));
            mediaPlayer.Play();
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            backgroundImage.ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Images/PathImage.png"));
            startButton.Visibility = Visibility.Collapsed;
            copyrightText.Visibility = Visibility.Collapsed;
            shogunStandOffTextBlack1.Visibility = Visibility.Collapsed;
            shogunStandOffTextBlack2.Visibility = Visibility.Collapsed;
            shogunStandOffTextBlack3.Visibility = Visibility.Collapsed;
            shogunStandOffTextBlack4.Visibility = Visibility.Collapsed;
            shogunStandOffTextWhite.Visibility = Visibility.Collapsed;
            baseTower.Visibility = Visibility.Visible;
            healthIndicator.Visibility = Visibility.Visible;
            damageButton.Visibility = Visibility.Visible;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            healthIndicator.Visibility = Visibility.Collapsed;
            baseTower.Visibility = Visibility.Collapsed;
            damageButton.Visibility = Visibility.Collapsed;
        }


        private void UpdatebaseTowerVisibility()
        {
            if (castle.Health <= 0)
            {
                baseTower.Visibility = Visibility.Collapsed;
            }
            else
            {
                baseTower.Visibility = Visibility.Visible;
            }
        }

        private void damageButton_Click(object sender, RoutedEventArgs e)
        {
            castle.Health -= 10; // Decrease castle's health by 10 
            UpdateHealthIndicator();
        }

        private void ShowGameOverScene()
        {
            if (baseTower == null || healthIndicator == null || damageButton == null)
            {
                throw new Exception("One or more required components are null.");
            }

            baseTower.Visibility = Visibility.Collapsed;
            healthIndicator.Visibility = Visibility.Collapsed;
            damageButton.Visibility = Visibility.Collapsed;
            GameOverImage.Visibility = Visibility.Visible;

        }


        private void UpdateHealthIndicator()
        {
            if (castle.Health > 0) // only update if health is above 0
            {
                // calculate ratio of green to red
                double greenRatio = (double)castle.Health / 100;
                double redRatio = 1 - greenRatio;

                // update healthIndicator's Fill property
                healthIndicator.Fill = new LinearGradientBrush
                {
                    StartPoint = new Point(0, 0),
                    EndPoint = new Point(1, 0),
                    GradientStops = new GradientStopCollection
            {
                new GradientStop { Color = Colors.Green, Offset = greenRatio },
                new GradientStop { Color = Colors.Red, Offset = greenRatio } // start red where green ends
            }
                };
            }

            if (castle.Health <= 0)
            {
                ShowGameOverScene();
            }
        }

    }
}

