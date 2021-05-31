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


        [Command("win"), Alias("ez")]
        [Summary("Congratulate yourselves on a win")]
        public async Task Win()
        {
            ulong myChannelId = 288694246721191948;
            var voiceChannels = this.Context.Guild.VoiceChannels;
            //string users = "";
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
        
        [Command("savage"), Alias("jason")]
        [Summary("Did somebody break your heart?")]
        public async Task Savage()
        {
            await ReplyAsync( "https://youtu.be/sQR2-Q-k_9Y?t=52");
        }
        
        [Command("wherewedropping"), Alias("drop")]
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
        [Summary("Did somebody break your heart?")]
        public async Task Sheesh()
        {
            await ReplyAsync( "https://www.youtube.com/watch?v=YgT6XABqS5Y");
        }
        

    }
    
    
}