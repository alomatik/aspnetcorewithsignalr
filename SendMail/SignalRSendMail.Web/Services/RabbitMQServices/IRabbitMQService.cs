using RabbitMQ.Client;

namespace SignalRSendMail.Web.Services.RabbitMQServices
{
    public interface IRabbitMQService:IDisposable
    {
        string ExchangeName { get; }
        string QueueName { get; }
        string RouterKeyName { get; }

        IModel ConnectRabbitMQ();
    }
}
