using Service.Common.Extensions;
using Service.Common.ModelExtensions;
using RabbitMQ.Client;
using System.Text;

namespace Service.Housing.Manager
{
    public class HousingPublisher
    {
        private readonly IModel _channel;

        public HousingPublisher(IModel channel)
        {
            _channel = channel;
            _channel.ExchangeDeclare(exchange: "housing", type: "topic");
        }

        public void PublishHousingMessage(string routingKey, HousingMessage message)
        {
            var body = Encoding.UTF8.GetBytes(JsonHelper.Serialize(message));
            _channel.BasicPublish(exchange: "housing", routingKey: routingKey, basicProperties: null, body: body);
        }
    }
}
