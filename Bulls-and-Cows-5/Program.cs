namespace BullsCows
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    internal class Program
    {
        private static readonly Random rr = new Random();
        private static readonly SortedList<int, string> klasirane = new SortedList<int, string>();
        private static int number, attempts;
        private static bool notCheated;
        private static string ch;

        private static void WriteAbout()
        {
            Console.WriteLine("Welcome to “Bulls and Cows” game. Please try to guess my secret 4-digit number.");
            Console.WriteLine(
                "Use 'top' to view the top scoreboard, 'restart' to start a new game and 'help' to cheat and 'exit' to quit the game.");
        }

        private static void TryAddToScoreboard()
        {
            if (klasirane.Count < 5 || klasirane.ElementAt(4).Key > attempts)
            {
                Console.WriteLine("Please enter your name for the top scoreboard: ");
                var name = Console.ReadLine().Trim();

                klasirane.Add(attempts, name);
                if (klasirane.Count == 6)
                {
                    klasirane.RemoveAt(5);
                }

                DisplayTop();
            }
        }

        private static void ProcessWin()
        {
            Console.WriteLine("Congratulations! You guessed the secret number in {0} attempts.", attempts);

            if (notCheated)
            {
                TryAddToScoreboard();
            }

            StartNewGame();
        }

        private static void ProcessGuess(int guess)
        {
            if (guess == number)
            {
                ProcessWin();
            }
            else
            {
                string snum = number.ToString(), sguess = guess.ToString();
                var isBull = new bool[4];
                int bulls = 0, cows = 0;
                for (var i = 0; i < 4; i++)
                {
                    if (isBull[i] = snum[i] == sguess[i])
                    {
                        bulls++;
                    }
                }

                var digs = new int[10];
                for (var d = 0; d < 10; d++)
                {
                    digs[d] = 0;
                }

                for (var i = 0; i < 4; i++)
                {
                    if (!isBull[i])
                    {
                        digs[snum[i] - '0']++;
                    }
                }

                for (var i = 0; i < 4; i++)
                {
                    if (!isBull[i])
                    {
                        if (digs[sguess[i] - '0'] > 0)
                        {
                            cows++;
                            digs[sguess[i] - '0']--;
                        }
                    }
                }

                Console.WriteLine("Wrong number! Bulls: {0}, Cows: {1}", bulls, cows);
                attempts++;
            }
        }

        private static void DisplayTop()
        {
            if (klasirane.Count() > 0)
            {
                Console.WriteLine("Scoreboard:");
                var i = 1;
                foreach (var t in klasirane)
                {
                    Console.WriteLine("{0}. {1} --> {2} guesses", i, t.Value, t.Key);
                }
            }
            else
            {
                Console.WriteLine("Top scoreboard is empty.");
            }
        }

        private static void StartNewGame()
        {
            WriteAbout();
            number = rr.Next(1000, 10000);
            attempts = 1;
            notCheated = true;
            ch = "XXXX";
        }

        private static void Cheat()
        {
            notCheated = false;
            if (ch.Contains('X'))
            {
                int i;
                do
                {
                    i = rr.Next(0, 4); 
                }
                while (ch[i] != 'X');
                var cha = ch.ToCharArray();
                cha[i] = number.ToString()[i];
                ch = new string(cha);
            }

            Console.WriteLine("The number looks like {0}.", ch);
        }

        private static bool ReadAction()
        {
            Console.WriteLine("Enter your guess or command: ");
            var line = Console.ReadLine().Trim();
            var patt = new Regex("[1-9][0-9][0-9][0-9]");

            switch (line)
            {
                case "top": DisplayTop();
                    break;
                case "restart": StartNewGame();
                    break;
                case "help": Cheat();
                    break;
                case "exit":
                    return false;

                default:
                    if (patt.IsMatch(line))
                    {
                        var guess = int.Parse(line);
                        ProcessGuess(guess);
                    }
                    else
                    {
                        Console.WriteLine("Please enter a 4-digit number or");
                        Console.WriteLine("one of the commands: 'top', 'restart', 'help' or 'exit'.");
                    }

                    break;
            }

            return true;
        }

        private static void Main(string[] args)
        {
            StartNewGame();
            while (ReadAction())
            {
            }
        }
    }
}