using EasonEetwViewer.HttpRequest.Dto.JsonTelegram;

namespace EasonEetwViewer.HttpRequest;
public interface ITelegramRetriever
{
    public Task<T> GetTelegramJsonAsync<T>(string id) where T : JsonSchemaHead;
}
