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
        public Tank_Enemy() : base(100, 150, 10, 0.5, 30, 3)
        {
        }

        public override UIElement CreatePlaceholder()
        {
            // Create a red square as the placeholder.
            Rectangle placeholder = new()
            {
                Width = 10,
                Height = 10,
                Fill = new SolidColorBrush(Colors.Blue)
            };
            return placeholder;
        }
        public override EnemyV2 Clone()
        {
            return new Tank_Enemy();
        }
    }
}
