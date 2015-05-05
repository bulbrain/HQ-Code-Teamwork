namespace Cows
{
    using System;

    public class Record : IComparable<Record>
    {
        public Record(string name, int score)
        {
            this.Name = name;
            this.Score = score;
        }

        public string Name { get; private set; }

        public int Score { get; private set; }

        public int CompareTo(Record other)
        {
            return this.Score.CompareTo(other.Score);
        }
    }
}