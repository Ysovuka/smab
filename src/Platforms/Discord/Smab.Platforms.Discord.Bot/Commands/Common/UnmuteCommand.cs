using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smab.Platforms.Discord.Commands.Common
{
    public class UnmuteCommand : ModuleBase
    {
        [Command("unmute"), Summary("Mutes the specified person for the specified duration.")]
        public async Task Execute(IUser user)
        {
            try
            {
                if (!Context.Guild.Roles.Any(r => r.Name == "Muted"))
                {
                    throw new InvalidOperationException();
                }

                var mutedRole = Context.Guild.Roles.FirstOrDefault(r => r.Name == "Muted");
                IGuildUser userInfo = user as IGuildUser;
                await userInfo.RemoveRoleAsync(mutedRole);

                await Context.Channel.SendMessageAsync($"{userInfo.Username} has been unmuted.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                if (user != null) await base.ReplyAsync($"Unable to mute {user.Username}.");
            }
        }
    }
}