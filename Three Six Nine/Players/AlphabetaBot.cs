using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Three_Six_Nine
{
    class AlphabetaBot : Algorithm
    {
        private int depth;
        public AlphabetaBot(Board board, int depth) : base(board)
        {
            this.depth = depth;
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
                int score = Alphabeta(gameState, depth - 1, false, index);
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
       
        private int Alphabeta(int[] gameState, int depth, bool isMaximizing, int lastIndex, int alpha= -999, int beta = 999)
        {
            List<int> indexes = board.GetAllEmptyCellsIndexes(gameState);
            if (indexes.Count == 0 || depth == 0)
            {
                int value = EvaluateMove(gameState, lastIndex);
                return isMaximizing ? -value : value;
            }
            if (isMaximizing)
            {
                foreach (int index in indexes)
                {
                    gameState[index] = 1;
                    alpha = Math.Max(alpha, Alphabeta(gameState, depth - 1, false, index, alpha, beta));
                    gameState[index] = 0;
                    if (alpha >= beta) return beta;
                }
                return alpha;
            }
            else
            {
                foreach (int index in indexes)
                {
                    gameState[index] = 1;
                    beta = Math.Min(beta, Alphabeta(gameState, depth - 1, true, index, alpha, beta));
                    gameState[index] = 0;
                    if (alpha >= beta) return alpha;
                }
                return beta;
            }
        }
    }
}
