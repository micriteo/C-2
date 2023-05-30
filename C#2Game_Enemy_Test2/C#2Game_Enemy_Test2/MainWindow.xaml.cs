// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;
using Microsoft.UI.Xaml.Controls;
using WinRT.Interop;
using Windows.UI.WindowManagement;
using Windows.UI.ViewManagement;



// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace C_2Game_Enemy_Test2
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        
        private List<Tower> towers = new ();
        private DispatcherTimer gameLoopTimer;
        private List<EnemyV2> enemies = new ();
        SpawnEnemy enemySpawner = new();
        Path path = new Path();


        public MainWindow()
        {
            InitializeComponent();

            

            enemies = enemySpawner.CreateWave(1);
            path.DisplayWaypoints(gameCanvas);

            //creating the tower
            towers.Add(new Tower(200, new Vector2(90, 50)));
            towers.Add(new Tower(80, new Vector2(120, 70)));

            //spawn stuff
            SpawnTower(towers, gameCanvas);
            Task task = SpawnEnemty(enemies, gameCanvas);

            // Start the game loop timer
            gameLoopTimer = new DispatcherTimer();
            gameLoopTimer.Tick += GameLoopTimer_Tick;
            gameLoopTimer.Interval = TimeSpan.FromMilliseconds(16); // Update at approximately 60 FPS
            gameLoopTimer.Start();
        }

        

        public static async Task SpawnEnemty(List<EnemyV2> enemies, Canvas gameCanvas)
        {
            foreach (var enemy in enemies)
            {
                gameCanvas.Children.Add(enemy.SetupPlaceholder());
                await Task.Delay(700);
            }
        }

        public static void SpawnTower(List<Tower> towers, Canvas gameCanvas)
        {
            foreach (var tower in towers)
            {
                gameCanvas.Children.Add(tower.PlaceHolder);
            }
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
            

            // Add the path to the Canvas
            /*foreach (var waypoint in waypoints)
            {
                Rectangle pathStep = new()
                {
                    Width = 10,
                    Height = 10,
                    Fill = new SolidColorBrush(Colors.Gray),
                };
                Canvas.SetLeft(pathStep, waypoint.X);
                Canvas.SetTop(pathStep, waypoint.Y);
                gameCanvas.Children.Add(pathStep);
            }*/
            double delta = gameLoopTimer.Interval.TotalSeconds;

            // Update enemies
            foreach (var enemy in enemies.ToList())
            {
                if (gameCanvas.Children.Contains(enemy.PlaceHolder))
                {
                    gameCanvas.Children.Remove(enemy.PlaceHolder);

                    enemy.AttackEvent += AttackEvent_Handler;
                    enemy.UpdateAttackCooldown(delta);

                    // Check if the enemy is within attack range of a tower
                    Tower towerInRange = enemy.FindClosestTower(towers);
                    if (towerInRange != null)
                    {
                        if (enemy.CanAttack())
                        {
                            enemy.AttackTower(towerInRange);
                            if (towerInRange.Health <= 0)
                            {
                                gameCanvas.Children.Remove(towerInRange.PlaceHolder);
                                towers.Remove(towerInRange);
                            }
                            enemy.ResetAttackCooldown();
                        }
                        enemy.Move(towers, 0); // Stop the enemy's movement
                    }
                    enemy.Move(towers, delta); // Move the enemy along the path
                    gameCanvas.Children.Add(enemy.PlaceHolder); // Add the enemy's placeholder to the canvas
                }
            }
        }
    }
}
