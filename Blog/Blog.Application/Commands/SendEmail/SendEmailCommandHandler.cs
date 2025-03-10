﻿using Blog.Application.Interfaces.Services;
using MediatR;

namespace Blog.Application.Commands.SendEmail
{
    public class SendEmailCommandHandler(IEmailService emailService) : IRequestHandler<SendEmailCommand>
    {
        private readonly IEmailService _emailService = emailService;

        public async Task Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            await _emailService.SendEmailAsync(request.FromAddress, request.Subject, request.Message);
        }
    }
}
