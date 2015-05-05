namespace Bulls
{
    using System;

    public class GameScore : IComparable
    {
        public GameScore(string ime, int guesses)
        {
            this.Name = ime;
            this.Guesses = guesses;
        }

        public string Name
        {
            get;
            private set;
        }

        public int Guesses
        {
            get;
            private set;
        }

        public static GameScore Deserialize(string data)
        {
            string[] dataAsStringArray = data.Split(new string[] { "_:::_" }, StringSplitOptions.None);
            if (dataAsStringArray.Length != 2)
            {
                return null;
            }

            string name = dataAsStringArray[0];

            int guesses = 0;
            int.TryParse(dataAsStringArray[1], out guesses);

            return new GameScore(name, guesses);
        }

        public override bool Equals(object obj)
        {
            GameScore objectToCompare = obj as GameScore;
            if (objectToCompare == null)
            {
                return false;
            }
            else
            {
                return this.Guesses.Equals(objectToCompare) && this.Name.Equals(objectToCompare);
            }
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode() ^ this.Guesses.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0} --> {1} {2}", this.Name, this.Guesses, this.Guesses == 1 ? "guess" : "guesses");
        }

        public int CompareTo(object obj)
        {
            GameScore objectToCompare = obj as GameScore;
            if (objectToCompare == null)
            {
                return -1;
            }

            if (this.Guesses.CompareTo(objectToCompare.Guesses) == 0)
            {
                return this.Name.CompareTo(objectToCompare.Name);
            }
            else
            {
                return this.Guesses.CompareTo(objectToCompare.Guesses);
            }
        }

        public string Serialize()
        {
            return string.Format("{0}_:::_{1}", this.Name, this.Guesses);
        }
    }
}
