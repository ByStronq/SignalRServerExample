namespace SignalRServerExample.Models
{
    public class Group
    {
        public string Name { get; set; }

        public List<Client> Clients { get; } = new();
    }
}
