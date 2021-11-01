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

        public List<int> getAllEmptyCellsIndexes(int[] gameState)
        {
            List<int> indexes = new List<int>();
            for(int i = 0; i < gameState.Length; i++)
            {
                if (gameState[i] == 0) indexes.Add(i);
            }
            return indexes;
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

            if (amount == 2) score++;
            if (amount == 5) score+=2;
            if (amount == 8) score+=3;
            amount = 0;

            for (int i = colIndex; i < gameState.Length; i+=9)
            {
                if (gameState[i] == 1) amount++;
            }

            if (amount == 2) score++;
            if (amount == 5) score += 2;
            if (amount == 8) score += 3;
            return score;
        }
        public int EvaluateMove(int p1Score, int p2Score)
        {
            return p2Score - p1Score;
        }

        public int BestMove()
        {
            int bestScore =-999;
            int move = -1;
            List<int> indexes = getAllEmptyCellsIndexes(BoardTable);
            foreach(int index in indexes)
            {
                BoardTable[index] = 1;
                int score = Minimax(BoardTable, 1, false, P1Score, P2Score);
                BoardTable[index] = 0;
                //Console.Write("["+index+"]: "+score + " ");
                if(score > bestScore)
                {
                    bestScore = score;
                    move = index;
                }
            }
            //Console.WriteLine();
            //Console.WriteLine("best: " + bestScore + ", index: " + move);
            //Console.WriteLine("p1:" + P1Score + ", AI: " + P2Score);
            return move;
        }
        public int RandomPick()
        {
            List<int> indexes = getAllEmptyCellsIndexes(BoardTable);
            Random rand = new Random();
            int move = indexes[rand.Next(indexes.Count)];
            return move;
        }
        #endregion

        #region Private Methods
        private int Minimax(int[] gameState, int depth, bool isMaximizing, int p1Score, int p2Score)
        {
            List<int> indexes = getAllEmptyCellsIndexes(gameState);
            if (indexes.Count == 0 || depth == 0)
            {
                return EvaluateMove(p1Score, p2Score);
            }
            if (isMaximizing)
            {
                //AI move
                int bestScore = -999;
                foreach(int index in indexes)
                {
                    int tempPoints = CalculatePoints(gameState, index);
                    gameState[index] = 1;
                    int score = Minimax(gameState, depth - 1, false, p1Score, p2Score+tempPoints);
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
                    int tempPoints = CalculatePoints(gameState, index);
                    gameState[index] = 1;
                    int score = Minimax(gameState, depth - 1, true, p1Score+tempPoints, p2Score);
                    //Console.Write("{" + score + "}");
                    gameState[index] = 0;
                    bestScore = Math.Min(score, bestScore);
                }
                // Console.Write("|" + bestScore + "|");
                return bestScore;
            }
        }
        #endregion
    }
}
