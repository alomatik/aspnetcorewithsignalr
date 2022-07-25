namespace SignalRLetsTalk.Web.Models
{
    public class GroupDto
    {
        public string GroupName { get; set; }
        public List<ClientDto> ClientDtoList { get; set; } = new List<ClientDto>();
    }
}
