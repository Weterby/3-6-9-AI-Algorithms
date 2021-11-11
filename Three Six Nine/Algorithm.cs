using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Three_Six_Nine
{
    abstract class Algorithm
    {
        protected Board board;

        public Algorithm(Board board)
        {
            this.board = board;
        }
        public abstract int CalculateMove();
    }
}
