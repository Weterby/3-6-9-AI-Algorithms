using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Three_Six_Nine
{
    class AbNegamaxBot : Algorithm
    {
        private int depth;
        private bool neg;
        public AbNegamaxBot(Board board, int depth) : base(board)
        {
            this.depth = depth;

            if (depth % 2 == 1) neg = true;
            else neg = false;
        }
        public override int MakeMove()
        {
            int bestScore = -999;
            int move = -1;
            int[] gameState = board.BoardTable;
            List<int> indexes = board.GetAllEmptyCellsIndexes(gameState);
            foreach (int index in indexes)
            {
                gameState[index] = 1;
                int score = -AbNegamax(gameState, depth - 1, index, neg);
                gameState[index] = 0;
                //Console.Write("[" + index + "]: " + score + " ");
                if (score > bestScore)
                {
                    bestScore = score;
                    move = index;
                }
            }
            //Console.WriteLine();
            //Console.WriteLine("best: " + bestScore + ", index: " + move);
            return move;
        }
       
       
        private int AbNegamax(int[] gameState, int depth, int lastIndex, bool neg, int alpha=-999, int beta=999)
        {
            List<int> indexes = board.GetAllEmptyCellsIndexes(gameState);
            if (indexes.Count == 0 || depth == 0)
            {
                int value = EvaluateMove(gameState, lastIndex);
                return neg ? -value : value;
            }
            foreach (int index in indexes)
            {
                gameState[index] = 1;
                alpha = Math.Max(alpha, -AbNegamax(gameState, depth - 1, index, !neg, alpha, beta));
                gameState[index] = 0;
                if (alpha >= beta) return beta;
            }
            return alpha;
        }
    
    }
}
