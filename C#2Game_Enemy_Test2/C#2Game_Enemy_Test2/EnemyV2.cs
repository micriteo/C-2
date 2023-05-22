using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace C_2Game_Enemy_Test2
{
    public abstract class EnemyV2 
    {

        protected int Health { get; set; }
        protected double Speed { get; set; }
        public Vector2 Position { get; set; }
        protected Path Follow_Path { get; set; }
        protected int Damage { get; set; }
        protected double AttackCooldown { get; set; }
        protected double AttackRange { get; set; }
        protected double AttackTimer { get; set; }
        protected double PowerLevel { get; set; }
        public UIElement PlaceHolder { get; set; }
        protected const float waypointThreshold = 0.5f;
        protected int currentWaypoint = 0; // Field to keep track of the current waypoint
        public event Action<string> AttackEvent;



        public EnemyV2(int health, double speed, Vector2 position, Path path, int damage, double attackCooldown, double attackRange,double powerLevel)
        {
            this.Health = health;
            this.Speed = speed;
            this.Position = position;
            this.Follow_Path = path;
            this.Damage = damage;
            this.AttackCooldown = attackCooldown;
            this.AttackRange = attackRange;
            this.PowerLevel = powerLevel;
            this.AttackTimer = 0.0;

            // Create the enemy's placeholder
            PlaceHolder = CreatePlaceholder();
            Canvas.SetLeft(this.PlaceHolder, this.Position.X);
            Canvas.SetTop(this.PlaceHolder, this.Position.Y);
        }

        /* This method is responsible for finding the closest tower within the attack range of the enemy.
         * It iterates over the list of towers and calculates the distance between the enemy and each tower. 
         * It returns the tower that is closest to the enemy.
         */
        public virtual Tower findClosestTower(List<Tower> towers)
        {
            Tower closestTower = null;
            double closestDistance = double.MaxValue;

            // Find the closest tower within attack range
            foreach (var tower in towers)
            {
                if (tower != null)
                {
                    double distance = Vector2.Distance(this.Position, tower.Position);
                    Debug.WriteLine($"Distance to tower: {distance}"); // Debug output
                    if (distance <= this.AttackRange && distance < closestDistance)
                    {
                        closestTower = tower;
                        closestDistance = distance;
                        Debug.WriteLine("Tower in range found!"); // Debug output
                        Debug.WriteLine(closestTower.Health);
                    }
                }
            }
            return closestTower;
        }

        /*
         * This method is called when the enemy attacks a tower.
         * It reduces the health of the target tower by the enemy's damage value.
         */
        public virtual void attackTower(Tower tower)
        {
            // Attack the tower and reduce its health
            tower.TakeDamage(this.Damage);
            Debug.WriteLine($"Enemy attacked tower for {Damage} damage!");
        }

        /*
         * This method is used to update the attack cooldown timer of the enemy. 
         * It takes into account the time elapsed (deltaTime) since the last update and increments the attack timer accordingly.
         */
        public virtual void UpdateAttackCooldown(double deltaTime)
        {
            // Update the attack cooldown timer
            this.AttackTimer += deltaTime;
            
            if (this.AttackTimer > this.AttackCooldown) // Ensure the attack cooldown doesn't exceed the desired interval
            {
                this.AttackTimer = this.AttackCooldown;
            }
        }

        /*
         * This method checks if the enemy is ready to perform an attack. 
         * It compares the current attack timer with the attack cooldown to determine if the enemy can attack.
         */
        public virtual bool CanAttack()
        {
            // Check if the enemy can attack based on the attack cooldown
            return this.AttackTimer >= this.AttackCooldown;
        }

        public virtual void ResetAttackCooldown()
        {
            // Reset the attack cooldown timer
            this.AttackTimer = 0.0;
        }

        /*
         * This method handles the movement of the enemy along the predefined path.
         * It checks if there is a tower within range before moving. 
         * If a tower is not in range, it calculates the direction to the next waypoint and moves the enemy in that direction based on its speed and the time elapsed (deltaTime).
         */
        public virtual void Move(List<Tower> towers, double deltaTime)
        {

            Tower towerInRange = findClosestTower(towers); //check if there is a tower in range before moving
            if (towerInRange != null)
            {
                return; //If there is a tower in range, stop moving and return
            }

            if (Vector2.Distance(this.Position, Follow_Path.Waypoints[currentWaypoint]) <= waypointThreshold) // Check if we have reached the current waypoint
            {
                currentWaypoint = (currentWaypoint + 1) % Follow_Path.Waypoints.Count; // Move to next waypoint
            }

            Vector2 direction = Vector2.Normalize(Follow_Path.Waypoints[currentWaypoint] - this.Position); // Calculate direction to next waypoint
            this.Position += direction * (float)(this.Speed * deltaTime);// Move in that direction

            // Update placeholder position on the canvas
            Canvas.SetLeft(this.PlaceHolder, this.Position.X);
            Canvas.SetTop(this.PlaceHolder, this.Position.Y);
        }

        /*
         * This method is responsible for updating the state of the enemy. It first checks if there is a tower in range.
         * If a tower is in range, it updates the attack cooldown and performs an attack if possible. 
         * If there is no tower in range, it calls the Move method to move the enemy along the path.
         */
        public virtual void update(List<Tower> towers, double deltaTime)
        {
            // Check if there is a tower in range
            Tower towerInRange = findClosestTower(towers);
            if (towerInRange != null)
            {
                UpdateAttackCooldown(deltaTime); // Update the attack cooldown

                // Attack the tower if the attack cooldown allows
                if (CanAttack())
                {
                    attackTower(towerInRange);
                    if (towerInRange.Health <= 0)
                    {
                        towers.Remove(towerInRange);
                    }
                    ResetAttackCooldown();
                }
            }
            else
            {
                // Move the enemy if no tower is in range
                Move(towers, deltaTime);
            }
        }

        /*
         * This abstract method is implemented by derived classes and is responsible for creating the UI element that represents the enemy on the game canvas. 
         * The placeholder element is used to visualize the enemy's position.
         */
        public abstract UIElement CreatePlaceholder();
    }
}
