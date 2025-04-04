using System.ComponentModel.DataAnnotations;


namespace PetPal.Models
{
	public class Appointment
	{
		public int AppointmentId { get; set; }

		[Required]
		public int PetId { get; set; }

		[Required] 
		public string PetName { get; set; }

		[Required]
		public DateTime AppointmentDateTime { get; set; }

		public string Location { get; set; }

		[Required]
		[StringLength(50)]
		public string AppointmentType { get; set; }

		[StringLength(500)]
		public string Notes { get; set; }
<<<<<<< HEAD
		public bool IsComplete { get; set; }
		public Pet? Pet { get; set; }
=======

		public Pet Pet { get; set; }

		//Not added to the database, only for display
		public bool IsPast => AppointmentDateTime < DateTime.Now;
>>>>>>> origin/master
	}
}
