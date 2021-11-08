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
        private bool isMaximizing;
        private int p1Score;
        private int p2Score;

        #endregion

        #region Public Properties

        public int[] BoardTable {
            get { return boardTable; }
            set { boardTable = value; }
        }
        public bool IsMaximizing {
            get { return isMaximizing; }
            set { isMaximizing = value; }
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
            BoardTable = new int[81];
            IsMaximizing = false;
            P1Score = 0;
            P2Score = 0;
        }

        public List<int> GetAllEmptyCellsIndexes(int[] gameState)
        {
            List<int> indexes = new List<int>();
            for(int i = 0; i < gameState.Length; i++)
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

        public int CalculatePoints(int[] gameState, int index)
        {
            int amount = 0;
            int score = 0;
            int offset = 9;
            int rowIndex = (index / offset)*offset;
            int colIndex = index % offset;
            for (int i = rowIndex; i < rowIndex+offset; i++)
            {
                if (gameState[i] == 1) amount++;
            }

            if (amount == 3) score++;
            if (amount == 6) score+=2;
            if (amount == 9) score+=3;
            amount = 0;

            for (int i = colIndex; i < gameState.Length; i+=9)
            {
                if (gameState[i] == 1) amount++;
            }

            if (amount == 3) score++;
            if (amount == 6) score += 2;
            if (amount == 9) score += 3;
            return score;
        }
        public int EvaluateMove(int[] gameState, bool isMaximizing, int index)
        {
            int value = CalculatePoints(gameState, index);
            return isMaximizing ? value : -value;
        }

        public int BestMove()
        {
            int bestScore =-999;
            int move = -1;
            List<int> indexes = GetAllEmptyCellsIndexes(BoardTable);
            foreach(int index in indexes)
            {
                BoardTable[index] = 1;
                int score = Minimax(BoardTable, 1, false, index);
                BoardTable[index] = 0;
                Console.Write("["+index+"]: "+score + " ");
                if (score > bestScore)
                {
                    bestScore = score;
                    move = index;
                }
            }
            Console.WriteLine();
            Console.WriteLine("best: " + bestScore + ", index: " + move);
            Console.WriteLine("p1:" + P1Score + ", AI: " + P2Score);
            return move;
        }
        public int RandomPick()
        {
            List<int> indexes = GetAllEmptyCellsIndexes(BoardTable);
            Random rand = new Random();
            int move = indexes[rand.Next(indexes.Count)];
            return move;
        }
        #endregion

        #region Private Methods
        private int Minimax(int[] gameState, int depth, bool isMaximizing, int lastIndex)
        {
            List<int> indexes = GetAllEmptyCellsIndexes(gameState);
            if (indexes.Count == 0 || depth == 0)
            {
                return EvaluateMove(gameState, isMaximizing, lastIndex);
            }
            if (isMaximizing)
            {
                //AI move
                int bestScore = -999;
                foreach(int index in indexes)
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
        #endregion
    }
}
