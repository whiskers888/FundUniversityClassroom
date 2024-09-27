using AccHousingService.Manager;
using Helper;

namespace AccHousingService
{
    public class AppContext : BaseAppContext
    {
        public AppContext(IConfiguration config) : base(config)
        {
            Title = "AccHousingService";
            Initialize();
        }

        public void Initialize()
        {
            HousingManager = new HousingManager(this);
        }
        public HousingManager HousingManager { get; set; }

        public DBContext CreateDbContext() => new(Configuration.GetConnectionString("DefaultConnection") ?? throw new ArgumentException("Строка подключения указана неверно."));

    }
}
