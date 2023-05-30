using System.Numerics;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Shapes;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Input;
using Windows.Foundation;
using Microsoft.UI.Xaml.Media.Imaging;
using System.Threading;

namespace Samurai_Standoff
{
    internal class Unit
    {
        public Vector2 Position { get; set; }
        public int Range { get; set; }
        public int Damage { get; set; }
        public int FireRate { get; set; }
        public Vector2 Size { get; set; }
        public Enemy Enemy { get; set; }
        public int Cost { get; set; }
        public Image Image { get; set; }
        public Rect hitBox { get; set; }
        public Vector2 hitBoxPos { get; set; }

        private bool attackingEnemy = false;

        //Constructors
        public Unit(Vector2 position, int range, int damage, int fireRate, Image image)
        {
            this.Position = position;
            this.Range = range;
            this.Damage = damage;
            this.FireRate = fireRate;
            this.Image = image;

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
                    Enemy = enemy;
                    break;
                }
            }

            if (Enemy != null)
            {
                if(attackingEnemy == false)
                {
                    attackingEnemy = true;
                    Console.WriteLine("attack");

                    Image.Source = new BitmapImage(new Uri(@"ms-appx:///Assets/teo_attack.gif"));

                    await Task.Delay(2300);

                    //attack target, remove their health, check if health is less than 0 if so remove target
                    Attack();

                    Image.Source = new BitmapImage(new Uri(@"ms-appx:///Assets/teoidle.png"));

                    if (Enemy != null)
                    {
                        if (Enemy.Health <= 0)
                        {
                            window.Children.Remove(Enemy.Image);
                            enemyList.Remove(Enemy);
                            Enemy = null;
                        }
                    }
                    attackingEnemy = false;
                }
            }
        }

        public void Attack()
        {
            if(Enemy != null)
            {
                Enemy.Health -= Damage;
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
