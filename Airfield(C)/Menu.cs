using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Airfield_C_
{
    [Serializable]
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void lable_Click(object sender, EventArgs e)
        {
            Form1 game = new Form1(false);
            this.Hide();
            game.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Form1 game = new Form1(true);
            this.Hide();
            game.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            About about = new About();
            this.Hide();
            about.ShowDialog();
            this.Show();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Reference reference = new Reference();
            this.Hide();
            reference.ShowDialog();
            this.Show();
        }

        private void Menu_Load(object sender, EventArgs e)
        {

        }
    }
}
