using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warcaby
{
    class ProjectSettings
    {
        //Liczba liści w drzewie: CHILDREN ^ DEPTH
        public static readonly int CHILDREN = 8;
        public static readonly int DEPTH = 7;
        public static readonly int TREE_VIEW_DEPTH = Int16.MaxValue; //3;
        public static readonly int SPEED = 5000;
    }
}
