using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

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

		[DataType(DataType.Time)]
		public TimeSpan? ScheduleTime { get; set; } 

		[DataType(DataType.Date)]
		public DateTime? EndDate { get; set; }

		[StringLength(50)]
		public string Recurrence { get; set; } // Daily, Weekly, etc.

		[Required]
		[StringLength(100)]
		public string ScheduleType { get; set; } // Feeding, Medication

		[StringLength(100)]
		public string? Portion { get; set; }

		[StringLength(100)]
		public string? Medication { get; set; }

		[StringLength(100)]
		public string? Dosage { get; set; }

		[StringLength(100)]
		public string? ReminderNote { get; set; } // Instead of "Frequency"

		[ValidateNever]
		public Pet Pet { get; set; }

		//Not in the database, display only
		public bool IsPast => ScheduleDate.Add(ScheduleTime ?? TimeSpan.Zero) < DateTime.Now;
	}
}
