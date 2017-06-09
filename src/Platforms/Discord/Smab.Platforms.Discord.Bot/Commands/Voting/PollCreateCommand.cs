using Discord.Commands;
using Smab.Systems.Voting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Smab.Platforms.Discord.Commands.Voting
{
    public class PollCreateCommand : ModuleBase
    {
        private readonly IPollManager _pollManager;
        public PollCreateCommand(IPollManager pollManager)
        {
            _pollManager = pollManager;
        }

        [Command("poll create"), Summary("Creates a new poll to allow people to vote for their favorite choice.")]
        public async Task Execute(string question, string duration)
        {
            try
            {
                _pollManager.Create(question, duration);

                await base.ReplyAsync("Poll created successfully.");
            }
            catch(InvalidOperationException)
            {
                await base.ReplyAsync("Poll was not created.");
            }            
        }
    }
}
