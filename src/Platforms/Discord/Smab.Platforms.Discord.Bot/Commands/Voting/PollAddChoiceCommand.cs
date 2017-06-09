using Discord.Commands;
using Smab.Systems.Voting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Smab.Platforms.Discord.Commands.Voting
{
    public class PollAddChoiceCommand : ModuleBase
    {
        private readonly IPollManager _pollManager;
        public PollAddChoiceCommand(IPollManager pollManager)
        {
            _pollManager = pollManager;
        }

        [Command("poll add"), Summary("Adds a new choice to the specified poll.")]
        public async Task Execute(string question, string choice)
        {
            try
            {
                _pollManager.AddChoice(question, choice);

                await base.ReplyAsync("Choice was successfully added to poll.");
            }
            catch (InvalidOperationException)
            {
                await base.ReplyAsync("Choice could not be added to poll.");
            }
        }
    }
}
