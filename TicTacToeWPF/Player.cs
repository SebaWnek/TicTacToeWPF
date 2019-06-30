using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeWPF
{
    public abstract class Player : INotifyPropertyChanged
    {
        public int NumericValue { get; set; } = 1;
        public string Name { get; set; }
        private int score = 0;
        public int Score
        {
            get { return score; }
            set
            {
                score = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public abstract byte[] NextMove();

        protected Player(string name, int number)
        {
            Name = name;
            NumericValue = number;
        }
    }
}
