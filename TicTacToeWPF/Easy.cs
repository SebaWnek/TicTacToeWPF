using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeWPF
{
    class Easy : AI
    {
        public Easy(string name, int number) : base(name, number)
        {
        }

        public override byte[] NextMove()
        {
            FindEmptyCells();
            SimulateThinking();
            return emptyCells[rnd.Next(0, emptyCells.Count())];
        }
    }
}
