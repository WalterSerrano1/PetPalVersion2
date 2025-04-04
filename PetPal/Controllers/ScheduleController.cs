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

		// GET: Create schedule for a pet
		[HttpGet]
		public async Task<IActionResult> Create(int petId)
		{
			var pet = await _context.Pet.FirstOrDefaultAsync(p => p.PetId == petId);
			if (pet == null) return NotFound();

			var schedule = new Schedule
			{
				PetId = petId,
				PetName = pet.PetName,
				ScheduleDate = DateTime.Today
			};

			return View("Schedule", schedule);
		}

		// POST: Save new schedule
		[HttpPost]
		public async Task<IActionResult> Create(Schedule schedule)
		{
			if (ModelState.IsValid)
			{
				_context.Schedules.Add(schedule);
				await _context.SaveChangesAsync();
				return RedirectToAction("PetDetails", "Pets", new { id = schedule.PetId });
			}

			// Reload PetName if validation fails
			schedule.PetName ??= (await _context.Pet.FindAsync(schedule.PetId))?.PetName;
			return View("Schedule", schedule);
		}

		// GET: Edit existing schedule
		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			var schedule = await _context.Schedules
				.Include(s => s.Pet)
				.FirstOrDefaultAsync(s => s.ScheduleId == id);

			if (schedule == null) return NotFound();

			schedule.PetName = schedule.Pet?.PetName;
			return View("Schedule", schedule);
		}

		// POST: Save schedule changes
		[HttpPost]
		public async Task<IActionResult> Edit(int id, Schedule schedule)
		{
			if (id != schedule.ScheduleId) return NotFound();

			if (ModelState.IsValid)
			{
				_context.Schedules.Update(schedule);
				await _context.SaveChangesAsync();
				return RedirectToAction("PetDetails", "Pets", new { id = schedule.PetId });
			}

			schedule.PetName ??= (await _context.Pet.FindAsync(schedule.PetId))?.PetName;
			return View("Schedule", schedule);
		}

		// POST: Delete a schedule
		[HttpPost]
		public async Task<IActionResult> Delete(int id)
		{
			var schedule = await _context.Schedules.FindAsync(id);
			if (schedule == null) return NotFound();

			_context.Schedules.Remove(schedule);
			await _context.SaveChangesAsync();

			return RedirectToAction("PetDetails", "Pets", new { id = schedule.PetId });
		}

		// GET: Fetch schedules for a specific pet
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
