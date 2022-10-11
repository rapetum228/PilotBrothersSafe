using System;

namespace PilotBrothersSafe.Models
{
    public class GameArgs : EventArgs
    {
        public GameArgs(int numberOfMoves)
        {
            NumberOfMoves = numberOfMoves;
        }
        public int NumberOfMoves { get; private set; }
    }
}