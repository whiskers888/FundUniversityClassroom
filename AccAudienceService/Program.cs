using AccAudienceService.Context;
using Microsoft.EntityFrameworkCore;

namespace AccAudienceService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton<AudienceAppContext>();

            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                    ?? throw new Npgsql.NpgsqlException("Строка подключения указана неверно.");
            builder.Services.AddDbContext<DBContext>(options =>
                options.UseNpgsql(connectionString));

            var app = builder.Build();

            /*if (app.Environment.IsDevelopment())
            {*/
            app.UseSwagger();
            app.UseSwaggerUI();
            /*}*/

            Migrate(app, builder, connectionString);

            app.UseRouting();

            app.MapControllers();

            app.Run();
        }


        // Авто миграция, надо как то бы вынести в helper,
        // но пока не разобрался как сделать так чтобы он знал о классе WebApplication
        public static void Migrate(WebApplication app, WebApplicationBuilder builder, string connectionString)
        {
            using (IServiceScope scope = app.Services.CreateScope())
            {
                IServiceProvider services = scope.ServiceProvider;
                DBContext context = services.GetRequiredService<DBContext>();
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
                    catch (Npgsql.NpgsqlException ex)
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
