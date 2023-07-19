using System.Collections.Concurrent;
using System.Net.WebSockets;

namespace Chat.Server.Handlers.Implementations
{
    public class ConnectionManager : IConnectionManager
    {
        private readonly ConcurrentDictionary<string, WebSocket> _connections;

        public ConnectionManager()
        {
            _connections = new ConcurrentDictionary<string, WebSocket>();
        }
        
        public string? AddConnection(WebSocket webSocket)
        {
            if (IsConnectionRegistered(webSocket))
            {
                return GetConnectionId(webSocket);
            }

            var connectionId = CreateConnectionId();
            return !_connections.TryAdd(connectionId, webSocket) ? null : connectionId;
        }

        public bool RemoveConnection(string guid)
        {
            return _connections.TryRemove(guid, out _);
        }

        public IEnumerable<WebSocket> GetAll()
        {
            return _connections.Select(_ => _.Value);
        }

        public WebSocket? GetConnectionById(string guid)
        {
            return _connections.TryGetValue(guid, out var webSocket) ? webSocket : null;
        }

        public string GetConnectionId(WebSocket webSocket)
        {
            return _connections.FirstOrDefault(_ => _.Value == webSocket).Key;
        }

        private bool IsConnectionRegistered(WebSocket webSocket) => _connections.Any(_ => _.Value == webSocket);

        private static string CreateConnectionId() => Guid.NewGuid().ToString();
    }
}
