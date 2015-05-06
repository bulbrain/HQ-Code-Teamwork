using System;

public class PlayerInfo : IComparable<PlayerInfo>
{
    private string username;
    private int guessNumber;

    public PlayerInfo(string username, int guessNumber)
    {
        this.Username = username;
        this.GuessNumber = guessNumber;
    }

    public int GuessNumber
    {
        get { return this.guessNumber; }
        set
        {
            if (value < 1000 || value > 9999)
            {
                throw new ArgumentOutOfRangeException("The guess number must be between 1000 and 9999!");
            }

            this.guessNumber = value;
        }
    }

    public string Username
    {
        get
        {
            return this.username;
        }

        set
        {
            if (String.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("The username should have at least 1 symbol!");
            }

            this.username = value;
        }
    }

    public int CompareTo(PlayerInfo other)
    {
        if (this.GuessNumber.CompareTo(other.GuessNumber) == 0)
        {
            return this.Username.CompareTo(other.Username);
        }
        else
        {
            return this.GuessNumber.CompareTo(other.GuessNumber);
        }
    }

    public override string ToString()
    {
        string result = string.Format("{0,3}    | {1}", this.GuessNumber, this.Username);
        return result;
    }
}