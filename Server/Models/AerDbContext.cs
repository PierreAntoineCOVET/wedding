using Microsoft.EntityFrameworkCore;

namespace Server.Models
{
    public class AerDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            /// For applying migrations
            //var connectionString = "server=localhost;user=root;database=aerdb;password=password;";
            var connectionString = "server=aerdb;user=root;database=aerdb;password=password;";
            var serverVersion = ServerVersion.AutoDetect(connectionString);

            options.UseMySql(connectionString, serverVersion);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(user => user.Id);

            modelBuilder.Entity<User>()
                .HasIndex(user => new { user.FirstName, user.LastName })
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(user => user.LastName);
            modelBuilder.Entity<User>()
                .Property(user => user.LastName)
                .HasMaxLength(50);

            modelBuilder.Entity<User>()
                .Property(user => user.FirstName)
                .HasMaxLength(50);

            modelBuilder.Entity<User>()
                .HasIndex(user => user.UserName);
            modelBuilder.Entity<User>()
                .Property(user => user.UserName)
                .HasMaxLength(100);

            modelBuilder.Entity<User>()
                .Property(user => user.Invitation)
                .HasMaxLength(5);

            base.OnModelCreating(modelBuilder);
        }
    }
}
