using Microsoft.UI;
using Microsoft.UI.Xaml;
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
    public class Melee_Enemy : EnemyV2
    {
        public Melee_Enemy(Vector2 position, Path path) : base(100, 30, position, path, 10, 0.5, 30, 50)
        {
        }

        public override UIElement CreatePlaceholder()
        {
            // Create a red square as the placeholder.
            Rectangle rectangle = new Rectangle();
            rectangle.Width = 10;
            rectangle.Height = 10;
            rectangle.Fill = new SolidColorBrush(Colors.Red);
            PlaceHolder = rectangle;
            return PlaceHolder;
        }
    }
}
