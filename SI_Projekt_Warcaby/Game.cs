using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Warcaby
{
    class Game // note : narazie nie zaimplementowałem jeszcze "damek". Obsługa sterowania: Klikamy na pionek (zaczyna czerwony), następnie na pole. Jeszcze dorobię jakieś oznaczenia, ze wybrany 
    {
        public enum Alhorithms
        {
            Human,
            Minimax,
            AlfaBeta
        }
        //stany gry w których moze być gracz
        private enum State
        {
            selecting_checker, //wybór pionka
            selecting_field,    //wybór pola na które ma przejść pionek
            selecting_field_series  //"kombo" zabójstw
        }

        public struct Move
        {
            public Move(Point f, Point t, bool killing)
            {
                this.killing = killing;
                From = f;
                To = t;
            }

            public bool killing;
            public Point From;
            public Point To;
        }

        public class Checker
        {
            public Checker(int owner)
            {
                this.owner = owner;
                this.isKing = false;
            }

            public Checker(Checker original) //copy constructor
            {
                this.owner = original.owner;
                this.isKing = original.isKing;
            }
            public bool isKing;
            public int owner;
        }
        private abstract class Player
        {
            protected bool side;

            protected Game game;

            public Player(Game g)
            {
                game = g;
            }
        };

        private class AIPlayer : Player
        {
            private MinMax algorithm;

            public AIPlayer(Game g, Game.Alhorithms alg) : base(g)
            {
                if(alg==Game.Alhorithms.Minimax)
                {
                    algorithm = new MinMax(g);
                }
                if(alg == Game.Alhorithms.AlfaBeta)
                {
                    algorithm = new AlfaBeta(g);
                }
                if(alg == Game.Alhorithms.Human)
                {
                    throw new Exception("Wrong argument for ai player constructor.");
                }
            }

            public Game.Move move()
            {
                return algorithm.SelectedMove;
            }
        }

        private class HumanPlayer : Player
        {
            private State currentState;

            private Point selectedCheckerField;

            public HumanPlayer(Game g)
                :base(g)
            {

            }

            public void ProcessInputLeftButton(Point selectedField)//w zależnosci od stanu tury gracza przetwarzamy działanie lewego przycisku
            {
                switch(currentState)       
                {
                    case State.selecting_checker:       //wybór pionka - chwilowo brak możliwości odznaczenia i zaznaczenia innego - todo
                        if(game.board[selectedField.Y * game.size + selectedField.X]!=null &&
                            game.board[selectedField.Y*game.size+selectedField.X].owner==game.currentPlayer)
                        {
                            selectedCheckerField = selectedField;
                            currentState = State.selecting_field;
                        }
                        break;

                    case State.selecting_field: //wybór kolejnego pola planszy na ktore ma stanąć pionek
                        Checker ch = game.board[selectedField.Y * game.size + selectedField.X];
                        if (ch!=null && ch.owner==game.currentPlayer)
                        {
                            currentState = State.selecting_checker;
                            ProcessInputLeftButton(selectedField);
                            return;
                        }
                        List<Move> l = game.getPossibleMoves(game.currentPlayer);
                        foreach (Move m in l)
                        {
                            if(m.From.Equals(selectedCheckerField) && m.To.Equals(selectedField))     //jedynie z możliwych do wyboru pól zwracanych do listy l
                            {
                                game.MoveChecker(selectedCheckerField, selectedField);
                                game.nextPlayer();
                                currentState = State.selecting_checker;
                            }
                        }
                        break;
                }
            }
        }

        Player[] players = new Player[2];

        int currentPlayer = 0;

        public int CurrentPlayer
        {
            get
            {
                return currentPlayer;
            }
        }

        private Checker[] board; // plansza ma -1 w miejscach braku pionka. Pionki gracza 0 mają indeks 0, a gracza 1 - 1.
                                 // pozycja na planszy to y*size + x

        public BoardDrawer boardDrawer;

        public Checker[] Board // UWAGA: ZWRACA TABLICĘ REFERENCJI!!! ~Krystian
        {
            get
            {
                return board;
            }
        } 

        private int size;

        public int BoardSize
        {
            get
            {
                return size;
            }
        }

        public int checkWin()
        {
            int c0 = 0, c1 = 0;
            foreach (Checker ch in board)
            {
                if(ch!=null)
                {
                    if (ch.owner == 0)
                    {
                        c0++;
                    }
                    if (ch.owner == 1)
                    {
                        c1++;
                    }
                }
            }
            return c0 == 0 ? 0 : (c1 == 0 ? 1 : -1);
        }

        public void nextPlayer()
        {
            int cond = checkWin();
            if (cond==0 || cond==1)
            {
                MessageBox.Show("Gra zakonczona.");
                return;
            }
            currentPlayer = 1 - currentPlayer;
            if(players[currentPlayer] is AIPlayer)
            {
                this.MoveChecker((players[currentPlayer] as AIPlayer).move());
                nextPlayer();
            }
            boardDrawer.Refresh();
        }

        public Game(Alhorithms p1, Alhorithms p2, int size, int checkersRows)
        {
            this.size = size;
            board = new Checker[size*size];
            //inicjalizacja planszy
            for (int i=0;i< size*size;i++)
            {
                board[i] = null;
            }
            //inicjalizaje pionków
            for(int y=0;y<checkersRows;y++)
            {
                for (int x = 0; x < size / 2; x++)
                {
                    board[size * y + 2*x + (y % 2)] = new Checker(0);
                }
            }

            for (int y = size-1; y >= size-checkersRows; y--)
            {
                for (int x = 0; x < size / 2; x++)
                {
                    board[size * y + 2 * x + (y % 2)] = new Checker(1);
                }
            }
            currentPlayer = 0;
            if (p2 == Alhorithms.Human)
            {
                players[1] = new HumanPlayer(this);
            }
            else
            {
                players[1] = new AIPlayer(this, p2);
                // bot here
            }
            if (p1 == Alhorithms.Human)
            {
                players[0] = new HumanPlayer(this);
            }
            else
            {
                players[0] = new AIPlayer(this, p1);
                currentPlayer = 1;
                // bot here
            }
        }

        public void ProcessInputLeftButton(Point selectedField)
        {
            if(players[currentPlayer] is HumanPlayer)
            {
                (players[currentPlayer] as HumanPlayer).ProcessInputLeftButton(selectedField);
            }
        }

        public void MoveChecker(Move m)
        {
            MoveChecker(m.From, m.To);
        }

        public void MoveChecker(Point from, Point to) //ta funkcja nie waliduje już ruchu, jednie przesuwa i zbija pionki. Narazie nie działa też dla "króla".
        {
            if(Math.Abs(from.X - to.X )> 1) //taki przeskok oznacza zabicie
            {
                board[(from.Y + ((to.Y - from.Y))/2) * size + from.X + ((to.X - from.X)/2)] = null;
            }
            board[to.Y * size + to.X] = board[from.Y * size + from.X];
            board[from.Y * size + from.X] = null;
        }

        //zwraca liste pól z przeciwnikami dookoła. Warunki w if-ach są trochę skomplikowane, ale chyba działają poprawnie. Wbrew pozorm jest tu trochę case'ów :D
        private List<Point> enemiesAroundToKill(int px, int py)
        {
            Checker curr = board[py * size + px];
            List<Point> ret = new List<Point>();
            if (curr.isKing)
            {

            }
            else
            {
                Checker target;
                Point targetPoint = new Point(px - 1, py - 1);
                if (targetPoint.X >= 0 && targetPoint.X < size && targetPoint.Y>=0 && targetPoint.Y<size)
                {
                    target = board[(py - 1) * size + px - 1];
                    if (target != null && target.owner == 1 - curr.owner)
                    {
                        if (px - 2 >= 0 && py - 2 >=0 && board[(py - 2) * size + (px - 2)] == null)
                        {
                            ret.Add(targetPoint);
                        }
                    }
                }

                targetPoint = new Point(px + 1, py - 1);
                if (targetPoint.X >= 0 && targetPoint.X < size && targetPoint.Y >= 0 && targetPoint.Y < size)
                {
                    target = board[(py - 1) * size + px + 1];
                    if (target != null && target.owner == 1 - curr.owner)
                    {
                        if (px + 2 < size && py - 2 >=0 && board[(py - 2) * size + (px + 2)] == null)
                        {
                            ret.Add(targetPoint);
                        }
                    }
                }

                targetPoint = new Point(px + 1, py + 1);
                if (targetPoint.X >= 0 && targetPoint.X < size && targetPoint.Y >= 0 && targetPoint.Y < size)
                {
                    target = board[(py + 1) * size + px + 1];

                    if (target != null && target.owner == 1 - curr.owner)
                    {
                        if (px + 2 <size && py + 2 < size && board[(py+2) * size + (px+2)] == null)
                        {
                            ret.Add(targetPoint);
                        }
                    }
                }

                targetPoint = new Point(px - 1, py + 1);
                if (targetPoint.X >= 0 && targetPoint.X < size && targetPoint.Y >= 0 && targetPoint.Y < size)
                {
                    target = board[(py + 1) * size + px - 1];

                    if (target != null && target.owner == 1 - curr.owner)
                    {
                        if (px - 2 >= 0 && py + 2 < size && board[(py + 2) * size + (px - 2)] == null)
                        {
                            ret.Add(targetPoint);
                        }
                    }
                }

            }
            return ret;
        }

        public List<Move> getPossibleMoves(int currentPlayer)
        {
            List<Move> ret = new List<Move>();
            List<Move> retKill = new List<Move>();

            for(int y=0;y< size;y++)
            {
                for(int x=0;x< size;x++)
                {
                    if(board[y * size + x]!=null && board[y*size+x].owner == currentPlayer)
                    {
                        List<Move> curr = getPossibleMovesForField(x, y);
                        foreach(Move m in curr)
                        {
                            if(m.killing)
                            {
                                retKill.Add(m);
                            }
                            else
                            {
                                ret.Add(m);
                            }
                        }
                    }
                }
            }

            if(retKill.Count > 0)
            {
                return retKill;
            }
            return ret;
        }

        //zwraca listę możliwych to "staniecia" pól, wokół podanego pola. Uwzględnia konieczność bicia, gdy wokół są przeciwnicy.
        public List<Move> getPossibleMovesForField(int px,int py)
        {
            Checker checker = board[py * size + px];

            if(checker == null)
            {
                return new List<Move>();
            }

            List<Move> ret = new List<Move>();

            List<Point> enemies = enemiesAroundToKill(px, py);

            Point curr = new Point(px, py);

            if(enemies.Count>0)
            {
                foreach(Point en in enemies)
                {
                    ret.Add(new Move(curr, new Point (en.X - (px - en.X), en.Y - (py - en.Y)),true));
                }
                return ret;
            }
            else
            {
                int tx, ty;
                if(checker.owner==0)
                {
                   tx = px + 1;
                   ty = py + 1;
                   if (tx >= 0 && tx < size && ty >= 0 && ty < size && board[ty * size + tx] == null)
                   {
                        ret.Add(new Move(curr,new Point(tx, ty), false));
                   }
                   tx = px - 1;
                   if (tx >= 0 && tx < size && ty >= 0 && ty < size && board[ty * size + tx] == null)
                   {
                        ret.Add(new Move(curr, new Point(tx, ty), false));
                    }
                }
                else
                {
                    tx = px + 1;
                    ty = py - 1;
                    if (tx >= 0 && tx < size && ty >= 0 && ty < size && board[ty * size + tx] == null)
                    {
                        ret.Add(new Move(curr, new Point(tx, ty), false));
                    }
                    tx = px - 1;
                    if (tx >= 0 && tx < size && ty >= 0 && ty < size && board[ty * size + tx] == null)
                    {
                        ret.Add(new Move(curr, new Point(tx, ty), false));
                    }
                }
            }

            return ret;
        }


        //funkcje static do użycia poza klasą game (np. w generowanym drzewie)
        public static List<Point> enemiesAroundToKill(int px, int py, Checker[] board, int size)
        {
            Checker curr = board[py * size + px];
            List<Point> ret = new List<Point>();
            if (curr.isKing)
            {

            }
            else
            {
                Checker target;
                Point targetPoint = new Point(px - 1, py - 1);
                if (targetPoint.X >= 0 && targetPoint.X < size && targetPoint.Y >= 0 && targetPoint.Y < size)
                {
                    target = board[(py - 1) * size + px - 1];
                    if (target != null && target.owner == 1 - curr.owner)
                    {
                        if (px - 2 >= 0 && py - 2 >= 0 && board[(py - 2) * size + (px - 2)] == null)
                        {
                            ret.Add(targetPoint);
                        }
                    }
                }

                targetPoint = new Point(px + 1, py - 1);
                if (targetPoint.X >= 0 && targetPoint.X < size && targetPoint.Y >= 0 && targetPoint.Y < size)
                {
                    target = board[(py - 1) * size + px + 1];
                    if (target != null && target.owner == 1 - curr.owner)
                    {
                        if (px + 2 < size && py - 2 >= 0 && board[(py - 2) * size + (px + 2)] == null)
                        {
                            ret.Add(targetPoint);
                        }
                    }
                }

                targetPoint = new Point(px + 1, py + 1);
                if (targetPoint.X >= 0 && targetPoint.X < size && targetPoint.Y >= 0 && targetPoint.Y < size)
                {
                    target = board[(py + 1) * size + px + 1];

                    if (target != null && target.owner == 1 - curr.owner)
                    {
                        if (px + 2 < size && py + 2 < size && board[(py + 2) * size + (px + 2)] == null)
                        {
                            ret.Add(targetPoint);
                        }
                    }
                }

                targetPoint = new Point(px - 1, py + 1);
                if (targetPoint.X >= 0 && targetPoint.X < size && targetPoint.Y >= 0 && targetPoint.Y < size)
                {
                    target = board[(py + 1) * size + px - 1];

                    if (target != null && target.owner == 1 - curr.owner)
                    {
                        if (px - 2 >= 0 && py + 2 < size && board[(py + 2) * size + (px - 2)] == null)
                        {
                            ret.Add(targetPoint);
                        }
                    }
                }

            }
            return ret;
        }

        public static List<Move> getPossibleMoves(int currentPlayer, Checker[] board, int size)
        {
            List<Move> ret = new List<Move>();
            List<Move> retKill = new List<Move>();

            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    if (board[y * size + x] != null && board[y * size + x].owner == currentPlayer)
                    {
                        List<Move> curr = getPossibleMovesForField(x, y, board, size);
                        foreach (Move m in curr)
                        {
                            if (m.killing)
                            {
                                retKill.Add(m);
                            }
                            else
                            {
                                ret.Add(m);
                            }
                        }
                    }
                }
            }

            if (retKill.Count > 0)
            {
                return retKill;
            }
            return ret;
        }
        
        public static List<Move> getPossibleMovesForField(int px, int py, Checker[] board, int size)
        {
            Checker checker = board[py * size + px];

            if (checker == null)
            {
                return new List<Move>();
            }

            List<Move> ret = new List<Move>();

            List<Point> enemies = enemiesAroundToKill(px, py, board, size);

            Point curr = new Point(px, py);

            if (enemies.Count > 0)
            {
                foreach (Point en in enemies)
                {
                    ret.Add(new Move(curr, new Point(en.X - (px - en.X), en.Y - (py - en.Y)), true));
                }
                return ret;
            }
            else
            {
                int tx, ty;
                if (checker.owner == 0)
                {
                    tx = px + 1;
                    ty = py + 1;
                    if (tx >= 0 && tx < size && ty >= 0 && ty < size && board[ty * size + tx] == null)
                    {
                        ret.Add(new Move(curr, new Point(tx, ty), false));
                    }
                    tx = px - 1;
                    if (tx >= 0 && tx < size && ty >= 0 && ty < size && board[ty * size + tx] == null)
                    {
                        ret.Add(new Move(curr, new Point(tx, ty), false));
                    }
                }
                else
                {
                    tx = px + 1;
                    ty = py - 1;
                    if (tx >= 0 && tx < size && ty >= 0 && ty < size && board[ty * size + tx] == null)
                    {
                        ret.Add(new Move(curr, new Point(tx, ty), false));
                    }
                    tx = px - 1;
                    if (tx >= 0 && tx < size && ty >= 0 && ty < size && board[ty * size + tx] == null)
                    {
                        ret.Add(new Move(curr, new Point(tx, ty), false));
                    }
                }
            }

            return ret;
        }

        public static void MoveChecker(Point from, Point to, Checker[] board, int size) //ta funkcja nie waliduje już ruchu, jednie przesuwa i zbija pionki. Narazie nie działa też dla "króla".
        {
            if (Math.Abs(from.X - to.X) > 1) //taki przeskok oznacza zabicie
            {
                board[(from.Y + ((to.Y - from.Y)) / 2) * size + from.X + ((to.X - from.X) / 2)] = null;
            }
            board[to.Y * size + to.X] = board[from.Y * size + from.X];
            board[from.Y * size + from.X] = null;
        }

        public static void MoveChecker(Move m, Checker[] board, int size)
        {
            MoveChecker(m.From, m.To, board, size);
        }

        public static Checker[] GetBoardCopy(Checker[] board)
        {
            var toReturn = new Checker[board.Length];
            var i = 0;
            foreach (var checker in board)
            {
                if (checker != null) toReturn[i] = new Checker(checker);
                else toReturn[i] = null;
                i++;
            }

            return toReturn;
        }

        public Checker[] GetBoardCopy()
        {
            var toReturn = new Checker[board.Length];
            var i = 0;
            foreach (var checker in board)
            {
                if (checker != null) toReturn[i] = new Checker(checker);
                else toReturn[i] = null;
                i++;
            }

            return toReturn;
        }

    }
}
