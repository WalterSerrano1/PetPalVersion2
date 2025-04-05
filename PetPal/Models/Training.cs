using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

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
		public int TrainingDurationMinutes { get; set; }

		[StringLength(500)]
		public string TrainingNotes { get; set; }

		[ValidateNever]
		public Pet Pet { get; set; }
		
	}
}
