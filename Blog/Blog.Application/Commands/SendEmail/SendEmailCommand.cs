using MediatR;

namespace Blog.Application.Commands.SendEmail
{
    public class SendEmailCommand : IRequest
    {
        public string EmailFrom { get; }
        public string Subject { get; }
        public string Message { get; }

        public SendEmailCommand(string emailFrom, string subject, string message)
        {
            EmailFrom = emailFrom;
            Subject = subject;
            Message = message;
        }
    }
}
