namespace Blog.Application.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string fromAddress, string subject, string body);
    }
}
