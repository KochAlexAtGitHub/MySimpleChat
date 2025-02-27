using Microsoft.AspNetCore.SignalR;

namespace MySimpleChat.Server
{
    public class ChatHub: Hub
    {
        public ChatHub() { }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
