using Chat.Server.Handlers;

namespace Chat.Server.Configuration.Middleware
{
    public class ChatSocketMiddleware
    {
        private readonly RequestDelegate _next;

        public ChatSocketMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var scoped = httpContext.RequestServices.CreateScope();
            var chatHandler = scoped.ServiceProvider.GetRequiredService<IChatHandler>();

            if (httpContext.Request.Path != "/chat")
                await _next.Invoke(httpContext);

            if (httpContext.WebSockets.IsWebSocketRequest)
            {
                using var webSocket = await httpContext.WebSockets.AcceptWebSocketAsync();
                await chatHandler.StartChat(webSocket);
            }
        }
    }
}
