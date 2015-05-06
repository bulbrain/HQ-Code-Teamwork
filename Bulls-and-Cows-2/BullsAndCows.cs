namespace BullsAndCowsGame
{
    using System;

    internal class BullsAndCows
    {
        public const string ScoresFile = "scores.txt";

        public const string WelcomeMessage =
            "Welcome to “Bulls and Cows” game. Please try to guess my secret 4-digit number.\nUse 'top' to view the top scoreboard, 'restart' to start a new game and 'help' to cheat and 'exit' to quit the game.";

        public const string WrongNumberMessage = "Wrong number!";
        public const string InvalidCommandMessage = "Incorrect guess or command!";

        public const string NumberGuessedWithoutCheats =
            "Congratulations! You guessed the secret number in {0} {1}.\nPlease enter your name for the top scoreboard: ";

        public const string NumberGuessedWithCheats =
            "Congratulations! You guessed the secret number in {0} {1} and {2} {3}.\nYou are not allowed to enter the top scoreboard.";

        public const string GoodBuyMessage = "Good bye!";

        public void Play()
        {
            CheckForBullsAndCows bullsAndCowsNumber = new CheckForBullsAndCows();
            Scoreboard scoreBoard = new Scoreboard(ScoresFile);
            Console.WriteLine(WelcomeMessage);
            while (true)
            {
                Console.Write("Enter your guess or command: ");
                string command = Console.ReadLine();
                if (command == "exit")
                {
                    Console.WriteLine(GoodBuyMessage);
                    break;
                }

                bullsAndCowsNumber = CommandExecute(command, scoreBoard, bullsAndCowsNumber);
            }

            scoreBoard.SaveToFile(ScoresFile);
        }

        private static CheckForBullsAndCows CommandExecute(string command, Scoreboard scoreBoard,
            CheckForBullsAndCows bullsAndCowsNumber)
        {
            switch (command)
            {
                case "top":
                {
                    Console.Write(scoreBoard);
                    break;
                }

                case "restart":
                {
                    Console.WriteLine();
                    Console.WriteLine(WelcomeMessage);
                    bullsAndCowsNumber = new CheckForBullsAndCows();
                    break;
                }

                case "help":
                {
                    Console.WriteLine("The number looks like {0}.", bullsAndCowsNumber.GetCheat());
                    break;
                }

                default:
                {
                    try
                    {
                        Result guessResult = bullsAndCowsNumber.TryToGuess(command);
                        if (guessResult.Bulls == 4)
                        {
                            if (bullsAndCowsNumber.Cheats == 0)
                            {
                                Console.Write(NumberGuessedWithoutCheats, bullsAndCowsNumber.GuessesCount,
                                    bullsAndCowsNumber.GuessesCount == 1 ? "attempt" : "attempts");
                                string name = Console.ReadLine();
                                scoreBoard.AddScore(name, bullsAndCowsNumber.GuessesCount);
                            }
                            else
                            {
                                Console.WriteLine(
                                    NumberGuessedWithCheats,
                                    bullsAndCowsNumber.GuessesCount,
                                    bullsAndCowsNumber.GuessesCount == 1 ? "attempt" : "attempts",
                                    bullsAndCowsNumber.Cheats,
                                    bullsAndCowsNumber.Cheats == 1 ? "cheat" : "cheats");
                            }

                            Console.Write(scoreBoard);
                            Console.WriteLine();
                            Console.WriteLine(WelcomeMessage);
                            bullsAndCowsNumber = new CheckForBullsAndCows();
                        }
                        else
                        {
                            Console.WriteLine("{0} {1}", WrongNumberMessage, guessResult);
                        }
                    }
                    catch (ArgumentException)
                    {
                        Console.WriteLine(InvalidCommandMessage);
                    }

                    break;
                }
            }
            return bullsAndCowsNumber;
        }
    }
}
