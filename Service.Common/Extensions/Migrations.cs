using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace Service.Common.Extensions
{
    public static class MigrationHelper
    {
        public static void Migrate<TContext>(IServiceProvider services, string connectionString) where TContext : class, IDbContext
        {
            using (var scope = services.CreateScope())
            {
                TContext context = scope.ServiceProvider.GetRequiredService<TContext>();
                context.ConnectionString = connectionString;

                int retries = 10;
                TimeSpan retryDelay = TimeSpan.FromSeconds(5);

                while (retries > 0)
                {
                    try
                    {
                        context.Database.Migrate();
                        break;
                    }
                    catch (NpgsqlException ex)
                    {
                        retries--;
                        if (retries == 0)
                        {
                            throw;
                        }
                        Console.WriteLine($"Не удалость установить подключение к БД. Попытка через {retryDelay.TotalSeconds} секунд... ({retries} попытка)");
                        Thread.Sleep(retryDelay);
                    }
                }
            }
        }
    }
}
