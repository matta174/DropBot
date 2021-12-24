using System;
using System.Globalization;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace DropBot.Modules
{
    [Name("PUBG")]
    public class PUBGModule : ModuleBase<SocketCommandContext>
    {
        readonly TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
        private static string tempURL = "https://pubgmap.io/";
        readonly Random _rand = new Random();
        
        private readonly string[] _locationsErangle = new[]
        {
            "Zharki", "Severny","Stalber", "Kameshki","Georgopol","Rozhok","Yasnaya Polyana","Lipovka","Gatka","Pochinki","Mylta Power","Mylta","Promorsk","Novorepnoye","Sosnovka Military Base"
        };
        private readonly string[] _locationsMiramar = new[]
        {
           "Alcantara","La Cobreria", "Torre Ahumada","Campo Militar", "Cruz del Valle","Tierra Bronca","El Pozo", "San Martin","El Azahar","Monte Nuevo","Pecado","La Bendita", "Impala","Chumacera","Los Leones","Puerto Paraiso","Valle del Mar","Prison", "Los Higos"
        };
        private readonly string[] _locationsSanhok = new[]
        {
            "Ha Tinh","Tat Mok","Khao","Mongnai","Camp Alpha","Bootcamp","Paradise Resort","Camp Bravo","Ruins","Pai Nan","Quarry","Lakawi","Tambang","Kampong","Na Kham","Sahmee","Camp Charlie","Ban Tai","Docks"
        };
        private readonly string[] _locationsVikendi = new[]
        {
            "Port","Zabava","Cosmodrome","Trevno","Krichas","Coal Mine","Dobro Mesto","Goroka","Mount Kreznic","Podvosto","Peshkova","Villa","Cement Factory","Vihar","Movatra","Dino Park","Tovar","Castle","Sawmill","Abbey","Volnova","Cantra","Hot Springs","Milnar","Pilnec","Winery"
        };
        
        [Command("pubgDrop"), Alias("pubgdrop", "pubg")]
        [Summary("Random Apex Drop Location Picker")]
        public async Task ApexDrop()
        {
            var index1 = _rand.Next(_locationsErangle.Length);
            var builder1 = new EmbedBuilder()
            {
                Color = new Color(114, 0, 0),
                Title ="Erangle: " + _locationsErangle[index1],
                Description =  "\u2139 Click on the link above for additional intel. ",
                Url = tempURL
            };
            await ReplyAsync(string.Empty,false,builder1.Build());

            var index2 = _rand.Next(_locationsMiramar.Length);
            var builder2 = new EmbedBuilder()
            {
                Color = new Color(0, 114, 0),
                Title ="Miramar: " + _locationsMiramar[index2],
                Description =  " \u2139 Click on the link above for additional intel. ",
                Url = tempURL
            };
            await ReplyAsync(string.Empty,false,builder2.Build());

            var index3 = _rand.Next(_locationsSanhok.Length);
            var builder3 = new EmbedBuilder()
            {
                Color = new Color(0, 0, 114),
                Title = "Sanhok: " + _locationsSanhok[index3],
                Description =  " \u2139 Click on the link above for additional intel. ",
                Url = tempURL
            };
            await ReplyAsync(string.Empty,false,builder3.Build());
            
            var index4 = _rand.Next(_locationsVikendi.Length);
            var builder4 = new EmbedBuilder()
            {
                Color = new Color(245, 215, 66),
                Title = "Vikendi: " + _locationsVikendi[index4],
                Description =  " \u2139 Click on the link above for additional intel. ",
                Url = tempURL
            };
            await ReplyAsync(string.Empty,false,builder4.Build());
        }

        
    }
}