
using AccHousingService;
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

            builder.Services.AddSingleton<AppContext>();
            builder.Services.AddDbContext<DbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            /*if (app.Environment.IsDevelopment())
            {*/
            app.UseSwagger();
            app.UseSwaggerUI();
            /*}*/

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
