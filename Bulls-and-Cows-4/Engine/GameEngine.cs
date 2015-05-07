using System;
using System.Text;

namespace BullsAndCowsGame.Engine
{
    class GameEngine
    {
        public static void GenerateNumber()
        {
            Random rnd = new Random();
            int num = rnd.Next(1000, 10000);
            Game._generatedNumber = num.ToString();
        }
        public static void CalculateBullsAndCowsCount(
            string playerInput, string generatedNumber, out int bullsCount, out int cowsCount)
        {
            bullsCount = 0;
            cowsCount = 0;
            var playerNumber = new StringBuilder(playerInput);
            var number = new StringBuilder(generatedNumber);
            for (var i = 0; i < playerNumber.Length; i++)
                if (playerNumber[i] == number[i])
                {
                    bullsCount++;
                    playerNumber.Remove(i, 1);
                    number.Remove(i, 1);
                    i--;
                }

            for (var i = 0; i < playerNumber.Length; i++)
            {
                for (var j = 0; j < number.Length; j++)
                {
                    if (playerNumber[i] != number[j]) continue;
                    cowsCount++;
                    playerNumber.Remove(i, 1);
                    number.Remove(j, 1);
                    i--;
                    break;
                }
            }
        }
    }
}
