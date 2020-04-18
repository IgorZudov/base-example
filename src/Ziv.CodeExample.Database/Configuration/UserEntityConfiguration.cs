using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ziv.CodeExample.Entities;

namespace Ziv.CodeExample.Database.Configuration
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Email).IsUnique();
            builder.Property(x => x.Email).HasMaxLength(50);
            builder.HasMany(x => x.Comments)
                .WithOne(x => x.User);
        }
    }
}