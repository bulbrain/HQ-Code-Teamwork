using System;
using System.Collections.Generic;
using System.Text;

internal class BullsAndCows
{
    private static readonly List<KeyValuePair<string, int>> sortedDict = new List<KeyValuePair<string, int>>();
    private static readonly char[] cheatNumber = { 'X', 'X', 'X', 'X' };
    private static readonly Dictionary<string, int> topScoreBoard = new Dictionary<string, int>();
    private static int lastPlayerScore = int.MinValue;

    private const int DigitsLength = 4;

    private static int SortDictionary(KeyValuePair<string, int> a, KeyValuePair<string, int> b)
    {
        return a.Value.CompareTo(b.Value);
    }

    private static void StartGame()
    {
        Console.WriteLine("Welcome to “Bulls and Cows” game. Please try to guess my secret 4-digit number.");
        Console.WriteLine("Use 'top' to view the top scoreboard, 'restart' to start a new game and 'help' " +
                          "to cheat and 'exit' to quit the game.");
    }

    /// <summary>
    /// We must check the number, is a digit.
    /// </summary>
    /// <param name="gameNumber"></param>
    /// <returns></returns>
    private static bool Check(string gameNumber)
    {
        var count = 0;
        for (var i = 0; i < DigitsLength; i++)
        {
            if (char.IsDigit(gameNumber[i]))
            {
                count++;
            }
        }

        if (count == 4)
        {
            return true;
        }

        return false;
    }
    /// <summary>
    /// Generate the secret number
    /// </summary>
    /// <returns>we return the secret number</returns>
    private static string GenerateRandomSecretNumber()
    {
        var secretNumber = new StringBuilder();
        var rand = new Random();
        while (secretNumber.Length != 4)
        {
            var number = rand.Next(0, 10);
            secretNumber.Append(number.ToString());
        }

        return secretNumber.ToString();
    }
    /// <summary>
    /// Check how many cows and bulls, we have in secret number
    /// </summary>
    /// <param name="secretNumber"></param>
    /// <param name="guessNumber"></param>
    /// <param name="bulls"></param>
    /// <param name="cows"></param>
    private static void CalculateBullsAndCows(string secretNumber, string guessNumber, ref int bulls, ref int cows)
    {
        var bullIndexes = new List<int>();
        var cowIndexes = new List<int>();
        for (var i = 0; i < secretNumber.Length; i++)
        {
            if (guessNumber[i].Equals(secretNumber[i]))
            {
                bullIndexes.Add(i);
                bulls++;
            }
        }

        for (var i = 0; i < guessNumber.Length; i++)
        {
            for (var index = 0; index < secretNumber.Length; index++)
            {
                if ((i != index) && !bullIndexes.Contains(index) && !cowIndexes.Contains(index) &&
                    !bullIndexes.Contains(i))
                {
                    if (guessNumber[i].Equals(secretNumber[index]))
                    {
                        cowIndexes.Add(index);
                        cows++;
                        break;
                    }
                }
            }
        }
    }

    private static char[] RevealNumberAtRandomPosition(string secretnumber, char[] cheatNumber)
    {
        while (true)
        {
            var rand = new Random();
            var index = rand.Next(0, 4);
            if (cheatNumber[index] == 'X')
            {
                cheatNumber[index] = secretnumber[index];
                return cheatNumber;
            }
        }
    }
    /// <summary>
    /// Save player result
    /// </summary>
    /// <param name="score"></param>
    private static void EnterScoreBoard(int score)
    {
        Console.Write("Please enter your name for the top scoreboard: ");
        var name = Console.ReadLine();
        topScoreBoard.Add(name, score);

        if (score > lastPlayerScore)
        {
            lastPlayerScore = score;
        }

        if (topScoreBoard.Count > 5)
        {
            foreach (var player in topScoreBoard)
            {
                if (player.Value == lastPlayerScore)
                {
                    topScoreBoard.Remove(player.Key);
                    break;
                }
            }
        }

        SortScoreBoard();
        PrintScoreBoard();
    }
    /// <summary>
    /// Sort players results
    /// </summary>
    private static void SortScoreBoard()
    {
        foreach (var pair in topScoreBoard)
        {
            sortedDict.Add(new KeyValuePair<string, int>(pair.Key, pair.Value));
        }

        sortedDict.Sort(SortDictionary);

    }
    /// <summary>
    /// Print top players results.
    /// </summary>
    private static void PrintScoreBoard()
    {
        Console.WriteLine("Scoreboard: ");
        var counter = 0;
        foreach (var player in sortedDict)
        {
            counter++;
            Console.WriteLine("{0}. {1} --> {2} guesses", counter, player.Key, player.Value);
        }

        sortedDict.Clear();
    }

    private static void Main()
    {
        StartGame();

        var generateNumber = GenerateRandomSecretNumber();
        string stringNumber = null;
        var bull = 0;
        var cow = 0;

        while (true)
        {
            Console.Write("Enter your guess or command: ");
            stringNumber = Console.ReadLine();

            if (stringNumber == "help")
            {
                var revealedDigits = RevealNumberAtRandomPosition(generateNumber, cheatNumber);
                var revealedNumber = new StringBuilder();
                for (var i = 0; i < 4; i++)
                {
                    revealedNumber.Append(revealedDigits[i]);
                }

                Console.WriteLine("The number looks like {0}", revealedNumber);
                cow++;
                continue;
            }

            if (stringNumber == "restart")
            {
                Console.WriteLine();
                StartGame();
                bull = 0;
                generateNumber = GenerateRandomSecretNumber();
                continue;
            }

            if (stringNumber == "top")
            {
                if (topScoreBoard.Count == 0)
                {
                    Console.WriteLine("Top scoreboard is empty.");
                }
                else
                {
                    SortScoreBoard();
                    PrintScoreBoard();
                }

                continue;
            }

            if (stringNumber == "exit")
            {
                Console.WriteLine("Good bye!");
                break;
            }

            if (stringNumber.Length != 4 || Check(stringNumber) == false)
            {
                Console.WriteLine("Incorrect guess or command!");
                continue;
            }

            bull++;
            var bulls = 0;
            var cows = 0;
            CalculateBullsAndCows(generateNumber, stringNumber, ref bulls, ref cows);
            if (stringNumber == generateNumber)
            {
                if (cow > 0)
                {
                    Console.WriteLine(
                        "Congratulations! You guessed the secret number in {0} attempts and {1} cheats.",
                        bull,
                        cow);
                    Console.WriteLine("You are not allowed to enter the top scoreboard.");
                    SortScoreBoard();
                    PrintScoreBoard();
                    Console.WriteLine();
                    StartGame();
                    bull = 0;
                    cow = 0;
                    generateNumber = GenerateRandomSecretNumber();
                }
                else
                {
                    Console.WriteLine("Congratulations! You guessed the secret number in {0} attempts.", bull);
                    EnterScoreBoard(bull);
                    cow = 0;
                    Console.WriteLine();
                    StartGame();
                    generateNumber = GenerateRandomSecretNumber();
                }

                continue;
            }

            Console.WriteLine("Wrong number! Bulls: {0}, Cows: {1}", bulls, cows);
        }
    }
}