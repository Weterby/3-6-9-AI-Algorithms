using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Three_Six_Nine
{
    class Board
    {
        #region Private Fields

        private int[] boardTable;
        private int p1Score;
        private int p2Score;

        #endregion

        #region Public Properties

        public int[] BoardTable {
            get { return boardTable; }
            set { boardTable = value; }
        }

        public int P1Score {
            get { return p1Score; }
            set { p1Score = value; }
        }
        public int P2Score {
            get { return p2Score; }
            set { p2Score = value; }
        }

        #endregion

        #region Public methods
        public Board()
        {
            boardTable = new int[81];
            p1Score = 0;
            p2Score = 0;
        }

        public List<int> GetAllEmptyCellsIndexes(int[] gameState)
        {
            List<int> indexes = new List<int>();
            for (int i = 0; i < gameState.Length; i++)
            {
                if (gameState[i] == 0) indexes.Add(i);
            }
            return indexes;
        }
        public int RemainingMoves()
        {
            List<int> list = GetAllEmptyCellsIndexes(BoardTable);
            return list.Count;
        }
        public int CheckWinner()
        {
            return P1Score > P2Score ? 1 : 2;
        }
        public void MarkField(int index)
        {
            BoardTable[index] = 1;
        }
        public int CalculatePoints(int[] gameState, int index)
        {
            int amount = 0;
            int score = 0;
            int offset = 9;
            int rowIndex = (index / offset) * offset;
            int colIndex = index % offset;
            for (int i = rowIndex; i < rowIndex + offset; i++)
            {
                if (gameState[i] == 1) amount++;
            }

            if (amount == 3) score++;
            if (amount == 6) score += 2;
            if (amount == 9) score += 3;
            amount = 0;

            for (int i = colIndex; i < gameState.Length; i += 9)
            {
                if (gameState[i] == 1) amount++;
            }

            if (amount == 3) score++;
            if (amount == 6) score += 2;
            if (amount == 9) score += 3;
            return score;
        }
        public void ResetBoard()
        {
            BoardTable = new int[81];
            p1Score = 0;
            p2Score = 0;
        }

        public Board DeepCopy()
        {
            Board boardCopy = new Board();
            boardCopy.BoardTable = new int[81];

            boardCopy.P1Score = P1Score;
            boardCopy.P2Score = P2Score;

            for(int i = 0; i <= BoardTable.Length; i++)
            {
                boardCopy.BoardTable[i] = BoardTable[i];
            }
            return boardCopy;
        }
    }
}
#endregion