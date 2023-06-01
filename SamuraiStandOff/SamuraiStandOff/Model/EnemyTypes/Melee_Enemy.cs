using C_2Game_Enemy_Test2;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;

namespace SamuraiStandOff.Model.EnemyTypes
{
    public class Melee_Enemy : Enemy
    {
        public Melee_Enemy() : base(1, 150, 10, 0.5, 30, 1) { }

        public override UIElement CreatePlaceholder()
        {
            // Create a red square as the placeholder.
            Rectangle placeholder = new()
            {
                Width = 10,
                Height = 10,
                Fill = new SolidColorBrush(Colors.Red)
            };

            return placeholder;
        }
        public override Enemy Clone()
        {
            return new Melee_Enemy();
        }
    }
}