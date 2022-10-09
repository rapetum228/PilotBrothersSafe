using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PilotBrothersSafe.Models
{
    public interface IDataProvider
    {
        event EventHandler<GameArgs> Gameover;
        int LenghtOnOneSide { get; set; }
        int SumAnglesInHorizontal { get; }
        int SumAnglesInVertical { get; }
        int CurrentSumAngles { get; }
        int NumberOnMoves { get; }
        void MoveToggle();
        void GameOnOver(bool isWin, int counter);
        void ResetCounter(ToggleState state);
        void RefreshCurrentSumOfAngles(int angle);

    }
}
