using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using System.Reflection;

namespace TheShooterBot
{
    public class CommandHandler
    {
        private DiscordSocketClient _client;
        private CommandService _service;
        
        
        public CommandHandler(DiscordSocketClient client)
        {
            _client = client;
            
            _service = new CommandService();

            _service.AddModulesAsync(Assembly.GetEntryAssembly());

            _client.MessageReceived += HandleCommandAsync;

            client.UserJoined += AnnounceJoinUser;
        }

        private async Task HandleCommandAsync(SocketMessage s)
        {
            var msg = s as SocketUserMessage;
            if (msg == null) return;

            var context = new SocketCommandContext(_client, msg);

            int argPos = 0;
            if(msg.HasCharPrefix('!', ref argPos)) // Prefix for using commands, e.g., !kick Username.
            {
                var result = await _service.ExecuteAsync(context, argPos);

                if(!result.IsSuccess)
                {
                    await context.Channel.SendMessageAsync(result.ErrorReason);
                }

            }

        }
        public async Task AnnounceJoinUser(SocketGuildUser user)
        {
            var channel = _client.GetChannel("Channel ID here.  Get rid of quotes.") as SocketTextChannel;

            await channel.SendMessageAsync($"{user.Mention}, Welcome to ServerName.  Hope you enjoy your stay.");
        }


    }
}
