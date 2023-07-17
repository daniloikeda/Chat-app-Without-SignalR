using Chat.Server.Handlers;
using Chat.Server.Handlers.Implementations;

namespace Chat.Server.Configuration
{
    public static class DependenciesExtension
    {
        public static void AddDependencies(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IConnectionManager, ConnectionManager>();
            builder.Services.AddScoped<IChatHandler, ChatHandler>();
        }
    }
}
