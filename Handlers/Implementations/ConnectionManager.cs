﻿using System.Collections.Concurrent;
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

        public bool AddConnection(string name, WebSocket connection)
        {
            return _connections.TryAdd(name, connection);
        }

        public bool RemoveConnection(string name)
        {
            return _connections.TryRemove(name, out _);
        }

        public WebSocket GetById(string name)
        {
            return _connections[name];
        }

        public IEnumerable<WebSocket> GetClientsSocket(string? exceptClient = null)
        {
            return exceptClient == null ? _connections.Select(_ => _.Value) : _connections.Where(_ => _.Key != exceptClient).Select(_ => _.Value);
        }
    }
}