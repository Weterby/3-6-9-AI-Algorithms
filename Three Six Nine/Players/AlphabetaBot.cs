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
        private int EvaluateMove(int[] gameState, int index)
        {
            int amount = 0;
            int scoreV = 0;
            int scoreH = 0;
            int offset = 9;
            int rowIndex = (index / offset) * offset;
            int colIndex = index % offset;
            for (int i = rowIndex; i < rowIndex + offset; i++)
            {
                if (gameState[i] == 1) amount++;
            }

            if (amount == 2) scoreH -= 5;
            if (amount == 3) scoreH += 5;
            if (amount == 5) scoreH -= 25;
            if (amount == 6) scoreH += 25;
            if (amount == 8) scoreH -= 125;
            if (amount == 9) scoreH += 125;
            amount = 0;

            for (int i = colIndex; i < gameState.Length; i += 9)
            {
                if (gameState[i] == 1) amount++;
            }

            if (amount == 2) scoreV -= 5;
            if (amount == 3) scoreV += 5;
            if (amount == 5) scoreV -= 25;
            if (amount == 6) scoreV += 25;
            if (amount == 8) scoreV -= 125;
            if (amount == 9) scoreV += 125;

            int value = 0;
            if (scoreH >= 0 && scoreV >= 0)
            {
                value = scoreH + scoreV;
            }
            else if (scoreH <= 0 && scoreV <= 0)
            {
                value = Math.Min(scoreH, scoreV);
            }
            else if (scoreH == -scoreV)
            {
                value = 0;
            }
            else if (scoreH == 0 && scoreV == 0)
            {
                value = 0;
            }
            else
            {
                value = scoreH + scoreV;
            }
            return value;
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
