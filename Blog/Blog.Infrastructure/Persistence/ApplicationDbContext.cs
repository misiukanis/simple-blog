using Blog.Domain.Core;
using Blog.Domain.Entities.PostAggregate;
using Blog.Domain.ValueObjects;
using Blog.Infrastructure.Identity;
using Blog.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Blog.Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IUnitOfWork
    {
        private readonly IDomainEventsDispatcher _domainEventsDispatcher;
                

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ForbiddenWord> ForbiddenWords { get; set; }


        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            IDomainEventsDispatcher domainEventsDispatcher) : base(options)
        {
            _domainEventsDispatcher = domainEventsDispatcher;
        }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var domainEntities = ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.GetDomainEvents() != null && x.Entity.GetDomainEvents().Any()).ToList();

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.GetDomainEvents())
                .ToList();

            domainEntities.ForEach(entity => entity.Entity.ClearDomainEvents());

            await _domainEventsDispatcher.DispatchEventsAsync(domainEvents);

            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
