using AccHousingService.Manager;
using Helper;

namespace AccHousingService
{
    public class AppContext : BaseAppContext
    {
        public AppContext(IConfiguration config) : base(config)
        {
            Title = "AccAudienceService";
            Configuration = config;
            Initialize();
        }

        public void Initialize()
        {
            AudienceManager = new AudienceManager(this);
        }

        public AudienceManager AudienceManager { get; set; }

        public string Title { get; set; }
        private IConfiguration Configuration { get; set; }

        public DBContext CreateDbContext() => new(Configuration.GetConnectionString("DefaultConnection") ?? throw new ArgumentException("Строка подключения указана неверно."));

    }
}
