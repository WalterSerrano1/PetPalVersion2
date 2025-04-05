using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetPal.Data;

namespace PetPal.Controllers
{
	public class CalendarController : Controller
	{
		//Get AppDbContext via constructor injection
		private AppDbContext context { get; set; }
		public CalendarController(AppDbContext ctx)
		{
			context = ctx;
		}

		//Get calendar events based on pet id
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


			//filter training by pet id
			var training = context.Training
				.Where(t => t.PetId == petId)
				.Select(t => new
				{
					title = t.TrainingType + " - " + t.PetName, //Event title

					start = t.TrainingDate.ToString("yyy-MM-dd"), //Training date

					color = "blue" //blue for training
				});

            // Add the training events to the collection of all events
            events.AddRange(training);

			//return event as JSON
			return Json(events);
		}

	}
}
