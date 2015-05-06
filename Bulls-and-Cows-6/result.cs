using System;
using System.Collections.Generic;
using System.Linq;

public class Result
{
    private const int MAX_PLAYERS = 5;
    private static Result instance;
    private readonly List<KeyValuePair<string, int>> resultInTheGame;

    private Result()
    {
        this.resultInTheGame = new List<KeyValuePair<string, int>>();
    }

    public static Result GetInstance()
    {
        if (instance == null)
        {
            instance = new Result();
        }

        return instance;
    }
    /// <summary>
    /// check for top result
    /// </summary>
    /// <param name="attempts"></param>
    /// <returns></returns>
    public bool IsHighScore(int attempts)
    {
        if (this.resultInTheGame.Count < MAX_PLAYERS || this.resultInTheGame.Last().Value < attempts)
        {
            return true;
        }
        return false;
    }

    public void Add(string name, int attempts)
    {
        this.resultInTheGame.Add(new KeyValuePair<string, int>(name, attempts));
        this.Sort();
        if (this.resultInTheGame.Count > MAX_PLAYERS)
        {
            this.resultInTheGame.RemoveAt(this.resultInTheGame.Count - 1);
        }
    }
    /// <summary>
    /// sort all results in the game
    /// </summary>
    public void SortResults()
    {
        if (this.resultInTheGame.Count == 0)
        {
            Console.WriteLine("Scoreboard empty!");
            Console.WriteLine();
            return;
        }

        this.Sort();
        Console.WriteLine("Scoreboard:");
        for (var index = 0; index < this.resultInTheGame.Count; index++)
        {
            var name = this.resultInTheGame[index].Key;
            var attempts = this.resultInTheGame[index].Value;
            Console.WriteLine("{0}. {1} --> {2} guesses", index + 1, name, attempts);
        }

        Console.WriteLine();
    }

    private void Sort()
    {
        this.resultInTheGame.Sort((first, second) => second.Value.CompareTo(first.Value));
    }
}