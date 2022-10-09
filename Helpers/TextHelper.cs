using PilotBrothersSafe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PilotBrothersSafe.Helpers
{
    public static class  TextHelper
    {
        public static string GetHelpMessage()
        {
            return "Чтобы начать играть введите в поле ввода количество рычагов на одной стороне и нажмите на кнопку 'Начать игру'"
            + "Число рычагов в диапазаоне от 2 до 15."
            + "При попытке начать сначала, просто нажмите 'Начать игру'(также можно изменить число в поле)."
            + "После победы нажатие на рычаги не даст никакого результата, для этого просто нажмите кнопку 'Начать игру', тогда процесс повторится.";
        }

        public static string GetValidationMessage()
        {
            return "Число должно быть не меньше 2-ух и не больше 15-ти";
        }

        public static string GetWinMessage(int numberMoves)
        {
            return $"Победа! Вы совершили {numberMoves} ходов.";
        }

        public static string GetAskForRestart()
        {
            return "Начать сначала?";
        }
    }
}
