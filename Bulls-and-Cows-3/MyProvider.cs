namespace Cows
{
    public class MyProvider : RandomNumberProvider
    {
        public override string GetRandomNumber()
        {
            return "1234";

            // ((int)(rand.NextDouble() * 9000 + 1000)).ToString();
        }
    }
}
