using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Smab.Platforms.Discord.Administration.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smab.Platforms.Discord.Bots
{
    public class AdministratorBot
    {
        private readonly IServiceProvider _serviceProvider;

        private CommandService _commands;
        private DiscordSocketClient _client;

        public AdministratorBot(IServiceProvider serviceProvider) { _serviceProvider = serviceProvider; }

        public async Task Start(string token)
        {
            _commands = new CommandService();
            _client = new DiscordSocketClient();

            _client.UserJoined += OnUserJoined;
            _client.UserLeft += OnUserLeft;
            _client.Log += Log;
            _client.MessageReceived += OnMessageReceived;

            await InstallCommands();

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();
        }

        public async Task Stop()
        {
            await _client.StopAsync();
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private async Task InstallCommands()
        {
            await _commands.AddModuleAsync<MuteCommand>();
            await _commands.AddModuleAsync<UnmuteCommand>();
        }

        private async Task OnUserJoined(SocketGuildUser messageArgs)
        {
            Console.WriteLine($"{messageArgs.Username} joined the server.");
            var channel = messageArgs.Guild.Channels.FirstOrDefault(c => c.Name.Equals("announcements"));
            await (channel as SocketTextChannel).SendMessageAsync($"{messageArgs.Username} has joined the server.");
        }

        private async Task OnUserLeft(SocketGuildUser messageArgs)
        {
            Console.WriteLine($"{messageArgs.Username} left the server.");
            var channel = messageArgs.Guild.Channels.FirstOrDefault(c => c.Name.Equals("announcements"));
            await (channel as SocketTextChannel).SendMessageAsync($"{messageArgs.Username} has left the server.");
        }

        private async Task OnMessageReceived(SocketMessage messageArgs)
        {
            var message = messageArgs as SocketUserMessage;
            if (message == null) return;

            int position = 0;
            if (!(message.HasCharPrefix('!', ref position) ||
                message.HasMentionPrefix(_client.CurrentUser, ref position))) return;

            var commandContext = new CommandContext(_client, message);
            var commandResult = await _commands.ExecuteAsync(commandContext, position, _serviceProvider);

            //if (!commandResult.IsSuccess)
            //    await commandContext.Channel.SendMessageAsync(commandResult.ErrorReason);
        }
    }
}
