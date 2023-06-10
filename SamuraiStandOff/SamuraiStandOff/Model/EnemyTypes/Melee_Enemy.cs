using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Shapes;
using System;

namespace SamuraiStandOff
{
    public class Melee_Enemy : Enemy
    {
        public Melee_Enemy() : base(1, 150, 10, 0.5, 30, 1) { }

        public override UIElement CreateEnemy()
        {
            /*
            // Create a red square as the placeholder.
            Rectangle placeholder = new()
            {
                Width = 10,
                Height = 10,
                Fill = new SolidColorBrush(Colors.Red)
            };
            */

            Image img = new Image();
            img.Source = new BitmapImage(new Uri("ms-appx:///Assets/Images/dimi_run.gif"));
            img.Width = 100;
            img.Height = 100;

            return img;
        }
        public override Enemy Clone()
        {
            return new Melee_Enemy();
        }
    }
}