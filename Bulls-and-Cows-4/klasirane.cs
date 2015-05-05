namespace BullsAndCowsGame
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    internal class Klasirane<T> : IEnumerable<T>, IEnumerator<T> where T : IComparable<T>
    {
        private readonly T[] data;
        private readonly int maxCountOfStoredData;
        private int position = -1;

        public Klasirane() : this(5)
        {
        }

        public Klasirane(int aMaxCountOfStoredData)
        {
            this.maxCountOfStoredData = aMaxCountOfStoredData;
            this.data = new T[this.maxCountOfStoredData];
            this.Count = 0;
        }

        object IEnumerator.Current
        {
            get { return this.data[this.position]; }
        }

        public int Count { get; private set; }

        public T Current
        {
            get { return this.data[this.position]; }
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
            this.Reset();
        }

        public bool MoveNext()
        {
            if (this.position < this.Count - 1)
            {
                this.position++;
                return true;
            }

            return false;
        }

        public void Reset()
        {
            this.position = -1;
        }

        public void Add(T item)
        {
            if (item.CompareTo(this.data[this.maxCountOfStoredData - 1]) >= 0)
            {
                var tPointer = 0;
                while (item.CompareTo(this.data[tPointer]) < 0)
                {
                    tPointer++;
                }

                for (var i = this.maxCountOfStoredData - 1; i > tPointer; i--)
                {
                    this.data[i] = this.data[i - 1];
                }

                this.data[tPointer] = item;
                if (this.Count < this.maxCountOfStoredData)
                {
                    this.Count++;
                }
            }
        }
    }
}