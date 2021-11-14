using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Three_Six_Nine
{
    class Simulation
    {
        private enum PlayerTurn
        {
            PlayerOne,
            PlayerTwo
        }

        private Board board;
        private Algorithm player1;
        private Algorithm player2;
        private PlayerTurn playerTurn;

        private int gameAmount;
        private bool auto;

        private float p1Wins = 0;
        private float p2Wins = 0;
        private float p1Avg = 0;
        private float p2Avg = 0;

        public Simulation(Board board, Algorithm player1, Algorithm player2, int gameAmount=100, bool auto=false)
        {
            this.board = board;
            this.player1 = player1;
            this.player2 = player2;
            this.gameAmount = gameAmount;
            this.auto = auto;

            playerTurn = PlayerTurn.PlayerOne;
        }

        public void StartSimulation()
        {
            for (int i = 0; i < gameAmount; i++)
            {
                while (board.RemainingMoves() > 0)
                {
                    NextMove();
                }

                //if player1 win
                if(board.CheckWinner() == 1)
                {
                    p1Wins++;
                }
                else
                {
                    p2Wins++;
                }

                p1Avg += board.P1Score;
                p2Avg += board.P2Score;
                board.ClearBoard();
            }

            p1Wins /= gameAmount;
            p2Wins /= gameAmount;
            p1Avg /= gameAmount;
            p2Avg /= gameAmount;

            Console.WriteLine($"Simulated games amount: {gameAmount}");
            Console.WriteLine($"Player1 ({player1.GetType().Name}): ");
            Console.WriteLine($"    Win %: {p1Wins * 100}");
            Console.WriteLine($"    Avg points earned: {p1Avg}");
            Console.WriteLine($"Player2 ({player2.GetType().Name}): ");
            Console.WriteLine($"    Win %: {p2Wins * 100}");
            Console.WriteLine($"    Avg points earned: {p2Avg}");
        }

        private void NextMove()
        {
            if (playerTurn == PlayerTurn.PlayerOne && !auto)
            {
                int move = player1.MakeMove();
                board.BoardTable[move] = 1;
                board.P1Score += board.CalculatePoints(board.BoardTable, move);
                playerTurn = PlayerTurn.PlayerTwo;
            }
            else
            {
                int move = player2.MakeMove();
                board.BoardTable[move] = 1;
                board.P2Score += board.CalculatePoints(board.BoardTable, move);
                playerTurn = PlayerTurn.PlayerOne;
            }
        }
    }
}
