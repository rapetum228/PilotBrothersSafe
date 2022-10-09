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

        //private int _commonHorizontalAngle = 0;
        //private int _commonVerticalAngle;
        //private int _currentSumAngles = 0;

        private DelegateCommand LeftButtonClickCommand { get; }

        public ToggleFieldLogic()
        {
            LeftButtonClickCommand = new DelegateCommand(LeftButtonClick);
        }

        public IDataProvider DataProvider { get; set; }

        internal void AttachToggles(Toggle[,] toggles)
        {
            toggles.ForEach(
                (toggle, x, y) =>
                {
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
            DataProvider.MoveToggle();
            if (DataProvider.CurrentSumAngles == DataProvider.SumAnglesInVertical 
                || DataProvider.CurrentSumAngles == DataProvider.SumAnglesInHorizontal)
            {
                DataProvider.GameOnOver(true, DataProvider.NumberOnMoves);
            }
        }

        private void ChangeStateByXY(int x, int y)
        {
            if (DataProvider.LenghtOnOneSide == 0)
            {
                return;
            }
            for (int i = 0; i < DataProvider.LenghtOnOneSide; i++)
            {
                DataProvider.RefreshCurrentSumOfAngles(_toggles[x, i].ChangeState());
                DataProvider.RefreshCurrentSumOfAngles(_toggles[i, y].ChangeState());

            }
            DataProvider.RefreshCurrentSumOfAngles(_toggles[x, y].ChangeState());
        }

        public void RandomMixStates()
        {
            var random = new Random();
            int x = random.Next(DataProvider.LenghtOnOneSide);
            int y = random.Next(DataProvider.LenghtOnOneSide);
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
