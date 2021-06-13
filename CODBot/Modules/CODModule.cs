using System;
using System.Collections.Generic;
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

        [Command("say"), Alias("s")]
        [Summary("Make the bot say something")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task Say([Remainder]string text)
        {
            await ReplyAsync(text +" " +  this.Context.User.Username);
        }

        
        [Command("missileinbound"), Alias("missile","incoming")]
        [Summary("Missile Inbound, get down!")]
        public async Task MissleInbound()
        {
            string[] gifs = new[]
            {
                "https://media.giphy.com/media/5YnhzjBAOTd6B7KhiF/giphy.gif",
                "https://media.giphy.com/media/iYfxT7U2QKR4A/giphy.gif",
                "https://media.giphy.com/media/TkBIQWNNGSqTS/giphy.gif",
                "https://media.giphy.com/media/115ai8kxEG4qo8/giphy.gif",
                "https://media.giphy.com/media/l4Ep9KQRRXtyjkIWQ/giphy.gif"
            };
            Random rand = new Random();
            int index = rand.Next(gifs.Length);
            var builder = new EmbedBuilder()
            {
                Color = new Color(114, 0, 0),
                Title = "ALERT ALERT ALERT",
                Description = "MISSLE INBOUND",
                ImageUrl = gifs[index]
            };

            await ReplyAsync("", isTTS:false,builder.Build());
        }

        [Command("win"), Alias("ez")]
        [Summary("Congratulate yourselves on a win")]
        public async Task Win()
        {
            var voiceChannels = this.Context.Guild.VoiceChannels;

            string endMessage = "";
            List<SocketGuildUser> allUsersList = new List<SocketGuildUser>();
            foreach (var channel in voiceChannels)
            {
                var users = channel.Users; // this.Context.Guild.Channels.FirstOrDefault(c => c.Id == myChannelId);
                foreach (var user in users)
                {
                    allUsersList.Add(user);
                }
            }
            StringBuilder sb = new StringBuilder("Congratulations on the win ");
            allUsersList.ForEach(item => sb.Append(item.Username + " ,"));
            sb.Length--;
            await ReplyAsync(sb.ToString());
        }
        
        [Command("callit"), Alias("call","whoiscallingit","callit")]
        [Summary("Who calls where we drop")]
        public async Task CallIt()
        {
            var voiceChannels = this.Context.Guild.VoiceChannels;

            string endMessage = "";
            List<SocketGuildUser> allUsersList = new List<SocketGuildUser>();
            foreach (var channel in voiceChannels)
            {
                var users = channel.Users; // this.Context.Guild.Channels.FirstOrDefault(c => c.Id == myChannelId);
                foreach (var user in users)
                {
                    allUsersList.Add(user);
                }
            }
            Random rand = new Random();  
            StringBuilder sb = new StringBuilder();
            int index = rand.Next(allUsersList.Count);
            
            sb.Append(allUsersList[index] + " calls where we drop.");

            await ReplyAsync(sb.ToString());
        }
        
        [Command("savage"), Alias("jason")]
        [Summary("Did somebody break your heart?")]
        public async Task Savage()
        {
            await ReplyAsync( "https://youtu.be/sQR2-Q-k_9Y?t=52");
        }
        

        [Command("wherewedropping"), Alias("drop", "wherewebloppin", "wherewedroppin", "whereweblappin","whereweblapping")]

        [Summary("Random Warzone Drop Location Picker")]
        public async Task WhereWeDropping()
        {
            Random rand = new Random();  
            string[] locations = new[]
            {
                "Summit", "Military Base", "Salt Mine", "Array", "TV Station", "Airport", "Storage Town", "Superstore",
                "Factory","Stadium", "Lumber", "Boneyard", "Train Station", "Hospital","Downtown","Farmland","Promenade West", "Promenade East",
                "Hills","Park","Port","Prison"
            };
            int index = rand.Next(locations.Length);
            await ReplyAsync("Enjoy your drop to: " + locations[index]);
        }
        
        
        [Command("sheesh"), Alias("sheesh", "bussin")]
        [Summary("Is it bussin'?")]
        public async Task Sheesh()
        {
            await ReplyAsync( "https://www.youtube.com/watch?v=YgT6XABqS5Y");
        }
        
        
        [Command("dropoptions"), Alias("options", "dropvotes")]

        [Summary("Warzone Drop Options")]
        public async Task DropOptions()
        {

            Random rand = new Random();  
            string[] locations = new[]
            {
                "Summit", "Military Base", "Salt Mine", "Array", "TV Station", "Airport", "Storage Town", "Superstore",
                "Factory","Stadium", "Lumber", "Boneyard", "Train Station", "Hospital","Downtown","Farmland","Promenade West", "Promenade East",
                "Hills","Park","Port","Prison"
            };
            int index = rand.Next(locations.Length);
            int index2 = rand.Next(locations.Length);
            while (index == index2)
            {
                index2 = rand.Next(locations.Length);
            }

            await SendOption(locations[index],1);
            await SendOption(locations[index2],2);
            

            var RedCircle = new Emoji("🔴");
            var BlueCircle = new Emoji("🔵");
            
            string message = "Vote for your preferred drop by clicking on the corresponding emoji";
            var sent = await Context.Channel.SendMessageAsync(message);
            await sent.AddReactionAsync(RedCircle);
            await sent.AddReactionAsync(BlueCircle);
            
            
            await ReplyAsync("");
        }



        public async Task SendOption(string location, int option)
        {
            Dictionary<string, string> openWith = new Dictionary<string, string>();
            openWith.Add("Summit","https://www.callofduty.com/warzone/strategyguide/tac-map-atlas/verdansk-north/zone-1a");    
            openWith.Add("Military Base","https://www.callofduty.com/warzone/strategyguide/tac-map-atlas/verdansk-north/zone-1c");
            openWith.Add("Salt Mine","https://www.callofduty.com/warzone/strategyguide/tac-map-atlas/verdansk-north/zone-1d");
            openWith.Add("Array","https://www.callofduty.com/warzone/strategyguide/tac-map-atlas/verdansk-north/zone-1e");
            openWith.Add("TV Station","https://www.callofduty.com/warzone/strategyguide/tac-map-atlas/verdansk-south-and-central/zone-4a");
            openWith.Add("Airport","https://www.callofduty.com/warzone/strategyguide/tac-map-atlas/verdansk-west/zone-2a");
            openWith.Add("Storage Town","https://www.callofduty.com/warzone/strategyguide/tac-map-atlas/verdansk-west/zone-2d");
            openWith.Add("Superstore","https://www.callofduty.com/warzone/strategyguide/tac-map-atlas/verdansk-west/zone-2e");
            openWith.Add("Factory","https://www.callofduty.com/warzone/strategyguide/tac-map-atlas/verdansk-west/zone-2b");
            openWith.Add("Stadium","https://www.callofduty.com/warzone/strategyguide/tac-map-atlas/verdansk-south-and-central/zone-4c");
            openWith.Add("Lumber","https://www.callofduty.com/warzone/strategyguide/tac-map-atlas/verdansk-east/zone-5b");
            openWith.Add("Boneyard","https://www.callofduty.com/warzone/strategyguide/tac-map-atlas/verdansk-west/zone-2f");
            openWith.Add("Train Station","https://www.callofduty.com/warzone/strategyguide/tac-map-atlas/verdansk-southwest/zone-3a");
            openWith.Add("Hospital","https://www.callofduty.com/warzone/strategyguide/tac-map-atlas/verdansk-southwest/zone-3b");
            openWith.Add("Downtown","https://www.callofduty.com/warzone/strategyguide/tac-map-atlas/verdansk-south-and-central/zone-4d");
            openWith.Add("Farmland","https://www.callofduty.com/warzone/strategyguide/tac-map-atlas/verdansk-east/zone-5c");
            openWith.Add("Promenade West","https://www.callofduty.com/warzone/strategyguide/tac-map-atlas/verdansk-southwest/zone-3c");
            openWith.Add("Promenade East","https://www.callofduty.com/warzone/strategyguide/tac-map-atlas/verdansk-southwest/zone-3d");
            openWith.Add("Hills","https://www.callofduty.com/warzone/strategyguide/tac-map-atlas/verdansk-southwest/zone-3e");
            openWith.Add("Park","https://www.callofduty.com/warzone/strategyguide/tac-map-atlas/verdansk-south-and-central/zone-4e");
            openWith.Add("Port","https://www.callofduty.com/warzone/strategyguide/tac-map-atlas/verdansk-east/zone-5d");
            openWith.Add("Prison","https://www.callofduty.com/warzone/strategyguide/tac-map-atlas/verdansk-east/zone-5e");
            
            var builder = new EmbedBuilder();
            if (option == 1)
            {

                builder.Color = new Color(114, 0, 0);
                builder.Title = location;
                builder.Description = 	"\uD83D\uDD34 Click on the link above for additional intel";
                builder.Url = openWith[location];
            }

            if (option == 2)
            {
                builder.Color = new Color(0, 0, 114);
                builder.Title = location;
                builder.Description = 	"\uD83D\uDD35 Click on the link above for additional intel"; 
                builder.Url = openWith[location];
            }
            await ReplyAsync("", isTTS:false,builder.Build());
        }
        

    }
    
    
}