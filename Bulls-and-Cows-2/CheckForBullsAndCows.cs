namespace BullsAndCowsGame
{
    using System;
    using System.Text;

    public class CheckForBullsAndCows
    {
        private Random rand;
        private char[] cheatNumber;

        public CheckForBullsAndCows()
        {
            this.rand = new Random();
            this.cheatNumber = new char[4] { 'X', 'X', 'X', 'X' };
            this.Cheats = 0;
            this.GuessesCount = 0;
            this.GenerateRandomNumbers();
        }

        public int Cheats
        {
            get;
            private set;
        }

        public int GuessesCount
        {
            get;
            private set;
        }

        public int FirstDigit
        {
            get;
            private set;
        }

        public int SecondDigit
        {
            get;
            private set;
        }

        public int ThirdDigit
        {
            get;
            private set;
        }

        public int FourthDigit
        {
            get;
            private set;
        }

        public string GetCheat()
        {
            if (this.Cheats < 4)
            {
                while (true)
                {
                    int randPossition = this.rand.Next(0, 4);
                    if (this.cheatNumber[randPossition] == 'X')
                    {
                        switch (randPossition)
                        {
                            case 0: this.cheatNumber[randPossition] = (char)(this.FirstDigit + '0'); 
                                break;
                            case 1: this.cheatNumber[randPossition] = (char)(this.SecondDigit + '0'); 
                                break;
                            case 2: this.cheatNumber[randPossition] = (char)(this.ThirdDigit + '0'); 
                                break;
                            case 3: this.cheatNumber[randPossition] = (char)(this.FourthDigit + '0'); 
                                break;
                        }

                        break;
                    }
                }

                this.Cheats++;
            }

            return new string(this.cheatNumber);
        }

        public TotalBullAndCows TryToGuess(string number)
        {
            if (string.IsNullOrEmpty(number) || number.Trim().Length != 4)
            {
                throw new ArgumentException("Invalid string number");
            }

            return this.TryToGuessNumber(number[0] - '0', number[1] - '0', number[2] - '0', number[3] - '0');
        }

        public override string ToString()
        {
            StringBuilder numberStringBuilder = new StringBuilder();
            numberStringBuilder.Append(this.FirstDigit);
            numberStringBuilder.Append(this.SecondDigit);
            numberStringBuilder.Append(this.ThirdDigit);
            numberStringBuilder.Append(this.FourthDigit);
            return numberStringBuilder.ToString();
        }

        public override bool Equals(object obj)
        {
            CheckForBullsAndCows objectToCompare = obj as CheckForBullsAndCows;
            if (objectToCompare == null)
            {
                return false;
            }
            else
            {
                return this.FirstDigit == objectToCompare.FirstDigit &&
                        this.SecondDigit == objectToCompare.SecondDigit &&
                        this.ThirdDigit == objectToCompare.ThirdDigit &&
                        this.FourthDigit == objectToCompare.FourthDigit;
            }
        }

        public override int GetHashCode()
        {
            return this.FirstDigit.GetHashCode() ^ this.SecondDigit.GetHashCode() ^ this.ThirdDigit.GetHashCode() ^ this.FourthDigit.GetHashCode();
        }

        private TotalBullAndCows TryToGuessNumber(int firstDigit, int secondDigit, int thirdDigit, int fourthDigit)
        {
            if (firstDigit < 0 || firstDigit > 9)
            {
                throw new ArgumentException("Invalid first digit");
            }

            if (secondDigit < 0 || secondDigit > 9)
            {
                throw new ArgumentException("Invalid second digit");
            }

            if (thirdDigit < 0 || thirdDigit > 9)
            {
                throw new ArgumentException("Invalid third digit");
            }

            if (fourthDigit < 0 || fourthDigit > 9)
            {
                throw new ArgumentException("Invalid fourth digit");
            }

            this.GuessesCount++;

            int bulls = 0;

            bool isFirstDigitBullOrCow = false;

            if (this.FirstDigit == firstDigit)
            {
                isFirstDigitBullOrCow = true;
                bulls++;
            }

            bool isSecondDigitBullOrCow = false;

            if (this.SecondDigit == secondDigit)
            {
                isSecondDigitBullOrCow = true;
                bulls++;
            }

            bool isThirdDigitBullOrCow = false;

            // checks if thirdDigit is a bull:
            if (this.ThirdDigit == thirdDigit)
            {
                isThirdDigitBullOrCow = true;
                bulls++;
            }

            bool isFourthDigitBullOrCow = false;

            // checks if fourthDigit is a bull:
            if (this.FourthDigit == fourthDigit)
            {
                isFourthDigitBullOrCow = true;
                bulls++;
            }

            int cows = 0;

            // checks if firstDigit is cow:
            if (!isSecondDigitBullOrCow && firstDigit == this.SecondDigit)
            {
                isSecondDigitBullOrCow = true;
                cows++;
            }
            else if (!isThirdDigitBullOrCow && firstDigit == this.ThirdDigit)
            {
                isThirdDigitBullOrCow = true;
                cows++;
            }
            else if (!isFourthDigitBullOrCow && firstDigit == this.FourthDigit)
            {
                isFourthDigitBullOrCow = true;
                cows++;
            }

            // checks if secondDigit is cow:
            if (!isFirstDigitBullOrCow && secondDigit == this.FirstDigit)
            {
                isFirstDigitBullOrCow = true;
                cows++;
            }
            else if (!isThirdDigitBullOrCow && secondDigit == this.ThirdDigit)
            {
                isThirdDigitBullOrCow = true;
                cows++;
            }
            else if (!isFourthDigitBullOrCow && secondDigit == this.FourthDigit)
            {
                isFourthDigitBullOrCow = true;
                cows++;
            }

            // checks if thirdDigit is cow:
            if (!isFirstDigitBullOrCow && thirdDigit == this.FirstDigit)
            {
                isFirstDigitBullOrCow = true;
                cows++;
            }
            else if (!isSecondDigitBullOrCow && thirdDigit == this.SecondDigit)
            {
                isSecondDigitBullOrCow = true;
                cows++;
            }
            else if (!isFourthDigitBullOrCow && thirdDigit == this.FourthDigit)
            {
                isFourthDigitBullOrCow = true;
                cows++;
            }
            if (!isFirstDigitBullOrCow && fourthDigit == this.FirstDigit)
            {
                isFirstDigitBullOrCow = true;
                cows++;
            }
            else if (!isSecondDigitBullOrCow && fourthDigit == this.SecondDigit)
            {
                isSecondDigitBullOrCow = true;
                cows++;
            }
            else if (!isThirdDigitBullOrCow && fourthDigit == this.ThirdDigit)
            {
                isThirdDigitBullOrCow = true;
                cows++;
            }

            TotalBullAndCows guessTotalBullAndCows = new TotalBullAndCows();
            guessTotalBullAndCows.Bulls = bulls;
            guessTotalBullAndCows.Cows = cows;
            guessTotalBullAndCows.Cows = cows;
            return guessTotalBullAndCows;
        }

        private void GenerateRandomNumbers()
        {
            this.FirstDigit = this.rand.Next(0, 10);
            this.SecondDigit = this.rand.Next(0, 10);
            this.ThirdDigit = this.rand.Next(0, 10);
            this.FourthDigit = this.rand.Next(0, 10);
        }
    }
}
