using RabbitMQ.Client;
using Service.Audience.Context;
using Service.Common.Extensions;
using Service.Common.ModelExtensions;
using System.Text;

namespace Service.Audience.Messaging.Publishers
{
    public class NotificationPublisher
    {
        private readonly IModel _channel;

        public NotificationPublisher(IModel channel)
        {
            _channel = channel;
            _channel.ExchangeDeclare(exchange: "notification", type: "fanout");
        }

        public void PublishNotification(string routingKey, NotificationMessage message)
        {
            var body = Encoding.UTF8.GetBytes(JsonHelper.Serialize(message));
            _channel.BasicPublish(exchange: "notification", routingKey: routingKey, basicProperties: null, body: body);
        }
    }

    public class ExpiringLicense : BackgroundService
    {
        private readonly AudienceAppContext _appContext;
        private readonly ILogger<ExpiringLicense> _logger;

        public ExpiringLicense(
            ILogger<ExpiringLicense> logger,
            AudienceAppContext appContext)
        {
            _logger = logger;
            _appContext = appContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var expiringSoftware = _appContext.SoftwareManager.Software
                    .Where(it => it.LicenseExpirationDate < DateTime.Now.AddDays(7));

                foreach (var software in expiringSoftware)
                {
                    _logger.LogInformation($"Отправка уведомления об истечении лицензии: {software.Name}");

                    _appContext.NotificationPublisher.PublishNotification("notification", new NotificationMessage()
                    {
                        Name = "Истекает лицензия",
                        Description = $"Лицензия на {software.Name} в аудитории {software.Audience.Number} {software.Audience.Housing.Name} истекает {software.LicenseExpirationDate}",
                    });
                }

                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
            }
        }
    }
}
