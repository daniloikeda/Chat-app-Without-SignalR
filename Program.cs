using Microsoft.AspNetCore.Builder;
using System.Net;
using Chat.Server.Configuration;
using Chat.Server.WebSocketsHandler;

var builder = WebApplication.CreateBuilder(args);
builder.AddDependenciesInjection();

var app = builder.Build();

app.UseRouting();

app.UseWebSockets();

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/chat")
    {
        if (context.WebSockets.IsWebSocketRequest)
        {
            using var webSocket = await context.WebSockets.AcceptWebSocketAsync();
            using var scope = app.Services.CreateScope();
            var messageHandler = scope.ServiceProvider.GetRequiredService<IMessageHandler>();
            await messageHandler.StartChat(webSocket);
        }
    }
    else
    {
        await next.Invoke();
    }
});

app.Run();
