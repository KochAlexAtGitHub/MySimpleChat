using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MySimpleChat.Client;
using MudBlazor.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton<MySimpleChat.Client.Services.ChatService>();

// SignalR-Client registrieren
builder.Services.AddSingleton(sp =>
{
    //var navigation = sp.GetRequiredService<NavigationManager>();
    return new HubConnectionBuilder()
        .WithUrl($"https://localhost:5001/chathub") // SignalR-Hub-URL
        .WithAutomaticReconnect() // Automatische Wiederverbindung
        .Build();
});


builder.Services.AddMudServices();

await builder.Build().RunAsync();
