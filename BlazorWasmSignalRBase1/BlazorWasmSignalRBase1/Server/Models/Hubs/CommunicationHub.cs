using Microsoft.AspNetCore.SignalR;

namespace BlazorWasmSignalRBase1.Server.Models.Hubs;
public class CommunicationHub : Hub
{
    public async Task SendMessage(string message) => await Clients.All.SendAsync("SendMessage",message);
}

