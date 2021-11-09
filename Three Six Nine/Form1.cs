using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
namespace Three_Six_Nine
{
    public partial class Form1 : Form
    {
        #region Private Fields

        private Board board;
        private List<Button> buttons = new List<Button>();
        private string gameMode;
        //private bool gameStarted;
        private bool isRandomPlaying = true;
        #endregion

        #region Public Methods
        public Form1()
        {
            InitializeComponent();

            foreach (Control c in BoardBox.Controls)
            {
                if (c.GetType() == typeof(Button))
                {
                    buttons.Add(c as Button);
                }
            }
        }
        #endregion

        #region Private Methods

        private void onBtnClick(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.BackColor = Color.Black;
            btn.Enabled = false;

            int index = -1;
            string number = btn.Name.Substring(6);
            Int32.TryParse(number, out index);
            if (index < 0)
            {
                MessageBox.Show("Blad w przetwarzaniu indeksu przycisku");
            }
            else
            {
                board.BoardTable[index] = 1;
                int score = board.CalculatePoints(board.BoardTable, index);
                board.P1Score += score;
                p1Label.Text = "Player 1: " + board.P1Score;
                board.IsMaximizing = !board.IsMaximizing;
            }
            List<int> list = board.GetAllEmptyCellsIndexes(board.BoardTable);

            if (list.Count() == 0) MessageBox.Show("Player " + board.CheckWinner() + " wins!");
            else
            {
                int btnNumber = board.BestMove();
                board.BoardTable[btnNumber] = 1;
                int score = board.CalculatePoints(board.BoardTable, btnNumber);
                board.IsMaximizing = !board.IsMaximizing;
                board.P2Score += score;
                p2Label.Text = "Player 2: " + board.P2Score;
                Button obj = buttons.Find(x => x.Name.Equals("button" + btnNumber));
                obj.BackColor = Color.Black;
                obj.Enabled = false;
            }
        }
        private void BtnCreateNewGame(object sender, EventArgs e)
        {
            ClearBoard();
            SimulateGame(100);
        }

        private void ClearBoard()
        {
            board = new Board();

            foreach (Button btn in buttons)
            {
                if (gameMode.Equals("AI vs Player"))
                {
                    btn.Enabled = true;
                }
                else btn.Enabled = false;
                btn.BackColor = Color.LightGray;
            }
        }
        private void SimulateGame(int gameAmount)
        {
            float randomWins = 0;
            float aiWins = 0;
            float randomAvg = 0;
            float aiAvg = 0;
            for (int i = 0; i < gameAmount; i++)
            {
                while (board.RemainingMoves() > 0) NextMove();
                if (board.CheckWinner() == 2)
                {
                    aiWins++;
                }
                else randomWins++;
                randomAvg += board.P1Score;
                aiAvg += board.P2Score;
                ClearBoard();
            }
            randomWins /= gameAmount;
            aiWins /= gameAmount;
            aiAvg /= gameAmount;
            randomAvg /= gameAmount;

            Console.WriteLine($"Simulated games amount: {gameAmount}");
            Console.WriteLine("RandomBot: ");
            Console.WriteLine($"    Win %: {randomWins * 100}");
            Console.WriteLine($"    Avg points earned: {randomAvg}");
            Console.WriteLine("AI: ");
            Console.WriteLine($"    Win %: {aiWins * 100}");
            Console.WriteLine($"    Avg points earned: {aiAvg}");
        }
        private void NextMove()
        {
            int move = -1;
            if (!isRandomPlaying)
            {
                move = board.BestMove();
                board.BoardTable[move] = 1;
                board.P2Score += board.CalculatePoints(board.BoardTable, move);
            }
            else
            {
                move = board.RandomPick();
                board.BoardTable[move] = 1;
                board.P1Score += board.CalculatePoints(board.BoardTable, move);
            }

            if(move<0) MessageBox.Show("Blad w przetwarzaniu indeksu");
            else 
            {
                isRandomPlaying = !isRandomPlaying;
                UpdateBoard(move);
            }
            
        }

        private void UpdateBoard(int index)
        {
            Button obj = buttons.Find(x => x.Name.Equals("button" + index));
            obj.BackColor = Color.Black;
            obj.Enabled = false;

            p1Label.Text = "Player 1: " + board.P1Score;
            p2Label.Text = "Player 2: " + board.P2Score;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("AI vs Random");
            comboBox1.Items.Add("AI vs Player");
            comboBox1.Text = "AI vs Random";       
        }
        #endregion

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            gameMode = comboBox1.SelectedItem.ToString();
        }
    }
}
