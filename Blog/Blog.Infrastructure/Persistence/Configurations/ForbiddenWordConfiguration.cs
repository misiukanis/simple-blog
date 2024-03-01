using Blog.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.Persistence.Configurations
{
    public class ForbiddenWordConfiguration : IEntityTypeConfiguration<ForbiddenWord>
    {
        public void Configure(EntityTypeBuilder<ForbiddenWord> builder)
        {
            builder.ToTable("ForbiddenWords");

            builder.Property<int>("ForbiddenWordId"); // shadow property
        }
    }
}
