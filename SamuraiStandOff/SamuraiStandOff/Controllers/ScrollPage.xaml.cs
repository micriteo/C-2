using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SamuraiStandOff.Controllers
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ScrollPage : Page
    {
        public ScrollPage()
        {
            this.InitializeComponent();
        }

        private void DamageBuff_Click(object sender, RoutedEventArgs e)
        {
            // Apply the buff first
            PlayScreen.Current.ApplyDamageBuffToAllUnits(10);

            // Save the game state after applying the buff
            //PlayScreen.Current.SaveGameState();

            // Resume the game
            //PlayScreen.Current.ResumeGame();

            // Navigate back to the PlayScreen
            this.Frame.Navigate(typeof(PlayScreen));

            PlayScreen.Current.ShowElements();

        }


        private void FireRateBuff_Click(object sender, RoutedEventArgs e)
        {
            //foreach (Unit unit in MainWindow.Current.UnitList)
            //{
            //    Debug.WriteLine($"Unit Damage before buff: {unit.FireRate}");

            //    unit.FireRate += 10;

            //    Debug.WriteLine($"Unit Damage after buff: {unit.FireRate}");
            //}
        }

        private void Health_Decrease_Debuff(object sender, RoutedEventArgs e)
        {
            //if (MainWindow.Current.EnemyList.Count <= 0)
            //{
            //    Debug.WriteLine("No more enemy");
            //}

            //foreach (var enemy in MainWindow.Current.EnemyList)
            //{
            //    if (enemy.Health < 10)
            //    {
            //        return;
            //    }
            //    Debug.WriteLine($"Enemy Health before debuff: {enemy.Health}");

            //    enemy.Health -= 10;

            //    Debug.WriteLine($"Enemy Health after debuff: {enemy.Health}");

            //}
        }

        private void Damage_Decrease_Debuff(object sender, RoutedEventArgs e)
        {
            //if(MainWindow.Current.EnemyList.Count <= 0) 
            //{
            //    Debug.WriteLine("No more enemy");
            //}

            //foreach (var enemy in MainWindow.Current.EnemyList)
            //{
            //    if (enemy.Damage < 10)
            //    {
            //        return;
            //    }
            //    Debug.WriteLine($"Enemy Damage before debuff: {enemy.Damage}");

            //    enemy.Damage -= 10;

            //    Debug.WriteLine($"Enemy Damage after debuff: {enemy.Damage}");
            //}
        }
    }
}
