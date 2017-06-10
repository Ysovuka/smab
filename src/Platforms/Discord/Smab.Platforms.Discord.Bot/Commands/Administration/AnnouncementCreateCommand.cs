using Discord;
using Discord.Commands;
using Smab.Systems.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smab.Platforms.Discord.Commands.Administration
{
    public class AnnouncementCreateCommand : ModuleBase
    {
        private readonly ITaskScheduler _taskScheduler;
        public AnnouncementCreateCommand(ITaskScheduler taskScheduler)
        {
            _taskScheduler = taskScheduler;
        }

        [Command("announce create"), Summary("Creates an announcement to run at the specified interval.")]
        public async Task Execute(string name, string announcement, string interval)
        {
            try
            {
                if (await _taskScheduler.Create(name,
                    async () =>
                    {
                        await Context.Channel.SendMessageAsync(announcement);
                    }, interval))
                {
                    await Context.Channel.SendMessageAsync($"Announcement was successfully created.");
                }
                else
                {
                    await Context.Channel.SendMessageAsync($"Announcement with that name already exists.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                await base.ReplyAsync($"Unable to create announcement.");
            }
        }
    }
}
