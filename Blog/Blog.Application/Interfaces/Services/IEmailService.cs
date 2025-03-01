namespace Blog.Application.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string fromAddress, string subject, string body);
    }
}
