using System;using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smab.Systems.Voting
{
    public class Poll
    {
        public IList<Choice> Choices { get; set; } = new List<Choice>();
        public IList<Vote> Votes = new List<Vote>();

        private DateTime _open;
        private DateTime _close;
        private TimeSpan _duration;

        public Poll(string question)
        {
            Question = question;
        }

        public string Question { get; private set; }

        public void SetDuration(string timeDuration)
        {
            _duration = TimeSpan.Parse(timeDuration);
        }

        public bool AddChoice(string choice)
        {
            if (Choices.Any(c => c.Value.Equals(choice)))
            {
                return false;
            }

            Choices.Add(new Choice(choice));

            return true;
        }

        public bool IsVotingAvailable()
        {
            if (_open == default(DateTime)) return false;
            if (_close != default(DateTime)) return false;

            DateTime expires = _open.Add(_duration);
            return expires > DateTime.Now;
        }

        public void Open() => _open = DateTime.Now;
        public void Close() => _close = DateTime.Now;

        public bool Vote(string id, string choice)
        {
            Choice selectedChoice = Choices.FirstOrDefault(c => c.Value.Equals(choice));

            if (choice == null)
            {
                throw new InvalidOperationException();
            }

            if (Votes.Any(v => v.Id.Equals(id)))
            {
                return false;
            }

            Votes.Add(new Vote(id, selectedChoice));

            return true;
        }

        public string GetResults()
        {
            if (IsVotingAvailable()) throw new InvalidOperationException();

            StringBuilder outputResults = new StringBuilder();
            outputResults.AppendLine($"Results for '{Question}':");
            outputResults.AppendLine("```Markdown");
            foreach (var group in Votes.GroupBy(v => v.Choice))
            {
                outputResults.AppendLine($"{Votes.Count(v => v.Choice.Value.Equals(group.Key.Value))} - {group.Key.Value}");
            }

            foreach(Choice choice in Choices.Where(c => !Votes.Any(v => v.Choice.Value.Equals(c.Value))))
            {
                outputResults.AppendLine($"0 - {choice.Value}");
            }
            outputResults.Append("```");

            return outputResults.ToString();
        }

        public string GetTimeRemaining()
        {
            if (_open == default(DateTime))
            {
                throw new InvalidOperationException();
            }

            DateTime expires = _open.Add(_duration);

            if (expires < DateTime.Now || _close != default(DateTime))
            {
                return $"Closed.";
            }

            TimeSpan remaining = (expires - DateTime.Now);
            return $"{remaining.Days}d {remaining.Hours}h {remaining.Minutes}m {remaining.Seconds}s";
        }
    }
}
