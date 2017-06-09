using Discord.Commands;
using Smab.Systems.Voting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Smab.Platforms.Discord.Commands.Voting
{
    public class PollCloseCommand : ModuleBase
    {
        private readonly IPollManager _pollManager;
        public PollCloseCommand(IPollManager pollManager)
        {
            _pollManager = pollManager;
        }

        [Command("poll close"), Summary("Closes the specified poll from being voted on.")]
        public async Task Execute(string question)
        {
            try
            {
                _pollManager.Close(question);

                await base.ReplyAsync($"Poll '{question}' was successfully closed.");
            }
            catch (InvalidOperationException)
            {
                await base.ReplyAsync("Poll could not be closed.");
            }
        }
    }
}
