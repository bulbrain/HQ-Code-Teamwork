namespace Cows
{
    using System;
    using System.Collections.Generic;

    public class Game
    {
        private static readonly bool ShouldContinue = true;
        private static readonly bool ShouldNotContinue = false;
        private readonly TopScoresDelegate doTopScores;
        private readonly ScoreBoard myBoard;
        private int cposs;
        private string generatedNumberToWhatToGuess;
        private List<int> poss;
        public int score;

        public Game(ScoreBoard bb, TopScoresDelegate doTopScores)
        {
            this.myBoard = bb;
            this.doTopScores = doTopScores;
        }

        private List<int> Positions
        {
            get
            {
                if (this.poss == null)
                {
                    this.poss = new List<int>();
                    for (var i = 0; i < this.generatedNumberToWhatToGuess.Length; ++i)
                    {
                        this.poss.Add(i);
                    }

                    for (var i = 0; i < this.generatedNumberToWhatToGuess.Length; i++)
                    {
                        var t = int.Parse(RandomNumberProvider.CurrentProvider.GetRandomNumber());
                        t = (int)((t - 1000.0) / 9000.0 * this.generatedNumberToWhatToGuess.Length);
                        var tmp = this.poss[t];
                        this.poss[t] = this.poss[i];
                        this.poss[i] = tmp;
                    }
                }

                return this.poss;
            }
        }

        public bool Run()
        {
            Console.WriteLine("A new game has begun!");
            this.Init();

            while (true)
            {
                var komandata = Console.ReadLine();
                switch (komandata)
                {
                    case "exit":
                        return ShouldNotContinue;
                    case "restart":
                        return ShouldContinue;
                    case "help":
                        this.score = -1;
                        this.ShowRand();
                        break;
                    case "top":
                        this.myBoard.Output();
                        break;
                    default:
                        if (this.score != -1)
                        {
                            this.score++;
                        }

                        if (this.MatchCurrent(komandata))
                        {
                            this.doTopScores(this, this.myBoard);

                            if (this.Qustion())
                            {
                                return ShouldContinue;
                            }

                            return ShouldNotContinue;
                        }

                        break;
                }
            }
        }

        private bool Qustion()
        {
            Console.WriteLine("Another game ? (Y/N)");
            var s = Console.ReadLine();
            if (s.ToLower() == "y")
            {
                return true;
            }

            return false;
        }

        private bool MatchCurrent(string cmd)
        {
            if (cmd == this.generatedNumberToWhatToGuess)
            {
                Console.WriteLine("HOLYCOW, YOU HAVE WON!");
                return true;
            }

            var found = new bool[this.generatedNumberToWhatToGuess.Length];

            var b = this.Count2(cmd, found);
            var c = this.Count1(cmd, found);

            Console.WriteLine(b + " bull" + ((b != 1) ? "s" : string.Empty) + " and " + c + " cow" +
                              ((c != 1) ? "s" : string.Empty));
            return false;
        }

        private int Count2(string cmd, bool[] found)
        {
            var c = 0;
            for (var i = 0; i < this.generatedNumberToWhatToGuess.Length; i++)
            {
                for (var j = 0; j < cmd.Length; j++)
                {
                    if (this.generatedNumberToWhatToGuess[i] == cmd[j])
                    {
                        if (i == j)
                        {
                            found[i] = true;
                            c++;
                        }
                    }
                }
            }

            return c;
        }

        private int Count1(string cmd, bool[] found)
        {
            var c = 0;
            for (var i = 0; i < this.generatedNumberToWhatToGuess.Length; i++)
            {
                if (!found[i])
                {
                    var found2 = false;
                    for (var j = 0; j < cmd.Length; j++)
                    {
                        if (this.generatedNumberToWhatToGuess[i] == cmd[j])
                        {
                            if (i != j)
                            {
                                found2 = true;
                            }
                            else
                            {
                                Environment.Exit(-1);
                            }
                        }
                    }

                    if (found2)
                    {
                        c++;
                    }
                }
            }

            return c;
        }

        private void ShowRand()
        {
            Console.WriteLine("Bull at position " + (this.Positions[++this.cposs % this.generatedNumberToWhatToGuess.Length] + 1) + ": <" +
                              this.generatedNumberToWhatToGuess[this.Positions[this.cposs % this.generatedNumberToWhatToGuess.Length]] + ">");
        }

        private void Init()
        {
            RandomNumberProvider.CurrentProvider = new MyProvider();
            this.generatedNumberToWhatToGuess = RandomNumberProvider.CurrentProvider.GetRandomNumber();
            this.score = 0;
        }
    }
}