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
using System.Threading.Tasks;
using System.Diagnostics;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Samurai_Standoff
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public partial class MainWindow : Page
    {
        private List<Enemy> enemyList = new List<Enemy>();
        private List<Unit> _unitList = new List<Unit>();

        public List<Unit> UnitList
        {
            get { return _unitList; }
            private set { _unitList = value; }
        }

        public List<Enemy> EnemyList
        {
            get { return enemyList; }
            private set { enemyList = value; }
        }

        private DispatcherTimer timer;
        public static MainWindow Current { get; private set; }

        public MainWindow()
        {
            Current = this;            this.InitializeComponent();
            NavigateAfterDelay();
            SpawnUnit();
            SpawnEnemy();
            SpawnEnemy();

            //start a clock that runs a method every 100 miliseconds
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += MoveEnemy;
            timer.Tick += UnitAttackAsync;
            timer.Start();
        }

        //Event handler for unit attacks
        private async void UnitAttackAsync(object sender, object e)
        {
            if(enemyList.Count > 0)
            {
                foreach (Unit unit in _unitList)
                {
                    await unit.FindOrAttackTarget(enemyList, MainCanvas);
                }
            }
        }

        //Event handler for the Tick event
        private void MoveEnemy(object sender, object e)
        {
            if(enemyList.Count > 0)
            {
                var enemy = enemyList[^1];
                enemy.Position = new Vector2(enemy.Position.X - enemy.Speed, enemy.Position.Y);
                Canvas.SetLeft(enemy.Image, enemy.Position.X);
            }
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
            Vector2 pos = new(1300, 300);
            Enemy enemy = new Enemy(4, pos, image, 100,100);
            enemyList.Add(enemy);
            MainCanvas.Children.Add(enemyList[enemyList.Count() - 1].Image);
            Canvas.SetLeft(image, pos.X);
            Canvas.SetTop(image, pos.Y);
        }

        public void SpawnUnit()
        {
            //unit image creation
            Image image = new Image();
            image.Source = new BitmapImage(new Uri(@"ms-appx:///Assets/teoidle.png"));
            image.Stretch = Stretch.Uniform;
            image.Height = 150;
            image.Width = 150;
            image.HorizontalAlignment = HorizontalAlignment.Center;
            image.VerticalAlignment = VerticalAlignment.Center;

            //unit object creation
            Vector2 pos = new(700, 175);
            Unit unit = new(pos, 30, 25, 100, image);
            _unitList.Add(unit);
            MainCanvas.Children.Add(unit.Image);
            Canvas.SetLeft(unit.Image, pos.X);
            Canvas.SetTop(unit.Image, pos.Y);
        }

        private async void NavigateAfterDelay()
        {
            await Task.Delay(TimeSpan.FromSeconds(5)); // Wait for 5 seconds
            MainCanvas.Visibility = Visibility.Collapsed; // Hide the main canvas
            //navigate to scroll page 
            Frame.Navigate(typeof(ScrollPage));

        }
    }
}
