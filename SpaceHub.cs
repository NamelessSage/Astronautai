using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Astronautai.Hubs
{
    class SpaceHub : Hub
    {
        public SpaceHub()
        {

        }

        public async Task SendInput(int id, string x, string y, string input)
        {
            await Clients.Others.SendAsync("ReceiveInput", id, x, y, input);
        }
    }
}
