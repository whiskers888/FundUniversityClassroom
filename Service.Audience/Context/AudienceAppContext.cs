using RabbitMQ.Client;
using Service.Audience.Manager;
using Service.Audience.Messaging.Publishers;
using Service.Common;

namespace Service.Audience.Context
{
    public class AudienceAppContext : IAppContext
    {
        public AudienceAppContext(IConfiguration config, IModel rabbitMqChannel, ILogger<AudienceAppContext> logger)
        {
            Title = "Service.Audience";
            Logger = logger;
            Configuration = config;
            RabbitMQChannel = rabbitMqChannel;
            Initialize();

        }

        public void Initialize()
        {

            AudienceManager = new AudienceManager(this);
            AudFieldManager = new AudFieldManager(this);
            SoftwareManager = new SoftwareManager(this);
            NotificationPublisher = new NotificationPublisher(RabbitMQChannel);
        }

        public IModel RabbitMQChannel { get; set; }

        public AudienceManager AudienceManager { get; set; }
        public SoftwareManager SoftwareManager { get; set; }
        public AudFieldManager AudFieldManager { get; set; }
        public NotificationPublisher NotificationPublisher { get; set; }
        public ILogger<AudienceAppContext> Logger { get; set; }

        public string Title { get; set; }
        public IConfiguration Configuration { get; set; }

        public DBContext CreateDbContext() => new(Configuration.GetConnectionString("DefaultConnection") ?? throw new ArgumentException("Строка подключения указана неверно."));

    }
}
