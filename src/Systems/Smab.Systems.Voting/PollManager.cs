using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smab.Systems.Voting
{
    public class PollManager
    {
        public IList<Poll> Polls { get; set; } = new List<Poll>();

        public PollManager() { }

        public Poll Create(string question, string timeDuration)
        {
            if (Polls.Any(p => p.Question.Equals(question)))
            {
                throw new InvalidOperationException();
            }

            Poll poll = new Poll(question);
            poll.SetDuration(timeDuration);
            Polls.Add(poll);

            return poll;
        }

        public bool AddChoice(string question, string choice)
        {
            Poll poll = Polls.FirstOrDefault(p => p.Question.Equals(question));

            if (poll == null)
            {
                throw new InvalidOperationException();
            }

            return poll.AddChoice(choice);
        }

        public void Open(string question)
        {
            Poll poll = Polls.FirstOrDefault(p => p.Question.Equals(question));

            if (poll == null)
            {
                throw new InvalidOperationException();
            }

            poll.Open();
        }

        public void Close(string question)
        {
            Poll poll = Polls.FirstOrDefault(p => p.Question.Equals(question));

            if (poll == null)
            {
                throw new InvalidOperationException();
            }

            poll.Close();
        }

        public string GetResults(string question)
        {
            Poll poll = Polls.FirstOrDefault(p => p.Question.Equals(question));

            if (poll == null)
            {
                throw new InvalidOperationException();
            }

            return poll.GetResults();
        }

        public bool Vote(string id, string question, string choice)
        {
            Poll poll = Polls.FirstOrDefault(p => p.Question.Equals(question));

            if (poll == null)
            {
                throw new InvalidOperationException();
            }

            return poll.Vote(id, choice);
        }

        public int GetVotes(string question)
        {
            Poll poll = Polls.FirstOrDefault(p => p.Question.Equals(question));

            if (poll == null)
            {
                throw new InvalidOperationException();
            }

            return poll.Votes.Count;
        }

        public string View(string question)
        {
            Poll poll = Polls.FirstOrDefault(p => p.Question.Equals(question));

            if (poll == null)
            {
                throw new InvalidOperationException();
            }

            StringBuilder outputResults = new StringBuilder();
            outputResults.AppendLine($"Question: {poll.Question}");
            outputResults.AppendLine("```Markdown");
            foreach (var choice in poll.Choices)
            {
                outputResults.AppendLine($"# {choice.Value}");
            }
            outputResults.AppendLine("```\r\n");
            outputResults.AppendLine($"Votes: {poll.Votes.Count.ToString()}");
            outputResults.Append($"Remaining: {poll.GetTimeRemaining()}");

            return outputResults.ToString();
        }

        public string Search(string term)
        {
            if (!Polls.Any(p => p.Question.Contains(term)))
            {
                return "No results found.";
            }

            StringBuilder outputResults = new StringBuilder();
            outputResults.AppendLine("Results:");
            outputResults.AppendLine("```Markdown");
            foreach(var poll in Polls.Where(p => p.Question.Contains(term) && p.IsVotingAvailable()))
            {
                outputResults.AppendLine($"# {poll.Question}");
            }
            outputResults.Append("```");

            return outputResults.ToString();
        }
    }
}
