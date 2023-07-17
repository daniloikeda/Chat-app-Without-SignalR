using Chat.Server.Configuration.Middleware;

namespace Chat.Server.Configuration
{
    public static class MiddlewareExtension
    {
        public static IApplicationBuilder UseChatSocketMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ChatSocketMiddleware>();
        }
    }
}
