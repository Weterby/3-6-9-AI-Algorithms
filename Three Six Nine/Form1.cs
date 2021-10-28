using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Three_Six_Nine
{
    public partial class Form1 : Form
    {
        private Board board;
        List<Button> buttons = new List<Button>();
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

        private void Form1_Load(object sender, EventArgs e)
        {

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
                int points = board.CalculatePoints(index);
                board.BoardTable[index] = 1;
                if (board.IsPlayerTurn)
                {
                    board.P1Score += points;
                    p1Label.Text = "Player 1: " + board.P1Score;
                }
                else
                {
                    board.P2Score += points;
                    p2Label.Text = "Player 2: " + board.P2Score;
                }
                board.IsPlayerTurn = !board.IsPlayerTurn;
            }
        }

        private void BtnCreateNewGame(object sender, EventArgs e)
        {
            foreach (Button btn in buttons) btn.Enabled=true;
            board = new Board();
            
        }
    }
}
