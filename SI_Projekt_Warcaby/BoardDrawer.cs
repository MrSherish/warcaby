using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Warcaby
{

    class BoardDrawer
    {
        private Panel boardPanel;   // panel w którym rysowana jest gra

        private int size;           //rozmiar planszy (ilosć rzędów/ kolumn);

        private int fieldSize;      // rozmiar pola w pikselach

        private Game game;          // referencja na klasę gry

        private bool showPotentialMoves;    //czy wyswietlac podpowiedzi do ruchów

        private List<Game.Move> currentPossibleMoves; // lista par punktow do wyswietlania podpowiedzi;

        private Brush[] brushes = new Brush[2];     //kolory pionków

        private Point selectedField;

        public BoardDrawer(Game g, Panel bP, int size)
        {
            //ustawienie kolorów pionków
            brushes[0] = Brushes.Red;           
            brushes[1] = Brushes.Blue;
            game = g;
            boardPanel = bP;
            this.size = size;
            if (size % 2 != 0)
            {
                throw new Exception("Invalid size value");
            }
            //odświeżenie i zarejstreowanie obsługi myszki oraz rysowania
            boardPanel.Invalidate();
            boardPanel.MouseClick += mouseClick;
            boardPanel.Paint += new PaintEventHandler(ScenePainter);

            //przeleiczamy wielkość jednego pola w pikselach
            fieldSize = Math.Min(boardPanel.Width, boardPanel.Height) / size;
        }

        public void Refresh()
        {
            boardPanel.Refresh();

        }

        //funkcja rysująca
        private void ScenePainter(Object sender, PaintEventArgs e)
        {
            Graphics gr = e.Graphics;
            bool flipFlop = true;
            //rysowanie pól
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size / 2; x++)
                {
                    if (flipFlop)
                    {
                        gr.FillRectangle(Brushes.Black, new Rectangle(2 * x * fieldSize, y * fieldSize, fieldSize, fieldSize));
                        gr.FillRectangle(Brushes.White, new Rectangle(2 * x * fieldSize + fieldSize, y * fieldSize, fieldSize, fieldSize));
                    }
                    else
                    {
                        gr.FillRectangle(Brushes.White, new Rectangle(2 * x * fieldSize, y * fieldSize, fieldSize, fieldSize));
                        gr.FillRectangle(Brushes.Black, new Rectangle(2 * x * fieldSize + fieldSize, y * fieldSize, fieldSize, fieldSize));
                    }
                }
                flipFlop = !flipFlop;
            }

            //rysowanie pionkow
            for(int y=0;y< size;y++)
            {
                for(int x=0;x< size;x++)
                {
                    if(game.Board[y * size + x]!=null && game.Board[y*size+x].owner>=0)
                    {
                        if(x == selectedField.X && y == selectedField.Y && game.CurrentPlayer==game.Board[selectedField.Y*size+selectedField.X].owner)//wybrany pionek
                        {
                            gr.FillPie(Brushes.GreenYellow,
                                new Rectangle(fieldSize * x + 1, fieldSize * y + 1, fieldSize - 2, fieldSize - 2),
                                    0, 360);
                        }
                        else
                        {
                            gr.FillPie(brushes[game.Board[y * size + x].owner],
                                new Rectangle(fieldSize * x + 1, fieldSize * y + 1, fieldSize - 2, fieldSize - 2),
                                    0, 360);
                        }
                    }
                }
            }


            //rysowanie podpowiedzi
            if(showPotentialMoves)
            {
                foreach(Game.Move m in currentPossibleMoves)
                {
                    gr.DrawLine(Pens.Ivory, new Point(m.From.X * fieldSize+fieldSize/2, m.From.Y * fieldSize + fieldSize / 2),
                        new Point(m.To.X * fieldSize + fieldSize / 2, m.To.Y * fieldSize + fieldSize / 2));
                }
                currentPossibleMoves.Clear();
            }

        }

        //obsługa myszki - wywołanie funkcji obsługi w klasie game.
        private void mouseClick(object sender, MouseEventArgs e)
        {
            selectedField = new Point(e.X / fieldSize,e.Y/fieldSize);
            //lewy guzik odpowiada za wybieranie 
            if (e.Button.Equals(MouseButtons.Left))
            {
                game.ProcessInputLeftButton(selectedField);
                Refresh();
            }
            //prawy służy do podpowiedzi
            if (e.Button.Equals(MouseButtons.Right))
            {
                showPotentialMoves = true;
                bool k; //potrzebny parametr, ale w tym miejscu nieużywany;
                currentPossibleMoves = Game.getPossibleMoves(game.CurrentPlayer,game.Board,game.BoardSize);
                Refresh();
            }
        }

    }
}
