using MassTransit;
using QueueRecords;

namespace RabbitMQ.Subscriber.A.RabbitMQ
{

    public class MessageNotificationConsumer : IConsumer<MessageRecord>
    {
        private ILogger<MessageNotificationConsumer> _logger;

        public MessageNotificationConsumer(ILogger<MessageNotificationConsumer> logger)
        {
            _logger = logger;
        }

        //순서를 무조건 보장해야지
        public Task Consume(ConsumeContext<MessageRecord> context)
        {
            _logger.LogInformation($"This is Subscriber A, Recevied Message : {context.Message.id}, {context.Message.msg}");
           return Task.CompletedTask;
        }
    }
}
