using Discord.Commands;
using Smab.Systems.Gaming;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Smab.Platforms.Discord.Commands.Gaming
{
    public class DiceCommand : ModuleBase
    {
        [Command("dice"), Summary("Rolls a die between 1 and 100 or the supplied maximum number.")]
        public async Task Execute(double maximum = 100)
        {
            try
            {
                Dice die = new Dice();
                await Context.Channel.SendMessageAsync($"{Context.Message.Author.Username} rolls {Math.Round(die.Roll(maximum))}.");
            }
            catch (Exception ex)
            {
                await Context.Channel.SendMessageAsync("Unable to get dice from the void.");
            }
        }
    }
}
