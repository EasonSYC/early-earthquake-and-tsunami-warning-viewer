using EasonEetwViewer.Dtos.Dto.JsonTelegram.TelegramBase;

namespace EasonEetwViewer.Dtos.Caller.Interfaces;
public interface ITelegramRetriever
{
    public Task<T> GetTelegramJsonAsync<T>(string id) where T : Head;
}
