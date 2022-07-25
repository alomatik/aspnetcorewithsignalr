using Microsoft.AspNetCore.SignalR;

namespace SignalRSendMail.Web.Hubs
{
    public class MessageHub:Hub
    {
        public async Task SendMessageAsync(string message)
        {
            await Clients.Caller.SendAsync("ReceiveMessage", message);
        } 
    }
}
