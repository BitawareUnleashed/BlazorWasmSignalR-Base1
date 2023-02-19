using BlazorWasmSignalRBase1.Server.Models.Hubs;
using BlazorWasmSignalRBase1.Server.Workers;
using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddSignalR();

builder.Services.AddSingleton<WatchWorker>();
var app = builder.Build();

app.Services.GetRequiredService<WatchWorker>().ExecuteAsync("https://localhost:7012/communicationhub",new CancellationToken());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.MapHub<CommunicationHub>("/communicationhub");
app.Run();
