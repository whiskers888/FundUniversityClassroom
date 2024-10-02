using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serivice.Context;
using Service.Common.Extensions;
using Service.Housing.Context;

namespace Serivice.Housing
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Service.Housing.API", Version = "v1" });
            });
            builder.Services.AddRabbitMQ(builder.Configuration);
            builder.Services.AddSingleton<HousingAppContext>();

            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                    ?? throw new Npgsql.NpgsqlException("Строка подключения указана неверно.");
            builder.Services.AddDbContext<DBContext>(options =>
                options.UseNpgsql(connectionString));

            var app = builder.Build();

            //if (app.Environment.IsDevelopment())
            //{
            app.UseSwagger();
            app.UseSwaggerUI();
            //}

            MigrationHelper.Migrate<DBContext>(app.Services, connectionString);

            app.UseRouting();

            app.MapControllers();

            app.Run();
        }
    }
}
