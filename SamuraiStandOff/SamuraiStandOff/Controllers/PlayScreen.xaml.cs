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
using Samurai_Standoff;
using System.Numerics;
using C_2Game_Enemy_Test2;
using System.Threading.Tasks;

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
        //lists holding the current unit and enemy data
        private List<Enemy> enemies = new List<Enemy>();
        private List<Unit> unitList = new List<Unit>();
        private List<Tower> towers = new();
        private DispatcherTimer gameLoopTimer;
        SpawnEnemy enemySpawner = new();
        EnemyPath path = new EnemyPath();
        public PlayScreen()
        {
            InitializeComponent();

            castle = new Castle(100);
            enemies = enemySpawner.CreateWave(1);
            path.DisplayWaypoints(MainCanvas);

            //creating the tower
            towers.Add(new Tower(200, new Vector2(90, 50)));
            towers.Add(new Tower(80, new Vector2(120, 70)));

            //spawn stuff
            SpawnTower(towers, MainCanvas);
            Task task = SpawnEnemty(enemies, MainCanvas);

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

            //game logic timer
            //start a clock that runs a method every 100 miliseconds
           /* DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(100);
            //enemy move function
            //timer.Tick += functionName;
            timer.Tick += UnitAttackAsync;
            timer.Start();*/

            // Start the game loop timer
            gameLoopTimer = new DispatcherTimer();
            gameLoopTimer.Tick += GameLoopTimer_Tick;
            gameLoopTimer.Interval = TimeSpan.FromMilliseconds(16); // Update at approximately 60 FPS
            gameLoopTimer.Start();

        }
        public static async Task SpawnEnemty(List<Enemy> enemies, Canvas gameCanvas)
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

        //event handler for units attack
        private async void UnitAttackAsync(object sender, object e)
        {
            if (enemies.Count > 0)
            {
                foreach (Unit unit in unitList)
                {
                    await unit.FindOrAttackTarget(enemies, MainCanvas);
                }
            }
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
                if (MainCanvas.Children.Contains(enemy.PlaceHolder))
                {
                    MainCanvas.Children.Remove(enemy.PlaceHolder);

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
                                MainCanvas.Children.Remove(towerInRange.PlaceHolder);
                                towers.Remove(towerInRange);
                            }
                            enemy.ResetAttackCooldown();
                        }
                        enemy.Move(towers, 0); // Stop the enemy's movement
                    }
                    enemy.Move(towers, delta); // Move the enemy along the path
                    MainCanvas.Children.Add(enemy.PlaceHolder); // Add the enemy's placeholder to the canvas
                }
            }
        }

        private void gameOverScene()
        {
            Debug.WriteLine("Entering gameOverScene");

            baseTower.Visibility = Visibility.Collapsed;
            healthIndicator.Visibility = Visibility.Collapsed;
            damageButton.Visibility = Visibility.Collapsed;

            //remove all placed units
            StackPanel parentContainer = buttonPanel; 
                                                      // Remove all unit elements from the draggable panel
            foreach (UIElement element in parentContainer.Children.ToList())
            {
                if (element is Button button)
                {
                    parentContainer.Children.Remove(button);
                }
            }

            //remove all units from canvas
            foreach (UIElement element in MainCanvas.Children.ToList())
            {
                if (element is Button button)
                {
                    MainCanvas.Children.Remove(button);
                }
            }

            //try changing the background here, before navigating
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

