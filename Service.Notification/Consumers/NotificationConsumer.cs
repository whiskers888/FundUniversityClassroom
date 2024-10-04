using Microsoft.AspNetCore.SignalR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Service.Common.Extensions;
using Service.Common.ModelExtensions;
using Service.Notification.Hubs;
using System.Text;

namespace Service.Notification.Consumers
{
    public class NotificationConsumer : BackgroundService
    {
        private readonly ILogger<NotificationConsumer> _logger;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IHubContext<NotificationHub> _hubContext;
        public NotificationConsumer(
            IHubContext<NotificationHub> hubContext,
            ILogger<NotificationConsumer> logger,
            IConnection connection,
            IModel channel)
        {
            _hubContext = hubContext;
            _logger = logger;
            _connection = connection;
            _channel = channel;

            _channel.ExchangeDeclare(exchange: "notification", type: "fanout");

            var queueName = _channel.QueueDeclare(queue: "notification_queue", durable: false, exclusive: false);

            _channel.QueueBind(queue: queueName, exchange: "notification", routingKey: "");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var notification = JsonHelper.Deserialize<NotificationMessage>(message);

                HandleNotification(notification);
            };

            _channel.BasicConsume(queue: "notification_queue", autoAck: true, consumer: consumer);
            await Task.CompletedTask;
        }

        private void HandleNotification(NotificationMessage notification)
        {
            // Хотелось бы подключить еще редис какой нибудь, чтобы следить за уведомлениями
            // Были ли они прочитаны и какие у пользователя имеются.
            // При входе подтягивать из него все уведомления пользователя
            // Но время уже поджимает, надо бы сделать фронтенд, поэтому оставил так
            _logger.LogInformation($"Получено уведомление: {notification.Name} Описание: {notification.Description}");

            _hubContext.Clients.All.SendAsync("ReceiveNotification", notification);
        }
    }
}
