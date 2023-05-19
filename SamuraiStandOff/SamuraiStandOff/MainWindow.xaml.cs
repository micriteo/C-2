using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.


namespace SamuraiStandOff
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            backgroundImage.ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/PathImage.png"));
            startButton.Visibility = Visibility.Collapsed;
            copyrightText.Visibility = Visibility.Collapsed;
            shogunStandOffTextBlack1.Visibility = Visibility.Collapsed;
            shogunStandOffTextBlack2.Visibility = Visibility.Collapsed;
            shogunStandOffTextBlack3.Visibility = Visibility.Collapsed;
            shogunStandOffTextBlack4.Visibility = Visibility.Collapsed;
            shogunStandOffTextWhite.Visibility = Visibility.Collapsed;
        }
    }
}

