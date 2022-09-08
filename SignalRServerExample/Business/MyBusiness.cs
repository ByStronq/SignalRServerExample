using Microsoft.AspNetCore.SignalR;
using SignalRServerExample.Hubs;

namespace SignalRServerExample.Business
{
    public class MyBusiness
    {
        private readonly IHubContext<MyHub> _myHubContext;

        public MyBusiness(
            IHubContext<MyHub> myHubContext
        ) {
            _myHubContext = myHubContext;
        }

        public async Task SendMessageAsync(string message)
        {
            await _myHubContext.Clients.All.SendAsync("receiveMessage", message);
        }
    }
}
