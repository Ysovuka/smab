using Discord;
using Discord.Commands;
using Smab.Systems.Voting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Smab.Platforms.Discord.Commands.Voting
{
    public class PollVoteCommand : ModuleBase
    {
        private readonly IPollManager _pollManager;
        
        public PollVoteCommand(IPollManager pollManager)
        {
            _pollManager = pollManager;
        }

        [Command("poll vote"), Summary("Closes the specified poll from being voted on.")]
        public async Task Execute(string question, string choice)
        {
            try
            {
                if (_pollManager.Vote(Context.Message.Author.Id.ToString(), question, choice))
                {
                    await base.ReplyAsync($"Thank you for voting, {Context.Message.Author.Username}.");
                }
                else
                {
                    await base.ReplyAsync($"Unfortunately you've already voted on this poll, {Context.Message.Author.Username}.");
                }

            }
            catch (InvalidOperationException)
            {
                await base.ReplyAsync("No results found.");
            }
        }
    }
}
