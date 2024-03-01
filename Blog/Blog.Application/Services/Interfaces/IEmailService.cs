namespace Blog.Application.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string emailFrom, string subject, string body);
    }
}
