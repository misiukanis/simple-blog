using Blog.Application.Services.Interfaces;
using MediatR;

namespace Blog.Application.Commands.SendEmail
{
    public class SendEmailCommandHandler : IRequestHandler<SendEmailCommand>
    {
        private readonly IEmailsService _emailsService;

        public SendEmailCommandHandler(IEmailsService emailsService)
        {
            _emailsService = emailsService;
        }

        public async Task<Unit> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            await _emailsService.SendEmailAsync(request.EmailFrom, request.Subject, request.Message);

            return Unit.Value;
        }
    }
}
