using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiStandOff.Model
{
    [Serializable]
    public class SerializableUnit
    {
        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public int Range { get; set; }
        public int Damage { get; set; }
        public int FireRate { get; set; }
        public float SizeX { get; set; }
        public float SizeY { get; set; }
        public int Cost { get; set; }
        public float HitBoxPosX { get; set; }
        public float HitBoxPosY { get; set; }
    }

}
