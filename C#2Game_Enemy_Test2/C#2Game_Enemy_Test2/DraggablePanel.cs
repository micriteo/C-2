using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace C_2Game_Enemy_Test2
{
    public partial class DraggablePanel : UserControl {

        private Point initialPosition;

        public DraggablePanel()
        {
            //this.InitializeComponent();
            this.PointerPressed += Button_PointerPressed;
            this.PointerMoved += Button_PointerMoved;
            this.PointerReleased += Button_PointerReleased;
        }

        private void Button_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            //if (e.Pointer.PointerDeviceType == Windows.Devices.Input.PointerDeviceType.Mouse)
            if (e.Pointer.PointerDeviceType.Equals(Windows.Devices.Input.PointerDeviceType.Mouse))
            {
                initialPosition = e.GetCurrentPoint(this).Position;
                Canvas.SetZIndex((UIElement)sender, 1);
                ((Button)sender).CapturePointer(e.Pointer);
            }
        }

        private void Button_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            if (e.Pointer.PointerDeviceType.Equals(Windows.Devices.Input.PointerDeviceType.Mouse))
            {
                var currentPosition = e.GetCurrentPoint(null).Position;
                var offsetX = currentPosition.X - initialPosition.X;
                var offsetY = currentPosition.Y - initialPosition.Y;
                Canvas.SetLeft((UIElement)sender, Canvas.GetLeft((UIElement)sender) + offsetX);
                Canvas.SetTop((UIElement)sender, Canvas.GetTop((UIElement)sender) + offsetY);
                initialPosition = currentPosition;
            }
        }

        private void Button_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            if (e.Pointer.PointerDeviceType.Equals(Windows.Devices.Input.PointerDeviceType.Mouse))
            {
                ((Button)sender).ReleasePointerCapture(e.Pointer);
                Canvas.SetZIndex((UIElement)sender, 0);
            }
        }
    }
}
