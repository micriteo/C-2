using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Input;
using Microsoft.UI.Xaml.Media.Imaging;
using System.Diagnostics;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SamuraiStandOff.Controllers
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PlayScreen : Page
    {
        private Castle castle;
        public PlayScreen()
        {
            this.InitializeComponent();

            castle = new Castle(100);

            //Create unit panel buttons and attach methods to XAML ui elements
            Button button1 = new Button() { Content = "Unit 1", Width = 100, Height = 50, Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 0, 0)) };
            button1.AddHandler(UIElement.PointerPressedEvent, new PointerEventHandler(Button_PointerPressed), true);
            button1.AddHandler(UIElement.PointerMovedEvent, new PointerEventHandler(Button_PointerMoved), true);
            button1.AddHandler(UIElement.PointerReleasedEvent, new PointerEventHandler(Button_PointerReleased), true);

            Button button2 = new Button() { Content = "Unit 2", Width = 100, Height = 50, Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 0, 0)) };
            button2.AddHandler(UIElement.PointerPressedEvent, new PointerEventHandler(Button_PointerPressed), true);
            button2.AddHandler(UIElement.PointerMovedEvent, new PointerEventHandler(Button_PointerMoved), true);
            button2.AddHandler(UIElement.PointerReleasedEvent, new PointerEventHandler(Button_PointerReleased), true);

            Button button3 = new Button() { Content = "Unit 3", Width = 100, Height = 50, Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 0, 0)) };
            button3.AddHandler(UIElement.PointerPressedEvent, new PointerEventHandler(Button_PointerPressed), true);
            button3.AddHandler(UIElement.PointerMovedEvent, new PointerEventHandler(Button_PointerMoved), true);
            button3.AddHandler(UIElement.PointerReleasedEvent, new PointerEventHandler(Button_PointerReleased), true);

            buttonPanel.Children.Add(button1);
            buttonPanel.Children.Add(button2);
            buttonPanel.Children.Add(button3);

        }

        private void damageButton_Click(object sender, RoutedEventArgs e)
        {
            castle.Health -= 10;
            UpdateHealthIndicator();
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
                new GradientStop { Color = Colors.Cyan, Offset = greenRatio },
                new GradientStop { Color = Colors.Red, Offset = greenRatio } // start red where green ends
            }
                };
            }
            if(castle.Health <= 0)
            {
                gameOverScene();
            }
        }

        private void gameOverScene()
        {
            Debug.WriteLine("Entering gameOverScene");

            baseTower.Visibility = Visibility.Collapsed;
            healthIndicator.Visibility = Visibility.Collapsed;
            damageButton.Visibility = Visibility.Collapsed;

            // Remove all placed units
            StackPanel parentContainer = buttonPanel; // Get a reference to the parent container
                                                      // Remove all unit elements from the draggable panel
            foreach (UIElement element in parentContainer.Children.ToList())
            {
                if (element is Button button)
                {
                    parentContainer.Children.Remove(button);
                }
            }

            // Remove all units from canvas
            foreach (UIElement element in MainCanvas.Children.ToList())
            {
                if (element is Button button)
                {
                    MainCanvas.Children.Remove(button);
                }
            }

            // Try changing the background here, before navigating
            MainCanvas.Background = null; // Clearing background
            MainCanvas.Background = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Images/gameOver.png")),
                Stretch = Stretch.Fill
            };
            Debug.WriteLine("Background should now be GameOver image");

            playFrame.Navigate(typeof(GameOver), MainCanvas);

            Debug.WriteLine("Exiting gameOverScene");
        }




        //---------------- Event handlers for Unit panel ------------
        public void Button_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Button button = (Button)sender;
            button.CapturePointer(e.Pointer);
        }

        public void Button_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            Button button = (Button)sender;
            if (button.PointerCaptures != null && button.PointerCaptures.Count > 0)
            {
                //Update position of the dragged Button element in real time
                PointerPoint pointerPoint = e.GetCurrentPoint(null);
                Canvas.SetLeft(button, pointerPoint.Position.X - button.ActualWidth / 2);
                Canvas.SetTop(button, pointerPoint.Position.Y - button.ActualHeight / 2);
            }
        }

        public void Button_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            Button buttonOG = (Button)sender;
            //TEMP
            //TODO Create an instance of Unit and insert
            //Make a copy of object
            Button buttonInsertCopy = new Button() { Content = buttonOG.Content, Width = buttonOG.Width, Height = buttonOG.Height, Background = buttonOG.Background };
            buttonOG.ReleasePointerCapture(e.Pointer);
            //buttonPanel.Children.Remove(buttonNew);
            MainCanvas.Children.Add(buttonInsertCopy);

            //Position element where player let go off the pointer
            Canvas.SetLeft(buttonInsertCopy, e.GetCurrentPoint(MainCanvas).Position.X - buttonInsertCopy.ActualWidth / 2);
            Canvas.SetTop(buttonInsertCopy, e.GetCurrentPoint(MainCanvas).Position.Y - buttonInsertCopy.ActualHeight / 2);

        }

        //-----------------------------------------------------------

    }
}

