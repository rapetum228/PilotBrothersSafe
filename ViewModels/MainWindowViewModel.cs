using PilotBrothersSafe.Common;
using PilotBrothersSafe.Models;
using PilotBrothersSafe.Prism;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PilotBrothersSafe.ViewModels
{
    public class MainWindowViewModel : Notifier
    {

        private IDataProvider _dataProvider;
        public IDataProvider DataProvider
        {
            get
            {
                return _dataProvider;
            }

            private set
            {
                _dataProvider = value;
                OnPropertyChanged(nameof(DataProvider));
            }
        }

        private int _sizeField;

        public MainWindowViewModel()
        {
            VisibilityBox = Visibility.Collapsed;
            MarginCap = new Thickness(0, 0, 0, 0);
            NewGameCommand = new DelegateCommand(p => CreateNewField());
        }

        public int SizeField
        {
            get { return _sizeField; }
            set
            {
                _sizeField = value;
                OnPropertyChanged(nameof(SizeField));
            }
        }

        public DelegateCommand NewGameCommand { get; private set; }

        private void CreateNewField()
        {
            if (DataProvider != null)
            {
                MessageBoxResult result = MessageBox.Show("Начать сначала?", "OK", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.No)
                {
                    return;
                }
            }
            
            DataProvider = new DataProvider(SizeField);
            MarginCap = new Thickness(0, 0, 0, 0);
            DataProvider.Gameover += OnGameover;
            VisibilityBox = Visibility.Visible;

        }

        private Thickness _marginCap;
        public Thickness MarginCap
        {
            get { return _marginCap; }
            set
            {
                _marginCap = value;
                OnPropertyChanged(nameof(MarginCap));
            }
        }

        private Visibility _visibilityBox;

        public Visibility VisibilityBox
        {
            get => _visibilityBox;
            set
            {
                _visibilityBox = value;
                OnPropertyChanged(nameof(VisibilityBox));
            }
        }

        private void OnGameover(object sender, GameArgs gameArgs)
        {
            if (gameArgs.IsWin)
            {
                MessageBox.Show($"Победа! Вы совершили {gameArgs.NumberOfMoves} ходов");
                MarginCap = new Thickness(0, 0, 0, 30);
            }
            DataProvider.Gameover -= OnGameover;
        }
    }
}
