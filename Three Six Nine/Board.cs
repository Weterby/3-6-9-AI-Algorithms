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
        public int[] BoardTable { get; set; }
        public bool IsPlayerTurn { get; set; }
        public int P1Score { get; set; }
        public int P2Score { get; set; }

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

        public int Evaluate(int[] gameState,int index)
        {
            return 0;
        }
    }
}
