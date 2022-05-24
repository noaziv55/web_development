using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Data
{
    public class WebChatContext : DbContext
    {
        private const string connectionString = "server=localhost;port=3306;database=ChatAppDB;user=admin;password=toor";
        //private const string connectionString = "Server=(localdb)\\mssqllocaldb;Database=WhenUpDB;Trusted_Connection=True;MultipleActiveResultSets=true";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(connectionString, MariaDbServerVersion.AutoDetect(connectionString));
            //optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuring the Name property as the primary
            // key of the Items table
            //modelBuilder.Entity<User>().HasKey(e => e.Username);
            //modelBuilder.Entity<Message>().HasKey(e => e.Id);
            //modelBuilder.Entity<Rating>().HasKey(e => e.Id);
            modelBuilder.Entity<Contact>().HasKey(e => new { e.UsernameOfContact, e.ContactOfUser });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        //public DbSet<Rating> Ratings { get; set; }
        public DbSet<Contact> Contacts { get; set; }


    }
}