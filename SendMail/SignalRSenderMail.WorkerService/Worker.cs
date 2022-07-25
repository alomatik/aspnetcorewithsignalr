using Microsoft.AspNetCore.SignalR.Client;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace SignalRSenderMail.WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        IRabbitMQClientService _rabbitmqClientService;
        IModel _channel;
        SendMailService _sendMailService;

        public Worker(ILogger<Worker> logger, IRabbitMQClientService rabbitmqClientService, SendMailService sendMailService)
        {
            _logger = logger;
            _rabbitmqClientService = rabbitmqClientService;
            _sendMailService = sendMailService;
        }


        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _channel = _rabbitmqClientService.ConnectRabbitMQ();
            return base.StartAsync(cancellationToken);
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += Consumer_Received;
            _channel.BasicConsume(queue: _rabbitmqClientService.QueueName,
                                   autoAck: false,
                                   consumer: consumer);
            await Task.CompletedTask;
        }

        private async void Consumer_Received(object? sender, BasicDeliverEventArgs e)
        {
            var mailAndMessage = JsonSerializer.Deserialize<MailAndMessage>(Encoding.UTF8.GetString(e.Body.Span));
            if (!_sendMailService.SendMail(mailAndMessage))
            {
                _logger.LogInformation("Mail gönderimi sırasında bir hata oluştu.");
            }
            else
            {
                _logger.LogInformation("Mail gönderimi başarıyla gerçekleşti.");
                _channel.BasicAck(e.DeliveryTag, false);
                HubConnection hubConnection = new HubConnectionBuilder().WithUrl("https://localhost:7118/messageHub").Build();
                await hubConnection.StartAsync();
                await hubConnection.InvokeAsync("SendMessageAsync", $" Successfuly send to {mailAndMessage.Mail}.");
            }
            await Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _rabbitmqClientService.Dispose();
            return base.StopAsync(cancellationToken);
        }
    }
}