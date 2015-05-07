using System;
using System.Text;
using BullsAndCowsGame.Writer;

namespace BullsAndCowsGame.Engine
{
    class Cheats
    {
        private StringBuilder _helpNumber;
        public static string HelpPattern;
        public static void GenerateHelpPattern()
        {
            string[] helpPaterns =
            {
                "1234", "1243", "1324", "1342", "1432", "1423",
                "2134", "2143", "2314", "2341", "2431", "2413",
                "3214", "3241", "3124", "3142", "3412", "3421",
                "4231", "4213", "4321", "4312", "4132", "4123"
            };

            var randomNumberGenerator = new Random(DateTime.Now.Millisecond);
            var randomPaternNumber = randomNumberGenerator.Next(helpPaterns.Length - 1);
            Game.HelpPattern = helpPaterns[randomPaternNumber];
        }

        public int ShowHelp(int cheats)
        {
            if (cheats < 4)
            {
                RevealDigit(cheats);
                cheats++;
                ConsoleWriter.PrintHelp(_helpNumber);
            }
            else
            {
                ConsoleWriter.DontPrintHelp();
            }

            return cheats;
        }

        private void RevealDigit(int cheats)
        {
            if (HelpPattern == null)
            {
                Cheats.GenerateHelpPattern();
            }

            var digitToReveal = HelpPattern[cheats] - '0';
            _helpNumber[digitToReveal - 1] = Game._generatedNumber[digitToReveal - 1];
        }
    }
}
