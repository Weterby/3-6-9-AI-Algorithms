using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Three_Six_Nine
{
    class Minimax : Algorithm
    {
        public Minimax(bool isMaximizing, int depth)
        {
            this.depth = depth;
            this.isMaximizing = isMaximizing;
        }

        public override int CalculateMove()
        {
            return 0;
        }
    }
}
