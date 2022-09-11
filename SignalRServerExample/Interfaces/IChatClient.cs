using SignalRServerExample.Models;

namespace SignalRServerExample.Interfaces
{
    public interface IChatClient
    {
        Task ClientJoined(string nickname);

        Task Clients(IEnumerable<Client> clients);

        Task ReceiveMessage(string message, string nickname);

        Task Groups(IEnumerable<Group> groups);
    }
}
