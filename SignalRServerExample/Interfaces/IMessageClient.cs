namespace SignalRServerExample.Interfaces
{
    public interface IMessageClient
    {
        Task Clients(IEnumerable<string> clients);

        Task UserJoined(string connectionId);

        Task UserLeaved(string connectionId);
    }
}
