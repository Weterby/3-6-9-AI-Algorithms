using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
namespace Three_Six_Nine
{
    public partial class Form1 : Form
    {
        #region Private Fields

        private Board board;
        private List<Button> buttons = new List<Button>();
        private string player1Type;
        private string player2Type;
        private bool gameStarted = false;

        private Algorithm player1;
        private Algorithm player2;
        private Game game;
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
                    c.Enabled = false;
                }
            }
        }
        #endregion

        #region Private Methods

        private void UpdateBoard()
        {
            p1Label.Text = "Player 1: " + board.P1Score;
            p2Label.Text = "Player 2: " + board.P2Score;
        }
        private void UpdateField(int index)
        {
            Button obj = buttons.Find(x => x.Name.Equals("button" + index));
            obj.BackColor = Color.Black;
            obj.Enabled = false;
        }

        private Algorithm InitializePlayer(string playerType, int depth=1)
        {
            Algorithm player = null;
            switch (playerType)
            {
                case "Player":
                    break;

                case "AI(Minimax)":
                    player = new MinimaxBot(board,depth);
                    break;

                case "AI(Negamax)":
                    player = new NegamaxBot(board, depth);
                    break;

                case "AI(Alphabeta)":
                    player = new AlphabetaBot(board, depth);
                    break;

                case "AI(AbNegamax)":
                    player = new AbNegamaxBot(board, depth);
                    break;

                case "AI(IterativeDeepening)":
                    player = new IterativeDeepeningBot(board, 5);
                    break;

                case "RandomBot":
                    player = new RandomBot(board);
                    break;
            }
            return player;
        }
        #endregion
        private void ClearBoard()
        {
            foreach (Button b in buttons) b.BackColor = Color.LightGray;
        }
        #region Form Events
        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Player");
            comboBox1.Items.Add("AI(Minimax)");
            comboBox1.Items.Add("AI(Negamax)");
            comboBox1.Items.Add("AI(Alphabeta)");
            comboBox1.Items.Add("AI(AbNegamax)");
            comboBox1.Items.Add("AI(IterativeDeepening)");
            comboBox1.Items.Add("RandomBot");
            comboBox1.Text = "RandomBot";

            comboBox2.Items.Add("AI(Minimax)");
            comboBox2.Items.Add("AI(Negamax)");
            comboBox2.Items.Add("AI(Alphabeta)");
            comboBox2.Items.Add("AI(AbNegamax)");
            comboBox2.Items.Add("AI(IterativeDeepening)");
            comboBox2.Items.Add("RandomBot");
            comboBox2.Text = "AI(Minimax)";
            ClearBoard();
        }
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
                board.MarkField(index);
                int score = board.CalculatePoints(board.BoardTable, index);
                board.P1Score += score;
                UpdateBoard();
            }

            if (board.RemainingMoves() == 0)
            { 
                MessageBox.Show("Player " + board.CheckWinner() + " wins!");
                gameStarted = false;
            }
            else
            {
                int btnNumber = game.PerformAIMove();
                UpdateBoard();
                UpdateField(btnNumber);
            }
        }
        private void BtnCreateNewGame(object sender, EventArgs e)
        {
            ClearBoard();
            board = new Board();
            gameStarted = true;
            if (player1Type.Equals("Player"))
            {
                foreach (Button b in buttons) b.Enabled = true;
            }
            else
            {
                foreach (Button b in buttons) b.Enabled = false;
            }
            player1 = InitializePlayer(player1Type,2);
            player2 = InitializePlayer(player2Type,2);

            game = new Game(board, player1, player2, 100);
            if(player1!= null)
            {
                game.StartSimulation();
                UpdateBoard();
            }
            
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            player1Type = comboBox1.SelectedItem.ToString();
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            player2Type = comboBox2.SelectedItem.ToString();
        }
        #endregion
    }
}
