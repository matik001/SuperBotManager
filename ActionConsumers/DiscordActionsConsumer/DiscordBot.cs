using Discord;
using Discord.Rest;
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
        public async Task<T> UseClient<T>(Func<DiscordSocketClient, Task<T>> func, Func<SocketMessage, Task>? onReceivedMessage = null)
        {
            using(var _client = new DiscordSocketClient())
            {
                var readyCompletionSource = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);

                if(onReceivedMessage != null)
                {
                    _client.MessageReceived += async (message) =>
                    {
                        await onReceivedMessage(message);
                    };
                }
                _client.Ready += async () =>
                {
                    readyCompletionSource.SetResult(true);
                    await Task.CompletedTask;
                };

                await _client.LoginAsync(TokenType.Bot, _token);
                await _client.StartAsync();
                await readyCompletionSource.Task;
                var res = await func(_client);
                return res;
            }
        }
        public async Task SendMessage(string message)
        {
            await UseClient(async (client) =>
            {
                foreach(var guild in client.Guilds)
                {
                    var channel = guild.TextChannels.FirstOrDefault();
                    if(channel != null)
                    {
                        await channel.SendMessageAsync(message);
                    }
                }
                return true;
            });
        }
        public async Task<string> Prompt(string message, int? spamIntervalSecs, CancellationToken cancelToken)
        {
            var sentMessages = new List<RestUserMessage>();
            var receivedMessageCompletionSource = new TaskCompletionSource<string>(TaskCreationOptions.RunContinuationsAsynchronously);
            var resultMessage = await UseClient(
                async (client) =>
                {
                    /// TODO - we can add here some limit of sent messages
                    while(true)
                    {
                        foreach(var guild in client.Guilds)
                        {
                            var channel = guild.TextChannels.FirstOrDefault();
                            if(channel != null)
                            {
                                var msg = await channel.SendMessageAsync(message);
                                sentMessages.Add(msg);
                            }
                        }

                        if(spamIntervalSecs == null)
                            return await receivedMessageCompletionSource.Task;

                        await Task.Delay(TimeSpan.FromSeconds(spamIntervalSecs.Value), cancelToken);
                        if(cancelToken.IsCancellationRequested)
                            return null;
                        if(receivedMessageCompletionSource.Task.IsCompleted)
                        {
                            return await receivedMessageCompletionSource.Task;
                        }
                    }
                },
                async (message) =>
                {
                    if(message.Reference != null && sentMessages.Any(a => message.Reference.MessageId.IsSpecified && a.Id == message.Reference.MessageId.Value))
                    {
                        await message.AddReactionAsync(new Emoji("👍")); /// 👀
                        receivedMessageCompletionSource.SetResult(message.Content);
                    }
                    await Task.CompletedTask;
                }
            );
            return resultMessage;

        }
    }
}
