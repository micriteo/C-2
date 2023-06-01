﻿using System.Numerics;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Foundation;
using Microsoft.UI.Xaml.Media.Imaging;
using C_2Game_Enemy_Test2;
using Microsoft.UI.Xaml;
using System.Diagnostics;

namespace Samurai_Standoff
{
    internal class Unit
    {
        public Vector2 Position { get; set; }
        public int Range { get; set; }
        public int Damage { get; set; }
        public int FireRate { get; set; }
        public Vector2 Size { get; set; }
        public Enemy TargetedEnemy { get; set; }
        public int Cost { get; set; }
        public Image Image { get; set; }
        public Rect hitBox { get; set; }
        public Vector2 hitBoxPos { get; set; }

        private bool attackingEnemy = false;

        //Constructors
        public Unit(Vector2 position, int range, int damage, int fireRate, Image image)
        {
            Position = position;
            Range = range;
            Damage = damage;
            FireRate = fireRate;
            Image = image;

            //increase size of unit by its range
            Rect newRect = new();
            newRect.Width = (image.Width) + (range * 2);
            newRect.Height = (image.Height) + (range * 2);
            hitBox = newRect;

            //calculate the posiiton of the square
            Vector2 newPosition = new Vector2();
            newPosition.X = Position.X - range;
            newPosition.Y = Position.Y - range;
            hitBoxPos = newPosition;
        }

        //Methods

        public async Task FindOrAttackTarget(List<Enemy> enemyList, Canvas window)
        {
            foreach (var enemy in enemyList)
            {
                //calculate the enemy in range and set them as enemy dont remove until they are
                //dead or out of range
                if(enemy == null ||
                    ((enemy.Position.X) >= (hitBoxPos.X) && 
                    (enemy.Position.X) <= (hitBoxPos.X + hitBox.Width)) &&
                    (enemy.Position.Y) >= (hitBoxPos.Y) &&
                    (enemy.Position.Y <= (hitBoxPos.Y + hitBox.Height))
                    )
                {
                    TargetedEnemy = enemy;
                    break;
                }
            }

            if (TargetedEnemy != null)
            {
                if(attackingEnemy == false)
                {
                    attackingEnemy = true;
                    Debug.WriteLine("Unit Attack");

                    Image.Source = new BitmapImage(new Uri(@"ms-appx:///Assets/teo_attack.gif"));

                    await Task.Delay(1300);

                    //attack target, remove their health, check if health is less than 0 if so remove target
                    Attack();

                    //finish the attack animation 
                    await Task.Delay(300);
                    Image.Source = new BitmapImage(new Uri(@"ms-appx:///Assets/teo_idle.png"));

                    if (TargetedEnemy != null)
                    {
                        if (TargetedEnemy.Health <= 0)
                        {
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
