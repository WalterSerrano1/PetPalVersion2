using Microsoft.AspNetCore.Mvc;

namespace PetManagement.Controllers
{
	public class PetsController : Controller
	{
		public IActionResult PetDetails()
		{
			return View();
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