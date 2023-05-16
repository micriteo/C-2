using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace C_2Game_Enemy_Test2
{
    public class Tower
    {
        private int health { get; set; }
        private Vector2 position { get; set; }

        private UIElement placeHolder { get; set; }

        public Tower(int health, Vector2 position)
        {
            this.health = health;
            this.position = position;
            createPlaceHolder();
        }

        public UIElement PlaceHolder
        {
            get { return this.placeHolder; }
            private set { this.placeHolder = value; }
        }

        public int Health
        { 
            get { return this.health; }
            private set { this.health = value; }
        }

        public Vector2 Position
        {
            get { return this.position; }
            private set { this.position = value; }
        }

        public void TakeDamage(int damage)
        {
            this.health -= damage;
            if (this.health <= 0)
            {
                this.health = 0;
                // Perform any other actions when the tower is destroyed
            }
        }

        public void createPlaceHolder()
        {
           this.placeHolder = new Rectangle
            {
                Width = 30,
                Height = 30,
                Fill = new SolidColorBrush(Colors.White),
                Stroke = new SolidColorBrush(Colors.AntiqueWhite),
                StrokeThickness = 2
            };

            Canvas.SetLeft(this.placeHolder, this.position.X);
            Canvas.SetTop(this.placeHolder, this.position.Y);
        }
    }
}
