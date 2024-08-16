using Blog.Domain.Entities.PostAggregate;
using Blog.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Blog.Infrastructure.Persistence.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments");
            builder.HasKey(x => x.CommentId);

            builder.Property(x => x.CommentId).ValueGeneratedNever();

            builder.Property(x => x.CommentStatus)
                .HasColumnName("CommentStatusId")
                .HasConversion(new EnumToNumberConverter<CommentStatus, int>());
        }
    }
}
