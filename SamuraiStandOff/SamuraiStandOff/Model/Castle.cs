using System.Numerics;

namespace SamuraiStandOff
{
    public class Castle
    {
        private int _health;

        public int Health
        {
            get
            {
                return _health;
            }
            set
            {
                if (value < 0)
                {
                    value = 0;
                }

                _health = value;
            }
        }

        public Castle(int health)
        {
            Health = health;
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;

        }

        public void Update()
        {

        }
    }
}
