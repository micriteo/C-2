using C_2Game_Enemy_Test2;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;

namespace SamuraiStandOff.Model.EnemyTypes
{
    public class Speed_Enemy : Enemy
    {
        public Speed_Enemy() : base(1, 175.0, 20, 0.5, 0.5, 2)
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

        public override Enemy Clone()
        {
            return new Speed_Enemy();
        }
    }
}