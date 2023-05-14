// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace C_2Game_Enemy_Test2
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private List<Enemy> enemies = new List<Enemy>();
        private Tower tower;
        private DispatcherTimer gameLoopTimer;


        public MainWindow()
        {
            
            this.InitializeComponent();

            enemies.Add(new Enemy(100, 1.0, new Vector2(0, 0), EnemyType.Melee));
            enemies.Add(new Enemy(200, 0.5, new Vector2(50, 50), EnemyType.Ranged));
            enemies.Add(new Enemy(300, 0.2, new Vector2(100, 115), EnemyType.Tank));

            //creating the tower
            tower = new Tower(60, new Vector2(100, 120));

            // Add the placeholders to the Canvas
            foreach (var enemy in enemies)
            {
                //Debug.WriteLine("Attack");
                gameCanvas.Children.Add(enemy.getPlaceHolder());
            }

            // Add the tower's placeholder to the gameCanvas
            gameCanvas.Children.Add(tower.PlaceHolder);

            // Start the game loop timer
            gameLoopTimer = new DispatcherTimer();
            gameLoopTimer.Tick += GameLoopTimer_Tick;
            gameLoopTimer.Interval = TimeSpan.FromMilliseconds(16); // Update at approximately 60 FPS
            gameLoopTimer.Start();
        }

        private void GameLoopTimer_Tick(object sender, object e)
        {
            // Update the game state
            UpdateGame();
        }

        private void AttackEvent_Handler(string message)
        {
            // Write the attack message to the debug output
            Debug.WriteLine(message);
        }
        private void UpdateGame() 
        {
            double delta = gameLoopTimer.Interval.TotalSeconds;

            // Update enemies
            foreach (var enemy in enemies.ToList())
            {
                enemy.AttackEvent += AttackEvent_Handler;

                enemy.update(new List<Tower> {tower },delta);
                enemy.AttackEvent -= AttackEvent_Handler;

            }
            // Remove destroyed tower
            if (tower != null)
            {
                if (tower.Health <= 0)
                {
                    gameCanvas.Children.Remove(tower.PlaceHolder);
                    tower = null;
                }
            }
        }


    }
}
