using Microsoft.AspNetCore.Mvc;
using PetPal.Models;
using Microsoft.EntityFrameworkCore;
using PetPal.Data;

namespace PetManagement.Controllers
{
	public class PetsController : Controller
	{
		private AppDbContext context { get; set; }
		public PetsController(AppDbContext ctx)
		{
			context = ctx;
		}

		[HttpGet]
		public IActionResult PetDetails(int id)
		{
			ViewBag.action = "PetDetails";

			var pet = context.Pet
				.Include(p => p.User)
				.FirstOrDefault(p => p.PetId == id);

			if (pet == null)
			{
				return NotFound();
			}

			return View(pet); 
		}
		public IActionResult Appointment()
		{
			return View();
		}
		public IActionResult Schedule()
		{
			return View();
		}
		public IActionResult Training()
		{
			return View();
		}
		public IActionResult AddPet()
		{
			return View();
		}

	}
}