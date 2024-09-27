using Microsoft.EntityFrameworkCore;

namespace AccHousingService
{
    public class DBContext : DbContext
    {

        /*Перечисление моделей*/
        public DBContext(string cnnString)
        {
            ConnectionString = cnnString;
        }

        public string ConnectionString { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(ConnectionString);
        }
    }
}

