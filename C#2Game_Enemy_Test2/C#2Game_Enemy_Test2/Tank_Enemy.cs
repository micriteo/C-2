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
    public class Tank_Enemy : EnemyV2
    {
        public Tank_Enemy() : base(100, 30, 10, 0.5, 30, 3)
        {
        }

        public override UIElement CreatePlaceholder()
        {
            Ellipse placeholder = new Ellipse() // Create a new Ellipse instance
            {
                Width = 30 ,// Set the width and height
                Height = 30,
                Fill = new SolidColorBrush(Colors.Green),// Set the color of the ellipse
                Margin = new Thickness(Position.X, Position.Y, 0, 0), // Set the position of the ellipse
            };
            return placeholder;
        }
        public override EnemyV2 Clone()
        {
            return new Tank_Enemy();
        }
    }
}
