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
        public List<Vector2> Waypoints { get; private set; }

        public Path(List<Vector2> waypoints)
        {
            this.Waypoints = waypoints;
        }

        
    }
    
}
