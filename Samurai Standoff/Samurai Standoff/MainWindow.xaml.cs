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
            Image image = new Image();
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
        private void Button_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Button button = (Button)sender;
            button.CapturePointer(e.Pointer);
        }

        private void Button_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            Button button = (Button)sender;
            if (button.PointerCaptures != null && button.PointerCaptures.Count > 0)
            {
                PointerPoint pointerPoint = e.GetCurrentPoint(null);
                Canvas.SetLeft(button, pointerPoint.Position.X - button.ActualWidth / 2);
                Canvas.SetTop(button, pointerPoint.Position.Y - button.ActualHeight / 2);
            }
        }

        private void Button_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            Button button = (Button)sender;
            button.ReleasePointerCapture(e.Pointer);
            buttonPanel.Children.Remove(button);
            MainCanvas.Children.Add(button);
            Canvas.SetLeft(button, e.GetCurrentPoint(MainCanvas).Position.X - button.ActualWidth / 2);
            Canvas.SetTop(button, e.GetCurrentPoint(MainCanvas).Position.Y - button.ActualHeight / 2);
        }

        //-----------------------------------------------------------
    }
}
