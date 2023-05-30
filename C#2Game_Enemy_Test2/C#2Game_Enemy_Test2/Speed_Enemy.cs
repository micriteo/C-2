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
using Windows.Foundation;

namespace C_2Game_Enemy_Test2
{
    public class Speed_Enemy : EnemyV2
    {
        public Speed_Enemy() : base(100, 175.0, 20, 2.0, 0.5, 2)
        {
        }

        public override UIElement CreatePlaceholder()
        {
            // Create a red square as the placeholder.
            Rectangle placeholder = new()
            {
                Width = 10,
                Height = 10,
                Fill = new SolidColorBrush(Colors.Green)
            };
            return placeholder;
        }

        public override EnemyV2 Clone()
        {
            return new Speed_Enemy();
        }
    }
}
