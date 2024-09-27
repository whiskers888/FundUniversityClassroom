namespace AccHousingService
{
    public class AppContext
    {
        public AppContext(IConfiguration config)
        {
            Title = "AccAudienceService";
            Configuration = config;
            Initialize();
        }

        public void Initialize()
        {

            /*Инициализация менеджеров*/

        }

        public string Title { get; set; }
        private IConfiguration Configuration { get; set; }

        public DBContext CreateDbContext() => new(Configuration.GetConnectionString("DefaultConnection") ?? throw new ArgumentException("Строка подключения указана неверно."));

    }
}
