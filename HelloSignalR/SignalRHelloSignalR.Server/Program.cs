 using SignalRHelloSignalR.Server.Hubs;
using SignalRHelloSignalR.Server.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<ChatService>();
builder.Services.AddCors(corsOptions =>
{
    corsOptions.AddDefaultPolicy(corsPolicyBuilder =>
    {
        corsPolicyBuilder.AllowAnyMethod().AllowAnyHeader().AllowAnyHeader().AllowCredentials().SetIsOriginAllowed(orign => true);
    });
});
builder.Services.AddSignalR();

builder.Services.AddControllers();

var app = builder.Build();

app.UseCors();

app.MapControllers();

//app.MapHub<ChatHub>("/chatHub");//www.site.com/chathub isteği ile huba bağlanılır. 
app.MapHub<MessageHub>("/messageHub");//www.site.com/chathub isteği ile huba bağlanılır. 


app.MapGet("/", () => "Hello World!");

app.Run();
