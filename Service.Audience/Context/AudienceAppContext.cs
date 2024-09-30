using RabbitMQ.Client;
using Service.Audience.Manager;
using Service.Common;

namespace Service.Audience.Context
{
    public class AudienceAppContext : IAppContext
    {
        public AudienceAppContext(IConfiguration config, IModel rabbitMqChannel)
        {
            Title = "AccAudienceService";
            Configuration = config;
            RabbitMQChannel = rabbitMqChannel;
            Initialize();
        }

        public void Initialize()
        {
            AudienceManager = new AudienceManager(this);
        }

        public IModel RabbitMQChannel { get; set; }

        public AudienceManager AudienceManager { get; set; }

        public string Title { get; set; }
        public IConfiguration Configuration { get; set; }

        public DBContext CreateDbContext() => new(Configuration.GetConnectionString("DefaultConnection") ?? throw new ArgumentException("Строка подключения указана неверно."));

    }
}
