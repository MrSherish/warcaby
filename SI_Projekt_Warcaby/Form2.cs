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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        internal void Populate(Node root)
        {
            bool alfabeta = false;
            Node node_to_select = null;

            TreeNode node = new TreeNode(root.Value.ToString());
            TreeNode node_to_select_in_tree = new TreeNode(root.Value.ToString());
            treeView1.BeginUpdate();
            treeView1.Nodes.Clear();
            treeView1.Nodes.Add(node);

            if (root.isLost != false) alfabeta = true;
            node_to_select = RecursiveMax(root, ProjectSettings.TREE_VIEW_DEPTH);
            RecursivePopulate(root, node, node_to_select_in_tree, node_to_select, ProjectSettings.TREE_VIEW_DEPTH, alfabeta);
            treeView1.SelectedNode = node_to_select_in_tree;
            
            treeView1.EndUpdate();
            treeView1.ExpandAll();
            
            treeView1.Refresh();
            treeView1.Invalidate();

        }
       
        internal void RecursivePopulate(Node root, TreeNode treenode, TreeNode to_select_in_tree, Node to_select, int depth, bool ab)
        {
            int length;
            if (root.Children == null) length = 1;
            else length = root.Children.Length;
            for (int i = 0; i < length; i++)
            {
                
                if (root.Children[i].Children == null)
                {
                    TreeNode node = null;
                    if (to_select == root.Children[i])
                    {
                        node = new TreeNode(">>>> " + root.Children[i].Value.ToString() + " <<<<");
                        node.ForeColor = Color.White;
                        node.BackColor = Color.DarkOliveGreen;
                        to_select_in_tree = node;
                    }
                    else
                    {
                        node = new TreeNode(root.Children[i].Value.ToString());
                        if (root.isLost == false && ab == true) node.ForeColor = Color.Red;
                    }

                     treenode.Nodes.Add(node);
                }
                else
                {
                    TreeNode node = null;
                   
                    node = new TreeNode(root.Value.ToString());
                    treenode.Nodes.Add(node);
                    if (root.isLost == false && ab == true) node.ForeColor = Color.Red;
                    RecursivePopulate(root.Children[i], node, to_select_in_tree, to_select, depth - 1,ab);
                    
                }
            }
        }

        /*
        internal void RecursivePopulate(Node root, TreeNode treenode, TreeNode to_select_in_tree, Node to_select, int depth, bool ab)
        {
            int length;
            if (root.Children == null || depth==0) length = 1;
            else length = root.Children.Length;
            for (int i = 0; i < length; i++)
            {
                
                TreeNode node = null;
                /*
                if (root == to_select)
                {
                    if (root.Value ==95)
                    {
                        int c = 45;
                    }
                    node = new TreeNode(">>>> " + root.Value.ToString() + " <<<<");
                    //if (root.isLost == false && ab == true) node.ForeColor = Color.Red;
                    node.ForeColor = Color.White;
                        node.BackColor = Color.DarkOliveGreen;
                    to_select_in_tree = node;
                }
                else //
                    node = new TreeNode(root.Value.ToString());
                treenode.Nodes.Add(node);
                if (root.isLost == false && ab == true) node.ForeColor = Color.Red;
                if (depth > 0)

                    RecursivePopulate(root.Children[i], node, to_select_in_tree, to_select, depth - 1,ab);
            }
        }
*/
        internal Node RecursiveMax(Node root, int depth)
        {
            while (root.ChosenOne != null && depth>0)
            {
                root = root.ChosenOne;
                depth--;
            }
            return root;
        }
    }
}