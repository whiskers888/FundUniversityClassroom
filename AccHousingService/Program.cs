
using AccHousingService.Context;
using Microsoft.EntityFrameworkCore;

namespace AccHousingService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton<HousingAppContext>();
            builder.Services.AddDbContext<DBContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            //if (app.Environment.IsDevelopment())
            //{
            app.UseSwagger();
            app.UseSwaggerUI();
            //}

            Migrate(app, builder);

            app.UseRouting();

            app.MapControllers();

            app.Run();
        }

        // Авто миграция, надо как то бы вынести в helper,
        // но пока не разобрался как сделать так чтобы он знал о классе WebApplication
        public static void Migrate(WebApplication app, WebApplicationBuilder builder)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<DBContext>();
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                context.ConnectionString = connectionString;

                var retries = 10;
                var retryDelay = TimeSpan.FromSeconds(5);

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
