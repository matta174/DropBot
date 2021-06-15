using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;


namespace CODBot.Modules
{

    [Name("COD")]
    public class CODModule : ModuleBase<SocketCommandContext>
    {
        readonly TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
        private readonly Dictionary<string, string> _locationIntelDict = new Dictionary<string,string>
        {
            {"Summit","https://www.callofduty.com/warzone/strategyguide/tac-map-atlas/verdansk-north/zone-1a"}, 
            {"Military Base","https://www.callofduty.com/warzone/strategyguide/tac-map-atlas/verdansk-north/zone-1c"},
            {"Salt Mine","https://www.callofduty.com/warzone/strategyguide/tac-map-atlas/verdansk-north/zone-1d"},
            {"Array","https://www.callofduty.com/warzone/strategyguide/tac-map-atlas/verdansk-north/zone-1e"},
            {"TV Station","https://www.callofduty.com/warzone/strategyguide/tac-map-atlas/verdansk-south-and-central/zone-4a"},
            {"Airport","https://www.callofduty.com/warzone/strategyguide/tac-map-atlas/verdansk-west/zone-2a"},
            {"Storage Town","https://www.callofduty.com/warzone/strategyguide/tac-map-atlas/verdansk-west/zone-2d"},
            {"Superstore","https://www.callofduty.com/warzone/strategyguide/tac-map-atlas/verdansk-west/zone-2e"},
            {"Factory","https://www.callofduty.com/warzone/strategyguide/tac-map-atlas/verdansk-west/zone-2b"},
            {"Stadium","https://www.callofduty.com/warzone/strategyguide/tac-map-atlas/verdansk-south-and-central/zone-4c"},
            {"Lumber","https://www.callofduty.com/warzone/strategyguide/tac-map-atlas/verdansk-east/zone-5b"},
            {"Boneyard","https://www.callofduty.com/warzone/strategyguide/tac-map-atlas/verdansk-west/zone-2f"},
            {"Train Station","https://www.callofduty.com/warzone/strategyguide/tac-map-atlas/verdansk-southwest/zone-3a"},
            {"Hospital","https://www.callofduty.com/warzone/strategyguide/tac-map-atlas/verdansk-southwest/zone-3b"},
            {"Downtown","https://www.callofduty.com/warzone/strategyguide/tac-map-atlas/verdansk-south-and-central/zone-4d"},
            {"Farmland","https://www.callofduty.com/warzone/strategyguide/tac-map-atlas/verdansk-east/zone-5c"},
            {"Promenade West","https://www.callofduty.com/warzone/strategyguide/tac-map-atlas/verdansk-southwest/zone-3c"},
            {"Promenade East","https://www.callofduty.com/warzone/strategyguide/tac-map-atlas/verdansk-southwest/zone-3d"},
            {"Hills","https://www.callofduty.com/warzone/strategyguide/tac-map-atlas/verdansk-southwest/zone-3e"},
            {"Park","https://www.callofduty.com/warzone/strategyguide/tac-map-atlas/verdansk-south-and-central/zone-4e"},
            {"Port","https://www.callofduty.com/warzone/strategyguide/tac-map-atlas/verdansk-east/zone-5d"},
            {"Prison","https://www.callofduty.com/warzone/strategyguide/tac-map-atlas/verdansk-east/zone-5e"}
        };

        private readonly string[] _locations = new[]
        {
            "Summit", "Military Base", "Salt Mine", "Array", "TV Station", "Airport", "Storage Town", "Superstore",
            "Factory","Stadium", "Lumber", "Boneyard", "Train Station", "Hospital","Downtown","Farmland","Promenade West", "Promenade East",
            "Hills","Park","Port","Prison"
        };
        

        [Command("wherewedropping"), Alias("warzonedrop", "wherewebloppin", "wherewedroppin", "whereweblappin","whereweblapping","drop","warzone")]

        [Summary("Random Warzone Drop Location Picker")]
        public async Task WhereWeDropping()
        {
            Random rand = new Random();
            var index = rand.Next(_locations.Length);
            
            
            var builder = new EmbedBuilder
            {
                Color = new Color(252, 186, 3),
                Title = _locations[index],
                Url = _locationIntelDict[_locations[index]],
                Description =  " \u2139 Click the link above for intel about " + _locations[index]
            };
            
            await ReplyAsync("",false,builder.Build());
        }

        [Command("dropoptions"), Alias("options", "dropvotes")]

        [Summary("Warzone Drop Options")]
        public async Task DropOptions()
        {

            var rand = new Random();  
            var locations = new[]
            {
                "Summit", "Military Base", "Salt Mine", "Array", "TV Station", "Airport", "Storage Town", "Superstore",
                "Factory","Stadium", "Lumber", "Boneyard", "Train Station", "Hospital","Downtown","Farmland","Promenade West", "Promenade East",
                "Hills","Park","Port","Prison"
            };
            var index = rand.Next(locations.Length);
            var index2 = rand.Next(locations.Length);
            while (index == index2)
            {
                index2 = rand.Next(locations.Length);
            }

            await SendOption(locations[index],1);
            await SendOption(locations[index2],2);
            

            var redCircle = new Emoji("🔴");
            var blueCircle = new Emoji("🔵");
            
            const string message = "Vote for your preferred drop by clicking on the corresponding emoji";
            var sent = await Context.Channel.SendMessageAsync(message);
            
            await sent.AddReactionAsync(redCircle);
            await sent.AddReactionAsync(blueCircle);
            
            
            await ReplyAsync("");
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
            await ReplyAsync( "",false,builder.Build());
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

            await ReplyAsync("", isTTS:false,builder.Build());
        }
        

    }
    
    
}