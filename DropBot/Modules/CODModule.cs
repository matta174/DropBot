using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;


namespace DropBot.Modules
{

    [Name("COD")]
    public class CODModule : ModuleBase<SocketCommandContext>
    {
        readonly TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
        private readonly Random _random = new Random();
        private static string tempURL = "https://www.callofduty.com/content/atvi/callofduty/blog/web/en/home/2021/11/call-of-duty-vanguard-warzone-caldera-season-one-map-intel.html";

        // ToDo: Update the links once we have an interactive map on CoD's website. 

        private readonly Dictionary<string, string> _locationIntelDict = new Dictionary<string, string>
        {
            {"Arsenal",tempURL},
            {"Docks",tempURL},
            {"Runway",tempURL},
            {"Ruins",tempURL},
            {"Mines",tempURL},
            {"Peak",tempURL},
            {"Beachhead",tempURL},
            {"Village",tempURL},
            {"Lagoon",tempURL},
            {"Airfield",tempURL},
            {"Fields",tempURL},
            {"Sub Pen",tempURL},
            {"Power Plant",tempURL},
            {"Capital",tempURL},
            {"Resort",tempURL}
        };

        private readonly string[] _locations = {
            "Arsenal", "Docks", "Runway", "Ruins", "Mines", "Peak", "Beachhead", "Village", "Lagoon", "Airfield",
            "Fields", "Sub Pen", "Power Plant", "Capital", "Resort"
        };

        [Command("warzonedrop"), Alias("wz", "warzone", "warzonedrop", "wzdrop")]
        [Summary("Random Warzone Drop Location Picker")]
        public async Task WarzoneDrop()
        {
            var index = _random.Next(_locations.Length);

            var builder = new EmbedBuilder
            {
                Color = new Color(252, 186, 3),
                Title = _locations[index],
                Url = _locationIntelDict[_locations[index]],
                Description = " \u2139 Click the link above for intel about " + _locations[index]
            }.WithCurrentTimestamp();

            await ReplyAsync(string.Empty, false, builder.Build());
        }

        [Command("warzonevote"), Alias("wzvote", "wzv")]
        [Summary("Random Warzone Drop Location Vote")]
        public async Task WarzoneVote()
        {
            var index = _random.Next(_locations.Length);
            var index2 = _random.Next(_locations.Length);
            while (index == index2)
            {
                index2 = _random.Next(_locations.Length);
            }
            var redCircle = new Emoji("🔴");
            var blueCircle = new Emoji("🔵");



            var builder = new ComponentBuilder()
                .WithButton($"{_locations[index]}", "Option-1", ButtonStyle.Danger, redCircle)
                .WithButton($"{_locations[index2]}", "Option-2", ButtonStyle.Primary, blueCircle);
            

            var embed = new EmbedBuilder()
                .WithTitle($"Warzone Vote")
                .AddField("Location One",$"{redCircle} {_locations[index]}", true)
                .AddField("Location Two", $"{blueCircle} {_locations[index2]}", true)
                .AddField("Votes for Location One",0)
                .AddField("Votes for Location Two",0)
                .WithCurrentTimestamp()
                .WithColor(Color.Purple);

            await ReplyAsync(null, components: builder.Build(), embed: embed.Build());
        }
    }


}