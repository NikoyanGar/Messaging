using Common.Messages;
using MassTransit;
using Microsoft.Extensions.Hosting;

namespace Publisher
{
    public class MessageType2Publisher : BackgroundService
    {
        readonly IBus _bus;
        Random random = new Random();

        public MessageType2Publisher(IBus bus)
        {
            _bus = bus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _bus.Publish(
                 new MessageType2
                 {
                     RequestBody = "{\"card_number\":\"1234567890123456\",\"card_holder\":\"John Doe\",\"expiration_date\":\"12/23\",\"cvv\":\"123\"}",
                     ResponseBody = "{\"order_id\":\"123456789\",\"status\":\"success\"}",
                     TraceId = Guid.NewGuid().ToString(),
                     Url = "processing",
                     EntityId = random.Next(1, 10),
                     Date = DateTime.UtcNow,// DateTime.UtcNow.AddHours(random.Next(1, 1000)),
                 }, stoppingToken); ;

            }
        }
    }
}

