using System;

public class PlayerInfo : IComparable<PlayerInfo>
{
    private string nickName;

    public PlayerInfo(string nickName, int guesses)
    {
        this.NickName = nickName;
        this.Guesses = guesses;
    }

    public int Guesses { get; set; }

    public string NickName
    {
        get
        {
            return this.nickName;
        }

        set
        {
            if (this.nickName == string.Empty)
            {
                throw new ArgumentException("NickName should have at least 1 symbol!");
            }
            else
            {
                this.nickName = value;
            }
        }
    }

    public int CompareTo(PlayerInfo other)
    {
        if (this.Guesses.CompareTo(other.Guesses) == 0)
        {
            return this.NickName.CompareTo(other.NickName);
        }
        else
        {
            return this.Guesses.CompareTo(other.Guesses);
        }
    }

    public override string ToString()
    {
        string result = string.Format("{0,3}    | {1}", this.Guesses, this.NickName);
        return result;
    }
}