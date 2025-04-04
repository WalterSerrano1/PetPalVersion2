using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetPal.Models;
using PetPal.Data;

namespace PetPal.Controllers
{
	public class TrainingController : Controller
	{
		private AppDbContext _context { get; set; }
		public TrainingController(AppDbContext ctx)
		{
			_context = ctx;
		}

		// GET; Training/Create/{petId}
		[HttpGet]
		public IActionResult Create(int petId)
		{
			var pet = _context.Pet.FirstOrDefault(p => p.PetId == petId);
			if (pet == null) return NotFound();

			//prefill PetId and PetName
			var training = new Training
			{
				PetId = petId,
				PetName = pet.PetName,
				TrainingDate = DateTime.Today
			};
			return View(training);
		}

		// POST: Training/Create
		[HttpPost]
		public async Task<IActionResult> Create(Training training)
		{
			if (ModelState.IsValid)
			{
				_context.Training.Add(training);
				await _context.SaveChangesAsync();
				return RedirectToAction("PetDetails", "Pets", new { id = training.PetId });
			}

			return View(training);
		
		}

		// GET: Training/Edit/{id}
		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			var training = await _context.Training
				.Include(t => t.Pet)
				.FirstOrDefaultAsync(t => t.TrainingId == id);

			if (training == null) return NotFound();

			training.PetName = training.Pet.PetName; // Set PetName from the related Pet entity
			return View("Training");
		}

		// POST: Training/Edit/{id}
		[HttpPost]
		public async Task<IActionResult> Edit(Training training)
		{
			if (ModelState.IsValid)
			{
				_context.Update(training);
				await _context.SaveChangesAsync();
				return RedirectToAction("PetDetails", "Pets", new { id = training.PetId });
			}
			return View(training);
		}

		// GET: Training/Delete/{id}
		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			var training = await _context.Training
				.Include(t => t.Pet)
				.FirstOrDefaultAsync(t => t.TrainingId == id);
			if (training == null) return NotFound();
			training.PetName = training.Pet.PetName; // Set PetName from the related Pet entity
			return View(training);
		}

		// POST: Training/Delete/{id}
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

		// GET: Training/Details/{id}
		[HttpGet]
		public async Task<IActionResult> GetDetailsPartial(int id)
		{
			var training = await _context.Training
				.Include(t => t.Pet)
				.FirstOrDefaultAsync(t => t.TrainingId == id);

			if (training == null) return NotFound();

			return PartialView("" + "TrainingDetails", training);
		}


	}
}
