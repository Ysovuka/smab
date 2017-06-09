using Discord.Commands;
using Smab.Systems.Voting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Smab.Platforms.Discord.Commands.Voting
{
    public class PollSearchCommand : ModuleBase
    {
        private readonly IPollManager _pollManager;
        public PollSearchCommand(IPollManager pollManager)
        {
            _pollManager = pollManager;
        }

        [Command("poll search"), Summary("Closes the specified poll from being voted on.")]
        public async Task Execute(string term)
        {
            try
            {
                await base.ReplyAsync(_pollManager.Search(term));
            }
            catch (InvalidOperationException)
            {
                await base.ReplyAsync("No results found.");
            }
        }
    }
}
