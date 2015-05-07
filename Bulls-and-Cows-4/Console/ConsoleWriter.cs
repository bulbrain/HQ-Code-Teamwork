using System;
using System.Text;

namespace BullsAndCowsGame.Writer
{
    public class ConsoleWriter
    {

        internal static void PrintWelcomeMessage()
        {
            Console.WriteLine(Constants.WelcomeMessage);
        }

        internal static void PrintCommandMessage()
        {
            Console.WriteLine(Constants.CommandMessage);
        }

        internal static void PrintWrongNumberMessage(int bullsCount, int cowsCount)
        {
            Console.WriteLine(Constants.WrongNumberMessage, bullsCount, cowsCount);
        }

        internal static void PrintWrongCommandMessage()
        {
            Console.WriteLine(Constants.WrongCommandMessage);
        }

        internal static void PrintHelp(StringBuilder helpNumber)
        {
            Console.WriteLine(Constants.AllowedHelpMessage + helpNumber);
        }

        internal static void DontPrintHelp()
        {
            Console.WriteLine(Constants.NotAllowedHelpMessage);
        }

        internal static void WinWithountCheats(int attempts)
        {
            Console.WriteLine(Constants.CongratsMessage + "{0} attempts.", attempts);
            Console.WriteLine(Constants.ScoreBoardMessage);
        }

        internal static void WinWithCheats(int attempts, int cheats)
        {
            Console.WriteLine(Constants.CongratsMessage + "{0} attempts and {1} cheats.", attempts, cheats);
            Console.WriteLine(Constants.NoScoreBoardMessage);
        }
    }
}
