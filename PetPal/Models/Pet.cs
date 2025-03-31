using System.ComponentModel.DataAnnotations;

namespace PetPal.Models
{
	public class Pet
	{
		public int PetId { get; set; }

		//Foreign Key
		public int UserId { get; set; }

		[Required]
		[StringLength(100)]
		public string PetName { get; set; }

		[Required]
		public string PetType { get; set; }


		[StringLength(100)]
		public string PetBreed { get; set; }

		public DateTime PetBirthday { get; set; }

		public int PetAge { get; set; }


		[StringLength(10)]
		public string PetGender { get; set; }
		
		public string ImageUrl { get; set; }

        public List<Appointment> Appointments { get; set; } = new List<Appointment>();

        public List<Schedule> Schedules { get; set; } = new List<Schedule>();

        public List<Training> Training { get; set; } = new List<Training>();

		//navigation property
		public User User { get; set; }
    }
}