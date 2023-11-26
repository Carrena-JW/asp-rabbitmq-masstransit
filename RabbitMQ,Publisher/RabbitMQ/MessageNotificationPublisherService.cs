using MassTransit;
using QueueRecords;

namespace RabbitMQ_Publisher.RabbitMQ
{

    public interface IMessageNotificationPublisherService
    {
        Task SendNotification(Guid guid, string message);
    }

    public class MessageNotificationPublisherService : IMessageNotificationPublisherService
    {
        private ILogger<MessageNotificationPublisherService> _logger;
        private IPublishEndpoint _publishEndpoint;

        public MessageNotificationPublisherService(ILogger<MessageNotificationPublisherService> logger, IPublishEndpoint publishEndpoint)
        {
            _logger = logger;
            _publishEndpoint = publishEndpoint;
           

        }

        public async Task SendNotification(Guid guid, string message)
        {
            _logger.LogInformation($"Publish message id : {guid}");
            await _publishEndpoint.Publish(new MessageRecord(guid, message));
        }
    }

   
}
