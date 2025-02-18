using Microsoft.AspNetCore.SignalR.Client;

namespace MySimpleChat.Client.Services;

public class ChatService
{
    private HubConnection _hubConnection;

    public event Action<string, string> OnMessageReceived;

    public ChatService()
    {
    }

    public async Task ConnectAsync()
    {
        _hubConnection = new HubConnectionBuilder().WithUrl("https://meinserver.com/chathub").Build();

        _hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
        {
            OnMessageReceived?.Invoke(user, message);
        });

        await _hubConnection.StartAsync();
    }

    public async Task SendMessage(string message)
    {
        if (_hubConnection.State == HubConnectionState.Connected)
        {
            await _hubConnection.SendAsync("SendMessage", "username", message);
        }
    }
}
