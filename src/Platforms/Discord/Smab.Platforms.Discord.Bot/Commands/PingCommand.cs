using Discord.Commands;
using System;
using System.Threading.Tasks;

namespace Smab.Platforms.Discord.Commands
{
    public class PingCommand : ModuleBase
    {
        [Command("ping"), Summary("Pings the bot.")]
        public async Task Execute()
        {
            await base.ReplyAsync("Pong!");
        }
    }
}
