using Microsoft.UI;
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
                new Vector2(100, 0),
                new Vector2(100, 100),
                new Vector2(30, 100),
                new Vector2(50, 80),
                new Vector2(0, 100)
            };
        }

        
    }
    
}
