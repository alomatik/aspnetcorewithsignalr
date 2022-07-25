using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRSenderMail.WorkerService
{
    public class RabbitMQClientService:IRabbitMQClientService
    {
        private readonly IConnectionFactory _connectionFactory;
        private IConnection _connection;
        private readonly ILogger<RabbitMQClientService> _logger;
        private IModel _channel;

        public string ExchangeName => "mailsend-direct-exchange";

        public string QueueName => "mailsend-queue";

        public string RouterKeyName => "mailsend-routekey";

        public RabbitMQClientService(IConnectionFactory connectionFactory, ILogger<RabbitMQClientService> logger)
        {
            _logger = logger;
            _connectionFactory = connectionFactory;
            _connection = _connectionFactory.CreateConnection();
            CreateChannel();
        }

        public IModel ConnectRabbitMQ()
        {
            //_channel.ExchangeDeclare(exchange: ExchangeName,
            //                        type: ExchangeType.Direct,
            //                        durable: true,
            //                        autoDelete: false);
            //_logger.LogInformation("rabbitMQ exchange declare edildi.(bildirildi.)");

            //_channel.QueueDeclare(queue: QueueName,
            //                       durable: true,
            //                       exclusive: false,
            //                       autoDelete: false);
            //_logger.LogInformation("rabbitMQ queue declare edildi.(bildirildi.)");

            //_channel.QueueBind(queue: QueueName,
            //                    exchange: ExchangeName,
            //                    routingKey: RouterKeyName
            //                    );
            //_logger.LogInformation("rabbitMQ'ya queue bağlandı.(bind)");

            _logger.LogInformation("rabbitMQ ile subscriber tarafında bağlantı kuruldu.");
            return _channel;
        }

        private void CreateChannel()
        {
            if (_channel is { IsOpen: true })
            {
                return;
            }
            _channel = _connection.CreateModel();
        }

        public void Dispose()
        {
            _channel?.Close();
            _channel?.Dispose();
            _channel = default;
            
            _connection?.Close();
            _connection?.Dispose();

            _logger.LogInformation("RabbitMQ ile bağlantı kapatıldı.");
        }

    }
}
