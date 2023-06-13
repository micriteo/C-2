using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiStandOff
{
    /*
     * This class is used to compile all game balancing values into one place. It allows future developers
     * to easily balance the game.
     * */
    public class Constants
    {
        public const int rangePrice = 200;
        public const int meleePrice = 100;
        public const int startBalance = 350;
        public const int bowPrice = 200;
        public const int coinCap = 500;

        // Enemy stats
        public const int enemyMeleeCost = 12;
        public const int enemySpeedCost = 6;
        public const int enemyTankCost = 25;

    }
}
