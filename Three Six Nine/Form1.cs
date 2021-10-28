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
        List<Button> buttons = new List<Button>();
        public Form1()
        {
            InitializeComponent();

            foreach (Control c in BoardBox.Controls)
            {
                if (c.GetType() == typeof(Button))
                {
                    buttons.Add((Button)c);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void onBtnClick(object sender, EventArgs e)
        {
            Button btn = sender as Button;
           // MessageBox.Show("clicked " + btn.Name);
            btn.BackColor = Color.Black;
            btn.Enabled = false;
        }

        private void BtnCreateNewGame(object sender, EventArgs e)
        {
            
        }
    }
}
