using Discord.Net.WebSockets;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;

namespace Smab.Platforms.Discord.Administration.Common
{
    public class MuteCommand : ModuleBase
    {
        [Command("mute"), Summary("Mutes the specified person for the specified duration.")]
        public async Task Execute(IUser user, string duration)
        {
            try
            {
                if (!Context.Guild.Roles.Any(r => r.Name == "Administrator"))
                {
                    return;
                }
                
                IGuildUser messageAuthor = Context.Message.Author as IGuildUser;
                
                if (!Context.Guild.Roles.Any(r => r.Name == "Muted"))
                {
                    await Context.Guild.CreateRoleAsync("Muted", new GuildPermissions());
                }

                var mutedRole = Context.Guild.Roles.FirstOrDefault(r => r.Name == "Muted");
                IGuildUser userInfo = user as IGuildUser;
                if (!userInfo.RoleIds.Any(id => id == mutedRole.Id))
                {
                    await userInfo.AddRoleAsync(mutedRole);
                    await Context.Channel.SendMessageAsync($"{userInfo.Username} has been muted.");
                }
                else
                {
                    await Context.Channel.SendMessageAsync($"{userInfo.Username} is already muted.");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                if (user != null) await base.ReplyAsync($"Unable to mute {user.Username}.");
            }
        }
    }
}
