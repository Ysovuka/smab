using Discord.Commands;
using Smab.Systems.Gaming;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
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
        public async Task ExecutePlus(string diceType = "1d100", string @operator = "", string inputNumber = "0")
        {
            try
            {
                long diceAmount = 0;
                long diceSize = 0;
                if (diceType.StartsWith("d"))
                {
                    diceAmount = 1;
                    diceSize = Convert.ToInt64(diceType.Replace("d", ""));
                }
                else
                {
                    string[] diceTypeSplit = diceType.ToLower().Split('d');
                    if (diceTypeSplit.Length <= 0 || diceTypeSplit.Length >= 3)
                    {
                        throw new InvalidOperationException();
                    }

                    diceAmount = Convert.ToInt64(diceTypeSplit[0].Replace("d", ""));
                    diceSize = Convert.ToInt64(diceTypeSplit[1].Replace("d", ""));
                }
                double number = 0;
                if (Regex.Matches(inputNumber, @"[a-zA-Z]").Count > 0)
                    if (!inputNumber.ToLower().Equals("pi"))
                        throw new InvalidOperationException();
                    else
                        number = Math.PI;
                else
                    number = Convert.ToDouble(inputNumber);

                Dice die = new Dice();
                await Context.Channel.SendMessageAsync
                    ($"{Context.Message.Author.Username} rolls {Math.Ceiling(die.Roll(diceAmount, diceSize, @operator, number))}.");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                await Context.Channel.SendMessageAsync("Unable to get dice from the void.");
            }
        }
    }
}
