using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameAdilet
{
    internal class Map
    {
        public int Width; //Ширина
        public int Height; //Высота

        public char[,] Matrix; //Массив символов карты

        //Конструктор
        public Map(int width, int height, char[,] matrix)
        {
            Width = width;
            Height = height;
            Matrix = matrix;
        }
    }
}
