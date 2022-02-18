using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace DropBot.Modules
{
    [Name("Apex")]
    public class ApexModule : ModuleBase<SocketCommandContext>
    {

        [Command("apexdrop"), Alias("apexmap", "apex")]
        [Summary("Apex drop for a specific map")]
        public async Task ApexDrop()
        {
            var menuBuilder = new SelectMenuBuilder()
                .WithPlaceholder("Select the map.")
                .WithCustomId("apex")
                .WithMinValues(1)
                .WithMaxValues(1)
                .AddOption("Olympus", "Olympus")
                .AddOption("Kings Canyon", "Kings Canyon")
                .AddOption("World's Edge", "World's Edge");

            var builder = new ComponentBuilder()
                .WithSelectMenu(menuBuilder);

            await ReplyAsync("Select which Apex Legends map.", components: builder.Build());
        }
    }
}