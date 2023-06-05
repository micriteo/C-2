using SamuraiStandoff;
using SamuraiStandOff.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiStandOff
{
    [Serializable]
    public static class GameState
    {
        public static List<Unit> Units { get; set; }
        public static List<Enemy> Enemies { get; set; }
        public static Money MoneyClass { get; set; }
        public static Wave WaveClass { get; set; }
            
    }
}
 