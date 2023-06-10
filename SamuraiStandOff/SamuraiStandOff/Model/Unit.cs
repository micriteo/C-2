using System.Numerics;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Foundation;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml;
using System.Diagnostics;
using SamuraiStandOff.Controllers;
using SamuraiStandOff;
using SamuraiStandOf;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;

namespace SamuraiStandoff
{
    public class Unit
    {
        public Vector2 Position { get; set; }
        public int Range { get; set; }
        public int Damage { get; set; }
        public int FireRate { get; set; }
        public Vector2 Size { get; set; }
        public Enemy TargetedEnemy { get; set; }
        public int Cost { get; set; }
        public Image CurrentImage { get; set; }
        readonly public Image ImageIdle;
        readonly public Image ImageAttack;
        public Rect hitBox { get; set; }
        public Vector2 hitBoxPos { get; set; }

        private bool attackingEnemy = false;

        //Constructors
        public Unit(Vector2 position, int range, int damage, int fireRate, Image imageIdle, Image imageAttack)
        {
            Position = position;
            Range = range;
            Damage = damage;
            FireRate = fireRate;
            ImageIdle = imageIdle;
            ImageAttack = imageAttack;

            //set the current animation of the unit to its idle position
            CurrentImage = imageIdle;

            //increase size of unit by its range
            Rect newRect = new();
            newRect.Width = (imageIdle.Width) + (range * 2);
            newRect.Height = (imageIdle.Height) + (range * 2);
            hitBox = newRect;

            //calculate the position of the square
            Vector2 newPosition = new Vector2();
            newPosition.X = Position.X - range;
            newPosition.Y = Position.Y - range;
            hitBoxPos = newPosition;
        }

        //Methods

        public async Task FindOrAttackTarget(List<Enemy> enemyList, Canvas window, PlayScreen screen)
        {
            foreach (var enemy  in enemyList)
            {
                //calculate the enemy in range and set them as enemy dont remove until they are
                //dead or out of range
                if(enemy == null ||
                    (((enemy.Position.X) >= (hitBoxPos.X) && 
                    (enemy.Position.X) <= (hitBoxPos.X + hitBox.Width)) &&
                    (enemy.Position.Y) >= (hitBoxPos.Y) &&
                    (enemy.Position.Y <= (hitBoxPos.Y + hitBox.Height)))
                    )
                {
                    TargetedEnemy = enemy;
                    break;
                }
            }

            if (TargetedEnemy != null)
            {

                //draw hitbox over character
                //DEBUG CODE
                
                Rectangle hitboxShape = new Rectangle();
                hitboxShape.Width = hitBox.Width;
                hitboxShape.Height = hitBox.Height;
                hitboxShape.Fill = new SolidColorBrush(Windows.UI.Color.FromArgb(1, 255, 255, 255));
                window.Children.Add(hitboxShape);
                Canvas.SetLeft(hitboxShape, hitBoxPos.X);
                Canvas.SetTop(hitboxShape, hitBoxPos.Y);
                
                //if not attacking an enemy
                if (attackingEnemy == false)
                {
                    attackingEnemy = true;
                    Debug.WriteLine("Unit Attack");

                    CurrentImage.Source = ImageAttack.Source;

                    await Task.Delay(1300);

                    //attack target, remove their health, check if health is less than 0 if so remove target
                    Attack();

                    //finish the attack animation 
                    await Task.Delay(300);
                    CurrentImage.Source = ImageIdle.Source;

                    if (TargetedEnemy != null)
                    {
                        if (TargetedEnemy.Health <= 0)
                        {
                            // Add money depending on enemy class
                            if (TargetedEnemy is Melee_Enemy)
                            {
                                screen.addMoney(Constants.enemyMeleeCost);
                            }
                            if (TargetedEnemy is Speed_Enemy)
                            {
                                screen.addMoney(Constants.enemySpeedCost);
                            }
                            if (TargetedEnemy is Tank_Enemy)
                            {
                                screen.addMoney(Constants.enemyTankCost);
                            }
                            window.Children.Remove(TargetedEnemy.PlaceHolder);
                            TargetedEnemy.PlaceHolder.Visibility = Visibility.Collapsed;
                            enemyList.Remove(TargetedEnemy);
                            TargetedEnemy = null;
                        }
                    }
                    attackingEnemy = false;
                }
            }
        }

        public void Attack()
        {
            if(TargetedEnemy != null)
            {
                TargetedEnemy.Health -= Damage;
            }
        }

        public void Update()
        {

        }

        public void Draw()
        {

        }
    }
}
