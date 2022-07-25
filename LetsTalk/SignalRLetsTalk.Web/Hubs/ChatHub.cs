using Microsoft.AspNetCore.SignalR;
using SignalRLetsTalk.Web.InMemoryData;
using SignalRLetsTalk.Web.Models;
using System.Security.Claims;

namespace SignalRLetsTalk.Web.Hubs
{
    public class ChatHub : Hub
    {

        public async Task ConvertNickName(string userNickName)
        {
            ClientDto clientDto = new ClientDto()
            {
                ConnectionId = Context.ConnectionId,
                NickName = userNickName
            };

            ClientDatas.ClientDtoList.Add(clientDto);
            await Clients.Others.SendAsync("ClientJoined", clientDto.NickName);

            await Clients.All.SendAsync("OnlineUsers", ClientDatas.ClientDtoList);

        }
        public async Task SendMessage(string message, string receiveNickName)
        {

            var senderClient = ClientDatas.ClientDtoList.FirstOrDefault(c => c.ConnectionId == Context.ConnectionId);

            if (receiveNickName == "AllUsers")
            {
                await Clients.All.SendAsync("ReceiveMessageAllClient", message, senderClient.NickName);
            }
            else
            {
                var receiveClient = ClientDatas.ClientDtoList.FirstOrDefault(cd => cd.NickName == receiveNickName);
                await Clients.Clients(receiveClient.ConnectionId).SendAsync("ReceiveMessageAClient", message, senderClient.NickName);
            }
        }

        public async Task SendMessageToGroup(string message, string groupName)
        {
            var senderClient = ClientDatas.ClientDtoList.FirstOrDefault(c => c.ConnectionId == Context.ConnectionId);

            await Clients.Group(groupName).SendAsync("ReceiveMessageAllClientInGroup", message, senderClient.NickName);
        }

        public async Task AddToGroup(string groupName)
        {
            var client = ClientDatas.ClientDtoList.FirstOrDefault(cd => cd.ConnectionId == Context.ConnectionId);
            ClientDto clientDto = new ClientDto
            {
                ConnectionId = client.ConnectionId,
                NickName = client.NickName
            };

            if (!ClientDatas.GroupDtoList.Any(x => x.GroupName == groupName))
            {
                GroupDto groupDto = new GroupDto();
                groupDto.GroupName = groupName;
                groupDto.ClientDtoList.Add(clientDto);
                ClientDatas.GroupDtoList.Add(groupDto);
            }
            else
            {
                ClientDatas.GroupDtoList.ForEach(gd =>
                {
                    if (!gd.ClientDtoList.Contains(clientDto))
                    {
                        gd.ClientDtoList.Add(client);
                    }
                });
            }

            await Groups.AddToGroupAsync(client.ConnectionId, groupName);

            await Clients.All.SendAsync("AllGroups", ClientDatas.GroupDtoList);
        }


        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);
        }
    }


    //public async Task SendMessage(string message, string senderUserNickName, string receiverUserNickName)
    //{
    //    foreach (var item in ClientDatas.ClientDtoList)
    //    {
    //        if (senderUserNickName == item.NickName)
    //        {
    //            await Clients.Client(item.ConnectionId).SendAsync("SenderUserMessage", message,senderUserNickName);
    //        }
    //        if (receiverUserNickName == item.NickName)
    //        {
    //            await Clients.Client(item.ConnectionId).SendAsync("ReceiveUserMessage", message,receiverUserNickName);
    //        }
    //    }
    //}

}
