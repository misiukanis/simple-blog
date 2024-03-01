using Blog.Application.Services.Interfaces;
using Blog.Shared.Extensions;
using Blog.Shared.Settings;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Blog.Infrastructure.Services
{
    public class EmailService(IOptionsSnapshot<EmailSettings> emailSettings) : IEmailService
    {
        private readonly EmailSettings _emailSettings = emailSettings.Value;

        public async Task SendEmailAsync(string emailFrom, string subject, string body)
        {
            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress(string.Empty, emailFrom));
            message.To.Add(new MailboxAddress(string.Empty, _emailSettings.EmailAddress));
            message.Subject = subject;

            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = body.nl2br();

            message.Body = bodyBuilder.ToMessageBody();

            using (SmtpClient client = new SmtpClient())
            {
                await client.ConnectAsync(_emailSettings.EmailHost, _emailSettings.EmailPort);
                await client.AuthenticateAsync(_emailSettings.EmailAddress, _emailSettings.EmailPassword);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
    }
}
