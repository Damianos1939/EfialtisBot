using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace EfialtisBot.Modules
{
    public class Misc : ModuleBase<SocketCommandContext>
    {
        [Command("echo")]
        public async Task Echo([Remainder]string message)
        {
            var embed = new EmbedBuilder();
            embed.WithTitle("Echoed Message");
            embed.WithDescription(message);
            embed.WithColor(new Color(0, 255, 0));

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("pick")]
        public async Task Pick([Remainder]string message)
        {
            string[] options = message.Split(new char[] { '|' } , StringSplitOptions.RemoveEmptyEntries);

            Random r = new Random();

            string selection = options[r.Next(0, options.Length)];

            var embed = new EmbedBuilder();
            embed.WithTitle("Choice for " + Context.User.Username);
            embed.WithDescription(selection);
            embed.WithColor(new Color(255, 255, 0));
            embed.WithThumbnailUrl("https://dotesports-media.nyc3.cdn.digitaloceanspaces.com/wp-content/uploads/2019/05/03130505/Malphite_Splash_16-770x454.jpg");

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("secret")]
        public async Task Secret([Remainder] string arg ="")
        {
            if (!IsUserWaiter((SocketGuildUser)Context.User)) 
            {
                await Context.Channel.SendMessageAsync(":x:" +Context.User.Mention + ", you don't have the appropriate permission.");
                return; 
            }
            var dmChannel = await Context.User.GetOrCreateDMChannelAsync();
            await dmChannel.SendMessageAsync(Utilities.GetAlert("SECRET"));
        }
        private bool IsUserWaiter(SocketGuildUser user)
        {
            var result = from r in user.Guild.Roles
                         where r.Name == "Waiter"
                         select r.Id;

            ulong roleID = result.FirstOrDefault();
            if (roleID == 0) return false;
            var targetRole = user.Guild.GetRole(roleID);
            return user.Roles.Contains(targetRole);
        }
    }
}
