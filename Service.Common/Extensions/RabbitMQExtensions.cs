using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace Service.Common.Extensions
{
    public static class RabbitMQExtensions
    {
        public static IServiceCollection AddRabbitMQ(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConnectionFactory>(sp =>
            {
                return new ConnectionFactory
                {
                    HostName = configuration["RabbitMQ:HostName"],
                    UserName = configuration["RabbitMQ:UserName"],
                    Password = configuration["RabbitMQ:Password"]
                };
            });

            services.AddSingleton(sp =>
            {
                var factory = sp.GetRequiredService<IConnectionFactory>();
                return CreateConnectionWithRetry(factory);
            });

            services.AddSingleton(sp =>
            {
                var connection = sp.GetRequiredService<IConnection>();
                return connection.CreateModel();
            });

            return services;
        }

        private static IConnection CreateConnectionWithRetry(IConnectionFactory factory)
        {
            const int maxRetries = 10;
            const int delayBetweenRetries = 5000; // 5 seconds

            for (int attempt = 1; attempt <= maxRetries; attempt++)
            {
                try
                {
                    return factory.CreateConnection();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to connect to RabbitMQ (attempt {attempt}/{maxRetries}): {ex.Message}");
                    if (attempt == maxRetries)
                    {
                        throw; // Re-throw the exception if all attempts fail
                    }
                    Thread.Sleep(delayBetweenRetries);
                }
            }

            throw new InvalidOperationException("This code should never be reached.");
        }
    }
}