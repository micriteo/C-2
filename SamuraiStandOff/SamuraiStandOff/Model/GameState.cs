using SamuraiStandoff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiStandOff
{
    [Serializable]
    public class GameState
    {
        public List<Unit> Units { get; set; }
        public List<Enemy> Enemies { get; set; }
        public int Money{ get; set; }
        public int WaveCount { get; set; }

    }
}
 