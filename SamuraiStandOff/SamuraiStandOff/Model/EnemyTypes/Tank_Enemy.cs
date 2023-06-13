using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Shapes;
using System;

namespace SamuraiStandOff
{
    public class Tank_Enemy : Enemy
    {
        public Tank_Enemy() : base(200, 150, 10, 0.5, 30, 3)
        {
        }

        public override UIElement CreateEnemy()
        {
            Image img = new Image();
            img.Source = new BitmapImage(new Uri("ms-appx:///Assets/Images/miro_run.gif"));
            img.Width = 100;
            img.Height = 100;

            return img;
        }
        public override Enemy Clone()
        {
            return new Tank_Enemy();
        }
    }
}