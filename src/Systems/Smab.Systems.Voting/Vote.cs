namespace Smab.Systems.Voting
{
    public class Vote
    {
        public Vote(string id, Choice choice)
        {
            Id = id;
            Choice = choice;
        }

        public string Id { get; private set; }
        public Choice Choice { get; private set; }
    }
}