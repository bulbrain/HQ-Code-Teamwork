using System;
using System.Collections.Generic;

internal class BullsAndCows
{
    private const int NUMBER_OF_DIGITS = 4;

    private const string START_EXPRESSION =
        "Welcome to “Bulls and Cows” game.Please try to guess my secret 4-digit number.\n" +
        "Use 'top' to view the top scoreboard, 'restart' to start a new game\n" +
        "and 'help' to cheat and 'exit' to quit the game.";

    private const string ENTER_GUES = "Enter your guess or command: ";
    private const string HELP = "The number looks like";
    private const string HELP_UNAVAILABLE = "You cannot use more help.";
    private const string WRONG_GUES = "Wrong number! ";
    private const string WRONG_INPUT = "Wrong input format! ";
    private const string IN_SCOREBOARD = "Please enter your name for the top scoreboard: ";
    private const string OUT_SCOREBOARD = "You are not allowed to enter the top scoreboard.";
    private const string EXIT_GAME = "Good bye!";
    private readonly Random r;
    private List<int> digits;
    private char[] helpExpression;

    public BullsAndCows()
    {
        this.r = new Random();
        this.SetDigits();
    }

    public static void Main()
    {
        var game = new BullsAndCows();
        game.StartGame();
    }

    public void StartGame()
    {
        while (true)
        {
            var flag1 = false;
            var count2 = 0;
            var count1 = 0;
            Console.WriteLine(START_EXPRESSION);
            do
            {
                Console.WriteLine(ENTER_GUES);
                var line = Console.ReadLine();

                if (line.Trim().ToLower().CompareTo("help") == 0)
                {
                    if (count2 == NUMBER_OF_DIGITS)
                    {
                        Console.WriteLine(HELP_UNAVAILABLE);
                        continue;
                    }

                    count2++;
                    var helpExpression = this.Help();
                    Console.WriteLine("{0} {1}", HELP, helpExpression);
                    continue;
                }

                if (line.Trim().ToLower().CompareTo("top") == 0)
                {
                    var scoreboard = klasirane.GetInstance();
                    scoreboard.sort();
                }
                else if (line.Trim().ToLower().CompareTo("restart") == 0)
                {
                    Console.WriteLine();
                    break;
                }
                else if (line.Trim().ToLower().CompareTo("exit") == 0)
                {
                    flag1 = true;
                    Console.WriteLine(EXIT_GAME);
                    break;
                }

                var count3 = 0;
                var count4 = 0;
                if (!this.ProccessGues(line.Trim(), out count3, out count4))
                {
                    Console.WriteLine(WRONG_INPUT);
                    continue;
                }

                count1++;
                if (count3 == NUMBER_OF_DIGITS)
                {
                    Console.WriteLine(
                        count2 == 0
                            ? "Congratulations! You guessed the secret number in {0} attempts and {1} cheats."
                            : "Congratulations! You guessed the secret number in {0} attempts.", 
                            count1, 
                            count2);
                    Console.WriteLine(new string('-', 80));

                    var scoreBoard = klasirane.GetInstance();
                    if (count2 == 0 && scoreBoard.IsHighScore(count1))
                    {
                        Console.WriteLine(IN_SCOREBOARD);
                        var name = Console.ReadLine();
                        scoreBoard.Add(name, count1);
                    }
                    else
                    {
                        Console.WriteLine(OUT_SCOREBOARD);
                    }

                    scoreBoard.sort();
                    break;
                }

                Console.WriteLine("{0} Bulls: {1}, Cows: {2}", WRONG_GUES, count3, count4);
            } 
            while (true);

            if (flag1)
            {
                break;
            }

            this.SetDigits();
        }
    }

    private void SetDigits()
    {
        this.digits = new List<int>();
        for (var index = 0; index < NUMBER_OF_DIGITS; index++)
        {
            this.digits.Add(this.r.Next(0, 10));
        }

        this.helpExpression = new char[NUMBER_OF_DIGITS];
        for (var index = 0; index < NUMBER_OF_DIGITS; index++)
        {
            this.helpExpression[index] = 'X';
        }
    }

    private bool ProccessGues(string gues, out int bulls, out int cows)
    {
        bulls = 0;
        cows = 0;
        if (gues.Length != NUMBER_OF_DIGITS)
        {
            return false;
        }

        var guestedDigits = new int[NUMBER_OF_DIGITS];
        for (var index = 0; index < NUMBER_OF_DIGITS; index++)
        {
            if (!int.TryParse(gues[index].ToString(), out guestedDigits[index]))
            {
                return false;
            }

            if (guestedDigits[index] == this.digits[index])
            {
                bulls++;
            }
            else if (this.digits.Contains(guestedDigits[index]))
            {
                cows++;
            }
        }

        return true;
    }

    private string Help()
    {
        var helpPosition = this.r.Next(NUMBER_OF_DIGITS);
        while (this.helpExpression[helpPosition] != 'X')
        {
            helpPosition = this.r.Next(NUMBER_OF_DIGITS);
        }

        this.helpExpression[helpPosition] = char.Parse(this.digits[helpPosition].ToString());
        return new string(this.helpExpression);
    }
}