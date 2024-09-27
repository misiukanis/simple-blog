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

            // ValueObject:
            builder.OwnsOne(
                x => x.Author,
                y =>
                {
                    y.Property(p => p.Name).HasColumnName("AuthorName");
                    y.Property(p => p.Email).HasColumnName("AuthorEmail");
                });

            builder.Property(x => x.CommentStatus)
                .HasColumnName("CommentStatusId")
                .HasConversion(new EnumToNumberConverter<CommentStatus, int>());
        }
    }
}
