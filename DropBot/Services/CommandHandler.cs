using Discord;
using Discord.Interactions;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace DropBot.Services
{
    public class CommandHandler
    {
        private const string V = "Sup";
        private readonly DiscordSocketClient _discord;
        private readonly CommandService _commands;
        private readonly IConfigurationRoot _config;
        private readonly IServiceProvider _provider;

        // DiscordSocketClient, CommandService, IConfigurationRoot, and IServiceProvider are injected automatically from the IServiceProvider
        public CommandHandler(
            DiscordSocketClient discord,
            CommandService commands,
            IConfigurationRoot config,
            IServiceProvider provider)
        {
            _discord = discord;
            _commands = commands;
            _config = config;
            _provider = provider;

            _discord.MessageReceived += OnMessageReceivedAsync;
            _discord.ButtonExecuted += ButtonHandlerAsync;
            _discord.SetGameAsync("dropbot.games");
        }

        private async Task OnMessageReceivedAsync(SocketMessage s)
        {
            var msg = s as SocketUserMessage;     // Ensure the message is from a user/bot
            if (msg == null) return;
            if (msg.Author.Id == _discord.CurrentUser.Id) return;     // Ignore self when checking commands

            var context = new SocketCommandContext(_discord, msg);     // Create the command context

            int argPos = 0;     // Check if the message has a valid command prefix
            if (!(msg.HasCharPrefix('!', ref argPos) || msg.HasMentionPrefix(_discord.CurrentUser, ref argPos)) || msg.Author.IsBot)
            {
                return;
            }
            if (msg.HasCharPrefix('!', ref argPos) || msg.HasMentionPrefix(_discord.CurrentUser, ref argPos))
            {
                var result = await _commands.ExecuteAsync(context, argPos, _provider);     // Execute the command

                if (!result.IsSuccess)     // If not successful, reply with the error.
                    await context.Channel.SendMessageAsync($"Oops! Something went wrong there: *{result.ErrorReason}* Type `!help` for more information on how to use the commands.");
            }
        }

        private async Task ButtonHandlerAsync(SocketMessageComponent component)
        {
            var Embed = component.Message.Embeds.GetEnumerator();
            Embed.MoveNext();
            var Title = Embed.Current.Title;
            var fields = Embed.Current.Fields.GetEnumerator();
            fields.MoveNext();
            var optionOne = fields.Current;
            fields.MoveNext();
            var optionTwo = fields.Current;
            fields.MoveNext();
            var vote1 = fields.Current;
            fields.MoveNext();
            var vote2 = fields.Current;

            // We can now check for our custom id
            switch (component.Data.CustomId)
            {
                case "Option-1":
                    var embed = component.Message.Embeds;
                    int.TryParse(vote1.Value, out int x);
                    var newEmbed = new EmbedBuilder()
                    .WithTitle(Title)
                    .AddField(optionOne.Name, optionOne.Value, true)
                    .AddField(optionTwo.Name, optionTwo.Value, true)
                    .AddField(vote1.Name, x + 1)
                    .AddField(vote2.Name, vote2.Value)
                    .WithColor(Color.DarkRed)
                    .WithCurrentTimestamp();

                    await component.Message.ModifyAsync(x =>
                    {
                        x.Embed = newEmbed.Build();
                    });
                    await component.RespondAsync($"{component.User.Mention} has voted!");
                    break;

                case "Option-2":
                    var embed2 = component.Message.Embeds;
                    int.TryParse(vote2.Value, out int x2);
                    var newEmbed2 = new EmbedBuilder()
                    .WithTitle(Title)
                    .AddField(optionOne.Name, optionOne.Value, true)
                    .AddField(optionTwo.Name, optionTwo.Value, true)
                    .AddField(vote1.Name, vote1.Value)
                    .AddField(vote2.Name, x2 + 1)
                    .WithColor(Color.Blue)
                    .WithCurrentTimestamp();

                    await component.Message.ModifyAsync(x =>
                    {
                        x.Embed = newEmbed2.Build();
                    });
                    await component.RespondAsync($"{component.User.Mention} has voted!");
                    break;
            }
        }
    }
}