using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiStandOff.Model
{
    public class Money
    {
        public int Currency { get; set; }

        public Money(int startingCurrency)
        {
            Currency = startingCurrency;
        }

        public void AddCurrency(int amount)
        {
            Currency += amount;
        }

        public bool SpendCurrency(int amount)
        {
            if (Currency >= amount)
            {
                Currency -= amount;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
