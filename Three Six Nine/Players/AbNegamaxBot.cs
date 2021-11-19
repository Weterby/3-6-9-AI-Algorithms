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
