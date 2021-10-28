using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Three_Six_Nine
{
    class Board
    {
        private int[] boardTable;
        private bool isPlayerTurn;
        private int p1Score;
        private int p2Score;
        public int[] BoardTable {
            get { return boardTable; }
            set { boardTable = value; }
        }
        public bool IsPlayerTurn {
            get { return isPlayerTurn; }
            set { isPlayerTurn = value; }
        }
        public int P1Score {
            get { return p1Score; }
            set { p1Score = value; }
        }
        public int P2Score {
            get { return p2Score; }
            set { p2Score = value; }
        }

        public Board()
        {
            BoardTable = new int[81];
            IsPlayerTurn = true;
            P1Score = 0;
            P2Score = 0;
        }

        public List<int> getAllEmptyCellsIndexes(int[] gameState)
        {
            List<int> indexes = new List<int>();
            for(int i = 0; i < gameState.Length; i++)
            {
                if (gameState[i] == 0) indexes.Add(i);
            }
            return indexes;
        }

        public int CheckWinner(int p1,int p2)
        {
            return p1 > p2 ? 1 : 2;
        }

        public void SetupBoard()
        {

        }
        public int CalculatePoints(int index)
        {
            int amount = 0;
            int score = 0;
            int offset = 9;
            int rowIndex = (index / offset)*offset;
            int colIndex = index % offset;
            for (int i = rowIndex; i < rowIndex+offset; i++)
            {
                if (boardTable[i] == 1) amount++;
            }

            if (amount == 2) score++;
            if (amount == 5) score+=2;
            if (amount == 8) score+=3;
            amount = 0;

            for (int i = colIndex; i < boardTable.Length; i+=9)
            {
                if (boardTable[i] == 1) amount++;
            }

            if (amount == 2) score++;
            if (amount == 5) score += 2;
            if (amount == 8) score += 3;
            return score;
        }
    }
}
