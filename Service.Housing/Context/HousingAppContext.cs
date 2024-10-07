using RabbitMQ.Client;
using Service.Common;
using Service.Housing.Context;
using Service.Housing.Manager;

namespace Serivice.Context
{
    public class HousingAppContext : IAppContext
    {
        public HousingAppContext(IConfiguration config, IModel rabbitMqChannel,
            ILogger<HousingAppContext> logger)
        {
            Title = "Service.Housing";
            Logger = logger;
            Configuration = config;
            HousingPublisher = new HousingPublisher(rabbitMqChannel);
            Initialize();
        }

        public void Initialize()
        {
            HousingManager = new HousingManager(this);
        }

        public HousingPublisher HousingPublisher { get; set; }
        public HousingManager HousingManager { get; set; }
        public ILogger<HousingAppContext> Logger { get; set; }
        public string Title { get; set; }
        public IConfiguration Configuration { get; set; }

        public DBContext CreateDbContext() => new(Configuration.GetConnectionString("DefaultConnection") ?? throw new ArgumentException("Строка подключения указана неверно."));

    }
}
