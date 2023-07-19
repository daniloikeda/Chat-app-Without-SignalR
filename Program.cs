using Chat.Server.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.AddDependencies();

var app = builder.Build();

app.UseRouting();

app.UseWebSockets();

app.UseChatSocketMiddleware();

app.Run();
