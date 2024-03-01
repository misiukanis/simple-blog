using Blog.Application.Services.Interfaces;
using Blog.Domain.Core;
using Blog.Domain.Repositories.Interfaces;
using Blog.Infrastructure.Persistence;
using Blog.Infrastructure.Repositories;
using Blog.Infrastructure.Services;
using Blog.Infrastructure.Services.Interfaces;
using Blog.Shared.Constants;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace Blog.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString(SettingConstants.DefaultConnection);
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            services.AddTransient<IDbConnection>(provider => new SqlConnection(connectionString));

            services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<ApplicationDbContext>());

            services.AddScoped<IDomainEventsDispatcher, DomainEventsDispatcher>();

            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IFileUploadingService, FileUploadingService>();

            services.AddScoped<IPostsRepository, PostsRepository>();
            services.AddScoped<ILookupService, LookupService>();

            return services;
        }
    }
}
