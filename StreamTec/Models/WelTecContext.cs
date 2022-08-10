using Microsoft.EntityFrameworkCore;

namespace StreamTec.Models
{
    public class WelTecContext : DbContext
    {
        public WelTecContext()
        {
        }

        //Creating a constructor for the Context Class inheriting from the base class
        public WelTecContext(DbContextOptions options) : base(options)
        {

        }

        //Creating an object of each model class using the DbSet from EntityFrameworkCore
        public DbSet<Student> Students { get; set; }
        public DbSet<Stream> Streams { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Using the modelBuilder from EntityFrameworkCore we can create an entity of student or stream
            //and convert it into a table using ToTable();
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<Stream>().ToTable("Stream");
        }
    }
}
