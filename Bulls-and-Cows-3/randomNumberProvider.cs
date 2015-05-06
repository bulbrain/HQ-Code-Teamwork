namespace Cows
{
    using System;

    public class RandomNumberProvider
    {
        private static RandomNumberProvider provider;
        private Random r = new Random();

        public static RandomNumberProvider CurrentProvider
        {
            get
            {
                if (provider == null)
                {
                    provider = new RandomNumberProvider();
                }

                return provider;
            }

            set
            {
                provider = value;
            }
        }

        public virtual string GetRandomNumber()
        {
            return ((int)(r.NextDouble() * 9000 + 1000)).ToString();
        }
    }
}