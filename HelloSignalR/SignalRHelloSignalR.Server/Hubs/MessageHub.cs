using Microsoft.AspNetCore.SignalR;

namespace SignalRHelloSignalR.Server.Hubs
{
    public class MessageHub:Hub
    {
        //private string _connectionId;
        //public MessageHub()
        //{
        //    _connectionId= Context.ConnectionId;
        //}
        public async Task SendMessageAsync(string message,string groupName, IEnumerable<string> excludedConnectionIds) //Client'ların invoke edeceği(çağıracağı) method.
        {
            #region Caller - Sadece server'a bildirim gönderen client'la iletişim kurar.
            //await Clients.Caller.SendAsync("ReceiveMessage", message);

            #endregion
            #region All - Server'a bağlı olan tüm clientlara mesaj gönderilir.
            //await Clients.All.SendAsync("ReceiveMessage", message);

            #endregion
            #region Other - Kendisi hariç servere bağlı tüm clientlara mesaj gönderilir.
            await Clients.Others.SendAsync("ReceiveMessage", message);

            #endregion

            #region HUB CLIENTS METHODS
            #region AllExcept 
            //**Belirtilen clinet/client'lar hariç tüm client'lara bildiride bulunur.
            //await Clients.AllExcept(connectionIds).SendAsync("ReceiveMessage",message);

            #endregion
            #region Client
            //**Yalnızca belirtilen client'a bildiride bulunur.
            //await Clients.Client(connectionIds.First()).SendAsync("ReceiveMessage", message);
            #endregion
            #region Clients
            //**Yalnızca belirtilen client'a/client'lara bildiride bulunur.
            //await Clients.Clients(connectionIds).SendAsync("ReceiveMessage", message);
            #endregion

            #region Group
            //**Belirtilen gruptaki tüm client'lara bildiride bulunur. Bunun için client'lar gruplara subscribe olmalıdır.
            //await Clients.Group(groupName).SendAsync("ReceiveMessage",message);
            #endregion
            #region Groups
            //**Birden çok gruba bildiride bulunur.
            //await Clients.Groups(groupName).SendAsync("ReceiveMessage", message);
            #endregion
            #region GroupExcept
            //**Belirtilen guptaki client'lar dışındaki client'lara bildiride bulunur.
            //await Clients.GroupExcept(groupName, excludedConnectionIds).SendAsync("ReceiveMessage", message);
            #endregion
            #region OthersInGroup
            //**Grupta mesajı gönderen client harici tüm client'lara bildiride bulunur.
            //await Clients.OthersInGroup(groupName).SendAsync("ReceiveMessage", message);
            #endregion
            #region User
            //**Auth olan kullanıcıya(id ile) bildirimde bulunur.
            //await Clients.User("userId").SendAsync("ReceiveMessage", message);
            #endregion
            #region Users
            //**Auth olan kullanıcılara(id listesi ile) bildirimde bulunur.
            //await Clients.Users("userIdList").SendAsync("ReceiveMessage",message);
            #endregion

            #endregion

        }

        public async Task AddGroupAsync(string connectionId, string groupName )
        {
              await Groups.AddToGroupAsync(connectionId, groupName); //ilgili id'li client'ı gruba ekler.
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync("YourJoined", Context.ConnectionId);
            
            await base.OnConnectedAsync();
        }

    }
}
