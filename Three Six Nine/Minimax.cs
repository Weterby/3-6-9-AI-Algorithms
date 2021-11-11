using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Three_Six_Nine
{
    class Minimax : Algorithm
    {
        private int depth;
        public Minimax(Board board, int depth) : base(board)
        {
            this.depth = depth;
        }
        public override int CalculateMove()
        {
            return 0;
        }
        private int EvaluateMove() {
            return 0;
        }
    }
}
