using EasonEetwViewer.Telegram.Dtos.TelegramBase;

namespace EasonEetwViewer.Telegram.Abstractions;
public interface ITelegramRetriever
{
    public Task<T> GetJsonTelegramAsync<T>(string id) where T : Head;
}
