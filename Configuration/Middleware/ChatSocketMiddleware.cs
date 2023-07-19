using System.Net.WebSockets;
using Chat.Server.Handlers;

namespace Chat.Server.Configuration.Middleware
{
    public class ChatSocketMiddleware
    {
        private readonly RequestDelegate _next;

        private IWebSocketHandler _chatHandler;

        public ChatSocketMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, IWebSocketHandler chatHandler)
        {
            _chatHandler = chatHandler;

            if (httpContext.Request.Path != "/chat")
                await _next.Invoke(httpContext);

            if (httpContext.WebSockets.IsWebSocketRequest)
            {
                using var webSocket = await httpContext.WebSockets.AcceptWebSocketAsync();
                _chatHandler.OnConnected(webSocket);
                var clientName = await GetClientNameAsync(webSocket);
                await ListenToClient(webSocket, clientName);
            }
        }

        public async Task<string> GetClientNameAsync(WebSocket webSocket)
        {
            await _chatHandler.SendMessage(webSocket, "Qual o seu nome ?");
            var clientName = await _chatHandler.ReceiveMessage(webSocket);
            await _chatHandler.SendMessageToAll($"~{clientName} entrou no chat!");

            return clientName;
        }

        public async Task ListenToClient(WebSocket webSocket, string clientName)
        {
            while (webSocket.State == WebSocketState.Open)
            {
                var message = await _chatHandler.ReceiveMessage(webSocket);

                message = $"{clientName}: {message}";
                await _chatHandler.SendMessageToAll(message);
            }
        }
    }
}
