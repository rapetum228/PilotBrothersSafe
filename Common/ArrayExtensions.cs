using PilotBrothersSafe.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PilotBrothersSafe.Common
{
    public static class ArrayExtensions
    {
        /// <summary>
        /// For each implementation for toggles array.
        /// </summary>
        /// <param name="toggles">Buttons array.</param>
        /// <param name="process">Process each one button.</param>
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

        /// <summary>
        /// For each implementation for array.
        /// </summary>
        /// <param name="toggles">Buttons array.</param>
        /// <param name="process">Process each one button.</param>
        public static void ForEach(this Toggle[,] toggles, Action<Toggle> process)
        {
            toggles.ForEach((toggle, x, y) => process(toggle));
        }
    }
}
