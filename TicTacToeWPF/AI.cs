using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace TicTacToeWPF
{
    abstract class AI : Player
    {
        static int MinTime { get; set; } = 50;
        static int MaxTime { get; set; } = 150;
        protected static Random rnd = new Random();
        protected List<byte[]> emptyCells = new List<byte[]>();
        protected List<byte[]> emptyCorners = new List<byte[]>();

        protected List<byte[]> emptyWalls = new List<byte[]>(); protected static void SimulateThinking()
        {
            Thread.Sleep(rnd.Next(MinTime, MaxTime));
        }

        protected void FindEmptyWalls()
        {
            emptyWalls.Clear();
            byte[,] walls = new byte[,] { { 0, 1 }, { 1, 0 }, { 1, 2 }, { 2, 1 } };
            for (int i = 0; i < 4; i++)
            {
                if (Game.gameMap[walls[i, 0], walls[i, 1]] == 0)
                {
                    emptyWalls.Add(new byte[] { walls[i, 0], walls[i, 1] });
                }
            }
        }

        protected bool FindEmptyCorners()
        {
            emptyCorners.Clear();
            for (byte i = 0; i < 2; i++)
            {
                for (byte j = 0; j < 2; j++)
                {
                    if (Game.gameMap[i * 2, j * 2] == 0)
                    {
                        emptyCorners.Add(new byte[] { (byte)(i * 2), (byte)(j * 2) });
                    }
                }
            }
            if (emptyCorners.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void FindEmptyCells()
        {
            emptyCells.Clear();
            for (byte i = 0; i < 3; i++)
            {
                for (byte j = 0; j < 3; j++)
                {
                    if (Game.gameMap[i, j] == 0)
                    {
                        emptyCells.Add(new byte[] { i, j });
                    }
                }
            }
        }

        public static int[] MapSums()
        {
            //1,2,3 - horizontal, 4,5,6 - vertical, 7 - descending, 8 - ascending
            int[] mapSums = new int[8];

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    mapSums[i] += Game.gameMap[i, j];
                }
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    mapSums[i + 3] += Game.gameMap[j, i];
                }
            }
            mapSums[6] = Game.gameMap[0, 0] + Game.gameMap[1, 1] + Game.gameMap[2, 2];
            mapSums[7] = Game.gameMap[0, 2] + Game.gameMap[1, 1] + Game.gameMap[2, 0];

            return mapSums;
        }

        protected AI(string name, int number) : base(name, number)
        {
        }
    }
}
