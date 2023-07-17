using System.Text;
using System.Net.WebSockets;

namespace Chat.Server.Handlers.Implementations
{
    public class ChatHandler : IChatHandler
    {
        private readonly IConnectionManager _socketManager;

        private string ClientName { get; set; }

        public ChatHandler(IConnectionManager socketManager)
        {
            _socketManager = socketManager;
            ClientName = "";
        }

        public async Task StartChat(WebSocket webSocket)
        {
            var receivedResult = await RegisterClient(webSocket);
            var buffer = new byte[1024 * 4];

            while (!receivedResult.CloseStatus.HasValue)
            {
                receivedResult = await ReceiveMessage(webSocket, buffer);
                await SendMessageToEveryone(ClientName, $"{ClientName}: {Encoding.ASCII.GetString(buffer)}");
            }

            _socketManager.RemoveConnection(ClientName);
        }

        private async Task<WebSocketReceiveResult> RegisterClient(WebSocket webSocket)
        {
            var buffer = new byte[100];

            await SendMessage(webSocket, "Qual o seu nome ?");

            var receivedResult = await ReceiveMessage(webSocket, buffer);

            ClientName = Encoding.ASCII.GetString(buffer);

            await SendMessage(webSocket, $"Bem-vindo {ClientName}!");

            await SendMessageToEveryone(ClientName, $"{ClientName} entrou no chat!");

            _socketManager.AddConnection(ClientName, webSocket);

            return receivedResult;
        }

        private async Task SendMessageToEveryone(string from, string message)
        {
            var clientsWebSockets = _socketManager.GetClientsSocket(from).ToList();

            if (!clientsWebSockets.Any()) return;

            foreach (var clientSocket in clientsWebSockets)
            {
                await SendMessage(clientSocket, message);
            }
        }

        private static async Task<WebSocketReceiveResult> ReceiveMessage(WebSocket webSocket, byte[] buffer) =>
            await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

        private static async Task SendMessage(WebSocket webSocket, string message) => await webSocket.SendAsync(PrepareMessage(message),
            WebSocketMessageType.Text, WebSocketMessageFlags.EndOfMessage, CancellationToken.None);

        private static ArraySegment<byte> PrepareMessage(string message) => new(Encoding.ASCII.GetBytes(message));
    }
}
