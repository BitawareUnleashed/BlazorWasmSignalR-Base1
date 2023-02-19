using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWasmSignalRBase1.Server.Workers;

public class WatchWorker
{
    private HubConnection? hubConnection;

    public async Task SetHub(string baseAddress)
    {
        hubConnection = new HubConnectionBuilder()
            .WithUrl(new Uri(baseAddress))
            .Build();
        await hubConnection.StartAsync();

    }

    public void ExecuteAsync(CancellationToken stoppingToken)
    {

        _ = Task.Run(async () =>
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);

                _ = hubConnection?.SendAsync("SendMessage",
                    DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + " - " +
                    DateTime.Now.Second.ToString("00"));

                //hubConnection?.SendAsync(nameof(IHub.SendMessage), new NotificationTransport()
                //{
                //    SendMessage = DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + " - " + DateTime.Now.Second.ToString("00"),
                //    MessageType = "TIME"
                //});
            }
        });

    }
}

