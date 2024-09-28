using AccHousingService.Models;
using Microsoft.EntityFrameworkCore;

namespace AccHousingService
{
    public class DBContext : DbContext
    {

        public DBContext(string cnnString)
        {
            ConnectionString = cnnString;
        }
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }

        public DbSet<EFHousing> EFHousing { get; set; }
        public string ConnectionString { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(ConnectionString);
        }
    }
}

