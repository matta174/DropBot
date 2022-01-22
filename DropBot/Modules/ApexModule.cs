using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace DropBot.Modules
{
    [Name("Apex")]
    public class ApexModule : ModuleBase<SocketCommandContext>
    {
        readonly TextInfo _textInfo = new CultureInfo("en-US", false).TextInfo;
        readonly Random _rand = new Random();
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

        private readonly Dictionary<string, string> _locationIntelDict = new Dictionary<string, string>
        {
            {"Kings Canyon", "https://www.metabomb.net/off-meta/gameplay-guides/apex-legends-map-guide"},
            {"World's Edge","https://www.dexerto.com/apex-legends/best-worlds-edge-landing-spots-apex-legends-1511158"},
            {"Olympus","https://www.rockpapershotgun.com/apex-legends-olympus-map-guide-best-locations-to-land"}
        };

        [Command("apexdrop"), Alias("apexdrop", "apex")]
        [Summary("Random Apex Drop Location Picker")]
        public async Task ApexDrop()
        {
            var index1 = _rand.Next(_locationsOlympus.Length);
            var builder1 = new EmbedBuilder()
            {
                Color = new Color(114, 0, 0),
                Title = "Olympus: " + _locationsOlympus[index1],
                Description = "\u2139 Click on the link above for additional intel. ",
                Url = "https://www.rockpapershotgun.com/apex-legends-olympus-map-guide-best-locations-to-land"
            };
            await ReplyAsync(string.Empty, false, builder1.Build());

            var index2 = _rand.Next(_locationsKingsCanyon.Length);
            var builder2 = new EmbedBuilder()
            {
                Color = new Color(0, 114, 0),
                Title = "Kings Canyon: " + _locationsKingsCanyon[index2],
                Description = " \u2139 Click on the link above for additional intel. ",
                Url = "https://www.metabomb.net/off-meta/gameplay-guides/apex-legends-map-guide"
            };
            await ReplyAsync(string.Empty, false, builder2.Build());

            var index3 = _rand.Next(_locationsWorldsEdge.Length);
            var builder3 = new EmbedBuilder()
            {
                Color = new Color(0, 0, 114),
                Title = "World's Edge: " + _locationsWorldsEdge[index3],
                Description = " \u2139 Click on the link above for additional intel. ",
                Url = "https://www.dexerto.com/apex-legends/best-worlds-edge-landing-spots-apex-legends-1511158/"
            };
            await ReplyAsync(string.Empty, false, builder3.Build());
        }

        [Command("apexdrop"), Alias("apexmap", "apex")]
        [Summary("Apex drop for a specific map")]
        public async Task ApexDrop([Remainder] string map)
        {
            if (_textInfo.ToTitleCase(map) == "Olympus")
            {
                var index = _rand.Next(_locationsOlympus.Length);
                var builder = new EmbedBuilder()
                {
                    Color = new Color(114, 0, 0),
                    Title = _locationsOlympus[index],
                    Description = " \u2139 Click on the link above for additional intel. ",
                    Url = "https://www.rockpapershotgun.com/apex-legends-olympus-map-guide-best-locations-to-land"
                };
                await ReplyAsync(string.Empty, false, builder.Build());
            }

            if (_textInfo.ToTitleCase(map) == "Kings Canyon")
            {
                var index = _rand.Next(_locationsKingsCanyon.Length);
                var builder = new EmbedBuilder()
                {
                    Color = new Color(0, 114, 0),
                    Title = _locationsKingsCanyon[index],
                    Description = " \u2139 Click on the link above for additional intel. ",
                    Url = "https://www.metabomb.net/off-meta/gameplay-guides/apex-legends-map-guide"
                };
                await ReplyAsync(string.Empty, false, builder.Build());
            }

            if (_textInfo.ToTitleCase(map) == "World's Edge")
            {
                var index = _rand.Next(_locationsWorldsEdge.Length);
                var builder = new EmbedBuilder()
                {
                    Color = new Color(0, 0, 114),
                    Title = _locationsWorldsEdge[index],
                    Description = " \u2139 Click on the link above for additional intel. ",
                    Url = "https://www.dexerto.com/apex-legends/best-worlds-edge-landing-spots-apex-legends-1511158/"
                };
                await ReplyAsync(string.Empty, false, builder.Build());
            }
        }

        [Command("apexdropvote"), Alias("apexvote")]

        [Summary("Random Apex Drop Location Vote")]
        public async Task ApexDropVote([Remainder] string map)
        {

            if (_textInfo.ToTitleCase(map) == "Olympus")
            {
                await SendVote(_locationsOlympus, "Olympus");
            }

            if (_textInfo.ToTitleCase(map) == "Kings Canyon")
            {
                await SendVote(_locationsKingsCanyon, "Kings Canyon");
            }

            if (_textInfo.ToTitleCase(map) == "World's Edge")
            {
                await SendVote(_locationsWorldsEdge, "World's Edge");
            }
        }

        private async Task SendVote(string[] map, string mapString)
        {
            var index1 = _rand.Next(map.Length);
            var index2 = _rand.Next(map.Length);
            while (index1 == index2)
            {
                index2 = _rand.Next(map.Length);
            }

            await SendOption(map[index1], 1,
                _locationIntelDict[mapString]);
            await SendOption(map[index2], 2,
                _locationIntelDict[mapString]);

            var redCircle = new Emoji("🔴");
            var blueCircle = new Emoji("🔵");

            const string message = "Vote for your preferred drop by clicking on the corresponding emoji";
            var sent = await Context.Channel.SendMessageAsync(message);

            await sent.AddReactionAsync(redCircle);
            await sent.AddReactionAsync(blueCircle);
        }

        private async Task SendOption(string location, int option, string url)
        {
            var builder = new EmbedBuilder();
            switch (option)
            {
                case 1:
                    builder.Color = new Color(114, 0, 0);
                    builder.Title = location;
                    builder.Description = "\uD83D\uDD34 Click on the link above for additional intel";
                    builder.Url = url;
                    break;
                case 2:
                    builder.Color = new Color(0, 0, 114);
                    builder.Title = location;
                    builder.Description = "\uD83D\uDD35 Click on the link above for additional intel";
                    builder.Url = url;
                    break;
            }

            await ReplyAsync(string.Empty, isTTS: false, builder.Build());
        }
    }
}