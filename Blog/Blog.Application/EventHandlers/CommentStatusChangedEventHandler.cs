using Blog.Domain.Events;
using Blog.Shared.Constants;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Blog.Application.EventHandlers
{
    public class CommentStatusChangedEventHandler(ILoggerFactory logger) : INotificationHandler<CommentStatusChangedEvent>
    {
        private readonly ILogger _logger = logger.CreateLogger(LoggerConstants.EventsLogger);

        public Task Handle(CommentStatusChangedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Comment status changed, comment id: {notification.Comment.CommentId}, new status: {notification.Comment.CommentStatus}, date: {notification.DateOccurred}");

            return Task.CompletedTask;
        }
    }
}
