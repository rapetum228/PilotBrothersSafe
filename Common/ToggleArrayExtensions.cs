using PilotBrothersSafe.Controls;
using System;

namespace PilotBrothersSafe.Common
{
    public static class ToggleArrayExtensions
    {
        public static void ForEach(this Toggle[,] toggles, Action<Toggle, int, int> process)
        {
            for (int i = 0; i < toggles.GetLength(0); i++)
            {
                for (int j = 0; j < toggles.GetLength(1); j++)
                {
                    var toggle = toggles[i, j];
                    process(toggle, i, j);
                }
            }
        }

        public static void ForEach(this Toggle[,] toggles, Action<Toggle> process)
        {
            toggles.ForEach((toggle, x, y) => process(toggle));
        }
    }
}
