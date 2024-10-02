
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Service.Audience.Context;
using Service.Audience.Manager;
using Service.Common.Extensions;

namespace Service.Audience
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Service.Audience.API", Version = "v1" });
            });
            builder.Services.AddRabbitMQ(builder.Configuration);

            builder.Configuration.AddEnvironmentVariables();

            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                    ?? throw new Npgsql.NpgsqlException("Строка подключения указана неверно.");

            builder.Services.AddDbContext<DBContext>(options =>
                options.UseNpgsql(connectionString));

            builder.Services.AddSingleton<AudienceAppContext>();

            builder.Services.AddHostedService<HousingConsumer>();

            var app = builder.Build();


            /*if (app.Environment.IsDevelopment())
            {*/
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");
            });
            /*}*/

            MigrationHelper.Migrate<DBContext>(app.Services, connectionString);


            app.UseRouting();

            app.MapControllers();

            app.Run();

        }
    }
}
