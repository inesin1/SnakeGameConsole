using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameAdilet
{
    internal class PlayerClone : Player
    {
        public int Timer; //Таймер удаления

        public PlayerClone(int x, int y, int timer, char sym = '□')
        {
            X = x;
            Y = y;
            Sym = sym;
            Timer = timer;
        }
    }
}
