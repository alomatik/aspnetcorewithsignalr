using RabbitMQ.Client;
using SignalRSendMail.Web.Models;
using System.Text;
using System.Text.Json;

namespace SignalRSendMail.Web.Services.RabbitMQServices
{
    public class RabbitMQPublisher
    {
        IRabbitMQService _rabbitmqService;
        IModel _channel;
        ILogger<RabbitMQPublisher> _logger;

        public RabbitMQPublisher(IRabbitMQService rabbitmqService, ILogger<RabbitMQPublisher> logger)
        {
            _rabbitmqService = rabbitmqService;
            _logger = logger;
        }

        public void Publish(MailandMessageDto mailandMessageDto)
        {
            var jsonBody= JsonSerializer.Serialize(mailandMessageDto);
            _channel = _rabbitmqService.ConnectRabbitMQ();
            _channel.BasicPublish(exchange: _rabbitmqService.ExchangeName,
                                 routingKey: _rabbitmqService.RouterKeyName,
                                 body: Encoding.UTF8.GetBytes(jsonBody)
                                 );
            _logger.LogInformation("rabbitMQ'ya publish edildi.");
        }
    }
}
