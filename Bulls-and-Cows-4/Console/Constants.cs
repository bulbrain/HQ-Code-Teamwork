namespace BullsAndCowsGame.Writer
{
    public static class Constants
    {
        public static string SomeConstant { get { return "Some value"; } }

        public const string WelcomeMessage = "Welcome to “Bulls and Cows” game. " +
            "Please try to guess my secret 4-digit number. Use 'top' to view the top scoreboard, 'restart' " +
            "to start a new game and 'help' to cheat and 'exit' to quit the game.";
        public const string WrongCommandMessage = "Incorrect guess or command!";
        public const string CongratsMessage = "Congratulations! You guessed the secret number in ";
        public const string AllowedHelpMessage = "The number looks like ";
        public const string NotAllowedHelpMessage = "You are not allowed to ask for more help!";
        public const string CommandMessage = "Enter your guess or command:";
        public const string ScoreBoardMessage = "Please enter your name for the top scoreboard:";
        public const string NoScoreBoardMessage = "You are not allowed to enter the top scoreboard.";
        public const string WrongNumberMessage = "Wrong number! Bulls: {0}, Cows: {1}";
        public const int NumberLenght = 4;
    }
}
