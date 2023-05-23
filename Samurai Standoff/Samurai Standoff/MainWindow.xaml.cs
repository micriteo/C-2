// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using System.Numerics;
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
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Input;
using static System.Net.Mime.MediaTypeNames;


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Samurai_Standoff
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private List<Enemy> enemyList = new List<Enemy>();

        public MainWindow()
        {
            this.InitializeComponent();
            SpawnEnemy();

            //start a clock that runs a method every 100 miliseconds
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += MoveEnemy;
            timer.Start();


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

        // Event handler for the Tick event
        private void MoveEnemy(object sender, object e)
        {
            var enemy = enemyList[^1];
            enemy.Position = new Vector2(enemy.Position.X - enemy.Speed, enemy.Position.Y);
            Canvas.SetLeft(enemy.EnemyImage, enemy.Position.X);
        }

        public void SpawnEnemy()
        {
            //enemy image creation
            Microsoft.UI.Xaml.Controls.Image image = new Microsoft.UI.Xaml.Controls.Image();
            image.Source = new BitmapImage(new Uri(@"ms-appx:///Assets/Images/comehere.png"));
            image.Stretch = Stretch.Uniform;
            image.Height = 50;
            image.HorizontalAlignment = HorizontalAlignment.Center;
            image.VerticalAlignment = VerticalAlignment.Center;
            

            //enemy object creation
            Vector2 pos = new(1450, 300);
            Enemy enemy = new Enemy(3, pos, image);
            enemyList.Add(enemy);
            MainCanvas.Children.Add(enemyList[enemyList.Count() - 1].EnemyImage);
            Canvas.SetLeft(image, pos.X);
            Canvas.SetTop(image, pos.Y);
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
