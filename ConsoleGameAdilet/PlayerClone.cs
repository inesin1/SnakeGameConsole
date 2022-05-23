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

        public PlayerClone(int timer)
        {
            Timer = timer;
        }
    }
}
