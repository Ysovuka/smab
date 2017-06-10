using Discord.Commands;
using Smab.Systems.Tasks;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Smab.Platforms.Discord.Commands.Administration
{
    public class AnnouncementStartCommand : ModuleBase
    {
        private readonly ITaskScheduler _taskScheduler;
        public AnnouncementStartCommand(ITaskScheduler taskScheduler)
        {
            _taskScheduler = taskScheduler;
        }

        [Command("announce start"), Summary("Starts the specified announcement.")]
        public async Task Execute(string name)
        {
            try
            {
                await _taskScheduler.Start(name);
                
                await Context.Channel.SendMessageAsync($"Announcement was successfully started.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                await base.ReplyAsync($"Failed to start announcement.");
            }
        }
    }
}
