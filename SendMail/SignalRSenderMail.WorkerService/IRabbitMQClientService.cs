using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRSenderMail.WorkerService
{
    public interface IRabbitMQClientService:IDisposable
    {
        string ExchangeName { get; }
        string QueueName { get; }
        string RouterKeyName { get; }

        IModel ConnectRabbitMQ();

    }
}
