using Microsoft.AspNetCore.SignalR;

namespace Application.Services.Assistant.Hubs
{
    public class AiHub:Hub
    {
        public async Task GetConnectionId()
        {
            var connectionId = Context.ConnectionId;
            await Clients.Caller.SendAsync("ReceiveConnectionId", connectionId);
        }

    }
}
