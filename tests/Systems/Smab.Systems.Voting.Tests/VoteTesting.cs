using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Smab.Systems.Voting.Tests
{
    public class VoteTesting
    {
        [Fact]
        public void Create()
        {
            PollManager _pollManager = new PollManager();

            Poll poll = _pollManager.Create("Alice in Wonderland", "1.00:00:00");

            Assert.Equal("Alice in Wonderland", poll.Question);
        }

        [Fact]
        public void AddChoice()
        {
            PollManager _pollManager = new PollManager();
            Poll poll = _pollManager.Create("Alice in Wonderland", "3.00:00:00");

            poll.AddChoice("Through the looking glass.");
            Assert.Equal(1, poll.Choices.Count);
            poll.AddChoice("Down the rabbit hole.");
            Assert.Equal(2, poll.Choices.Count);
        }

        [Fact]
        public void Search()
        {
            PollManager _pollManager = new PollManager();
            _pollManager.Create("Poll 1", "3.00:00:00");
            _pollManager.Create("Poll 2", "3.00:00:00");
            _pollManager.Create("Poll 3", "3.00:00:00");
            _pollManager.Create("Poll 4", "3.00:00:00");
            _pollManager.Create("Poll 5", "3.00:00:00");

            _pollManager.Open("Poll 1");

            _pollManager.Open("Poll 2");
            _pollManager.Close("Poll 2");

            _pollManager.Open("Poll 3");

            Assert.Equal(@"Results:
```Markdown
# Poll 1
# Poll 3
```", _pollManager.Search("Poll"));
        }

        [Fact]
        public void Vote()
        {
            PollManager _pollManager = new PollManager();
            Poll poll = _pollManager.Create("Alice in Wonderland", "3.00:00:00");

            _pollManager.AddChoice("Alice in Wonderland", "Through the looking glass.");
            _pollManager.AddChoice("Alice in Wonderland", "Down the rabbit hole.");
            _pollManager.Open("Alice in Wonderland");
            _pollManager.Vote("0123456789", "Alice in Wonderland", "Down the rabbit hole.");

            Assert.Equal(1, _pollManager.GetVotes("Alice in Wonderland"));

            _pollManager.Close("Alice in Wonderland");
            Assert.Equal(@"Results for 'Alice in Wonderland':
```Markdown
1 - Down the rabbit hole.
0 - Through the looking glass.
```", _pollManager.GetResults("Alice in Wonderland"));

            Assert.Equal(@"Question: Alice in Wonderland
```Markdown
# Through the looking glass.
# Down the rabbit hole.
```

Votes: 1
Remaining: Closed.", _pollManager.View("Alice in Wonderland"));
        }
    }
}
