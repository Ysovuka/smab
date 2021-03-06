﻿using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Smab.Platforms.Discord.Commands.Voting;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Smab.Platforms
{
    public class VoteBot
    {
        private readonly IServiceProvider _serviceProvider;

        private CommandService _commands;
        private DiscordSocketClient _client;
        
        public VoteBot(IServiceProvider serviceProvider) { _serviceProvider = serviceProvider; }

        public async Task Start(string token)
        {
            _commands = new CommandService();
            _client = new DiscordSocketClient();

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
            await _commands.AddModuleAsync<PollAddChoiceCommand>();
            await _commands.AddModuleAsync<PollCloseCommand>();
            await _commands.AddModuleAsync<PollCreateCommand>();
            await _commands.AddModuleAsync<PollOpenCommand>();
            await _commands.AddModuleAsync<PollResultsCommand>();
            await _commands.AddModuleAsync<PollSearchCommand>();
            await _commands.AddModuleAsync<PollViewCommand>();
            await _commands.AddModuleAsync<PollVoteCommand>();
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
