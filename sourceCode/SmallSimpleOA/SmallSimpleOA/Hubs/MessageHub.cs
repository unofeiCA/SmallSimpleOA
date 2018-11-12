using System;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace SmallSimpleOA.Hubs
{
    public class MessageHub : Hub
    {
        public MessageHub()
        {
        }

        public async Task SendMessage(string from, string to, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
