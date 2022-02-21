using Blog.Application.Services.Interfaces;
using Blog.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Services
{
    public class LookupService : ILookupService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public LookupService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<string>> GetForbiddenWordsAsync()
        {
            return await _applicationDbContext.ForbiddenWords
                .Select(x => x.Name)
                .AsNoTracking()
                .ToListAsync();
        }

    }
}
