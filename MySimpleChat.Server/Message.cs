using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text.Json.Serialization;

namespace MySimpleChat.Server
{
    public class Message
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string ChatId { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        
        public string Msg { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }

}
