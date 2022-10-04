using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Discord.Webhook;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BotDiscord
{
    class Program
    {
        static void Main(string[] args) => new Program().RunBotAsync().GetAwaiter().GetResult();
     
        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;
        public async Task RunBotAsync()
        {
            _client = new DiscordSocketClient();
            _commands = new CommandService();
            _services = new ServiceCollection().AddSingleton(_client).AddSingleton(_commands).BuildServiceProvider();

            string tokenBot = "ODcyNTU3MzQ0MjU1NTI0OTI0.YQrmWg.J0orfdQOXH2GJsO5_DDH5cwe8_U";

            // assinatura de evento

            _client.Ready += ClientReady;
            _client.Log += ClientLog;
            _client.UserJoined += UserJoinned;  

            await ClientReady();
            await ComandosBot();
            _client.LoginAsync(TokenType.Bot, tokenBot);
            await _client.StartAsync();
            await Task.Delay(-1);
        }

        private async Task UserJoinned(SocketGuildUser user)
        {
            var usuario = user.Guild;
        }

        private Task ClientLog(LogMessage arg)
        {
            Console.WriteLine(arg);
            return Task.CompletedTask;
        }

        private async Task ClientReady()
        {
            await _client.SetGameAsync("cobaia em C#", "http://google.com", StreamType.NotStreaming);
        }
        public async Task ComandosBot()
        {
            _client.MessageReceived += IniciandoComandos;
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly());
        }

        private async Task IniciandoComandos(SocketMessage arg)
        {
            var mensagem = arg as SocketUserMessage;
            if (mensagem is null || mensagem.Author.IsBot) return;

            var Context = new SocketCommandContext(_client, mensagem);
            int argPost = 0;
            if (mensagem.HasStringPrefix(";", ref argPost))
            {
                var result = await _commands.ExecuteAsync(Context, argPost);
                if(!result.IsSuccess)
                {
                    Console.WriteLine(result.ErrorReason);
                }
            }
        }
    }
}
