using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasonEetwViewer.HttpRequest.Dto.JsonTelegram;

namespace EasonEetwViewer.HttpRequest;
public interface ITelegramRetriever
{
    public Task<T> GetTelegramJsonAsync<T>(string id) where T : JsonSchemaHead;
}
