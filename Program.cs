using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;



namespace TheShooterBot
{
    public class Program
    {
        static void Main(string[] args)
            => new Program().StartAsync().GetAwaiter().GetResult();

        private DiscordSocketClient _client;
        private CommandHandler _handler;

        public async Task StartAsync()
        {
            
            _client = new DiscordSocketClient();

            await _client.LoginAsync(TokenType.Bot, "Token of Bot Located Here.  Find it in Discord Developer panel.  Do NOT get rid of quotes!");

            await _client.StartAsync();

            _handler = new CommandHandler(_client);
            await Task.Delay(-1);
        }
        
    }
}