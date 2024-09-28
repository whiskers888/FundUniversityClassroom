using AccAudienceService.Models;
using Microsoft.EntityFrameworkCore;

namespace AccHousingService
{
    public class DBContext : DbContext
    {

        public DBContext(string cnnString)
        {
            ConnectionString = cnnString;
        }
        public DbSet<EFAudience> EFAudiences { get; set; }
        public string ConnectionString { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(ConnectionString);
        }
    }
}
