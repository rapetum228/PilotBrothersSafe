using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PilotBrothersSafe.Models
{
    public class DataProvider : IDataProvider
    {
        public DataProvider(int lenghtOnOneSide)
        {
            LenghtOnOneSide = lenghtOnOneSide;
        }

        public int LenghtOnOneSide { get; set; }

        public int SumAnglesInHorizontal => 0;

        public int SumAnglesInVertical => LenghtOnOneSide * LenghtOnOneSide * (int)ToggleState.Vertical;

        public int CurrentSumAngles { get; private set; }

        public int NumberOnMoves { get; private set; }

        public event EventHandler<GameArgs> Gameover;

        public void GameOnOver(bool isWin, int counter)
        {
            Gameover?.Invoke(this, new GameArgs(isWin, counter));
        }

        public void MoveToggle()
        {
            NumberOnMoves += 1;
        }

        public void RefreshCurrentSumOfAngles(int angle)
        {
            CurrentSumAngles += angle;
        }

        public void ResetCounter(ToggleState state)
        {
            CurrentSumAngles = (int) state * LenghtOnOneSide * LenghtOnOneSide;
            NumberOnMoves = 0;
        }
    }
}
