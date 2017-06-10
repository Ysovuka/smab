using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Smab.Platforms.Discord.Bots;
using Smab.Platforms.Discord.Commands;
using Smab.Platforms.Discord.Commands.Voting;
using Smab.Systems.Tasks;
using Smab.Systems.Voting;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Smab.Platforms
{
    class Program
    {
        private GameBot _gameBot;
        private VoteBot _voteBot;
        private AdministratorBot _administratorBot;
        private IServiceProvider _serviceProvider;

        public static void Main(string[] args)
            => new Program().MainAsync(args).GetAwaiter().GetResult();

        public async Task MainAsync(string[] args)
        {
            Console.CancelKeyPress += OnCancelKeyPress;

            _serviceProvider = RegisterServices().BuildServiceProvider();

            foreach (string token in args)
            {
                string[] tokenSplit = token.Split('=');
                if (tokenSplit.Length > 0)
                {
                    if (tokenSplit[0].StartsWith("-vote"))
                    {
                        await Task.Run(async () =>
                        {
                            _voteBot = new VoteBot(_serviceProvider);
                            await _voteBot.Start(tokenSplit[1].Replace("=", ""));
                        });
                    }

                    if (tokenSplit[0].StartsWith("-administrator"))
                    {
                        await Task.Run(async () =>
                        {
                            _administratorBot = new AdministratorBot(_serviceProvider);
                            await _administratorBot.Start(tokenSplit[1].Replace("=", ""));
                        });
                    }

                    if (tokenSplit[0].StartsWith("-game"))
                    {
                        await Task.Run(async () =>
                        {
                            _gameBot = new GameBot(_serviceProvider);
                            await _gameBot.Start(tokenSplit[1].Replace("=", ""));
                        });
                    }
                }
            }

            await Task.Delay(-1);
        }

        private async void OnCancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            await _voteBot.Stop();
            await _administratorBot.Stop();
        }

        private IServiceCollection RegisterServices()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton<ITaskScheduler, Systems.Tasks.TaskScheduler>();
            serviceCollection.AddSingleton<IPollManager, PollManager>();

            return serviceCollection;
        }

       


    }
}