using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;


namespace DropBot.Modules
{

    [Name("COD")]
    public class CODModule : ModuleBase<SocketCommandContext>
    {
        readonly TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
        private static string tempURL = "https://www.callofduty.com/content/atvi/callofduty/blog/web/en/home/2021/11/call-of-duty-vanguard-warzone-caldera-season-one-map-intel.html";

        // ToDo: Update the links once we have an interactive map on CoD's website. 

        private readonly Dictionary<string,string> _locationIntelDict = new Dictionary<string, string>
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

        [Command("warzonedrop"), Alias("warzone","warzonedrop","wzdrop","wz")]

        [Summary("Random Warzone Drop Location Picker")]
        public async Task WarzoneDrop()
        {
            var rand = new Random();
            var index = rand.Next(_locations.Length);
            
            var builder = new EmbedBuilder
            {
                Color = new Color(252, 186, 3),
                Title = _locations[index],
                Url = _locationIntelDict[_locations[index]],
                Description =  " \u2139 Click the link above for intel about " + _locations[index]
            };
            
            await ReplyAsync(string.Empty,false,builder.Build());
        }
        
        [Command("warzonevote"), Alias( "wzvote")]

        [Summary("Random Warzone Drop Location Vote")]
        public async Task WarzoneVote()
        {
            var rand = new Random();
            var index = rand.Next(_locations.Length);
            var index2 = rand.Next(_locations.Length);
            while (index == index2)
            {
                index2 = rand.Next(_locations.Length);
            }


            await SendOption(_locations[index],1);
            await SendOption(_locations[index2],2);

            
            var redCircle = new Emoji("🔴");
            var blueCircle = new Emoji("🔵");
            
            const string message = "Vote for your preferred drop by clicking on the corresponding emoji";
            var sent = await Context.Channel.SendMessageAsync(message);

            await sent.AddReactionAsync(redCircle);
            System.Threading.Thread.Sleep(1500); // We don't want to get rate limited every time we hit wzvote
            await sent.AddReactionAsync(blueCircle);

            return;
        }

        [Command("intel"), Alias("intelligence", "info")]
        [Summary("Gives intel on a location")]
        public async Task Intel([Remainder]string location)
        {

            location = textInfo.ToTitleCase(location);

            var builder = new EmbedBuilder
            {
                Color = new Color(252, 186, 3),
                Title = location,
                Url = _locationIntelDict[location],
                Description = "	\u2139 Click the link above for intel about " + location
            };
            await ReplyAsync( string.Empty,false,builder.Build());
        }
        
        private async Task SendOption(string location, int option)
        {
            var builder = new EmbedBuilder();
            switch (option)
            {
                case 1:
                    builder.Color = new Color(114, 0, 0);
                    builder.Title = location;
                    builder.Description = 	"\uD83D\uDD34 Click on the link above for additional intel";
                    builder.Url = _locationIntelDict[location];
                    break;
                case 2:
                    builder.Color = new Color(0, 0, 114);
                    builder.Title = location;
                    builder.Description = 	"\uD83D\uDD35 Click on the link above for additional intel"; 
                    builder.Url = _locationIntelDict[location];
                    break;
            }

            await ReplyAsync(string.Empty, isTTS:false,builder.Build());
        }
        

    }
    
    
}