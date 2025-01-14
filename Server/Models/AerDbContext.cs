using Microsoft.EntityFrameworkCore;

namespace Server.Models
{
    public class AerDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            /// For applying migrations
            //var connectionString = "server=localhost;user=root;database=db;password=password;";
            var connectionString = "server=aerdb;user=root;database=db;password=password;";
            var serverVersion = ServerVersion.AutoDetect(connectionString);

            options.UseMySql(connectionString, serverVersion);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(e => new { e.FirstName, e.LastName })
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(e => e.LastName);

            modelBuilder.Entity<User>()
                .HasIndex(e => e.UserName);

            base.OnModelCreating(modelBuilder);
        }
    }
}
