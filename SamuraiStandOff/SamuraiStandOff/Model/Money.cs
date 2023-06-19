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
    }
}
