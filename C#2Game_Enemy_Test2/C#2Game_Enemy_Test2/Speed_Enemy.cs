﻿using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace C_2Game_Enemy_Test2
{
    public class Speed_Enemy : EnemyV2
    {
        public Speed_Enemy( Vector2 position, Path path) : base(100, 50.0, position, path, 20, 2.0, 0.5, 50.0)
        {
        }

        public override UIElement CreatePlaceholder()
        {
            // Create a new Polygon instance
            Polygon placeholder = new Polygon()
            {
                // Set the points to form a triangle
                Points = new PointCollection()
            {
                new Point(0, 0),
                new Point(20, 0),
                new Point(10, 20),
            },

                // Set the color of the polygon
                Fill = new SolidColorBrush(Colors.Green), // Green color for Ranged Enemy

                // Set the position of the polygon
                Margin = new Thickness(Position.X, Position.Y, 0, 0),
            };

            return placeholder;
        }
    }
}
