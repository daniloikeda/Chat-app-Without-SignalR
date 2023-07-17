using System.Net.WebSockets;

namespace Chat.Server.Handlers
{
    public interface IChatHandler
    {
        Task StartChat(WebSocket webSocket);
    }
}
