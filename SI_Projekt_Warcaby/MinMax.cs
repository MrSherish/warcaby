using System;
using System.Collections.Generic;
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
                return game.getPossibleMoves(game.CurrentPlayer)[0];
                // docelowo :
                //return move;
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
            try
            {
                DateTime before = DateTime.Now;
                root.CurrentPlayer = game.CurrentPlayer;
                buildTree(root, Depth, game.GetBoardCopy());
                DateTime after = DateTime.Now;
                string message = string.Format("Zbudowanie drzewa zajelo: {0}.", after - before);
                //MessageBox.Show(message);
                
                if (computerStarts)
                    max(root);
                else min(root);
            }
            catch (NullReferenceException e)
            {
                MessageBox.Show("Wyjatek: " + e.Message);
            }
        }

        virtual protected void buildTree(Node node, int levelsLeft, Game.Checker[] board)
        {
            if(node == null)
                throw new NullReferenceException("Niezainicjalizowany wierzcholek.");
            
            
            var moves = Game.getPossibleMoves(node.CurrentPlayer, board, game.BoardSize);
            node.Children = new Node[moves.Count];
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
                    node.Children[i].Value = ratePositions(newBoard);
                    leafs.AddLast(node.Children[i]);
                }
                else
                {
                    buildTree(node.Children[i], levelsLeft - 1, newBoard);
                }
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

        protected short ratePositions(Game.Checker[] board)
        {
            return (short)random.Next(20);
        }
    }
}
