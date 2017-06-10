using Discord.Commands;
using Smab.Systems.Tasks;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Smab.Platforms.Discord.Commands.Administration
{
    public class AnnouncementStopCommand : ModuleBase
    {
        private readonly ITaskScheduler _taskScheduler;
        public AnnouncementStopCommand(ITaskScheduler taskScheduler)
        {
            _taskScheduler = taskScheduler;
        }

        [Command("announce stop"), Summary("Starts the specified announcement.")]
        public async Task Execute(string name)
        {
            try
            {
                await _taskScheduler.Stop(name);

                await Context.Channel.SendMessageAsync($"Announcement was successfully stopped.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                await base.ReplyAsync($"Failed to stop announcement.");
            }
        }
    }
}
