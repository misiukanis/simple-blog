using Blog.Application.Interfaces.ReadRepositories;
using Blog.Application.Interfaces.Services;
using Blog.Domain.Interfaces.Persistence;
using Blog.Domain.Interfaces.Repositories;
using Blog.Infrastructure.Persistence;
using Blog.Infrastructure.Providers;
using Blog.Infrastructure.Providers.Interfaces;
using Blog.Infrastructure.ReadRepositories;
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
            var connectionString = configuration.GetConnectionString(SettingConstants.DefaultConnection) ?? throw new InvalidOperationException($"Connection string '{SettingConstants.DefaultConnection}' not found.");

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<ApplicationDbContext>());

            services.AddScoped<IPostRepository, PostRepository>();

            services.AddSingleton<IDbConnectionFactory>(provider => new DbConnectionFactory(connectionString));

            services.AddScoped<IReadPostRepository, ReadPostRepository>();
            services.AddScoped<IReadCommentRepository, ReadCommentRepository>();            

            services.AddScoped<IDomainEventsDispatcher, DomainEventsDispatcher>();

            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IFileUploadingService, FileUploadingService>();

            return services;
        }
    }
}
