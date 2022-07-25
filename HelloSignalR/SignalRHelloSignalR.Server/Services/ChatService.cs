using Microsoft.AspNetCore.SignalR;
using SignalRHelloSignalR.Server.Hubs;

namespace SignalRHelloSignalR.Server.Services
{
    public class ChatService
    {
        IHubContext<ChatHub> _hubContext;

        public ChatService(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendMessageAsync(string message) //Client'ların invoke edeceği(çağıracağı) method.
        {
            await _hubContext.Clients.All.SendAsync(method: "ReceiveMessage", //Server tarafından subscribe olan client'larda tetiklenecek method.
                                        arg1: message);//Server tarafından client'lara gönderilecek değer.
        }

    }
}
