using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Smab.Platforms.Discord.Commands;
using Smab.Platforms.Discord.Commands.Voting;
using Smab.Systems.Voting;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Smab.Platforms
{
    class Program
    {
        private VoteBot _voteBot;
        private IServiceProvider _serviceProvider;

        public static void Main(string[] args)
            => new Program().MainAsync(args[0]).GetAwaiter().GetResult();

        public async Task MainAsync(string token)
        {
            Console.CancelKeyPress += OnCancelKeyPress;

            _serviceProvider = RegisterServices().BuildServiceProvider();

            _voteBot = new VoteBot(_serviceProvider);
            await _voteBot.Start(token);


            await Task.Delay(-1);
        }

        private async void OnCancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            await _voteBot.Stop();
        }

        private IServiceCollection RegisterServices()
        {
            var serviceCollection = new ServiceCollection();
            
            serviceCollection.AddSingleton<IPollManager, PollManager>();

            return serviceCollection;
        }

       


    }
}