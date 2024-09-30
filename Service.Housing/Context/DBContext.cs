using Service.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Service.Housing.Models.EF;

namespace Service.Housing.Context
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

        public DbSet<EFHousing> EFHousing { get; set; }

        public DatabaseFacade DatabaseContext => Database;
        public string ConnectionString { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(ConnectionString);
        }
    }
}

