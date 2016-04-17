using System;
using System.Collections.Generic;
using System.Windows;

namespace Warcaby
{
    class MinMax
    {
        protected static readonly int CHILDREN = ProjectSettings.CHILDREN;
        private static Random random = new Random();
        public int Depth { get; set; }
        public bool computerStarts { get; set; }
        
        protected Node root = new Node();
        private LinkedList<Node> leafs = new LinkedList<Node>();

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
                buildTree(root, Depth);
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

        virtual protected void buildTree(Node node, int levelsLeft)
        {
            if(node == null)
                throw new NullReferenceException("Niezainicjalizowany wierzcholek.");
            
            node.Children = new Node[CHILDREN];
            for(int i = 0; i < CHILDREN; i++)
            {
                node.Children[i] = new Node();
                node.Children[i].Parent = node;
                node.Children[i].Value = -1;        //Nieobliczona wartość
                if (levelsLeft <= 1)         //Inicjalizowanie liście - ostatni poziom głębokości w drzewie
                {
                    node.Children[i].Value = ratePositions();
                    leafs.AddLast(node.Children[i]);
                } 
                else buildTree(node.Children[i], levelsLeft - 1);
            }
        }

        virtual protected void max(Node node)
        {
            if(node == null)
                throw new NullReferenceException("Niezainicjalizowany wierzcholek.");
            if (node.Children == null)      //Jeśli jesteśmy w liściu, kończymy procedurę
                return;
            for (int i = 0; i < CHILDREN; i++)
                if (node.Children[i].Value == -1)
                    min(node.Children[i]);
            for (int i = 0; i < CHILDREN; i++)
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
            for (int i = 0; i < CHILDREN; i++)
                if (node.Children[i].Value == -1)
                    max(node.Children[i]);
            for (int i = 0; i < CHILDREN; i++)
                if (node.Children[i].Value < node.Value)
                    node.Value = node.Children[i].Value;
        }

        protected short ratePositions()
        {
            return (short)random.Next(20);
        }
    }
}
