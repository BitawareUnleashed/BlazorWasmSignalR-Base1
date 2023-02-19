using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace BlazorWasmSignalRBase1.Client.Pages;

public partial class Index
{
    private HubConnection? hubConnection;
    private string Watch = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        hubConnection = new HubConnectionBuilder()
            .WithUrl(new Uri("https://localhost:7012/communicationhub"))
        .Build();

        hubConnection.On<string>("SendMessage", message =>
        {
            Watch = message ?? string.Empty;
            StateHasChanged();
        });
        await hubConnection.StartAsync();


        //notificationService = new NotificationService(NavigationManager, Http);
        //_ = notificationService.InitializeNotifications();
    }
}

