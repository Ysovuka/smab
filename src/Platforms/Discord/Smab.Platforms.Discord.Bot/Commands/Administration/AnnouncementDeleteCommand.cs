using Discord.Commands;
using Smab.Systems.Tasks;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Smab.Platforms.Discord.Commands.Administration
{
    public class AnnouncementDeleteCommand : ModuleBase
    {
        private readonly ITaskScheduler _taskScheduler;
        public AnnouncementDeleteCommand(ITaskScheduler taskScheduler)
        {
            _taskScheduler = taskScheduler;
        }

        [Command("announce delete"), Summary("Deletes the specified announcement.")]
        public async Task Execute(string name)
        {
            try
            {
                if (await _taskScheduler.Delete(name))
                {
                    await Context.Channel.SendMessageAsync($"Announcement was successfully deleted.");
                }
                else
                {
                    await Context.Channel.SendMessageAsync($"Unable to delete announcement.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                await base.ReplyAsync($"Unable to delete announcement.");
            }
        }
    }
}
