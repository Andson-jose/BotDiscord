using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorialBot.Module.Comandos
{
    public class PingPong : ModuleBase<SocketCommandContext>
    {
        [Command("ping")]

        public async Task MessageBot()
        {
            await ReplyAsync("pong");
        }
        
    }
}
