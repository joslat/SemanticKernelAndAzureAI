using Microsoft.AspNetCore.SignalR;

namespace BusyLightServer;

public class BusyLightHub : Hub
{
    public async Task SendAction(string user, string action)
    {
        await Clients.All.SendAsync("ReceiveAction", user, action);
    }
}
