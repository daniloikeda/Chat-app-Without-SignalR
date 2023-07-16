using System.Net.WebSockets;

namespace Chat.Server.WebSocketsHandler
{
    public interface IMessageHandler
    {
        Task StartChat(WebSocket webSocket);
    }
}
