using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Three_Six_Nine.PNS.Data
{
    class Resource
    {
        private int maxAmount;
        private int usedAmount;

        public bool IsAvailable()
        {
            return usedAmount++ > maxAmount;
        }

        public void Reset()
        {
            usedAmount = 0;
        }
    }
}
