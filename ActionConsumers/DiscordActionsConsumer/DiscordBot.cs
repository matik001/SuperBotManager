using Discord;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordActionsConsumer
{
    internal class DiscordBot
    {
        private string _token;

        public DiscordBot(string token)
        {
            this._token = token;
        }
        public async Task SendMessage(string message)
        {
            using(var _client = new DiscordSocketClient())
            {
                var readyCompletionSource = new TaskCompletionSource<bool>();

                _client.Ready += () =>
                {
                    readyCompletionSource.SetResult(true);
                    return Task.CompletedTask;
                };

                await _client.LoginAsync(TokenType.Bot, _token);
                await _client.StartAsync();
                await readyCompletionSource.Task;


                foreach(var guild in _client.Guilds)
                {
                    var channel = guild.TextChannels.FirstOrDefault();
                    if(channel != null)
                    {
                        await channel.SendMessageAsync(message);
                    }
                }
            }
        }
    }
}
