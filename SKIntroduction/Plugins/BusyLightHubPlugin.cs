using Microsoft.SemanticKernel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SKIntroduction;

public class BusyLightHubPlugin
{
    private BusyLightHubClient _client;
    private string _destinationUser;
    private string _currentUser;
    public BusyLightHubPlugin(string currentUser, string destinationUser)
    {
        // we will sent the commands to the BusyLightHub with a destination user.
        // we need both users, current and destination to capture the commands and see if they are for us.
        _destinationUser = destinationUser;
        _currentUser = currentUser;

        _client = new BusyLightHubClient(_currentUser);
    }

    [KernelFunction, Description("Send Wrong")]
    public async Task Wrong()
    {
        await _client.SendActionAsync(_destinationUser, "Wrong");
    }

    [KernelFunction, Description("Send Correct")]
    public async Task Correct()
    {
        await _client.SendActionAsync(_destinationUser, "Wrong");
    }

    [KernelFunction, Description("Send Banana Flash")]
    public async Task BananaAttention()
    {
        await _client.SendActionAsync(_destinationUser, "Attention");
    }
}
