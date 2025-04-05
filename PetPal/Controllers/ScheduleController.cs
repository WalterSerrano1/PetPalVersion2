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

			if (id == null)
			{
				var pet = await _context.Pet.FirstOrDefaultAsync(p => p.PetId == petId);
				if (pet == null) return NotFound();

				schedule = new Schedule
				{
					PetId = petId,
					PetName = pet.PetName,
					ScheduleDate = DateTime.Today
				};
			}
			else
			{
				schedule = await _context.Schedules
					.Include(s => s.Pet)
					.FirstOrDefaultAsync(s => s.ScheduleId == id);

				if (schedule == null) return NotFound();
				schedule.PetName = schedule.Pet?.PetName;
			}

			return View("Schedule", schedule);
		}

		// POST: Upsert (Create/Edit) Schedule
		[HttpPost]
		public async Task<IActionResult> Schedule(Schedule schedule)
		{
			if (ModelState.IsValid)
			{
				if (schedule.ScheduleId == 0)
				{
					_context.Schedules.Add(schedule);
				}
				else
				{
					_context.Schedules.Update(schedule);
				}

				await _context.SaveChangesAsync();
				return RedirectToAction("PetDetails", "Pets", new { id = schedule.PetId });
			}

			schedule.PetName ??= (await _context.Pet.FindAsync(schedule.PetId))?.PetName;
			return View("Schedule", schedule);
		}

		[HttpPost]
		public async Task<IActionResult> Delete(int id)
		{
			var schedule = await _context.Schedules.FindAsync(id);
			if (schedule == null) return NotFound();

			_context.Schedules.Remove(schedule);
			await _context.SaveChangesAsync();

			return RedirectToAction("PetDetails", "Pets", new { id = schedule.PetId });
		}

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
