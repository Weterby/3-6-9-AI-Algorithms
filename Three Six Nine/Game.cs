using System;
using Three_Six_Nine.PNS;
using System.Linq;
using System.Collections.Generic;

namespace Three_Six_Nine
{
    class Game
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
        private ProofNumberSearch pns;

        private int gameAmount;

        private float p1Wins = 0;
        private float p2Wins = 0;
        private float p1Avg = 0;
        private float p2Avg = 0;

        private int pnsBreak = 5;

        public Game(Board board, Algorithm player1, Algorithm player2, int gameAmount=100)
        {
            this.board = board;
            this.player1 = player1;
            this.player2 = player2;
            this.gameAmount = gameAmount;

            playerTurn = PlayerTurn.PlayerOne;
        }

        public void StartSimulation()
        {
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            int limit = 0;
            if (pnsBreak > 0) limit = pnsBreak;

            for (int i = 0; i < gameAmount; i++)
            {
                playerTurn = PlayerTurn.PlayerOne;
                pns = new ProofNumberSearch(new Node(board, PNS.Data.NodeType.Or, null, -1), 500);
                while (board.RemainingMoves() > limit)
                {
                    NextMove();
                }

                while (board.RemainingMoves() > 0)
                {
                    PnsMove();
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
                board.ResetBoard();
            }
            watch.Stop();
            p1Wins /= gameAmount;
            p2Wins /= gameAmount;
            p1Avg /= gameAmount;
            p2Avg /= gameAmount;

            pns.Resource.Reset();

            Console.WriteLine($"Simulated games amount: {gameAmount}");
            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
            Console.WriteLine($"Player1 ({player1.GetType().Name}): ");
            Console.WriteLine($"    Win %: {p1Wins * 100}");
            Console.WriteLine($"    Avg points earned: {p1Avg}");
            Console.WriteLine($"Player2 ({player2.GetType().Name}): ");
            Console.WriteLine($"    Win %: {p2Wins * 100}");
            Console.WriteLine($"    Avg points earned: {p2Avg}");
        }
        public int PerformAIMove()
        {
            ChangeTurn();
            int move = NextMove();
            return move;
        }
        private int NextMove()
        {
            int move = -1;
            if (playerTurn == PlayerTurn.PlayerOne)
            {
                move = player1.MakeMove();
                board.MarkField(move);
                board.P1Score += board.CalculatePoints(board.BoardTable, move);
                ChangeTurn();
            }
            else
            {
                move = player2.MakeMove();
                board.MarkField(move);
                board.P2Score += board.CalculatePoints(board.BoardTable, move);
                ChangeTurn();
            }
            return move;
        }

        private int PnsMove()
        {
            int move = -1;
            if (playerTurn == PlayerTurn.PlayerOne)
            {
                move = player1.MakeMove();
                board.MarkField(move);
                board.P1Score += board.CalculatePoints(board.BoardTable, move);
                ChangeTurn();
            }
            else
            {
                Node best = pns.Search(new Node(board, PNS.Data.NodeType.Or, null, -1));
                move = best.Index;
                board.MarkField(move);
                board.P2Score += board.CalculatePoints(board.BoardTable, move);
                ChangeTurn();
            }
            return move;
        }

        private void ChangeTurn()
        {
            if (playerTurn == PlayerTurn.PlayerOne)
            {
                playerTurn = PlayerTurn.PlayerTwo;
            }
            else
            {
                playerTurn = PlayerTurn.PlayerOne;
            }
        }
    }
}
