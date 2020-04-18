using Microsoft.EntityFrameworkCore;
using Ziv.CodeExample.Database.Configuration;
using Ziv.CodeExample.Entities;

namespace Ziv.CodeExample.Database
{
    /// <summary>
    /// Контекст EF Core для приложения
    /// </summary>
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new UserEntityConfiguration());
            builder.ApplyConfiguration(new CommentEntityConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }
    }
}