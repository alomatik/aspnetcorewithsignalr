namespace SignalRHelloSignalR.Server.Hubs.StronglyTypesInterfaces
{
    public interface ISubscribeMethodsName //Client tarafından serverde tetiklenecek method isimleri interface altında method olarak yazılr ve bu methodlar parametre olarak da server'in göndereceği değerleri alır. İsimlendirme hatalarının önüne geçilir. 
    {
        Task ReceiveMessage(string message);
        Task TotalClients(List<string> clients);
        Task UserJoined(string connectionId);
        Task UserLeft(string connectionId);
    }
}
