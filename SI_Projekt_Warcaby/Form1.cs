using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Warcaby
{
    public partial class Form1 : Form
    {
        private BoardDrawer drawer;
        private Game game;
        public Form1()
        {
            InitializeComponent();
            this.comboBoxAlgP1.SelectedIndex = 0;
            this.comboBoxAlgP2.SelectedIndex = 0;
            //maksymalna liczba rzędów pionków musi byc mniejsza od połowy wielksci planszy
            this.rowsOfCheckers.Value = 1;
            this.rowsOfCheckers.Maximum = this.boardSizeSelector.Value/2 - 1;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void rowsOfCheckers_ValueChanged(object sender, EventArgs e)
        {

        }

        private void boardSizeSelector_ValueChanged(object sender, EventArgs e)
        {
            this.rowsOfCheckers.Value = 1;
            this.rowsOfCheckers.Maximum = this.boardSizeSelector.Value/2 - 1;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            //załadowanie nowej gry
            Game.Alhorithms p1, p2;
            if(isP1Human.Checked)
            {
                p1 = Game.Alhorithms.Human;
            }
            else
            {
                if(comboBoxAlgP1.SelectedItem == comboBoxAlgP1.Items[0])
                {
                    p1 = Game.Alhorithms.Minimax;
                }
                else
                {
                    p1 = Game.Alhorithms.AlfaBeta;
                }
            }
            if(isP2Human.Checked)
            {
                p2 = Game.Alhorithms.Human;
            }
            else
            {
                if (comboBoxAlgP2.SelectedItem == comboBoxAlgP2.Items[0])
                {
                    p2 = Game.Alhorithms.Minimax;
                }
                else
                {
                    p2 = Game.Alhorithms.AlfaBeta;
                }
            }
            game = new Game(p1, p2,Decimal.ToInt32(boardSizeSelector.Value), Decimal.ToInt32(rowsOfCheckers.Value));
            drawer = new BoardDrawer(game, boardPanel,Decimal.ToInt32(boardSizeSelector.Value));
            game.boardDrawer = drawer;
            if (!isP1Human.Checked)
            {
                game.nextPlayer();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void isP1Human_CheckedChanged(object sender, EventArgs e)
        {
            if((sender as CheckBox).Checked)
            {
                comboBoxAlgP1.Visible = false;
            }
            else
            {
                comboBoxAlgP1.Visible = true;
            }
        }

        private void isP2Human_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as CheckBox).Checked)
            {
                comboBoxAlgP2.Visible = false;
            }
            else
            {
                comboBoxAlgP2.Visible = true;
            }
        }
    }
}
