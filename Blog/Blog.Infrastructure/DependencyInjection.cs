using Blog.Application.Providers.Interfaces;
using Blog.Application.Services.Interfaces;
using Blog.Domain.Core;
using Blog.Domain.Repositories.Interfaces;
using Blog.Infrastructure.Persistence;
using Blog.Infrastructure.Providers;
using Blog.Infrastructure.Repositories;
using Blog.Infrastructure.Services;
using Blog.Infrastructure.Services.Interfaces;
using Blog.Shared.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString(SettingConstants.DefaultConnection);

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<ApplicationDbContext>());

            services.AddScoped<IDbConnectionFactory>(provider => new DbConnectionFactory(connectionString));

            services.AddScoped<IDomainEventsDispatcher, DomainEventsDispatcher>();

            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IFileUploadingService, FileUploadingService>();

            services.AddScoped<IPostsRepository, PostsRepository>();
            services.AddScoped<IForbiddenWordsService, ForbiddenWordsService>();

            return services;
        }
    }
}
