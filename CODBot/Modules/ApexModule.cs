using System;
using System.Globalization;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace CODBot.Modules
{
    [Name("Apex")]
    public class ApexModule : ModuleBase<SocketCommandContext>
    {
        readonly TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

        private readonly string[] _locationsKingsCanyon = new[]
        {
            "Slum Lakes","Artillery","Relay","The Pit", "Containment","Wetlands","Runoff","Bunker","The Cage","Swamps","Airbase","Market","Hydro Dam","Skull Town","Repulsor","Thunderdome","Water Treatment"
        };
        private readonly string[] _locationsWorldsEdge = new[]
        {
            "Skyhook","Survey Camp", "Refinery","The Epicenter","Drill Site","Fragment West","Fragment East","Overlook","Lava Fissure","The Train Yard", "Harvester", "The Geyser","Thermal Station","Sorting Factory","The Tree","The Dome","Lava City"
        };
        private readonly string[] _locationsOlympus = new[]
        {
            "Docks","Carrier","Power Grid","Rift","Oasis","Turbine","Energy Depot","Gardens","Estates","Hammond Labs","Grow Towers","Elysium","Hydroponics","Solar Array","Orbital Cannon","Bonsai Plaza"
        };
        
        [Command("apexdrop"), Alias("apexdrop", "apex")]

        [Summary("Random Apex Drop Location Picker")]
        public async Task ApexDrop()
        {
            Random rand = new Random();
            
            var index1 = rand.Next(_locationsOlympus.Length);
            var builder1 = new EmbedBuilder()
            {
                Color = new Color(114, 0, 0),
                Title ="Olympus: " + _locationsOlympus[index1],
                Description =  "\u2139 Click on the link above for additional intel. ",
                Url = "https://www.rockpapershotgun.com/apex-legends-olympus-map-guide-best-locations-to-land"
            };
            await ReplyAsync("",false,builder1.Build());

            var index2 = rand.Next(_locationsKingsCanyon.Length);
            var builder2 = new EmbedBuilder()
            {
                Color = new Color(0, 114, 0),
                Title ="Kings Canyon: " + _locationsKingsCanyon[index2],
                Description =  " \u2139 Click on the link above for additional intel. ",
                Url = "https://www.metabomb.net/off-meta/gameplay-guides/apex-legends-map-guide"
            };
            await ReplyAsync("",false,builder2.Build());

            var index3 = rand.Next(_locationsWorldsEdge.Length);
            var builder3 = new EmbedBuilder()
            {
                Color = new Color(0, 0, 114),
                Title = "World's Edge: " + _locationsWorldsEdge[index3],
                Description =  " \u2139 Click on the link above for additional intel. ",
                Url = "https://www.dexerto.com/apex-legends/best-worlds-edge-landing-spots-apex-legends-1511158/"
            };
            await ReplyAsync("",false,builder3.Build());
        }
        
        [Command("apexdrop"), Alias("apexdrop", "apex")]

        [Summary("Random Apex Drop Location Picker")]
        public async Task ApexDrop([Remainder]string map)
        {
            Random rand = new Random();


            if (textInfo.ToTitleCase(map) == "Olympus")
            {
                var index = rand.Next(_locationsOlympus.Length);
                var builder = new EmbedBuilder()
                {
                    Color = new Color(114, 0, 0),
                    Title = _locationsOlympus[index],
                    Description =  " \u2139 Click on the link above for additional intel. ",
                    Url = "https://www.rockpapershotgun.com/apex-legends-olympus-map-guide-best-locations-to-land"
                };
                await ReplyAsync("",false,builder.Build());
            }

            if (textInfo.ToTitleCase(map) == "Kings Canyon")
            {
                var index = rand.Next(_locationsKingsCanyon.Length);
                var builder = new EmbedBuilder()
                {
                    Color = new Color(0, 114, 0),
                    Title = _locationsKingsCanyon[index],
                    Description =  " \u2139 Click on the link above for additional intel. ",
                    Url = "https://www.metabomb.net/off-meta/gameplay-guides/apex-legends-map-guide"
                };
                await ReplyAsync("",false,builder.Build());
            }

            if (textInfo.ToTitleCase(map) == "World's Edge")
            {
                
                var index = rand.Next(_locationsWorldsEdge.Length);
                var builder = new EmbedBuilder()
                {
                    Color = new Color(0, 0, 114),
                    Title = _locationsWorldsEdge[index],
                    Description =  " \u2139 Click on the link above for additional intel. ",
                    Url = "https://www.dexerto.com/apex-legends/best-worlds-edge-landing-spots-apex-legends-1511158/"

                };
                await ReplyAsync("",false,builder.Build());
            }
            
        }

        [Command("apexdropvote"), Alias("apexvote")]

        [Summary("Random Apex Drop Location Vote")]
        public async Task ApexDropVote([Remainder] string map)
        {
            Random rand = new Random();
            
            if (textInfo.ToTitleCase(map) == "Olympus")
            {
                
                var index1 = rand.Next(_locationsOlympus.Length);
                var index2 = rand.Next(_locationsOlympus.Length);
                while (index1 == index2)
                {
                    index2 = rand.Next(_locationsOlympus.Length);
                }

                await SendOption(_locationsOlympus[index1], 1,
                    "https://www.rockpapershotgun.com/apex-legends-olympus-map-guide-best-locations-to-land");
                await SendOption(_locationsOlympus[index2], 2,
                    "https://www.rockpapershotgun.com/apex-legends-olympus-map-guide-best-locations-to-land");
                
                var redCircle = new Emoji("🔴");
                var blueCircle = new Emoji("🔵");
            
                const string message = "Vote for your preferred drop by clicking on the corresponding emoji";
                var sent = await Context.Channel.SendMessageAsync(message);
            
                await sent.AddReactionAsync(redCircle);
                await sent.AddReactionAsync(blueCircle);
                return;
                
            }

            if (textInfo.ToTitleCase(map) == "Kings Canyon")
            {
                var index1 = rand.Next(_locationsKingsCanyon.Length);
                var index2 = rand.Next(_locationsKingsCanyon.Length);
                while (index1 == index2)
                {
                    index2 = rand.Next(_locationsKingsCanyon.Length);
                }
                await SendOption(_locationsKingsCanyon[index1], 1,
                    "https://www.metabomb.net/off-meta/gameplay-guides/apex-legends-map-guide");
                await SendOption(_locationsKingsCanyon[index2], 2,
                    "https://www.metabomb.net/off-meta/gameplay-guides/apex-legends-map-guide");
                
                var redCircle = new Emoji("🔴");
                var blueCircle = new Emoji("🔵");
            
                const string message = "Vote for your preferred drop by clicking on the corresponding emoji";
                var sent = await Context.Channel.SendMessageAsync(message);
            
                await sent.AddReactionAsync(redCircle);
                await sent.AddReactionAsync(blueCircle);
            }

            if (textInfo.ToTitleCase(map) == "World's Edge")
            {
                var index1 = rand.Next(_locationsWorldsEdge.Length);
                var index2 = rand.Next(_locationsWorldsEdge.Length);
                while (index1 == index2)
                {
                    index2 = rand.Next(_locationsWorldsEdge.Length);
                }
                await SendOption(_locationsWorldsEdge[index1], 1,
                    "https://www.dexerto.com/apex-legends/best-worlds-edge-landing-spots-apex-legends-1511158/");
                await SendOption(_locationsWorldsEdge[index2], 2,
                    "https://www.dexerto.com/apex-legends/best-worlds-edge-landing-spots-apex-legends-1511158/");
                
                var redCircle = new Emoji("🔴");
                var blueCircle = new Emoji("🔵");
            
                const string message = "Vote for your preferred drop by clicking on the corresponding emoji";
                var sent = await Context.Channel.SendMessageAsync(message);
            
                await sent.AddReactionAsync(redCircle);
                await sent.AddReactionAsync(blueCircle);
            }
            
        }

        private async Task SendOption(string location, int option, string url)
        {
            var builder = new EmbedBuilder();
            switch (option)
            {
                case 1:
                    builder.Color = new Color(114, 0, 0);
                    builder.Title = location;
                    builder.Description = 	"\uD83D\uDD34 Click on the link above for additional intel";
                    builder.Url = url;
                    break;
                case 2:
                    builder.Color = new Color(0, 0, 114);
                    builder.Title = location;
                    builder.Description = 	"\uD83D\uDD35 Click on the link above for additional intel"; 
                    builder.Url = url;
                    break;
            }

            await ReplyAsync("", isTTS:false,builder.Build());
        }

        
        
    }
}