using Blog.Domain.Entities.PostAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.Persistence.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("Posts");
            builder.HasKey(x => x.PostId);

            builder.Property(x => x.PostId).ValueGeneratedNever();

            builder.HasMany(x => x.Comments).WithOne().HasForeignKey("PostId");
        }
    }
}
