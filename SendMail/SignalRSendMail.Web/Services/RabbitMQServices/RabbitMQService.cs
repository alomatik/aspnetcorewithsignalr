using RabbitMQ.Client;

namespace SignalRSendMail.Web.Services.RabbitMQServices
{
    public class RabbitMQService : IRabbitMQService
    {
        private readonly IConnectionFactory _connectionFactory;
        private IConnection _connection;
        private readonly ILogger<RabbitMQService> _logger;
        private IModel _channel;

        public string ExchangeName => "mailsend-direct-exchange";

        public string QueueName => "mailsend-queue";

        public string RouterKeyName => "mailsend-routekey";

        public RabbitMQService(IConnectionFactory connectionFactory, ILogger<RabbitMQService> logger)
        {
            _logger = logger;
            _connectionFactory = connectionFactory;
            _connection = _connectionFactory.CreateConnection();
            CreateChannel();
        }

        public IModel ConnectRabbitMQ()
        {
            _channel.ExchangeDeclare(exchange: ExchangeName,
                                    type: ExchangeType.Direct,
                                    durable: true,
                                    autoDelete: false);
            _logger.LogInformation("rabbitMQ exchange declare edildi.(bildirildi.)");

            _channel.QueueDeclare(queue: QueueName,
                                   durable: true,
                                   exclusive: false,
                                   autoDelete: false);
            _logger.LogInformation("rabbitMQ queue declare edildi.(bildirildi.)");

            _channel.QueueBind(queue: QueueName,
                                exchange: ExchangeName,
                                routingKey: RouterKeyName
                                );
            _logger.LogInformation("rabbitMQ'ya queue bağlandı.(bind)");

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
