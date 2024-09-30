using Microsoft.Extensions.Configuration;

namespace Service.Common
{
    public interface IAppContext
    {
        public string Title { get; set; }
        public IConfiguration Configuration { get; set; }
    }
}
