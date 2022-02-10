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
        private readonly Random _random = new Random();

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
            var user = this.Context.Guild.GetUser(this.Context.User.Id);
            var voiceChannel = user.VoiceChannel;

            if(voiceChannel == null)
            {
                await ReplyAsync("You must be in a voice channel to use this command.");
                return;
            }
            
            var usersList = voiceChannel.Users.ToList();
            var index = _random.Next(usersList.Count);

            var builder = new EmbedBuilder()
            .WithThumbnailUrl(usersList[index].GetAvatarUrl() ?? Context.User.GetDefaultAvatarUrl())
            .WithDescription($"{usersList[index].Mention} calls where we drop. ")
            .WithCurrentTimestamp()
            .WithColor(Color.Red);

            var embed = builder.Build();
            await ReplyAsync(null, false, embed);
        }

        [Command("win"), Alias("ez")]
        [Summary("Congratulate yourselves on a win")]
        public async Task Win()
        {
            var user = this.Context.Guild.GetUser(this.Context.User.Id);
            var voiceChannel = user.VoiceChannel;

            if(voiceChannel == null)
            {
                await ReplyAsync("You must be in a voice channel to use this command.");
                return;
            }
            var allUsersList = voiceChannel.Users.ToList();
            StringBuilder sb = new StringBuilder();
            allUsersList.ForEach(item => sb.Append($"{item.Mention}, "));
            sb.Length -= 2;

            var builder = new EmbedBuilder()
            {
                Color = new Color(255, 215, 0),
                Title = "Congratulations on the win!",
                Description = sb.ToString(),
            };
            await ReplyAsync(string.Empty, isTTS: false, builder.Build());
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
            var index = _random.Next(gifs.Length);
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
