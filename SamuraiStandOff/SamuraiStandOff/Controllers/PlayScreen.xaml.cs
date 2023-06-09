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
using System.Numerics;
using System.Threading.Tasks;
using SamuraiStandoff;
using SamuraiStandOff.Model;
using Windows.Storage;
using Newtonsoft.Json;
using Microsoft.UI.Xaml.Markup;
using Windows.Media.Playback;
using Windows.Media.Core;




// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SamuraiStandOff.Controllers
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PlayScreen : Page
    {
        private MediaPlayer media;
        private Castle castle;
        //lists holding the current unit and enemy data
        private List<Enemy> enemies = new();
        private List<Unit> unitList = new();
        public List<Unit> UnitList
        {
            get { return unitList; }
            private set { unitList = value; }
        }
        public static PlayScreen Current { get; private set; }
        private DispatcherTimer gameLoopTimer;
        SpawnEnemy enemySpawner = new();
        private Money money;
        private int WaveCount = 1;
        private bool GameOver;
        public Window scrollWin;
        int turn = 0;

        public bool IsGamePaused { get; set; } = false;

        public PlayScreen()
        {
            money = new Money(Constants.startBalance);
            Current = this;
            InitializeComponent();
            GameOver = false;
            castle = new Castle(300);

            //spawn stuff
            Task task = SpawnEnemies(MainCanvas);

            //Assign starting money balance
            money.Currency = Constants.startBalance;
            moneyTextBlock.Text = money.Currency.ToString();

            media = new MediaPlayer();
            media.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/Audio/naruto_8bit.mp3"));
            media.IsLoopingEnabled = true;
            media.Play();


            //Create unit panel buttons and attach methods to XAML ui elements
            string meleeName = "Melee - " + Constants.meleePrice.ToString();
            string rangeName = "Archer - " + Constants.rangePrice.ToString();
            Button button1 = new Button() 
            {
                Content = meleeName,
                Width = 150,
                Height = 50,
                Background = new ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Images/buttonBackground.png")),
                    Stretch = Stretch.Fill
                },
               
                FontSize = 14, //Pixel font size
            };
            button1.AddHandler(UIElement.PointerPressedEvent, new PointerEventHandler(Button_PointerPressed), true);
            button1.AddHandler(UIElement.PointerMovedEvent, new PointerEventHandler(Button_PointerMoved), true);
            button1.AddHandler(UIElement.PointerReleasedEvent, new PointerEventHandler(Button_PointerReleased_Melee), true);

            Button button2 = new Button() 
            { 
                Content = rangeName,
                Width = 150,
                Height = 50,
                Background = new ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Images/buttonBackground.png")),
                    Stretch = Stretch.Fill
                },

                FontSize = 14, // Pixel-like font size
            };
            button2.AddHandler(UIElement.PointerPressedEvent, new PointerEventHandler(Button_PointerPressed), true);
            button2.AddHandler(UIElement.PointerMovedEvent, new PointerEventHandler(Button_PointerMoved), true);
            button2.AddHandler(UIElement.PointerReleasedEvent, new PointerEventHandler(Button_PointerReleased_Archer), true);

            buttonPanel.Children.Add(button1);
            buttonPanel.Children.Add(button2);

            //game logic timer
            //start a clock that runs a method every 100 miliseconds
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(100);
            //enemy move function
            timer.Tick += UnitAttackAsync;
            timer.Start();

            // Start the game loop timer
            gameLoopTimer = new DispatcherTimer();
            gameLoopTimer.Tick += GameLoopTimer_Tick;
            gameLoopTimer.Interval = TimeSpan.FromMilliseconds(16); // Update at approximately 60 FPS
            gameLoopTimer.Start();
        }
        public async Task SpawnEnemies(Canvas gameCanvas)
        {
            enemies = enemySpawner.CreateWave(WaveCount);
            int counter = 0;
            foreach (var enemy in enemies.ToList())
            {
                gameCanvas.Children.Add(enemy.SetUpEnemy());
                counter++;
                await Task.Delay(700);
                Debug.WriteLine("Enemy " + counter + " spawned");
            }
        }

        //event handler for units attack
        private async void UnitAttackAsync(object sender, object e)
        {
            if (enemies.Count > 0)
            {
                foreach (Unit unit in unitList.ToList())
                {
                    await unit.FindOrAttackTarget(enemies, MainCanvas, this);
                }
            }
        }

        private void UpdateHealthIndicator()
        {
            if (castle.Health > 0) //only update if health is above 0
            {
                //calculate ratio of green to red
                double greenRatio = (double)castle.Health / 300;
                double redRatio = 1 - greenRatio;

                //update healthIndicator's Fill property
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
            if (castle.Health <= 0)
            {
                gameOverScene();
            }
        }
        private void GameLoopTimer_Tick(object sender, object e)
        {
            //Update the game state
            UpdateGame();
        }

        private void AttackEvent_Handler(string message)
        {

        }

        private void UpdateGame()
        {
            //Debug.WriteLine(IsGamePaused);
            double delta = gameLoopTimer.Interval.TotalSeconds;

            if (enemies.Count == 0 && GameOver == false && IsGamePaused == false)
            {
                if(turn == 0)
                {
                    //Show the scroll screen on wave 2 and then every 5 waves
                    if (WaveCount == 1 || WaveCount % 5 == 0)
                    {
                        TransitionToScrollPage();
                    }
                    turn = 1;
                }
                else
                {
                    WaveCount++;
                    waveCountLabel.Text = "Wave: " + WaveCount; //Update the wave count
                    Task task = SpawnEnemies(MainCanvas);
                    turn = 0;
                }
            }

            //Update enemies
            foreach (var enemy in enemies.ToList())
            {
                if (MainCanvas.Children.Contains(enemy.PlaceHolder))
                {
                    MainCanvas.Children.Remove(enemy.PlaceHolder);

                    enemy.AttackEvent += AttackEvent_Handler;
                    enemy.UpdateAttackCooldown(delta);

                    //Check if the enemy is within attack range of a tower

                    if (enemy.FindCastle(castle))
                    {
                        if (enemy.CanAttack())
                        {
                            enemy.AttackCastle(castle);
                            UpdateHealthIndicator();
                            enemy.ResetAttackCooldown();
                        }
                        enemy.Move(castle, 0); //Stop the enemy's movement
                    }
                    enemy.Move(castle, delta); //Move the enemy along the path
                    MainCanvas.Children.Add(enemy.PlaceHolder); //Add the enemy's placeholder to the canvas
                }
            }
            UnitAttackAsync(this, null);

        }


        private void gameOverScene()
        {
            GameOver = true;

            media.Pause();

            baseTower.Visibility = Visibility.Collapsed;
            healthIndicator.Visibility = Visibility.Collapsed;
            moneyPanel.Visibility = Visibility.Collapsed;

            //remove all placed units
            StackPanel parentContainer = buttonPanel;
            //Remove all unit elements from the draggable panel
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

            foreach (Enemy enemy in enemies.ToList())
            {
                MainCanvas.Children.Remove(enemy.PlaceHolder);
                enemy.PlaceHolder.Visibility = Visibility.Collapsed;
                enemies.Remove(enemy);
            }

            foreach (Unit unit in unitList.ToList())
            {
                MainCanvas.Children.Remove(unit.ImageIdle);
                unit.ImageIdle.Visibility = Visibility.Collapsed;
                unitList.Remove(unit);
            }

            //try changing the background here, before navigating
            MainCanvas.Background = null; //Clearing background
            MainCanvas.Background = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Images/gameOver.png")),
                Stretch = Stretch.Fill
            };

            playFrame.Navigate(typeof(GameOver), MainCanvas);
        }

        private void TransitionToScrollPage()
        {
            //Create the scroll window, but keep it closed
            scrollWin = new Window();
            ScrollPage page = new ScrollPage(this);
            scrollWin.Content = page;
            scrollWin.Activate();
            IsGamePaused = true;
        }

        public void closeScrollWindow()
        {
            scrollWin.Close();
        }

        public void ApplyDamageBuff(int amount)
        {
            //iterate over all units
            foreach (var unit in unitList)
            {
                //increase the unit's damage by the buff amount
                unit.Damage += amount;
            }
        }

        public void ApplyRangeBuff(int amount)
        {
            foreach (var unit in unitList)
            {
                unit.Range += amount;
            }
        }

        public void ApplyHealthDecreaseDebuff(int amount)
        {
            foreach(var unit in enemies)
            {
                unit.Health -= amount;
            }
        }

        public void ApplyDamageDebuff(int amount)
        {
            foreach(var unit in enemies)
            {
                unit.PowerLevel -= amount;
            }
        }

        public void AddMoney(int sum)
        {
            if (sum > 0)
            {
                if (money.Currency + sum >= Constants.coinCap)
                {
                    money.Currency = Constants.coinCap;

                } else
                {
                    money.Currency = money.Currency + sum;
                }
                moneyTextBlock.Text = money.Currency.ToString();
            }
        }

        public void RemoveMoney(int sum)
        {
            if (sum > 0)
            {
                money.Currency = money.Currency - sum;
                moneyTextBlock.Text = money.Currency.ToString();
            }
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

        public void Button_PointerReleased_Melee(object sender, PointerRoutedEventArgs e)
        {
            //Check whether a player can afford a unit
            if (money.Currency < Constants.meleePrice) { return; }
            //Subtract price of the unit
            RemoveMoney(Constants.meleePrice);

            Button buttonOG = (Button)sender;
            //Get location of mouse and create a vector2 object
            Vector2 posUnitMelee = new((float)e.GetCurrentPoint(MainCanvas).Position.X, (float)e.GetCurrentPoint(MainCanvas).Position.Y);
            //Get Image for Melee Unit
            Image imgMeleeIdle = GetUnitImageIdle(1);
            Image imgMeleeAttack = GetUnitImageAttack(1);
            //Calculate unit position
            double unitPosLeft = (float)e.GetCurrentPoint(MainCanvas).Position.X - imgMeleeIdle.ActualWidth / 2;
            double unitPosTop = (float)e.GetCurrentPoint(MainCanvas).Position.Y - imgMeleeIdle.ActualHeight / 2;
            //Create instance of Unit
            Unit unitTempMelee = new(new Vector2((float)unitPosLeft, (float)unitPosTop), 10, 25, 100, imgMeleeIdle, imgMeleeAttack);
            unitList.Add(unitTempMelee);
            buttonOG.ReleasePointerCapture(e.Pointer);
            //Position unit where player let go off the mouse
            MainCanvas.Children.Add(unitTempMelee.CurrentImage);
            //Offset mouse coordinates by 1/2 of the image size because Images position is defined as top left corner
            Canvas.SetLeft(unitTempMelee.ImageIdle, unitPosLeft);
            Canvas.SetTop(unitTempMelee.ImageIdle, unitPosTop);
        }

        public void Button_PointerReleased_Archer(object sender, PointerRoutedEventArgs e)
        {
            // Check whether a player can afford a unit
            if (money.Currency < Constants.rangePrice) { return; }
            // Subtract price of the unit
            RemoveMoney(Constants.rangePrice);

            Button buttonOG = (Button)sender;
            //Get location of mouse and create a vector2 object
            Vector2 posUnitRange = new((float)e.GetCurrentPoint(MainCanvas).Position.X, (float)e.GetCurrentPoint(MainCanvas).Position.Y);
            //Get Image for Melee Unit
            Image imgRangeIdle = GetUnitImageIdle(2);
            Image imgRangeAttack = GetUnitImageAttack(2);
            //calculate unit position
            double unitPosLeft = (float)e.GetCurrentPoint(MainCanvas).Position.X - imgRangeIdle.ActualWidth / 2;
            double unitPosTop = (float)e.GetCurrentPoint(MainCanvas).Position.Y - imgRangeAttack.ActualHeight / 2;
            //Create instance of Unit
            Unit unitTempRange = new(new Vector2((float)unitPosLeft, (float)unitPosTop), 80, 215, 100, imgRangeIdle, imgRangeAttack);
            unitList.Add(unitTempRange);
            buttonOG.ReleasePointerCapture(e.Pointer);
            //Position unit where player let go off the mouse
            MainCanvas.Children.Add(unitTempRange.CurrentImage);
            //Offset mouse coordinates by 1/2 of the image size because Images position is defined as top left corner
            Canvas.SetLeft(unitTempRange.ImageIdle, unitPosLeft);
            Canvas.SetTop(unitTempRange.ImageIdle, unitPosTop);
        }

        //1-melee, 2-range
        public Image GetUnitImageIdle(int unitNumber)
        {
            Image image = new Image();
            // Get appropriate image
            switch (unitNumber)
            {
                case 1:
                    image.Source = new BitmapImage(new Uri(@"ms-appx:///Assets/Images/teo_idle.png"));
                    break;
                case 2:
                    image.Source = new BitmapImage(new Uri(@"ms-appx:///Assets/Images/mathew_idle.png"));
                    break;
                default:
                    image.Source = new BitmapImage(new Uri(@"ms-appx:///Assets/teo_idle.png"));
                    break;
            }
            image.Stretch = Stretch.Uniform;
            image.Height = 150;
            image.Width = 150;
            image.HorizontalAlignment = HorizontalAlignment.Center;
            image.VerticalAlignment = VerticalAlignment.Center;
            return image;
        }

        //1-melee, 2-range
        public Image GetUnitImageAttack(int unitNumber)
        {
            Image image = new Image();
            // Get appropriate image
            switch (unitNumber)
            {
                case 1:
                    image.Source = new BitmapImage(new Uri(@"ms-appx:///Assets/Images/teo_attack.gif"));
                    break;
                case 2:
                    image.Source = new BitmapImage(new Uri(@"ms-appx:///Assets/Images/mathew_attack.gif"));
                    break;
                default:
                    image.Source = new BitmapImage(new Uri(@"ms-appx:///Assets/teo_idle.png"));
                    break;
            }
            image.Stretch = Stretch.Uniform;
            image.Height = 150;
            image.Width = 150;
            image.HorizontalAlignment = HorizontalAlignment.Center;
            image.VerticalAlignment = VerticalAlignment.Center;
            return image;
        }
    }
}

