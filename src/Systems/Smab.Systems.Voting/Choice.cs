namespace Smab.Systems.Voting
{
    public class Choice
    {
        public Choice(string choice)
        {
            Value = choice;
        }

        public string Value { get; private set; }
    }
}