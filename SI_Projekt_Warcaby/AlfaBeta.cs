using System;
using System.Collections.Generic;

namespace Warcaby
{
    class AlfaBeta : MinMax
    {

        /// <summary>
        /// Wymagany konstruktor
        /// </summary>
        public AlfaBeta(Game g)
            :base(g)
        {

        }

        protected override void runAlgorithm()
        {
            root.Alfa = Int16.MinValue;
            root.Beta = Int16.MaxValue;
            base.runAlgorithm();
        }
        protected override void max(Node node)
        {
            if (node == null)
                throw new NullReferenceException("Niezainicjalizowany wierzcholek.");
            if (node.Children == null)
                node.Value = ratePositions();
            else
            {
                for (int i = 0; i < CHILDREN; i++)
                {
                    if (node.Children[i].Value == -1 && node.Alfa < node.Beta)
                        min(node.Children[i]);
                    else if (node.Alfa >= node.Beta)
                        node.Children[i].Disabled = true;
                }
                for (int i = 0; i < CHILDREN; i++)
                    if (node.Children[i].Value > node.Value && !node.Children[i].Disabled)
                        node.Value = node.Children[i].Value;
            }
            Node parent = node.Parent;
            if (parent != null && node.Value < parent.Beta)
            {
                parent.Beta = node.Value;
                for (int i = 0; i < CHILDREN; i++)
                {
                    if (parent.Children[i] != node)
                        parent.Children[i].Beta = node.Value;
                }
            }
                

        }

        protected override void min(Node node)
        {
            if (node == null)
                throw new NullReferenceException("Niezainicjalizowany wierzcholek.");
            if (node.Children == null)
                node.Value = ratePositions();
            else
            {
                node.Value = Int16.MaxValue;
                for (int i = 0; i < CHILDREN; i++)
                {
                    if (node.Children[i].Value == -1 && node.Alfa < node.Beta)
                        max(node.Children[i]);
                    else if (node.Alfa >= node.Beta)
                        node.Children[i].Disabled = true;
                }
                for (int i = 0; i < CHILDREN; i++)
                    if (node.Children[i].Value < node.Value && !node.Children[i].Disabled)
                        node.Value = node.Children[i].Value;
            }
            
            Node parent = node.Parent;
            if (parent != null && node.Value > parent.Alfa)
            {
                parent.Alfa = node.Value;
                for (int i = 0; i < CHILDREN; i++)
                {
                    if (parent.Children[i] != node)
                        parent.Children[i].Alfa = node.Value;
                }
            }
                
            
        }

        protected override void buildTree(Node node, int levelsLeft)
        {
            if (node == null)
                throw new NullReferenceException("Niezainicjalizowany wierzcholek.");

            node.Children = new Node[CHILDREN];
            for (int i = 0; i < CHILDREN; i++)
            {
                node.Children[i] = new Node();
                node.Children[i].Parent = node;
                node.Children[i].Value = -1;        //Nieobliczona wartość
                node.Children[i].Alfa = Int16.MinValue;
                node.Children[i].Beta = Int16.MaxValue;
                node.Children[i].Disabled = false;
                if (levelsLeft > 1)
                    //node.Children[i].Value = ratePositions();
                    buildTree(node.Children[i], levelsLeft - 1);
            }
        }
    }
}
