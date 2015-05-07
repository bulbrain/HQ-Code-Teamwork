namespace Cows
{
    using System;
    using System.Collections.Generic;

    public delegate void TopScoresDelegate(Game g, ScoreBoard board);

    internal class Program
    {
        private static int scoreBoardSize = 5;

        private static void DoTopScores(Game g, ScoreBoard board)
        {
            if (g.score != -1 && g.score < board.Board[scoreBoardSize-1].Score)
            {
                Console.Write("TOP SCORE! Please enter your name:");
                var name = Console.ReadLine();
                var list = new List<Record>(board.Board);
                list.Add(new Record(name, g.score));
                list.Sort();
                for (var i = 0; i < scoreBoardSize; i++)
                {
                    board.Board[i] = list[i];
                }
            }
        }

        private static void Main()
        {
            var board = new ScoreBoard();
            while (new Game(board, DoTopScores).Run()) ;
        }
    }
}