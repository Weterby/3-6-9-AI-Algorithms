using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Three_Six_Nine
{
    class MinimaxBot : Algorithm
    {
        private int depth;
        public MinimaxBot(Board board, int depth) : base(board)
        {
            this.depth = depth;
        }
        public override int CalculateMove()
        {
            return 0;
        }
        private int EvaluateMove(int index) {
            return 0;
        }
        private int Minimax(int[] gameState, int depth, bool isMaximizing, int lastIndex)
        {
            List<int> indexes = board.GetAllEmptyCellsIndexes(gameState);
            if (indexes.Count == 0 || depth == 0)
            {
                int value = EvaluateMove(lastIndex);
                return isMaximizing ? -value : value;
            }
            if (isMaximizing)
            {
                //AI move
                int bestScore = -999;
                foreach (int index in indexes)
                {
                    gameState[index] = 1;
                    int score = Minimax(gameState, depth - 1, false, index);
                    gameState[index] = 0;
                    bestScore = Math.Max(score, bestScore);
                }
                return bestScore;
            }
            else
            {
                //player simulated move
                int bestScore = 999;
                foreach (int index in indexes)
                {
                    gameState[index] = 1;
                    int score = Minimax(gameState, depth - 1, true, index);
                    gameState[index] = 0;
                    bestScore = Math.Min(score, bestScore);
                }
                return bestScore;
            }
        }
    }
}
