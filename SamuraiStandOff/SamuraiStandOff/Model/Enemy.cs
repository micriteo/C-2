using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SamuraiStandOff;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;


namespace SamuraiStandOff
{
    [Serializable]
    public abstract class Enemy
    {
        public int Health { get; set; }
        public double Speed { get; set; }
        public Vector2 Position { get; set; }
        public EnemyPath Follow_Path { get; set; }
        public int Damage { get; set; }
        public double AttackCooldown { get; set; }
        public double AttackRange { get; set; }
        public double AttackTimer { get; set; }
        public int PowerLevel { get; set; }
        public UIElement PlaceHolder { get; set; }
        public const float waypointThreshold = 0.5f;
        public int currentWaypoint = 0; // Field to keep track of the current waypoint
        public event Action<string> AttackEvent;
        public bool hasReachedDestination = false; 

        public Enemy(int health, double speed, int damage, double attackCooldown, double attackRange, int powerLevel)
        {
            Health = health;
            Speed = speed;
            Position = new Vector2(1500, 250);
            Follow_Path = new EnemyPath();
            Damage = damage;
            AttackCooldown = attackCooldown;
            AttackRange = attackRange;
            PowerLevel = powerLevel;
            AttackTimer = 0.0;

            // Create the enemy's placeholder
            PlaceHolder = CreateEnemy();
        }

        
        public virtual bool FindCastle(Castle castle)
        {
            // Check if the enemy has reached the destination (150, 620)
            if (hasReachedDestination)
            {
                return true;
            }

            return false;
        }

        /*
         * This method is called when the enemy attacks a tower.
         * It reduces the health of the target tower by the enemy's damage value.
         */
        public virtual void AttackCastle(Castle castle)
        {
            // Attack the tower and reduce its health
            castle.TakeDamage(Damage);
        }

        /*
         * This method is used to update the attack cooldown timer of the enemy. 
         * It takes into account the time elapsed (deltaTime) since the last update and increments the attack timer accordingly.
         */
        public virtual void UpdateAttackCooldown(double deltaTime)
        {
            // Update the attack cooldown timer
            AttackTimer += deltaTime;

            if (AttackTimer > AttackCooldown) // Ensure the attack cooldown doesn't exceed the desired interval
            {
                AttackTimer = AttackCooldown;
            }
        }

        /*
         * This method checks if the enemy is ready to perform an attack. 
         * It compares the current attack timer with the attack cooldown to determine if the enemy can attack.
         */
        public virtual bool CanAttack()
        {
            // Check if the enemy can attack based on the attack cooldown
            return AttackTimer >= AttackCooldown;
        }

        public virtual void ResetAttackCooldown()
        {
            // Reset the attack cooldown timer
            AttackTimer = 0.0;
        }

        /*
         * This method handles the movement of the enemy along the predefined path.
         * It checks if there is a tower within range before moving. 
         * If a tower is not in range, it calculates the direction to the next waypoint and moves the enemy in that direction based on its speed and the time elapsed (deltaTime).
         */
        public virtual void Move(Castle castle, double deltaTime)
        {

            if (!hasReachedDestination)
            {
                Vector2 direction = Vector2.Normalize(Follow_Path.Waypoints[currentWaypoint] - Position);
                float distanceToWaypoint = Vector2.Distance(Position, Follow_Path.Waypoints[currentWaypoint]);
                float movementThisFrame = (float)(Speed * deltaTime);

                // If we're going to move beyond the waypoint this frame, 
                // set our position to the waypoint directly to avoid overshooting it
                if (movementThisFrame >= distanceToWaypoint)
                {
                    Position = Follow_Path.Waypoints[currentWaypoint];
                    currentWaypoint = (currentWaypoint + 1) % Follow_Path.Waypoints.Count;

                    if (Position == new Vector2(150, 620))
                    {
                        hasReachedDestination = true;
                    }
                }
                else
                {
                    Position += direction * movementThisFrame;
                }

                //add 25 to place in the middle of position instead of top left without affecting calculations
                Canvas.SetLeft(PlaceHolder, Position.X + 25);
                Canvas.SetTop(PlaceHolder, Position.Y + 25);
            }

            // Attack the tower if we have reached the specific position (150, 620)
            if (hasReachedDestination && CanAttack())
            {
                AttackCastle(castle);
                ResetAttackCooldown();
            }
        }

        /*
         * This method is responsible for updating the state of the enemy. It first checks if there is a tower in range.
         * If a tower is in range, it updates the attack cooldown and performs an attack if possible. 
         * If there is no tower in range, it calls the Move method to move the enemy along the path.
         */

        /*
         * This abstract method is implemented by derived classes and is responsible for creating the UI element that represents the enemy on the game canvas. 
         * The placeholder element is used to visualize the enemy's position.
         */

        public UIElement SetUpEnemy()
        {
            Canvas.SetLeft(PlaceHolder, Position.X);
            Canvas.SetTop(PlaceHolder, Position.Y);
            return PlaceHolder;
        }

        public abstract UIElement CreateEnemy();

        public abstract Enemy Clone();
    }
}
