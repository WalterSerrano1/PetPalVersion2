using System.ComponentModel.DataAnnotations;

namespace PetPal.Models
{
	public class Pet
	{
		//primary key
		public int PetId { get; set; }

		//Foreign Key
		public int UserId { get; set; }

		[Required (ErrorMessage ="Pet name is required")]
		[StringLength(100)]
		public string PetName { get; set; }

		[Required(ErrorMessage ="Pet type is required")]
		public string PetType { get; set; }


		[StringLength(100)]
		public string PetBreed { get; set; }

		public DateTime PetBirthday { get; set; }

        [Required(ErrorMessage = "Age is required")]
        [Range(0, 100, ErrorMessage = "Age must be between 0 and 100")]
        public int PetAge { get; set; }


        [Required(ErrorMessage = "Gender is required")]
        [StringLength(10)]
		public string PetGender { get; set; }
		
		public string ImageUrl { get; set; } = "/images/default-pet.jpg"; //default value

        public List<Appointment> Appointments { get; set; } = new List<Appointment>();

        public List<Schedule> Schedules { get; set; } = new List<Schedule>();

        public List<Training> Training { get; set; } = new List<Training>();

		//navigation property
		public User? User { get; set; }
    }
}