using System.Linq;
using BullsAndCowsGame.Player;

namespace BullsAndCowsGame.Writer
{
    class ConsoleReader
    {
        public static bool IsValidInput(string playerInput)
        {
            if (playerInput == string.Empty || playerInput.Length != Constants.NumberLenght)
            {
                return false;
            }

            return playerInput.All(char.IsDigit);
        }

        public static PlayerCommand PlayerInputToPlayerCommand(string playerInput)
        {
            string input = playerInput.ToLower();
            PlayerCommand command;
            switch (input)
            {
                case "top": command = PlayerCommand.Top;
                    break;
                case "restart": command = PlayerCommand.Restart;
                    break;
                case "help": command = PlayerCommand.Help;
                    break;
                case "exit": command = PlayerCommand.Exit;
                    break;
                default: command = PlayerCommand.Other;
                    break;
            }
            return command;
        }
    }
}
