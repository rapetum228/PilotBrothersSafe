using PilotBrothersSafe.Common;
using PilotBrothersSafe.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Input;

namespace PilotBrothersSafe.Models
{
    public class ToggleFieldLogic
    {
        private Toggle[,] _toggles;

        private int _commonHorizontalAngle = 0;
        private int _commonVerticalAngle;
        private int _currentSumAngles = 0;

        private IDataProvider _gameOver;
        private DelegateCommand LeftButtonClickCommand { get; }

        public ToggleFieldLogic()
        {
            LeftButtonClickCommand = new DelegateCommand(LeftButtonClick);
        }

        public int LenghtOnOneSide { get; set; }

        internal void AttachToggles(Toggle[,] toggles)
        {
            _commonVerticalAngle = 0;
            _currentSumAngles = 0;
            toggles.ForEach(
                (toggle, x, y) =>
                {
                    _commonVerticalAngle += (int)ToggleState.Vertical;
                    _currentSumAngles += (int)(toggle.CurrentToggleState);
                    var info = new ToggleInfo(x, y, toggle);
                    toggle.Tag = info;
                    toggle.InputBindings.Add(
                        new InputBinding(LeftButtonClickCommand, new MouseGesture(MouseAction.LeftClick))
                        {
                            CommandParameter = info
                        });
                });

            _toggles = toggles;
        }

        private void LeftButtonClick(object parameter)
        {
            var info = (ToggleInfo)parameter;
            int x = info.X;
            int y = info.Y;

            ChangeStateByXY(x, y);
            if (_currentSumAngles == _commonHorizontalAngle || _currentSumAngles == _commonVerticalAngle)
            {
                MessageBox.Show($"{_currentSumAngles} - это победа");
                _gameOver.GameoverEvent();
            }
        }

        private void ChangeStateByXY(int x, int y)
        {
            if (LenghtOnOneSide == 0)
            {
                return;
            }
            for (int i = 0; i < LenghtOnOneSide; i++)
            {
                _currentSumAngles += _toggles[x, i].ChangeState();
                _currentSumAngles += _toggles[i, y].ChangeState();

            }
            _currentSumAngles += _toggles[x, y].ChangeState();
        }

        public void RandomMixStates()
        {
            var random = new Random();
            int x = random.Next(LenghtOnOneSide);
            int y = random.Next(LenghtOnOneSide);
            ChangeStateByXY(x, y);
        }

        private class ToggleInfo
        {
            public ToggleInfo(int x, int y, Toggle toggle)
            {
                X = x;
                Y = y;
                Toggle = toggle;
            }
            public Toggle Toggle { get; }
            public int X { get; }
            public int Y { get; }
        }
    }
}
