namespace Cows
{
    using System;

    public class RandomNumberProvider
    {
        private static RandomNumberProvider currentProvider;
        private Random r = new Random();

        public static RandomNumberProvider CurrentProvider
        {
            get
            {
                if (currentProvider == null)
                {
                    currentProvider = new RandomNumberProvider();
                }

                return currentProvider;
            }

            set
            {
                currentProvider = value;
            }
        }

        public virtual string GetRandomNumber()
        {
            return 4165.ToString();
        }
    }
}