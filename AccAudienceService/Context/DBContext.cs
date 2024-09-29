using Helper.Models;
using Microsoft.EntityFrameworkCore;

namespace AccAudienceService.Context
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
        public DbSet<EFAudience> EFAudiences { get; set; }
        public string ConnectionString { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(ConnectionString);
        }
    }
}
