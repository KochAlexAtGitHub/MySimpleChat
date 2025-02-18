using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MySimpleChat.Server;

public class CosmosDbService
{
    private readonly Container _container;

    public CosmosDbService(string endpointUri, string primaryKey, 
        string databaseId, string containerId)
    {
        var cosmosClient = new CosmosClient(endpointUri, primaryKey);
        _container = cosmosClient.GetContainer(databaseId, containerId);
    }

    public async Task<List<Message>> GetMessagesAsync(string chatId)
    {
        var query = new QueryDefinition("SELECT * FROM Message m WHERE m.chatId = @chatId ORDER BY m.timestamp ASC")
            .WithParameter("@chatId", chatId);
        List<Message> messages = new();
        using FeedIterator<Message> resultSet = _container.GetItemQueryIterator<Message>(query);

        while(resultSet.HasMoreResults)
        {
            FeedResponse<Message> response = await resultSet.ReadNextAsync();
            messages.AddRange(response);
        }
        return messages;
    }

    public async Task SendMessageAsync(Message message)
    {
        await _container.CreateItemAsync(message, new PartitionKey(message.ChatId));
    }
}
