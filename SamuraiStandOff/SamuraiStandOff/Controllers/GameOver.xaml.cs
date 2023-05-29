using System;
using System.IO;
using Windows.Foundation;
using Windows.Media.Core;
using Windows.Media.Playback;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Media.Imaging;

namespace SamuraiStandOff.Controllers
{
    public sealed partial class GameOver : Page
    {
        private Canvas MainCanvas;

        public GameOver()
        {
            this.InitializeComponent();
            PlaySound("ms-appx:///Assets/Audio/yoooooo japanese sound  kabuki yoo.mp3");
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Get the reference to MainCanvas from the navigation parameter
            MainCanvas = e.Parameter as Canvas;

            var mediaPlayer = new MediaPlayer();
            mediaPlayer.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/Audio/yoooooo japanese sound  kabuki yoo.mp3"));
            mediaPlayer.Play();

            // Set the background image every time you navigate to this page
            if (MainCanvas != null)
            {
                try
                {
                    MainCanvas.Background = new ImageBrush
                    {
                        ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Images/gameOver.png")),
                        Stretch = Stretch.Fill
                    };
                }
                catch (UriFormatException ex)
                {
                    // Handle the exception here, you might want to log it or show a default image
                }
            }
        }


        private void PlaySound(string soundFilePath)
        {
            try
            {
                var mediaPlayer = new MediaPlayer();
                mediaPlayer.Source = MediaSource.CreateFromUri(new Uri(soundFilePath));
                mediaPlayer.Play();
            }
            catch (UriFormatException ex)
            {
                
            }
        }

       private void YesButton_Click(object sender, RoutedEventArgs e)
        {
         if (MainCanvas != null)
         {
            try
            {
                MainCanvas.Background = null; // Clearing background
                MainCanvas.Background = new ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Images/pathImage.jpg")),
                    Stretch = Stretch.Fill
                };
                deathLogo.Visibility = Visibility.Collapsed;
                deathBlockText.Visibility = Visibility.Collapsed;
                deathBlockTextShader.Visibility = Visibility.Collapsed;
                continueQuestion.Visibility = Visibility.Collapsed;
                yesButton.Visibility = Visibility.Collapsed;
                noButton.Visibility = Visibility.Collapsed;
                continueQuestionShader.Visibility = Visibility.Collapsed;
            }
            catch (UriFormatException ex)
            {
            
            }
        }

        // Navigate to the new page
        // Make sure gameOver is a valid Frame or Navigation object
        gameOver.Navigate(typeof(PlayScreen));

        deathBlockText.Visibility = Visibility.Collapsed;
        deathBlockTextShader.Visibility = Visibility.Collapsed;
        }


        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainCanvas != null)
            {
                try
                {
                    MainCanvas.Background = null; // Clearing background     
                    deathLogo.Visibility = Visibility.Collapsed;
                    deathBlockText.Visibility = Visibility.Collapsed;
                    deathBlockTextShader.Visibility = Visibility.Collapsed;
                    continueQuestion.Visibility = Visibility.Collapsed;
                    yesButton.Visibility = Visibility.Collapsed;
                    noButton.Visibility = Visibility.Collapsed;
                    continueQuestionShader.Visibility = Visibility.Collapsed;
                }
                catch (UriFormatException ex)
                {

                }

                gameOver.Navigate(typeof(MainWindow));

                deathBlockText.Visibility = Visibility.Collapsed;
                deathBlockTextShader.Visibility = Visibility.Collapsed;
                deathBlockText.Visibility = Visibility.Collapsed;
                deathBlockTextShader.Visibility = Visibility.Collapsed;
            }
        }
    }
}
