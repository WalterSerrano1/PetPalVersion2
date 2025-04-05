using Microsoft.AspNetCore.Mvc;
using PetPal.Data;
using PetPal.Models;
using Microsoft.EntityFrameworkCore;

namespace PetPal.Controllers
{
	public class ScheduleController : Controller
	{
		private readonly AppDbContext _context;

		public ScheduleController(AppDbContext context)
		{
			_context = context;
		}

		// GET: Upsert (Create/Edit) Schedule
		[HttpGet]
		public async Task<IActionResult> Schedule(int petId, int? id)
		{
			Schedule schedule;

            // Create new schedule scenario (id is null)
            if (id == null)
			{
                // Get the pet to associate with the new schedule
                var pet = await _context.Pet.FirstOrDefaultAsync(p => p.PetId == petId);
				if (pet == null) return NotFound();

                // Initialize a new schedule with pet info and today's date
                schedule = new Schedule
				{
					PetId = petId,
					PetName = pet.PetName,
					ScheduleDate = DateTime.Today
				};
			}
            // Edit existing schedule
            else
            {
                // Retrieve the existing schedule with related pet data
                schedule = await _context.Schedules
					.Include(s => s.Pet)
					.FirstOrDefaultAsync(s => s.ScheduleId == id);
				if (schedule == null) return NotFound();

                // Set pet name from related pet entity
                schedule.PetName = schedule.Pet?.PetName;
			}

            // Return the view with the schedule model
            return View("Schedule", schedule);
		}

		// POST: Upsert (Create/Edit) Schedule
		[HttpPost]
		public async Task<IActionResult> Schedule(Schedule schedule)
		{
			if (ModelState.IsValid)
			{
                // if 0 is add operation
                if (schedule.ScheduleId == 0)
				{
					//add schedule
					_context.Schedules.Add(schedule);
				}
				else
				{	
					//update existing schedule
					_context.Schedules.Update(schedule);
				}

				//save changes to db
				await _context.SaveChangesAsync();

                // Redirect to pet details page
                return RedirectToAction("PetDetails", "Pets", new { id = schedule.PetId });
			}

            // If model validation fails, ensure PetName is populated
            schedule.PetName ??= (await _context.Pet.FindAsync(schedule.PetId))?.PetName;

            // Redisplay the form with validation errors
            return View("Schedule", schedule);
		}

		//delete schedule
		[HttpPost]
		public async Task<IActionResult> Delete(int id)
		{
			//find schedule to delete
			var schedule = await _context.Schedules.FindAsync(id);
			if (schedule == null) return NotFound();

			//remove from database
			_context.Schedules.Remove(schedule);
			await _context.SaveChangesAsync();

            // Redirect to pet details page
            return RedirectToAction("PetDetails", "Pets", new { id = schedule.PetId });
		}

        // Get all schedules for a specific pet (used for AJAX partial view loading)
        [HttpGet]
		public async Task<IActionResult> GetSchedule(int petId)
		{
			var schedules = await _context.Schedules
				.Where(s => s.PetId == petId)
				.ToListAsync();

			return PartialView("FeedingSchedule", schedules);
		}
	}
}
