using System.Net.WebSockets;

namespace Chat.Server.Handlers
{
    public interface IConnectionManager
    {
        string? AddConnection(WebSocket webSocket);

        bool RemoveConnection(string guid);

        IEnumerable<WebSocket> GetAll();

        WebSocket? GetConnectionById(string guid);

        string GetConnectionId(WebSocket webSocket);
    }
}
