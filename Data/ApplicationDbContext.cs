using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BridalBlueprint.Models;

namespace BridalBlueprint.Data 
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Wedding> Weddings { get; set; }
        public DbSet<WeddingUser> WeddingUsers { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<WeddingTask> WeddingTasks { get; set; }
        public DbSet<BudgetItem> BudgetItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<WeddingUser>()
                .HasKey(wu => new { wu.UserId, wu.WeddingId });

            builder.Entity<WeddingUser>()
                .HasOne(wu => wu.User)
                .WithMany(u => u.WeddingUsers)
                .HasForeignKey(wu => wu.UserId);

            builder.Entity<WeddingUser>()
                .HasOne(wu => wu.Wedding)
                .WithMany(w => w.WeddingUsers)
                .HasForeignKey(wu => wu.WeddingId);
        }
    }
}


