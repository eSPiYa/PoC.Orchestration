using Microsoft.AspNetCore.SignalR;

namespace PoC.Orchestration.Api.Hubs
{
    public class RealTimeHub: Hub
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
