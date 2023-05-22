using ABI.System.Numerics;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samurai_Standoff
{
    internal class Unit
    {
        public Vector2 Position { get; set; }
        public int Range { get; set; }
        public int Damage { get; set; }
        public int FireRate { get; set; }
        public Vector2 Size { get; set; }
        public int Enemy { get; set; }
        public int Cost { get; set; }
        public Image image { get; set; }

        //Constructors
        Unit(Vector2 position, int range, int damage, int fireRate, Vector2 size, int enemy, int cost)
        {
            this.Position = position;
            this.Range = range;
            this.Damage = damage;
            this.FireRate = fireRate;
            this.Size = size;
            this.Enemy = enemy;
            this.Cost = cost;
        }


        //Methods
        public void FindTarget()
        {

        }

        public void Attack()
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
