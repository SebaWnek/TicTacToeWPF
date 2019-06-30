using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace TicTacToeWPF
{
    class Human : Player
    {
        public Human(string name, int number) : base(name, number)
        {
        }

        public override byte[] NextMove()
        {
            MainWindow.mre.WaitOne();


            return MainWindow.pressed;
        }
    }
}
