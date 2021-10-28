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
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void OnClick(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            MessageBox.Show("clicked " + pb.Name);
        }
    }
}
