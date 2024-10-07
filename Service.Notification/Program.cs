
using Service.Common.Extensions;
using Service.Notification.Consumers;

namespace Service.Notification
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddRabbitMQ(builder.Configuration);
            builder.Services.AddSignalR();

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen();


            builder.Services.AddHostedService<NotificationConsumer>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            app.UseSwagger();
            app.UseSwaggerUI();
            //}

            app.UseRouting();

            app.Run();
        }
    }
}
