using Microsoft.AspNetCore.SignalR;

namespace BusyLightServer;

public class BusyLightHub : Hub
{
    public async Task SendBusyLightAction(string user, string action)
    {
        await Clients.All.SendAsync("BusyLightAction", user, action);
    }
}
