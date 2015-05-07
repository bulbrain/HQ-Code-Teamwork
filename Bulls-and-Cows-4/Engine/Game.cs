using System;
using System.Text;
using BullsAndCowsGame.Player;
using BullsAndCowsGame.Writer;

namespace BullsAndCowsGame.Engine
{
    internal class Game
    {
        private readonly ScoreBoard<Player.Player> _highScores;
        public static string _generatedNumber;
        private StringBuilder _helpNumber;
        public static string HelpPattern;

        public Game()
        {
            _highScores = new ScoreBoard<Player.Player>();
        }

        public ScoreBoard<Player.Player> HighScores
        {
            get { return _highScores; }
        }

        public void Start()
        {
            PlayerCommand enteredCommand;
            do
            {
                ConsoleWriter.PrintWelcomeMessage();
                GameEngine.GenerateNumber();
                var attempts = 0;
                var cheats = 0;
                _helpNumber = new StringBuilder("XXXX");
                HelpPattern = null;
                do
                {
                    ConsoleWriter.PrintCommandMessage();
                    var playerInput = Console.ReadLine();
                    enteredCommand = ConsoleReader.PlayerInputToPlayerCommand(playerInput);

                    if (enteredCommand == PlayerCommand.Top)
                    {
                        _highScores.PrintScoreboard(this);
                    }
                    else if (enteredCommand == PlayerCommand.Help)
                    {
                        cheats = ShowHelp(cheats);
                    }
                    else
                    {
                        if (ConsoleReader.IsValidInput(playerInput))
                        {
                            attempts++;
                            int bullsCount;
                            int cowsCount;
                            GameEngine.CalculateBullsAndCowsCount(playerInput, _generatedNumber, out bullsCount, out cowsCount);
                            if (bullsCount == Constants.NumberLenght)
                            {
                                FinishGame(attempts, cheats);
                                break;
                            }

                            ConsoleWriter.PrintWrongNumberMessage(bullsCount, cowsCount);
                        }
                        else
                        {
                            if (enteredCommand != PlayerCommand.Restart && enteredCommand != PlayerCommand.Exit)
                            {
                                ConsoleWriter.PrintWrongCommandMessage();
                            }
                        }
                    }
                } 
                while (enteredCommand != PlayerCommand.Exit && enteredCommand != PlayerCommand.Restart);
                Console.WriteLine();
            } 
            while (enteredCommand != PlayerCommand.Exit);
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
            _helpNumber[digitToReveal - 1] = _generatedNumber[digitToReveal - 1];
        }

        private void FinishGame(int attempts, int cheats)
        {
            if (cheats == 0)
            {
                ConsoleWriter.WinWithountCheats(attempts);
                HighScores.AddPlayerToScoreboard(attempts, this);
                _highScores.PrintScoreboard(this);
            }
            else
            {
                ConsoleWriter.WinWithCheats(attempts, cheats);
            }
        }
    }
}