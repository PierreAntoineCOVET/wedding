using Microsoft.EntityFrameworkCore;

namespace Server.Model
{
    public class AerDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        private readonly string DbPath;

        public AerDbContext()
        {
            //var folder = Environment.SpecialFolder.LocalApplicationData;
            //var path = Environment.GetFolderPath(folder);
            //Console.WriteLine(System.IO.Path.Join(path, "aer.db"));
            //DbPath = System.IO.Path.Join(path, "aer.db");
            DbPath = "aer.db";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(e => e.Name)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
