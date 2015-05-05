namespace Cows
{
    using System;

    public class ScoreBoard
    {
        public Record[] Board = new Record[5];

        public ScoreBoard()
        {
            for (var i = 0; i < 5; i++)
            {
                this.Board[i] = new Record("Unknown", int.MaxValue);
            }
        }

        public void Output()
        {
            Console.WriteLine("----Scoreboard----");
            Console.WriteLine("1.(" + this.Board[0].Score + ")" + this.Board[0].Name);
            Console.WriteLine("2.(" + this.Board[1].Score + ")" + this.Board[1].Name);
            Console.WriteLine("3.(" + this.Board[2].Score + ")" + this.Board[2].Name);
            Console.WriteLine("4.(" + this.Board[3].Score + ")" + this.Board[3].Name);
            Console.WriteLine("5.(" + this.Board[4].Score + ")" + this.Board[4].Name);
            Console.WriteLine("------------------");
        }
    }
}