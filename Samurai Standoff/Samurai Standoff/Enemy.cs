using System.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Controls;

namespace Samurai_Standoff
{
    public  class Enemy
    {
        //variables
        public int Health { get; set; }
        public int Speed { get; set; }

        public int Damage { get; set; }
        public Vector2 Position { get; set; }
        public int PathProgress { get; set; }
        public bool IsActive { get; set; }
        public Image Image { get; set; }

       

        public Enemy(int speed, Vector2 position, Image image, int health,int damage) 
        {
            this.Speed = speed;
            this.Position = position;
            this.Image = image;
            this.Health = health;
            this.Damage = damage;
        }

        //methods
        public void TakeDamage()
        {

        }

        public void Update() 
        {
            
        }

        public void Draw()
        {

        }

    }
}
