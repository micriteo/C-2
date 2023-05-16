using System.Numerics;
using Microsoft.UI.Xaml;
using Microsoft.VisualBasic.FileIO;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using Microsoft.UI;
using Windows.Foundation;
using Microsoft.UI.Xaml.Controls;
using System.Collections;
using Serilog;
using System.Diagnostics;

namespace C_2Game_Enemy_Test2
{
    public class Enemy
    {
        private int health { get; set; }
        private double speed { get; set; }
        private Vector2 position { get; set; }
        private int pathProgress { get; set; }
        private bool isActive { get; set; }
        private EnemyType enemyType { get; set; }
        private int powerLevel { get; set; }

        // basic structure of an enemy class
        private UIElement placeHolder { get; set; }

        public event Action<string> AttackEvent; // Define the event

        //more advance structure

        private double attackRange { get; set; }

        private int damage { get; set; }

        private double attackTimer { get; set; } // Timer to track elapsed time since last attack

        private double attackCooldown {get;set; } // Cooldown in seconds

        public Enemy(int health, double speed, Vector2 position, EnemyType enemyType)
        {
            this.health = health;
            this.speed = speed;
            this.position = position;
            this.isActive = true;
            this.enemyType = enemyType;

            // Initialize attack timer    
            this.attackTimer = 0.0; 


            //set damage based on the enemy type

            // Set damage based on the enemy type
            this.damage = getDamageByType(enemyType);

            // Set attack range based on the enemy type
            this.attackRange = getAttackRangeByType(enemyType);

            // Create the placeholder based on the enemy type
            this.placeHolder = createPlaceholderByType(enemyType);

            this.attackCooldown = getAttackCooldownByType(enemyType);


            /* Option 1: Create a new placeholder for each enemy in the constructor

             Pros:
             -> Simple and straightforward.
             Each enemy has its own separate placeholder, so you don't need to worry about shared state.
             Cons:
             -> May be less efficient if you have a lot of enemies, since each one has its own separate UIElement.*/



            /* // Create a placeholder based on the enemy type
             this.placeHolder = enemyType switch
             {
                 EnemyType.Melee => new Ellipse
                 {
                     Width = 10,
                     Height = 10,
                     Fill = new SolidColorBrush(Colors.Red),
                 },
                 EnemyType.Ranged => new Rectangle
                 {
                     Width = 10,
                     Height = 10,
                     Fill = new SolidColorBrush(Colors.Green),
                 },
                 EnemyType.Tank => new Polygon
                 {
                     Points = new PointCollection
                     {
                         new Point(0, 0),
                         new Point(10, 0),
                         new Point(5, 10)
                     },
                     Fill = new SolidColorBrush(Colors.Blue),
                 },
                 _ => throw new ArgumentException($"Unsupported enemy type: {enemyType}")
             };*/


            // Set the position of the placeholder within the Canvas
            Canvas.SetLeft(this.placeHolder, this.position.X);
            Canvas.SetTop(this.placeHolder, position.Y);

            /*// Set the attack range based on the enemy type
            this.attackRange = enemyType switch
            {
                   EnemyType.Melee => 50,
                   EnemyType.Ranged => 40,
                   EnemyType.Tank => 30,
                   _ => 0.0 // Default attack range if no matching enemy type
            };*/
            
            /*
             * Canvas.SetLeft and Canvas.SetTop lines, these are used to set the position of a UI element within a Canvas.
             * The Canvas class is a type of layout panel in UWP and WinUI that allows you to position child elements absolutely, relative to the top-left corner of the canvas.
             * In this case, Canvas.SetLeft(enemy.Placeholder, enemy.Position.X) sets the horizontal position of the enemy's placeholder, and Canvas.SetTop(enemy.Placeholder, 
                    enemy.Position.Y) sets the vertical position. 
            
            *The position is given by the Position property of the Enemy class, which is updated in the Update method.
            *This allows the enemy's position on the screen to change as the game progresses.
            *
            */


            /* Option 2: Define placeholder templates in XAML and assign them in the constructor

             Pros:
             Potentially more efficient, especially if you have many enemies, since you're reusing the same template for each one.
             Makes it easier to update the placeholders later, as you only need to change the templates in one place.

             Cons:
             More complex, as you need to manage the templates and ensure each enemy has its own separate instance if the placeholders have state that can change.
             Requires a deep clone operation each time you create an enemy, which could negate the performance benefits if not done correctly.*/
        }

        public int PowerLevel
        {
            get { return this.powerLevel; }
            private set { this.powerLevel = value; }

        }

        public void takeDamage(int damage)
        {
            this.health -= damage;
            if (this.health <= 0)
            {
                this.isActive = false;
                this.health = 0;
            }
        }

