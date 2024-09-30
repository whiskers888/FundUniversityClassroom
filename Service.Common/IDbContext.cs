using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Service.Common
{
    public interface IDbContext
    {
        DatabaseFacade Database { get; }
        string ConnectionString { get; set; }
    }
}
