using System.Net.WebSockets;

namespace Chat.Server.WebSocketsHandler
{
    public interface IWebSocketManager
    {
        bool AddConnection(string name, WebSocket connection);

        WebSocket GetById(string name);

        IEnumerable<WebSocket> GetClientsSocket(string? exceptClient = null);

        bool RemoveConnection(string name);
    }
}
