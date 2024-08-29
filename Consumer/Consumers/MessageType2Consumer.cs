using Common.Messages;
using MassTransit;
using Newtonsoft.Json;

namespace Consumer.Consumers
{
    public class MessageType2Consumer : IConsumer<MessageType2>
    {
        private readonly ILogger<MessageType2Consumer> _logger;

        public MessageType2Consumer(ILogger<MessageType2Consumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<MessageType2> context)
        {
             string messageJson = JsonConvert.SerializeObject(context.Message);
            _logger.LogInformation("Message consumed: {Message}", messageJson);

            // throw new NotImplementedException();
        }
    }
    public class MessageType2ConsumerDefinition : ConsumerDefinition<MessageType2Consumer>
    {
        public MessageType2ConsumerDefinition()
        {
            //// override the default endpoint name
            //EndpointName = "order-service";

            //// limit the number of messages consumed concurrently
            //// this applies to the consumer only, not the endpoint
            //ConcurrentMessageLimit = 8;
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<MessageType2Consumer> consumerConfigurator)
        {
            //// configure message retry with millisecond intervals
            //endpointConfigurator.UseMessageRetry(r => r.Intervals(100, 200, 500, 800, 1000));

            //// use the outbox to prevent duplicate events from being published
            //endpointConfigurator.UseInMemoryOutbox();
        }
    }
}
