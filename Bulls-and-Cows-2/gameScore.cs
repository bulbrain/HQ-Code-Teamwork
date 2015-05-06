using System;

namespace BullsAndCowsGame
{
    public class GameScore : IComparable
    {
        public GameScore(string username, int guesses)
        {
            this.Username = username;
            this.Guesses = guesses;
        }

        public string Username { get; private set; }
        public int Guesses { get; private set; }

        public int CompareTo(object obj)
        {
            var objectToCompare = obj as GameScore;
            if (objectToCompare == null)
            {
                return -1;
            }

            if (Guesses.CompareTo(objectToCompare.Guesses) == 0)
            {
                return Username.CompareTo(objectToCompare.Username);
            }
            return Guesses.CompareTo(objectToCompare.Guesses);
        }

        public static GameScore Deserialize(string data)
        {
            var dataAsStringArray = data.Split(new[] {"_:::_"}, StringSplitOptions.None);
            if (dataAsStringArray.Length != 2)
            {
                return null;
            }

            var name = dataAsStringArray[0];

            var guesses = 0;
            int.TryParse(dataAsStringArray[1], out guesses);

            return new GameScore(name, guesses);
        }

        public override bool Equals(object obj)
        {
            var objectToCompare = obj as GameScore;
            if (objectToCompare == null)
            {
                return false;
            }
            return Guesses.Equals(objectToCompare) && Username.Equals(objectToCompare);
        }

        public override int GetHashCode()
        {
            return Username.GetHashCode() ^ Guesses.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0} --> {1} {2}", Username, Guesses, Guesses == 1 ? "guess" : "guesses");
        }

        public string Serialize()
        {
            return string.Format("{0}_:::_{1}", Username, Guesses);
        }
    }
}