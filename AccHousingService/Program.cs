
using Microsoft.EntityFrameworkCore;

namespace AccHousingService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton<AppContext>();
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            Console.WriteLine($"Connection String: {connectionString}");
            builder.Services.AddDbContext<DBContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            app.UseSwagger();
            app.UseSwaggerUI();
            //}
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<DBContext>();
                context.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                context.Database.Migrate();
            }
            app.UseRouting();

            app.MapControllers();

            app.Run();
        }
    }
}
