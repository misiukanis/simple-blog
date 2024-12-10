using MediatR;

namespace Blog.Application.Commands.SendEmail
{
    public class SendEmailCommand(
        string fromAddress, 
        string subject, 
        string message) : IRequest
    {
        public string FromAddress { get; } = fromAddress;
        public string Subject { get; } = subject;
        public string Message { get; } = message;
    }
}
