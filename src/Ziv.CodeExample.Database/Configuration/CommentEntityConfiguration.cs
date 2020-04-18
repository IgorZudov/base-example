using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ziv.CodeExample.Entities;

namespace Ziv.CodeExample.Database.Configuration
{
    public class CommentEntityConfiguration  : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Text).HasMaxLength(500);
            builder.HasOne(x => x.User)
                .WithMany(x => x.Comments);
        }
    }
}