using Microsoft.AspNetCore.Builder;
using System.Net;
using Chat.Server.Configuration;
using Chat.Server.Handlers;

var builder = WebApplication.CreateBuilder(args);
builder.AddDependencies();

var app = builder.Build();

app.UseRouting();

app.UseWebSockets();

app.UseChatSocketMiddleware();

app.Run();
