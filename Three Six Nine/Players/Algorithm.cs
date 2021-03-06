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
        public abstract int MakeMove();
        public int EvaluateMove(int[] gameState, int index)
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
    }
}
