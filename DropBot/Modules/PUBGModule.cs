using Discord;
using Discord.Commands;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace DropBot.Modules
{
    [Name("PUBG")]
    public class PUBGModule : ModuleBase<SocketCommandContext>
    {
        [Command("pubgDrop"), Alias("pubg", "pubgdrop")]
        [Summary("Random PUBG Drop Location Picker")]
        public async Task PUBGDrop()
        {
            var menuBuilder = new SelectMenuBuilder()
                .WithPlaceholder("Select the map.")
                .WithCustomId("pubg")
                .WithMinValues(1)
                .WithMaxValues(1)
                .AddOption("Erangle", "Erangle")
                .AddOption("Sanhok", "Sanhok")
                .AddOption("Vikendi", "Vikendi")
                .AddOption("Miramar", "Miramar");

            var builder = new ComponentBuilder()
                .WithSelectMenu(menuBuilder);

            await ReplyAsync("Select which PUBG map.", components: builder.Build());
        }
    }
}