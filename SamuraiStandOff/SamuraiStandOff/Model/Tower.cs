using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using System.Numerics;

namespace SamuraiStandOff
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
            get { return placeHolder; }
            private set { placeHolder = value; }
        }

        public int Health
        {
            get { return health; }
            private set { health = value; }
        }

        public Vector2 Position
        {
            get { return position; }
            private set { position = value; }
        }

        public void TakeDamage(int damage)
        {
            health -= damage;
            if (health <= 0)
            {
                health = 0;
                // Perform any other actions when the tower is destroyed
            }
        }

        public void createPlaceHolder()
        {
            placeHolder = new Rectangle
            {
                Width = 30,
                Height = 30,
                Fill = new SolidColorBrush(Colors.White),
                Stroke = new SolidColorBrush(Colors.AntiqueWhite),
                StrokeThickness = 2
            };

            Canvas.SetLeft(placeHolder, position.X);
            Canvas.SetTop(placeHolder, position.Y);
        }
    }
}
