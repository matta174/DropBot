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
        private readonly DiscordSocketClient _discord;
        private readonly CommandService _commands;
        private readonly IConfigurationRoot _config;
        private readonly IServiceProvider _provider;
        readonly Random _random = new Random();

        private readonly string[] _locationsErangle = new[]
        {
            "Zharki", "Severny","Stalber", "Kameshki","Georgopol","Rozhok","Yasnaya Polyana","Lipovka","Gatka","Pochinki","Mylta Power","Mylta","Promorsk","Novorepnoye","Sosnovka Military Base"
        };
        private readonly string[] _locationsMiramar = new[]
        {
           "Alcantara","La Cobreria", "Torre Ahumada","Campo Militar", "Cruz del Valle","Tierra Bronca","El Pozo", "San Martin","El Azahar","Monte Nuevo","Pecado","La Bendita", "Impala","Chumacera","Los Leones","Puerto Paraiso","Valle del Mar","Prison", "Los Higos"
        };
        private readonly string[] _locationsSanhok = new[]
        {
            "Ha Tinh","Tat Mok","Khao","Mongnai","Camp Alpha","Bootcamp","Paradise Resort","Camp Bravo","Ruins","Pai Nan","Quarry","Lakawi","Tambang","Kampong","Na Kham","Sahmee","Camp Charlie","Ban Tai","Docks"
        };
        private readonly string[] _locationsVikendi = new[]
        {
            "Port","Zabava","Cosmodrome","Trevno","Krichas","Coal Mine","Dobro Mesto","Goroka","Mount Kreznic","Podvosto","Peshkova","Villa","Cement Factory","Vihar","Movatra","Dino Park","Tovar","Castle","Sawmill","Abbey","Volnova","Cantra","Hot Springs","Milnar","Pilnec","Winery"
        };
        private readonly string[] _locationsKingsCanyon = new[]
{
            "Slum Lakes","Artillery","Relay","The Pit", "Containment","Wetlands","Runoff","Bunker","The Cage","Swamps","Airbase","Market","Hydro Dam","Skull Town","Repulsor","Thunderdome","Water Treatment"
        };
        private readonly string[] _locationsWorldsEdge = new[]
        {
            "Skyhook","Survey Camp", "Refinery","The Epicenter","Launch Site","Fragment West","Fragment East","Overlook","Lava Fissure","The Train Yard", "Harvester", "The Geyser","Thermal Station","Sorting Factory","The Tree","The Dome","Lava City"
        };
        private readonly string[] _locationsOlympus = new[]
        {
            "Docks","Carrier","Power Grid","Rift","Oasis","Turbine","Energy Depot","Gardens","Estates","Hammond Labs","Grow Towers","Elysium","Hydroponics","Solar Array","Orbital Cannon","Bonsai Plaza"
        };

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
            _discord.SelectMenuExecuted += MenuHandlerAsync;
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
            fields.MoveNext();
            var voters = fields.Current;
            var votersString = voters.Value;
            if (voters.Value == "No one has voted yet.")
                votersString = String.Empty;
            if (votersString.Contains(component.User.Username))
            {
                await component.DeferAsync(ephemeral: true);
                return;
            }

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
                    .AddField(voters.Name, $"{votersString} {component.User.Username}")
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
                    .AddField(voters.Name, $"{votersString} {component.User.Username}")
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

        private async Task MenuHandlerAsync(SocketMessageComponent component)
        {
            switch (component.Data.CustomId)
            {
                case "pubg":
                    var map = string.Join(", ", component.Data.Values);
                    await PUBGSelectHandler(component, map);
                    break;
                case "apex":
                    map = string.Join(", ", component.Data.Values);
                    await ApexSelectHandler(component, map);
                    break;

            }
        }


        private async Task PUBGSelectHandler(SocketMessageComponent component, string map)
        {
            var dropLocation = string.Empty;
            if (map == "Erangle")
            {
                dropLocation = $"{_locationsErangle[_random.Next(_locationsErangle.Length)]}";
            }
            if(map == "Miramar")
            {
                dropLocation = $"{_locationsMiramar[_random.Next(_locationsMiramar.Length)]}";
            }
            if (map == "Sanhok")
            {
                dropLocation = $"{_locationsSanhok[_random.Next(_locationsSanhok.Length)]}";
            }
            if (map == "Vikendi")
            {
                dropLocation = $"{_locationsVikendi[_random.Next(_locationsVikendi.Length)]}";
            }
            var embed = new EmbedBuilder()
                .WithTitle($"{map}: {dropLocation}")
                .WithDescription(" \u2139 Click on the link above for additional intel. ")
                .WithUrl("https://pubgmap.io/")
                .WithThumbnailUrl("https://play-lh.googleusercontent.com/JRd05pyBH41qjgsJuWduRJpDeZG0Hnb0yjf2nWqO7VaGKL10-G5UIygxED-WNOc3pg")
                .WithCurrentTimestamp()
                .WithColor(_random.Next(256), _random.Next(256), _random.Next(256));

            await component.UpdateAsync(x =>
            {
                x.Embed = embed.Build();
            });
        }

        private async Task ApexSelectHandler(SocketMessageComponent component, string map)
        {
            var dropLocation = string.Empty;
            if (map == "Kings Canyon")
            {
                dropLocation = $"{_locationsKingsCanyon[_random.Next(_locationsKingsCanyon.Length)]}";
            }
            if (map == "World's Edge")
            {
                dropLocation = $"{_locationsWorldsEdge[_random.Next(_locationsWorldsEdge.Length)]}";
            }
            if (map == "Olympus")
            {
                dropLocation = $"{_locationsOlympus[_random.Next(_locationsOlympus.Length)]}";
            }

            var embed = new EmbedBuilder()
                .WithTitle($"{map}: {dropLocation}")
                .WithThumbnailUrl("https://upload.wikimedia.org/wikipedia/en/d/db/Apex_legends_cover.jpg")
                .WithCurrentTimestamp()
                .WithColor(_random.Next(256), _random.Next(256), _random.Next(256));

            await component.UpdateAsync(x =>
            {
                x.Embed = embed.Build();
            });
        }
    }
}