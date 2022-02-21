namespace Blog.Application.Services.Interfaces
{
    public interface IEmailsService
    {
        Task SendEmailAsync(string emailFrom, string subject, string body);
    }
}
