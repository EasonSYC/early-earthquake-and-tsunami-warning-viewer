using EasonEetwViewer.HttpRequest.Dto.JsonTelegram.TelegramBase;

namespace EasonEetwViewer.Dmdata.Caller.Interfaces;
public interface ITelegramRetriever
{
    public Task<T> GetTelegramJsonAsync<T>(string id) where T : Head;
}
