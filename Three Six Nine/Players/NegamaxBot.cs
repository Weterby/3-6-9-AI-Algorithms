using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Three_Six_Nine
{
    class NegamaxBot : Algorithm
    {
        private int depth;
        private bool neg;
        public NegamaxBot(Board board, int depth) : base(board)
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
                int score = -Negamax(gameState, depth-1, index, neg);
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
       
        private int Negamax(int[] gameState, int depth, int lastIndex, bool neg)
        {
            List<int> indexes = board.GetAllEmptyCellsIndexes(gameState);
            if (indexes.Count == 0 || depth == 0)
            {
                int value = EvaluateMove(gameState, lastIndex);
                return neg ? -value : value;
            }

            int bestScore = -999;
            foreach (int index in indexes)
            {
                gameState[index] = 1;
                int score = -Negamax(gameState, depth - 1, index, !neg);
                gameState[index] = 0;
                bestScore = Math.Max(score, bestScore);
            }
            return bestScore;
        }
    }
}
