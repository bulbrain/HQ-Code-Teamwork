using System;

namespace BullsAndCowsGame.Player
{
    internal class Player : IComparable<Player>
    {
        public Player(string playerName, int attempts)
        {
            Name = playerName;
            Attempts = attempts;
        }

        public string Name { get; set; }

        public int Attempts { get; set; }

        public int CompareTo(Player other)
        {
            if (other == null)
            {
                return 1;
            }

            return other.Attempts - Attempts;
        }
    }
}