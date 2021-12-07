
using System;

namespace Three_Six_Nine.PNS.Data
{
    class Resource
    {
        private int maxAmount;
        private int usedAmount=0;


        public Resource(int maxAmount)
        {
            this.maxAmount = maxAmount;
        }

        public bool IsAvailable()
        {
            //Console.WriteLine(usedAmount);
            return usedAmount++ < maxAmount;
        }

        public void Reset()
        {
            usedAmount = 0;
        }
    }
}
