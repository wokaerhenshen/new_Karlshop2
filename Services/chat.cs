using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace new_Karlshop.Services
{
    public class Chat : Hub
    {
        public async Task Send(string chatName, string message)
        {
            await Clients.All.InvokeAsync("respond", chatName, message);
        }

    }
}
