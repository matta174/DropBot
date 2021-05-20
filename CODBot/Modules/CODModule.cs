using System.Threading.Tasks;
using Discord;
using Discord.Commands;

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
            

            await ReplyAsync(text);
        }
    }
}