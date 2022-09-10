using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace SignalRServerExample.Hubs
{
    [Route("/messagehub")]
    public class MessageHub : Hub
    {
        public override async Task OnConnectedAsync() => await Clients.Caller.SendAsync("getConnectionId", Context.ConnectionId);

        public async Task AddGroup(string connectionId, string groupName) => await Groups.AddToGroupAsync(connectionId, groupName);

        // public async Task SendMessageAsync(string message)
        // public async Task SendMessageAsync(string message, IEnumerable<string> connectionIds)
        public async Task SendMessageAsync(string message, string groupName)
        // public async Task SendMessageAsync(string message, string groupName, IEnumerable<string> connectionIds)
        // public async Task SendMessageAsync(string message, IEnumerable<string> groupNames)
        {
            #region Caller

                // It only communicates with the client that sends a notification to the server.

                // await Clients.Caller.SendAsync("receiveMessage", message);

            #endregion

            #region All

                // It communicates with all clients connected to the server.

                // await Clients.All.SendAsync("receiveMessage", message);

            #endregion

            #region Others

                // It only communicates with all clients except the client that sends notification to the server.

                // await Clients.Others.SendAsync("receiveMessage", message);

            #endregion

            #region Hub Client Methods

                #region AllExcept

                    // It notifies all clients connected to the server except the specified clients.

                    // await Clients.AllExcept(connectionIds).SendAsync("receiveMessage", message);

                #endregion

                #region Client
                    
                    // It communicates only with the client with the specified connection id.

                    // await Clients.Client(connectionIds.First()).SendAsync("receiveMessage", message);

                #endregion

                #region Clients

                    // It communicates only with clients with specified connection ids.

                    // await Clients.Clients(connectionIds).SendAsync("receiveMessage", message);

                #endregion

                #region Group

                    // Notifies all clients in the specified group.
                    // Groups must be created first and then clients must subscribe to the groups.

                    // await Clients.Group(groupName).SendAsync("receiveMessage", message);

                #endregion

                #region GroupExcept

                    // It is a function that allows us to forward messages to all clients in the specified group, except the specified clients.

                    // await Clients.GroupExcept(groupName, connectionIds).SendAsync("receiveMessage", message);

                #endregion

                #region Groups

                    // It is a function that allows us to notify clients in more than one group.

                    // await Clients.Groups(groupNames).SendAsync("receiveMessage", message);

                #endregion

                #region OthersInGroup

                    // It is the function that notifies all clients in the group, except the notifying client.

                    await Clients.OthersInGroup(groupName).SendAsync("receiveMessage", message);

                #endregion

                #region User

                    // It is the function that allows to notify a user that has been authenticated.

                    // await Clients.User(userId: "").SendAsync("receiveMessage", message);

                    // Work on Authentication will be done in other commits!

                #endregion

                #region Users

                    // It is the function that allows to notify more than one user that has been authenticated.

                    // await Clients.Users(userIds: new[] { "" }).SendAsync("receiveMessage", message);

                    // Work on Authentication will be done in other commits!

                #endregion

            #endregion
        }
    }
}
