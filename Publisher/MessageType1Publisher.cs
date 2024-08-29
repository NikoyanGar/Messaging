using Common.Messages;
using MassTransit;
using Microsoft.Extensions.Hosting;
using Bogus;

namespace Publisher
{
    public class MessageType1Publisher : BackgroundService
    {
        readonly IBus _bus;
        readonly Faker faker;

        public MessageType1Publisher(IBus bus)
        {
            _bus = bus;
            faker = new Faker();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _bus.Publish(
                 new MessageType1
                 {
                     RequestBody = GenerateRequestBody(),
                     ResponseBody = GenerateResponseBody(),
                     TraceId = Guid.NewGuid().ToString(),
                     EntityId = faker.Random.Int(1, 1000000),
                     Date = DateTime.UtcNow,
                 }, stoppingToken);

                await Task.Delay(1000); // Add a 1-second delay
            }
        }

        private string GenerateRequestBody()
        {
            return $"{{\"card_number\":\"{faker.Finance.CreditCardNumber()}\",\"card_holder\":\"{faker.Name.FullName()}\",\"expiration_date\":\"{faker.Date.Future().ToString("MM/yy")}\",\"cvv\":\"{faker.Random.Number(100, 999)}\"}}";
        }

        private string GenerateResponseBody()
        {
            return $"{{\"order_id\":\"{faker.Random.Number(100000000, 999999999)}\",\"status\":\"success\"}}";
        }
    }
}
