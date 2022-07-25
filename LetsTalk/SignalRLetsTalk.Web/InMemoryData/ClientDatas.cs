using SignalRLetsTalk.Web.Models;

namespace SignalRLetsTalk.Web.InMemoryData
{
    public static class ClientDatas
    {
        public static List<ClientDto> ClientDtoList { get; } = new List<ClientDto>();
        public static List<GroupDto> GroupDtoList { get; } = new List<GroupDto>();
    }
}
