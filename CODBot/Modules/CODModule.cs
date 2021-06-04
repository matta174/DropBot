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

        
        [Command("missleinbound"), Alias("missle","incoming")]
        [Summary("Missle Inbound, get down!")]
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
        

    }
    
    
}