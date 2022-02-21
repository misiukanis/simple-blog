using Blog.Common.Constants;
using Blog.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Blog.Application.EventHandlers
{
    public class CommentStatusChangedEventHandler : INotificationHandler<CommentStatusChangedEvent>
    {
        private readonly ILogger _logger;

        public CommentStatusChangedEventHandler(ILoggerFactory logger)
        {
            _logger = logger.CreateLogger(LoggerConstants.EventsLogger);
        }

        public Task Handle(CommentStatusChangedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Comment status changed, comment id: {notification.Comment.CommentId}, new status: {notification.Comment.CommentStatus}, date: {notification.DateOccurred}");

            return Task.CompletedTask;
        }
    }
}
