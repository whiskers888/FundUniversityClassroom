using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Service.Audience.Models.EFModels;
using Service.Common;

namespace Service.Audience.Context
{
    public class DBContext : DbContext, IDbContext
    {

        public DBContext(string cnnString)
        {
            ConnectionString = cnnString;
        }

        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }
        public DbSet<EFAudience> EFAudiences { get; set; }
        public DbSet<EFHousingSummary> EFHousing { get; set; }
        public string ConnectionString { get; set; }

        public DatabaseFacade DatabaseContext => Database;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(ConnectionString);
        }
    }
}
