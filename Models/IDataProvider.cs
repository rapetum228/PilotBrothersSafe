using System;

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
        void GameOnOver();
        void ResetCounter(ToggleState state);
        void RefreshCurrentSumOfAngles(int angle);

    }
}
