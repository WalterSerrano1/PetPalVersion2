using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetPal.Models;
using PetPal.Data;

namespace PetPal.Controllers
{
	public class TrainingController : Controller
	{
		private readonly AppDbContext _context;

		public TrainingController(AppDbContext ctx)
		{
			_context = ctx;
		}

		// GET: Training/Upsert?petId=2&id=5
		[HttpGet]
		public async Task<IActionResult> Training(int petId, int? id)
		{
			Training training;

			if (id == null)
			{
				var pet = await _context.Pet.FirstOrDefaultAsync(p => p.PetId == petId);
				if (pet == null) return NotFound();

				training = new Training
				{
					PetId = petId,
					PetName = pet.PetName,
					TrainingDate = DateTime.Today
				};
			}
			else
			{
				training = await _context.Training
					.Include(t => t.Pet)
					.FirstOrDefaultAsync(t => t.TrainingId == id);

				if (training == null) return NotFound();
				training.PetName = training.Pet?.PetName;
			}

			return View("Training", training);
		}

		// POST: Training/Upsert
		[HttpPost]
		public async Task<IActionResult> Training(Training training)
		{
			if (ModelState.IsValid)
			{
				if (training.TrainingId == 0)
				{
					_context.Training.Add(training);
				}
				else
				{
					_context.Training.Update(training);
				}

				await _context.SaveChangesAsync();
				return RedirectToAction("PetDetails", "Pets", new { id = training.PetId });
			}

			// fallback to set PetName for redisplay
			training.PetName ??= (await _context.Pet.FindAsync(training.PetId))?.PetName;
			return View("Training", training);
		}

		// GET: Training/Delete/{id}
		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			var training = await _context.Training
				.Include(t => t.Pet)
				.FirstOrDefaultAsync(t => t.TrainingId == id);

			if (training == null) return NotFound();

			training.PetName = training.Pet.PetName;
			return View(training);
		}

		// POST: Training/DeleteConfirmed
		[HttpPost, ActionName("Delete")]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var training = await _context.Training.FindAsync(id);
			if (training != null)
			{
				_context.Training.Remove(training);
				await _context.SaveChangesAsync();
			}

			return RedirectToAction("PetDetails", "Pets", new { id = training.PetId });
		}

		// GET: Training/GetDetailsPartial/{id}
		[HttpGet]
		public async Task<IActionResult> GetDetailsPartial(int id)
		{
			var training = await _context.Training
				.Include(t => t.Pet)
				.FirstOrDefaultAsync(t => t.TrainingId == id);

			if (training == null) return NotFound();

			return PartialView("TrainingDetails", training);
		}
	}
}
