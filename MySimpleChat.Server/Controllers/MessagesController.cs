using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MySimpleChat.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly CosmosDbService _cosmosDbService;

        public MessagesController()
        {
            _cosmosDbService = new CosmosDbService(
                "https://localhost:8081",
                "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==",
                "MessengerDB", "Messages"
                );
        }

        [HttpGet("{chatId}")]
        public async Task<ActionResult<List<Message>>> GetMessages(string chatId)
        {
            return await _cosmosDbService.GetMessagesAsync(chatId);
        }

        [HttpPost]
        public async Task<ActionResult> SendMessage([FromBody] Message message)
        {
            await _cosmosDbService.SendMessageAsync(message);
            return Ok();
        }
    }
}
