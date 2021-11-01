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
        private bool gameStarted;
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
                int score = board.CalculatePoints(board.BoardTable, index);
                board.BoardTable[index] = 1;
                board.P1Score += score;
                p1Label.Text = "Player 1: " + board.P1Score;
                board.IsMaximizing = !board.IsMaximizing;
            }
            List<int> list = board.getAllEmptyCellsIndexes(board.BoardTable);
            
            if (list.Count() == 0) MessageBox.Show("Player " + board.CheckWinner() + " wins!");
            else
            {
                int btnNumber = board.BestMove();
                int score = board.CalculatePoints(board.BoardTable, btnNumber);
                board.BoardTable[btnNumber] = 1;
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
            board = new Board();

            foreach (Button btn in buttons)
            {
                if(gameMode.Equals("AI vs Player") || gameMode.Equals("Player vs Player"))
                {
                    btn.Enabled = true;
                }
                else btn.Enabled = false;
                btn.BackColor = Color.LightGray;
            }
            
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
            comboBox1.Items.Add("AI vs AI");
            comboBox1.Items.Add("AI vs Player");
            comboBox1.Items.Add("AI vs Random");
            comboBox1.Items.Add("Player vs Player");
            comboBox1.Text = "AI vs Random";
            
        }
        #endregion

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            gameMode = comboBox1.SelectedItem.ToString();
        }
    }
}
