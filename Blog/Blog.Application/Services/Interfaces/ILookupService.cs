namespace Blog.Application.Services.Interfaces
{
    public interface ILookupService
    {
        Task<IEnumerable<string>> GetForbiddenWordsAsync();
    }
}
