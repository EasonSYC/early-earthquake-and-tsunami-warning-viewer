using EasonEetwViewer.Telegram.Dtos.TelegramBase;

namespace EasonEetwViewer.Telegram.Abstractions;
public interface ITelegramRetriever
{
    public Task<T> GetTelegramJsonAsync<T>(string id) where T : Head;
}
