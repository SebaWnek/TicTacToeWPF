using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows;
using System.Threading;

namespace TicTacToeWPF
{
    public class Game
    {
        public static Player player1;
        public static Player player2;
        private static bool seamlessPlay;
        private static int gameCounter = 0;
        private static int[] score = new int[] { 0,0};
        public static int[] Score
        {
            get { return score; }
            set
            {
                score = value;
                OnStaticPropertyChanged("Score");
            }
        }
        public static int GameCounter
        {
            get { return gameCounter; }
            set
            {
                gameCounter = value;
                OnStaticPropertyChanged("GameCounter");
            }
        }

        public static bool SeamlessPlay { get => seamlessPlay; set => seamlessPlay = value; }

        public static int[,] gameMap;

        public static Player CreatePlayer(string playerType, string name, int number)
        {
            switch (playerType)
            {
                case "Human":
                    return new Human(name, number);
                case "Easy":
                    return new Easy(name, number);
                case "Medium":
                    return new Medium(name, number);
                case "Hard":
                    return new Hard(name, number);
                default:
                    throw new Exception("Wrong player type");
            }
        }

        public static void InitializeGame(object datatmp)
        {
            string[] data = datatmp as string[];
            player1 = CreatePlayer(data[0], "O", 1);
            player2 = CreatePlayer(data[1], "X", 10);
            SeamlessPlay = true ? data[2] == "Continuous" : false;
            PlayGame();
        }

        public static void PlayGame()
        {
            gameMap = new int[3, 3];
            Application.Current.Dispatcher.Invoke(() =>
            {
                MainWindow.GameMapInterface.Clear();
            });
            for (int i = 0; i < 9; i++)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    MainWindow.GameMapInterface.Add(' ');
                });
            }
            int movesDone = 0;
            byte[] nextPosition;
            Player currentPlayer;
            bool won = false;

            while (movesDone < 9)
            {
                currentPlayer = SelectPlayer(movesDone);
                nextPosition = currentPlayer.NextMove();
                gameMap[nextPosition[0], nextPosition[1]] = currentPlayer.NumericValue;
                Application.Current.Dispatcher.Invoke(() =>
                {
                    MainWindow.GameMapInterface[nextPosition[0] + 3 * nextPosition[1]] = currentPlayer.Name[0];
                });
                if (CheckIfWon(currentPlayer))
                {
                    if (!seamlessPlay)
                    {
                        MessageBox.Show($"{currentPlayer.Name} has won!"); 
                    }
                    currentPlayer.Score++;
                    won = true;
                    break;
                }
                movesDone++;
            }

            if (!won && !seamlessPlay)
            {
                MessageBox.Show("It's a tie!");
            }

            GameCounter++;
            Score = new int[] { player1.Score, player2.Score};

            if (SeamlessPlay)
            {
                Thread.Sleep(1000);
                PlayGame();
            }
        }

        private static Player SelectPlayer(int move)
        {
            if ((move + GameCounter) % 2 == 0)
            {
                return player1;
            }
            else
            {
                return player2;
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
                    mapSums[i] += gameMap[i, j];
                }
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    mapSums[i + 3] += gameMap[j, i];
                }
            }
            mapSums[6] = gameMap[0, 0] + gameMap[1, 1] + gameMap[2, 2];
            mapSums[7] = gameMap[0, 2] + gameMap[1, 1] + gameMap[2, 0];

            return mapSums;
        }

        private static bool CheckIfWon(Player player)
        {
            int[] mapSums = MapSums();
            if (mapSums.Contains(3) || mapSums.Contains(30))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static event PropertyChangedEventHandler StaticPropertyChanged;

        private static void OnStaticPropertyChanged(string propertyName)
        {
            var handler = StaticPropertyChanged;
            if (handler != null)
            {
                handler(null, new PropertyChangedEventArgs(propertyName));
            }
        }

        internal static void ResetGame()
        {
            Score = new int[] { 0, 0 };
            GameCounter = 0;
            MainWindow.GameMapInterface.Clear();
            gameMap = new int[3, 3];
        }
    }
}
