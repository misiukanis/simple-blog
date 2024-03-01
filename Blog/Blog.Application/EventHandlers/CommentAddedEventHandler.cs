using Blog.Domain.Events;
using Blog.Shared.Constants;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Blog.Application.EventHandlers
{
    public class CommentAddedEventHandler(ILoggerFactory logger) : INotificationHandler<CommentAddedEvent>
    {
        private readonly ILogger _logger = logger.CreateLogger(LoggerConstants.EventsLogger);

        public Task Handle(CommentAddedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Comment added, comment id: {notification.Comment.CommentId}, date: {notification.DateOccurred}");

            return Task.CompletedTask;
        }
    }
}
