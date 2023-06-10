using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Shapes;
using SamuraiStandOff;
using System;

namespace SamuraiStandOf
{
    public class Speed_Enemy : Enemy
    {
        public Speed_Enemy() : base(1, 175.0, 20, 0.5, 0.5, 2)
        {
        }

        public override UIElement CreateEnemy()
        {
            /*
            // Create a red square as the placeholder.
            Rectangle placeholder = new()
            {
                Width = 10,
                Height = 10,
                Fill = new SolidColorBrush(Colors.Green)
            };
            */

            Image img = new Image();
            img.Source = new BitmapImage(new Uri("ms-appx:///Assets/Images/alin_run.gif"));
            img.Width = 100;
            img.Height = 100;

            return img;
        }

        public override Enemy Clone()
        {
            return new Speed_Enemy();
        }
    }
}