using System.ComponentModel.DataAnnotations;

namespace PetPal.Models
{
	public class Schedule
	{
		public int ScheduleId { get; set; }

		[Required]
		public int PetId { get; set; }

		public string PetName { get; set; }

		[Required]
		[DataType(DataType.Date)]
		public DateTime ScheduleDate { get; set; }

		[DataType(DataType.Date)]
		public DateTime? EndDate { get; set; } // Optional end of recurrence

		[StringLength(50)]
		public string Recurrence { get; set; } // e.g., Daily, Weekly, Monthly, None

		[Required]
		[StringLength(100)]
		public string ScheduleType { get; set; } // Feeding, Medication

		[StringLength(100)]
		public string Portion { get; set; } // If feeding

		[StringLength(100)]
		public string Medication { get; set; } // If medication

		[StringLength(100)]
		public string Dosage { get; set; } // If medication

		[StringLength(100)]
		public string Frequency { get; set; } // Display info or reminder repetition

		public Pet Pet { get; set; }
	}
}
