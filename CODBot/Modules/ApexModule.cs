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
            "Skyhook","Survey Camp", "Refinery","The Epicenter","Drill Site","Fragment West","Fragment East","Overlook","Lava Fissure","The Train Yard", "Mirage Voyage", "Harvester", "The Geyser","Thermal Station","Sorting Factory","The Tree","The Dome","Lava City"
        };
        private readonly string[] _locationsOlympus = new[]
        {
            "Docks","Carrier","Power Grid","Rift","Oasis","Turbine","Energy Depot","Gardens","Estates","Hammond Labs","Grow Towers","Elysium","Hydroponics","Solar Array","Orbital Cannon","Bonsai Plaza"
        };

        
        
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
        
    }
}