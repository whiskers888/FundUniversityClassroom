using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Service.Audience.Context;
using Service.Audience.Models.EFModels;
using Service.Common.Extensions;
using Service.Common.ModelExtensions;
using System.Text;

namespace Service.Audience.Manager
{
    public class HousingConsumer : BackgroundService
    {
        private readonly ILogger<HousingConsumer> _logger;
        private readonly AudienceAppContext _appContext;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly DBContext _dbContext;

        public HousingConsumer(
            ILogger<HousingConsumer> logger,
            AudienceAppContext appContext,
            IConnection connection,
            IModel channel)
        {
            _logger = logger;
            _appContext = appContext;
            _dbContext = appContext.CreateDbContext();
            _connection = connection;
            _channel = channel;

            _channel.ExchangeDeclare(exchange: "housing", type: "topic");

            var queueName = _channel.QueueDeclare(queue: "housing_queue", durable: false, exclusive: false, autoDelete: false);
            _channel.QueueBind(queue: "housing_queue", exchange: "housing", routingKey: "housing.*");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                HousingMessage message = JsonHelper.Deserialize<HousingMessage>(Encoding.UTF8.GetString(body));
                var routingKey = ea.RoutingKey;
                _logger.LogInformation($"Получено здание: ID:{message.Id} Name:{message.Name} с ключом: {routingKey}");

                switch (routingKey)
                {
                    case "housing.add":
                        Create(message);
                        break;
                    case "housing.update":
                        Update(message);
                        break;
                    case "housing.delete":
                        Delete(message);
                        break;
                    default:
                        _logger.LogWarning($"Неправильный ключ: {routingKey}");
                        break;
                }
            };

            _channel.BasicConsume(queue: "housing_queue", autoAck: true, consumer: consumer);

            await Task.CompletedTask;
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }

        public void Create(HousingMessage message)
        {
            if (_dbContext.EFHousing.FirstOrDefault(it => it.Id == message.Id) == null)
            {
                EFHousingSummary entity = new EFHousingSummary()
                {
                    Id = message.Id,
                    Name = message.Name,
                };
                _dbContext.Add(entity);
                _dbContext.SaveChanges();
                _logger.LogInformation($"Добавлено здание: ID:{message.Id} Name:{message.Name}");
                return;
            }

            _logger.LogError($"Здание не было добавлено в сервис аудиторий: ID:{message.Id} Name:{message.Name}");
        }
        public void Update(HousingMessage message)
        {
            EFHousingSummary entity = _dbContext.EFHousing.FirstOrDefault(it => it.Id == message.Id);
            if (entity != null)
            {
                entity.Name = message.Name;
                EntityEntry entityEntry = _dbContext.Entry(entity);
                if (entityEntry.State != EntityState.Added)
                    entityEntry.State = EntityState.Modified;
                _dbContext.SaveChanges();
                _logger.LogInformation($"Обновлено здание: ID:{message.Id} Name:{message.Name}");
                return;
            }
            else
            {
                _logger.LogError($"Здание не было обновлено в сервисе аудиторий: ID:{message.Id} Name:{message.Name}, так как его не существует");
            }

            _logger.LogError($"Здание не было обновлено в сервисе аудиторий: ID:{message.Id} Name:{message.Name}");

        }
        public void Delete(HousingMessage message)
        {
            EFHousingSummary entity = _dbContext.EFHousing.FirstOrDefault(it => it.Id == message.Id);
            if (entity != null)
            {
                _dbContext.Remove(entity);
                _dbContext.SaveChanges();
                _logger.LogInformation($"Удалено здание: ID:{message.Id} Name:{message.Name}");
                return;
            }
            else
            {
                _logger.LogError($"Здание не было удалено в сервисе аудиторий: ID:{message.Id} Name:{message.Name}, так как его не существует");
            }
            _logger.LogInformation($"Здание не было удалено в сервисе аудиторий: ID:{message.Id} ");
        }
    }
}