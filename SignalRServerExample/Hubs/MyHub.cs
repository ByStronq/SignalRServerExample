using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRServerExample.Interfaces;

namespace SignalRServerExample.Hubs
{
    [Route("/myhub")]
    public class MyHub : Hub<IMessageClient>
    {
        private static readonly List<string> clients = new List<string>();

        //public async Task SendMessageAsync(string message)
        //{
        //    await Clients.All.SendAsync("receiveMessage", message);
        //}

        public override async Task OnConnectedAsync()
        {
            clients.Add(Context.ConnectionId);
            //await Clients.All.SendAsync("clients", clients);
            //await Clients.Others.SendAsync("userJoined", Context.ConnectionId);
            await Clients.All.Clients(clients);
            await Clients.Others.UserJoined(Context.ConnectionId);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            clients.Remove(Context.ConnectionId);
            //await Clients.All.SendAsync("clients", clients);
            //await Clients.Others.SendAsync("userLeaved", Context.ConnectionId);
            await Clients.All.Clients(clients);
            await Clients.Others.UserLeaved(Context.ConnectionId);
        }
    }
}
