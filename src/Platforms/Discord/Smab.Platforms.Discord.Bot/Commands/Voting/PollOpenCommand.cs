using Discord.Commands;
using Smab.Systems.Voting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Smab.Platforms.Discord.Commands.Voting
{
    public class PollOpenCommand : ModuleBase
    {
        private readonly IPollManager _pollManager;
        public PollOpenCommand(IPollManager pollManager)
        {
            _pollManager = pollManager;
        }

        [Command("poll open"), Summary("Opens the specified poll for voting.")]
        public async Task Execute(string question)
        {
            try
            {
                _pollManager.Open(question);

                await base.ReplyAsync($"Poll '{question}' was successfully opened for voting.");
            }
            catch (InvalidOperationException)
            {
                await base.ReplyAsync("Poll could not be opened.");
            }
        }
    }
}
