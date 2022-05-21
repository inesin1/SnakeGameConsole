using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameAdilet
{
    internal class Player
    {
        public int X; //Позиция по X
        public int Y; //Позиция по Y

        public char Sym; //Символ, который его обозначает

        public Player(int x, int y, char sym)
        {
            X = x;
            Y = y;
            Sym = sym;
        }
    }
}
