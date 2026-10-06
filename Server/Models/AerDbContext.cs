using Microsoft.EntityFrameworkCore;

namespace Server.Models
{
    public class AerDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Form> Forms { get; set; }

        public AerDbContext(DbContextOptions<AerDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(user => user.Id);

                entity.HasIndex(user => new { user.FirstName, user.LastName })
                    .IsUnique();

                entity.HasIndex(user => user.LastName);
                entity.Property(user => user.LastName)
                    .HasMaxLength(50);

                entity.Property(user => user.FirstName)
                    .HasMaxLength(50);

                entity.HasIndex(user => user.UserName);
                entity.Property(user => user.UserName)
                    .HasMaxLength(100);

                entity.Property(user => user.Invitation)
                    .HasColumnType("integer");

                entity.HasOne(user => user.Form)
                    .WithOne(form => form.User)
                    .HasForeignKey<Form>(form => form.UserId);
            });

            modelBuilder.Entity<Form>(entity =>
            {
                entity.HasKey(form => form.Id);

                //entity.HasIndex(form => form.UserId)
                //    .IsUnique();

                entity.Property(form => form.FoodAllergy)
                    .HasMaxLength(300);

                entity.Property(form => form.MusicRecommendation)
                    .HasMaxLength(500);

                entity.Property(form => form.Other)
                    .HasMaxLength(500);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
