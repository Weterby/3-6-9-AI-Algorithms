using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Three_Six_Nine
{
    class IterativeDeepening : Algorithm
    {
        private int depth;
        private bool neg;
        private long timePerMove = 2;
        private bool timeout;
        private Stopwatch watch;
        public IterativeDeepening(Board board, int depth) : base(board)
        {
            this.depth = depth;

            if (depth % 2 == 1) neg = true;
            else neg = false;
        }
        public override int MakeMove()
        {
            watch = new Stopwatch();

            int bestScore = -999;
            int move = -1;
            int[] gameState = board.BoardTable;
            List<int> indexes = board.GetAllEmptyCellsIndexes(gameState);

            watch.Start();

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
                if (timeout)
                {
                    return move;
                }
            }
            //Console.WriteLine();
            //Console.WriteLine("best: " + bestScore + ", index: " + move);
            return move;
        }

        private bool OutOfTime()
        {
            return watch.ElapsedMilliseconds >= timePerMove;
        }

        private int AbNegamax(int[] gameState, int depth, int lastIndex, bool neg, int alpha = -999, int beta = 999)
        {
            List<int> indexes = board.GetAllEmptyCellsIndexes(gameState);

            
            if (indexes.Count == 0 || depth == 0)
            {
                int value = EvaluateMove(gameState, lastIndex);
                return neg ? -value : value;
            }
            foreach (int index in indexes)
            {
                if (OutOfTime())
                {
                    watch.Stop();
                    timeout = true;
                    return alpha;
                }
                gameState[index] = 1;
                alpha = Math.Max(alpha, -AbNegamax(gameState, depth - 1, index, !neg, alpha, beta));
                gameState[index] = 0;
                
                if (alpha >= beta) return beta;
            }
            return alpha;
        }

    }
}
