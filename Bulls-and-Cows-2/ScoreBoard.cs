namespace Bulls
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    public class Scoreboard
    {
        private const int MaxPlayersToShowInScoreboard = 10;
        private readonly SortedSet<GameScore> scores;

        public Scoreboard(string filename)
        {
            this.scores = new SortedSet<GameScore>();
            try
            {
                using (var inputStream = new StreamReader(filename))
                {
                    while (!inputStream.EndOfStream)
                    {
                        var scoreString = inputStream.ReadLine();
                        this.scores.Add(GameScore.Deserialize(scoreString));
                    }
                }
            }
            catch (IOException)
            {
                // Stop reading
            }
        }

        public void AddScore(string name, int guesses)
        {
            var newScore = new GameScore(name, guesses);
            this.scores.Add(newScore);
        }

        public void SaveToFile(string filename)
        {
            try
            {
                using (var outputStream = new StreamWriter(filename))
                {
                    foreach (var gameScore in this.scores)
                    {
                        outputStream.WriteLine(gameScore.Serialize());
                    }
                }
            }
            catch (IOException)
            {
                // Stop writing
            }
        }

        public override string ToString()
        {
            if (this.scores.Count == 0)
            {
                return "Top scoreboard is empty." + Environment.NewLine;
            }

            var scoreBoard = new StringBuilder();
            scoreBoard.AppendLine("Scoreboard:");
            var count = 0;
            foreach (var gameScore in this.scores)
            {
                count++;
                scoreBoard.AppendLine(string.Format("{0}. {1}", count, gameScore));
                if (count > MaxPlayersToShowInScoreboard)
                {
                    break;
                }
            }

            return scoreBoard.ToString();
        }
    }
}