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
        public Direction Dir; //Направление
        public int Length = 0; //Длина

        public char Sym; //Символ, который его обозначает

        //Список направлений
        public enum Direction
        {
            Left,
            Right,
            Up,
            Down
        }

        public Player(int x = 8, int y = 8, Direction dir = Direction.Right, char sym = '*')
        {
            X = x;
            Y = y;
            Dir = dir;
            Sym = sym;
        }
    }
}
