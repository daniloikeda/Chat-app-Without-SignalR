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
                await connection.SendAsync(new ArraySegment<byte>(messageAsByte, 0, message.Count()), WebSocketMessageType.Text,
                    WebSocketMessageFlags.EndOfMessage, CancellationToken.None);
            }
        }

        public async Task<string> ReceiveMessage(WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            var receiveResult = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            return Encoding.UTF8.GetString(buffer, 0, receiveResult.Count);
        }

        // #############################

        //public async Task StartChat(WebSocket webSocket)
        //{
        //    if (!RegisterConnection(webSocket))
        //    {
        //        await webSocket.CloseAsync(WebSocketCloseStatus.InternalServerError,
        //            "Não foi possível registrar a sua conexão", CancellationToken.None);
        //        return;
        //    }

        //    var receivedResult = await RegisterClient(webSocket);

        //    _socketManager.RemoveConnection(Guid);
        //}

        //private bool RegisterConnection(WebSocket webSocket)
        //{
        //    Guid = _socketManager.AddConnection(webSocket);
        //    return Guid != null;
        //}

        //private async Task<WebSocketReceiveResult> RegisterClient(WebSocket webSocket)
        //{
        //    var clientNameBuffer = new byte[100];

        //    await SendMessage(webSocket, "Qual o seu nome ?");

        //    var receivedResult = await ReceiveMessage(webSocket, clientNameBuffer);

        //    var clientName = Encoding.UTF8.GetString(clientNameBuffer);

        //    await SendMessage(webSocket, $"Bem-vindo {clientName}!");

        //    await SendMessageToAll(clientName, $"{clientName} entrou no chat!", true);

        //    return receivedResult;
        //}

        //private async Task StartChatting(WebSocket web)
        //{

        //    var buffer = new byte[1024 * 4];

        //    while (!receivedResult.CloseStatus.HasValue)
        //    {
        //        receivedResult = await ReceiveMessage(webSocket, buffer);
        //        await SendMessageToEveryone(ClientName, $"{ClientName}: {Encoding.UTF8.GetString(buffer)}");
        //    }
        //}

        //private async Task SendMessageToAll(string from, string message, bool exceptToSender = false)
        //{
        //    var clientsWebSockets = _socketManager.GetAllConnections().ToList();
        //    if (exceptToSender)
        //    {
        //        clientsWebSockets = clientsWebSockets.Where(_ => _.ClientName != from).ToList();
        //    }

        //    if (!clientsWebSockets.Any()) return;

        //    foreach (var clientSocket in clientsWebSockets)
        //    {
        //        await SendMessage(clientSocket.WebSocket, message);
        //    }
        //}

        //private static async Task<WebSocketReceiveResult> ReceiveMessage(WebSocket webSocket, byte[] buffer) =>
        //    await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

        //private static async Task SendMessage(WebSocket webSocket, string message) => await webSocket.SendAsync(PrepareMessage(message),
        //    WebSocketMessageType.Text, WebSocketMessageFlags.EndOfMessage, CancellationToken.None);

        //private static ArraySegment<byte> PrepareMessage(string message) => new(Encoding.UTF8.GetBytes(message));
    }
}
