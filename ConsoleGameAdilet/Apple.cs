using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameAdilet
{
    internal class Apple
    {
        public int X; //Положение по X
        public int Y; //Положение по Y

        public char Sym; //Символ

        public Apple(int x = 3, int y = 3, char sym = 'A')
        {
            X = x;
            Y = y;
            Sym = sym;
        }
    }
}
