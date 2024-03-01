using MediatR;

namespace Blog.Application.Commands.SendEmail
{
    public class SendEmailCommand(
        string emailFrom, 
        string subject, 
        string message) : IRequest
    {
        public string EmailFrom { get; } = emailFrom;
        public string Subject { get; } = subject;
        public string Message { get; } = message;
    }
}
