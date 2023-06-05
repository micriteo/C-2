using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using System.Collections.Generic;
using System.Numerics;

namespace SamuraiStandOff
{
    public class EnemyPath
    {
        public List<Vector2> Waypoints;

        public EnemyPath()
        {
            Waypoints = new List<Vector2>
            {
                new Vector2(1500, 250),
                new Vector2(1450, 250),
                new Vector2(1400, 270),
                new Vector2(1350, 320),
                new Vector2(1340, 350),
                new Vector2(1300, 450),
                new Vector2(1270, 520),
                new Vector2(1220, 550),
                new Vector2(1130, 570),
                new Vector2(1080, 580),
                new Vector2(1000, 570),
                new Vector2(950, 560),
                new Vector2(900, 540),

                new Vector2(800, 400),
                new Vector2(770, 300),
                new Vector2(750, 250),
                new Vector2(690, 180),

                new Vector2(540,130),
                new Vector2(480,120),
                new Vector2(420,130),

                new Vector2(330, 250),
                new Vector2(300, 350),
                new Vector2(290, 450),
                new Vector2(280, 500),
                new Vector2(250, 550),
                new Vector2(200, 600),
                 new Vector2(150, 620),

               // new Vector2(130, 465),


            };
        }

        public void DisplayWaypoints(Canvas canvas)
        {
            foreach (Vector2 waypoint in Waypoints)
            {
                Ellipse waypointEllipse = new Ellipse();
                waypointEllipse.Fill = new SolidColorBrush(Colors.Black);
                waypointEllipse.Width = 20;
                waypointEllipse.Height = 20;
                waypointEllipse.Margin = new Thickness(waypoint.X, waypoint.Y, 0, 0);

                canvas.Children.Add(waypointEllipse);
            }
        }


    }

}
