using System;

namespace PilotBrothersSafe.Models
{
    public class GameArgs : EventArgs
    {
        public GameArgs(bool isWin, int numberOfMoves)
        {
            IsWin = isWin;
            NumberOfMoves = numberOfMoves;
        }

        public bool IsWin { get; private set; }
        public int NumberOfMoves { get; private set; }
    }
}