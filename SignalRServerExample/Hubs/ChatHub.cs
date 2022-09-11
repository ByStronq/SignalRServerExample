using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRServerExample.Data;
using SignalRServerExample.Interfaces;
using SignalRServerExample.Models;

namespace SignalRServerExample.Hubs
{
    [Route("/chathub")]
    public class ChatHub : Hub<IChatClient>
    {
        public async Task GetNickName(string nickname)
        {
            ClientSource.Clients.Add(new Client
            {
                ConnectionId = Context.ConnectionId,
                Nickname = nickname
            });

            await Clients.Others.ClientJoined(nickname);

            await Clients.All.Clients(ClientSource.Clients);
        }

        public async Task SendMessage(string message, string? clientName)
        {
            await (clientName is null
                ? Clients.Others
                : Clients.Client(ClientSource.Clients.First(client => client.Nickname.Equals(clientName)).ConnectionId)
            ).ReceiveMessage(message, ClientSource.Clients.First(client => client.ConnectionId.Equals(Context.ConnectionId)).Nickname);
        }

        public async Task AddGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            var group = new Group
            {
                Name = groupName
            };

            group.Clients.Add(ClientSource.Clients.First(client => client.ConnectionId.Equals(Context.ConnectionId)));

            GroupSource.Groups.Add(group);

            await Clients.All.Groups(GroupSource.Groups);
        }

        public async Task JoinGroups(IEnumerable<string> groupNames)
        {
            var myClient = ClientSource.Clients.First(client => client.ConnectionId.Equals(Context.ConnectionId));

            foreach (var groupName in groupNames)
            {
                var group = GroupSource.Groups.First(group => group.Name.Equals(groupName));

                bool amIAlreadySubscribedToThisGroup = group.Clients.Any(client => client.Equals(myClient));

                if (!amIAlreadySubscribedToThisGroup)
                {
                    group.Clients.Add(myClient);

                    await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
                }
            }
        }

        public async Task GetClientsInGroup(string? groupName)
        {
            await Clients.Caller.Clients(
                groupName is not null
                    ? GroupSource.Groups.First(group =>
                        group.Name.Equals(groupName)
                    ).Clients : ClientSource.Clients
            );
        }

        public async Task SendMessageToGroup(string message, string groupName)
        {
            await Clients.Group(groupName).ReceiveMessage(message, ClientSource.Clients.First(client => client.ConnectionId.Equals(Context.ConnectionId)).Nickname);
        }
    }
}
