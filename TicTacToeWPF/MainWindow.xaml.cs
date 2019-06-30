using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Runtime.CompilerServices;

namespace TicTacToeWPF
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window/*, INotifyPropertyChanged*/
    {
        public static ObservableCollection<char> GameMapInterface { get; set; } = new ObservableCollection<char>();


        public static byte[] pressed = new byte[2];
        
        public static ManualResetEvent mre = new ManualResetEvent(false);

        Game game;

        public MainWindow()
        {
            InitializeComponent();
            game = new Game();
            DataContext = this;
        }


        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            var t = new Thread(Game.InitializeGame);
            t.Start(new String[] { playerOTypeBox.Text, playerXTypeBox.Text, gameTypeBox.Text });
            if (gameTypeBox.Text == "Continuous")
            {
                stopButton.IsEnabled = true; 
            }
            resetButton.IsEnabled = true;
        }

        private void GameButton_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            pressed[1] = byte.Parse(clickedButton.Name[6].ToString());
            pressed[0] = byte.Parse(clickedButton.Name[7].ToString());
            pressed[0]--;
            pressed[1]--;
            mre.Set();
            mre.Reset();
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            Game.SeamlessPlay = false;
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            resetButton.IsEnabled = false;
            Game.ResetGame();
        }
    }
}
