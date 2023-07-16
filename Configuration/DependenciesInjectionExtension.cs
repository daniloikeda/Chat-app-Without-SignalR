using Chat.Server.WebSocketsHandler;

namespace Chat.Server.Configuration
{
    public static class DependenciesInjectionExtension
    {
        public static void AddDependenciesInjection(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IWebSocketManager, WebSocketsHandler.WebSocketManager>();
            builder.Services.AddScoped<IMessageHandler, MessageHandler>();
        }
    }
}
