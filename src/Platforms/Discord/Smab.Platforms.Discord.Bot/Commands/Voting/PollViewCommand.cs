using Discord.Commands;
using Smab.Systems.Voting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Smab.Platforms.Discord.Commands.Voting
{
    public class PollViewCommand : ModuleBase
    {
        private readonly IPollManager _pollManager;
        public PollViewCommand(IPollManager pollManager)
        {
            _pollManager = pollManager;
        }

        [Command("poll view"), Summary("Closes the specified poll from being voted on.")]
        public async Task Execute(string question)
        {
            try
            {
                await base.ReplyAsync(_pollManager.View(question));
            }
            catch (InvalidOperationException)
            {
                await base.ReplyAsync("No results found.");
            }
        }
    }
}
