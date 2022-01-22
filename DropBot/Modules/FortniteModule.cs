using Discord;
using Discord.Commands;
using System;
using System.Threading.Tasks;

namespace DropBot.Modules
{
    [Name("Fortnite")]
    public class FortniteModule : ModuleBase<SocketCommandContext>
    {
        private static string tempURL = "https://www.epicgames.com/fortnite/en-US/news/whats-new-in-fortnite-battle-royale-chapter-3-season-1-flipped";

        private readonly string[] _locations = {
            "LogJam Lumberyard", "Sleepy Sound", "Shifty Shafts", "The Daily Bugle", "Coney Crossroads", "Camp Cuddle", "Sanctuary", "Greasy Grove", "Rocky Reels", "The Joneses", "Condo Canyon", "Chonkers Speedway"
        };

        [Command("FortniteDrop"), Alias("fn", "fortnite", "fortnitedrop", "fndrop")]
        [Summary("Random Fortnite Chapter 3 Drop Location Picker")]
        public async Task FortniteDrop()
        {
            var rand = new Random();
            var index = rand.Next(_locations.Length);

            var builder = new EmbedBuilder
            {
                Color = new Color(252, 186, 3),
                Title = _locations[index],
                Url = tempURL,
                Description = " \u2139 Click the link above for intel about " + _locations[index]
            };

            await ReplyAsync(string.Empty, false, builder.Build());
        }
    }
}