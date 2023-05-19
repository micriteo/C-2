using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Brushes;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.UI.Xaml.Shapes;

public class Castle
{
    private Rectangle _rect;
    private int _health;

    public int Health
    {
        get
        {
            return this._health;
        }
        set
        {
            if (value < 0)
            {
                value = 0;
            }

            this._health = value;
        }
    }

    public Castle(int health, Rectangle rect)
    {
        Health = health;
        _rect = rect;
    }

    public void Draw(CanvasAnimatedDrawEventArgs args)
    {
        var blue = (Microsoft.UI.Colors)typeof(Microsoft.UI.Colors).GetField("Blue").GetValue(null);
    }


    public void Update(CanvasAnimatedUpdateEventArgs args)
    {

    }
}
