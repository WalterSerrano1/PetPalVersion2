using Microsoft.EntityFrameworkCore;
using PetPal.Models;
using System.ComponentModel.DataAnnotations;

namespace PetPal.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Pet> Pet { get; set; }

        public DbSet<Appointment> Appointment { get; set; }

        // Add other DbSet properties for your entities here

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //hash passwords for premade users
            string password1 = BCrypt.Net.BCrypt.HashPassword("Josie");
            string password2 = BCrypt.Net.BCrypt.HashPassword("Walter");

            //Configure user/pet relationship
            modelBuilder.Entity<Pet>()
                .HasOne(p => p.User)
                .WithMany(u => u.Pets)
                .HasForeignKey(p => p.UserId);

            //seed Users
            modelBuilder.Entity<User>().HasData(
            new User { UserId = 1, UserName = "Josie", Password = password1 },
            new User { UserId = 2, UserName = "Walter", Password = password2 }
            );

            modelBuilder.Entity<Pet>().HasData(
                new Pet { PetId = 1, PetName = "Joey",     PetType = "Cat", PetBreed = "Japanese Bobtail",     PetBirthday = new DateTime(2010, 07, 10),   PetAge = 15, PetGender = "Male",     ImageUrl = "/images/Joey.jpg",      UserId = 1 },
                new Pet { PetId = 2, PetName = "Mavis",    PetType = "Dog", PetBreed = "Australian Shepard",   PetBirthday = new DateTime(2022, 01, 01),   PetAge = 3,  PetGender = "Male",     ImageUrl = "/images/Mavis.jpg",     UserId = 2 },
                new Pet { PetId = 3, PetName = "Ollie",    PetType = "Dog", PetBreed = "Golden Doodle",        PetBirthday = new DateTime(2021, 01, 01),   PetAge = 4,  PetGender = "Male",     ImageUrl = "/images/Ollie.jpg",     UserId = 2 },
                new Pet { PetId = 4, PetName = "Max",      PetType = "Dog", PetBreed = "Labrador",             PetBirthday = new DateTime(2021, 01, 01),   PetAge = 4,  PetGender = "Male",     ImageUrl = "",                      UserId = 2 }
            );
            // Configure entity relationships, indexes, etc. here if needed
        }
    }
}