        public double getAttackRangeByType(EnemyType enemy)
        {
            // Set the damage based on the enemy type
            return enemyType switch
            {
                EnemyType.Melee => 10,   // Set melee enemy range
                EnemyType.Ranged => 20,  // Set ranged enemy range
                EnemyType.Tank => 30,    // Set tank enemy range
                _ => throw new ArgumentException($"Unsupported enemy type: {enemyType}")
            };
        }

        public UIElement getPlaceHolder()
        {
            return this.placeHolder;
        }

        public int getDamageByType(EnemyType enemyType)
        {
            // Set the damage based on the enemy type
            return enemyType switch
            {
                EnemyType.Melee => 10,   // Set melee enemy damage
                EnemyType.Ranged => 20,  // Set ranged enemy damage
                EnemyType.Tank => 30,    // Set tank enemy damage
                _ => throw new ArgumentException($"Unsupported enemy type: {enemyType}")
            };
        }

        public UIElement createPlaceholderByType(EnemyType enemyType)
        {
            // Create the placeholder based on the enemy type
            return enemyType switch
            {
                EnemyType.Melee => new Ellipse
                {
                    Width = 10,
                    Height = 10,
                    Fill = new SolidColorBrush(Colors.Red)
                },
                EnemyType.Ranged => new Rectangle
                {
                    Width = 10,
                    Height = 10,
                    Fill = new SolidColorBrush(Colors.Green)
                },
                EnemyType.Tank => new Polygon
                {
                    Points = new PointCollection
            {
                new Point(0, 0),
                new Point(10, 0),
                new Point(5, 10)
            },
                    Fill = new SolidColorBrush(Colors.Blue)
                },
                _ => throw new ArgumentException($"Unsupported enemy type: {enemyType}")
            };
        }

       /*   
        * FindClosestTower method to find the closest tower within the attack range.
        * If a target tower is found, it then calls the AttackTower method to perform the attack.
        */
        public Tower findClosestTower(List<Tower> towers)
        {
            Tower closestTower = null;
            double closestDistance = double.MaxValue;

            foreach (var tower in towers)
            {   
                if(tower != null )
                { 
                    double distance = Vector2.Distance(this.position, tower.Position);
                    if (distance <= this.attackRange && distance < closestDistance)
                    {
                        closestTower = tower;
                        closestDistance = distance;
                    }
                }
            }
            return closestTower;
        }

        public void attackTower(Tower tower)
        {
            tower.TakeDamage(this.damage);
            Debug.WriteLine($"Enemy attacked tower for {damage} damage!");


        }

        public double getAttackCooldownByType(EnemyType enemyType)
        {
            // Set attack cooldown based on enemy type
            return enemyType switch
            {
                EnemyType.Melee => 0.5,   // Melee enemy attacks every 1 second
                EnemyType.Ranged => 2.5,  // Ranged enemy attacks every 5 seconds
                EnemyType.Tank => 5.0,   // Tank enemy attacks every 10 seconds
                _ => throw new ArgumentException($"Unsupported enemy type: {enemyType}")
            };
        }

        public void UpdateAttackCooldown(double deltaTime)
        {
            this.attackTimer += deltaTime;

            // Ensure the attack cooldown doesn't exceed the desired interval
            if (this.attackTimer > this.attackCooldown)
            {
                this.attackTimer = this.attackCooldown;
            }
        }

        public bool CanAttack()
        {
            return  this.attackTimer >= this.attackCooldown;
        }

        public void ResetAttackCooldown()
        {
            this.attackTimer = 0.0;
        }



        public void update(List<Tower> towers,double deltaTime)
        {
            // Update position based on Speed and path. Increment PathProgress.
            // If it reaches the end of the path, set IsActive to false.
            // This is a placeholder - you'll need to replace it with your actual pathfinding logic.

            List<Tower> towerToRemove = new List<Tower>();// List to store towers to be removed. This is used to avoid  

            if (towers.Count == 0)
            {
                // No towers to attack
                return;
            }

            Tower targetTower = findClosestTower(towers);
            if (targetTower != null)
            {
                /* attackTower(targetTower);
                 if (targetTower.Health <= 0)
                 {
                     towerToRemove.Add(targetTower); // Add the tower to the remove list
                 }*/

                // Update attack cooldown and timer
                UpdateAttackCooldown(deltaTime);
                if (CanAttack())
                {
                    attackTower(targetTower);
                    if (targetTower.Health <= 0)
                    {
                        towerToRemove.Add(targetTower); // Add the tower to the remove list
                    }
                    ResetAttackCooldown(); // Reset the attack cooldown after performing the attack
                }

            }

            // Remove destroyed towers
            foreach (var towersToRemove in towerToRemove)
            {
                towers.Remove(towersToRemove);
            }

        }

        public void draw()
        {
            // Draw the enemy at its current position. 
            // This will depend on the specifics of your graphics library.
        }
    }
}
