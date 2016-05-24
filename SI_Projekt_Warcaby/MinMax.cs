using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Warcaby
{
    class MinMax
    {
        //protected static readonly int CHILDREN = ProjectSettings.CHILDREN;
        private static Random random = new Random();
        public int Depth { get; set; }
        public bool computerStarts { get; set; }
        
        protected Node root = new Node();
        private LinkedList<Node> leafs = new LinkedList<Node>();
        protected int numOfCheckedLeafs = 0; // Liczenie, ile liści sprawdzamy dla mini maxa i alfa bety

        /// <summary>
        /// Konstruktor ustawiajacy grę
        /// </summary>
        public MinMax(Game game)
        {
            this.game = game;
        }

        /// <summary>
        /// Referencja na instancję aktualnej rozgrywki
        /// </summary>
        protected Game game;

        /// <summary>
        ///  Ruch, który powinnien ustawiać algorytm na podstawie heurystyk etc. Będzie używany przez bota;
        /// </summary>
        private Game.Move move;
        public Game.Move SelectedMove
        {
            get
            {
                //tymczasowo pierwszy możliwy ruch
                //return game.getPossibleMoves(game.CurrentPlayer)[0];
                // docelowo :
                run();
                var max = 0;
                var index = 0;
                for (int i = 0; i < root.Children.Length; ++i)
                {
                    if (root.Children[i].Value > max)
                    {
                        max = root.Children[i].Value;
                        index = i;
                    }
                }
                if (root.Moves.Count > 0) move = root.Moves[index];
                return move;
            }
        }

        /// <summary>
        /// Punkt startowy algorytmu
        /// </summary>
        public string run()
        {
            DateTime before = DateTime.Now;
            runAlgorithm();
            DateTime after = DateTime.Now;
            string message = string.Format("\nWykonanie jednego ruchu zajelo: {0}.", after - before);
            message += " Wynik: " + root.Value;
            return message;
        }

        virtual protected void runAlgorithm()
        {
            
            DateTime before = DateTime.Now;
            root.CurrentPlayer = game.CurrentPlayer;
            Depth = game.TreeDepth;
            root.Moves = Game.getPossibleMoves(root.CurrentPlayer, game.GetBoardCopy(), game.BoardSize);
            buildTree(root, Depth, game.GetBoardCopy());
            DateTime after = DateTime.Now;
            string message = string.Format("Zbudowanie drzewa zajelo: {0}.", after - before);
            if(!(this is AlfaBeta))
                MessageBox.Show("MiniMax, liczba liści: " + numOfCheckedLeafs);
                
            if (computerStarts)
                max(root);
            else min(root);
            
        }

        virtual protected void buildTree(Node node, int levelsLeft, Game.Checker[] board)
        {
            if(node == null)
                throw new NullReferenceException("Niezainicjalizowany wierzcholek.");
            
            
            var moves = Game.getPossibleMoves(node.CurrentPlayer, board, game.BoardSize);
            node.Children = new Node[moves.Count];
            //node.Moves = moves; //We need moves only for root
            for (int i = 0; i < moves.Count; i++)
            {
                node.Children[i] = new Node();
                node.Children[i].Parent = node;
                node.Children[i].Value = -1;        //Nieobliczona wartość
                var newBoard = Game.GetBoardCopy(board);
                Game.MoveChecker(moves[i], newBoard, game.BoardSize);
                node.Children[i].Board = newBoard;
                node.Children[i].CurrentPlayer = node.CurrentPlayer == 0 ? 1 : 0;
                if (levelsLeft <= 1) //Inicjalizowanie liście - ostatni poziom głębokości w drzewie
                {
                    if (node.CurrentPlayer != game.CurrentPlayer)
                        node.Children[i].Value = ratePositions(newBoard, node.CurrentPlayer);
                    else node.Children[i].Value = 0;
                    leafs.AddLast(node.Children[i]);
                }
                else
                {
                    buildTree(node.Children[i], levelsLeft - 1, newBoard);
                }
                node.Children[i].Board = null; //RAM OPTIMIZATION, UGLY AS F**K
                //Garbage Collector is going to kill us
            }
        }

        virtual protected void max(Node node)
        {
            if(node == null)
                throw new NullReferenceException("Niezainicjalizowany wierzcholek.");
            if (node.Children == null)      //Jeśli jesteśmy w liściu, kończymy procedurę
                return;
            for (int i = 0; i < node.Children.Length; i++)
                if (node.Children[i].Value == -1)
                    min(node.Children[i]);
            for (int i = 0; i < node.Children.Length; i++)
                if (node.Children[i].Value > node.Value)
                    node.Value = node.Children[i].Value;

        }

        virtual protected void min(Node node)
        {
            if(node == null)
                throw new NullReferenceException("Niezainicjalizowany wierzcholek.");
            if (node.Children == null)      //Jeśli jesteśmy w liściu, kończymy procedurę
                return;
            node.Value = Int16.MaxValue;
            for (int i = 0; i < node.Children.Length; i++)
                if (node.Children[i].Value == -1)
                    max(node.Children[i]);
            for (int i = 0; i < node.Children.Length; i++)
                if (node.Children[i].Value < node.Value)
                    node.Value = node.Children[i].Value;
        }

        protected short ratePositions(Game.Checker[] board, int currentPlayer)
        {
            numOfCheckedLeafs++;
            var points = 0;
            short checkers = 0;
            short kings = 0;
            short canKill = 0;
            short pointsForLevel = 0;
            short atTheEdge = 0;

            var boardSize = game.BoardSize;

            for (var i = 0; i < board.Length; ++i)
            {
                if (board[i] == null) continue;
                var posx = i%game.BoardSize;
                var posy = i/game.BoardSize;
                if (board[i].owner == currentPlayer)
                {
                    checkers++;
                    if (Game.enemiesAroundToKill(posx, posy, board, game.BoardSize).Count > 0)
                    {
                        canKill++;
                    }
                    if (currentPlayer == 0)
                    {
                        pointsForLevel += (short) posy;
                    }
                    else
                    {
                        if (!board[i].isKing) pointsForLevel += (short) (boardSize - posy - 1);
                    }
                    if (board[i].isKing) kings++;
                    if (posx == 0 || posx == boardSize - 1 || posy == 0 || posy == boardSize - 1)
                    {
                        atTheEdge++;
                    }
                }
            }
            points += checkers*10 + kings * 50 + canKill*30 + pointsForLevel + atTheEdge*2;

            //return (short)random.Next(20);
            return (short) points;
        }
    }
}
