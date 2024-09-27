using Microsoft.Extensions.Configuration;

namespace Helper
{
    public class BaseAppContext
    {
        public BaseAppContext(IConfiguration config)
        {
            Configuration = config;
        }

        public string Title { get; set; }
        protected IConfiguration Configuration { get; set; }
    }
}
