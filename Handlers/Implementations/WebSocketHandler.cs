using System.Text;
using System.Net.WebSockets;

namespace Chat.Server.Handlers.Implementations
{
    public class WebSocketHandler : IWebSocketHandler
    {
        private readonly IConnectionManager _connectionManager;

        public WebSocketHandler(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public void OnConnected(WebSocket webSocket)
        {
            _connectionManager.AddConnection(webSocket);
        }

        public async Task<bool> CloseConnectionAsync(string connectionId)
        {
            var webSocket = _connectionManager.GetConnectionById(connectionId);
            if (webSocket == null)
            {
                return false;
            }

            await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Chat disconnected", CancellationToken.None);
            return _connectionManager.RemoveConnection(connectionId);
        }

        public async Task SendMessage(WebSocket webSocket, string message)
        {
            var messageAsByte = Encoding.UTF8.GetBytes(message);
            await webSocket.SendAsync(new ArraySegment<byte>(messageAsByte, 0, message.Count()), WebSocketMessageType.Text,
                WebSocketMessageFlags.EndOfMessage, CancellationToken.None);
        }

        public async Task SendMessageToAll(string message)
        {
            var connections = _connectionManager.GetAll();
            foreach (var connection in connections)
            {
                var messageAsByte = Encoding.UTF8.GetBytes(message);
                await connection.SendAsync(new ArraySegment<byte>(messageAsByte, 0, message.Length), WebSocketMessageType.Text,
                    WebSocketMessageFlags.EndOfMessage, CancellationToken.None);
            }
        }

        public async Task<string> ReceiveMessage(WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            var receiveResult = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            return Encoding.UTF8.GetString(buffer, 0, receiveResult.Count);
        }
    }
}
