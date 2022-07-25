using Microsoft.AspNetCore.SignalR;
using SignalRHelloSignalR.Server.Hubs.StronglyTypesInterfaces;

namespace SignalRHelloSignalR.Server.Hubs
{
    public class ChatHub : Hub<ISubscribeMethodsName> 
    {
        static List<string> clients = new List<string>();

        public async Task SendMessageAsync(string message) //Client'ların invoke edeceği(çağıracağı) method.
        {
            await Clients.All.ReceiveMessage(message);

            //await Clients.All.SendAsync(method: "ReceiveMessage", //Server tarafından subscribe olan client'larda tetiklenecek method.
            //                            arg1: message);//Server tarafından client'lara gönderilecek değer.
        }

        public override async Task OnConnectedAsync()//Hub sınıfından gelen bir method'dur.Client sisteme bağlandığı zaman tetiklenir.
        {
            var connectionId = Context.ConnectionId;//Hub'a bağlantı gerçekleştiren client'lara sistem tarafından verilen unique bir değerdir. 

            clients.Add(Context.ConnectionId);
            await Clients.All.TotalClients(clients);
            await Clients.All.UserJoined(connectionId);
            await base.OnConnectedAsync();

            //clients.Add(Context.ConnectionId);
            //await Clients.All.SendAsync("TotalClients", clients);
            //await Clients.All.SendAsync("UserJoined", Context.ConnectionId);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)//Hub sınıfından gelen bir method'dur.Client'ın bağlantısı koptuğu zaman tetiklenir.
        {
            var connectionId = Context.ConnectionId;//Hub'a bağlantı gerçekleştiren client'lara sistem tarafından verilen unique bir değerdir. 
            clients.Remove(Context.ConnectionId);
            await Clients.All.TotalClients(clients);
            await Clients.All.UserLeft(connectionId);
            await base.OnDisconnectedAsync(exception);

            //await Clients.All.SendAsync("TotalClients", clients);
            //await Clients.All.SendAsync("UserLeft", Context.ConnectionId);
            //var connectionId = Context.ConnectionId;
        }
    }
}
