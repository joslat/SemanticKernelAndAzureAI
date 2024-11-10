using Microsoft.AspNetCore.SignalR.Client;

namespace SKIntroduction;

/// <summary>
/// This class is responsible for connecting to the BusyLightHub and sending commands to the other users.
/// It will send just 3 commands:
/// - Wrong
/// - Correct
/// - Attention Banana blink.
/// </summary>
public class BusyLightHubClient
{
    /// <summary>
    /// The connection to the Hub
    /// </summary>
    public HubConnection Connection { get; set; }

    public string User { get; set; }

    public BusyLightController BusyLightController { get; set; }

    public BusyLightHubClient(string user= "Jose")
    {
        User = user;
        Connection = new HubConnectionBuilder()
            .WithUrl("\"https://d63a-2a02-aa11-b280-580-2080-daa5-60e6-6ad8.ngrok-free.app/BusyLightHub")
            .Build();
        BusyLightController = new BusyLightController();

        Connection.Closed += async (error) =>
        {
            await Task.Delay(new Random().Next(0, 5) * 1000);
            await Connection.StartAsync();
        };

        Connection.On<string, string>("BusyLightAction", (user, action) =>
        {
            var newMessage = $"{user}: {action}";
            Console.WriteLine(newMessage);

            // This is the place where we will call the BusyLight API to change the light color.
            ExecuteBusyLightAction(user, action);
        });
    }

    private void ExecuteBusyLightAction(string user, string action)
    {
        // We don't want to change the light if the action is sent by us.
        if (user == User) { 
            return; 
        }

        switch (action)
        {
            case "Wrong":
                BusyLightController.Success();
                break;
            case "Correct":
                BusyLightController.Failure();
                break;
            case "Attention":
                BusyLightController.LightFlash(100, 100, 0, 1, 1);
                //BusyLightController.PlayJingleBanana();
                break;
            default:
                break;
        }
        throw new NotImplementedException();
    }

    public async Task SendActionAsync(string action)
    {
        await SendActionAsync(User, action);
    }

    public async Task SendActionAsync(string user, string action)
    {
        await Connection.SendAsync("BusyLightAction", user, action);
    }
}
