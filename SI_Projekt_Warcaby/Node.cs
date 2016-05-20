using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warcaby
{
    /// <summary>
    /// Reprezentuje jeden stan z drzewa gry. Pola Value, Alfa i Beta są typu short w celu oszczędności pamięci.
    /// (Zapisane na 16 bitach zamiast na 32).
    /// </summary>
    class Node
    {
        public short Value {get; set;}
        public Node[] Children;
        public List<Game.Move> Moves;
        public Node Parent;
        public short Alfa { get; set; }
        public short Beta { get; set; }
        public bool Disabled { get; set; }
        public int CurrentPlayer { get; set; }
        public Game.Checker[] Board { get; set; }
    }
}
