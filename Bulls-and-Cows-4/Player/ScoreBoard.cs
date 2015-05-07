using System;
using System.Collections;
using System.Collections.Generic;
using BullsAndCowsGame.Engine;

namespace BullsAndCowsGame.Player
{
    internal class ScoreBoard<T> : IEnumerable<T>, IEnumerator<T> where T : IComparable<T>
    {
        private readonly T[] _data;
        private const int MaxCountOfStoredData = 5;
        private int _position = -1;

        public ScoreBoard()
        {
            _data = new T[MaxCountOfStoredData];
            Count = 0;
        }

        object IEnumerator.Current
        {
            get { return _data[_position]; }
        }

        public int Count { get; private set; }

        public T Current
        {
            get { return _data[_position]; }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
        }

        public void Dispose()
        {
            Reset();
        }

        public bool MoveNext()
        {
            if (_position < Count - 1)
            {
                _position++;
                return true;
            }

            return false;
        }

        public void Reset()
        {
            _position = -1;
        }

        public void Add(T item)
        {
            if (item.CompareTo(_data[MaxCountOfStoredData - 1]) >= 0)
            {
                var tPointer = 0;
                while (item.CompareTo(_data[tPointer]) < 0)
                {
                    tPointer++;
                }

                for (var i = MaxCountOfStoredData - 1; i > tPointer; i--)
                {
                    _data[i] = _data[i - 1];
                }

                _data[tPointer] = item;
                if (Count < MaxCountOfStoredData)
                {
                    Count++;
                }
            }
        }

        public void PrintScoreboard(Game game)
        {
            if (Count == 0)
            {
                Console.WriteLine("Top scoreboard is empty.");
            }
            else
            {
                Console.WriteLine("Scoreboard:");
                var i = 1;
                foreach (var p in game.HighScores)
                {
                    Console.WriteLine("{0}. {1} --> {2} guess" + ((p.Attempts == 1) ? string.Empty : "es"), i++, p.Name, p.Attempts);
                }
            }
        }

        public void AddPlayerToScoreboard(int attempts, Game game)
        {
            var playerName = Console.ReadLine();
            var player = new Player(playerName, attempts);
            game.HighScores.Add(player);
        }
    }
}