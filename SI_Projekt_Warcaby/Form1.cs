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
        public Form1()
        {
            InitializeComponent();
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
            Game g = new Game(true, true,Decimal.ToInt32(boardSizeSelector.Value), Decimal.ToInt32(rowsOfCheckers.Value));
            drawer = new BoardDrawer(g, boardPanel,Decimal.ToInt32(boardSizeSelector.Value));
            g.boardDrawer = drawer;
    }
    }
}
