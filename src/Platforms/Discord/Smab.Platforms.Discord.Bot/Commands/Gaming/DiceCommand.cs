using Discord.Commands;
using Smab.Systems.Gaming;
using System;
using System.Collections.Concurrent;
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
            catch
            {
                await Context.Channel.SendMessageAsync("Unable to get dice from the void.");
            }
        }

        [Command("dice roll"), Summary("Rolls a die between 1 and 100 or the supplied maximum number.")]
        public async Task ExecutePlus(string diceType = "1d100", string @operator = "", double number = 0)
        {
            try
            {
                string[] diceTypeSplit = diceType.ToLower().Split('d');
                if (diceTypeSplit.Length <= 0 || diceTypeSplit.Length >= 3)
                {
                    throw new InvalidOperationException();
                }

                long diceAmount = Convert.ToInt64(diceTypeSplit[0].Replace("d", ""));
                long diceSize = Convert.ToInt64(diceTypeSplit[1].Replace("d", ""));

                Dice die = new Dice();

                await Context.Channel.SendMessageAsync
                    ($"{Context.Message.Author.Username} rolls {die.Roll(diceAmount, diceSize, @operator, number)}.");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                await Context.Channel.SendMessageAsync("Unable to get dice from the void.");
            }
        }
    }
}
