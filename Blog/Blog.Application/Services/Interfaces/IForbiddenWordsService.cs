namespace Blog.Application.Services.Interfaces
{
    public interface IForbiddenWordsService
    {
        Task<IEnumerable<string>> GetForbiddenWordsAsync();
    }
}
