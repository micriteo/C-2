using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiStandOff.Model
{
    [Serializable]
    public class Wave
    {
        // The wave number.
        public int WaveNumber { get; set; }

        // Constructor for the wave class.
        public Wave(int waveNumber)
        {
            WaveNumber = waveNumber;
        }
    }

}
    