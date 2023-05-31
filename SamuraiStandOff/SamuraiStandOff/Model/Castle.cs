using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiStandOff
{
    public class Castle
    {
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

        public Castle(int health)
        {
            Health = health;
        }

        public void Draw()
        {
          
        }


        public void Update()
        {

        }
    }
}
