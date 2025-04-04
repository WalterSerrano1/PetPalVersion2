using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetPal.Data;

namespace PetPal.Controllers
{
	public class CalendarController : Controller
	{
		private AppDbContext context { get; set; }
		public CalendarController(AppDbContext ctx)
		{
			context = ctx;
		}

		[HttpGet]
		public JsonResult GetCalendarEvents(int petId)
		{
			//store multiple events (appointments & training)
			var events = new List<object>();

            // Get appointments for this pet and add them to events
            var appointments = context.Appointments
            	.Where(a => a.PetId == petId)
            	.Select(a => new
				{
            		title = a.AppointmentType + " - " + a.PetName, //Event title

            		start = a.AppointmentDateTime.ToString("s"), //When the event occurs

            		color = "green" //green for appointments
				});

            events.AddRange(appointments); //Add appointment events to the list 


            var training = context.Training
				.Where(t => t.PetId == petId)
				.Select(t => new
			{
				title = t.TrainingType + " - " + t.PetName, //Event title

				start = t.TrainingDate.ToString("yyy-MM-dd"), //Training date

				color = "blue" //blue for training
			});

			events.AddRange(training);

			return Json(events);
		}

		//I seperated from the calendar due the calendar being overloaded by feeding schdules
		[HttpGet]
		public JsonResult GetTodaySchedules(int petId)
		{
			//Get today's date
			var today = DateTime.Today;

			//Query only todays feeding and medication
			var schedule = context.Schedules
				.Where
					(s => s.PetId == petId &&
						  s.ScheduleDate == today && 
						  (s.ScheduleType == "Feeding" || s.ScheduleType == "Medication"))
				.Select(s => new
				{
					s.PetName,
					s.ScheduleType,
					Time = s.ScheduleTime.HasValue
				? s.ScheduleTime.Value.ToString(@"hh\:mm")
				: "Time not set",
					s.Medication,
					s.Dosage,
					s.Portion,
					s.ReminderNote
				})
				.ToList();
			//This query gets all feeding and medication schedules from the database where the schedule matches the today
			//Filters these two types and shows the relevant data pet,name schedule tpe etc...

			return Json(schedule);
		}
	}
}
