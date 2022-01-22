using Discord;
using Discord.Commands;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DropBot.Modules
{

    [Name("Accessories")]
    public class AccessoriesModule : ModuleBase<SocketCommandContext>
    {

        [Command("savage"), Alias("jason")]
        [Summary("Did somebody break your heart?")]
        public async Task Savage()
        {
            await ReplyAsync("https://youtu.be/sQR2-Q-k_9Y?t=52");
        }

        [Command("callit"), Alias("call", "whoiscallingit", "callit")]
        [Summary("Who calls where we drop")]
        public async Task CallIt()
        {
            var voiceChannels = this.Context.Guild.VoiceChannels;

            var allUsersList = voiceChannels.SelectMany(channel => channel.Users).ToList();
            var rand = new Random();
            var sb = new StringBuilder();
            var index = rand.Next(allUsersList.Count);

            sb.Append(allUsersList[index].Mention + " calls where we drop.");

            await ReplyAsync(sb.ToString());
        }

        [Command("win"), Alias("ez")]
        [Summary("Congratulate yourselves on a win")]
        public async Task Win()
        {
            var voiceChannels = this.Context.Guild.VoiceChannels;

            var allUsersList = voiceChannels.SelectMany(channel => channel.Users).ToList();
            StringBuilder sb = new StringBuilder("Congratulations on the win ");
            allUsersList.ForEach(item => sb.Append(item.Username + ", "));
            sb.Length -= 2;
            await ReplyAsync(sb.ToString());
        }

        [Command("missileinbound"), Alias("missile", "incoming", "inbound")]
        [Summary("Missile Inbound, get down!")]
        public async Task MissileInbound()
        {
            var gifs = new[]
            {
                "https://media.giphy.com/media/5YnhzjBAOTd6B7KhiF/giphy.gif",
                "https://media.giphy.com/media/iYfxT7U2QKR4A/giphy.gif",
                "https://media.giphy.com/media/TkBIQWNNGSqTS/giphy.gif",
                "https://media.giphy.com/media/115ai8kxEG4qo8/giphy.gif",
                "https://media.giphy.com/media/l4Ep9KQRRXtyjkIWQ/giphy.gif"
            };
            var rand = new Random();
            var index = rand.Next(gifs.Length);
            var builder = new EmbedBuilder()
            {
                Color = new Color(114, 0, 0),
                Title = "ALERT ALERT ALERT",
                Description = "MISSILE INBOUND",
                ImageUrl = gifs[index]
            };

            await ReplyAsync(string.Empty, isTTS: false, builder.Build());
        }

        [Command("summon"), Alias("assemble", "rallyup", "rally")]
        [Summary("Assemble the team")]
        [RequireUserPermission(GuildPermission.MentionEveryone)]
        public async Task Summon()
        {
            await ReplyAsync("@here let's play.");
        }

        [Command("sheesh"), Alias("bussin")]
        [Summary("Is it bussin'?")]
        public async Task Sheesh()
        {
            await ReplyAsync("https://www.youtube.com/watch?v=YgT6XABqS5Y");
        }
    }
}
