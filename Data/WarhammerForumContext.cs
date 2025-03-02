using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WarhammerForum.Models;

namespace WarhammerForum.Data
{
    public class WarhammerForumContext : IdentityDbContext<ApplicationUser>
    {
        public WarhammerForumContext(DbContextOptions<WarhammerForumContext> options)
            : base(options)
        {
        }

        public DbSet<WarhammerForum.Models.Discussion> Discussion { get; set; } = default!;
        public DbSet<WarhammerForum.Models.Comment> Comment { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Start fresh with base Identity configuration
            base.OnModelCreating(modelBuilder);

            // Fix the circular reference with minimal configuration
            modelBuilder.Entity<ApplicationUser>()
                .HasMany<Comment>()
                .WithOne(c => c.ApplicationUser)
                .HasForeignKey(c => c.ApplicationUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}