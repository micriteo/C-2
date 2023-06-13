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
            private MediaPlayer mediaPlayer;


        public GameOver()
        {
                this.InitializeComponent();
                this.mediaPlayer = new MediaPlayer();
                PlaySound("ms-appx:///Assets/Audio/yoooooo japanese sound  kabuki yoo.mp3");
        }

            protected override void OnNavigatedTo(NavigationEventArgs e)
            {
                base.OnNavigatedTo(e);

                //get the reference from the main canvas so we can manipulate it 
                MainCanvas = e.Parameter as Canvas;

                this.mediaPlayer.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/Audio/yoooooo japanese sound  kabuki yoo.mp3"));
                this.mediaPlayer.Play();

                //set the background image each time when you navigate to the page
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
                        //exception message
                        Console.WriteLine("Invalid URI format: " + ex.Message);

                    }
                }
            }


            private void PlaySound(string soundFilePath)
            {
                try
                {
                    mediaPlayer.Source = MediaSource.CreateFromUri(new Uri(soundFilePath));
                    mediaPlayer.Play();
                }
                catch (UriFormatException ex)
                {
                    Console.WriteLine("Invalid URI format: " + ex.Message);
                }
            }

           private void YesButton_Click(object sender, RoutedEventArgs e)
           {
             if (MainCanvas != null)
             {
                try
                {
                    MainCanvas.Background = null; //clearing background
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

                    this.mediaPlayer.Pause();
                }
                catch (UriFormatException ex)
                {
            
                }
            }

            //navigate to the new frame
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
                        MainCanvas.Background = null; 
                        deathLogo.Visibility = Visibility.Collapsed;
                        deathBlockText.Visibility = Visibility.Collapsed;
                        deathBlockTextShader.Visibility = Visibility.Collapsed;
                        continueQuestion.Visibility = Visibility.Collapsed;
                        yesButton.Visibility = Visibility.Collapsed;
                        noButton.Visibility = Visibility.Collapsed;
                        continueQuestionShader.Visibility = Visibility.Collapsed;
                        this.mediaPlayer.Pause();
                    }
                    catch (UriFormatException ex)
                    {
                        Console.WriteLine("Invalid URI format: " + ex.Message);

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
