using PilotBrothersSafe.Common;
using PilotBrothersSafe.Helpers;
using PilotBrothersSafe.Models;
using System.Windows;

namespace PilotBrothersSafe.ViewModels
{
    public class MainWindowViewModel : Notifier
    {

        private IDataProvider _dataProvider;
        private int _sizeField;
        private Thickness _marginCap;
        private Visibility _visibilityBox;
        private bool _boxIsEnabled;
        private bool _startbuttonIsEnabled;

        public MainWindowViewModel()
        {
            VisibilityBox = Visibility.Collapsed;
            MarginCap = new Thickness(0, 0, 0, 0);
            BoxIsEnabled = false;
            NewGameCommand = new DelegateCommand(p => NewGame());
            HelpCommand = new DelegateCommand(p => Help());
        }

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
        public int SizeField
        {
            get { return _sizeField; }
            set
            {
                _sizeField = value;
                OnPropertyChanged(nameof(SizeField));
            }
        }
        public Thickness MarginCap
        {
            get { return _marginCap; }
            set
            {
                _marginCap = value;
                OnPropertyChanged(nameof(MarginCap));
            }
        }
        public Visibility VisibilityBox
        {
            get => _visibilityBox;
            set
            {
                _visibilityBox = value;
                OnPropertyChanged(nameof(VisibilityBox));
            }
        }
        public bool BoxIsEnabled
        {
            get => _boxIsEnabled;
            set
            {
                _boxIsEnabled = value;
                OnPropertyChanged(nameof(BoxIsEnabled));
            }
        }
        public bool StartButtonIsEnabled
        {
            get => _startbuttonIsEnabled;
            set
            {
                _startbuttonIsEnabled = value;
                OnPropertyChanged(nameof(StartButtonIsEnabled));
            }
        }
        public DelegateCommand NewGameCommand { get; private set; }
        public DelegateCommand HelpCommand { get; private set; }
        private void NewGame()
        {
            if (!ValidationText())
            {
                return;
            }
            if (DataProvider != null)
            {
                MessageBoxResult result = MessageBox.Show(TextHelper.GetAskForRestart(), "Предупреждение", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.No)
                {
                    return;
                }
            }

            DataProvider = new DataProvider(SizeField);
            MarginCap = new Thickness(0, 0, 0, 0);
            DataProvider.Gameover += OnGameover;
            VisibilityBox = Visibility.Visible;
            BoxIsEnabled = true;

        }

        private void Help()
        {
            MessageBox.Show(TextHelper.GetHelpMessage());
        }

        private bool ValidationText()
        {
            if (SizeField < 2 || SizeField > 15)
            {
                MessageBoxResult result = MessageBox.Show(TextHelper.GetValidationMessage(), "Внимание", MessageBoxButton.OK);
                return false;
            }
            return true;
        }

        private void OnGameover(object sender, GameArgs gameArgs)
        {
            MessageBox.Show(TextHelper.GetWinMessage(gameArgs.NumberOfMoves));
            MarginCap = new Thickness(0, 0, 0, 30);
            DataProvider.Gameover -= OnGameover;
            BoxIsEnabled = false;
        }
    }
}
