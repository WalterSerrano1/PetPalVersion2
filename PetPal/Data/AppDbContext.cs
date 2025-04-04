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
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Training> Training { get; set; }

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
                new Pet { PetId = 1, PetName = "Joey", PetType = "Cat", PetBreed = "Japanese Bobtail", PetBirthday = new DateTime(2010, 07, 10), PetAge = 15, PetGender = "Male", ImageUrl = "/images/Joey.jpg", UserId = 1 },
                new Pet { PetId = 2, PetName = "Mavis", PetType = "Dog", PetBreed = "Australian Shepard", PetBirthday = new DateTime(2022, 03, 23), PetAge = 4, PetGender = "Female", ImageUrl = "/images/Mavis.jpg", UserId = 2 },
                new Pet { PetId = 3, PetName = "Ollie", PetType = "Dog", PetBreed = "Golden Doodle", PetBirthday = new DateTime(2020, 11, 01), PetAge = 4, PetGender = "Male", ImageUrl = "/images/Ollie.jpg", UserId = 2 },
                new Pet { PetId = 4, PetName = "Max", PetType = "Dog", PetBreed = "Labrador", PetBirthday = new DateTime(2021, 01, 01), PetAge = 4, PetGender = "Male", ImageUrl = "", UserId = 2 }
            );

            modelBuilder.Entity<Appointment>().HasData(
                new Appointment
                {
                    AppointmentId = 1,
                    PetId = 1,
                    PetName = "Joey",
                    AppointmentDateTime = new DateTime(2025, 04, 20, 10, 30, 0), //2025 April 20, 10:30
                    Location = "Happy Paws Clinic",
                    AppointmentType = "Vet",
                    Notes = "Regualar check-up and vaccination update",
                },

                new Appointment
                {
                    AppointmentId = 2,
                    PetId = 2,
                    PetName = "Mavis",
                    AppointmentDateTime = new DateTime(2025, 04, 10, 14, 30, 0),// 2025 April 10, 2:00
                    Location = "Pet Smart",
                    AppointmentType = "Grooming",
                    Notes = "Full grooming with nail trim",
                },

                new Appointment
                {
                    AppointmentId = 3,
                    PetId = 3,
                    PetName = "Ollie",
                    AppointmentDateTime = new DateTime(2025, 04, 12, 14, 30, 0),// 2025 April 12, 2:00
                    Location = "Dog & Cat Hospital - East Hill",
                    AppointmentType = "Vet",
                    Notes = "Follow-up for paw stitches",
                }
             );

            modelBuilder.Entity<Schedule>().HasData(
                new Schedule
                {
                    ScheduleId = 1,
                    PetId = 1,
                    PetName = "Joey",
                    ScheduleDate = new DateTime(2025, 04, 04),
                    ScheduleTime = new TimeSpan(8, 0, 0), // 8:00 AM
                    ScheduleType = "Feeding",
                    Portion = "1/4 cup dry food",
                    Recurrence = "Daily",
                    ReminderNote = "Morning feeding",
                },
                new Schedule
                {
                    ScheduleId = 2,
                    PetId = 2,
                    PetName = "Mavis",
                    ScheduleDate = new DateTime(2025, 04, 04),
                    ScheduleTime = new TimeSpan(11, 0, 0),
                    ScheduleType = "Feeding",
                    Portion = "1/2 cup of OpenFarm food",
                    Recurrence = "Daily",
                    ReminderNote = "Morning feeding"

                },
                new Schedule
                {
					ScheduleId = 3,
					PetId = 3,
					PetName = "Ollie",
					ScheduleDate = new DateTime(2025, 04, 04),
					ScheduleTime = new TimeSpan(18, 0, 0), // 6:00 PM
					ScheduleType = "Medication",
					Medication = "Antibiotic",
					Dosage = "5 ml",
					Recurrence = "Daily",
					ReminderNote = "After evening meal",
				}
            );

            modelBuilder.Entity<Training>().HasData(
                new Training
                {
					TrainingId = 1,
					PetId = 1,
					PetName = "Joey",
					TrainingDate = new DateTime(2025, 04, 01),
					TrainingType = "Play Time",
					TrainingDurationMinutes = 20,
					TrainingNotes = "Interactive toy session indoors."
				},
                new Training
                {
					TrainingId = 2,
					PetId = 2,
					PetName = "Mavis",
					TrainingDate = new DateTime(2025, 04, 03),
					TrainingType = "Walking",
					TrainingDurationMinutes = 30,
					TrainingNotes = "Walked around the park. Very energetic."
				},
                new Training
                {
					TrainingId = 3,
					PetId = 3,
					PetName = "Ollie",
					TrainingDate = new DateTime(2025, 04, 04),
					TrainingType = "New Trick",
					TrainingDurationMinutes = 60,
					TrainingNotes = "Practiced recall and fetch. Good progress."
				}
            );

        }
		// Configure entity relationships, indexes, etc. here if needed
	}
}