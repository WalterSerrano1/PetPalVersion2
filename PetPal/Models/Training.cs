using System.ComponentModel.DataAnnotations;

namespace PetPal.Models
{
	public class Training
	{
		public int TrainingId { get; set; }

		[Required]
		public int PetId { get; set; }

		public string PetName { get; set; }

		[Required]
		[DataType(DataType.Date)]
		public DateTime TrainingDate { get; set; }

		[Required]
		[StringLength(100)]
		public string TrainingType { get; set; } //Walking, Play time or training etc.

		[Range(1, 300)]
		public int TrainingDuration { get; set; }

		[StringLength(500)]
		public string TrainingNotes { get; set; }
		public Pet Pet { get; set; }
	}
}
