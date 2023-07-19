using System.Net.WebSockets;

namespace Chat.Server.Handlers
{
    public interface IWebSocketHandler
    {
        void OnConnected(WebSocket webSocket);

        Task<bool> CloseConnectionAsync(string connectionId);

        Task<string> ReceiveMessage(WebSocket webSocket);

        Task SendMessage(WebSocket webSocket, string message);

        Task SendMessageToAll(string message);
    }
}
