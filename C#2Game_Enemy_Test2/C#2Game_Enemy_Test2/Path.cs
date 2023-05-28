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
    public class Path
    {
        public List<Vector2> Waypoints;

        public Path()
        {
            Waypoints = new List<Vector2>
            {
                new Vector2(1100, 0),
                new Vector2(1100, 170),
                new Vector2(1050, 180),
                new Vector2(1000, 195),
                new Vector2(970, 230),
                new Vector2(945, 280),
                new Vector2(920, 380),
                new Vector2(900, 420),
                new Vector2(880, 440),
                new Vector2(830, 455),
                new Vector2(780, 465),
                new Vector2(730, 465),
                new Vector2(680, 450),
                new Vector2(630, 430),
                
                new Vector2(600, 400),
                new Vector2(570, 300),

                new Vector2(520,150),
                new Vector2(480,110),
                new Vector2(420,80),

                new Vector2(330, 70),
                new Vector2(280, 80),
                new Vector2(240, 110),
                new Vector2(220, 150),

                new Vector2(200, 320),
                new Vector2(130, 465),


                new Vector2(0, 100)
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
