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
		[DataType(DataType.Date)]
		public DateTime AppointmentDate { get; set; }

		[DataType(DataType.Time)]
		public DateTime AppointmentTime { get; set; }

		public string Location { get; set; }

		[Required]
		[StringLength(50)]
		public string AppointmentType { get; set; }

		[StringLength(500)]
		public string Notes { get; set; }
		public bool IsComplete { get; set; }
		public Pet Pet { get; set; }
	}
}
